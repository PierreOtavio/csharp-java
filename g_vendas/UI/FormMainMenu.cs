using g_vendas.Controllers;
using g_vendas.ExportacaoExcel;
using g_vendas.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace g_vendas.UI
{
    public class FormMainMenu : Form
    {
        private Panel panelCentral;
        private Button btnCadastrar, btnRelatorio, btnExportar;

        public FormMainMenu()
        {
            //Definir Ícone:
            FormUtils.SetIcon(this, "Pizza-Da-Grá.ico");

            // Cores da imagem
            this.Text = "Menu Principal";
            this.BackColor = ColorTranslator.FromHtml("#E5E1E1");
            this.Size = new Size(900, 700); // Tamanho padrão desejado
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true; // Permite maximizar

            // Painel central
            panelCentral = new Panel();
            //panelCentral.BackColor = Color.Transparent;
            panelCentral.BackColor = Color.FromArgb(236, 234, 234); // ou Color.White

            this.Controls.Add(panelCentral);

            // Botão 1: Cadastrar vendas
            btnCadastrar = CriarBotaoMenu("\u002B", "Cadastrar vendas");
            btnCadastrar.Click += BtnCadastrar_Click;
            panelCentral.Controls.Add(btnCadastrar);

            // Botão 2: Relatório mensal
            btnRelatorio = CriarBotaoMenu("\u2B07", "Relatório mensal");
            btnRelatorio.Click += BtnRelatorio_Click;
            panelCentral.Controls.Add(btnRelatorio);

            // Botão 3: Exportar como .xlsx
            btnExportar = CriarBotaoMenu("\uD83D\uDCCE", "Exportar como .xlsx");
            btnExportar.Click += BtnExportar_Click;
            panelCentral.Controls.Add(btnExportar);

            this.Resize += (s, e) => AtualizarLayout();
            AtualizarLayout();
        }

        private void BtnCadastrar_Click(object sender, EventArgs e)
        {
            FormCadastroVenda1 cadVenda = new FormCadastroVenda1();
            cadVenda.Show();
            this.Hide();
        }

        private void BtnRelatorio_Click(object sender, EventArgs e)
        {
            FormRelatorioHome relatorio = new FormRelatorioHome();
            relatorio.Show();
            this.Hide();
        }

        private Button CriarBotaoMenu(string icone, string texto)
        {
            Button btn = new Button();
            btn.Font = new Font("Segoe UI", 24, FontStyle.Regular);
            btn.BackColor = ColorTranslator.FromHtml("#C19898");
            btn.ForeColor = Color.Black;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.Padding = new Padding(40, 0, 0, 0);
            btn.Size = new Size(450, 75);
            btn.Text = $"  {icone}   {texto}";
            btn.Cursor = Cursors.Hand;
            btn.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#E6D1D1");
            return btn;
        }

        private void AtualizarLayout()
        {
            int larguraPanel = 500;
            int alturaBotao = 75;
            int espacamento = 35;
            int alturaPanel = (alturaBotao * 3) + (espacamento * 2);

            panelCentral.Size = new Size(larguraPanel, alturaPanel);
            panelCentral.Location = new Point(
                (this.ClientSize.Width - larguraPanel) / 2,
                (this.ClientSize.Height - alturaPanel) / 2
            );

            int margemHorizontal = 0;
            int posY = 0;

            btnCadastrar.Size = new Size(larguraPanel, alturaBotao);
            btnCadastrar.Location = new Point(margemHorizontal, posY);

            btnRelatorio.Size = new Size(larguraPanel, alturaBotao);
            btnRelatorio.Location = new Point(margemHorizontal, posY + alturaBotao + espacamento);

            btnExportar.Size = new Size(larguraPanel, alturaBotao);
            btnExportar.Location = new Point(margemHorizontal, posY + (alturaBotao + espacamento) * 2);
        }

        // Função de exportação para Excel
        private void BtnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                var vendasController = new VendasController();
                var produtosController = new ProdutosController();
                var pizzaSaboresController = new PizzaSaboresController();
                var saboresPizzaController = new SaboresPizzaController();
                var itensVendasController = new ItensVendasController();

                List<Venda> vendas = vendasController.ListarVendas();
                List<Produtos> produtos = produtosController.ListarProdutos();
                List<PizzaSabores> pizzaSabores = pizzaSaboresController.ObterTodosSabores();
                List<SaboresPizza> saboresPizza = saboresPizzaController.searchAllFl();
                List<ItensVendas> itensVendas = (List<ItensVendas>)itensVendasController.ListarItens();

                List<(string, object)> dados = new List<(string, object)>
        {
            ("Vendas", vendas),
            ("Produtos", produtos),
            ("PizzaSabores", pizzaSabores),
            ("SaboresPizza", saboresPizza),
            ("ItensVendas", itensVendas)
        };

                ExcelExporter.ExportAllToExcel(dados, "RelatorioCompleto.xlsx");

                MessageBox.Show("Exportação concluída na área de trabalho!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao exportar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
