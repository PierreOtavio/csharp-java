using System;

namespace WindowsFormsApp1.classes
{
    public class LancamentoEstoque
    {
        public DateTime data { get; set; }
        public string historico { get; set; }

        public int? qtdeEntrada { get; set; }
        public decimal? custoUnitario { get; set; }
        public decimal? totalEntrada => qtdeEntrada * custoUnitario;

        public int? qtdeSaida { get; set; }
        public decimal? custoMedioSaida { get; set; }
        public decimal? totalSaida => qtdeSaida * custoMedioSaida;

        public int saldoQtde { get; set; }
        public decimal saldoCustoMedio { get; set; }
        public decimal saldoTotal { get; set; }


        public LancamentoEstoque(DateTime data, string historico, int saldoQuantidade, decimal saldoCustoMedio, decimal saldoTotal, int? quantidadeEntrada = null, decimal? custoUnitarioEntrada = null, int? quantidadeSaida = null, decimal? custoMedioSaida = null)
        {
            this.data = data;
            this.historico = historico;
            this.qtdeEntrada = quantidadeEntrada;
            this.custoUnitario = custoUnitarioEntrada;
            this.qtdeSaida = quantidadeSaida;
            this.custoMedioSaida = custoMedioSaida;
            this.saldoQtde = saldoQuantidade;
            this.saldoCustoMedio = saldoCustoMedio;
            this.saldoTotal = saldoTotal;
        }

    }
}
