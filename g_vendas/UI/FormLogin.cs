using System;
using System.Drawing;
using System.Windows.Forms;

namespace g_vendas.UI
{
    public class FormLogin : Form
    {
        private Button btnEntrar;
        private Panel panelFundo, panelLogo, panelLogin;
        private PictureBox picLogo;
        private Label lblBemVindo, lblLogin;

        public FormLogin()
        {
            this.Text = "Login - Pizzaria da Grá";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.BackColor = ColorTranslator.FromHtml("#F4F4F4");

            //Definir Ícone:
            FormUtils.SetIcon(this, "Pizza-Da-Grá.ico");

            // Painel de fundo
            panelFundo = new Panel();
            panelFundo.BackColor = Color.FromArgb(189, 154, 154, 154);
            this.Controls.Add(panelFundo);

            // Painel do logo
            panelLogo = new Panel();
            panelLogo.BackColor = Color.FromArgb(255, 204, 128);
            panelFundo.Controls.Add(panelLogo);

            // Logo
            picLogo = new PictureBox();
            picLogo.Dock = DockStyle.Fill;
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.Image = Image.FromFile(@"C:\Users\perre\OneDrive\Área de Trabalho\Pizzas\Pizza da Grá.png");
            panelLogo.Controls.Add(picLogo);

            // Label de boas-vindas
            lblBemVindo = new Label();
            lblBemVindo.Text = "BEM VINDO AO SISTEMA GERENCIADOR DE\nVENDAS DA PIZZARIA DA GRÁ";
            lblBemVindo.Font = new Font("Segoe UI", 10, FontStyle.Italic);
            lblBemVindo.AutoSize = false;
            lblBemVindo.TextAlign = ContentAlignment.TopCenter;
            panelFundo.Controls.Add(lblBemVindo);

            // Painel de login
            panelLogin = new Panel();
            panelFundo.BackColor = ColorTranslator.FromHtml("#C7A5A5");
            //panelLogin.BackColor = Color.White;
            panelFundo.Controls.Add(panelLogin);

            // Label LOGIN
            lblLogin = new Label();
            lblLogin.Text = "LOGIN";
            lblLogin.Font = new Font("Segoe UI", 20, FontStyle.Regular);
            lblLogin.AutoSize = false;
            panelLogin.BackColor = Color.White;
            lblLogin.TextAlign = ContentAlignment.MiddleCenter;
            lblLogin.Dock = DockStyle.Top;
            lblLogin.Height = 60;
            panelLogin.Controls.Add(lblLogin);

            // Botão Entrar
            btnEntrar = new Button();
            btnEntrar.Text = "Entrar";
            btnEntrar.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            btnEntrar.BackColor = ColorTranslator.FromHtml("#A45D5D");
            btnEntrar.ForeColor = Color.White;
            btnEntrar.FlatStyle = FlatStyle.Flat;
            btnEntrar.FlatAppearance.BorderSize = 0;
            btnEntrar.Cursor = Cursors.Hand;
            btnEntrar.Click += BtnEntrar_Click;
            panelLogin.Controls.Add(btnEntrar);

            // Associa o evento de redimensionamento
            this.Resize += FormLogin_Resize;
            // Posiciona os controles inicialmente
            AtualizarLayout();
        }

        private void FormLogin_Resize(object sender, EventArgs e)
        {
            AtualizarLayout();
        }

        private void AtualizarLayout()
        {
            // Margens relativas
            int margem = (int)(this.ClientSize.Width * 0.025f); // 2.5% da largura da janela
            int alturaPanelFundo = (int)(this.ClientSize.Height * 0.8f); // 80% da altura da janela
            int larguraPanelFundo = (int)(this.ClientSize.Width * 0.9f); // 90% da largura da janela

            // Painel de fundo
            panelFundo.Location = new Point(margem, (this.ClientSize.Height - alturaPanelFundo) / 2);
            panelFundo.Size = new Size(larguraPanelFundo, alturaPanelFundo);

            // Painel do logo (esquerda)
            int larguraPanelLogo = (int)(panelFundo.Width * 0.35f); // 35% da largura do painel de fundo
            int alturaPanelLogo = larguraPanelLogo;
            int margemInterna = (int)(panelFundo.Height * 0.1f); // 10% da altura do painel de fundo

            panelLogo.Location = new Point(margemInterna, margemInterna);
            panelLogo.Size = new Size(larguraPanelLogo, alturaPanelLogo);

            // Label de boas-vindas
            lblBemVindo.Location = new Point(margemInterna, panelLogo.Bottom + margemInterna);
            lblBemVindo.Size = new Size(larguraPanelLogo, (int)(panelFundo.Height * 0.15f));

            // Painel de login (direita)
            int larguraPanelLogin = (int)(panelFundo.Width * 0.4f); // 40% da largura do painel de fundo
            int alturaPanelLogin = (int)(panelFundo.Height * 0.6f); // 60% da altura do painel de fundo

            panelLogin.Location = new Point(
                panelFundo.Width - larguraPanelLogin - margemInterna,
                (panelFundo.Height - alturaPanelLogin) / 2
            );
            panelLogin.Size = new Size(larguraPanelLogin, alturaPanelLogin);

            // Botão Entrar
            btnEntrar.Size = new Size((int)(panelLogin.Width * 0.8f), (int)(panelLogin.Height * 0.15f));
            btnEntrar.Location = new Point(
                (panelLogin.Width - btnEntrar.Width) / 2,
                (int)(panelLogin.Height * 0.6f)
            );
        }

        private void BtnEntrar_Click(object sender, EventArgs e)
        {
            FormMainMenu proximaPagina = new FormMainMenu();
            proximaPagina.Show();
            this.Hide();
        }
    }
}
