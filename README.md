# Sistema de Gerenciamento Integrado para Confecção e Comércio de Vestuário Personalizado (WPF)

Este projeto consiste no desenvolvimento de um **sistema de gerenciamento integrado**, criado para atender empresas do setor de **confecção e comércio de vestuário personalizado**. O software tem como objetivo otimizar processos internos, oferecendo controle eficiente de **estoque, produção, vendas, fornecedores, clientes e entregas**, além de proporcionar maior organização e confiabilidade dos dados.

Desenvolvido em **C# / WPF**, o sistema utiliza um **banco de dados relacional (MySQL)** para garantir integridade das informações, suporte a múltiplas operações e escalabilidade. Com foco na eficiência, o sistema busca reduzir desperdícios, agilizar tarefas operacionais e apoiar decisões estratégicas através de dados centralizados.

---

## ✅ Funcionalidades Principais

* Controle de estoque (entradas, saídas, níveis críticos e materiais)
* Acompanhamento do fluxo de produção de vestuário personalizado
* Gerenciamento de pedidos, status e prazos de entrega
* Cadastro e gestão de fornecedores e clientes
* Controle de vendas, compras e produtos finalizados
* Relatórios gerenciais de estoque, produção, vendas e logística
* Interface intuitiva projetada para usuários administrativos e operacionais
* Integração entre setores (estoque, produção, vendas e logística)

---

## 🎯 Objetivo Geral

Desenvolver um sistema que centralize e otimize os processos de gestão em empresas de vestuário personalizado, promovendo produtividade, organização, precisão de dados e visão estratégica.

---

## 🔧 Tecnologias Utilizadas

* **C# (.NET / WPF)**
* **XAML**
* **MySQL** (Banco de dados relacional)
* **Arquitetura em camadas (MVC + DAO)**
* **SQL (DDL, DML e consultas avançadas)**

---

## 📌 Status do Projeto

✅ Em desenvolvimento ativo  
🔄 Novas telas e integrações estão sendo implementadas  
🚀 Expansões futuras incluirão dashboard, melhorias no design e automações operacionais

---

## 📁 Estrutura (resumo)

* **/Views** — Telas WPF do sistema  
* **/Controllers** — Intermediário entre Views e Models; gerencia lógica de negócios e navegação  
* **/Models** — Classes de entidade e domínio  
* **/Database** — Scripts SQL (criação, inserts e consultas)  
* **/Docs** — Mini TCC, MER, DER e documentação do projeto  

---

## 🛠 Controllers

**Resumo Geral:**  
Os Controllers gerenciam a **lógica de negócios** e atuam como intermediários entre Views e DAOs.  
- Validam entradas e regras de negócio  
- Chamam DAOs para CRUD de dados  
- Controlam navegação entre telas  
- Tratam exceções e retornam mensagens de sucesso/erro  

**Controllers e explicações finais:**

* **ClienteController** — Gerencia clientes: valida campos, previne duplicidade de CPF/CNPJ e e-mail, realiza CRUD via ClienteDAO, controla navegação entre telas.  
* **EmpresaController** — Gerencia empresas e administradores, valida CNPJ, e-mail, razão social e senha; realiza CRUD via EmpresaDAO; controla telas.  
* **FuncionarioController** — Gerencia funcionários: inserção, atualização, exclusão e consulta via FuncionarioDAO.  
* **PedidoController** — Gerencia pedidos: permite listar todos os pedidos via PedidoDAO; futuramente CRUD completo.  
* **UsuarioController** — Gerencia usuários: valida campos, previne duplicidade de e-mail, cria hash + salt, realiza CRUD via UsuarioDAO.  
* **LoginController** — Responsável pela autenticação de usuários via UsuarioDAO.  
* **ConfigController** — Controla navegação entre telas administrativas e tratamento de erros de navegação.  

---

## 🗄 DAOs

**Resumo Geral:**  
Os DAOs (Data Access Objects) são responsáveis pela **persistência de dados** no MySQL:  
- Executam SQL (INSERT, SELECT, UPDATE, DELETE)  
- Transformam registros do banco em objetos Models  
- Centralizam lógica de acesso a dados, separando da lógica de negócios  

**DAOs e explicações finais:**

* **ClienteDAO** — CRUD de clientes, valida duplicidade de CPF/CNPJ e e-mail, lista nomes.  
* **EmpresaDAO** — CRUD de empresas, valida duplicidade de CNPJ, e-mail e razão social.  
* **FuncionarioDAO** — CRUD de funcionários, consulta por empresa ou ID.  
* **PedidoDAO** — Lista pedidos e mapeia campos nulos; futuro CRUD completo.  
* **UsuarioDAO** — Inserção com hash + salt, autenticação, listagem por empresa, atualização, exclusão e verificação de e-mail duplicado.  
* **Connection** — Classe utilitária que centraliza a string de conexão e fornece conexões abertas para todos os DAOs.

---

## 👥 Autores

Projeto desenvolvido por estudantes do Ensino Médio Integrado ao Técnico em Desenvolvimento de Sistemas — ETEC Hortolândia (2025).
