using g_vendas.DAL_s;
using g_vendas.Logger; // Importa sua classe de logger
using g_vendas.Models;
using System;
using System.Collections.Generic;

namespace g_vendas.BLL
{
    public static class ProdutosBLL
    {
        // Busca todos os produtos (com tratamento de erro e log)
        public static List<Produtos> GetProdutos()
        {
            try
            {
                loggerC_.Info("Buscando todos os produtos no banco de dados.");
                return ProdutosDAL.buscarProdutos();
            }
            catch (Exception ex)
            {
                loggerC_.Error("Erro ao buscar produtos.", ex);
                throw new ApplicationException("Falha ao carregar produtos do banco.", ex);
            }
        }

        // Insere um novo produto (com validações e log)
        public static void InsertProduto(Produtos produto)
        {
            // Validações básicas
            if (string.IsNullOrWhiteSpace(produto.Nome))
            {
                loggerC_.Error("Tentativa de inserir produto com nome vazio.");
                throw new ArgumentException("O nome do produto é obrigatório.");
            }

            if (produto.Preco <= 0)
            {
                loggerC_.Error($"Tentativa de inserir produto com preço inválido: {produto.Preco}");
                throw new ArgumentException("O preço deve ser maior que zero.");
            }

            if (string.IsNullOrWhiteSpace(produto.Descricao))
            {
                loggerC_.Error("Tentativa de inserir produto com descrição vazia.");
                throw new ArgumentException("A descrição é obrigatória.");
            }

            try
            {
                loggerC_.Info("Inserindo produto: {Nome}, Tipo: {Tipo}", produto.Nome, produto.Tipo);
                ProdutosDAL.insertProd(produto);
                loggerC_.Info("Produto inserido com sucesso: {Nome}", produto.Nome);
            }
            catch (Exception ex)
            {
                loggerC_.Error("Erro ao inserir produto.", ex);
                throw new ApplicationException("Falha ao salvar o produto.", ex);
            }
        }

    }
}
