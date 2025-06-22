<!-- Banner estilizado --> <p align="center"> <img src="https://capsule-render.vercel.app/api?type=waving&color=0:aa4b6b,100:6b6b83&height=120&section=header&text=Sistema%20de%20Vendas%20-%20Pizza%20da%20Gr%C3%A1&fontSize=30&fontColor=fff"/> </p> <h1 align="center">🍕 Sistema de Vendas - Pizza da Grá</h1> <p align="center"> <b>Projeto Desktop • Windows Forms (C#) + MySQL</b> </p> <p align="center"> <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white"/> <img src="https://img.shields.io/badge/.NET%20Framework-512BD4?style=for-the-badge&logo=.net&logoColor=white"/> <img src="https://img.shields.io/badge/MySQL-4479A1?style=for-the-badge&logo=mysql&logoColor=white"/> <img src="https://img.shields.io/badge/Windows%20Forms-0078D4?style=for-the-badge&logo=windows&logoColor=white"/> </p>
📖 Sobre o Projeto

Este sistema de vendas foi desenvolvido como parte de um projeto acadêmico no curso técnico em Desenvolvimento de Sistemas. O objetivo foi consolidar os conhecimentos adquiridos em C# com Windows Forms e MySQL, criando uma aplicação prática e funcional para gerenciamento de vendas de uma pizzaria fictícia chamada Pizza da Grá.

O projeto aplica conceitos como:


  Programação Orientada a Objetos

  Acesso a banco de dados relacional com MySQL

  Criação programática de interfaces gráficas

  Estruturação em camadas (Model, Controller, DAL)



🛠️ Tecnologias Utilizadas


  Linguagem: C# (.NET Framework 4.7.2)

  Interface Gráfica: Windows Forms (sem designer visual)

  Banco de Dados: MySQL 8.0

  IDE: Visual Studio 2022

  Conectividade: MySql.Data

  Bibliotecas: ClosedXML, System.Drawing


📦 Funcionalidades


  Cadastro de produtos e sabores de pizza

  Registro e visualização de vendas

  Seleção de múltiplos sabores com controle de proporções

  Relatórios e exportação para Excel

  Validações básicas de dados e tratamento de erros
  

🧱 Estrutura do Banco de Dados 
```

CREATE SCHEMA IF NOT EXISTS DB_vendas;
USE DB_vendas;

CREATE TABLE produtos (
  id_produto INT PRIMARY KEY AUTO_INCREMENT,
  nome VARCHAR(100),
  tipo ENUM('Pizza', 'Refrigerante', 'Outro'),
  descricao VARCHAR(200),
  preco DECIMAL(10,2)
);

CREATE TABLE vendas (
  id_venda INT PRIMARY KEY AUTO_INCREMENT,
  data_venda DATETIME DEFAULT CURRENT_TIMESTAMP,
  valor_total DECIMAL(10,2),
  desconto DECIMAL(10,2),
  forma_pagamento ENUM('Dinheiro', 'Cartão', 'PIX', 'Outro')
);

CREATE TABLE pizza_sabores (
  id_item INT,
  id_sabor INT,
  proporcao DECIMAL(3,2),
  PRIMARY KEY (id_item, id_sabor),
  FOREIGN KEY (id_item) REFERENCES itens_venda(id_item) ON DELETE CASCADE,
  FOREIGN KEY (id_sabor) REFERENCES sabores_pizza(id_sabor)
);
```

🧩 Arquitetura em Camadas
  Models: Representações das entidades (Produto, Venda, PizzaSabores, etc)

  Controllers: Lógica de negócio para operações de venda e cadastro

  DAL (Data Access Layer): ConsultorUniversal<T> com operações genéricas em MySQL

  UI: Telas criadas programaticamente com C#

🧪 Telas Implementadas


  FormLogin: Tela inicial com login

  FormMainMenu: Menu principal com navegação

  FormCadastroVenda1 & 2: Cadastro de pedidos com múltiplos sabores

  FormRelatorioHome: Dashboard com vendas

  FormVerMais: Visualização detalhada de um pedido

  
🚧 Como Rodar o Projeto


  Clone o repositório:

```git clone https://github.com/seuusuario/seurepo.git```

  Importe o projeto no Visual Studio 2022

  Configure a string de conexão com seu banco MySQL

  Execute o script SQL para criar o banco de dados

  Compile e execute a aplicação
  

💡 Aprendizados


  Uso de enums e relacionamentos em banco de dados

  Criação programática de formulários e responsividade simples

  Estruturação básica em camadas (MVC simplificado)

  Exportação para Excel e filtros por data

  CRUD genérico com programação genérica em C#
  

📈 Resultados Alcançados

  Sistema funcional de vendas com interface intuitiva

  Registro de pizzas com múltiplos sabores e proporções

  Exportação de relatórios para Excel

  Tratamento de exceções e validação de dados

🎯 Possíveis Melhorias Futuras


  Implementação de testes automatizados

  Uso de padrões de projeto (como Repository, Service)

  Refatoração para maior desacoplamento

  Interface mais moderna e personalizável

  Documentação de código mais completa
  

👨‍💻 Sobre o Autor

Sou [Otávio Pierre], estudante do curso técnico em Desenvolvimento de Sistemas, apaixonado por programação e design de sistemas. Este projeto marca meu progresso no aprendizado de C# e MySQL, com foco em construção de sistemas reais e funcionais.
📬 Contato


  Email: perresla111@gmail.com

  Telefone: (37) 99832-0306

  
<p align="center"> <img src="https://komarev.com/ghpvc/?username=SeuGitHub&color=red" alt="Profile views"/> </p>
