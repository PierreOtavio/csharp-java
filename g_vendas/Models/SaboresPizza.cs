namespace g_vendas.Models
{
    public class SaboresPizza
    {
        public int id { get; set; }
        public readonly string nome;
        public readonly string descricao;

        public SaboresPizza(string nome, string descricao)
        {
            this.nome = nome;
            this.descricao = descricao;
        }

        public SaboresPizza()
        {
            nome = "";
            descricao = "";
        }
    }
}
