using g_vendas.Controllers;
using g_vendas.Models;
using g_vendas.Models.Contextualizator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace g_vendas.UI
{
    public class FormCadastroVenda1 : Form
    {
        private Panel panelFundo, panelBranco, linhaVertical;
        private Label lblTitulo;
        private Button btnCancelar, btnProximo;
        private Button btnPizza, btnDivisao, btnFormaPagamento, btnSaboresNormais, btnSaboresEspeciais;
        private ListBox lstPizza, lstDivisao, lstFormaPagamento;
        private CheckedListBox chkListSaboresNormais, chkListSaboresEspeciais;
        private Color corFundo = Color.FromArgb(185, 154, 134);
        //private Color corPainel = Color.White;
        private Color corBotao = Color.FromArgb(220, 200, 200);
        private Color corBotaoHover = Color.FromArgb(230, 210, 210);
        private Color corBorda = Color.FromArgb(150, 120, 120);
        private Color corTexto = Color.Black;

        private readonly string[] opcoesPizza = { "Grande", "Média", "Pequena" };
        private readonly string[] opcoesDivisao = { "1", "1/2", "1/4" };
        private readonly string[] opcoesPagamento = { "Dinheiro", "Cartao", "PIX", "Outro" };
        private readonly string[] saboresNormais = { "Frango", "Calabresa", "Napolitana", "Lombinho", "À moda da Grá" };
        private readonly string[] saboresEspeciais = { "Atum", "Pepperoni", "Brigadeiro", "Portuguesa", "P. Espanhola", "5 Queijos", "Frango c/ Bacon", "Frango c/ Catupiry", "Toscana", "Margherita", "" };

        public FormCadastroVenda1()
        {
            // Ícone
            FormUtils.SetIcon(this, "Pizza-Da-Grá.ico");

            // Formulário principal
            this.Text = "Cadastrar venda";
            this.Size = new Size(900, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = true;
            this.BackColor = Color.FromArgb(236, 234, 234);

            // Painel de fundo (rosado)
            panelFundo = new Panel
            {
                BackColor = corFundo,
                Location = new Point(0, 0),
                Size = this.ClientSize
            };
            this.Controls.Add(panelFundo);

            //// Painel de sombra (cinza claro)
            //// Remova ou ajuste o painel de sombra para não cobrir o painel branco!
            //panelSombra = new Panel
            //{
            //    BackColor = Color.FromArgb(50, 0, 0, 0), // sombra sutil
            //    Location = new Point(38, 48),
            //    Size = new Size(804, 464) // ligeiramente maior que o painel branco
            //};
            //panelFundo.Controls.Add(panelSombra);
            //panelSombra.SendToBack(); // Garante que fique atrás do painel branco

            // Painel branco central
            panelBranco = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Location = new Point(30, 40),
                Size = new Size(800, 460)
            };
            panelFundo.Controls.Add(panelBranco);
            panelBranco.BringToFront();

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
            int xEsq = 40, yBase = 70;

            // Pizza (dropdown único)
            btnPizza = CriarBotaoDropdown("Selecione o tamanho");
            panelBranco.Controls.Add(btnPizza);

            lstPizza = CriarListBox(opcoesPizza);
            panelBranco.Controls.Add(lstPizza);

            btnPizza.Click += (s, e) => ExibirDropdownSimples(lstPizza, btnPizza);
            lstPizza.SelectedIndexChanged += (s, e) =>
            {
                if (lstPizza.SelectedItem != null)
                {
                    btnPizza.Text = lstPizza.SelectedItem.ToString();
                    lstPizza.Visible = false;
                }
            };

            // Sabores Normais (dropdown múltiplo)
            btnSaboresNormais = CriarBotaoDropdown("Selecione sabores normais");
            panelBranco.Controls.Add(btnSaboresNormais);

            chkListSaboresNormais = CriarCheckedListBox(saboresNormais);
            panelBranco.Controls.Add(chkListSaboresNormais);

            btnSaboresNormais.Click += (s, e) => ExibirDropdown(chkListSaboresNormais, btnSaboresNormais);
            chkListSaboresNormais.ItemCheck += (s, e) =>
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    AtualizarTextoBotaoSabores(btnSaboresNormais, chkListSaboresNormais, "normais");
                });
            };

            // --- Coluna Direita ---
            int xDir = panelBranco.Width / 2 + 20, yDir = yBase;

            // Divisão (dropdown único)
            btnDivisao = CriarBotaoDropdown("Selecione a divisão");
            panelBranco.Controls.Add(btnDivisao);

            lstDivisao = CriarListBox(opcoesDivisao);
            panelBranco.Controls.Add(lstDivisao);

            btnDivisao.Click += (s, e) => ExibirDropdownSimples(lstDivisao, btnDivisao);
            lstDivisao.SelectedIndexChanged += (s, e) =>
            {
                if (lstDivisao.SelectedItem != null)
                {
                    btnDivisao.Text = lstDivisao.SelectedItem.ToString();
                    lstDivisao.Visible = false;
                }
            };

            // Sabores Especiais (dropdown múltiplo)
            btnSaboresEspeciais = CriarBotaoDropdown("Selecione sabores especiais");
            panelBranco.Controls.Add(btnSaboresEspeciais);

            chkListSaboresEspeciais = CriarCheckedListBox(saboresEspeciais);
            panelBranco.Controls.Add(chkListSaboresEspeciais);

            btnSaboresEspeciais.Click += (s, e) => ExibirDropdown(chkListSaboresEspeciais, btnSaboresEspeciais);
            chkListSaboresEspeciais.ItemCheck += (s, e) =>
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    AtualizarTextoBotaoSabores(btnSaboresEspeciais, chkListSaboresEspeciais, "especiais");
                });
            };

            // Forma de Pagamento (dropdown único)
            btnFormaPagamento = CriarBotaoDropdown("Selecione a forma de pagamento");
            panelBranco.Controls.Add(btnFormaPagamento);

            lstFormaPagamento = CriarListBox(opcoesPagamento);
            panelBranco.Controls.Add(lstFormaPagamento);

            btnFormaPagamento.Click += (s, e) => ExibirDropdownSimples(lstFormaPagamento, btnFormaPagamento);
            lstFormaPagamento.SelectedIndexChanged += (s, e) =>
            {
                if (lstFormaPagamento.SelectedItem != null)
                {
                    btnFormaPagamento.Text = lstFormaPagamento.SelectedItem.ToString();
                    lstFormaPagamento.Visible = false;
                }
            };

            // --- Botões ---
            btnCancelar = new Button
            {
                Text = "❌ Cancelar",
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
                Text = "Próximo ▶",
                Font = new Font("Segoe UI", 16, FontStyle.Regular),
                BackColor = Color.FromArgb(40, 180, 40),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Cursor = Cursors.Hand,
                Size = new Size(180, 50),
                Location = new Point(panelBranco.Width / 2 + 20, panelBranco.Height - 70)
            };
            btnProximo.Click += (s, e) =>
            {
                var vendasController = new VendasController();
                // 1. Obtém os dados selecionados pelo usuário
                string pizzaSelecionada = btnPizza.Text;
                string divisaoSelecionada = btnDivisao.Text;
                string formaPagamentoSelecionada = btnFormaPagamento.Text;

                // 2. Busca o preço da pizza no catálogo
                decimal precoPizza = CatalogoProdutos.ObterPreco(pizzaSelecionada);

                // 3. Calcula a proporção (só para controle de sabores, não para preço)
                decimal proporcao = divisaoSelecionada == "1/2" ? 0.5m : divisaoSelecionada == "1/4" ? 0.25m : 1m;
                var pizzaProporcao = new PizzaSabores(proporcao);

                // 4. Monta a lista de sabores selecionados (normais e especiais)
                var nomesSabores = new List<string>();
                foreach (var item in chkListSaboresNormais.CheckedItems)
                    nomesSabores.Add(item.ToString());
                foreach (var item in chkListSaboresEspeciais.CheckedItems)
                    nomesSabores.Add(item.ToString());

                // 5. Junta todos os sabores em uma string, separados por "/"
                string descricaoSabores = string.Join("/", nomesSabores);

                // 6. Cria apenas UM item de pizza, com todos os sabores na observação
                var produtoPizza = new Produtos(pizzaSelecionada, precoPizza, "Pizza", Produtos.tipo.Pizza);
                var itens = new List<ItensVendas>();
                itens.Add(new ItensVendas(0, 0, 0, 1, precoPizza, $"{descricaoSabores} {pizzaSelecionada}"));

                // 7. (Opcional) Adiciona o refrigerante, se selecionado
                // Exemplo: se você tiver uma lista ou combo para refrigerante, pode adicionar aqui
                // decimal precoRefri = CatalogoProdutos.ObterPreco("2L");
                // itens.Add(new ItensVendas(0, 0, 0, 1, precoRefri, "Coca 2L"));

                // 8. Converte a forma de pagamento para o enum
                //var vendasController = new VendasController();
                var formaPagamento = vendasController.ConverterFormaPagamento(formaPagamentoSelecionada);

                // 9. Monta o contexto para passar para o próximo form
                var contexto = new ContextoCadastroVenda
                {
                    Produtos = new List<Produtos> { produtoPizza },
                    PizzaSabores = pizzaProporcao,
                    SaboresPizza = new List<SaboresPizza>(), // você pode preencher aqui, se necessário
                    ItensVendas = itens,
                    Venda = new Venda(DateTime.Now, 0, 0, formaPagamento)
                };

                // 10. Avança para o próximo form
                var form2 = new FormCadastroVenda2(contexto);
                form2.Show();
                this.Hide();
            };
            panelBranco.Controls.Add(btnProximo);
            //panelBranco.Controls.Add(btnProximo);

            // Evento de redimensionamento
            this.Resize += FormCadastroVenda_Resize;
            AtualizarLayout();
        }

        private Button CriarBotaoDropdown(string texto)
        {
            var btn = new Button
            {
                Text = texto,
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                BackColor = corBotao,
                ForeColor = corTexto,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderColor = corBorda, BorderSize = 1 },
                Height = 36,
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 30, 0)
            };
            btn.Paint += (s, e) =>
            {
                var b = (Button)s;
                string arrow = "▼";
                var size = e.Graphics.MeasureString(arrow, b.Font);
                e.Graphics.DrawString(arrow, b.Font, Brushes.Black,
                    b.Width - size.Width - 15, (b.Height - size.Height) / 2);
            };
            btn.MouseEnter += (s, e) => { btn.BackColor = corBotaoHover; };
            btn.MouseLeave += (s, e) => { btn.BackColor = corBotao; };
            return btn;
        }

        private CheckedListBox CriarCheckedListBox(string[] itens)
        {
            var chk = new CheckedListBox
            {
                Items = { },
                Font = new Font("Segoe UI", 15, FontStyle.Regular),
                BackColor = Color.FromArgb(240, 230, 230),
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false,
                CheckOnClick = true,
                Height = 120
            };
            foreach (var item in itens)
                chk.Items.Add(item);
            return chk;
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

        // Exibe o dropdown (multiseleção)
        private void ExibirDropdown(CheckedListBox chkList, Button btn)
        {
            Point p = btn.PointToScreen(new Point(0, btn.Height));
            chkList.Location = panelBranco.PointToClient(p);
            chkList.BringToFront();
            chkList.Width = btn.Width;
            chkList.Visible = !chkList.Visible;
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

        private void AtualizarTextoBotaoSabores(Button btn, CheckedListBox chkList, string tipo)
        {
            var selecionados = new List<string>();
            foreach (var item in chkList.CheckedItems)
                selecionados.Add(item.ToString());

            if (selecionados.Count == 0)
                btn.Text = $"Selecione sabores {tipo} ▼";
            else if (selecionados.Count <= 2)
                btn.Text = string.Join(", ", selecionados);
            else
                btn.Text = $"{selecionados.Count} selecionados ▼";
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
            // Pizza
            btnPizza.Location = new Point(xEsquerda, yInicial);
            btnPizza.Size = new Size((int)(panelBranco.Width * 0.4f), 36);
            lstPizza.Location = new Point(xEsquerda, yInicial + btnPizza.Height);
            lstPizza.Size = new Size(btnPizza.Width, 100);

            // Sabores Normais
            btnSaboresNormais.Location = new Point(xEsquerda, yInicial + 90);
            btnSaboresNormais.Size = new Size((int)(panelBranco.Width * 0.4f), 36);
            chkListSaboresNormais.Location = new Point(xEsquerda, yInicial + 90 + btnSaboresNormais.Height);
            chkListSaboresNormais.Size = new Size(btnSaboresNormais.Width, 120);

            // --- Coluna Direita ---
            // Divisão
            btnDivisao.Location = new Point(xDireita, yInicial);
            btnDivisao.Size = new Size((int)(panelBranco.Width * 0.4f), 36);
            lstDivisao.Location = new Point(xDireita, yInicial + btnDivisao.Height);
            lstDivisao.Size = new Size(btnDivisao.Width, 100);

            // Sabores Especiais
            btnSaboresEspeciais.Location = new Point(xDireita, yInicial + 90);
            btnSaboresEspeciais.Size = new Size((int)(panelBranco.Width * 0.4f), 36);
            chkListSaboresEspeciais.Location = new Point(xDireita, yInicial + 90 + btnSaboresEspeciais.Height);
            chkListSaboresEspeciais.Size = new Size(btnSaboresEspeciais.Width, 120);

            // Forma de Pagamento
            btnFormaPagamento.Location = new Point(xDireita, yInicial + 180);
            btnFormaPagamento.Size = new Size((int)(panelBranco.Width * 0.4f), 36);
            lstFormaPagamento.Location = new Point(xDireita, yInicial + 180 + btnFormaPagamento.Height);
            lstFormaPagamento.Size = new Size(btnFormaPagamento.Width, 100);

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
    }
}
