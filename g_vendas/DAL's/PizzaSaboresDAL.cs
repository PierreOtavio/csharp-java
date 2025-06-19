using g_vendas.ExportacaoExcel;
using g_vendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Teste1.Data.Query;

namespace g_vendas.DAL_s
{
    internal class PizzaSaboresDAL
    {
        public static List<PizzaSabores> search()
        {
            List<PizzaSabores> listaPSab = new List<PizzaSabores>();
            Action<PizzaSabores> storeOn = ps => listaPSab.Add(ps);

            var consultor = new ConsultorUniversal<PizzaSabores>
                (
                    tabela: "pizza_sabores",
                    campos: "*",
                    armazenador: storeOn
                );

            consultor.BuscarEArmazenar();
            return listaPSab;
        }

        public static decimal CalcularProporcaoMedia()
        {
            var lista = search(); // busca todos os sabores
            if (lista == null || lista.Count == 0)
                return 0;

            return lista.Average(s => s.proporcao);
        }



        public static void exportToExcel(List<PizzaSabores> listaPSab)
        {
            ExcelExporter.ExportToExcel(listaPSab, "RelatórioPizzaSabores");
        }
    }
}
