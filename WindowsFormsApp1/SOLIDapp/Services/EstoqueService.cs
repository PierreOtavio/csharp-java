using System;

namespace WindowsFormsApp1.classes
{
    public class EstoqueService : IEstoqueService
    {
        private readonly ContoladorEstoque _controlador;

        public EstoqueService(ContoladorEstoque controlador)
        {
            _controlador = controlador;
        }

        public void RegistrarEntrada(DateTime data, string historico, int qtde, decimal custoUnitario)
        {
            _controlador.RegisterEntrada(data, historico, qtde, custoUnitario);
        }

        public void RegistrarSaida(DateTime data, string historico, int qtde)
        {
            _controlador.RegisterSaida(data, historico, qtde, 0); // custoUnitário ignorado na saída
        }
    }
}
