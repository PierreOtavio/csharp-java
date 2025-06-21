using g_vendas.BLL_s;
using g_vendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace g_vendas.Controllers
{
    public class VendasController
    {
        //private readonly VendasBLL _bll = new VendasBLL();

        // Listar todas as vendas
        public List<Venda> ListarVendas()
        {
            try
            {
                return VendasBLL.ObterTodasVendas();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao carregar vendas: {ex.Message}");
            }
        }

        // Cadastrar venda completa
        // No VendasController
        public static int CadastrarVendaCompleta(Venda venda)
        {
            try
            {
                // Supondo que VendasBLL.CadastrarVenda retorna o ID gerado
                int idVenda = VendasBLL.CadastrarVenda(venda);
                if (idVenda <= 0)
                    throw new Exception("Falha ao cadastrar venda. Verifique os dados informados.");
                return idVenda;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao cadastrar venda: {ex.Message}");
            }
        }


        // Buscar vendas por período
        public List<Venda> BuscarVendasPorPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                return VendasBLL.ObterVendasPorPeriodo(dataInicio, dataFim);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar vendas por período: {ex.Message}");
            }
        }

        // Calcular total de vendas do dia
        public decimal CalcularTotalDia(DateTime data)
        {
            try
            {
                return VendasBLL.CalcularTotalVendasDia(data);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao calcular total do dia: {ex.Message}");
            }
        }

        // Exportar vendas para Excel
        public void ExportarVendas(List<Venda> vendas = null)
        {
            try
            {
                bool sucesso = VendasBLL.ExportarParaExcel(vendas);
                if (!sucesso)
                    throw new Exception("Falha ao exportar relatório.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao exportar relatório: {ex.Message}");
            }
        }

        // Validar dados de venda antes do cadastro
        public string ValidarVenda(DateTime dataVenda, decimal valorTotal, decimal desconto)
        {
            try
            {
                var vendaTemp = new Venda(dataVenda, valorTotal, desconto, Venda.forma_pagamento.Dinheiro);
                bool valida = VendasBLL.ValidarVenda(vendaTemp);
                return valida ? "Dados da venda são válidos" : "Dados da venda são inválidos";
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro na validação: {ex.Message}");
            }
        }

        // Obter estatísticas de formas de pagamento
        public object ObterEstatisticasFormaPagamento()
        {
            try
            {
                return VendasBLL.ObterEstatisticasFormaPagamento();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter estatísticas: {ex.Message}");
            }
        }

        // Calcular valor final da venda (com desconto)
        public decimal CalcularValorFinal(decimal valorTotal, decimal desconto)
        {
            try
            {
                var vendaTemp = new Venda { valor_total = valorTotal, desconto = desconto };
                return VendasBLL.CalcularValorFinal(vendaTemp);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao calcular valor final: {ex.Message}");
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

        public Venda BuscarVendaPorId(int idVenda)
        {
            // Supondo que você já tenha um método para listar todas as vendas:
            List<Venda> vendas = ListarVendas();
            return vendas.FirstOrDefault(v => v.id_venda == idVenda);
        }
    }
}
