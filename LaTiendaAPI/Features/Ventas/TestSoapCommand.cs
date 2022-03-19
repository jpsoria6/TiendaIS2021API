using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LoginServiceReference;
using AFIPServiceReference;
using LaTienda.Model;

namespace LaTienda.API.Features.Ventas
{
    public class TestSoapCommand
    {
        public class Command : IRequest<CommandResult>
        {
            public string GUID { get; set; }
        }


        public class CommandResult
        {
            public int IdVenta { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {

            public CommandValidator()
            {



            }
        }

        public class Handler : IRequestHandler<Command, CommandResult>
        {

            public Handler()
            {

            }

            public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
            {

                var response = GetSolicitarAutorizacion(request.GUID);

                if (string.IsNullOrEmpty(response.SolicitarAutorizacionResult.Error))
                {
                    var FecaAuth = new FEAuthRequest()
                    {
                        Cuit = response.SolicitarAutorizacionResult.Cuit,
                        Sign = response.SolicitarAutorizacionResult.Sign,
                        Token = response.SolicitarAutorizacionResult.Token
                    };
                    var FecaeRequest = new FECAERequest();
                    FecaeRequest.FeCabReq = GetFECabRequest();
                    FecaeRequest.FeDetReq = new FECAEDetRequest[1];
                    FecaeRequest.FeDetReq[0] = GetFECAEDetRequest(1,null);
                    var body = new FECAESolicitarRequestBody(FecaAuth, FecaeRequest);
                    FECAESolicitarRequest fECAESolicitar = new FECAESolicitarRequest();
                    fECAESolicitar.Body = body;

                    ServiceSoapClient soapClient = new ServiceSoapClient(ServiceSoapClient.EndpointConfiguration.ServiceSoap);
                    
                    var resFECAE = await soapClient.FECAESolicitarAsync(FecaAuth, FecaeRequest);


                }


                return new CommandResult
                {

                };
            }

            public SolicitarAutorizacionResponse GetSolicitarAutorizacion(string GUID)
            {
                LoginServiceClient client = new LoginServiceClient();
                var requestSoap = new SolicitarAutorizacionRequest
                {
                    codigo = GUID
                };
                var response = client.SolicitarAutorizacion(requestSoap);
                return response;
            }
            public FECAECabRequest GetFECabRequest()
            {
                var FeCabReq = new FECAECabRequest();
                FeCabReq.CantReg = 1;
                FeCabReq.CbteTipo = 1;
                FeCabReq.PtoVta = 13;
                return FeCabReq;
            }
            public FECAEDetRequest GetFECAEDetRequest(int cuit,Venta venta)
            {
                var fecaeDetRequest = new FECAEDetRequest()
                {
                    Concepto = 1,
                    DocTipo = 80,
                    DocNro = cuit,
                    CbteDesde = 1,
                    CbteHasta = 1,
                    ImpTotal = venta.Monto,
                    ImpTotConc = venta.Monto - venta.LineaDeVentas.Sum(l => l.Stock.Producto.Iva),
                    ImpNeto = venta.LineaDeVentas.Sum(l => l.Stock.Producto.NetoGravado),
                    ImpOpEx = venta.Cliente.CondicionTributaria == CondicionTributaria.Exento ? venta.LineaDeVentas.Sum(l => l.Stock.Producto.NetoGravado) : 0,
                    ImpIVA = venta.Cliente.CondicionTributaria == CondicionTributaria.ResponsableInscripto ? venta.LineaDeVentas.Sum(l => l.Stock.Producto.Iva) : 0,
                    //ImpTrib
                    MonId = "PES",
                    MonCotiz = 1,
                };
                return fecaeDetRequest;
            }
        }
    }
}
