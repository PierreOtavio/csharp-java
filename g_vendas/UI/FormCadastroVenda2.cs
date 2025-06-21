using g_vendas.Controllers;
using g_vendas.Models;
using g_vendas.Models.Contextualizator;
using System;
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
            this.BackColor = Color.FromArgb(236, 234, 234);

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
                BackColor = Color.FromArgb(40, 180, 40),
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
            int margem = (int)(this.ClientSize.Width * 0.05f);
            int alturaPanelFundo = (int)(this.ClientSize.Height * 0.8f);
            int larguraPanelFundo = (int)(this.ClientSize.Width * 0.9f);

            panelFundo.Location = new Point(margem, (this.ClientSize.Height - alturaPanelFundo) / 2);
            panelFundo.Size = new Size(larguraPanelFundo, alturaPanelFundo);

            int margemInterna = (int)(panelFundo.Height * 0.08f);
            int larguraPanelBranco = (int)(panelFundo.Width * 0.92f);
            int alturaPanelBranco = (int)(panelFundo.Height * 0.80f);

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

                // 2. Quantidade (para pizza/sabores)
                // Aqui, você pode atualizar a quantidade da pizza, se necessário
                // (Se quiser, pode usar um controle para definir a quantidade de pizza)
                // Exemplo:
                // int quantidadePizza = ...; // coletado de algum controle
                // var itemPizza = contexto.ItensVendas.FirstOrDefault(i => contexto.Produtos.Any(p => p.Tipo == Produtos.tipo.Pizza && p.Nome.Contains(i.Observacao)));
                // if (itemPizza != null) itemPizza.Quantidade = quantidadePizza;

                // 3. Desconto
                decimal desconto = 0;
                string descontoStr = lstDesconto.SelectedItem?.ToString()?.Trim().ToLower();
                if (descontoStr == "sim")
                {
                    using (var formDesconto = new FormDesconto())
                    {
                        if (formDesconto.ShowDialog() == DialogResult.OK)
                        {
                            if (decimal.TryParse(formDesconto.txtDesconto.Text, out decimal valorDigitado))
                            {
                                valorDigitado = valorDigitado / 100;
                                desconto = valorDigitado; // Aqui você atualiza a variável
                                //MessageBox.Show($"Desconto aplicado: {desconto}%");
                            }
                        }
                    }
                }


                // 4. Valor total
                decimal valorTotal = contexto.ItensVendas.Sum(item => item.Preco_Unitario * item.Quantidade);

                // 5. Montar e salvar venda final
                // 1. Monta e salva venda final
                contexto.Venda = new Venda(DateTime.Now, valorTotal, desconto, contexto.Venda.FormaPagamento);
                //var vendasController = new VendasController();
                // 1. Salva a venda e pega o ID
                int idVenda = VendasController.CadastrarVendaCompleta(contexto.Venda);

                // 2. Atualiza o id_venda nos itens
                foreach (var item in contexto.ItensVendas)
                    item.Id_Venda = idVenda;

                // 3. Salva os itens
                foreach (var item in contexto.ItensVendas)
                    new ItensVendasController().CadastrarItem(item);
                // (Opcional) Salva produtos e sabores
                foreach (var sabor in contexto.SaboresPizza)
                    new SaboresPizzaController().insertFl(sabor);
                foreach (var prod in contexto.Produtos)
                    new ProdutosController().AdicionarProduto(prod);
                //MessageBox.Show("Venda finalizada com sucesso!");


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
