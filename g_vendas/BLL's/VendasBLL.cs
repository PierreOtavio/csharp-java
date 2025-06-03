using g_vendas.DAL_s;
using g_vendas.Logger;
using g_vendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace g_vendas.BLL_s
{
    internal class VendasBLL
    {
        public static List<Venda> ObterTodasVendas()
        {
            try
            {
                return VendasDAL.buscarVendas();
            }
            catch (Exception ex)
            {
                loggerC_.Error($"{ex}");
                return new List<Venda>();
            }
        }

        public static bool CadastrarVenda(Venda venda)
        {
            try
            {
                // Validações de regras de negócio
                if (!ValidarVenda(venda))
                    return false;

                // Aplicar regras de negócio específicas
                AplicarRegrasPadrao(venda);

                VendasDAL.insertVenda(venda);
                return true;
            }
            catch (Exception ex)
            {
                loggerC_.Error($"{ex}");
                return false;
            }
        }

        public static bool ValidarVenda(Venda venda)
        {
            if (venda == null)
            {
                loggerC_.Error($"Venda não pode ser nula");
                return false;
            }

            if (venda.valor_total <= 0)
            {
                loggerC_.Error("Valor total deve ser maior que zero");
                return false;
            }

            if (venda.desconto < 0)
            {
                loggerC_.Error("Desconto não pode ser negativo");
                return false;
            }

            if (venda.desconto > venda.valor_total)
            {
                loggerC_.Error("Desconto não pode ser maior que o valor total");
                return false;
            }

            if (venda.data_venda > DateTime.Now)
            {
                loggerC_.Error("Data da venda não pode ser no futuro");
                return false;
            }

            return true;
        }

        private static void AplicarRegrasPadrao(Venda venda)
        {
            // Aplicar desconto máximo de 50%
            decimal descontoMaximo = venda.valor_total * 0.5m;
            if (venda.desconto > descontoMaximo)
            {
                venda.desconto = descontoMaximo;
                loggerC_.Error($"Desconto limitado a 50% do valor total: {descontoMaximo:C}");
            }

            // Arredondar valores para 2 casas decimais
            venda.valor_total = Math.Round(venda.valor_total, 2);
            venda.desconto = Math.Round(venda.desconto, 2);
        }

        public static decimal CalcularValorFinal(Venda venda)
        {
            if (venda == null) return 0;
            return venda.valor_total - venda.desconto;
        }

        public static List<Venda> ObterVendasPorPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                var todasVendas = VendasDAL.buscarVendas();
                return todasVendas.Where(v => v.data_venda >= dataInicio && v.data_venda <= dataFim).ToList();
            }
            catch (Exception ex)
            {
                loggerC_.Error($"{ex}");
                return new List<Venda>();
            }
        }

        public static decimal CalcularTotalVendasDia(DateTime data)
        {
            try
            {
                var vendasDia = ObterVendasPorPeriodo(data.Date, data.Date.AddDays(1).AddTicks(-1));
                return vendasDia.Sum(v => CalcularValorFinal(v));
            }
            catch (Exception ex)
            {
                loggerC_.Error($"{ex}");
                return 0;
            }
        }

        public static bool ExportarParaExcel(List<Venda> vendas = null)
        {
            try
            {
                if (vendas == null)
                    vendas = ObterTodasVendas();

                if (!vendas.Any())
                {
                    Console.WriteLine("Não há vendas para exportar");
                    return false;
                }

                VendasDAL.exportToExcel(vendas);
                return true;
            }
            catch (Exception ex)
            {
                loggerC_.Error($"{ex}");
                return false;
            }
        }

        public static Dictionary<Venda.forma_pagamento, int> ObterEstatisticasFormaPagamento()
        {
            try
            {
                var vendas = ObterTodasVendas();
                var estatisticas = new Dictionary<Venda.forma_pagamento, int>();

                foreach (Venda.forma_pagamento forma in Enum.GetValues(typeof(Venda.forma_pagamento)))
                {
                    estatisticas[forma] = 0;
                }

                // Como a forma de pagamento não está sendo salva ainda, retorna estatísticas vazias
                // Quando implementar a gravação da forma de pagamento, pode contar aqui

                return estatisticas;
            }
            catch (Exception ex)
            {
                loggerC_.Error($"{ex}");
                return new Dictionary<Venda.forma_pagamento, int>();
            }
        }
    }
}
