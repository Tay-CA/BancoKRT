# BancoKRT

Objetivo: Criar um sistema para a gestão de limite das contas do Banco fictício KRT.

Funcionalidades: 
-   cadastro das informações referentes à gestão de limite no banco de dados;
-   busca de informações de limite para uma conta já cadastrada;
-   alteração de limite para transações PIX de uma conta já cadastrada;
-   remoção de registros do banco de dados de limite;
-   transações de PIX entre contas cadastradas.

Como configurar:
- instale AWS CLI;
- conecte-se ao DynamoDb localmente (para esse projeto foi utilizada a imagem via docker);
- configure as credentials do banco;
- insira as credentials e a URL local para o DynamoDB no appsettings do projeto;
- ative o GestaoLimitesContas.Api como startup project;
- rode o projeto como HTTPS.

O que foi utilizado:
- .Net 8;
- DynamoDb;
- Mediatr;
- CQRS;
- Testes Unitários;
- Domain Driven Design;
- SOLID;
- Clean code.
