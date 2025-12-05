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
    public partial class TopUpForm : Form
    {
        ValidasiController validasi = new ValidasiController();
        private int userId;
        public TopUpForm(int id)
        {
            InitializeComponent();
            userId = id;
        }

        private void TopUpForm_Load(object sender, EventArgs e)
        {

        }

        private void btnTopUp_Click(object sender, EventArgs e)
        {
            if(validasi.ValidasiNominal(txtNominal.Text) == false)
            {
                return;
            }
            if (decimal.TryParse(txtNominal.Text, out decimal nominal) && nominal > 0)
            {
                using (MySqlConnection conn = new MySqlConnection("server=localhost;database=dompetin;uid=root;pwd=;"))
                {
                    conn.Open();

                    // 1️⃣ Update saldo user
                    string updateSaldo = "UPDATE users SET saldo = saldo + @jumlah WHERE user_id = @id";
                    MySqlCommand cmd1 = new MySqlCommand(updateSaldo, conn);
                    cmd1.Parameters.AddWithValue("@jumlah", nominal);
                    cmd1.Parameters.AddWithValue("@id", userId);
                    cmd1.ExecuteNonQuery();

                    // 2️⃣ Catat ke tabel transactions
                    string insertTransaksi = @"INSERT INTO transactions (user_id, merchant_id, tipe, jumlah, keterangan) 
                                               VALUES (@id, NULL, 'TopUp', @jumlah, 'Top up saldo')";
                    MySqlCommand cmd2 = new MySqlCommand(insertTransaksi, conn);
                    cmd2.Parameters.AddWithValue("@id", userId);
                    cmd2.Parameters.AddWithValue("@jumlah", nominal);
                    cmd2.ExecuteNonQuery();

                    conn.Close();
                }

                lblStatus.Text = "✅ Top Up berhasil: Rp " + nominal.ToString("N0");
                lblStatus.ForeColor = System.Drawing.Color.Green;
                txtNominal.Clear();
            }
            else
            {
                lblStatus.Text = "⚠️ Masukkan nominal yang valid!";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }

        }

        private void txtTopUp_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new MainForm(userId, "").Show();
            this.Hide();
        }
    }
}
