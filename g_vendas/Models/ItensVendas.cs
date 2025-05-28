namespace g_vendas.Models
{
    public class ItensVendas
    {
        public int Id_Item { get; set; }             // id_item int(11) AI PK
        public int Id_Venda { get; set; }            // id_venda int(11)
        public int Id_Produto { get; set; }          // id_produto int(11)
        public int Quantidade { get; set; }          // quantidade int(11)
        public decimal Preco_Unitario { get; set; }  // preco_unitario decimal(10,2)
        public string Observacao { get; set; }       // observacao varchar(200)

        // Construtor padrão
        public ItensVendas() { }

        // Construtor completo
        public ItensVendas(int idItem, int idVenda, int idProduto, int quantidade, decimal precoUnitario, string observacao)
        {
            Id_Item = idItem;
            Id_Venda = idVenda;
            Id_Produto = idProduto;
            Quantidade = quantidade;
            Preco_Unitario = precoUnitario;
            Observacao = observacao;
        }
    }
}
