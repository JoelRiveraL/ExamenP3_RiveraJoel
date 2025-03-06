using Microsoft.Data.SqlClient;
using TDDTestingMVC.Data;

namespace TDDTestingMVC.Models
{
    public class ClienteDataAccessLayer
    {
        // Esto se lo debe colocar en otra parte por seguridad
        string connection = "Data Source=ALESSO; Initial Catalog=dbproductos; User ID=sa; Password=4812; TrustServerCertificate=True;";

        public List<Cliente> getAllClientes()
        {
            List<Cliente> listaCliente = new List<Cliente>();
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("cliente_SelectAll", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Cliente cliente = new Cliente();
                    cliente.Codigo = Convert.ToInt32(rdr["Codigo"]);
                    cliente.Cedula = rdr["Cedula"].ToString();
                    cliente.Nombres = rdr["Nombres"].ToString();
                    cliente.Apellidos = rdr["Apellidos"].ToString();
                    cliente.FechaNacimiento = Convert.ToDateTime(rdr["FechaNacimiento"]);
                    cliente.Mail = rdr["Mail"].ToString();
                    cliente.Telefono = rdr["Telefono"].ToString();
                    cliente.Direccion = rdr["Direccion"].ToString();
                    cliente.Estado = Convert.ToBoolean(rdr["Estado"]);
                    listaCliente.Add(cliente);
                }
                con.Close();
            }
            return listaCliente;
        }

        public void AddClientes(Cliente Cliente)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("cliente_Insert", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Cedula", Cliente.Cedula);
                cmd.Parameters.AddWithValue("@Apellidos", Cliente.Apellidos);
                cmd.Parameters.AddWithValue("@Nombres", Cliente.Nombres);
                cmd.Parameters.AddWithValue("@FechaNacimiento", Cliente.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Mail", Cliente.Mail);
                cmd.Parameters.AddWithValue("@Telefono", Cliente.Telefono);
                cmd.Parameters.AddWithValue("@Direccion", Cliente.Direccion);
                cmd.Parameters.AddWithValue("@Estado", Cliente.Estado);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


        public void UpdateCliente(Cliente Cliente)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("cliente_Update", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Codigo", Cliente.Codigo);
                cmd.Parameters.AddWithValue("@Cedula", Cliente.Cedula);
                cmd.Parameters.AddWithValue("@Apellidos", Cliente.Apellidos);
                cmd.Parameters.AddWithValue("@Nombres", Cliente.Nombres);
                cmd.Parameters.AddWithValue("@FechaNacimiento", Cliente.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Mail", Cliente.Mail);
                cmd.Parameters.AddWithValue("@Telefono", Cliente.Telefono);
                cmd.Parameters.AddWithValue("@Direccion", Cliente.Direccion);
                cmd.Parameters.AddWithValue("@Estado", Cliente.Estado);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteCliente(int Codigo)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("cliente_Delete", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Codigo", Codigo);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public bool VerificarClienteExis(string cedula)
        {
            bool existe = false;

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("cliente_CheckCedula", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Cedula", cedula);

                con.Open();

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    existe = Convert.ToInt32(result) == 1;
                }

                con.Close();
            }

            return existe;
        }

        public bool VerificarTelefonoExis(string telefono)
        {
            bool existe = false;

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("cliente_CheckTelefono", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Telefono", telefono);

                con.Open();

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    existe = Convert.ToInt32(result) == 1;
                }

                con.Close();
            }

            return existe;
        }

        public bool ValidarCedulaEc(string cedula)
        {
            if (string.IsNullOrEmpty(cedula) || cedula.Length != 10)
            {
                return false;
            }

            int total = 0;
            int longitud = cedula.Length;
            int longcheck = longitud - 1;

            for (int i = 0; i < longcheck; i++)
            {
                int aux = (cedula[i] - '0') * (i % 2 == 0 ? 2 : 1);
                if (aux > 9)
                {
                    aux -= 9;
                }
                total += aux;
            }

            total = total % 10 != 0 ? 10 - total % 10 : 0;

            return cedula[longitud - 1] - '0' == total;
        }
    }
}
