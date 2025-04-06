# Teste Técnico Pleno

Bem-vindo ao repositório do **Teste Técnico Pleno**. Este projeto foi desenvolvido para demonstrar habilidades em desenvolvimento de APIs utilizando tecnologias modernas, seguindo boas práticas de código e arquitetura.

## Índice

- [Sobre o Projeto](#sobre-o-projeto)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Configuração e Execução](#configuração-e-execução)
- [Testes](#testes)
- [Contribuição](#contribuição)
- [Licença](#licença)
- [Contato](#contato)

## Sobre o Projeto

O **Teste Técnico Pleno** é uma API desenvolvida em C# com o objetivo de gerenciar clientes para Mobi2Buy. A API permite operações como criação, leitura, atualização e exclusão de clientes, integrando-se com serviços externos para validação de dados, como o ViaCEP para consulta de endereços.

## Tecnologias Utilizadas

- **C#**: Linguagem principal utilizada no desenvolvimento.
- **.NET 7.0**: Framework utilizado para construção da API.
- **Entity Framework Core**: ORM para manipulação do banco de dados.
- **SQLite**: Banco de dados utilizado para armazenamento dos dados.
- **Moq**: Biblioteca para criação de mocks nos testes unitários.
- **Bogus**: Gerador de dados falsos para testes.
- **RabbitMQ**: Mensageria para comunicação assíncrona.
- **Docker**: Utilizado para conteinerização da aplicação.

## Estrutura do Projeto

A estrutura do projeto está organizada da seguinte forma:
TesteTecnico-Pleno/
 ```bash
├── API/                 # Contém os controllers e configurações da API
├── Application/         # Interfaces e serviços da aplicação
├── Domain/              # Modelos e entidades do domínio
├── Infrastructure/      # Configurações de banco de dados e repositórios
├── Tests/               # Testes unitários e de integração
├── ClienteService.csproj
├── Program.cs
├── appsettings.json
├── Dockerfile
└── docker-compose.yml
 ```

## Configuração e Execução

### Pré-requisitos

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Docker](https://www.docker.com/get-started) (opcional, para execução via contêiner)

### Passos para Executar

1. **Clone o repositório**:

   ```bash
   git clone https://github.com/FabioNeves28/TesteTecnico-Pleno.git
   ```
2. **Navegue até o diretório do projeto**:
   ```bash
   cd TesteTecnico-Pleno
   ```
3. **Restaurar as dependências:**:
   ```bash
   dotnet restore
   ```
4. **Aplicar as migrações e atualizar o banco de dados**:
   ```bash
   dotnet ef database update
   ```
5. **Executar a aplicação:**:
   ```bash
   dotnet run
   ```

## Executando com Docker

1. **Construa a imagem Docker:**:
   ```bash
   docker build -t teste-tecnico-pleno .
   ```
   
2. **Execute o contêiner:**:
   ```bash
   docker run -d -p 5000:80 teste-tecnico-pleno
   ```


   
   


