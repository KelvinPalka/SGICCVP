create database minitcc_pntj;
use minitcc_pntj;
create user 'alunos'@'localhost' identified by 'etec';
grant all privileges on minitcc_pntj.* to 'alunos'@'localhost';
flush privileges;

create table empresa (
    id_empresa int primary key auto_increment,
    cnpj char(14) unique not null,
    nome_fantasia varchar(100) not null,
    email varchar(100) unique not null,
    telefone varchar(20) not null,
    razao_social varchar(100) not null unique,
    endereco varchar(200) not null
);

create table cliente (
    id_cliente int primary key auto_increment,
    nome varchar(100) not null,
    cpf_cnpj char(14) unique not null,
    endereco varchar(200) not null,
    telefone varchar(20) not null,
    email varchar(100) unique not null
);

create table estoque (
    id_estoque int primary key auto_increment,
    data_atualizacao date,
    local_armazenamento varchar(200) not null
);

create table fornecedor (
    id_fornecedor int primary key auto_increment,
    cpf_cnpj char(14) unique not null,
    nome varchar(100) not null,
    email varchar(100) unique not null,
    telefone varchar(20) not null
);

create table despesa (
    id_despesa int primary key auto_increment,
    descricao varchar(250),
    data_despesa date not null,
    data_vencimento date not null,
    data_pagamento date,
    tipo varchar(100) not null,
    valor decimal(10,2) not null,
    forma_pagamento varchar(50)
);

create table funcionario (
    id_funcionario INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(100) NOT NULL,
    cpf CHAR(14) UNIQUE NOT NULL,
    cargo VARCHAR(100) NOT NULL,
    telefone VARCHAR(20) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    IdEmpresa INT NOT NULL,
    Departamento VARCHAR(100),
    CONSTRAINT FK_Funcionarios_Empresa
        FOREIGN KEY (IdEmpresa) REFERENCES empresa(id_empresa)
);

create table linha (
    cod_linha int primary key auto_increment,
    cor varchar(100) not null,
    qntd_estocada decimal(10,2) not null,
    valor decimal(10,2) not null,
    id_estoque int,
    constraint fk_linha_estoque foreign key (id_estoque)
        references estoque(id_estoque)
);

create table fio (
    cod_fio int primary key auto_increment,
    qntd_estocada decimal(10,2) not null,
    valor decimal(10,2) not null,
    id_estoque int,
    constraint fk_fio_estoque foreign key (id_estoque)
        references estoque(id_estoque)
);

create table tecido (
    cod_tecido int primary key auto_increment,
    cor varchar(100) not null,
    tipo varchar(100) not null,
    textura varchar(100) not null,
    qntd_estocada decimal(10,2) not null,
    valor decimal(10,2) not null,
    id_estoque int,
    constraint fk_tecido_estoque foreign key (id_estoque)
        references estoque(id_estoque)
);

create table tinta (
    cod_tinta int primary key auto_increment,
    cor varchar(100) not null,
    qntd_estocada decimal(10,2) not null,
    valor decimal(10,2) not null,
    tipo varchar(100) not null,
    id_estoque int,
    constraint fk_tinta_estoque foreign key (id_estoque)
        references estoque(id_estoque)
);

create table papel (
    cod_papel int primary key auto_increment,
    qntd_estocada decimal(10,2) not null,
    valor decimal(10,2) not null,
    id_estoque int,
    constraint fk_papel_estoque foreign key (id_estoque)
        references estoque(id_estoque)
);

create table produto (
    id_produto int primary key auto_increment,
    cod_papel int,
    cod_tecido int,
    cod_linha int,
    cod_fio int,
    cod_tinta int,
    constraint fk_prod_papel foreign key (cod_papel) references papel(cod_papel),
    constraint fk_prod_tecido foreign key (cod_tecido) references tecido(cod_tecido),
    constraint fk_prod_linha foreign key (cod_linha) references linha(cod_linha),
    constraint fk_prod_fio foreign key (cod_fio) references fio(cod_fio),
    constraint fk_prod_tinta foreign key (cod_tinta) references tinta(cod_tinta)
);

create table compra (
    id_compra int primary key auto_increment,
    qntd int not null,
    valor decimal(10,2) not null,
    id_cliente int,
    constraint fk_compra_cliente foreign key (id_cliente)
        references cliente(id_cliente)
);

create table pedido (
    id_pedido int primary key auto_increment,
    data_pedido date not null,
    data_entrega date,
    qntd int,
    valor decimal(10,2) not null,
    status_pedido varchar(100),
    descricao varchar(200),
    id_cliente int,
    id_produto int,
    constraint fk_pedido_cliente foreign key (id_cliente) references cliente(id_cliente),
    constraint fk_pedido_produto foreign key (id_produto) references produto(id_produto)
);

create table transportadora (
    id_transportadora int primary key auto_increment,
    cnpj char(14) unique not null,
    nome_fantasia varchar(100) not null,
    email varchar(100) unique not null,
    telefone varchar(20) not null,
    razao_social varchar(100) not null,
    endereco varchar(200) not null
);

