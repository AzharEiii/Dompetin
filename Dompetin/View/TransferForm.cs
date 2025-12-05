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
    public partial class TransferForm : Form
    {
        ValidasiController validasi = new ValidasiController();
        private int userId;
        public TransferForm(int id)
        {
            InitializeComponent();
            userId = id;
        }

        private void TransferForm_Load(object sender, EventArgs e)
        {

        }

        private void txtNominal_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if(!validasi.ValidasiTujuan(txtTujuan.Text))
            {
                return;
            }

            if(!validasi.ValidasiNominal(txtNominal.Text))
            {
                return;
            }

            string tujuan = txtTujuan.Text.Trim();
            if (!decimal.TryParse(txtNominal.Text, out decimal nominal) || nominal <= 0)
            {
                lblStatus.Text = "⚠️ Nominal tidak valid!";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                return;
            }

            using (MySqlConnection conn = new MySqlConnection("server=localhost;database=dompetin;uid=root;pwd=;"))
            {
                conn.Open();

                // 1️⃣ Cek apakah penerima ada
                string cekPenerima = "SELECT user_id FROM users WHERE email=@user OR no_hp=@user";
                MySqlCommand cmd1 = new MySqlCommand(cekPenerima, conn);
                cmd1.Parameters.AddWithValue("@user", tujuan);

                object penerimaObj = cmd1.ExecuteScalar();
                if (penerimaObj == null)
                {
                    lblStatus.Text = "❌ Pengguna tidak ditemukan!";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                int penerimaId = Convert.ToInt32(penerimaObj);

                // 2️⃣ Cek saldo pengirim
                string cekSaldo = "SELECT saldo FROM users WHERE user_id=@id";
                MySqlCommand cmd2 = new MySqlCommand(cekSaldo, conn);
                cmd2.Parameters.AddWithValue("@id", userId);
                decimal saldoPengirim = Convert.ToDecimal(cmd2.ExecuteScalar());

                if (saldoPengirim < nominal)
                {
                    lblStatus.Text = "❌ Saldo tidak mencukupi!";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // 3️⃣ Kurangi saldo pengirim
                string updatePengirim = "UPDATE users SET saldo = saldo - @nominal WHERE user_id=@id";
                MySqlCommand cmd3 = new MySqlCommand(updatePengirim, conn);
                cmd3.Parameters.AddWithValue("@nominal", nominal);
                cmd3.Parameters.AddWithValue("@id", userId);
                cmd3.ExecuteNonQuery();

                // 4️⃣ Tambahkan saldo penerima
                string updatePenerima = "UPDATE users SET saldo = saldo + @nominal WHERE user_id=@id";
                MySqlCommand cmd4 = new MySqlCommand(updatePenerima, conn);
                cmd4.Parameters.AddWithValue("@nominal", nominal);
                cmd4.Parameters.AddWithValue("@id", penerimaId);
                cmd4.ExecuteNonQuery();

                // 5️⃣ Catat transaksi pengirim
                string insertTransaksi1 = @"INSERT INTO transactions (user_id, merchant_id, tipe, jumlah, keterangan)
                                            VALUES (@uid, NULL, 'Transfer', @nominal, @ket)";
                MySqlCommand cmd5 = new MySqlCommand(insertTransaksi1, conn);
                cmd5.Parameters.AddWithValue("@uid", userId);
                cmd5.Parameters.AddWithValue("@nominal", nominal);
                cmd5.Parameters.AddWithValue("@ket", "Transfer ke " + tujuan);
                cmd5.ExecuteNonQuery();

                // 6️⃣ Catat transaksi penerima (opsional)
                string insertTransaksi2 = @"INSERT INTO transactions (user_id, merchant_id, tipe, jumlah, keterangan)
                                            VALUES (@uid, NULL, 'Transfer', @nominal, @ket)";
                MySqlCommand cmd6 = new MySqlCommand(insertTransaksi2, conn);
                cmd6.Parameters.AddWithValue("@uid", penerimaId);
                cmd6.Parameters.AddWithValue("@nominal", nominal);
                cmd6.Parameters.AddWithValue("@ket", "Menerima transfer dari user " + userId);
                cmd6.ExecuteNonQuery();

                lblStatus.Text = $"✅ Transfer Rp{nominal:N0} ke {tujuan} berhasil!";
                lblStatus.ForeColor = System.Drawing.Color.Green;

                txtNominal.Clear();
                txtTujuan.Clear();
            }
        
    }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new MainForm(userId, "").Show();
            this.Hide();

        }
    }
}
