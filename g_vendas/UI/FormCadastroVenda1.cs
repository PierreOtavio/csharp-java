using System;
using System.Drawing;
using System.Windows.Forms;

namespace g_vendas.UI
{
    public class FormCadastroVenda : Form
    {
        private Panel panelFundo, panelBranco;
        private Label lblTitulo;
        private Panel linhaVertical;
        private Button btnCancelar, btnProximo;
        private ComboBox cbPizza, cbDivisao;
        private Label lblPizza, lblSaboresNormais, lblDivisao, lblSaboresEspeciais;
        private CheckBox[] chkSaboresNormais, chkSaboresEspeciais;

        public FormCadastroVenda()
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
            this.Controls.Add(panelFundo);

            // Painel branco central
            panelBranco = new Panel();
            panelBranco.BackColor = Color.White;
            panelFundo.Controls.Add(panelBranco);

            // Label título
            lblTitulo = new Label();
            lblTitulo.Text = "Cadastrar venda:";
            lblTitulo.Font = new Font("Segoe UI", 22, FontStyle.Regular);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.AutoSize = true;
            panelFundo.Controls.Add(lblTitulo);

            // Linha divisória vertical
            linhaVertical = new Panel();
            linhaVertical.BackColor = Color.FromArgb(185, 154, 134, 134);
            linhaVertical.Size = new Size(2, 340);
            panelBranco.Controls.Add(linhaVertical);

            // --- Coluna Esquerda ---

            // ComboBox Pizza
            lblPizza = new Label();
            lblPizza.Text = "Pizza:";
            lblPizza.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            lblPizza.AutoSize = true;
            panelBranco.Controls.Add(lblPizza);

            cbPizza = new ComboBox();
            cbPizza.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            cbPizza.DropDownStyle = ComboBoxStyle.DropDownList;
            cbPizza.Items.AddRange(new string[] { "Grande", "Média" });
            cbPizza.SelectedIndex = 0;
            cbPizza.BackColor = Color.FromArgb(230, 210, 210, 210);
            panelBranco.Controls.Add(cbPizza);

            // Sabores Normais
            lblSaboresNormais = new Label();
            lblSaboresNormais.Text = "Sabores Normais:";
            lblSaboresNormais.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            lblSaboresNormais.AutoSize = true;
            panelBranco.Controls.Add(lblSaboresNormais);

            string[] saboresNormais = { "Frango", "Calabresa", "Presunto", "Lombinho", "4 Queijos" };
            chkSaboresNormais = new CheckBox[saboresNormais.Length];
            for (int i = 0; i < saboresNormais.Length; i++)
            {
                chkSaboresNormais[i] = new CheckBox();
                chkSaboresNormais[i].Text = saboresNormais[i];
                chkSaboresNormais[i].Font = new Font("Segoe UI", 12, FontStyle.Regular);
                chkSaboresNormais[i].BackColor = Color.FromArgb(230, 210, 210, 210);
                panelBranco.Controls.Add(chkSaboresNormais[i]);
            }

            // --- Coluna Direita ---

            // ComboBox Divisão
            lblDivisao = new Label();
            lblDivisao.Text = "Divisão:";
            lblDivisao.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            lblDivisao.AutoSize = true;
            panelBranco.Controls.Add(lblDivisao);

            cbDivisao = new ComboBox();
            cbDivisao.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            cbDivisao.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDivisao.Items.AddRange(new string[] { "1", "1/2", "1/4" });
            cbDivisao.SelectedIndex = 0;
            cbDivisao.BackColor = Color.FromArgb(230, 210, 210, 210);
            panelBranco.Controls.Add(cbDivisao);

            // Sabores Especiais
            lblSaboresEspeciais = new Label();
            lblSaboresEspeciais.Text = "Sabores Especiais:";
            lblSaboresEspeciais.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            lblSaboresEspeciais.AutoSize = true;
            panelBranco.Controls.Add(lblSaboresEspeciais);

            string[] saboresEspeciais = { "Atum", "Pepperoni", "Brigadeiro", "Portuguesa", "P. Espanhola" };
            chkSaboresEspeciais = new CheckBox[saboresEspeciais.Length];
            for (int i = 0; i < saboresEspeciais.Length; i++)
            {
                chkSaboresEspeciais[i] = new CheckBox();
                chkSaboresEspeciais[i].Text = saboresEspeciais[i];
                chkSaboresEspeciais[i].Font = new Font("Segoe UI", 12, FontStyle.Regular);
                chkSaboresEspeciais[i].BackColor = Color.FromArgb(230, 210, 210, 210);
                panelBranco.Controls.Add(chkSaboresEspeciais[i]);
            }

            // --- Botões ---

            btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            btnCancelar.BackColor = Color.FromArgb(220, 40, 40);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelar.TextAlign = ContentAlignment.MiddleCenter;
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
            btnProximo.ImageAlign = ContentAlignment.MiddleRight;
            btnProximo.TextAlign = ContentAlignment.MiddleCenter;
            // btnProximo.Click += (s, e) => { /* ação do próximo */ };
            panelBranco.Controls.Add(btnProximo);

            // Associa o evento de redimensionamento
            this.Resize += FormCadastroVenda_Resize;
            // Posiciona os controles inicialmente
            AtualizarLayout();
        }

        private void FormCadastroVenda_Resize(object sender, EventArgs e)
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

            // --- Coluna Esquerda ---

            int xEsquerda = (int)(panelBranco.Width * 0.05f);
            int yInicial = (int)(panelBranco.Height * 0.1f);

            // Pizza
            lblPizza.Location = new Point(xEsquerda, yInicial);
            cbPizza.Size = new Size((int)(panelBranco.Width * 0.4f), 36);
            cbPizza.Location = new Point(xEsquerda, yInicial + 35);

            // Sabores Normais
            lblSaboresNormais.Location = new Point(xEsquerda, yInicial + 90);
            for (int i = 0; i < chkSaboresNormais.Length; i++)
            {
                chkSaboresNormais[i].Size = new Size((int)(panelBranco.Width * 0.35f), 30);
                chkSaboresNormais[i].Location = new Point(xEsquerda, yInicial + 120 + (i * 32));
            }

            // --- Coluna Direita ---

            int xDireita = panelBranco.Width / 2 + (int)(panelBranco.Width * 0.05f);

            // Divisão
            lblDivisao.Location = new Point(xDireita, yInicial);
            cbDivisao.Size = new Size((int)(panelBranco.Width * 0.4f), 36);
            cbDivisao.Location = new Point(xDireita, yInicial + 35);

            // Sabores Especiais
            lblSaboresEspeciais.Location = new Point(xDireita, yInicial + 90);
            for (int i = 0; i < chkSaboresEspeciais.Length; i++)
            {
                chkSaboresEspeciais[i].Size = new Size((int)(panelBranco.Width * 0.35f), 30);
                chkSaboresEspeciais[i].Location = new Point(xDireita, yInicial + 120 + (i * 32));
            }

            // --- Botões ---

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
    }
}