create table entrega (
    id_entrega int primary key auto_increment,
    data_entrega date,
    status_entrega varchar(100),
    id_transportadora int,
    id_pedido int,
    constraint fk_entrega_transportadora foreign key (id_transportadora)
        references transportadora(id_transportadora),
    constraint fk_entrega_pedido foreign key (id_pedido)
        references pedido(id_pedido)
);

create table usuario (
    id_usuario int primary key auto_increment,
    id_empresa int not null,
    nome varchar(100) not null,
    email varchar(100) unique not null,
    senha_hash varchar(255) not null,
    salt varchar(64) not null,
    tipo_usuario varchar(50) not null,
    constraint fk_usuario_empresa foreign key (id_empresa)
        references empresa(id_empresa)
);

insert into empresa (cnpj, nome_fantasia, email, telefone, razao_social, endereco) values
('12345678000199','atelie criativo','contato@atelie.com','11988887777','atelie criativo ltda','rua das artes, 123 - sp'),
('98765432000155','fios & tecidos','vendas@fioetecidos.com','11999996666','fios e tecidos ltda','av. industrial, 456 - sp');

insert into cliente (nome, cpf_cnpj, endereco, telefone, email) values
('maria silva','12345678901','rua das flores, 200 - sp','11911112222','maria@email.com'),
('joao pereira','98765432100','av. paulista, 500 - sp','11933334444','joao@email.com');

insert into estoque (data_atualizacao, local_armazenamento) values
('2025-08-01','galpão central - sp'),
('2025-08-15','depósito zona leste - sp');

insert into fornecedor (cpf_cnpj, nome, email, telefone) values
('11223344556677','tecidos brasil','fornece@tecidos.com','1133224455'),
('99887766554433','tintas premium','contato@tintaspremium.com','1144332211');

insert into despesa(descricao, data_despesa, data_vencimento, data_pagamento, tipo, valor, forma_pagamento) values
('compra de linhas','2025-08-01','2025-08-10','2025-08-08','material','500.00','pix'),
('conta de energia','2025-08-05','2025-08-15',null,'contas fixas','1200.00','boleto');

INSERT INTO funcionario (nome, cpf, cargo, telefone, email, IdEmpresa, Departamento) VALUES
('ana costa','12345678901','costureira','11955556666','ana@empresa.com', 1, 'Costura'),
('carlos lima','98765432100','gerente','11977778888','carlos@empresa.com', 1, 'Gestão');

insert into linha (cor, qntd_estocada, valor, id_estoque) values
('vermelho',100,5.50,1),
('azul',150,6.00,2);

insert into fio (qntd_estocada, valor, id_estoque) values
(200,3.50,1),
(300,4.00,2);

insert into tecido (cor, tipo, textura, qntd_estocada, valor, id_estoque) values
('branco','algodao','liso',50,20.00,1),
('preto','seda','riscado',30,45.00,2);

insert into tinta (cor, qntd_estocada, valor, tipo, id_estoque) values
('vermelho',100,12.50,'acrilica',1),
('azul',80,15.00,'oleo',2);

insert into papel (qntd_estocada, valor, id_estoque) values
(500,0.50,1),
(300,0.80,2);

insert into produto (cod_papel, cod_tecido, cod_linha, cod_fio, cod_tinta) values
(1,1,1,1,1),
(2,2,2,2,2);

insert into compra (qntd, valor, id_cliente) values
(10,150.00,1),
(5,75.00,2);

insert into pedido (data_pedido, data_entrega, qntd, valor, status_pedido, descricao, id_cliente, id_produto) values
('2025-08-20','2025-08-25',10,200.00,'em andamento','pedido de teste',1,1),
('2025-08-21','2025-08-28',5,120.00,'concluido','pedido de exemplo',2,2);

insert into transportadora (cnpj, nome_fantasia, email, telefone, razao_social, endereco) values
('11223344000199','trans express','contato@transexpress.com','11988889999','trans express ltda','rua do transporte, 100 - sp'),
('99887766000155','entrega rapida','contato@entregarapida.com','11977778888','entrega rapida ltda','av. logistica, 200 - sp');

insert into entrega (data_entrega, status_entrega, id_transportadora, id_pedido) values
('2025-08-25','em transito',1,1),
('2025-08-28','concluida',2,2);

create view relatorio_pedidos as
select 
    pedido.id_pedido,
    pedido.data_pedido,
    pedido.data_entrega,
    pedido.valor,
    pedido.status_pedido,
    cliente.nome as cliente_nome,
    cliente.telefone as cliente_telefone,
    produto.id_produto,
    tecido.cor as tecido_cor
from pedido
inner join cliente on pedido.id_cliente = cliente.id_cliente
inner join produto on pedido.id_produto = produto.id_produto
inner join tecido on produto.cod_tecido = tecido.cod_tecido;

select * from funcionario;
select * from usuario;
