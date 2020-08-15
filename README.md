# Locadoura
> Projeto realizado com intuito de simular uma gestão de filmes dentro de uma locadoura

Nesse projeto foi abordado o fluxo básico de uma locadoura, em que é possivel cadastrar um cliente, um livro e as locaçoes.

## Configurando e executando o  backend

Para inicializar o projeto é necessário ter as seguintes ferramentas:
- Visual Studio 2019
- .NET Core 2.2 SDK
- MySql WorkBeach

Abrindo o projeto:
- Dentro da pasta "Controller" que fica na raiz do projeto, tem um arquivo chamado "Controller.sln", abra ele no VS 2019

Executando o projeto localmente:
- Com o projeto aberto no VS 2019, precisamos verificar se o arquivo de configuração está correto
  - vá para o arquivo "appsettings.json" que está localizado dentro do projeto "Controller"
  - dentro dele verifique se a connectionString nomeada como "DefaultConnection" está configurada para acessar um banco MySql

- Após a configuraçao iremos gerar o banco de dados
  - na caixa que fica no menu superior pesquise por "Package Manager Console" e abra
  - Dentro do console iremos executar os comandos necessário para gerar o banco de dados
  - antes de executar os comandos verifique se o projeto "Repository" está selecionado no console como default project

  ```bash
  update-database
  ```
 
  - após a execução desse comando verifique se o banco foi criado corretamente
- Com o banco já criado iremos executar a aplicação
  - pode ser usado a tecla "F5" para uma execução rapida, ou clicando no icone de play no menu superior
  - caso ocorra algum erro altere no menu superior de "Docker" para "ISS Express" e execute o projeto novamente

## Configurando e executando o  backend

O frontend pode ser executado de duas formas, uma rodando a aplicação em vue.js ou acessando a seguinte url: https://yuri-duque-locadoura.netlify.app/

- executando projeto em vue.js
  - abra no visual studio code a pasta "Frontend" que está na raiz do projeto
  - execute os seguintes comandos
  
  ```bash
  npm install
  npm run serve
  ```

