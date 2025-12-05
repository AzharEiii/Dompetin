using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dompetin.Controller_Dompet
{
    internal class Connection
    {

            public static MySqlCommand cmd; // Menjalankan perintah SQL
            public static DataSet ds;       // Menyimpan hasil query
            public static MySqlDataAdapter da; // Penghubung antara DB dan DataTable

            // === STATIC FUNCTION UNTUK KONEKSI KE DATABASE ===
            public static MySqlConnection GetConn()
            {
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = "server=localhost;user=root;database=dompetin"; // sesuaikan dengan DB kamu

                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed Connection: " + ex.Message);
                }

                return conn;
            }
        }
    }





