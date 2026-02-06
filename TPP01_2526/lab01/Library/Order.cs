using System;

namespace Library;

public class Order
{
    required public string Id { get; init; }
    public string NombreCliente { get; set; }
    required public int NumeroArticulos { get; set; }
    required public decimal PrecioUnitario { get; set; }
    required public decimal GastosEnvio { get; set; }
    public decimal Subtotal { 
        get{
            return PrecioUnitario * NumeroArticulos;
        }
    }
    public decimal Total { 
        get{
            return Subtotal + GastosEnvio;
        }
    }
    enum OrderStatus {Borrador, Pagado, Enviado, Entregado, Cancelado}

    public Order() {
    }

    public override string ToString()
    {
        return $"Order ID: {Id}\n" +
               $"Cliente: {NombreCliente}\n" +
               $"Número de Artículos: {NumeroArticulos}\n" +
               $"Precio Unitario: {PrecioUnitario} euros\n" +
               $"Gastos de Envío: {GastosEnvio} euros\n" +
               $"Subtotal: {Subtotal} euros\n" +
               $"Total: {Total} euros";
    }
}
