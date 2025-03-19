# Testes Unitários

Este repositório contém exemplos de testes unitários utilizando Moq e frameworks de testes como xUnit e MSTest. Os testes verificam o comportamento de dois serviços: `OrderProcessingService` e `BankAccount`.

## Tópicos

- [OrderProcessingService Tests](#orderprocessingservice-tests)
- [BankAccount Tests](#bankaccount-tests)

## BankAccount Tests

Esse exemplo foi criado pela Microsoft e está disponível na documentação oficial sobre testes unitários. Os testes implementados validam o comportamento da classe de conta bancária, verificando se os métodos de débito funcionam corretamente e se as exceções são lançadas quando os valores são inválidos.

### Testes Implementados:

- **`Debit_WithValidAmount_UpdatesBalance`**:
  - Verifica se o saldo da conta bancária é atualizado corretamente após um débito.
- **`Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange`**:
  - Verifica se uma exceção é lançada quando o valor a ser debitado é maior do que o saldo da conta.

## OrderProcessingService Tests

Os testes de `OrderProcessingService` verificam a funcionalidade de processamento de pedidos, como:

- Se um pedido foi processado corretamente.
- Se o email de confirmação foi enviado.
- Se as interações com o repositório de pedidos ocorreram conforme esperado.

### Testes Implementados:

- **`ProcessOrder_ShouldProcessOrder_WhenOrderExistsAndIsNotProcessed`**:
  - Verifica se um pedido é processado corretamente quando ele existe e ainda não foi processado.
  - Garante que o pedido é marcado como processado.
  - Verifica se o método `SaveOrder` é chamado corretamente.
  - Confirma que um email de confirmação foi enviado ao cliente.

- **`ProcessOrder_ShouldNotProcess_WhenOrderIsAlreadyProcessed`**:
  - Verifica se o pedido não é processado novamente caso já tenha sido processado.
  - Garante que o repositório não salva o pedido novamente.
  - Certifica-se de que nenhum email é enviado para o cliente.

- **`ProcessOrder_ShouldReturnFalse_WhenOrderDoesNotExist`**:
  - Retorna `false` quando o pedido não existe no repositório.
  - Garante que nenhuma tentativa de salvar o pedido ocorre.
  - Verifica que nenhum email de confirmação é enviado.

- **`ProcessOrder_ShouldReturnCorrectResult_WhenUsingClassData`**:
  - Usa `ClassData` para fornecer dados de teste reutilizáveis.
  - Testa diferentes cenários para validar o comportamento do método `ProcessOrder`.

- **`ProcessOrder_ShouldReturnCorrectResult_WhenUsingMemberData`**:
  - Usa `MemberData` para fornecer dados de teste de um método estático.
  - Testa diferentes cenários para validar o comportamento do método `ProcessOrder`.

