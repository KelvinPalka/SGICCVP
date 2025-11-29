create database MiniTCC_PNTJ;
use MiniTCC_PNTJ;

create table Empresa(
	id_empresa int primary key auto_increment,
	cnpj char(14) unique not null,
    nome_fantasia varchar(100) not null,
    email varchar(100) unique not null,
    telefone varchar(20) not null,
    razao_social varchar(100) not null,
    endereco varchar(200) not null
);

create table Cliente(
	id_cliente int primary key auto_increment,
    nome varchar(100) not null,
    cpf_cnpj char(14) unique not null,
    endereco varchar(200) not null,
    telefone varchar(20) not null,
    email varchar(100) unique not null
);

create table Estoque(
	id_estoque int primary key auto_increment,
    data_atualizacao date,
    local_armazenamento varchar(200) not null
);

create table Fornecedor(
	id_fornecedor int primary key auto_increment,
    cpf_cnpj char(14) unique not null,
    nome varchar(100) not null,
    email varchar(100) unique not null,
    telefone varchar(20) not null
);

create table Despesa(
	id_despesa int primary key auto_increment,
    descricao varchar(250),
    data_despesa date not null, 
    data_vencimento date not null,
    data_pagamento date,
    tipo varchar(100) not null,
    valor decimal(10,2) not null,
    forma_pagamento varchar(50)
);

create table Funcionario(
	id_funcionario int primary key auto_increment,
    nome varchar(100) not null,
    cpf char(14) unique not null,
    cargo varchar(100) not null,
    telefone varchar(20) not null,
    email varchar(100) unique not null
);

create table Linha(
	cod_linha int primary key auto_increment,
    cor varchar(100) not null,
    qntd_estocada decimal(10,2) not null,
    valor decimal(10,2) not null,
    id_estoque int,
    constraint fk_Linha_Estoque_id_estoque foreign key(id_estoque)
		references Estoque(id_estoque)
);

create table Fio(
	cod_fio int primary key auto_increment,
    qntd_estocada decimal(10,2) not null,
    valor decimal(10,2) not null,
    id_estoque int,
    constraint fk_Fio_Estoque_id_estoque foreign key(id_estoque)
		references Estoque(id_estoque)
);

create table Tecido(
	cod_tecido int primary key auto_increment,
    cor varchar(100) not null,
    tipo varchar(100) not null,
    textura varchar(100) not null,
    qntd_estocada decimal(10,2) not null,
    valor decimal(10,2) not null,
    id_estoque int,
    constraint fk_Tecido_Estoque_id_estoque foreign key(id_estoque)
		references Estoque(id_estoque)
);

create table Tinta(
	cod_tinta int primary key auto_increment,
    cor varchar(100) not null,
    qntd_estocada decimal(10,2) not null,
    valor decimal(10,2) not null,
    tipo varchar(100) not null,
    id_estoque int,
    constraint fk_Tinta_Estoque_id_estoque foreign key(id_estoque)
		references Estoque(id_estoque)
);

create table Papel(
	cod_papel int primary key auto_increment,
    qntd_estocada decimal(10,2) not null,
    valor decimal(10,2) not null,
    id_estoque int,
    constraint fk_Papel_Estoque_id_estoque foreign key(id_estoque)
		references Estoque(id_estoque)
);

create table Produto(
	id_produto int primary key auto_increment,
    cod_papel int,
    constraint fk_Papel_cod_papel foreign key(cod_papel)
		references Papel(cod_papel),
    cod_tecido int,
    constraint fk_Tecido_cod_tecido foreign key(cod_tecido)
		references Tecido(cod_tecido),
    cod_linha int,
    constraint fk_Linha_cod_linha foreign key(cod_linha)
		references Linha(cod_linha),
    cod_fio int,
    constraint fk_Fio_cod_fio foreign key(cod_fio)
		references Fio(cod_fio),
    cod_tinta int,
    constraint fk_Tinta_cod_tinta foreign key(cod_tinta)
		references Tinta(cod_tinta)
);

create table Compra(
	id_compra int primary key auto_increment,
    qntd int not null,
    valor decimal(10,2) not null,
    id_cliente int,
    constraint fk_Compra_Cliente_id_cliente foreign key(id_cliente)
		references Cliente(id_cliente)
);

create table Pedido(
	id_pedido int primary key auto_increment,
    data_pedido date not null,
    data_entrega date,
    qntd int,
    valor decimal(10,2) not null,
    status_pedido varchar(100),
    descricao varchar(200),
        id_cliente int,
    constraint fk_Pedido_Cliente_id_cliente foreign key(id_cliente)
		references Cliente(id_cliente),
	id_produto int,
    constraint fk_Produto_id_produto foreign key(id_produto)
		references Produto(id_produto)
);

create table Transportadora(
	id_transportadora int primary key auto_increment,
    cnpj char(14) unique not null,
	nome_fantasia varchar(100) not null,
    email varchar(100) unique not null,
    telefone varchar(20) not null,
    razao_social varchar(100) not null,
    endereco varchar(200) not null
);

