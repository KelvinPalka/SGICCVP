drop database minitcc_pntj;
create database minitcc_pntj;
use minitcc_pntj;

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

INSERT IGNORE INTO empresa
(cnpj, nome_fantasia, email, telefone, razao_social, endereco)
VALUES
('14345478000192','atelie criativo','contato@atelie.com','11988887777','atelie criativo ltda','rua das artes, 532 - sp'),
('98765432000155','fios & tecidos','vendas@fioetecidos.com','11999996666','fios e tecidos ltda','av. industrial, 897 - sp');

INSERT INTO empresa
(cnpj, nome_fantasia, email, telefone, razao_social, endereco)
VALUES
('22133444000177','arte moderna','contato@artemoderna.com','11981112222','arte moderna ltda','rua criativa, 101 - sp'),
('33445566000188','cores & formas','vendas@coresformas.com','11982223333','cores e formas ltda','av. design, 450 - sp'),
('44556677000199',
 'Pri &  Rafa Camisetas',
 'prierafa@contato.com',
 '11987654321',
 'Pri e Rafa LTDA',
 'R. Cap. Lorival Mey, 750 - Remanso Campineiro, Hortolândia - SP, 13184-470');
INSERT INTO funcionario (nome, cpf, cargo, telefone, email, IdEmpresa, Departamento) VALUES
-- Direção e Administrativo
('Mariana Alves',           '20000000001', 'Diretora de Operações',          '11910000001', 'mariana.alves@confecpersonalizada.com.br',        5, 'Diretoria'),
('Ricardo Pereira',         '20000000002', 'Gerente Administrativo',         '11910000002', 'ricardo.pereira@confecpersonalizada.com.br',      5, 'Administrativo'),
('Patrícia Rocha',          '20000000003', 'Assistente Administrativo',      '11910000003', 'patricia.rocha@confecpersonalizada.com.br',       5, 'Administrativo'),
('Lucas Carvalho',          '20000000004', 'Analista Financeiro',            '11910000004', 'lucas.carvalho@confecpersonalizada.com.br',       5, 'Financeiro'),
('Fernanda Nunes',          '20000000005', 'Auxiliar Financeiro',            '11910000005', 'fernanda.nunes@confecpersonalizada.com.br',       5, 'Financeiro'),

-- RH
('Juliana Costa',           '20000000006', 'Analista de RH',                 '11910000006', 'juliana.costa@confecpersonalizada.com.br',        5, 'Recursos Humanos'),
('Tiago Moreira',           '20000000007', 'Assistente de RH',               '11910000007', 'tiago.moreira@confecpersonalizada.com.br',        5, 'Recursos Humanos'),

-- Criação e Design
('Ana Paula Mendes',        '20000000008', 'Coordenadora de Criação',        '11910000008', 'ana.mendes@confecpersonalizada.com.br',           5, 'Criação'),
('Bruno Silva',             '20000000009', 'Designer de Estampas',           '11910000009', 'bruno.silva@confecpersonalizada.com.br',          5, 'Criação'),
('Carla Oliveira',          '20000000010', 'Designer de Moda',               '11910000010', 'carla.oliveira@confecpersonalizada.com.br',       5, 'Criação'),
('Eduardo Lima',            '20000000011', 'Ilustrador',                     '11910000011', 'eduardo.lima@confecpersonalizada.com.br',         5, 'Criação'),
('Gabriela Santos',         '20000000012', 'Designer Gráfico',               '11910000012', 'gabriela.santos@confecpersonalizada.com.br',      5, 'Criação'),

-- Modelagem e Corte
('Helena Martins',          '20000000013', 'Modelista',                      '11910000013', 'helena.martins@confecpersonalizada.com.br',       5, 'Modelagem'),
('Rafael Souza',            '20000000014', 'Auxiliar de Modelagem',          '11910000014', 'rafael.souza@confecpersonalizada.com.br',         5, 'Modelagem'),
('Isabela Correia',         '20000000015', 'Encarregada de Corte',           '11910000015', 'isabela.correia@confecpersonalizada.com.br',      5, 'Corte'),
('João Pedro Araújo',       '20000000016', 'Operador de Corte',              '11910000016', 'joao.araujo@confecpersonalizada.com.br',          5, 'Corte'),
('Camila Freitas',          '20000000017', 'Auxiliar de Corte',              '11910000017', 'camila.freitas@confecpersonalizada.com.br',       5, 'Corte'),

