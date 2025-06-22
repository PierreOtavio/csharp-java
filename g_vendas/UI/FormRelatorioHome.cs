using g_vendas.Controllers;
using g_vendas.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace g_vendas.UI
{
    public class FormRelatorioHome : Form
    {
        private Panel panelFundo;
        private Panel panelCardsContainer;
        private Button btnExportar, btnVoltar;
        private List<Panel> cards = new List<Panel>();
        private VendasController vendasController = new VendasController();
        private ItensVendasController itensController = new ItensVendasController();

        public FormRelatorioHome()
        {
            InicializarComponentes();
            this.Resize += FormRelatorioHome_Resize;
            AtualizarLayout();
            CarregarVendas();
        }


        private void InicializarComponentes()
        {
            FormUtils.SetIcon(this, "Pizza-Da-Grá.ico");

            this.Text = "Relatório Mensal";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = true;
            this.BackColor = Color.FromArgb(240, 240, 240);

            // Painel de fundo
            panelFundo = new Panel
            {
                BackColor = Color.FromArgb(185, 154, 134),
                //Dock = DockStyle.Fill
            };
            this.Controls.Add(panelFundo);

            // Título
            var lblTitulo = new Label
            {
                Text = "Relatório mensal:",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.Black,
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
                Location = new Point(10, 5)
            };
            panelFundo.Controls.Add(lblTitulo);

            // Botão Exportar (topo esquerdo)
            btnExportar = new Button
            {
                Text = "Exportar como .xlsx",
                BackColor = ColorTranslator.FromHtml("#21B421"),
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 12, FontStyle.Italic),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(200, 40),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(10, 5)
            };
            btnExportar.Click += BtnExportar_Click;
            panelFundo.Controls.Add(btnExportar);

            // Painel branco dos cards
            panelCardsContainer = new Panel
            {
                BackColor = Color.White,
                Location = new Point(30, 80),
                Size = new Size(820, 560),
                BorderStyle = BorderStyle.None,
                AutoScroll = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            panelFundo.Controls.Add(panelCardsContainer);

            // Botão Voltar (inferior esquerdo)
            btnVoltar = new Button
            {
                Text = "◀ Voltar",
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                BackColor = Color.FromArgb(236, 179, 138),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                //FlatAppearance = { BorderSize = 2 },
                Cursor = Cursors.Hand,
                Size = new Size(120, 40),
                Location = new Point(30, panelFundo.Height - 60) // Será ajustado no layout
            };
            btnVoltar.Click += (s, e) =>
            {
                FormMainMenu formAnterior = new FormMainMenu();
                formAnterior.Show();
                this.Hide();
            };
            panelFundo.Controls.Add(btnVoltar);


            this.Resize += (s, e) => AtualizarLayout();
        }

        private void CarregarVendas()
        {
            panelCardsContainer.Controls.Clear();
            cards.Clear();

            List<Venda> vendas;
            try
            {
                vendas = vendasController.ListarVendas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar vendas: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (vendas == null || vendas.Count == 0)
            {
                var lbl = new Label
                {
                    Text = "Nenhuma venda encontrada.",
                    Font = new Font("Segoe UI", 14, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Location = new Point(20, 30)
                };
                panelCardsContainer.Controls.Add(lbl);
                return;
            }

            int y = 20;
            foreach (var venda in vendas)
            {
                List<ItensVendas> itensVenda;
                try
                {
                    itensVenda = itensController.BuscarItensPorVenda(venda.id_venda) as List<ItensVendas>;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao buscar itens da venda {venda.id_venda}: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue;
                }

                var card = CriarCardVenda(venda, itensVenda, y);
                panelCardsContainer.Controls.Add(card);
                cards.Add(card);
                y += card.Height + 20;
            }
        }

        private Panel CriarCardVenda(Venda venda, List<ItensVendas> itensVenda, int posY)
        {
            string saboresETamanho = "";
            string bebida = "";

            foreach (var item in itensVenda)
            {
                if (string.IsNullOrWhiteSpace(item.Observacao)) continue;
                var obs = item.Observacao.ToLower();

                // Pizza: contém tamanho
                if (obs.Contains("grande") || obs.Contains("média") || obs.Contains("pequena"))
                    saboresETamanho = item.Observacao; // Ex: "Frango/Calabresa Grande"

                // Refrigerante
                if (obs.Contains("coca") || obs.Contains("guaraná") || obs.Contains("fanta"))
                    bebida = item.Observacao; // Ex: "Coca 2L"
            }

            // Monta o card
            var card = new Panel
            {
                BackColor = Color.FromArgb(225, 220, 220),
                Size = new Size(panelCardsContainer.Width - 60, 90),
                Location = new Point(30, posY)
            };

            // Sabores e tamanho (esquerda)
            var lblDescricao = new Label
            {
                Text = saboresETamanho,
                Font = new Font("Segoe UI", 14, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleLeft,
                AutoSize = false,
                Size = new Size(card.Width / 2, card.Height - 20),
                Location = new Point(10, 10)
            };
            card.Controls.Add(lblDescricao);

            // Bebida e valor (direita)
            var lblAdicional = new Label
            {
                Text = (string.IsNullOrEmpty(bebida) ? "" : bebida + "\n") + $"R${venda.ValorFinal:F2}",
                Font = new Font("Segoe UI", 14, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleRight,
                AutoSize = false,
                Size = new Size(card.Width / 3, card.Height - 20),
                Location = new Point(card.Width - (card.Width / 3) - 160, 10)
            };
            card.Controls.Add(lblAdicional);

            // Botão "Ver mais"
            var btnVerMais = new Button
            {
                Text = "Ver mais",
                BackColor = ColorTranslator.FromHtml("#9F4848"),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Italic),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(140, 42),
                Location = new Point(card.Width - 150, (card.Height - 42) / 2),
                Tag = venda
            };
            btnVerMais.Click += BtnVerMais_Click;
            card.Controls.Add(btnVerMais);

            return card;
        }



        private void BtnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                vendasController.ExportarVendas();
                MessageBox.Show("Relatório exportado com sucesso!", "Exportação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao exportar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnVerMais_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var venda = btn.Tag as Venda;
            FormVerMais formVerMais = new FormVerMais(venda.id_venda);
            formVerMais.Show();
            this.Hide();
        }

        private void FormRelatorioHome_Resize(object sender, EventArgs e)
        {
            AtualizarLayout();
        }

        private void AtualizarLayout()
        {
            // Defina a margem desejada para o espaço branco (por exemplo, 30px)
            int margem = 30;

            // Ajuste o tamanho do panelFundo para ficar menor que o formulário
            int larguraPanelFundo = this.ClientSize.Width - 2 * margem;
            int alturaPanelFundo = this.ClientSize.Height - 2 * margem;

            // Centralize o panelFundo
            panelFundo.Location = new Point(margem, margem);
            panelFundo.Size = new Size(larguraPanelFundo, alturaPanelFundo);

            // Agora ajuste o panelBranco dentro do panelFundo normalmente
            int margemInterna = (int)(panelFundo.Height * 0.08f);
            int larguraPanelBranco = (int)(panelFundo.Width * 0.92f);
            int alturaPanelBranco = (int)(panelFundo.Height * 0.80f);

            panelCardsContainer.Location = new Point((panelFundo.Width - larguraPanelBranco) / 2, margemInterna);
            panelCardsContainer.Size = new Size(larguraPanelBranco, alturaPanelBranco);

            // Label Relatório no canto superior esquerdo fixo
            var lblRelatorio = panelFundo.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "labelRelatorio");
            if (lblRelatorio != null)
            {
                lblRelatorio.Location = new Point(30, panelFundo.Height - lblRelatorio.Height - 70);
                lblRelatorio.AutoSize = false;
            }

            // Botão Exportar no canto superior direito fixo
            btnExportar.Location = new Point(panelFundo.Width - btnExportar.Width - 30, 10);

            // Botão Voltar no canto inferior esquerdo fixo
            var btnVoltar = panelFundo.Controls.OfType<Button>().FirstOrDefault(b => b.Text == "◀ Voltar");
            if (btnVoltar != null)
            {
                btnVoltar.Location = new Point(30, panelFundo.Height - btnVoltar.Height - 30);
            }

            // Painel de cards com rolagem proporcional dentro do painel marrom
            //int margemInterna = 30;
            int topoCards = lblRelatorio != null ? lblRelatorio.Bottom + 20 : 60;
            int alturaPanelCards = panelFundo.Height - topoCards - btnVoltar.Height - 50;
            int larguraPanelCards = panelFundo.Width - 2 * margemInterna;

            panelCardsContainer.Location = new Point(margemInterna, topoCards);
            panelCardsContainer.Size = new Size(larguraPanelCards, alturaPanelCards);
            panelCardsContainer.AutoScroll = true; // Habilita rolagem

            // Reposiciona os cards dentro do painel de cards
            int y = margemInterna / 2;
            foreach (var card in cards)
            {
                card.Size = new Size(panelCardsContainer.Width - margemInterna, 90);
                card.Location = new Point(margemInterna / 2, y);

                // Ajusta labels e botão
                if (card.Controls.Count >= 3)
                {
                    var lblDescricao = card.Controls[0] as Label;
                    var lblAdicional = card.Controls[1] as Label;
                    var btnVerMais = card.Controls[2] as Button;

                    lblDescricao.Size = new Size(card.Width / 2, card.Height - 20);
                    lblAdicional.Size = new Size(card.Width / 3, card.Height - 20);
                    lblAdicional.Location = new Point(card.Width - (card.Width / 3) - 160, 10);
                    btnVerMais.Location = new Point(card.Width - 150, (card.Height - 42) / 2);
                }

                y += card.Height + margemInterna / 2;
            }
        }

    }
}
