CREATE DATABASE myfinance;
GO

USE myfinance;

CREATE TABLE planoconta(
	id int identity(1,1) not null,
	descricao varchar(50) not null,
	tipo char(1) not null,
	primary key (id)
);

CREATE TABLE transacao(
	id int identity(1,1) not null,
	historico text null,
	data date not null,
	valor decimal(9,2),
	planocontaid int not null,
	primary key(id),
	foreign key(planocontaid) references planoconta(id)
);

INSERT INTO planoconta(descricao,tipo) values ('Combustivel', 'D');

INSERT INTO planoconta(descricao,tipo) values ('Alimentação', 'D');

INSERT INTO planoconta(descricao,tipo) values ('Impostos', 'D');

INSERT INTO planoconta(descricao,tipo) values ('Salário', 'R');

SELECT * FROM planoconta;

INSERT INTO transacao(historico, data, valor, planocontaid)
VALUES('Gasolina para viagem', GETDATE(), 300, 1);

INSERT INTO transacao(historico, data, valor, planocontaid)
VALUES('Compras do mês', GETDATE()+2, 650, 2);

INSERT INTO transacao(historico, data, valor, planocontaid)
VALUES('Salário do mês', '2023-01-07', 1000, 4);

SELECT * FROM transacao;
