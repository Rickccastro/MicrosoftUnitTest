# Testes Unitários


Este repositório contém exemplos de testes unitários utilizando **Moq** e frameworks de testes como **xUnit** e **MSTest**. Os testes verificam o comportamento de dois serviços: `OrderProcessingService` e `BankAccount`.

## Tópicos

- [OrderProcessingService Tests](#orderprocessingservice-tests)
- [BankAccount Tests](#bankaccount-tests)
- [Como Rodar os Testes](#como-rodar-os-testes)

---

## BankAccount Tests
Esse exemplo foi criado pela Microsoft e está disponivel na documentação da sessão de testes unitarios na [Documentação do Teste de Unidade](https://learn.microsoft.com/pt-br/visualstudio/test/walkthrough-creating-and-running-unit-tests-for-managed-code?view=vs-2022)
 os testes implementados validam o comportamento da classe da  conta bancária, verificando se os métodos de débito estão funcionando corretamente e se as exceções são lançadas quando os valores são inválidos.

Testes Implementados:
Debit_WithValidAmount_UpdatesBalance

Verifica se o saldo da conta bancária é atualizado corretamente após um débito.
Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange

Verifica se uma exceção é lançada quando o valor a ser debitado é maior do que o saldo da conta.


## OrderProcessingService Tests

Os testes de `OrderProcessingService` verificam a funcionalidade de processamento de pedidos, como a verificação se um pedido foi processado corretamente, se o email foi enviado, e se as interações com o repositório de pedidos ocorreram conforme esperado.

### Testes Implementados:

1. **ProcessOrder_ShouldProcessOrder_WhenOrderExistsAndIsNotProcessed**
    - Verifica se o pedido é processado corretamente quando existe e não foi processado.
    - Utiliza o método `Fact` para um único caso de teste.

2. **ProcessOrder_ShouldReturnCorrectResult_WhenOrderExistsAndIsNotProcessed**
    - Testa diferentes cenários de pedidos com o uso de `Theory` e `InlineData`, verificando se o pedido é processado corretamente ou não, com base no estado do pedido.

3. **ProcessOrder_ShouldReturnCorrectResult_WhenUsingClassData**
    - Usa `ClassData` para fornecer dados de teste, permitindo a reutilização de dados e mantendo os testes organizados.

4. **ProcessOrder_ShouldReturnCorrectResult_WhenUsingMemberData**
    - Usa `MemberData` para fornecer dados de teste a partir de um método estático, permitindo mais flexibilidade para definir os dados.
