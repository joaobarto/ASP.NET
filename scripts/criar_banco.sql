CREATE DATABASE [GerenciadorTarefas];
GO
USE [GerenciadorTarefas];
GO

CREATE TABLE Tarefas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Descricao NVARCHAR(255) NOT NULL,
    Concluida BIT NOT NULL DEFAULT 0,
    DataCriacao DATETIME DEFAULT GETDATE()
);
