using g_vendas.Controllers;
using g_vendas.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace g_vendas.UI
{
    public class FormVerMais : Form
    {
        private Panel panelFundo, panelBranco, panelConteudo, panelPago, panelValores;
        private Label lblTitulo, lblPizza, lblSabores, lblPago, lblPassado, lblRefrigerante, lblQual, lblValores, lblValoresP, lblValoresR;
        private Button btnVoltar, btnExportar;

        public FormVerMais(int idVenda)
        {
            FormUtils.SetIcon(this, "Pizza-Da-Grá.ico");

            // Busca os dados
            var venda = new VendasController().BuscarVendaPorId(idVenda);
            var itensVenda = new ItensVendasController().BuscarItensPorVenda(idVenda) as List<ItensVendas>;
            var listaPizzaSabores = new PizzaSaboresController().ObterTodosSabores(); // pizza_sabores
            var listaSaboresPizza = new SaboresPizzaController().searchAllFl(); // sabores_pizza

            string tamanho = "";
            string textoSabores = "";
            string textoProporcoes = "";
            decimal valorPizza = 0;
            string bebida = "Não";
            string refriNome = "Nenhum";
            decimal valorRefri = 0;

            if (itensVenda != null)
            {
                var itemPizza = itensVenda.FirstOrDefault(item =>
                    !string.IsNullOrEmpty(item.Observacao) &&
                    (item.Observacao.ToLower().Contains("pizza") ||
                     item.Observacao.ToLower().Contains("grande") ||
                     item.Observacao.ToLower().Contains("média") ||
                     item.Observacao.ToLower().Contains("pequena"))
                );

                if (itemPizza != null)
                {
                    // Tamanho
                    if (itemPizza.Observacao.ToLower().Contains("grande")) tamanho = "Grande";
                    else if (itemPizza.Observacao.ToLower().Contains("média")) tamanho = "Média";
                    else if (itemPizza.Observacao.ToLower().Contains("pequena")) tamanho = "Pequena";

                    valorPizza = itemPizza.Preco_Unitario;

                    var nomesSabores = new List<string>();
                    foreach (var ps in listaPizzaSabores)
                    {
                        var sabor = listaSaboresPizza.FirstOrDefault(s => s.id == ps.id_sabor);
                        string nomeSabor = sabor != null ? sabor.nome : $"Sabor {ps.id_sabor}";
                        nomesSabores.Add($"{nomeSabor}: {ps.proporcao}");
                    }
                    textoProporcoes = string.Join("\n", nomesSabores);

                    // Lógica antiga dos sabores:
                    string obs = itemPizza.Observacao;
                    if (!string.IsNullOrEmpty(tamanho))
                        obs = obs.Replace(tamanho, "").Trim();

                    var sabores = obs.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim())
                        .Where(s => !string.IsNullOrWhiteSpace(s))
                        .ToList();
                    textoSabores = string.Join("\n", sabores);
                }

                // Bebida
                var itemBebida = itensVenda.FirstOrDefault(item =>
                    !string.IsNullOrEmpty(item.Observacao) &&
                    (item.Observacao.ToLower().Contains("coca") ||
                     item.Observacao.ToLower().Contains("guaraná") ||
                     item.Observacao.ToLower().Contains("fanta"))
                );
                if (itemBebida != null)
                {
                    bebida = "Sim";
                    refriNome = itemBebida.Observacao;
                    valorRefri = itemBebida.Preco_Unitario;
                }
            }

            // Layout igual ao print
            this.Text = "Informações do pedido";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = true;
            this.BackColor = Color.FromArgb(240, 240, 240);

            // Painel de fundo rosado
            panelFundo = new Panel
            {
                BackColor = Color.FromArgb(185, 154, 134),
                Location = new Point(60, 40),
                Size = new Size(780, 570)
            };
            this.Controls.Add(panelFundo);

            // Título
            lblTitulo = new Label
            {
                Text = "Informações do pedido:",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.Black,
                Location = new Point(30, 20),
                AutoSize = true
            };
            panelFundo.Controls.Add(lblTitulo);

            // Painel branco central
            panelBranco = new Panel
            {
                BackColor = Color.White,
                Location = new Point(30, 60),
                Size = new Size(720, 470)
            };
            panelFundo.Controls.Add(panelBranco);

            // Painel de conteúdo cinza claro
            panelConteudo = new Panel
            {
                BackColor = Color.FromArgb(230, 230, 230),
                Location = new Point(30, 35),
                Size = new Size(660, 400)
            };
            panelBranco.Controls.Add(panelConteudo);

            // Botão Voltar (canto inferior esquerdo do painel marrom)
            btnVoltar = new Button
            {
                Text = "◀ Voltar",
                BackColor = Color.FromArgb(236, 179, 138),
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(130, 38),
                Location = new Point(20, panelFundo.Height - 58)
            };
            btnVoltar.FlatAppearance.BorderSize = 0;
            btnVoltar.Click += (s, e) =>
            {
                FormRelatorioHome form = new FormRelatorioHome();
                form.Show();
                this.Hide();
            };
            panelFundo.Controls.Add(btnVoltar);

            // Botão Exportar (verde)
            btnExportar = new Button
            {
                Text = "Exportar como .xlsx",
                BackColor = ColorTranslator.FromHtml("#21B421"),
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(180, 38),
                Location = new Point(panelConteudo.Width - 190, 20)
            };
            btnExportar.FlatAppearance.BorderSize = 0;
            btnExportar.Click += (s, e) => new VendasController().ExportarVendas(new List<Venda> { venda });
            panelConteudo.Controls.Add(btnExportar);

            // Labels de informações (lado esquerdo)
            int xEsq = 30, yBase = 30, yEspaco = 40;
            lblPizza = new Label
            {
                Text = $"Pizza: {tamanho}",
                Font = new Font("Segoe UI", 13, FontStyle.Regular),
                Location = new Point(xEsq, yBase),
                AutoSize = true
            };
            panelConteudo.Controls.Add(lblPizza);

            lblSabores = new Label
            {
                Text = $"Sabores:\n{textoSabores}",
                Font = new Font("Segoe UI", 13, FontStyle.Regular),
                Location = new Point(xEsq, yBase + yEspaco),
                AutoSize = false,
                Size = new Size(280, 80)
            };
            panelConteudo.Controls.Add(lblSabores);

            // Painel "Pago: Sim" (verde)
            panelPago = new Panel
            {
                BackColor = ColorTranslator.FromHtml("#21B421"),
                Location = new Point(xEsq, yBase + 2 * yEspaco + 60),
                Size = new Size(270, 170)
            };
            panelConteudo.Controls.Add(panelPago);

            lblPago = new Label
            {
                Text = $"Pago: Sim",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(12, 10),
                AutoSize = true
            };
            panelPago.Controls.Add(lblPago);

            lblPassado = new Label
            {
                Text = $"Passado no {venda.FormaPagamento}\nvalor de: R${venda.valor_total:F2}\n com desconto de : R${venda.desconto * 100}\n ficando no final valor de R${Math.Round(venda.ValorFinal, 2)}",
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = Color.White,
                Location = new Point(12, 35),
                AutoSize = true
            };
            panelPago.Controls.Add(lblPassado);

            int xDir = 330, yDir = 30;

            lblRefrigerante = new Label
            {
                Text = $"Refrigerante: {bebida}",
                Font = new Font("Segoe UI", 13, FontStyle.Regular),
                Location = new Point(xDir, yDir + yEspaco),
                AutoSize = true
            };
            panelConteudo.Controls.Add(lblRefrigerante);

            lblQual = new Label
            {
                Text = $"Qual? {refriNome}",
                Font = new Font("Segoe UI", 13, FontStyle.Regular),
                Location = new Point(xDir, yDir + 2 * yEspaco),
                AutoSize = true
            };
            panelConteudo.Controls.Add(lblQual);

            // Painel valores individuais (laranja)
            panelValores = new Panel
            {
                BackColor = Color.FromArgb(236, 179, 138),
                Location = new Point(xDir, yDir + 3 * yEspaco + 10),
                Size = new Size(200, 70)
            };
            panelConteudo.Controls.Add(panelValores);

            lblValores = new Label
            {
                Text = "Valores individuais:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.Black,
                Location = new Point(10, 5),
                AutoSize = true
            };
            panelValores.Controls.Add(lblValores);

            lblValoresP = new Label
            {
                Text = $"P: R${valorPizza:F2}",
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = Color.Black,
                Location = new Point(10, 28),
                AutoSize = true
            };
            panelValores.Controls.Add(lblValoresP);

            lblValoresR = new Label
            {
                Text = $"R: R${valorRefri:F2}",
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = Color.Black,
                Location = new Point(100, 28),
                AutoSize = true
            };
            panelValores.Controls.Add(lblValoresR);

            this.Resize += (s, e) => AtualizarLayout();
            AtualizarLayout();
        }

        private void AtualizarLayout()
        {
            if (panelFundo == null || panelBranco == null || panelConteudo == null ||
                btnVoltar == null || btnExportar == null ||
                lblTitulo == null || lblPizza == null || lblSabores == null ||
                panelPago == null || lblPago == null || lblPassado == null ||
                lblRefrigerante == null || lblQual == null ||
                panelValores == null || lblValores == null || lblValoresP == null || lblValoresR == null)
                return;

            int margem = 30;
            int larguraPanelFundo = this.ClientSize.Width - 2 * margem;
            int alturaPanelFundo = this.ClientSize.Height - 2 * margem;

            panelFundo.Location = new Point(margem, margem);
            panelFundo.Size = new Size(larguraPanelFundo, alturaPanelFundo);

            int margemInterna = (int)(panelFundo.Height * 0.08f);
            int larguraPanelBranco = (int)(panelFundo.Width * 0.92f);
            int alturaPanelBranco = (int)(panelFundo.Height * 0.80f);

            panelBranco.Location = new Point((panelFundo.Width - larguraPanelBranco) / 2, margemInterna);
            panelBranco.Size = new Size(larguraPanelBranco, alturaPanelBranco);

            int larguraPanelConteudo = (int)(panelBranco.Width * 0.92f);
            int alturaPanelConteudo = (int)(panelBranco.Height * 0.85f);

            panelConteudo.Location = new Point((panelBranco.Width - larguraPanelConteudo) / 2, (int)(panelBranco.Height * 0.08f));
            panelConteudo.Size = new Size(larguraPanelConteudo, alturaPanelConteudo);

            btnVoltar.Size = new Size(130, 38);
            btnVoltar.Location = new Point(20, panelFundo.Height - btnVoltar.Height - 20);

            btnExportar.Location = new Point(panelConteudo.Width - btnExportar.Width - 10, 20);
            btnExportar.Size = new Size(180, 38);

            int xEsq = 30, yBase = 30, yEspaco = 40;
            lblPizza.Location = new Point(xEsq, yBase);
            lblSabores.Location = new Point(xEsq, yBase + yEspaco);

            panelPago.Location = new Point(xEsq, yBase + 2 * yEspaco + 60);
            panelPago.Size = new Size(270, 170);

            lblPago.Location = new Point(12, 10);
            lblPassado.Location = new Point(12, 35);

            int xDir = panelConteudo.Width / 2 + 30;
            int yDir = 30;

            lblRefrigerante.Location = new Point(xDir, yDir + yEspaco);
            lblQual.Location = new Point(xDir, yDir + 2 * yEspaco);

            panelValores.Location = new Point(xDir, yDir + 3 * yEspaco + 10);
            panelValores.Size = new Size(200, 70);

            lblValores.Location = new Point(10, 5);
            lblValoresP.Location = new Point(10, 28);
            lblValoresR.Location = new Point(100, 28);

            lblTitulo.Location = new Point(30, 20);
        }
    }
}
