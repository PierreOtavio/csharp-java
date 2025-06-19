using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace g_vendas.ExportacaoExcel
{
    public static class ExcelExporter
    {
        public static void ExportToExcel<T>(List<T> lista, string nomeArquivoPrefixo = "Relatorio", string nomePlanilha = "Dados")
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string nomeArquivo = $"{nomeArquivoPrefixo}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            string caminhoCompleto = Path.Combine(desktopPath, nomeArquivo);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(nomePlanilha);

                // Pega as propriedades públicas da classe
                PropertyInfo[] propriedades = typeof(T).GetProperties().OrderBy(p => p.Name).ToArray();

                // Preenche o cabeçalho
                for (int coluna = 0; coluna < propriedades.Length; coluna++)
                {
                    worksheet.Cell(1, coluna + 1).Value = propriedades[coluna].Name;
                }

                // Preenche as linhas
                for (int linha = 0; linha < lista.Count; linha++)
                {
                    for (int coluna = 0; coluna < propriedades.Length; coluna++)
                    {
                        object valor = propriedades[coluna].GetValue(lista[linha]);
                        worksheet.Cell(linha + 2, coluna + 1).Value = valor?.ToString() ?? string.Empty;
                    }
                }

                workbook.SaveAs(caminhoCompleto);
            }

            Console.WriteLine($"Arquivo salvo na área de trabalho: {nomeArquivo}");
        }

        private static object FormatValue(object value, Type type)
        {
            if (value == null) return string.Empty;

            // Formatação especial para tipos específicos
            if (type == typeof(DateTime) || type == typeof(DateTime?))
                return ((DateTime)value).ToString("dd/MM/yyyy HH:mm:ss");

            if (type == typeof(decimal) || type == typeof(decimal?))
                return Math.Round((decimal)value, 2);

            return value.ToString();
        }

        public static void ExportAllToExcel(
            List<(string nomePlanilha, object listaDados)> dados,
            string nomeArquivo = "RelatorioCompleto.xlsx")
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string caminhoCompleto = Path.Combine(desktopPath, nomeArquivo);

            using (var workbook = new XLWorkbook())
            {
                foreach (var (nomePlanilha, lista) in dados)
                {
                    var worksheet = workbook.Worksheets.Add(nomePlanilha);
                    Type tipo = lista.GetType().GetGenericArguments()[0];
                    PropertyInfo[] properties = tipo.GetProperties();

                    // Cabeçalho
                    for (int col = 0; col < properties.Length; col++)
                        worksheet.Cell(1, col + 1).Value = properties[col].Name;

                    // Linhas
                    int row = 2;
                    foreach (var item in (System.Collections.IEnumerable)lista)
                    {
                        for (int col = 0; col < properties.Length; col++)
                        {
                            object value = properties[col].GetValue(item);
                            worksheet.Cell(row, col + 1).Value = value?.ToString() ?? string.Empty;
                        }
                        row++;
                    }
                }

                workbook.SaveAs(caminhoCompleto);
            }
        }

    }
}
