using System;
using System.Collections.Generic;
using System.Text;

namespace LaTienda.Model
{
    interface IRepositorio
    {
        Stock GetStock(int idColor,int idTalle,string codigoProducto,int idSucursal);
        bool GetUsuario(int legajo);
    }
}
