using g_vendas.Logger;
using g_vendas.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Teste1.Data.Query
{
    public class ConsultorUniversal<T> where T : new()
    {
        private readonly string _connectionString = "server=127.0.0.1;port=3306;uid=root;pwd=;database=db_vendas;";
        private readonly string _tabela;
        private readonly string _campos;
        private readonly string _condicao;
        private Action<T> _armazenador;
        public ConsultorUniversal(string tabela, string campos, Action<T> armazenador, string condicao = null)
        {
            _tabela = tabela;
            _campos = campos;
            _armazenador = armazenador;
            _condicao = condicao;
        }

        public void BuscarEArmazenar()
        {
            string query;
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            // Decide automaticamente se aplica filtro ou não
            if (string.IsNullOrWhiteSpace(_condicao))
                query = $"SELECT {_campos} FROM {_tabela}";
            else
                query = $"SELECT {_campos} FROM {_tabela} WHERE {_condicao}";

            if (!string.IsNullOrWhiteSpace(_condicao))
            {
                // Supondo que a condição venha no formato "campo = @parametro"
                // Exemplo: "id = @id"
                var parts = _condicao.Split('=');
                if (parts.Length == 2)
                {
                    var paramName = parts[1].Trim();
                    query += " WHERE " + parts[0].Trim() + " = " + paramName;
                    parameters.Add(new MySqlParameter(paramName, parts[1].Trim()));
                }
            }

            try
            {
                using (var conexao = new MySqlConnection(_connectionString))
                {
                    conexao.Open();
                    using (var cmd = new MySqlCommand(query, conexao))
                    using (var reader = cmd.ExecuteReader())
                    {
                        var propriedades = typeof(T).GetProperties();

                        while (reader.Read())
                        {
                            T obj = new T();

                            foreach (var prop in propriedades)
                            {
                                if (!reader.HasColumn(prop.Name)) continue;
                                var valor = reader[prop.Name];
                                if (valor != DBNull.Value)
                                {
                                    if (prop.Name == "FormaPagamento" || prop.Name == "forma_pagamento")
                                    {
                                        string formaPagamentoStr = valor.ToString();
                                        if (Enum.TryParse<Venda.forma_pagamento>(formaPagamentoStr, out var formaPagamento))
                                        {
                                            prop.SetValue(obj, formaPagamento);
                                        }
                                        else
                                        {
                                            prop.SetValue(obj, Venda.forma_pagamento.Dinheiro); // valor padrão
                                        }
                                    }
                                    else
                                    {
                                        prop.SetValue(obj, Convert.ChangeType(valor, prop.PropertyType));
                                    }
                                }
                            }

                            _armazenador(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loggerC_.Error($"{ex}");
            }
        }

        public long Inserir(string tabela, Dictionary<string, object> valores)
        {
            try
            {
                var campos = string.Join(", ", valores.Keys);
                var parametros = string.Join(", ", valores.Keys.Select(k => "@" + k));
                string query = $"INSERT INTO {tabela} ({campos}) VALUES ({parametros});";

                using (var conexao = new MySqlConnection(_connectionString))
                {
                    conexao.Open();
                    using (var cmd = new MySqlCommand(query, conexao))
                    {
                        foreach (var kvp in valores)
                            cmd.Parameters.AddWithValue("@" + kvp.Key, kvp.Value ?? DBNull.Value);

                        cmd.ExecuteNonQuery();
                        return cmd.LastInsertedId; // Retorna o ID gerado
                    }
                }
            }
            catch (Exception e)
            {
                loggerC_.Error($"{e}");
                return 0; // Retorna 0 em caso de erro
            }
        }


        public void Update(string tabela, Dictionary<string, object> valores, Dictionary<string, object> condicoes)
        {
            if (valores == null || valores.Count == 0)
                throw new ArgumentException("Valores para atualização não podem ser nulos ou vazios.");

            try
            {
                // Monta a parte SET do SQL: campo1 = @campo1, campo2 = @campo2
                var sets = string.Join(", ", valores.Keys.Select(k => $"{k} = @{k}"));

                // Monta a parte WHERE do SQL: campo3 = @campo3 AND campo4 = @campo4
                var whereClauses = string.Join(" AND ", condicoes.Keys.Select(k => $"{k} = @{k}"));

                string query = $"UPDATE {tabela} SET {sets} WHERE {whereClauses}";

                using (var conexao = new MySqlConnection(_connectionString))
                {
                    conexao.Open();
                    using (var cmd = new MySqlCommand(query, conexao))
                    {
                        // Adiciona parâmetros dos valores a atualizar
                        foreach (var kvp in valores)
                        {
                            cmd.Parameters.AddWithValue($"@{kvp.Key}", kvp.Value ?? DBNull.Value);
                        }

                        // Adiciona parâmetros das condições WHERE
                        foreach (var kvp in condicoes)
                        {
                            cmd.Parameters.AddWithValue($"@{kvp.Key}", kvp.Value ?? DBNull.Value);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                loggerC_.Error($"{ex}");
            }
        }


        public void Delete(string tabela, Dictionary<string, object> condicoes)
        {
            if (condicoes == null || condicoes.Count == 0)
                throw new ArgumentException("Condições para exclusão não podem ser nulas ou vazias.");

            try
            {
                // Monta a parte WHERE do SQL: campo1 = @campo1 AND campo2 = @campo2
                var whereClauses = string.Join(" AND ", condicoes.Keys.Select(k => $"{k} = @{k}"));

                string query = $"DELETE FROM {tabela} WHERE {whereClauses}";

                using (var conexao = new MySqlConnection(_connectionString))
                {
                    conexao.Open();
                    using (var cmd = new MySqlCommand(query, conexao))
                    {
                        foreach (var kvp in condicoes)
                        {
                            cmd.Parameters.AddWithValue($"@{kvp.Key}", kvp.Value ?? DBNull.Value);
                        }
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                loggerC_.Error($"{ex}");
            }
        }

    }

    public static class ExtensoesDataReader
    {
        public static bool HasColumn(this MySqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
