using g_vendas.ExportacaoExcel;
using g_vendas.Models;
using System;
using System.Collections.Generic;
using Teste1.Data.Query;

namespace g_vendas.DAL_s
{
    internal class SaboresPizzaDAL
    {
        public static List<SaboresPizza> buscarSabores()
        {
            List<SaboresPizza> listaSabores = new List<SaboresPizza>();
            Action<SaboresPizza> storeOn = s => listaSabores.Add(s);

            var consultor = new ConsultorUniversal<SaboresPizza>
                (
                    tabela: "sabores_pizza",
                    campos: "*",
                    armazenador: storeOn
                );

            consultor.BuscarEArmazenar();
            return listaSabores;
        }

        public static void insertSabor(SaboresPizza sabores)
        {
            var valores = new Dictionary<string, object>
                {
                    {"nome", sabores.nome },
                    {"descricao", sabores.descricao }
                };

            var consultor = new ConsultorUniversal<SaboresPizza>
                (
                    tabela: "sabores_pizza",
                    campos: "*",
                    armazenador: null
                );

            consultor.Inserir("sabores_pizza", valores);
        }

        public static void exportToExcel(List<SaboresPizza> listaSabores)
        {
            ExcelExporter.ExportToExcel(listaSabores, "Sabores");
        }
    }
}
