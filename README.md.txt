Lista de Tarefas - Projeto Full Stack

Este projeto é uma aplicação de Lista de Tarefas, desenvolvida com Vue.js no front-end e C# (.NET) no back-end, utilizando SQLite para persistência dos dados.

Tecnologias Utilizadas:

Frontend: Vue.js 3

Backend: C# .NET Core

Banco de Dados: SQLite

Comunicação Frontend/Backend: Axios

Estrutura do Projeto:

Projeto/ ├── tarefas-frontend/ (Aplicação Vue.js) ├── tarefas-backend/ (API em C# com SQLite)

Antes de iniciar, é necessário ter instalado:

Node.js + npm (https://nodejs.org/pt)

.NET SDK 8.0 (https://dotnet.microsoft.com/pt-br/download)

Como rodar o projeto:

1 - Clonar o repositório

git clone https://github.com/Lucas-luza/lista-de-tarefa.git

2 - Rodar o Backend (C#)

cd tarefas-backend dotnet run

O backend vai rodar localmente em http://localhost:5167

Observação:
Caso o comando dotnet run gerar outro endereço (outro localhost e porta diferente), é necessário editar a constante API_URL no arquivo: 
tarefas-frontend\src\services\TarefasService

3 - Rodar o Frontend (Vue.js)

cd tarefas-frontend npm install npm run serve

O frontend vai rodar localmente em http://localhost:8080

Funcionalidades:

Adicionar nova tarefa

Editar tarefa existente

Marcar tarefa como concluída

Visualizar tarefas concluídas e pendentes
