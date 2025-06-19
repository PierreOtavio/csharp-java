namespace g_vendas.Models
{
    internal class SaboresPizza
    {
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
