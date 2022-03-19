using AFIPServiceReference;
using LaTienda.Model;
using LoginServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.API.Services
{
    public class ClientServiceAfip : IAfipService
    {
        private readonly LoginServiceClient _loginClient;
        private readonly ServiceSoapClient _serviceSoapAfip;
        public ClientServiceAfip(LoginServiceClient loginServiceClient, ServiceSoapClient serviceSoap)
        {
            _loginClient = loginServiceClient;
            _serviceSoapAfip = serviceSoap;
        }
        public async Task<FECAESolicitarResponse> AutorizarCompAFIPAsync(string GUID, Venta venta, long cuit, Cliente cliente)
        {
            var response = GetSolicitarAutorizacion(GUID);

            if (string.IsNullOrEmpty(response.SolicitarAutorizacionResult.Error))
            {
                var FecaAuth = new FEAuthRequest()
                {
                    Cuit = response.SolicitarAutorizacionResult.Cuit,
                    Sign = response.SolicitarAutorizacionResult.Sign,
                    Token = response.SolicitarAutorizacionResult.Token
                };
                var ultimoComp = await CheckUltimoComprobante(FecaAuth);
                var FecaeRequest = new FECAERequest();
                FecaeRequest.FeCabReq = GetFECabRequest(cliente);
                FecaeRequest.FeDetReq = new FECAEDetRequest[1];
                FecaeRequest.FeDetReq[0] = GetFECAEDetRequest(cuit, venta, ultimoComp);
                var body = new FECAESolicitarRequestBody(FecaAuth, FecaeRequest);
                FECAESolicitarRequest fECAESolicitar = new FECAESolicitarRequest();
                fECAESolicitar.Body = body;

                var resFECAE = await _serviceSoapAfip.FECAESolicitarAsync(FecaAuth, FecaeRequest);


                return resFECAE;
            }
            return null;
        }
        private async Task<int> CheckUltimoComprobante(FEAuthRequest authRequest)
        {
            ServiceSoapClient soapClient = new ServiceSoapClient(ServiceSoapClient.EndpointConfiguration.ServiceSoap);
            var response = await soapClient.FECompUltimoAutorizadoAsync(authRequest, 13, 1);
            return response.Body.FECompUltimoAutorizadoResult.CbteNro;
        }
        private SolicitarAutorizacionResponse GetSolicitarAutorizacion(string GUID)
        {
            var requestSoap = new SolicitarAutorizacionRequest
            {
                codigo = GUID
            };
            var response = _loginClient.SolicitarAutorizacion(requestSoap);
            return response;
        }
        private FECAECabRequest GetFECabRequest(Cliente cliente)
        {
            var FeCabReq = new FECAECabRequest();
            FeCabReq.CantReg = 1;
            FeCabReq.CbteTipo = MapTipoComprobante(cliente.GetTipoComprobante());
            FeCabReq.PtoVta = 13;
            return FeCabReq;
        }
        private int MapTipoComprobante(TipoComprobante comprobante)
        {
            if (comprobante == TipoComprobante.FacturaA)
            {
                return 1;
            }
            else
            {
                return 6;
            }
        }
        private FECAEDetRequest GetFECAEDetRequest(long cuit, Venta venta, int ultComp)
        {
            var fecaeDetRequest = new FECAEDetRequest()
            {
                Concepto = 1,
                DocTipo = 80,
                DocNro = cuit,
                CbteDesde = ultComp + 1,
                CbteHasta = ultComp + 1,
                CbteFch = DateTime.Now.Year.ToString() + GetMonthDayFormat(DateTime.Now.Month) + GetMonthDayFormat(DateTime.Now.Day),
                ImpTotConc = venta.Monto - venta.LineaDeVentas.Sum(l => l.Stock.Producto.Iva),
                ImpNeto = venta.Cliente.CondicionTributaria == CondicionTributaria.Monotributo ? 0 : venta.LineaDeVentas.Sum(l => l.Stock.Producto.NetoGravado),
                ImpOpEx = venta.Cliente.CondicionTributaria == CondicionTributaria.Exento ? venta.LineaDeVentas.Sum(l => l.Stock.Producto.NetoGravado) : 0,
                ImpIVA = venta.Cliente.CondicionTributaria == CondicionTributaria.ResponsableInscripto ? venta.LineaDeVentas.Sum(l => l.Stock.Producto.Iva) : 0,
                ImpTrib = 0,
                MonId = "PES",
                MonCotiz = 1,
            };

            fecaeDetRequest.ImpTotal = fecaeDetRequest.ImpTotConc + fecaeDetRequest.ImpNeto + fecaeDetRequest.ImpOpEx + fecaeDetRequest.ImpTrib + fecaeDetRequest.ImpIVA;
            return fecaeDetRequest;
        }
        private string GetMonthDayFormat(int number)
        {
            if (number < 10)
            {
                return "0" + number.ToString();
            }
            return number.ToString();
        }
    }
}
