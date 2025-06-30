CREATE DATABASE IF NOT EXISTS `Gerenciador_Tarefas` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;
USE `Gerenciador_Tarefas`;


CREATE TABLE IF NOT EXISTS Tarefas  (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Descricao VARCHAR(255) NOT NULL,
    Concluida TINYINT NOT NULL DEFAULT 0,
    DataCriacao DATETIME DEFAULT CURRENT_TIMESTAMP
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
