namespace g_vendas.Models
{
    public class PizzaSabores
    {
        public decimal proporcao;
        public int id;
        public int id_pizzaSabores;

        public PizzaSabores() { }

        public PizzaSabores(decimal proporcao)
        {
            this.proporcao = proporcao;
            //this.id = id;
        }
    }
}
