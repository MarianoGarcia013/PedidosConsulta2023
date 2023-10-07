﻿using PedidosConsulta2023.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosConsulta2023.Datos.Interfaz
{
    internal interface IPedidos
    {
        DataTable ConsultarDB();
        List<Pedido> ObtenerPedidosPorFiltro(int cliente, DateTime desde, DateTime hasta);

    }
}
