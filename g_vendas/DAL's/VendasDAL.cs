using ClosedXML.Excel;
using g_vendas.Models;
using System;
using System.Collections.Generic;
using System.IO;
using Teste1.Data.Query;

namespace g_vendas.DAL_s
{
    internal class VendasDAL
    {
        //private readonly string connectStr = "server=127.0.0.1;port=3306;uid=root;pwd=";
        public static List<Venda> buscarVendas()
        {
            List<Venda> listaVenda = new List<Venda>();
            Action<Venda> armazenarNaLista = v => listaVenda.Add(v);

            var consultorVendas = new ConsultorUniversal<Venda>
                (
                tabela: "vendas",
                campos: "*",
                armazenador: armazenarNaLista
                );

            consultorVendas.BuscarEArmazenar();
            return listaVenda;
        }

        public static void insertVenda(Venda venda)
        {
            var valores = new Dictionary<string, object>
            {
                {"data_venda", venda.data_venda},
                {"desconto", venda.desconto},
                {"valor_total", venda.valor_total}
                //{"forma_pagamento", venda.fo}
            };

            var consultor = new ConsultorUniversal<Venda>
                (
                tabela: "vendas",
                campos: "*",
                armazenador: null
                );

            consultor.Inserir("vendas", valores);
        }

        public static void exportToExcel(List<Venda> listaVenda)
        {
            string dkPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            string archiveName = $"RelatórioVendas_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            string completePath = Path.Combine(dkPath, archiveName);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Vendas");

                worksheet.Cell(1, 1).Value = "Forma Pagamento";
                worksheet.Cell(1, 2).Value = "Data Venda";
                worksheet.Cell(1, 3).Value = "Desconto Aplicado";
                worksheet.Cell(1, 4).Value = "Valor Desconto";

                int linha = 2;
                foreach (var venda in listaVenda)
                {
                    //worksheet.Cell(linha, 1).Value = venda.;
                    worksheet.Cell(linha, 2).Value = venda.data_venda.ToString("dd/MM/yyyy HH:mm:ss");
                    worksheet.Cell(linha, 3).Value = venda.desconto;
                    worksheet.Cell(linha, 4).Value = venda.valor_total;
                    linha++;
                }

                workbook.SaveAs(completePath);
            }
            Console.WriteLine($"arquivo salvo na área de trabalho {archiveName}");
        }
    }
}
