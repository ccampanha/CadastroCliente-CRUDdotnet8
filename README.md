# Cadastro de Clientes API

## Descrição

Esta é uma solução de API CRUD (Create, Read, Update, Delete) utilizando **ASP.NET Core 8.0**, projetada para criar e gerenciar um cadastro de clientes simples. A aplicação é uma API RESTful, que pode ser facilmente testada e documentada através do **Swagger**. Esta solução foi solicitada para um processo seletivo de Desenvolvedora Backend, com uma configuração genérica e requisitos simples.

### Funcionalidades

Foram desenvolvidas as seguintes funcionalidades:
- Listar dados de forma paginada (utilizando uma biblioteca para paginação)
- Criar registros de clientes, permitindo múltiplos endereços e telefones
- Atualizar registros existentes, incluindo alteração de endereços, telefones e e-mails. Não foram criados estas alterações separadas, dece ser atualizado o registro inteiro.
- Excluir registros de clientes
- Adicionar endereço ao cliente.
- Busca de endereço por CEP através da API [ViaCEP](https://viacep.com.br/)
- Adicionar endereço ao cliente com os dados de endereço e também com utilizando uma funcionalidade que carrega parte dos dados de endereço utilizando a através da API [ViaCEP](https://viacep.com.br/)

## Modelo de Dados

A classe/tabela **Cliente** possui um relacionamento de 1 para muitos com **Endereco**, **Telefone** e **Email**. Abaixo estão as entidades criadas:

### Cliente
- **CNPJ**: string (14 caracteres)
- **Nome**: string
- **Status**: Ativo/Inativo (default: Ativo)
- **Lista de Endereços**
- **Lista de Telefones**
- **Lista de E-mails**

### Endereço
- **Id**: Campo único de identificação
- **Nome**: string (opcional)
- **CEP**: string (formato: "01001-000", 9 caracteres, opcional)
- **Logradouro**: string
- **Número**: int (opcional)
- **Complemento**: string (100 caracteres, opcional)
- **Bairro**: string (50 caracteres, obrigatório)
- **Cidade**: string (50 caracteres, obrigatório)
- **UF**: string (2 caracteres, obrigatório)

### Telefone
- **Id**: Campo único de identificação
- **CodigoDDD**: string (2, obrigatório)
- **Número**: string (10, obrigatório)
- **Contato**: string (15, opcional)

### E-mail
- **Id**: Campo único de identificação
- **EnderecoEmail**: string (100, obrigatório)

Cada entidade **ClientePJ** pode ter várias listas de **Endereços**, **E-mails** e **Telefones** que vêm de uma entidade separada em relacionamento de tabelas.

## Estrutura do Projeto

- A solução foi dividido em dois projetos separados, a API e o Teste:
- **CadastroClienteAPI**: O projeto principal da API.
- **CadastroClienteAPI.Teste**: O projeto de testes unitários.

Foi utilizadas 

### Pastas no Projeto CadastroClienteAPI
- **Controllers**: Controladores da API.
- **Infrastructure**: Para configuração de banco de dados, mapeamento e políticas de resiliência.
- **Models**: Entidades e classes de dados.
- **Interfaces**: Interfaces de abstração.
- **Services**: Implementação de regras de negócios.
- **Repositories**: Manipulação de dados com o Entity Framework.
- **Validators**: Classes de validação usando FluentValidation.

## Integração com API Externa

Foi criado um endpoint proxy **BuscaEnderecoCEP** para consultar o endereço através da API [ViaCEP](https://viacep.com.br/). 

Para utilizar este endpoint, o CEP deve estar no formato de 8 caracteres numéricos, e você pode acessar a seguinte URL:
(https://viacep.com.br/ws/<cep recebido>/json/)


Além disso, foi implementada a funcionalidade para adicionar endereço por CEP. Os endpoints **AdicionaEnderecoCEP** e **BuscaEnderecoCEP** foram adicionados no **EnderecoController**.

## Bibliotecas Utilizadas

O projeto utiliza as seguintes bibliotecas, sugeridas pelo "Cliente":
- **FluentValidation**: Para validações de entrada.
- **Moq**: Para testes unitários.
- **Swagger**: Para documentação das APIs.
- **Automapper**: Para mapeamento de objetos.
- **Polly**: Foram feitas tentativas de implementação de políticas de resiliência (ex: retry, circuit breaker) na busca da API Externa, que infelizmente não funcionaram, e devido ao prazo decidi retirar e deixar para implementações fututras. Deixei a classe de configuração das polítticas PollyPolicies.cs no código para testar em futuras implementações.

## Validação

Foi utilizado o FluentValidation que impede a entrada dos erros de acordo com o configurado. Foram criadas classes de validações para executar o projeto, e todas estas configurações foram incluídas/chamadas na classe de validação do cliente, já que as alterações são feitas nesta area. Como não se trata de um projeto real alguns validadores foram desenvolvidos mas não foram utilizados a fim de facilitar os testes, como o validador de CNPJ.

## Como Executar o Projeto

Para executar o projeto, siga os passos abaixo:

1. **Clone o repositório**:
   ```bash
   git clone https://github.com/seuusuario/CadastroClienteAPI.git
   cd CadastroClienteAPI

2. **Restaure os pacotes do NuGet:**:
   ```bash
   dotnet restore

3. **Execute a aplicação:**:
   ```bash
   dotnet run

4. **Acesse a documentação da API no Swagger em:**:
   http://localhost:5000/swagger

5. **Execute os testes:**:
   ```bash
   cd CadastroClienteAPI.Teste
   dotnet test

5. **Execute os testes:**:
   ```bash
   cd CadastroClienteAPI.Teste
   dotnet test

## Problemas a serem corrigidos:
Devido ao prazo e outros compromissos, não foi possível corrigir alguns erros de funcionamento/especificação descritos abaixo:
**-Enum**: Foram criados Enum para os campos Ativo/Inativo e também TipoTelefone, mas aparece um valor numérico ao invés da descrição.
**-Erro na alteração de dados do Cliente**: Ao fazer o Update do cliente, ao invés de apenas alterar as listas de Endereço, Email e Telefone, são gerados novos registros para esta lista, alterando o valor dos Ids de Endereço, Email e Telefone. Foram várias horas tentando solucionar este problema e várias alterações de código e por fim decidi subir o código desta forma. Faltou a "equipe amiga" para ajudar a encontrar aquela "virgula esquecida".
**-Erro nos testes**: Devido ao problema citado acima de Erro na alteração de dados do Cliente, nem todos os testes foram bem sucedidos.

## Melhorias Futuras: 
Pretendo criar as seguintes melhorias/correções:
- Corrigir os problemas acima.
- Criar testes mais abrangentes.
- Criar uma classe abstrata Pessoa, que poderá ser utilizado para outros tipos de contato, como funcionários e tambem para especializar o cliente em Pessoa Física e Pessoa Jurídica, já que o cliente citado é de Pessoa Juridica, com CNPJ.
- Incluir mais atributos como Razão Social, Nome Fantasia, etc no cliente, definição se os endereços, telefones e emails são os principais, Enum para UF.
- Incluir mais funcionalidades como CRUD para Endereço, Email e Telefone.

## Considerações Finais
Este projeto é uma implementação inicial e está aberto a melhorias. Sugestões e contribuições são sempre bem-vindas. Para quaisquer dúvidas ou problemas, sinta-se à vontade para abrir uma issue neste repositório.

## Sobre mim
Tenho muitos anos de trabalho mas sou relativamente nova em programação, sempre buscando novos conhecimentos. Tenho formação inicial em Engenharia de Produção (UFRJ) e experiência de trabalho/estágio na em indústria e na área de qualidade. Sempre tive paixão por programação e decidi fazer uma Pós- graduação de Análise de Sistemas da PUC/Rio, curso excelente e muito bem avaliado na época. 
Trabalhei na area de como bolsista na Fundação COPPETEC e como consultora na Embratel (PJ), quando fui aprovada no concurso de Administradora de Redes da Caixa Econômica Federal, onde trabalhei por 18 anos, mas por burocracias do meu cargo não foi possível seguirtrabalhar como Analista de Sistemas ou desenvolvedora, um grande sonho. Saí da Caixa no programa de demissão voluntária e fui viver outro sonho de morar na Argentina, país onde tenho muitos amigos. Neste periodo da Argentina, fiz cursos na àrea de gastronomia e de Desenvolvedora Full Stack, além de cursos de React e Python. Ao voltar para o Brasil, começou a pandemia e neste periodo fiz o curso "Universidade Webmarketing" da Hostnet, onde aprendíamos sobre negócios, contratos e a desenvolver páginas em Wordpress, mas descobri que não gosto da área de vendas e decidi investir em mais estudos e em formação, voltando a fazer uma graduação EAD em Sistemas para Internet na UNICESUMAR que me abriu portas para buscar trabalhos na área de desenvolvimento. Tive a experiência de 2 anos em trabalho remoto e equipe ágil como desenvolvedora backend (.NET / C# / SQL Server), mas também fazendo manutenção no site legado desenvolvido em ASP MVC.
Segue meu linkedin para contato: https://www.linkedin.com/in/cristiana-campanha/


