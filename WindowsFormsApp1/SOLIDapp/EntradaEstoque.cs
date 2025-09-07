using System;

namespace WindowsFormsApp1.classes
{
    public class EntradaEstoque : IMovimentacaoEstoque
    {
        public DateTime Data { get; }
        public string Historico { get; }
        public int Quantidade { get; }
        public decimal CustoUnitario { get; }

        public EntradaEstoque(DateTime data, string historico, int quantidade, decimal custoUnitario)
        {
            Data = data;
            Historico = historico;
            Quantidade = quantidade;
            CustoUnitario = custoUnitario;
        }

        public void Executar(ContoladorEstoque estoque)
        {
            estoque.RegisterEntrada(Data, Historico, Quantidade, CustoUnitario);
        }
    }
}
