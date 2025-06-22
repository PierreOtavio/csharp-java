namespace g_vendas.Models
{
    public class PizzaSabores
    {
        public string proporcao;
        public int id;
        public int id_item;
        public int id_sabor;

        public PizzaSabores() { }

        public PizzaSabores(string proporcao) => this.proporcao = proporcao;
    }
}