-- Costura
('Luciana Barros',          '20000000018', 'Supervisora de Costura',         '11910000018', 'luciana.barros@confecpersonalizada.com.br',       5, 'Costura'),
('Marta Ribeiro',           '20000000019', 'Costureira',                     '11910000019', 'marta.ribeiro@confecpersonalizada.com.br',        5, 'Costura'),
('Paulo Henrique Dias',     '20000000020', 'Costureiro',                     '11910000020', 'paulo.dias@confecpersonalizada.com.br',           5, 'Costura'),
('Renata Gomes',            '20000000021', 'Costureira',                     '11910000021', 'renata.gomes@confecpersonalizada.com.br',         5, 'Costura'),
('Sandra Figueiredo',       '20000000022', 'Costureira',                     '11910000022', 'sandra.figueiredo@confecpersonalizada.com.br',    5, 'Costura'),
('Cláudia Teixeira',        '20000000023', 'Costureira Overloquista',        '11910000023', 'claudia.teixeira@confecpersonalizada.com.br',     5, 'Costura'),
('Patrícia Lima',           '20000000024', 'Costureira Reta',                '11910000024', 'patricia.lima@confecpersonalizada.com.br',        5, 'Costura'),
('Diego Fernandes',         '20000000025', 'Auxiliar de Costura',            '11910000025', 'diego.fernandes@confecpersonalizada.com.br',      5, 'Costura'),

-- Estamparia e Bordado
('Felipe Nascimento',       '20000000026', 'Coordenador de Estamparia',      '11910000026', 'felipe.nascimento@confecpersonalizada.com.br',    5, 'Estamparia'),
('Natália Prado',           '20000000027', 'Operadora de Silk Screen',       '11910000027', 'natalia.prado@confecpersonalizada.com.br',        5, 'Estamparia'),
('Rodrigo Almeida',         '20000000028', 'Operador de Sublimação',         '11910000028', 'rodrigo.almeida@confecpersonalizada.com.br',      5, 'Estamparia'),
('Beatriz Ferreira',        '20000000029', 'Auxiliar de Estamparia',         '11910000029', 'beatriz.ferreira@confecpersonalizada.com.br',     5, 'Estamparia'),
('Caroline Campos',         '20000000030', 'Bordadeira',                     '11910000030', 'caroline.campos@confecpersonalizada.com.br',      5, 'Bordado'),
('Guilherme Torres',        '20000000031', 'Operador de Máquina de Bordar',  '11910000031', 'guilherme.torres@confecpersonalizada.com.br',     5, 'Bordado'),
('Tatiane Souza',           '20000000032', 'Auxiliar de Bordado',            '11910000032', 'tatiane.souza@confecpersonalizada.com.br',        5, 'Bordado'),

-- Acabamento e Controle de Qualidade
('Rafaela Cardoso',         '20000000033', 'Encarregada de Acabamento',      '11910000033', 'rafaela.cardoso@confecpersonalizada.com.br',      5, 'Acabamento'),
('Sérgio Lopes',            '20000000034', 'Auxiliar de Acabamento',         '11910000034', 'sergio.lopes@confecpersonalizada.com.br',         5, 'Acabamento'),
('Monique Araújo',          '20000000035', 'Passadeira',                     '11910000035', 'monique.araujo@confecpersonalizada.com.br',       5, 'Acabamento'),
('Vanessa Mota',            '20000000036', 'Conferente de Qualidade',        '11910000036', 'vanessa.mota@confecpersonalizada.com.br',         5, 'Qualidade'),
('Hugo Ribeiro',            '20000000037', 'Inspetor de Qualidade',          '11910000037', 'hugo.ribeiro@confecpersonalizada.com.br',         5, 'Qualidade'),

