using System;

namespace WindowsFormsApp1.classes
{
    public class SaidaEstoque : IMovimentacaoEstoque
    {
        public DateTime Data { get; }
        public string Historico { get; }
        public int Quantidade { get; }

        public SaidaEstoque(DateTime data, string historico, int quantidade)
        {
            Data = data;
            Historico = historico;
            Quantidade = quantidade;
        }

        public void Executar(ContoladorEstoque estoque)
        {
            estoque.RegisterSaida(Data, Historico, Quantidade, 0); // custoUnitário ignorado na saída
        }
    }
}
