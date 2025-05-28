using g_vendas.Controllers;
using System;
using System.Windows.Forms;


namespace g_vendas.UI
{
    public partial class FormVendas1 : Form
    {
        private VendasController _controller;

        public FormVendas1()
        {
            InitializeComponent();
            _controller = new VendasController();
            CarregarFormasPagamento();
            CarregarVendas();
        }

        private void CarregarFormasPagamento()
        {
            var formas = _controller.ObterFormasPagamento();
            comboBoxFormaPagamento.DataSource = formas;
        }

        private void CarregarVendas()
        {
            var response = _controller.ListarVendas();

            if (response.Sucesso)
            {
                dataGridViewVendas.DataSource = response.Dados;
                lblStatus.Text = response.Mensagem;
            }
            else
            {
                MessageBox.Show(response.Mensagem, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                var dataVenda = dateTimePickerData.Value;
                var valorTotal = decimal.Parse(txtValorTotal.Text);
                var desconto = decimal.Parse(txtDesconto.Text);
                var formaPagamento = _controller.ConverterFormaPagamento(comboBoxFormaPagamento.Text);

                var response = _controller.CadastrarVenda(dataVenda, valorTotal, desconto, formaPagamento);

                if (response.Sucesso)
                {
                    MessageBox.Show(response.Mensagem, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparCampos();
                    CarregarVendas();
                }
                else
                {
                    MessageBox.Show(response.Mensagem, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro nos dados informados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            var response = _controller.ExportarVendas();
            MessageBox.Show(response.Mensagem, response.Sucesso ? "Sucesso" : "Erro",
                          MessageBoxButtons.OK, response.Sucesso ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }

        private void LimparCampos()
        {
            txtValorTotal.Clear();
            txtDesconto.Clear();
            dateTimePickerData.Value = DateTime.Now;
            comboBoxFormaPagamento.SelectedIndex = 0;
        }
    }
}

