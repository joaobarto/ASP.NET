-- 🔸 Criação da Tabela de Tarefas
CREATE TABLE Tarefas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Descricao NVARCHAR(255) NOT NULL,
    Concluida BIT NOT NULL DEFAULT 0,
    DataCriacao DATETIME DEFAULT GETDATE()
);

-- 🔹 Inserir uma Tarefa
INSERT INTO Tarefas (Descricao)
VALUES ('Estudar para a prova de matemática');

-- 🔹 Listar Todas as Tarefas
SELECT 
    Id, 
    Descricao, 
    CASE 
        WHEN Concluida = 1 THEN 'Concluída' 
        ELSE 'Pendente' 
    END AS Status,
    DataCriacao
FROM Tarefas
ORDER BY DataCriacao DESC;

-- 🔹 Marcar uma Tarefa como Concluída

UPDATE Tarefas
SET Concluida = 1
WHERE Id = 1;

-- 🔹 Remover uma Tarefa

DELETE FROM Tarefas
WHERE Id = 1;
