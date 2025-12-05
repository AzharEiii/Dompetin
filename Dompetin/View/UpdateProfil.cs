using Dompetin.Controller_Dompet;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dompetin.View
{
    public partial class UpdateProfil : Form
    {
        ValidasiController validasi = new ValidasiController();
        private int userId;
        public UpdateProfil(int id)
        {
            InitializeComponent();
            userId = id;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string namaBaru = txtnama.Text.Trim();
            string emailBaru = txtemail.Text.Trim();

            // ✅ Panggil validasi dari file terpisah

            if (!validasi.ValidasiNama(namaBaru))
                return;

            if (!validasi.ValidasiEmail(emailBaru))
                return;

            using (MySqlConnection conn = new MySqlConnection("server=localhost;database=dompetin;uid=root;pwd=;"))
            {
                conn.Open();

                // Pastikan email tidak dipakai user lain
                string cekQuery = "SELECT COUNT(*) FROM users WHERE email = @em AND user_id != @id";
                MySqlCommand cek = new MySqlCommand(cekQuery, conn);
                cek.Parameters.AddWithValue("@em", emailBaru);
                cek.Parameters.AddWithValue("@id", userId);
                int count = Convert.ToInt32(cek.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Email ini sudah digunakan oleh pengguna lain!", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Update ke DB
                string updateQuery = "UPDATE users SET nama = @nama, email = @em WHERE user_id = @id";
                MySqlCommand cmd = new MySqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@nama", namaBaru);
                cmd.Parameters.AddWithValue("@em", emailBaru);
                cmd.Parameters.AddWithValue("@id", userId);
                cmd.ExecuteNonQuery();

                conn.Close();
            }

            MessageBox.Show("Profil berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadProfil();
        }

        private void LoadProfil()
        {
            using (MySqlConnection conn = new MySqlConnection("server=localhost;database=dompetin;uid=root;pwd=;"))
            {
                conn.Open();

                string query = "SELECT nama, email FROM users WHERE user_id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", userId);
                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtnama.Text = dr["nama"].ToString();
                    txtemail.Text = dr["email"].ToString();
                }

                dr.Close();
                conn.Close();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new ProfilForm(userId).Show();
            this.Close();
        }
    }
}
