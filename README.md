# Teste.EL.NucleoAluguel.API

O Projeto representa uma API criada para atender um cenário hipotético de uma locadora de veículos. Mais informações sobre o funcionamento podem ser verificadas na sessão Resquisitos do projeto. 

## Decisões Técnicas

• Optou-se por desenvolver uma api RESTfull, sendo possível verificar sua documentação exposta por OpenAPI (Swagger).

• Utilizando ASP.NET Core 3.1 por ser performático e multiplataforma.

• Estrutura da Solução criada com base em principios do DDD, visando diminuir a complexidade e facilitar o entendimento e manutenção das regras de negócio.

• Para aplicar a ideia 'Fail Fast Principle', está sendo utilizad Flunt.Validations. A biblioteca proporciona uma forma simples de realizar validações, sendo utilizada de forma que o domínio seja responsável por definir seu estado válido ao ser instanciado.

• Criando projeto de testes com xUnit: Optou-se por este framework por ser mais performático e por ser mais adequado para a execução do TDD. Diferentemente do nUnit e MSTest, o xUnit cria uma instancia da classe de teste para cada um dos métodos, garantindo que os testes sejam executados paralelamente e de forma isolada).

• Implementando Autenticação e Autorização no esquema Bearer Authentication + JWT. A escolha é justificada por ser uma solução simples e robusta o suficiente para resolver o problema (Keep it Simple Stupid).


## Requisitos do projeto

• Prover um Serviço Web com arquitetura a seu critério para atender as demandas do Site e/ou App 

• Um serviço que deverá disponibilizar autenticação e autorização para usuários com os perfis: OPERADOR, CLIENTE.

• O cadastro de usuário terá os campos login, senha, onde o campo login pode ser o CPF do cliente (perfil CLIENTE) ou a matrícula (perfil OPERADOR). 

• O cadastro de cliente deverá ter os campos: ID, NOME, CPF, ANIVERSARIO, ENDEREÇO (CEP, logradouro, número, complemento, cidade e estado).

• O cadastro de operador deverá ter os campos: MATRÍCULA, NOME. 

• Se o Login for CPF retornar os dados do cliente, se for por matrícula retornar os dados do operador. 

• Cadastro de veículos informando os campos: PLACA, MARCA, MODELO, ANO, VALOR HORA, COMBUSTÍVEL (gasolina, álcool, diesel), LIMITE PORTA MALAS, CATEGORIA (básico, completo, luxo).

• Cadastro de Marcas e Modelos para relacionar ao cadastro de veículos.

• Disponibilizar um serviço de simulação de locação com base no veículo selecionado, valor hora do veículo, total de horas. 

• Serviço de agendamento de um veículo calculando o valor final considerando a categoria do veículo, valor hora, total de horas.

• Disponibilizar um modelo de contrato de locação no formato .PDF.

• Check-List para vistoria na devolução do veículo considerando os itens: CARRO LIMPO, TANQUE CHEIO, AMASSADOS e ARRANHÕES, considerando um custo adicional de 30% do valor da locação em cada ocorrência. 
 
