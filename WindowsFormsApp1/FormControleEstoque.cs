using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1.classes;

namespace WindowsFormsApp1
{
    public partial class FormControleEstoque : Form
    {
        private DataGridView dataGridView1;
        private GroupBox groupEntrada, groupSaida;
        private DateTimePicker dtpEntrada, dtpSaida;
        private TextBox txtHistoricoEntrada, txtHistoricoSaida;
        private NumericUpDown numQuantidadeEntrada, numCustoEntrada, numQuantidadeSaida;
        private Button btnRegistrarEntrada, btnRegistrarSaida;
        private IEstoqueService _estoqueService;
        private BindingList<LancamentoEstoque> _bindingList;

        public FormControleEstoque()
        {
            InitializeComponent();
            ConfigurarControle();
            ConfigurarDataGridView();
        }

        private void InitializeComponent()
        {
            this.Text = "Controle de Estoque";
            this.ClientSize = new Size(900, 420);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // DataGridView
            dataGridView1 = new DataGridView
            {
                Location = new Point(10, 10),
                Size = new Size(870, 200),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                RowHeadersVisible = false
            };
            Controls.Add(dataGridView1);

            // GroupBox Entrada
            groupEntrada = new GroupBox
            {
                Text = "Entrada de Estoque",
                Location = new Point(10, 220),
                Size = new Size(870, 80)
            };
            Controls.Add(groupEntrada);

            groupEntrada.Controls.Add(new Label { Text = "Data:", Location = new Point(10, 30), AutoSize = true });
            dtpEntrada = new DateTimePicker { Location = new Point(60, 25), Width = 120 };
            groupEntrada.Controls.Add(dtpEntrada);

            groupEntrada.Controls.Add(new Label { Text = "Histórico:", Location = new Point(200, 30), AutoSize = true });
            txtHistoricoEntrada = new TextBox { Location = new Point(265, 25), Width = 150 };
            groupEntrada.Controls.Add(txtHistoricoEntrada);

            groupEntrada.Controls.Add(new Label { Text = "Quantidade:", Location = new Point(430, 30), AutoSize = true });
            numQuantidadeEntrada = new NumericUpDown { Location = new Point(510, 25), Width = 60, Minimum = 0, Maximum = 10000 };
            groupEntrada.Controls.Add(numQuantidadeEntrada);

            groupEntrada.Controls.Add(new Label { Text = "Custo Unitário:", Location = new Point(590, 30), AutoSize = true });
            numCustoEntrada = new NumericUpDown { Location = new Point(680, 25), Width = 80, DecimalPlaces = 2, Minimum = 0, Maximum = 100000 };
            groupEntrada.Controls.Add(numCustoEntrada);

            btnRegistrarEntrada = new Button { Text = "Registrar Entrada", Location = new Point(780, 23), Width = 80 };
            btnRegistrarEntrada.Click += btnRegistrarEntrada_Click;
            groupEntrada.Controls.Add(btnRegistrarEntrada);

            // GroupBox Saída
            groupSaida = new GroupBox
            {
                Text = "Saída de Estoque",
                Location = new Point(10, 310),
                Size = new Size(870, 80)
            };
            Controls.Add(groupSaida);

            groupSaida.Controls.Add(new Label { Text = "Data:", Location = new Point(10, 30), AutoSize = true });
            dtpSaida = new DateTimePicker { Location = new Point(60, 25), Width = 120 };
            groupSaida.Controls.Add(dtpSaida);

            groupSaida.Controls.Add(new Label { Text = "Histórico:", Location = new Point(200, 30), AutoSize = true });
            txtHistoricoSaida = new TextBox { Location = new Point(265, 25), Width = 150 };
            groupSaida.Controls.Add(txtHistoricoSaida);

            groupSaida.Controls.Add(new Label { Text = "Quantidade:", Location = new Point(430, 30), AutoSize = true });
            numQuantidadeSaida = new NumericUpDown { Location = new Point(510, 25), Width = 60, Minimum = 0, Maximum = 10000 };
            groupSaida.Controls.Add(numQuantidadeSaida);

            btnRegistrarSaida = new Button { Text = "Registrar Saída", Location = new Point(780, 23), Width = 80 };
            btnRegistrarSaida.Click += btnRegistrarSaida_Click;
            groupSaida.Controls.Add(btnRegistrarSaida);
        }

        private void ConfigurarControle()
        {
            var produto = new Produto("1.12.25.073", "Calça de Brim Azul Escuro");
            var controlador = new ContoladorEstoque(produto);
            _estoqueService = new EstoqueService(controlador);
            _bindingList = new BindingList<LancamentoEstoque>(controlador.LancamentoEstoque);
        }

        private void ConfigurarDataGridView()
        {
            dataGridView1.DataSource = _bindingList;

            // Formatação das colunas numéricas/monetárias
            if (dataGridView1.Columns.Contains("CustoUnitario"))
                dataGridView1.Columns["CustoUnitario"].DefaultCellStyle.Format = "c2";
            if (dataGridView1.Columns.Contains("TotalEntrada"))
                dataGridView1.Columns["TotalEntrada"].DefaultCellStyle.Format = "c2";
            if (dataGridView1.Columns.Contains("CustoMedioSaida"))
                dataGridView1.Columns["CustoMedioSaida"].DefaultCellStyle.Format = "c2";
            if (dataGridView1.Columns.Contains("TotalSaida"))
                dataGridView1.Columns["TotalSaida"].DefaultCellStyle.Format = "c2";
            if (dataGridView1.Columns.Contains("SaldoCustoMedio"))
                dataGridView1.Columns["SaldoCustoMedio"].DefaultCellStyle.Format = "c2";
            if (dataGridView1.Columns.Contains("SaldoTotal"))
                dataGridView1.Columns["SaldoTotal"].DefaultCellStyle.Format = "c2";
        }

        private void btnRegistrarEntrada_Click(object sender, EventArgs e)
        {
            try
            {
                _estoqueService.RegistrarEntrada(
                    dtpEntrada.Value,
                    txtHistoricoEntrada.Text,
                    (int)numQuantidadeEntrada.Value,
                    numCustoEntrada.Value
                );
                _bindingList.ResetBindings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegistrarSaida_Click(object sender, EventArgs e)
        {
            try
            {
                _estoqueService.RegistrarSaida(
                    dtpSaida.Value,
                    txtHistoricoSaida.Text,
                    (int)numQuantidadeSaida.Value
                );
                _bindingList.ResetBindings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
