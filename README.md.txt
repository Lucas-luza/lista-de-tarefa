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

.NET SDK 9.0 (https://dotnet.microsoft.com/pt-br/download)

Como rodar o projeto:

1 - Clonar o repositório

git clone https://github.com/Lucas-luza/lista-de-tarefa.git

Ou baixar o .zip do projeto.

2 - Rodar o Backend (C#)

no prompt de comando: 
cd tarefas-backend dotnet restore
cd tarefas-backend dotnet run

O backend vai rodar localmente em http://localhost:5167

Deixe essa janela aberta, CNTRL + C, desliga o servidor.

Observação:
Caso o comando dotnet run gerar outro endereço (outro localhost e porta diferente), é necessário editar a constante API_URL no arquivo: 
tarefas-frontend\src\services\TarefasService

3 - Rodar o Frontend (Vue.js)

no prompt de comando:
cd tarefas-frontend npm install
cd tarefas-frontend npm run serve

O frontend vai rodar localmente em http://localhost:8080

Funcionalidades:

Adicionar nova tarefa

Editar tarefa existente

Marcar tarefa como concluída

Visualizar tarefas concluídas e pendentes