create table Entrega(
	id_entrega int primary key auto_increment,
	data_entrega date,
    status_entrega varchar(100),
    id_transportadora int,
    constraint fk_Transportadora_id_transportadora foreign key(id_transportadora)
		references Transportadora(id_transportadora),
	id_pedido int,
    constraint fk_Pedido_id_pedido foreign key(id_pedido)
		references Pedido(id_pedido)
); 

insert into Empresa (cnpj, nome_fantasia, email, telefone, razao_social, endereco) values ('12345678000199','Ateliê Criativo','contato@atelie.com','11988887777','Ateliê Criativo LTDA','Rua das Artes, 123 - SP');
insert into Empresa (cnpj, nome_fantasia, email, telefone, razao_social, endereco) values ('98765432000155','Fios & Tecidos','vendas@fioetecidos.com','11999996666','Fios e Tecidos LTDA','Av. Industrial, 456 - SP');
select * from Empresa;
select nome_fantasia, telefone from Empresa where cnpj='12345678000199';

insert into Cliente (nome, cpf_cnpj, endereco, telefone, email) values ('Maria Silva','12345678901','Rua das Flores, 200 - SP','11911112222','maria@email.com');
insert into Cliente (nome, cpf_cnpj, endereco, telefone, email) values ('João Pereira','98765432100','Av. Paulista, 500 - SP','11933334444','joao@email.com');
select * from Cliente;
select nome, telefone from Cliente where nome like 'Maria%';

insert into Estoque (data_atualizacao, local_armazenamento) values ('2025-08-01','Galpão Central - SP');
insert into Estoque (data_atualizacao, local_armazenamento) values ('2025-08-15','Depósito Zona Leste - SP');
select * from Estoque;
select local_armazenamento from Estoque where data_atualizacao > '2025-08-10';

insert into Fornecedor (cpf_cnpj, nome, email, telefone) values ('11223344556677','Tecidos Brasil','fornece@tecidos.com','1133224455');
insert into Fornecedor (cpf_cnpj, nome, email, telefone) values ('99887766554433','Tintas Premium','contato@tintaspremium.com','1144332211');
select * from Fornecedor;
select nome, telefone from Fornecedor where nome like '%Brasil%';

insert into Despesa (descricao, data_despesa, data_vencimento, data_pagamento, tipo, valor, forma_pagamento) values ('Compra de linhas','2025-08-01','2025-08-10','2025-08-08','Material','500.00','PIX');
insert into Despesa (descricao, data_despesa, data_vencimento, tipo, valor, forma_pagamento) values ('Conta de energia','2025-08-05','2025-08-15','Contas Fixas','1200.00','Boleto');
select * from Despesa;
select descricao, valor from Despesa where valor > 600;

insert into Funcionario (nome, cpf, cargo, telefone, email) values ('Ana Costa','12345678901','Costureira','11955556666','ana@empresa.com');
insert into Funcionario (nome, cpf, cargo, telefone, email) values ('Carlos Lima','98765432100','Gerente','11977778888','carlos@empresa.com');
select * from Funcionario;
select nome, cargo from Funcionario where cargo='Gerente';

insert into Linha (cor, qntd_estocada, valor, id_estoque) values ('Vermelho', 100, 5.50, 1);
insert into Linha (cor, qntd_estocada, valor, id_estoque) values ('Azul', 150, 6.00, 2);
select * from Linha;
select cor, valor from Linha where qntd_estocada>120;

insert into Fio (qntd_estocada, valor, id_estoque) values (200, 3.50, 1);
insert into Fio (qntd_estocada, valor, id_estoque) values (300, 4.00, 2);
select * from Fio;
select qntd_estocada, valor from Fio where valor>3.70;

insert into Tecido (cor, tipo, textura, qntd_estocada, valor, id_estoque) values ('Branco','Algodão','Liso', 50, 20.00, 1);
insert into Tecido (cor, tipo, textura, qntd_estocada, valor, id_estoque) values ('Preto','Seda','Riscado', 30, 45.00, 2);
select * from Tecido;
select cor, tipo, valor from Tecido where valor>30;

insert into Tinta (cor, qntd_estocada, valor, tipo, id_estoque) values ('Vermelho', 100, 12.50, 'Acrílica', 1);
insert into Tinta (cor, qntd_estocada, valor, tipo, id_estoque) values ('Azul', 80, 15.00, 'Óleo', 2);
select * from Tinta;
select cor, tipo from Tinta where valor>13;

insert into Papel (qntd_estocada, valor, id_estoque) values (500, 0.50, 1);
insert into Papel (qntd_estocada, valor, id_estoque) values (300, 0.80, 2);
select * from Papel;
select qntd_estocada, valor from Papel where qntd_estocada>400;

insert into Produto (cod_papel, cod_tecido, cod_linha, cod_fio, cod_tinta) values (1,1,1,1,1);
insert into Produto (cod_papel, cod_tecido, cod_linha, cod_fio, cod_tinta) values (2,2,2,2,2);
select * from Produto;
select id_produto, cod_tecido from Produto;

