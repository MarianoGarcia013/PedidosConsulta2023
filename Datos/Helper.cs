using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosConsulta2023.Datos
{
    internal class Helper
    {
        private SqlConnection cnn;
        private static Helper instancia;

        private Helper()
        {
            cnn = new SqlConnection(@"Data Source=DESKTOP-SFDA7AL\MSSQLSERVER2;Initial Catalog=db_pedidos;Integrated Security=True");

        }
        public static Helper ObtenerInstancia()
        {
            if (instancia == null)
                instancia = new Helper();
            return instancia;
        }

        public DataTable ConsultarDB(string NombreSP)
        {
            DataTable tabla = new DataTable();
            cnn.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = cnn;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = NombreSP;

            tabla.Load(comando.ExecuteReader());
            cnn.Close();
            return tabla;
        }
        public DataTable ConsultaSQL(string spNombre, List<Parametro> values, CommandType cmdType = CommandType.StoredProcedure)
        {
            DataTable tabla = new DataTable();

            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(spNombre, cnn);
                cmd.CommandType = cmdType;
                if (values != null)
                {
                    foreach (Parametro oParametro in values)
                    {
                        cmd.Parameters.AddWithValue(oParametro.Clave, oParametro.Valor);
                    }
                }
                tabla.Load(cmd.ExecuteReader());
            }
            catch (SqlException ex)
            {
                // Manejo de la excepción, puedes mostrar un mensaje o registrar el error en un archivo de registro
                Console.WriteLine("Error en ConsultaSQL: " + ex.Message);
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }

            return tabla;
        }


        public DataTable ConsultarPedidos(int cliente, DateTime fechaDesde, DateTime fechaHasta)
        {
            DataTable tabla = new DataTable();
            List<Parametro> parametros = new List<Parametro>();

            try
            {
                cnn.Open();

                // Configura los parámetros
                parametros.Add(new Parametro("@cliente", cliente));
                parametros.Add(new Parametro("@fecha_desde", fechaDesde));
                parametros.Add(new Parametro("@fecha_hasta", fechaHasta));

                // Llama al método ConsultaSQL con el nombre del procedimiento almacenado y los parámetros
                tabla = ConsultaSQL("SP_CONSULTAR_PEDIDOS", parametros);
            }
            catch (SqlException ex)
            {
                // Manejo de la excepción, puedes mostrar un mensaje o registrar el error en un archivo de registro
                Console.WriteLine("Error en ConsultarPedidos: " + ex.Message);
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }

            return tabla;
        }
    }
}
