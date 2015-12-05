DROP TABLE tb_user_access_level
DROP TABLE tb_configuration_limitation
DROP TABLE tb_access_level_permission
DROP TABLE tb_access_level
DROP TABLE tb_action
DROP TABLE tb_sector
DROP TABLE tb_limitation_type
DROP TABLE tb_configuration
DROP TABLE tb_permission
DROP TABLE tb_permission_group
DROP TABLE tb_register
DROP TABLE tb_user
DROP TABLE tb_company

CREATE TABLE tb_permission_group (
  id_permission_group INTEGER  NOT NULL,
  ds_permission_group VARCHAR(100)
  PRIMARY KEY(id_permission_group)
);

CREATE TABLE tb_permission (
  id_permission INTEGER  NOT NULL,
  tb_permission_group_id_permission_group INTEGER NOT NULL,
  ds_permission VARCHAR(100),
  ds_route VARCHAR(50),
  vl_order INT NOT NULL,
  PRIMARY KEY(id_permission),
  FOREIGN KEY(tb_permission_group_id_permission_group)
    REFERENCES tb_permission_group(id_permission_group)
);

CREATE TABLE tb_sector (
  id_sector INTEGER IDENTITY,
  ds_sector VARCHAR(100),
  PRIMARY KEY(id_sector)
);

CREATE TABLE tb_limitation_type (
  id_limitation_type INTEGER NOT NULL,
  ds_limitation_type VARCHAR(100),
  PRIMARY KEY(id_limitation_type)
);

CREATE TABLE tb_access_level (
  id_access_level INTEGER IDENTITY,
  ds_access_level VARCHAR(100),
  PRIMARY KEY(id_access_level)
);

CREATE TABLE tb_action (
  id_action INTEGER  NOT NULL,
  ds_action VARCHAR(100),
  PRIMARY KEY(id_action)
);

CREATE TABLE tb_configuration_limitation (
  id_configuration_limitation INTEGER IDENTITY,
  tb_limitation_type_id_limitation_type INTEGER  NOT NULL,
  vl_latitude float,
  vl_longitude float,
  ds_host VARCHAR(100),
  PRIMARY KEY(id_configuration_limitation),
  FOREIGN KEY(tb_limitation_type_id_limitation_type)
    REFERENCES tb_limitation_type(id_limitation_type)
);

CREATE TABLE tb_access_level_permission (
  tb_access_level_id_access_level INTEGER  NOT NULL,
  tb_permission_id_permission INTEGER  NOT NULL,
  PRIMARY KEY(tb_access_level_id_access_level, tb_permission_id_permission),
  FOREIGN KEY(tb_access_level_id_access_level)
    REFERENCES tb_access_level(id_access_level),
  FOREIGN KEY(tb_permission_id_permission)
    REFERENCES tb_permission(id_permission)
);

CREATE TABLE tb_configuration (
  id_configuration INTEGER identity,
  tb_configuration_limitation_id_configuration_limitation INTEGER  NOT NULL,
  PRIMARY KEY(id_configuration),
  FOREIGN KEY(tb_configuration_limitation_id_configuration_limitation)
    REFERENCES tb_configuration_limitation(id_configuration_limitation)
);

CREATE TABLE tb_company (
	id_company INTEGER IDENTITY,
	ds_token VARCHAR(10) NOT NULL,
	ds_name VARCHAR(100),
	PRIMARY KEY(id_company)
)

CREATE TABLE tb_user (
  id_user INTEGER identity,
  tb_configuration_id_configuration INTEGER  NOT NULL,
  tb_sector_id_sector INTEGER  NOT NULL,
  id_identification INTEGER  NULL,
  ds_email VARCHAR(100),
  ds_password VARCHAR(100),
  ds_name VARCHAR(100),
  ds_lastname VARCHAR(100),
  id_user_superior INTEGER,
  tb_company_id_company INTEGER NULL,
  ds_photo VARCHAR(250),
  PRIMARY KEY(id_user),
  FOREIGN KEY(tb_sector_id_sector)
    REFERENCES tb_sector(id_sector),
  FOREIGN KEY(tb_configuration_id_configuration)
    REFERENCES tb_configuration(id_configuration),
  FOREIGN KEY(tb_company_id_company)
    REFERENCES tb_company(id_company) 
);

