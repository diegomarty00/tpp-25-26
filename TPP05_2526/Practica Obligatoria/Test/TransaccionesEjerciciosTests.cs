namespace Test;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Transacciones;

[TestClass]
public sealed class TransaccionesEjerciciosTests
{
    private static readonly Dictionary<string, decimal> MargenPorRegion =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["Europa"] = 0.80m,
            ["Asia"] = 0.60m,
            ["Norteamérica"] = 0.75m // ejemplo razonable; ajusta si tu profe especifica otro
        };

    private static List<Venta> Datos()
        => new()
        {
            new Venta { Region = "Europa", Estado = Estado.Cancelada,  Cantidad = 100m },
            new Venta { Region = "Asia",   Estado = Estado.Confirmada, Cantidad = 500m },
            new Venta { Region = "Europa", Estado = Estado.Confirmada, Cantidad = 120m },
            new Venta { Region = "Asia",   Estado = Estado.Cancelada,  Cantidad = 300m },
            new Venta { Region = "Norteamérica", Estado = Estado.Confirmada, Cantidad = 1000m },
            new Venta { Region = "Norteamérica", Estado = Estado.Cancelada,  Cantidad = 50m },
            new Venta { Region = "Europa", Estado = Estado.Confirmada, Cantidad = 200m },
        };

    // (a) Nº de ventas no confirmadas en Norteamérica
    [TestMethod]
    public void VentasNoConfirmadasNA_ConteoCorrecto()
    {
        var ventas = Datos();
        var n = Program.VentasNoConfirmadasEnNA(ventas);
        Assert.AreEqual(1, n);
    }

    // (b) Importe total confirmadas en Europa
    [TestMethod]
    public void TotalConfirmadasEuropa_SumaCorrecta()
    {
        var ventas = Datos();
        var total = Program.TotalConfirmadasEuropa(ventas);
        Assert.AreEqual(120m + 200m, total);
    }

    // (c) Región con mayor facturación neta (nombre + importe)
    [TestMethod]
    public void RegionConMayorFacturacionNeta_DevuelveTop()
    {
        var ventas = Datos();
        var (region, neto) = Program.RegionConMayorFacturacionNeta(ventas, MargenPorRegion);
        Assert.AreEqual("Norteamérica", region);
        Assert.AreEqual(1000m * 0.75m, neto, "El neto de NA confirmada debe ser el mayor en estos datos");
    }

    // (d) Agrupar por rangos con Reduce → Dictionary<string, List<Venta>>, [min, max)
    [TestMethod]
    public void AgruparPorRangos_RespetaIntervalosSemiabiertos()
    {
        var ventas = Datos();
        var ranges = new (decimal, decimal)[] { (0m, 100m), (0m, 500m), (500m, 2000m) };
        var dic = Program.AgruparPorRangos(ranges, ventas);

        Assert.IsTrue(dic.ContainsKey("0-100"));
        Assert.IsTrue(dic.ContainsKey("0-500"));
        Assert.IsTrue(dic.ContainsKey("500-2000"));

        // 50 -> [0,100) y también [0,500)
        Assert.IsTrue(dic["0-100"].Any(v => v.Cantidad == 50m));
        Assert.IsTrue(dic["0-500"].Any(v =>
            v.Cantidad == 50m || v.Cantidad == 100m || v.Cantidad == 120m || v.Cantidad == 200m));

        // 1000 -> [500,2000)
        Assert.IsTrue(dic["500-2000"].Any(v => v.Cantidad == 1000m));
    }
}