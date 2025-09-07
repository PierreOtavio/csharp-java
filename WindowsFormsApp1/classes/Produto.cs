namespace WindowsFormsApp1.classes
{
    public class Produto
    {
        public string codigo { get; set; }
        public string descricao { get; set; }

        public Produto(string codigo, string descricao)
        {
            this.codigo = codigo;
            this.descricao = descricao;
        }

        public override string ToString()
        {
            return $"{codigo} - {descricao}";
        }
    }
}
