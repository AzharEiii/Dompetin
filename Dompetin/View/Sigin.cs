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
    public partial class Sigin : Form
    {
        DompetController dompet = new DompetController();
        public Sigin()
        {
            InitializeComponent();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

        private void btndaftar_Click(object sender, EventArgs e)
        {
            ValidasiController validasi = new ValidasiController();

            // Ambil data dari input
            string nama = txtnama.Text.Trim();
            string email = txtemail.Text.Trim();
            string pass = txtpass.Text.Trim();
            string noHp = txtnohp.Text.Trim();

            // Jalankan validasi
            if (!validasi.ValidasiRegister(nama, email, pass, noHp))
                return;

            // Kalau validasi lolos, lanjut simpan ke database
            using (MySqlConnection conn = new MySqlConnection("server=localhost;database=dompetin;uid=root;pwd=;"))
            {
                conn.Open();

                // Cek apakah email sudah terdaftar
                string cekQuery = "SELECT COUNT(*) FROM users WHERE email = @em";
                MySqlCommand cek = new MySqlCommand(cekQuery, conn);
                cek.Parameters.AddWithValue("@em", email);
                int count = Convert.ToInt32(cek.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Email sudah digunakan, silakan gunakan email lain!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Simpan ke database
                string query = "INSERT INTO users (nama, email, password, no_hp, saldo) VALUES (@nama, @em, @pw, @hp, 0)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nama", nama);
                cmd.Parameters.AddWithValue("@em", email);
                cmd.Parameters.AddWithValue("@pw", pass);
                cmd.Parameters.AddWithValue("@hp", noHp);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Akun berhasil dibuat! Silakan login.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                conn.Close();
            }

            // Kembali ke form login
            new Dompetin.Form1().Show();
            this.Hide();
        }

        private void Sigin_Load(object sender, EventArgs e)
        {

        }
    }
}
