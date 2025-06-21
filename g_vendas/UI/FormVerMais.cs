using g_vendas.Controllers;
using g_vendas.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace g_vendas.UI
{
    public class FormVerMais : Form
    {
        public FormVerMais(int idVenda)
        {
            // Busca os dados
            var venda = new VendasController().BuscarVendaPorId(idVenda); // [5]
            var itensVenda = new ItensVendasController().BuscarItensPorVenda(idVenda) as List<ItensVendas>; // [1]
            var listaSabores = new PizzaSaboresController().ObterTodosSabores(); // [2]
            var pizzaSabores = listaSabores.FirstOrDefault(p => p.id == venda.id_pizzaSabores) ?? new PizzaSabores(0.5m); // [8]

            // Extrai informações para exibir
            string tamanho = "";
            var sabores = new List<string>();
            string bebida = "Não";
            string refriNome = "-";
            decimal valorPizza = 0;
            decimal valorRefri = 0;

            if (itensVenda != null)
            {
                foreach (var item in itensVenda)
                {
                    if (!string.IsNullOrEmpty(item.Observacao))
                    {
                        if (item.Observacao.ToLower().Contains("grande")) tamanho = "Grande";
                        if (item.Observacao.ToLower().Contains("frango") || item.Observacao.ToLower().Contains("calabresa"))
                            sabores.Add(item.Observacao);
                        if (item.Observacao.ToLower().Contains("coca") || item.Observacao.ToLower().Contains("guaraná") || item.Observacao.ToLower().Contains("fanta"))
                        {
                            bebida = "Sim";
                            refriNome = item.Observacao;
                            valorRefri = item.Preco_Unitario;
                        }
                        if (item.Observacao.ToLower().Contains("pizza") || item.Observacao.ToLower().Contains("grande") || item.Observacao.ToLower().Contains("média"))
                            valorPizza = item.Preco_Unitario;
                    }
                }
            }

            string divisao = pizzaSabores.proporcao > 0 && pizzaSabores.proporcao < 1
                ? $"1/{(int)(1 / pizzaSabores.proporcao)}"
                : "1";

            // Layout igual ao print
            this.Text = "Informações do pedido";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(240, 240, 240);

            // Painel de fundo rosado
            var panelFundo = new Panel
            {
                BackColor = Color.FromArgb(185, 154, 134),
                Location = new Point(60, 40),
                Size = new Size(780, 570)
            };
            this.Controls.Add(panelFundo);

            // Título
            var lblTitulo = new Label
            {
                Text = "Informações do pedido:",
                Font = new Font("Segoe UI", 16, FontStyle.Regular),
                ForeColor = Color.White,
                Location = new Point(30, 20),
                AutoSize = true
            };
            panelFundo.Controls.Add(lblTitulo);

            // Painel branco central
            var panelBranco = new Panel
            {
                BackColor = Color.White,
                Location = new Point(30, 60),
                Size = new Size(720, 470)
            };
            panelFundo.Controls.Add(panelBranco);

            // Painel de conteúdo cinza claro
            var panelConteudo = new Panel
            {
                BackColor = Color.FromArgb(230, 230, 230),
                Location = new Point(30, 35),
                Size = new Size(660, 400)
            };
            panelBranco.Controls.Add(panelConteudo);

            // Botão Voltar (laranja)
            var btnVoltar = new Button
            {
                Text = "◀ Voltar",
                BackColor = Color.FromArgb(236, 179, 138),
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(110, 38),
                Location = new Point(10, 10)
            };
            btnVoltar.FlatAppearance.BorderSize = 0;
            btnVoltar.Click += (s, e) =>
            {
                FormRelatorioHome form = new FormRelatorioHome();
                form.Show();
                this.Hide();
            };
            panelBranco.Controls.Add(btnVoltar);

            // Botão Exportar (verde)
            var btnExportar = new Button
            {
                Text = "Exportar como .xlsx",
                BackColor = Color.FromArgb(37, 168, 37),
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
            var lblPizza = new Label
            {
                Text = $"Pizza: {tamanho}",
                Font = new Font("Segoe UI", 13, FontStyle.Regular),
                Location = new Point(xEsq, yBase),
                AutoSize = true
            };
            panelConteudo.Controls.Add(lblPizza);

            var lblSabores = new Label
            {
                Text = $"Sabores: {string.Join(" e ", sabores)}",
                Font = new Font("Segoe UI", 13, FontStyle.Regular),
                Location = new Point(xEsq, yBase + yEspaco),
                AutoSize = true
            };
            panelConteudo.Controls.Add(lblSabores);

            // Painel "Pago: Sim" (verde)
            var panelPago = new Panel
            {
                BackColor = Color.FromArgb(37, 168, 37),
                Location = new Point(xEsq, yBase + 2 * yEspaco + 10),
                Size = new Size(220, 90)
            };
            panelConteudo.Controls.Add(panelPago);

            var lblPago = new Label
            {
                Text = $"Pago: Sim",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(12, 10),
                AutoSize = true
            };
            panelPago.Controls.Add(lblPago);

            var lblPassado = new Label
            {
                Text = $"Passado no {venda.FormaPagamento.ToString().ToLower()}\nvalor de: R${venda.valor_total:F2}",
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = Color.White,
                Location = new Point(12, 35),
                AutoSize = true
            };
            panelPago.Controls.Add(lblPassado);

            // Labels de informações (lado direito)
            int xDir = 330, yDir = 30;
            var lblDivisao = new Label
            {
                Text = $"Divisão: {divisao}",
                Font = new Font("Segoe UI", 13, FontStyle.Regular),
                Location = new Point(xDir, yDir),
                AutoSize = true
            };
            panelConteudo.Controls.Add(lblDivisao);

            var lblRefrigerante = new Label
            {
                Text = $"Refrigerante: {bebida}",
                Font = new Font("Segoe UI", 13, FontStyle.Regular),
                Location = new Point(xDir, yDir + yEspaco),
                AutoSize = true
            };
            panelConteudo.Controls.Add(lblRefrigerante);

            var lblQual = new Label
            {
                Text = $"Qual? {refriNome}",
                Font = new Font("Segoe UI", 13, FontStyle.Regular),
                Location = new Point(xDir, yDir + 2 * yEspaco),
                AutoSize = true
            };
            panelConteudo.Controls.Add(lblQual);

            // Painel valores individuais (laranja)
            var panelValores = new Panel
            {
                BackColor = Color.FromArgb(236, 179, 138),
                Location = new Point(xDir, yDir + 3 * yEspaco + 10),
                Size = new Size(200, 70)
            };
            panelConteudo.Controls.Add(panelValores);

            var lblValores = new Label
            {
                Text = "Valores individuais:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.Black,
                Location = new Point(10, 5),
                AutoSize = true
            };
            panelValores.Controls.Add(lblValores);

            var lblValoresP = new Label
            {
                Text = $"P: R${valorPizza:F2}",
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = Color.Black,
                Location = new Point(10, 28),
                AutoSize = true
            };
            panelValores.Controls.Add(lblValoresP);

            var lblValoresR = new Label
            {
                Text = $"R: R${valorRefri:F2}",
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = Color.Black,
                Location = new Point(100, 28),
                AutoSize = true
            };
            panelValores.Controls.Add(lblValoresR);
        }
    }
}
