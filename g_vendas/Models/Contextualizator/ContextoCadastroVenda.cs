using System.Collections.Generic;

namespace g_vendas.Models.Contextualizator
{
    public class ContextoCadastroVenda
    {
        public Venda Venda { get; set; }
        public List<ItensVendas> ItensVendas { get; set; } = new List<ItensVendas>();
        public List<SaboresPizza> SaboresPizza { get; set; } = new List<SaboresPizza>();
        public List<Produtos> Produtos { get; set; } = new List<Produtos>();
        public PizzaSabores PizzaSabores { get; set; }

        public ContextoCadastroVenda() { }

        // Se quiser um construtor com listas, use assim:
        public ContextoCadastroVenda(Venda venda, List<ItensVendas> itens, List<SaboresPizza> saboresP, List<Produtos> produtos, PizzaSabores pSabores)
        {
            this.Venda = venda;
            this.ItensVendas = itens;
            this.SaboresPizza = saboresP;
            this.Produtos = produtos;
            this.PizzaSabores = pSabores;
        }
    }
}