insert into Compra (qntd, valor, id_cliente) values (10, 150.00, 1);
insert into Compra (qntd, valor, id_cliente) values (5, 75.00, 2);
select * from Compra;
select qntd, valor from Compra where valor>100;

insert into Pedido (data_pedido, data_entrega, qntd, valor, status_pedido, descricao, id_cliente, id_produto) values ('2025-08-20','2025-08-25',10,200.00,'Em andamento','Pedido de teste',1,1);
insert into Pedido (data_pedido, data_entrega, qntd, valor, status_pedido, descricao, id_cliente, id_produto) values ('2025-08-21','2025-08-28',5,120.00,'Concluído','Pedido de exemplo',2,2);
select * from Pedido;
select id_pedido, status_pedido from Pedido;

insert into Transportadora (cnpj, nome_fantasia, email, telefone, razao_social, endereco) values ('11223344000199','Trans Express','contato@transexpress.com','11988889999','Trans Express LTDA','Rua do Transporte, 100 - SP');
insert into Transportadora (cnpj, nome_fantasia, email, telefone, razao_social, endereco) values ('99887766000155','Entrega Rápida','contato@entregarapida.com','11977778888','Entrega Rápida LTDA','Av. Logística, 200 - SP');
select * from Transportadora;
select nome_fantasia, telefone from Transportadora;

insert into Entrega (data_entrega, status_entrega, id_transportadora, id_pedido) values ('2025-08-25','Em trânsito',1,1);
insert into Entrega (data_entrega, status_entrega, id_transportadora, id_pedido) values ('2025-08-28','Concluída',2,2);
select * from Entrega;
select data_entrega, status_entrega from Entrega where status_entrega='Em trânsito';


SELECT 
    Pedido.id_pedido, 
    Pedido.data_pedido, 
    Pedido.valor, 
    Pedido.status_pedido, 
    Cliente.nome AS cliente_nome, 
    Cliente.telefone AS cliente_telefone
FROM 
    Pedido
INNER JOIN 
    Cliente ON Pedido.id_cliente = Cliente.id_cliente;


SELECT 
    Produto.id_produto,
    Produto.cod_papel,
    Produto.cod_linha,
    Produto.cod_fio,
    Tecido.cor AS tecido_cor, 
    Tecido.tipo AS tecido_tipo
FROM 
    Produto
INNER JOIN 
    Tecido ON Produto.cod_tecido = Tecido.cod_tecido;


SELECT 
    Pedido.id_pedido,
    Pedido.data_pedido,
    Pedido.data_entrega,
    Pedido.valor,
    Pedido.status_pedido,
    Cliente.nome AS cliente_nome,
    Cliente.telefone AS cliente_telefone,
    Produto.id_produto,
    Tecido.cor AS tecido_cor
FROM 
    Pedido
INNER JOIN 
    Cliente ON Pedido.id_cliente = Cliente.id_cliente
INNER JOIN 
    Produto ON Pedido.id_produto = Produto.id_produto
INNER JOIN 
    Tecido ON Produto.cod_tecido = Tecido.cod_tecido;


SELECT 
    Despesa.id_despesa,
    Despesa.descricao,
    Despesa.valor AS despesa_valor,
    Fornecedor.nome AS fornecedor_nome,
    Fornecedor.telefone AS fornecedor_telefone
FROM 
    Despesa
INNER JOIN 
    Fornecedor ON Despesa.id_despesa = Fornecedor.id_fornecedor;


CREATE VIEW RelatorioPedidos AS 
SELECT 
    Pedido.id_pedido,
    Pedido.data_pedido,
    Pedido.data_entrega,
    Pedido.valor,
    Pedido.status_pedido,
    Cliente.nome AS cliente_nome,
    Cliente.telefone AS cliente_telefone,
    Produto.id_produto,
    Tecido.cor AS tecido_cor
FROM 
    Pedido
INNER JOIN 
    Cliente ON Pedido.id_cliente = Cliente.id_cliente
INNER JOIN 
    Produto ON Pedido.id_produto = Produto.id_produto
INNER JOIN 
    Tecido ON Produto.cod_tecido = Tecido.cod_tecido;

SELECT * FROM RelatorioPedidos;


SELECT 
    Funcionario.id_funcionario,
    Funcionario.nome AS funcionario_nome,
    Funcionario.cargo,
    Funcionario.telefone,
    Departamento.nome AS departamento_nome
FROM 
    Funcionario
INNER JOIN 
    Departamento ON Funcionario.cod_depto = Departamento.cod_depto;

CREATE USER 'alunos'@'localhost' IDENTIFIED BY 'etec';
GRANT ALL PRIVILEGES ON MiniTCC_PNTJ.* TO 'alunos'@'localhost';
FLUSH PRIVILEGES;
SHOW GRANTS FOR 'alunos'@'localhost';



