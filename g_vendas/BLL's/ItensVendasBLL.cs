using g_vendas.DAL_s;
using g_vendas.Logger;
using g_vendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace g_vendas.BLL_s
{
    public class ItensVendasBLL
    {
        /// <summary>
        /// Busca todos os itens de venda.
        /// </summary>
        public List<ItensVendas> BuscarItensVendas()
        {
            try
            {
                return ItensVendasDAL.buscarItensVendas();
            }
            catch (Exception ex)
            {
                // Logar erro, se necessário
                throw new Exception("Erro ao buscar itens de vendas.", ex);
            }
        }

        /// <summary>
        /// Insere um novo item de venda com validação.
        /// </summary>
        public void InserirItem(ItensVendas item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "O item não pode ser nulo.");

            if (item.Quantidade <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");

            if (item.Preco_Unitario < 0)
                throw new ArgumentException("O preço unitário não pode ser negativo.");

            try
            {
                ItensVendasDAL.insertItem(item);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir item de venda.", ex);
            }
        }

        /// <summary>
        /// Exporta uma lista de itens de venda para Excel.
        /// </summary>
        public void ExportarItensParaExcel(List<ItensVendas> listaItens)
        {
            if (listaItens == null)
                throw new ArgumentNullException(nameof(listaItens), "A lista de itens não pode ser nula.");

            try
            {
                ItensVendasDAL.exportToExcel(listaItens);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao exportar itens para Excel.", ex);
            }
        }

        /// <summary>
        /// Calcula o valor total de um item de venda.
        /// </summary>
        public decimal CalcularValorTotalItem(ItensVendas item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "O item não pode ser nulo.");

            return item.Quantidade * item.Preco_Unitario;
        }

        /// <summary>
        /// Busca itens de venda por ID da venda.
        /// </summary>
        public List<ItensVendas> BuscarItensPorVenda(int idVenda)
        {
            var todosItens = BuscarItensVendas();
            return todosItens.Where(i => i.Id_Venda == idVenda).ToList();
        }

        /// <summary>
        /// Atualiza um item de venda existente.
        /// </summary>
        public void AtualizarItem(ItensVendas item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "O item não pode ser nulo.");

            if (item.Quantidade <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");

            if (item.Preco_Unitario < 0)
                throw new ArgumentException("O preço unitário não pode ser negativo.");

            try
            {
                ItensVendasDAL.updateItem(item);
            }
            catch (Exception ex)
            {
                loggerC_.Error("Erro ao atualizar item de venda.", ex);
            }
        }

        public void RemoverItem(int idItem)
        {
            try
            {
                ItensVendasDAL.deleteItem(idItem);
            }
            catch (Exception ex)
            {
                loggerC_.Error("Erro ao remover item de venda.", ex);
            }
        }
    }
}