CREATE TABLE tb_user_access_level (
  tb_user_id_user INTEGER  NOT NULL,
  tb_access_level_id_access_level INTEGER  NOT NULL,
  PRIMARY KEY(tb_user_id_user, tb_access_level_id_access_level),
  FOREIGN KEY(tb_user_id_user)
    REFERENCES tb_user(id_user),
  FOREIGN KEY(tb_access_level_id_access_level)
    REFERENCES tb_access_level(id_access_level)
);

CREATE TABLE tb_register (
  id_register INTEGER  identity,
  id_last_register INTEGER,
  tb_action_id_action INTEGER  NOT NULL,
  tb_user_id_user INTEGER  NOT NULL,
  tb_limitation_type_id_limitation_type INTEGER  NOT NULL,
  dt_register DATETIME,
  vl_latitude float ,
  vl_longitude float ,
  ds_host VARCHAR (100),
  PRIMARY KEY(id_register),
  FOREIGN KEY(tb_action_id_action)
    REFERENCES tb_action(id_action),
  FOREIGN KEY(tb_user_id_user)
    REFERENCES tb_user(id_user),
  FOREIGN KEY(tb_limitation_type_id_limitation_type)
    REFERENCES tb_limitation_type(id_limitation_type)
);

--DADOS INICIAIS
INSERT INTO tb_access_level VALUES ('Administrador')
INSERT INTO tb_access_level VALUES ('Usuário')

INSERT INTO [tb_action] VALUES (1, 'Entrada')
INSERT INTO [tb_action] VALUES (2, 'Pausa')
INSERT INTO [tb_action] VALUES (3, 'Volta da Pausa')
INSERT INTO [tb_action] VALUES (4, 'Saída')

INSERT INTO tb_limitation_type VALUES (1, 'Nenhuma')
INSERT INTO tb_limitation_type VALUES (2, 'Gps')
INSERT INTO tb_limitation_type VALUES (3, 'Wifi')

INSERT INTO tb_sector VALUES ('Administrador')
INSERT INTO tb_sector VALUES ('Recursos Humanos')
INSERT INTO tb_sector VALUES ('Produtos')
INSERT INTO tb_sector VALUES ('Projetos')

INSERT INTO tb_permission_group VALUES (1, 'Menu Principal')
INSERT INTO tb_permission_group VALUES (2, 'Menu Configuração')
INSERT INTO tb_permission_group VALUES (3, 'Menu Usuário')

INSERT INTO tb_permission VALUES (1, 1, 'Registro de ponto', 'principal', 1)
INSERT INTO tb_permission VALUES (2, 1, 'Relatório', 'relatorio', 2)
INSERT INTO tb_permission VALUES (3, 1, 'Relatório Administrativo', 'relatorioAdministrativo', 3)
INSERT INTO tb_permission VALUES (4, 1, 'Configuração', 'configuracao', 4)
INSERT INTO tb_permission VALUES (5, 1, 'Sair', 'sair', 5)
INSERT INTO tb_permission VALUES (6, 2, 'Perfil', 'configuracao.perfil', 1)
INSERT INTO tb_permission VALUES (7, 2, 'Funcionários', 'configuracao.funcionario', 2)
INSERT INTO tb_permission VALUES (8, 2, 'Permissões', 'configuracao.permissao', 3)
INSERT INTO tb_permission VALUES (9, 3, 'Restrição', 'usuario.restricao', 1)
INSERT INTO tb_permission VALUES (10, 3, 'Nível de Acesso', 'usuario.acesso', 2)


INSERT INTO tb_access_level_permission VALUES (1, 1)
INSERT INTO tb_access_level_permission VALUES (1, 2)
INSERT INTO tb_access_level_permission VALUES (1, 3)
INSERT INTO tb_access_level_permission VALUES (1, 4)
INSERT INTO tb_access_level_permission VALUES (1, 5)
INSERT INTO tb_access_level_permission VALUES (1, 6)
INSERT INTO tb_access_level_permission VALUES (1, 7)
INSERT INTO tb_access_level_permission VALUES (1, 9)
INSERT INTO tb_access_level_permission VALUES (1, 10)

INSERT INTO tb_access_level_permission VALUES (2, 1)
INSERT INTO tb_access_level_permission VALUES (2, 2)
INSERT INTO tb_access_level_permission VALUES (2, 4)
INSERT INTO tb_access_level_permission VALUES (2, 5)
INSERT INTO tb_access_level_permission VALUES (2, 6)