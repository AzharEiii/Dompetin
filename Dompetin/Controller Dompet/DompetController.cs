using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dompetin.Controller_Dompet
{
    internal class DompetController : Connection
    {
        public DompetController() { }

        // ==== Fungsi untuk Register ====
        public bool Register(string nama, string email, string noHp, string password)
        {
            bool status = false;

            using (MySqlConnection conn = Connection.GetConn()) // 
            {
                try
                {
                    string query = "INSERT INTO users (nama, email, no_hp, password) VALUES (@nama, @Email, @NoHp, @Password)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nama", nama);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@NoHp", noHp);
                    cmd.Parameters.AddWithValue("@Password", password);

                    cmd.ExecuteNonQuery();
                    status = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saat register: " + ex.Message);
                }
            }

            return status;

        }

        public bool Login(string usernameOrHp, string password)
        {
            bool isLogin = false;

            using (MySqlConnection conn = Connection.GetConn())
            {
                try
                {
                    string query = "SELECT * FROM users WHERE (email = @user OR no_hp = @user) AND password = @pass";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@user", usernameOrHp);
                    cmd.Parameters.AddWithValue("@pass", password);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // jika ada data cocok
                        {
                            isLogin = true;
                        }
                        else
                        {
                            MessageBox.Show("Email/No HP atau password salah!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saat login: " + ex.Message);
                }
            }

            return isLogin;
        }

        public int GetUserId(string user)
        {
            int id = 0;
            using (var conn = new MySqlConnection("server=localhost;database=dompetin;uid=root;pwd=;"))
            {
                conn.Open();
                string query = "SELECT user_id FROM users WHERE email=@user OR no_hp=@user";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", user);
                var result = cmd.ExecuteScalar();
                if (result != null)
                    id = Convert.ToInt32(result);
            }
            return id;
        }

        public string GetNamaUser(string user)
        {
            string nama = "";
            using (var conn = new MySqlConnection("server=localhost;database=dompetin;uid=root;pwd=;"))
            {
                conn.Open();
                string query = "SELECT nama FROM users WHERE email=@user OR no_hp=@user";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", user);
                var result = cmd.ExecuteScalar();
                if (result != null)
                    nama = result.ToString();
            }
            return nama;
        }
    }


}
        

