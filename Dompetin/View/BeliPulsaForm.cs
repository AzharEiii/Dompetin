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
    public partial class BeliPulsaForm : Form
    {
       ValidasiController validasi = new ValidasiController();
        private int userId;
        public BeliPulsaForm(int id)
        {
            InitializeComponent();
            userId = id;
        }

        private void BeliPulsaForm_Load(object sender, EventArgs e)
        {
            LoadProviders();
            LoadProduk();
        }


        private void LoadProviders()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection("server=localhost;database=dompetin;uid=root;pwd=;"))
                {
                    conn.Open();
                    // Ambil semua merchant yang termasuk kategori 'Pulsa/Data'
                    string query = "SELECT merchant_id, nama_merchant FROM merchants WHERE kategori = 'Telekomunikasi'";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Setting ComboBox
                    cmbProvider.DataSource = dt;
                    cmbProvider.DisplayMember = "nama_merchant"; // Yang ditampilkan ke user
                    cmbProvider.ValueMember = "merchant_id";     // Nilai yang akan diambil (ID Merchant)
                    cmbProvider.SelectedIndex = -1; // Kosongkan pilihan awal
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat daftar provider: " + ex.Message);
            }
        }

        private void LoadProduk()
        {
            // Struktur data anonim untuk menyimpan Display Text dan Nilai Harga (Value)
            var produkList = new[]
            {
                new { Display = "Pulsa 10.000", Harga = 10500.00m },
                new { Display = "Pulsa 25.000", Harga = 25500.00m },
                new { Display = "Paket Data 1GB", Harga = 30000.00m },
                new { Display = "Paket Data 5GB", Harga = 75000.00m }
         };

            // Setting ComboBox Produk
            cmbProduk.DataSource = produkList;
            cmbProduk.DisplayMember = "Display"; // Teks yang dilihat user (misal: "Pulsa 10.000")
            cmbProduk.ValueMember = "Harga";     // Nilai yang akan diambil (Harga dalam decimal)
            cmbProduk.SelectedIndex = -1; // Kosongkan pilihan awal
        }

        private void btnBeli_Click(object sender, EventArgs e)
        {
            if(!validasi.ValidasiNoHp(txtNomorHp.Text))
            {
                return;
            }

            // 1. Validasi Input
            if (cmbProvider.SelectedValue == null || string.IsNullOrWhiteSpace(txtNomorHp.Text) || cmbProduk.SelectedValue == null)
            {
                MessageBox.Show("Lengkapi data provider, nomor HP, dan produk.");
                return;
            }

            int selectedMerchantId = Convert.ToInt32(cmbProvider.SelectedValue);
            string nomorHp = txtNomorHp.Text;

            // AMBIL HARGA DARI COMBOBOX
            decimal hargaYangDipilih = Convert.ToDecimal(cmbProduk.SelectedValue);

            // 2. Konfirmasi Pembayaran
            DialogResult result = MessageBox.Show($"Beli pulsa/paket data seharga Rp {hargaYangDipilih:N0} untuk {nomorHp}?",
                                                 "Konfirmasi Pembayaran", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                // Panggil fungsi proses transaksi dengan harga yang dinamis dari ComboBox
                ProsesPembayaranPulsa(selectedMerchantId, hargaYangDipilih, nomorHp);
            }
        }

        private void ProsesPembayaranPulsa(int merchantId, decimal jumlahTransaksi, string nomorHp)
        {
            using (MySqlConnection conn = new MySqlConnection("server=localhost;database=dompetin;uid=root;pwd=;"))
            {
                conn.Open();
                MySqlTransaction trans = conn.BeginTransaction(); // Gunakan transaksi DB untuk keamanan

                try
                {
                    // A. Cek Saldo User
                    string checkSaldoQuery = "SELECT saldo FROM users WHERE user_id = @id";
                    MySqlCommand cmdCheck = new MySqlCommand(checkSaldoQuery, conn, trans);
                    cmdCheck.Parameters.AddWithValue("@id", userId);
                    decimal currentSaldo = Convert.ToDecimal(cmdCheck.ExecuteScalar());

                    if (currentSaldo < jumlahTransaksi)
                    {
                        trans.Rollback();
                        MessageBox.Show("Saldo tidak cukup!");
                        return;
                    }

                    // B. Kurangi Saldo User
                    string updateSaldoQuery = "UPDATE users SET saldo = saldo - @jumlah WHERE user_id = @id";
                    MySqlCommand cmdUpdate = new MySqlCommand(updateSaldoQuery, conn, trans);
                    cmdUpdate.Parameters.AddWithValue("@jumlah", jumlahTransaksi);
                    cmdUpdate.Parameters.AddWithValue("@id", userId);
                    cmdUpdate.ExecuteNonQuery();

                    // C. Catat Transaksi Pembayaran
                    string keterangan = $"Pembelian Pulsa/Data ke {nomorHp}";
                    string insertTransQuery = @"INSERT INTO transactions (user_id, merchant_id, tipe, jumlah, keterangan)
                                        VALUES (@uid, @mid, 'Pembayaran', @jumlah, @keterangan)";
                    MySqlCommand cmdTrans = new MySqlCommand(insertTransQuery, conn, trans);
                    cmdTrans.Parameters.AddWithValue("@uid", userId);
                    cmdTrans.Parameters.AddWithValue("@mid", merchantId); // Menggunakan ID merchant
                    cmdTrans.Parameters.AddWithValue("@jumlah", jumlahTransaksi);
                    cmdTrans.Parameters.AddWithValue("@keterangan", keterangan);
                    cmdTrans.ExecuteNonQuery();

                    // D. Commit Transaksi
                    trans.Commit();
                    MessageBox.Show("Pembelian Pulsa/Paket Data Berhasil!");
                    // Anda mungkin perlu memanggil fungsi untuk refresh saldo di MainForm
                }
                catch (Exception ex)
                {
                    trans.Rollback(); // Batalkan semua perubahan jika terjadi error
                    MessageBox.Show("Transaksi gagal: " + ex.Message);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new MainForm(userId, "").Show();
            this.Hide();

        }
    }

}
