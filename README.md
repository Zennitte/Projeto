
# Simulador de Transações

Aplicação que simula o fluxo de transações de um banco

## Setup
### Requisitos
- Docker
- Git
### Get Started
- Acesse o repositório
- Copia a url (https://github.com/Zennitte/Projeto.git)
- Abra o terminal do git e digite o comando: git clone + url
- Acesse a pasta raiz do Projeto
- Abra o terminal
- Execute o comando: docker compose up --build
- Após a criação dos containers, aguarde 60 segundos para a criação das tabelas do banco no container DB
- Acesse a API pela url(http://localhost:4000)
## Rotas
### Users
#### Users[Get]
- Requer Autorização
- Retorna todos os usuários da Aplicação
#### Users/{id}[Get]
- Requer Autorização
- Retorna o usuário cujo o id é equivalente ao passado
#### Users[Post]
- Cria um usuário
- Retorna o usuário criado
- Recebe como parâmetros: username e password
#### Users/Login[Post]
- Recebe como parâmetros: username e password
- Retorna um token jwt
### Accounts
#### Accounts[Get]
- Requer Autorização
- Retorna todas as contas da Aplicação
#### Accounts/{id}[Get]
- Requer Autorização
- Retorna a conta cujo o id é equivalente ao passado
### Transactions
#### Transactions/All[Get]
- Requer Autorização
- Retorna todas as transações do usuário autenticado
#### Transactions/CashIn[Get]
- Requer Autorização
- Retorna todas as transações de cashin do usuário autenticado
#### Transactions/CashOut[Get]
- Requer Autorização
- Retorna todas as transações de cashout do usuário autenticado
#### Transactions/All[Get]
- Requer Autorização
- Cria uma transação
- Retorna a transação criada
- Recebe como parametros: 
  - debitedUser: username cujo valor da transação será retirado
  - creditedUser: username cujo valor da transação será depositado
  - amount: valor da transação
## Autorização
Para acessar as rotas que requerem Autorização, siga o seguintes passos.
- Clique na opção Authorize no topo da página
- Após a abertura do menu insira no campo value: Bearer + token(gerado pela rota de login)
- Clique no botão authorize para aplicar a Autorização
- Feche o menu
- Pronto Autorização aplicada
- Obs: 
  - Certifique-se de após o Bearer adicionar um espaço antes do token, exemplo: Bearer token
  - Certifique-se do token não conter aspas