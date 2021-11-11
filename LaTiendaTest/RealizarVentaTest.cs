using LaTienda.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LaTiendaTest
{
    [TestClass]
    public class RealizarVentaTest
    {
        [TestMethod]
        public void RealizarVentaSinProductos()
        {
            Venta venta = new Venta();
            var resultado = venta.ValidarVenta();
            Assert.IsFalse(resultado);
        }


        [TestMethod]
        public void RealizarVentaSinClienteAsociado()
        {
            Venta venta = new Venta();
            venta.AgregarLineaDeVenta(new LineaDeVenta());
            var resultado = venta.ValidarVenta();
            Assert.IsFalse(resultado);
        }


        [TestMethod]
        public void RealizarVentaSinEmpleadoAsociado()
        {
            Venta venta = new Venta();
            venta.AgregarLineaDeVenta(new LineaDeVenta());
            venta.Cliente = new Cliente();
            var resultado = venta.ValidarVenta();
            Assert.IsFalse(resultado);
        }


        [TestMethod]
        public void RealizarVentaExitosa()
        {
            Venta venta = new Venta();
            venta.AgregarLineaDeVenta(new LineaDeVenta());
            venta.Cliente = new Cliente();
            venta.Empleado = new Empleado();
            var resultado = venta.ValidarVenta();
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void RealizarVentaMontoNegativo()
        {
            Venta venta = new Venta();
            venta.AgregarLineaDeVenta(new LineaDeVenta { Precio = -45, Cantidad= 1 });
            venta.Cliente = new Cliente();
            venta.Empleado = new Empleado();
            venta.CalcularMonto();
            var resultado = venta.ValidarVenta();
            Assert.IsFalse(resultado);
        }

    }
}
