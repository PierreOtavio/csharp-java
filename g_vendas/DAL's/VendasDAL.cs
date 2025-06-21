using g_vendas.ExportacaoExcel;
using g_vendas.Models;
using System;
using System.Collections.Generic;
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

        public static int insertVenda(Venda venda)
        {
            var valores = new Dictionary<string, object>
    {
        {"data_venda", venda.data_venda},
        {"desconto", venda.desconto},
        {"valor_total", venda.valor_total},
        {"forma_pagamento", venda.FormaPagamento}
    };

            var consultor = new ConsultorUniversal<Venda>(
                tabela: "vendas",
                campos: "*",
                armazenador: null
            );

            long idGerado = consultor.Inserir("vendas", valores);
            return (int)idGerado;
        }


        public static void exportToExcel(List<Venda> listaVenda)
        {
            ExcelExporter.ExportToExcel(listaVenda, "RelatórioVendas");
        }
    }
}
