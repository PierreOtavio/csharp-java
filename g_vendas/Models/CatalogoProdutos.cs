using System.Collections.Generic;

namespace g_vendas.Models
{
    public static class CatalogoProdutos
    {
        // Dicionário de produtos (Nome do produto -> Preço)
        public static Dictionary<string, decimal> Precos = new Dictionary<string, decimal>
        {
            // Pizzas
            { "Grande", 59.90m },
            { "Média", 49.90m },
            { "Pequena", 29.90m },

            // Refrigerantes (por litragem)
            { "2L", 14.90m },
            { "1,5L", 12.90m },
            { "1L", 10.00m },
            { "350mL", 5.00m },

            //{ "Fanta Laranja 2L", 14.90m },
            //{ "Fanta Laranja 1,5L", 12.90m },
            //{ "Fanta Laranja 1L", 10.00m },
            //{ "Fanta Laranja 350mL", 5.00m },

            //{ "Guaraná Antártica 2L", 14.90m },
            //{ "Guaraná Antártica 1,5L", 12.90m },
            //{ "Guaraná Antártica 1L", 10.00m },
            //{ "Guaraná Antártica 350mL", 5.00m }
        };

        // Método para buscar o preço pelo nome do produto
        public static decimal ObterPreco(string nomeProduto)
        {
            if (Precos.TryGetValue(nomeProduto, out decimal preco))
                return preco;
            else
                return 0m; // ou lance uma exceção, se preferir
        }
    }
}
