-- Criação da sequência para gerar os valores automaticamente
CREATE SEQUENCE words_id_seq START 1;

-- Criação da tabela Words
CREATE TABLE "Words" (
    "Id" int PRIMARY KEY DEFAULT nextval('words_id_seq') not null,
	"Word" bytea not null,
	"Date" TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP
);


select * from "Words"


drop table "Words"
drop sequence words_id_seq