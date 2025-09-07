using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsFormsApp1.classes
{
    public class ContoladorEstoque
    {
        public Produto ProdControlado { get; set; }
        public List<LancamentoEstoque> LancamentoEstoque { get; private set; }

        public ContoladorEstoque(Produto produto)
        {
            ProdControlado = produto;
            LancamentoEstoque = new List<LancamentoEstoque>();

            // Lançamento inicial para evitar lista vazia
            var saldoInicio = new LancamentoEstoque(
                data: DateTime.Now.Date.AddDays(-1),
                historico: "Saldo Inicial",
                saldoQuantidade: 0,
                saldoCustoMedio: 0.0m,
                saldoTotal: 0.0m
            );
            LancamentoEstoque.Add(saldoInicio);
        }

        public void RegisterEntrada(DateTime data, string historico, int qtde, decimal custoUnitario)
        {
            if (qtde <= 0 || custoUnitario < 0)
                throw new ArgumentException("Quantidade e custo unitário devem ser maiores que zero.");

            var ultimoLanc = LancamentoEstoque.Last();
            decimal totalEntrada = qtde * custoUnitario;

            int newSaldoQtde = ultimoLanc.saldoQtde + qtde;
            decimal newSaldoTotal = ultimoLanc.saldoTotal + totalEntrada;
            decimal newSaldoMedio = (newSaldoQtde > 0) ? newSaldoTotal / newSaldoQtde : 0;

            var newLancamento = new LancamentoEstoque(
                data: data,
                historico: historico,
                saldoQuantidade: newSaldoQtde,
                saldoCustoMedio: newSaldoMedio,
                saldoTotal: newSaldoTotal,
                quantidadeEntrada: qtde,
                custoUnitarioEntrada: custoUnitario
            );

            LancamentoEstoque.Add(newLancamento);
        }

        public void RegisterSaida(DateTime data, string historico, int qtde, decimal custoUnitarioIgnorado)
        {
            if (!LancamentoEstoque.Any())
                throw new InvalidOperationException("Não há lançamentos para registrar saída.");

            var ultimoLanc = LancamentoEstoque.Last();

            if (qtde <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero.");
            if (qtde > ultimoLanc.saldoQtde)
                throw new InvalidOperationException("Não há estoque suficiente.");

            decimal custoMedioAtual = ultimoLanc.saldoCustoMedio;
            decimal totalSaida = qtde * custoMedioAtual;

            int novoSaldoQtde = ultimoLanc.saldoQtde - qtde;
            decimal novoSaldoTotal = ultimoLanc.saldoTotal - totalSaida;
            decimal novoSaldoCustoMedio = ultimoLanc.saldoCustoMedio;

            if (novoSaldoQtde == 0)
            {
                novoSaldoCustoMedio = 0;
                novoSaldoTotal = 0;
            }

            var novoLancamento = new LancamentoEstoque(
                data: data,
                historico: historico,
                saldoQuantidade: novoSaldoQtde,
                saldoCustoMedio: novoSaldoCustoMedio,
                saldoTotal: novoSaldoTotal,
                quantidadeSaida: qtde,
                custoMedioSaida: custoMedioAtual
            );

            LancamentoEstoque.Add(novoLancamento);
        }
    }
}
