using g_vendas.BLL_s;
using g_vendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace g_vendas.Controllers
{
    internal class VendasController
    {
        public class ResponseModel
        {
            public bool Sucesso { get; set; }
            public string Mensagem { get; set; }
            public object Dados { get; set; }

            public ResponseModel(bool sucesso, string mensagem, object dados = null)
            {
                Sucesso = sucesso;
                Mensagem = mensagem;
                Dados = dados;
            }
        }

        // Listar todas as vendas
        public ResponseModel ListarVendas()
        {
            try
            {
                var vendas = VendasBLL.ObterTodasVendas();
                return new ResponseModel(true, "Vendas carregadas com sucesso", vendas);
            }
            catch (Exception ex)
            {
                return new ResponseModel(false, $"Erro ao carregar vendas: {ex.Message}");
            }
        }

        // Cadastrar nova venda
        public ResponseModel CadastrarVenda(DateTime dataVenda, decimal valorTotal, decimal desconto, Venda.forma_pagamento formaPagamento)
        {
            try
            {
                var novaVenda = new Venda(dataVenda, valorTotal, desconto, formaPagamento);

                bool sucesso = VendasBLL.CadastrarVenda(novaVenda);

                if (sucesso)
                    return new ResponseModel(true, "Venda cadastrada com sucesso!", novaVenda);
                else
                    return new ResponseModel(false, "Falha ao cadastrar venda. Verifique os dados informados.");
            }
            catch (Exception ex)
            {
                return new ResponseModel(false, $"Erro ao cadastrar venda: {ex.Message}");
            }
        }

        // Cadastrar venda completa (sobrecarga)
        public ResponseModel CadastrarVenda(Venda venda)
        {
            try
            {
                bool sucesso = VendasBLL.CadastrarVenda(venda);

                if (sucesso)
                    return new ResponseModel(true, "Venda cadastrada com sucesso!", venda);
                else
                    return new ResponseModel(false, "Falha ao cadastrar venda. Verifique os dados informados.");
            }
            catch (Exception ex)
            {
                return new ResponseModel(false, $"Erro ao cadastrar venda: {ex.Message}");
            }
        }

        // Buscar vendas por período
        public ResponseModel BuscarVendasPorPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                var vendas = VendasBLL.ObterVendasPorPeriodo(dataInicio, dataFim);
                string mensagem = vendas.Any() ? $"{vendas.Count} vendas encontradas" : "Nenhuma venda encontrada no período";

                return new ResponseModel(true, mensagem, vendas);
            }
            catch (Exception ex)
            {
                return new ResponseModel(false, $"Erro ao buscar vendas por período: {ex.Message}");
            }
        }

        // Calcular total de vendas do dia
        public ResponseModel CalcularTotalDia(DateTime data)
        {
            try
            {
                decimal total = VendasBLL.CalcularTotalVendasDia(data);
                return new ResponseModel(true, $"Total do dia: {total:C}", total);
            }
            catch (Exception ex)
            {
                return new ResponseModel(false, $"Erro ao calcular total do dia: {ex.Message}");
            }
        }

        // Exportar vendas para Excel
        public ResponseModel ExportarVendas(List<Venda> vendas = null)
        {
            try
            {
                bool sucesso = VendasBLL.ExportarParaExcel(vendas);

                if (sucesso)
                    return new ResponseModel(true, "Relatório exportado com sucesso para a área de trabalho!");
                else
                    return new ResponseModel(false, "Falha ao exportar relatório.");
            }
            catch (Exception ex)
            {
                return new ResponseModel(false, $"Erro ao exportar relatório: {ex.Message}");
            }
        }

        // Validar dados de venda antes do cadastro
        public ResponseModel ValidarVenda(DateTime dataVenda, decimal valorTotal, decimal desconto)
        {
            try
            {
                var vendaTemp = new Venda(dataVenda, valorTotal, desconto, Venda.forma_pagamento.Dinheiro);
                bool valida = VendasBLL.ValidarVenda(vendaTemp);

                if (valida)
                    return new ResponseModel(true, "Dados da venda são válidos");
                else
                    return new ResponseModel(false, "Dados da venda são inválidos");
            }
            catch (Exception ex)
            {
                return new ResponseModel(false, $"Erro na validação: {ex.Message}");
            }
        }

        // Obter estatísticas de formas de pagamento
        public ResponseModel ObterEstatisticasFormaPagamento()
        {
            try
            {
                var estatisticas = VendasBLL.ObterEstatisticasFormaPagamento();
                return new ResponseModel(true, "Estatísticas carregadas", estatisticas);
            }
            catch (Exception ex)
            {
                return new ResponseModel(false, $"Erro ao obter estatísticas: {ex.Message}");
            }
        }

        // Calcular valor final da venda (com desconto)
        public ResponseModel CalcularValorFinal(decimal valorTotal, decimal desconto)
        {
            try
            {
                var vendaTemp = new Venda { valor_total = valorTotal, desconto = desconto };
                decimal valorFinal = VendasBLL.CalcularValorFinal(vendaTemp);

                return new ResponseModel(true, $"Valor final: {valorFinal:C}", valorFinal);
            }
            catch (Exception ex)
            {
                return new ResponseModel(false, $"Erro ao calcular valor final: {ex.Message}");
            }
        }

        // Métodos utilitários para os Forms
        public List<string> ObterFormasPagamento()
        {
            return Enum.GetNames(typeof(Venda.forma_pagamento)).ToList();
        }

        public Venda.forma_pagamento ConverterFormaPagamento(string formaPagamento)
        {
            return (Venda.forma_pagamento)Enum.Parse(typeof(Venda.forma_pagamento), formaPagamento);
        }
    }
}
