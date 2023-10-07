using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosConsulta2023.Dominio
{
    internal class Pedido
    {
        public int Id { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string DirecEntrega { get; set; }
        public string Entregado { get; set; }
        public DateTime FechaBaja { get; set; }
    }
}
