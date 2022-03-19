using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AFIPServiceReference;
using FluentValidation;
using LaTienda.API.Persistence;
using LaTienda.API.Services;
using LaTienda.Model;
using LoginServiceReference;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LaTienda.API.Features.Ventas
{
    public class CreateVentaCommand
    {
        public class Command : IRequest<CommandResult>
        {
            public int IdEmpleado { get; set; }
            public string Cuit { get; set; }
            public List<LineaDeVentaDTO> LineaDeVentas { get; set; }
        }

        public class LineaDeVentaDTO
        {
            public int Cantidad { get; set; }
            public int IdTalle { get; set; }
            public double Precio { get; set; }
            public int IdColor { get; set; }
            public string CodigoProducto { get; set; }
        }

        public class CommandResult
        {
           public int IdVenta { get; set; }
           public List<LineaDeVentaDTO> LineaDeVenta { get; set; }
           public Comprobante Comprobante { get; set; }
           public double Total { get; set; }
           public Object FeCabResp { get; set; }
           public Object FeDetResp { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {

            public CommandValidator()
            {
                


            }
        }

        public class Handler : IRequestHandler<Command, CommandResult>
        {
            private TiendaContext _context;
            private IAfipService _afipService;
            public Handler(TiendaContext context, IAfipService afipService)
            {
                _context = context;
                _afipService = afipService;
            }

            public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
            {
                var empleado = await _context.Empleados
                    .Include(e => e.Sucursal)
                    .FirstOrDefaultAsync(e => e.Id == request.IdEmpleado);
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Cuit.Equals(request.Cuit));
                var ldvs = await MapLineaDeVentas(request.LineaDeVentas,empleado.Sucursal.Id);
                var venta = new Venta()
                {
                    Empleado = empleado,
                    LineaDeVentas = ldvs,
                    FechaHora = DateTime.Now,
                    Cliente = cliente,
                };
                venta.CalcularMonto();
                var comprobante = new Comprobante()
                {
                    Cliente = cliente,
                    Tipo = cliente.GetTipoComprobante()
                };
                var responseComp = await _afipService.AutorizarCompAFIPAsync("F8E57E68-1416-480F-A540-0D3223F28A0F", venta,long.Parse(request.Cuit),cliente);
                if(responseComp.Body.FECAESolicitarResult.FeDetResp[0] != null)
                comprobante.NumeroComprobante = responseComp.Body.FECAESolicitarResult.FeDetResp[0].CbteDesde.ToString();
                venta.Comprobante = comprobante;
                _context.Ventas.Add(venta);
                _context.Comprobantes.Add(comprobante);
                _context.SaveChanges();
                return new CommandResult()
                {
                    IdVenta = venta.Id,
                    LineaDeVenta = request.LineaDeVentas,
                    Total = venta.Monto,
                    Comprobante = comprobante,
                    FeCabResp = responseComp.Body.FECAESolicitarResult.FeCabResp,
                    FeDetResp = responseComp.Body.FECAESolicitarResult.FeDetResp
                };
            }
            public async Task<List<LineaDeVenta>> MapLineaDeVentas(List<LineaDeVentaDTO> lineaDeVentaDtos,int idSucursal)
            {
                var lineaDeVentas = new List<LineaDeVenta>();
                foreach (var ldv in lineaDeVentaDtos)
                {
                    try
                    {
                        var stock = await _context.Stocks
                            .Include(t => t.Talle)
                            .Include(c=> c.Color)
                            .Include(p => p.Producto)
                            .FirstOrDefaultAsync(s =>
                                s.Color.Id == ldv.IdColor && s.Talle.Id == ldv.IdTalle &&
                                s.Producto.Codigo.Equals(ldv.CodigoProducto) &&
                                s.Sucursal.Id == idSucursal);
                        stock.DisminuirCantidad(ldv.Cantidad);
                        var ldvInstance = new LineaDeVenta()
                        {
                            Cantidad = ldv.Cantidad,
                            Stock = stock,
                            Precio = ldv.Precio
                        };
                        lineaDeVentas.Add(ldvInstance);
                    }
                    catch (Exception e)
                    {

                    }
                    
                }
                return lineaDeVentas;
            }
           
        }

        
    }
}
