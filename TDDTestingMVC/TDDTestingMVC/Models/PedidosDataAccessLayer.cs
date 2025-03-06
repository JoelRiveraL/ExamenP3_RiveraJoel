using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using TDDTestingMVC.Data;

namespace TDDTestingMVC.Models
{
    public class PedidoDataAccessLayer
    {
        private readonly string connection = "Data Source=ALESSO; Initial Catalog=dbproductos; User ID=sa; Password=4812; TrustServerCertificate=True;";

        public List<Pedido> GetAllPedidos()
        {
            List<Pedido> listaPedidos = new List<Pedido>();
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("pedido_SelectAll", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Pedido pedido = new Pedido
                    {
                        PedidoID = Convert.ToInt32(rdr["PedidoID"]),
                        ClienteID = Convert.ToInt32(rdr["ClienteID"]),
                        FechaPedido = Convert.ToDateTime(rdr["FechaPedido"]),
                        Monto = Convert.ToDecimal(rdr["Monto"]),
                        Estado = rdr["Estado"].ToString()
                    };
                    listaPedidos.Add(pedido);
                }
                con.Close();
            }
            return listaPedidos;
        }

        public void AddPedido(Pedido pedido)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("pedido_Insert", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClienteID", pedido.ClienteID);
                cmd.Parameters.AddWithValue("@Monto", pedido.Monto);
                cmd.Parameters.AddWithValue("@Estado", pedido.Estado);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdatePedido(Pedido pedido)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                if (pedido.ClienteID <= 0 || pedido.Monto <= 0 || string.IsNullOrWhiteSpace(pedido.Estado) || pedido.Estado.Length < 2)
                {
                    con.Close();
                }

                else
                { 
                    SqlCommand cmd = new SqlCommand("pedido_Update", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PedidoID", pedido.PedidoID);
                    cmd.Parameters.AddWithValue("@ClienteID", pedido.ClienteID);
                    cmd.Parameters.AddWithValue("@Monto", pedido.Monto);
                    cmd.Parameters.AddWithValue("@Estado", pedido.Estado);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
        }

        public void DeletePedido(int pedidoID)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("pedido_Delete", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PedidoID", pedidoID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public Pedido GetPedidoById(int pedidoID)
        {
            List<Pedido> listaPedidos = GetAllPedidos();

            Pedido pedido = listaPedidos.FirstOrDefault(p => p.PedidoID == pedidoID);

            return pedido;
        }

    }
}
