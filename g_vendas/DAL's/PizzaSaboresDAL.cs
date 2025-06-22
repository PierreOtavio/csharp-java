using g_vendas.ExportacaoExcel;
using g_vendas.Models;
using System;
using System.Collections.Generic;
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


        public static void insertProporcao(int id_item, int id_sabor, string pizzaProporcao)
        {
            var valores = new Dictionary<string, object>
            {
                {"id_item", id_item},
                {"id_sabor", id_sabor},
                {"proporcao", pizzaProporcao}
                // Adicione outros campos se necessário
            };
            var consultor = new ConsultorUniversal<PizzaSabores>(
                tabela: "pizza_sabores",
                campos: "*",
                armazenador: null
            );
            consultor.Inserir("pizza_sabores", valores);
        }

        public static void exportToExcel(List<PizzaSabores> listaPSab)
        {
            ExcelExporter.ExportToExcel(listaPSab, "RelatórioPizzaSabores");
        }
    }
}
