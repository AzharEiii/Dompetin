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
    public partial class MainForm : Form
    {
        private int userId;
        private string namaUser;

        public MainForm(int id, string nama)
        {
            InitializeComponent();
            userId = id;
            namaUser = nama;
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            lblNama.Text = "Halo, " + namaUser + " 👋";

            using (MySqlConnection conn = new MySqlConnection("server=localhost;database=dompetin;uid=root;pwd=;"))
            {
                conn.Open();

                string query = "SELECT saldo FROM users WHERE user_id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", userId);

                var saldo = cmd.ExecuteScalar();

                if (saldo != null)
                {
                    lblSaldo.Text = "Saldo kamu: Rp " + Convert.ToDecimal(saldo).ToString("N0");
                }
                else
                {
                    lblSaldo.Text = "Saldo kamu: Rp 0";
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnTopUp_Click(object sender, EventArgs e)
        {
            TopUpForm topup = new TopUpForm(userId);
            topup.ShowDialog();

            // Setelah topup selesai, refresh saldo
            using (MySqlConnection conn = new MySqlConnection("server=localhost;database=dompetin;uid=root;pwd=;"))
            {
                conn.Open();
                string query = "SELECT saldo FROM users WHERE user_id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", userId);
                var saldo = cmd.ExecuteScalar();
                lblSaldo.Text = "Saldo kamu: Rp " + Convert.ToDecimal(saldo).ToString("N0");
            }

            this.Hide();


        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProfilForm pf = new ProfilForm(userId);
            this.Hide();
            pf.ShowDialog();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            TransferForm tf = new TransferForm(userId);
            tf.ShowDialog();

            // Refresh saldo setelah transfer
            using (MySqlConnection conn = new MySqlConnection("server=localhost;database=dompetin;uid=root;pwd=;"))
            {
                conn.Open();
                string query = "SELECT saldo FROM users WHERE user_id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", userId);
                var saldo = cmd.ExecuteScalar();
                lblSaldo.Text = "Saldo kamu: Rp " + Convert.ToDecimal(saldo).ToString("N0");
            }

            this.Hide();
        }

        private void btnRiwayat_Click(object sender, EventArgs e)
        {
            RiwayatForm rf = new RiwayatForm(userId);
            this.Hide();
            rf.ShowDialog();

        }

        private void btn_Click(object sender, EventArgs e)
        {
            BeliPulsaForm formPulsa = new BeliPulsaForm(userId); // <--- ID USER HARUS DIKIRIM

            // Tampilkan form sebagai dialog agar MainForm menunggu dan bisa di-refresh nanti
            formPulsa.ShowDialog();

            // Panggil fungsi refresh saldo setelah form BeliPulsaForm ditutup
            RefreshSaldo();
        }

        private void RefreshSaldo()
        {
            using (MySqlConnection conn = new MySqlConnection("server=localhost;database=dompetin;uid=root;pwd=;"))
            {
                conn.Open();
                string query = "SELECT saldo FROM users WHERE user_id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", userId);
                var saldo = cmd.ExecuteScalar();

                if (saldo != null)
                {
                    lblSaldo.Text = "Saldo kamu: Rp " + Convert.ToDecimal(saldo).ToString("N0");
                }
            }
        }
    }
}
