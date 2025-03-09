using System.Collections;
namespace OrderManagementTests;


public class OrderData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { 1, true, false };  // Pedido já processado
        yield return new object[] { 2, false, false }; // Pedido não encontrado
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}