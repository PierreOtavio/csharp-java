using g_vendas.Controllers;
using g_vendas.Models;
using g_vendas.Models.Contextualizator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace g_vendas.UI
{
    public class FormCadastroVenda2 : Form
    {
        private Panel panelFundo, panelBranco, linhaVertical;
        private Label lblTitulo;
        private Button btnCancelar, btnProximo;
        private Button btnRefrigerante, btnQuantidade, btnDesconto;
        private ListBox lstRefrigerante, lstQuantidade, lstDesconto;
        private Color corFundo = Color.FromArgb(185, 154, 134);
        private Color corPainel = Color.White;
        private Color corBotao = Color.FromArgb(220, 200, 200);
        private Color corBotaoHover = Color.FromArgb(230, 210, 210);
        private Color corBorda = Color.FromArgb(150, 120, 120);
        private Color corTexto = Color.Black;

        private readonly string[] opcoesRefrigerante = { "Nenhum", "Coca Cola", "Fanta Laranja", "Guaraná Antártica" };
        private readonly string[] opcoesQuantidade = { "350mL", "1L", "1,5L", "2L" };
        private readonly string[] opcoesDesconto = { "Sim", "Não" };

        private ContextoCadastroVenda contexto;

        public FormCadastroVenda2(ContextoCadastroVenda contextoRecebido)
        {
            //Definir Ícone:
            FormUtils.SetIcon(this, "Pizza-Da-Grá.ico");
            this.contexto = contextoRecebido;

            this.Text = "Cadastrar venda";
            this.Size = new Size(900, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = corPainel;

            // Painel de fundo (rosado)
            panelFundo = new Panel
            {
                BackColor = corFundo,
                Location = new Point(0, 0),
                Size = this.ClientSize
            };
            this.Controls.Add(panelFundo);

            // Painel branco central
            panelBranco = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Location = new Point(30, 40),
                Size = new Size(800, 460)
            };
            panelFundo.Controls.Add(panelBranco);

            // Título
            lblTitulo = new Label
            {
                Text = "Cadastrar venda:",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.Black,
                BackColor = corFundo,
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
                Location = new Point(10, 5)
            };
            panelFundo.Controls.Add(lblTitulo);

            // Linha divisória vertical
            linhaVertical = new Panel
            {
                BackColor = corFundo,
                Size = new Size(2, 340),
                Location = new Point(panelBranco.Width / 2 - 1, 70)
            };
            panelBranco.Controls.Add(linhaVertical);

            // --- Coluna Esquerda ---
            int yBase = 70;

            // Refrigerante (dropdown)
            btnRefrigerante = CriarBotaoDropdown("Refrigerante:");
            panelBranco.Controls.Add(btnRefrigerante);

            lstRefrigerante = CriarListBox(opcoesRefrigerante);
            panelBranco.Controls.Add(lstRefrigerante);

            btnRefrigerante.Click += (s, e) => ExibirDropdownSimples(lstRefrigerante, btnRefrigerante);
            lstRefrigerante.SelectedIndexChanged += (s, e) =>
            {
                if (lstRefrigerante.SelectedItem != null)
                {
                    btnRefrigerante.Text = "Refrigerante: " + lstRefrigerante.SelectedItem.ToString();
                    lstRefrigerante.Visible = false;
                }
            };

            // Quantidade (dropdown)
            btnQuantidade = CriarBotaoDropdown("Quantidade:");
            panelBranco.Controls.Add(btnQuantidade);

            lstQuantidade = CriarListBox(opcoesQuantidade);
            panelBranco.Controls.Add(lstQuantidade);

            btnQuantidade.Click += (s, e) => ExibirDropdownSimples(lstQuantidade, btnQuantidade);
            lstQuantidade.SelectedIndexChanged += (s, e) =>
            {
                if (lstQuantidade.SelectedItem != null)
                {
                    btnQuantidade.Text = "Quantidade: " + lstQuantidade.SelectedItem.ToString();
                    lstQuantidade.Visible = false;
                }
            };

            // --- Coluna Direita ---
            int xDir = panelBranco.Width / 2 + 20, yDir = yBase;

            // Desconto (dropdown)
            btnDesconto = CriarBotaoDropdown("Desconto:");
            panelBranco.Controls.Add(btnDesconto);

            lstDesconto = CriarListBox(opcoesDesconto);
            panelBranco.Controls.Add(lstDesconto);

            btnDesconto.Click += (s, e) => ExibirDropdownSimples(lstDesconto, btnDesconto);
            lstDesconto.SelectedIndexChanged += (s, e) =>
            {
                if (lstDesconto.SelectedItem != null)
                {
                    btnDesconto.Text = "Desconto: " + lstDesconto.SelectedItem.ToString();
                    lstDesconto.Visible = false;
                }
            };

            // --- Botões ---
            btnCancelar = new Button
            {
                Text = "Cancelar",
                Font = new Font("Segoe UI", 16, FontStyle.Regular),
                BackColor = Color.FromArgb(220, 40, 40),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Cursor = Cursors.Hand,
                Size = new Size(180, 50),
                Location = new Point(panelBranco.Width / 2 - 200, panelBranco.Height - 70)
            };
            btnCancelar.Click += (s, e) =>
            {
                FormMainMenu form = new FormMainMenu();
                form.Show();
                this.Hide();
            };
            panelBranco.Controls.Add(btnCancelar);

            btnProximo = new Button
            {
                Text = "Próximo",
                Font = new Font("Segoe UI", 16, FontStyle.Regular),
                BackColor = ColorTranslator.FromHtml("#21B421"),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Cursor = Cursors.Hand,
                Size = new Size(180, 50),
                Location = new Point(panelBranco.Width / 2 + 20, panelBranco.Height - 70)
            };
            btnProximo.Click += BtnProximo_Click;
            panelBranco.Controls.Add(btnProximo);

            // Evento de redimensionamento
            this.Resize += FormCadastroVenda_Resize;
            AtualizarLayout();
        }

        private Button CriarBotaoDropdown(string texto)
        {
            var btn = new Button
            {
                Text = texto,
                Font = new Font("Segoe UI", 16, FontStyle.Regular),
                BackColor = corBotao,
                ForeColor = corTexto,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderColor = corBorda, BorderSize = 1 },
                Height = 38,
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 30, 0)
            };
            btn.Paint += (s, e) =>
            {
                var b = (Button)s;
                string arrow = "▲";
                var size = e.Graphics.MeasureString(arrow, b.Font);
                e.Graphics.DrawString(arrow, b.Font, Brushes.Black,
                    b.Width - size.Width - 15, (b.Height - size.Height) / 2);
            };
            btn.MouseEnter += (s, e) => { btn.BackColor = corBotaoHover; };
            btn.MouseLeave += (s, e) => { btn.BackColor = corBotao; };
            return btn;
        }

        private ListBox CriarListBox(string[] itens)
        {
            var lst = new ListBox
            {
                Font = new Font("Segoe UI", 15, FontStyle.Regular),
                BackColor = Color.FromArgb(240, 230, 230),
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false,
                Height = 100
            };
            lst.Items.AddRange(itens);
            return lst;
        }

        // Exibe o dropdown (seleção única)
        private void ExibirDropdownSimples(ListBox lst, Button btn)
        {
            Point p = btn.PointToScreen(new Point(0, btn.Height));
            lst.Location = panelBranco.PointToClient(p);
            lst.BringToFront();
            lst.Width = btn.Width;
            lst.Visible = !lst.Visible;
        }

        private void FormCadastroVenda_Resize(object sender, EventArgs e)
        {
            AtualizarLayout();
        }

        private void AtualizarLayout()
        {
            // Defina a margem desejada para o espaço branco(por exemplo, 30px)
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

            panelBranco.Location = new Point((panelFundo.Width - larguraPanelBranco) / 2, margemInterna);
            panelBranco.Size = new Size(larguraPanelBranco, alturaPanelBranco);

            //int margemInterna = (int)(panelFundo.Height * 0.08f);
            //int larguraPanelBranco = (int)(panelFundo.Width * 0.92f);
            //int alturaPanelBranco = (int)(panelFundo.Height * 0.80f);

            panelBranco.Location = new Point((panelFundo.Width - larguraPanelBranco) / 2, margemInterna);
            panelBranco.Size = new Size(larguraPanelBranco, alturaPanelBranco);

            linhaVertical.Location = new Point(panelBranco.Width / 2 - 1, (int)(panelBranco.Height * 0.08f));
            linhaVertical.Size = new Size(2, (int)(panelBranco.Height * 0.74f));

            int xEsquerda = (int)(panelBranco.Width * 0.05f);
            int xDireita = panelBranco.Width / 2 + (int)(panelBranco.Width * 0.05f);
            int yInicial = (int)(panelBranco.Height * 0.10f);

            // --- Coluna Esquerda ---
            btnRefrigerante.Location = new Point(xEsquerda, yInicial);
            btnRefrigerante.Size = new Size((int)(panelBranco.Width * 0.4f), 38);
            lstRefrigerante.Location = new Point(xEsquerda, yInicial + btnRefrigerante.Height);
            lstRefrigerante.Size = new Size(btnRefrigerante.Width, 100);

            btnQuantidade.Location = new Point(xEsquerda, yInicial + 120);
            btnQuantidade.Size = new Size((int)(panelBranco.Width * 0.4f), 38);
            lstQuantidade.Location = new Point(xEsquerda, yInicial + 120 + btnQuantidade.Height);
            lstQuantidade.Size = new Size(btnQuantidade.Width, 100);

            // --- Coluna Direita ---
            btnDesconto.Location = new Point(xDireita, yInicial);
            btnDesconto.Size = new Size((int)(panelBranco.Width * 0.4f), 38);
            lstDesconto.Location = new Point(xDireita, yInicial + btnDesconto.Height);
            lstDesconto.Size = new Size(btnDesconto.Width, 100);

            // Botões
            int larguraBotao = (int)(panelBranco.Width * 0.22f);
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

        private void BtnProximo_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Refrigerante
                string refri = lstRefrigerante.SelectedItem?.ToString() ?? "Nenhum";
                string litragem = lstQuantidade.SelectedItem?.ToString() ?? "350mL";
                if (refri != "Nenhum")
                {
                    decimal precoRefri = CatalogoProdutos.ObterPreco(litragem);
                    var produtoRefri = new Produtos(refri, precoRefri, "Refrigerante", Produtos.tipo.Refrigerante);
                    contexto.Produtos.Add(produtoRefri);

                    var itemRefri = new ItensVendas(0, 0, 0, 1, precoRefri, $"{refri} {litragem}");
                    contexto.ItensVendas.Add(itemRefri);
                }

                // 1. Calcula o valor total dos itens
                decimal valorTotalItens = contexto.ItensVendas.Sum(item => item.Preco_Unitario * item.Quantidade);

                // 2. Inicializa o desconto como zero (porcentagem)
                decimal descontoPercentual = 0;

                // 3. Se o usuário escolheu aplicar desconto
                string descontoStr = lstDesconto.SelectedItem?.ToString()?.Trim().ToLower();
                if (descontoStr == "sim")
                {
                    using (var formDesconto = new FormDesconto())
                    {
                        if (formDesconto.ShowDialog() == DialogResult.OK)
                        {
                            if (decimal.TryParse(formDesconto.txtDesconto.Text, out decimal valorDigitado))
                            {
                                descontoPercentual = valorDigitado / 100m; // Converte para decimal e divide por 100
                            }
                        }
                    }
                }

                // 4. Atribui o valor total e o desconto à venda
                contexto.Venda.valor_total = valorTotalItens;
                contexto.Venda.desconto = descontoPercentual;

                // 5. O valor final pode ser obtido pela propriedade ValorFinal do model Venda
                decimal valorFinal = contexto.Venda.ValorFinal;

                // Pode exibir o valor final para o usuário, se quiser
                MessageBox.Show($"Valor total: {valorTotalItens:C}\nDesconto: {descontoPercentual:P0}\nValor final: {valorFinal:C}");

                var pizzaSaboresController = new PizzaSaboresController();
                var prodControl = new ProdutosController();
                var saboresController = new SaboresPizzaController();
                var vendasController = new VendasController();

                // 2. Salva o produto e pega o ID
                int idProduto = prodControl.AdicionarProduto(contexto.Produtos[0]);

                //MessageBox.Show($"Valor total da venda: {contexto.Venda.valor_total}");

                // 3. Salva a venda e pega o ID gerado
                int idVenda = vendasController.CadastrarVendaCompleta(contexto.Venda);

                // 4. Atualiza os itens de venda com os IDs corretos
                foreach (var item in contexto.ItensVendas)
                {
                    item.Id_Venda = idVenda;
                    item.Id_Produto = idProduto;
                }

                // 5. Salva os itens de venda e armazena o id_item gerado do item de pizza
                int idItemPizza = 0;

                foreach (var item in contexto.ItensVendas)
                {
                    new ItensVendasController().CadastrarItem(item);

                    if (item.Observacao != null && item.Observacao.ToLower().Contains("pizza"))
                    {
                        // Busca o id gerado do item de pizza após o insert
                        var itensDaVenda = new ItensVendasController().BuscarItensPorVenda(idVenda) as List<ItensVendas>;
                        var itemPizza = itensDaVenda.FirstOrDefault(i => i.Id_Produto == idProduto && i.Id_Venda == idVenda);

                        if (itemPizza != null)
                        {
                            idItemPizza = itemPizza.Id_Item;
                            foreach (var sabor in contexto.SaboresPizza)
                            {
                                var saborBanco = saboresController.BuscarSaborPorNome(sabor.nome);
                                if (saborBanco != null)
                                {
                                    pizzaSaboresController.InsertProporcao(idItemPizza, saborBanco.id, contexto.PizzaSabores.proporcao);
                                }
                            }
                        }
                    }
                }

                // 6. Salva os sabores (garante que estão no banco)
                foreach (var sabor in contexto.SaboresPizza)
                    saboresController.insertFl(sabor);

                // 7. Para cada sabor, salva a proporção na tabela pizza_sabores
                foreach (var sabor in contexto.SaboresPizza)
                {
                    // Busca o id do sabor pelo nome
                    var saborBanco = saboresController.BuscarSaborPorNome(sabor.nome);
                    if (saborBanco != null && idItemPizza != 0)
                    {
                        // Salva a proporção para este sabor e este item de pizza
                        pizzaSaboresController.InsertProporcao(idItemPizza, saborBanco.id, contexto.PizzaSabores.proporcao);
                    }
                }

                // 8. Salva outros produtos (ex: refrigerante)
                foreach (var prod in contexto.Produtos)
                    prodControl.AdicionarProduto(prod);

                MessageBox.Show("Venda finalizada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormMainMenu formMM = new FormMainMenu();
                formMM.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao finalizar venda: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
