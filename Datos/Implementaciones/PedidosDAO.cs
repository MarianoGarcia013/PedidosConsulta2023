using PedidosConsulta2023.Datos.Interfaz;
using PedidosConsulta2023.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosConsulta2023.Datos.Implementaciones
{
    internal class PedidosDAO : IPedidos
    {
        public DataTable ConsultarDB()
        {
            return Helper.ObtenerInstancia().ConsultarDB("SP_CONSULTAR_CLIENTES");
        }

        public List<Pedido> ObtenerPedidosPorFiltro(int cliente, DateTime desde, DateTime hasta)
        {
            throw new NotImplementedException();
        }
    }
}
