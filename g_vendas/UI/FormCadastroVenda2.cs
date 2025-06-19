using System;
using System.Drawing;
using System.Windows.Forms;

namespace g_vendas.UI
{
    public class FormCadastroVenda2 : Form
    {
        private Panel panelFundo, panelBranco;
        private Label lblTitulo;
        private Panel linhaVertical;
        private Button btnCancelar, btnProximo;
        private ListBox listaRefrigerante, listaQuantidade, listaPagamento, listaDesconto;

        public FormCadastroVenda2()
        {
            //Definir Ícone:
            FormUtils.SetIcon(this, "Pizza-Da-Grá.ico");

            // Configurações do formulário principal
            this.Text = "Cadastrar venda";
            this.Size = new Size(900, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.BackColor = Color.FromArgb(236, 234, 234);

            // Painel de fundo (rosado)
            panelFundo = new Panel();
            panelFundo.BackColor = Color.FromArgb(185, 154, 134, 134);
            panelFundo.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(panelFundo);

            // Painel branco central
            panelBranco = new Panel();
            panelBranco.BackColor = Color.White;
            panelBranco.BorderStyle = BorderStyle.FixedSingle;
            panelFundo.Controls.Add(panelBranco);

            // Label título
            lblTitulo = new Label();
            lblTitulo.Text = "Cadastrar venda:";
            lblTitulo.Font = new Font("Segoe UI", 22, FontStyle.Regular);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.AutoSize = true;
            panelFundo.Controls.Add(lblTitulo);

            // Linha divisória central
            linhaVertical = new Panel();
            linhaVertical.BackColor = Color.FromArgb(185, 154, 134, 134);
            panelBranco.Controls.Add(linhaVertical);

            // ----------- COLUNA ESQUERDA -----------
            // Refrigerante
            listaRefrigerante = AdicionarGrupoLista(panelBranco, "Refrigerante:", new string[] { "Nenhum", "Coca-Cola", "Fanta Laranja", "Guaraná Antártica" }, 0, 0, 0, 0);
            // Quantidade
            listaQuantidade = AdicionarGrupoLista(panelBranco, "Quantidade:", new string[] { "1,5L", "2L" }, 0, 0, 0, 0);

            // ----------- COLUNA DIREITA -----------
            // Pago com
            listaPagamento = AdicionarGrupoLista(panelBranco, "Pago com:", new string[] { "Cartão", "PIX", "Dinheiro" }, 0, 0, 0, 0);
            // Desconto
            listaDesconto = AdicionarGrupoLista(panelBranco, "Desconto:", new string[] { "Sim", "Não" }, 0, 0, 0, 0);

            listaDesconto.SelectedIndexChanged += (s, e) =>
            {
                if (listaDesconto.SelectedItem != null && listaDesconto.SelectedItem.ToString().ToLower() == "sim")
                {
                    using (FormDesconto modal = new FormDesconto())
                    {
                        modal.ShowDialog(this);
                    }
                    // Sempre retorna para a página inicial, independente do botão
                    FormMainMenu paginaInicial = new FormMainMenu();
                    paginaInicial.Show();
                    this.Close();
                }
            };

            // ----------- BOTÕES -----------
            btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            btnCancelar.BackColor = Color.FromArgb(220, 40, 40);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.Click += (s, e) => this.Close();
            panelBranco.Controls.Add(btnCancelar);

            btnProximo = new Button();
            btnProximo.Text = "Próximo";
            btnProximo.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            btnProximo.BackColor = Color.FromArgb(40, 180, 40);
            btnProximo.ForeColor = Color.White;
            btnProximo.FlatStyle = FlatStyle.Flat;
            btnProximo.FlatAppearance.BorderSize = 0;
            btnProximo.Cursor = Cursors.Hand;
            // btnProximo.Click += (s, e) => { /* ação do próximo */ };
            panelBranco.Controls.Add(btnProximo);

            // Associa o evento de redimensionamento
            this.Resize += FormCadastroVenda2_Resize;
            // Posiciona os controles inicialmente
            AtualizarLayout();
        }

        private void FormCadastroVenda2_Resize(object sender, EventArgs e)
        {
            AtualizarLayout();
        }

        private void AtualizarLayout()
        {
            // Margens relativas
            int margem = (int)(this.ClientSize.Width * 0.05f); // 5% da largura da janela
            int alturaPanelFundo = (int)(this.ClientSize.Height * 0.8f); // 80% da altura da janela
            int larguraPanelFundo = (int)(this.ClientSize.Width * 0.9f); // 90% da largura da janela

            // Painel de fundo
            panelFundo.Location = new Point(margem, (this.ClientSize.Height - alturaPanelFundo) / 2);
            panelFundo.Size = new Size(larguraPanelFundo, alturaPanelFundo);

            // Painel branco central
            int margemInterna = (int)(panelFundo.Height * 0.1f); // 10% da altura do painel de fundo
            int larguraPanelBranco = (int)(panelFundo.Width * 0.9f);
            int alturaPanelBranco = (int)(panelFundo.Height * 0.8f);
            panelBranco.Location = new Point((panelFundo.Width - larguraPanelBranco) / 2, margemInterna);
            panelBranco.Size = new Size(larguraPanelBranco, alturaPanelBranco);

            // Label título
            lblTitulo.Location = new Point(margemInterna, margemInterna / 2);

            // Linha divisória vertical
            linhaVertical.Location = new Point(panelBranco.Width / 2 - 1, (int)(panelBranco.Height * 0.1f));
            linhaVertical.Size = new Size(2, (int)(panelBranco.Height * 0.7f));

            // ----------- COLUNA ESQUERDA -----------
            int xEsquerda = (int)(panelBranco.Width * 0.05f);
            int yInicial = (int)(panelBranco.Height * 0.1f);
            int grupoLargura = (int)(panelBranco.Width * 0.4f);
            int grupoAltura = (int)(panelBranco.Height * 0.3f);
            int grupoAlturaPequena = (int)(panelBranco.Height * 0.2f);

            // Refrigerante
            ReposicionarGrupo(listaRefrigerante, "Refrigerante:", xEsquerda, yInicial, grupoLargura, grupoAltura);

            // Quantidade
            ReposicionarGrupo(listaQuantidade, "Quantidade:", xEsquerda, yInicial + grupoAltura + 20, grupoLargura, grupoAlturaPequena);

            // ----------- COLUNA DIREITA -----------
            int xDireita = panelBranco.Width / 2 + (int)(panelBranco.Width * 0.05f);

            // Pago com
            ReposicionarGrupo(listaPagamento, "Pago com:", xDireita, yInicial, grupoLargura, grupoAltura);

            // Desconto
            ReposicionarGrupo(listaDesconto, "Desconto:", xDireita, yInicial + grupoAltura + 20, grupoLargura, grupoAlturaPequena);

            // ----------- BOTÕES -----------
            int larguraBotao = (int)(panelBranco.Width * 0.2f);
            int alturaBotao = 50;
            int yBotao = panelBranco.Height - alturaBotao - margemInterna / 2;

            btnCancelar.Size = new Size(larguraBotao, alturaBotao);
            btnCancelar.Location = new Point(
                (panelBranco.Width / 2) - larguraBotao - (int)(larguraBotao * 0.1f),
                yBotao
            );

            btnProximo.Size = new Size(larguraBotao, alturaBotao);
            btnProximo.Location = new Point(
                (panelBranco.Width / 2) + (int)(larguraBotao * 0.1f),
                yBotao
            );
        }

        private ListBox AdicionarGrupoLista(Panel panel, string titulo, string[] opcoes, int x, int y, int largura, int altura)
        {
            Panel grupo = new Panel();
            grupo.Location = new Point(x, y);
            grupo.Size = new Size(largura, altura);
            grupo.BackColor = Color.Transparent;
            panel.Controls.Add(grupo);

            Label lblTitulo = new Label();
            lblTitulo.Text = titulo;
            lblTitulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitulo.ForeColor = Color.Black;
            lblTitulo.BackColor = Color.FromArgb(220, 200, 200, 200);
            lblTitulo.Size = new Size(largura, 38);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            grupo.Controls.Add(lblTitulo);

            Label seta = new Label();
            seta.Text = "\u25B2"; // ▲
            seta.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            seta.ForeColor = Color.Black;
            seta.BackColor = Color.Transparent;
            seta.Size = new Size(30, 38);
            seta.Location = new Point(largura - 30, 0);
            seta.TextAlign = ContentAlignment.MiddleCenter;
            grupo.Controls.Add(seta);

            ListBox lista = new ListBox();
            lista.Font = new Font("Segoe UI", 13, FontStyle.Regular);
            lista.Size = new Size(largura, altura - 44);
            lista.Location = new Point(0, 42);
            lista.BorderStyle = BorderStyle.None;
            lista.BackColor = Color.FromArgb(230, 210, 210, 210);
            lista.ItemHeight = 28;
            lista.SelectionMode = SelectionMode.One;
            lista.Items.AddRange(opcoes);
            grupo.Controls.Add(lista);

            return lista;
        }

        private void ReposicionarGrupo(ListBox lista, string titulo, int x, int y, int largura, int altura)
        {
            Panel grupo = (Panel)lista.Parent;
            grupo.Location = new Point(x, y);
            grupo.Size = new Size(largura, altura);

            Label lblTitulo = (Label)grupo.Controls[0];
            lblTitulo.Size = new Size(largura, 38);

            Label seta = (Label)grupo.Controls[1];
            seta.Location = new Point(largura - 30, 0);

            lista.Size = new Size(largura, altura - 44);
        }
    }
}