-- Comercial e Atendimento
('Daniela Brito',           '20000000038', 'Coordenadora Comercial',         '11910000038', 'daniela.brito@confecpersonalizada.com.br',        5, 'Comercial'),
('Marcelo Tavares',         '20000000039', 'Vendedor Interno',               '11910000039', 'marcelo.tavares@confecpersonalizada.com.br',      5, 'Comercial'),
('Larissa Gomes',           '20000000040', 'Vendedora Externa',              '11910000040', 'larissa.gomes@confecpersonalizada.com.br',        5, 'Comercial'),
('André Santos',            '20000000041', 'Atendente de Loja',              '11910000041', 'andre.santos@confecpersonalizada.com.br',         5, 'Varejo'),
('Priscila Duarte',         '20000000042', 'Atendimento ao Cliente',         '11910000042', 'priscila.duarte@confecpersonalizada.com.br',      5, 'SAC'),

-- Marketing e E-commerce
('Caroline Ribeiro',        '20000000043', 'Analista de Marketing',          '11910000043', 'caroline.ribeiro@confecpersonalizada.com.br',     5, 'Marketing'),
('Henrique Costa',          '20000000044', 'Social Media',                   '11910000044', 'henrique.costa@confecpersonalizada.com.br',       5, 'Marketing'),
('Bianca Silva',            '20000000045', 'Fotógrafa de Produtos',          '11910000045', 'bianca.silva@confecpersonalizada.com.br',         5, 'Marketing'),
('Jonas Pereira',           '20000000046', 'Gestor de E-commerce',           '11910000046', 'jonas.pereira@confecpersonalizada.com.br',        5, 'E-commerce'),
('Marina Lopes',            '20000000047', 'Assistente de E-commerce',       '11910000047', 'marina.lopes@confecpersonalizada.com.br',         5, 'E-commerce'),

-- Logística e Expedição
('Rodrigo Silva',           '20000000048', 'Coordenador de Logística',       '11910000048', 'rodrigo.silva.log@confecpersonalizada.com.br',    5, 'Logística'),
('Felipe Santos',           '20000000049', 'Auxiliar de Expedição',          '11910000049', 'felipe.santos@confecpersonalizada.com.br',        5, 'Expedição'),
('Simone Rocha',            '20000000050', 'Conferente de Estoque',          '11910000050', 'simone.rocha@confecpersonalizada.com.br',         5, 'Almoxarifado');

select * from funcionario; 

INSERT INTO cliente
(nome, cpf_cnpj, endereco, telefone, email)
VALUES
('lucas rocha','32165498700','rua horizonte, 80 - sp','11991110000','lucas@email.com'),
('fernanda alves','65498732100','av. central, 1200 - sp','11992223344','fernanda@email.com');

INSERT INTO estoque
(data_atualizacao, local_armazenamento)
VALUES
('2025-09-01','galpão norte - sp'),
('2025-09-10','depósito sul - sp');

INSERT INTO fornecedor
(cpf_cnpj, nome, email, telefone)
VALUES
('55443322110099','malhas premium','contato@malhaspremium.com','1133445566'),
('66778899001122','cores vivas','atendimento@coresvivas.com','1144556677');

INSERT INTO despesa
(descricao, data_despesa, data_vencimento, data_pagamento, tipo, valor, forma_pagamento)
VALUES
('compra de tecidos','2025-09-02','2025-09-12','2025-09-11','material',850.00,'pix'),
('internet empresarial','2025-09-05','2025-09-20',NULL,'servicos',300.00,'boleto');

INSERT INTO funcionario
(nome, cpf, cargo, telefone, email, IdEmpresa, Departamento)
VALUES
('paula nunes','45612378900','designer','11993334444','paula@artemoderna.com',1,'Design'),
('roberto santos','78945612300','supervisor','11994445555','roberto@artemoderna.com',1,'Administração');

INSERT INTO linha
(cor, qntd_estocada, valor, id_estoque)
VALUES
('verde',120,6.20,1),
('amarelo',180,5.80,2);

