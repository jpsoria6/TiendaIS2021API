using AFIPServiceReference;
using LaTienda.Model;
using LoginServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.API.Services
{
    public interface IAfipService
    {
        Task<FECAESolicitarResponse> AutorizarCompAFIPAsync(string GUID, Venta venta, long cuit, Cliente cliente);
    }
}
