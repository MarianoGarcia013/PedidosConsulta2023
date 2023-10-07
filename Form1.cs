using PedidosConsulta2023.Datos;
using PedidosConsulta2023.Datos.Implementaciones;
using PedidosConsulta2023.Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PedidosConsulta2023
{
    public partial class FormConsulta : Form
    {
        Cliente cliente;
        PedidosDAO DAO;
        public FormConsulta()
        {
            InitializeComponent();
            DAO = new PedidosDAO();
            cliente = new Cliente();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            CargarCombo();
        }
        private void CargarCombo()
        {
            DataTable tabla = DAO.ConsultarDB();
            cboClientes.DataSource = tabla;
            cboClientes.ValueMember = tabla.Columns[0].ColumnName;
            cboClientes.DisplayMember = tabla.Columns[1].ColumnName;
            cboClientes.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            List<Parametro> lst = new List<Parametro>();
            lst.Add(new Parametro("@fecha_desde", dtpDesde.Value.ToString("yyyyMMdd")));
            lst.Add(new Parametro("@fecha_hasta", dtpHasta.Value.ToString("yyyyMMdd")));
            lst.Add(new Parametro("@cliente", cboClientes.SelectedValue));

            DataTable tabla = DAO.ConsultarDB("SP_CONSULTAR_PEDIDOS", lst);
            dgvPresupuestos.Rows.Clear();
            foreach (DataRow fila in tabla.Rows)
            {
                dgvPresupuestos.Rows.Add(new object[] { fila["presupuesto_nro"].ToString(),
                                                        ((DateTime)fila["fecha"]).ToShortDateString(),
                                                        fila["cliente"].ToString(),
                                                        fila["total"].ToString(),});
            }
        }
    }
}