INSERT INTO fio
(qntd_estocada, valor, id_estoque)
VALUES
(250,3.90,1),
(400,4.30,2);

INSERT INTO tecido
(cor, tipo, textura, qntd_estocada, valor, id_estoque)
VALUES
('cinza','linho','fosco',40,38.00,1),
('azul marinho','jeans','áspero',60,42.00,2);

INSERT INTO tinta
(cor, qntd_estocada, valor, tipo, id_estoque)
VALUES
('preto',90,14.50,'esmalte',1),
('branco',110,13.00,'latex',2);

INSERT INTO papel
(qntd_estocada, valor, id_estoque)
VALUES
(600,0.65,1),
(420,0.95,2);

INSERT INTO produto
(cod_papel, cod_tecido, cod_linha, cod_fio, cod_tinta)
VALUES
(1,1,1,1,1),
(2,2,2,2,2);

INSERT INTO compra
(qntd, valor, id_cliente)
VALUES
(12,240.00,1),
(7,161.00,2);

INSERT INTO pedido
(data_pedido, data_entrega, qntd, valor, status_pedido, descricao, id_cliente, id_produto)
VALUES
('2025-09-15','2025-09-22',8,320.00,'em andamento','pedido institucional',1,1),
('2025-09-16','2025-09-25',4,210.00,'finalizado','pedido personalizado',2,2);

INSERT INTO transportadora
(cnpj, nome_fantasia, email, telefone, razao_social, endereco)
VALUES
('44556677000199','logistica nova','contato@logisticanova.com','11995556666','logistica nova ltda','rua cargas, 900 - sp'),
('55667788000144','rota expressa','suporte@rotaexpressa.com','11996667777','rota expressa ltda','av. transporte, 1500 - sp');

INSERT INTO entrega
(data_entrega, status_entrega, id_transportadora, id_pedido)
VALUES
('2025-09-22','em rota',1,1),
('2025-09-25','entregue',2,2);

-- View 1: Relatório de Vendas por Cliente
DROP VIEW IF EXISTS relatorio_vendas_por_cliente;
CREATE VIEW relatorio_vendas_por_cliente AS
SELECT
    c.id_cliente,
    c.nome AS cliente_nome,
    COUNT(p.id_pedido) AS total_pedidos,
    SUM(p.valor) AS total_valor_pedidos,
    AVG(p.valor) AS media_valor_pedido
FROM cliente c
LEFT JOIN pedido p ON c.id_cliente = p.id_cliente
GROUP BY c.id_cliente, c.nome;

-- View 2: Relatório Agrupado de Pedidos (por status)
DROP VIEW IF EXISTS relatorio_pedidos_agrupado;
CREATE VIEW relatorio_pedidos_agrupado AS
SELECT
    status_pedido,
    COUNT(id_pedido) AS quantidade_pedidos,
    SUM(valor) AS soma_valor,
    AVG(valor) AS media_valor
FROM pedido
GROUP BY status_pedido;

-- ************************************************************
-- A consulta a seguir estava aninhada incorretamente.
-- Se você quiser esta como uma terceira VIEW, aqui está ela:
-- ************************************************************

-- View 3: Detalhes de Pedidos "Em Andamento"
-- (Nome sugerido, pois o original estava confuso)
DROP VIEW IF EXISTS relatorio_pedidos_em_andamento_detalhe;
CREATE VIEW relatorio_pedidos_em_andamento_detalhe AS
SELECT
    p.id_pedido,
    p.data_pedido,
    p.data_entrega,
    p.valor,
    p.status_pedido,
    c.nome AS cliente_nome,
    pr.id_produto,
    t.cor AS tecido_cor,
    pap.qntd_estocada AS papel_qntd,
    lin.qntd_estocada AS linha_qntd,
    fi.qntd_estocada AS fio_qntd
