using System;

namespace WindowsFormsApp1.classes
{
    public interface IEstoqueService
    {
        void RegistrarEntrada(DateTime data, string historico, int qtde, decimal custoUnitario);
        void RegistrarSaida(DateTime data, string historico, int qtde);
    }
}
