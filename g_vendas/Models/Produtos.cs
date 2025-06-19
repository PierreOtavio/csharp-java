namespace g_vendas.Models
{
    public class Produtos
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public tipo Tipo { get; set; }

        public enum tipo
        {
            Refrigerante,
            Pizza,
            Outros
        }

        public Produtos(string Nome, decimal Preco, string Descricao, tipo Tipo)
        {
            this.Nome = Nome;
            this.Preco = Preco;
            this.Descricao = Descricao;
            this.Tipo = Tipo;
        }

        public Produtos()
        {
            Nome = "nulo";
            Preco = 0;
            Descricao = "sem descrição";
            Tipo = 0;
        }
    }
}
