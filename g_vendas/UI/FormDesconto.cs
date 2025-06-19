using System.Drawing;
using System.Windows.Forms;

namespace g_vendas.UI
{
    public class FormDesconto : Form
    {
        public TextBox txtDesconto;
        public Button btnConfirmar;
        public Button btnCancelar;

        public FormDesconto()
        {
            //Definir Ícone:
            FormUtils.SetIcon(this, "Pizza-Da-Grá.ico");

            this.Text = "Desconto";
            this.Size = new Size(400, 200);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(245, 225, 225);

            // Label
            Label lbl = new Label();
            lbl.Text = "Escreva o desconto em %:";
            lbl.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            lbl.AutoSize = true;
            this.Controls.Add(lbl);

            // TextBox
            txtDesconto = new TextBox();
            txtDesconto.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            txtDesconto.Text = "10";
            this.Controls.Add(txtDesconto);

            // Botão Cancelar
            btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            btnCancelar.BackColor = Color.FromArgb(220, 40, 40);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.DialogResult = DialogResult.Cancel;
            this.Controls.Add(btnCancelar);

            // Botão Confirmar
            btnConfirmar = new Button();
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            btnConfirmar.BackColor = Color.FromArgb(40, 180, 40);
            btnConfirmar.ForeColor = Color.White;
            btnConfirmar.FlatStyle = FlatStyle.Flat;
            btnConfirmar.FlatAppearance.BorderSize = 0;
            btnConfirmar.DialogResult = DialogResult.OK;
            this.Controls.Add(btnConfirmar);

            this.AcceptButton = btnConfirmar;
            this.CancelButton = btnCancelar;

            // Ajusta o layout inicialmente
            AtualizarLayout();
        }

        private void AtualizarLayout()
        {
            // Margem lateral fixa (pode ser relativa ao tamanho do form)
            int margem = (int)(this.ClientSize.Width * 0.08f);

            // Label
            Label lbl = (Label)this.Controls[0];
            lbl.Location = new Point(margem, margem);

            // TextBox
            txtDesconto.Size = new Size(this.ClientSize.Width - margem * 2, 36);
            txtDesconto.Location = new Point(margem, lbl.Bottom + 10);

            // Botões
            int larguraBotao = (int)((this.ClientSize.Width - margem * 3) / 2);
            int alturaBotao = 40;
            int yBotao = this.ClientSize.Height - alturaBotao - margem;

            btnCancelar.Size = new Size(larguraBotao, alturaBotao);
            btnCancelar.Location = new Point(margem, yBotao);

            btnConfirmar.Size = new Size(larguraBotao, alturaBotao);
            btnConfirmar.Location = new Point(btnCancelar.Right + margem, yBotao);
        }
    }
}