FROM pedido p
INNER JOIN cliente c ON p.id_cliente = c.id_cliente
INNER JOIN produto pr ON p.id_produto = pr.id_produto
LEFT JOIN tecido t ON pr.cod_tecido = t.cod_tecido
LEFT JOIN papel pap ON pr.cod_papel = pap.cod_papel
LEFT JOIN linha lin ON pr.cod_linha = lin.cod_linha
LEFT JOIN fio fi ON pr.cod_fio = fi.cod_fio
WHERE p.status_pedido = 'em andamento';

DROP PROCEDURE IF EXISTS sp_total_vendas_por_cliente;
DELIMITER $$
CREATE PROCEDURE sp_total_vendas_por_cliente(IN p_id_cliente INT)
BEGIN
    SELECT
        c.id_cliente,
        c.nome AS cliente_nome,
        COUNT(p.id_pedido) AS total_pedidos,
        COALESCE(SUM(p.valor),0) AS total_valor,
        COALESCE(AVG(p.valor),0) AS media_valor
    FROM cliente c
    LEFT JOIN pedido p ON c.id_cliente = p.id_cliente
    WHERE c.id_cliente = p_id_cliente
    GROUP BY c.id_cliente, c.nome;
END$$
DELIMITER ;
CALL sp_total_vendas_por_cliente(1);

DROP PROCEDURE IF EXISTS sp_criar_pedido
DELIMITER $$
CREATE PROCEDURE sp_criar_pedido(
    IN p_data_pedido DATE,
    IN p_data_entrega DATE,
    IN p_qntd INT,
    IN p_valor DECIMAL(10,2),
    IN p_status VARCHAR(100),
    IN p_descricao VARCHAR(200),
    IN p_id_cliente INT,
    IN p_id_produto INT
)
BEGIN
    INSERT INTO pedido (data_pedido, data_entrega, qntd, valor, status_pedido, descricao, id_cliente, id_produto)
    VALUES (p_data_pedido, p_data_entrega, p_qntd, p_valor, p_status, p_descricao, p_id_cliente, p_id_produto);
    SELECT LAST_INSERT_ID() AS novo_id_pedido;
END$$
DELIMITER ;
CALL sp_criar_pedido('2025-10-01','2025-10-07',2,180.00,'em processamento','pedido via procedure',1,1);

DROP PROCEDURE IF EXISTS sp_atualizar_estoque_produto;
-- Atenção: Coloque um espaço ou nova linha antes e depois de DELIMITER

DELIMITER $$ 

CREATE PROCEDURE sp_atualizar_estoque_produto(IN p_id_produto INT, IN p_qntd INT)
BEGIN
    DECLARE v_cod_papel INT;
    DECLARE v_cod_tecido INT;
    DECLARE v_cod_linha INT;
    DECLARE v_cod_fio INT;
    DECLARE v_cod_tinta INT;

    SELECT cod_papel, cod_tecido, cod_linha, cod_fio, cod_tinta
    INTO v_cod_papel, v_cod_tecido, v_cod_linha, v_cod_fio, v_cod_tinta
    FROM produto
    WHERE id_produto = p_id_produto
    LIMIT 1;

    IF v_cod_papel IS NOT NULL THEN
        UPDATE papel SET qntd_estocada = qntd_estocada - p_qntd WHERE cod_papel = v_cod_papel;
    END IF;

    IF v_cod_tecido IS NOT NULL THEN
        UPDATE tecido SET qntd_estocada = qntd_estocada - p_qntd WHERE cod_tecido = v_cod_tecido;
    END IF;

    IF v_cod_linha IS NOT NULL THEN
        UPDATE linha SET qntd_estocada = qntd_estocada - p_qntd WHERE cod_linha = v_cod_linha;
    END IF;

    IF v_cod_fio IS NOT NULL THEN
        UPDATE fio SET qntd_estocada = qntd_estocada - p_qntd WHERE cod_fio = v_cod_fio;
    END IF;

    IF v_cod_tinta IS NOT NULL THEN
        UPDATE tinta SET qntd_estocada = qntd_estocada - p_qntd WHERE cod_tinta = v_cod_tinta;
    END IF;
END$$

-- Atenção: Resete o delimitador imediatamente após o $$
DELIMITER ;

CALL sp_atualizar_estoque_produto(1,2);

select * from empresa;