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
    public partial class RiwayatForm : Form
    {
        private int userId;
        public RiwayatForm(int id)
        {
            InitializeComponent();
            this.userId = id;
        }

        private void RiwayatForm_Load(object sender, EventArgs e)
        {
            
            LoadRiwayat();
        }

        private void LoadRiwayat(string kataKunci = null) // Tambahkan parameter opsional
        {
            using (MySqlConnection conn = new MySqlConnection("server=localhost;database=dompetin;uid=root;pwd=;"))
            {
                conn.Open();

                // 1. Definisikan Query Dasar (Termasuk JOIN ke merchants)
                // Kita menggunakan JOIN agar bisa mencari berdasarkan nama merchant juga
                string query = @"SELECT 
                                 t.transaksi_id AS 'ID',    
                                 DATE_FORMAT(t.tanggal, '%Y-%m-%d %H:%i') AS 'Tanggal',
                                 t.tipe AS 'Tipe',
                                 CONCAT('Rp ', FORMAT(t.jumlah, 0)) AS 'Jumlah',
                                 t.keterangan AS 'Keterangan',
                                 m.nama_merchant AS 'Merchant'
                           FROM transactions t
                           LEFT JOIN merchants m ON t.merchant_id = m.merchant_id
                           WHERE t.user_id = @id";

                // 2. Tambahkan Kondisi Pencarian Jika Ada Kata Kunci
                if (!string.IsNullOrWhiteSpace(kataKunci))
                {
                    query += @" AND (t.keterangan LIKE @search 
                         OR t.tipe LIKE @search 
                         OR m.nama_merchant LIKE @search)"; // Mencari di 3 kolom
                }

                query += " ORDER BY t.tanggal DESC"; // Urutkan hasil

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", userId);

                // 3. Tambahkan Parameter @search
                if (!string.IsNullOrWhiteSpace(kataKunci))
                {
                    cmd.Parameters.AddWithValue("@search", $"%{kataKunci}%");
                }

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvRiwayat.DataSource = dt;

                // Agar tampilan tabel rapi
                dgvRiwayat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvRiwayat.ReadOnly = true;
                dgvRiwayat.AllowUserToAddRows = false;
                dgvRiwayat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // Sembunyikan kolom ID (untuk kebutuhan penghapusan)
                if (dgvRiwayat.Columns.Contains("ID"))
                {
                    dgvRiwayat.Columns["ID"].Visible = false;
                }
                // Sembunyikan kolom Merchant (kolom ini hanya untuk pencarian)
                if (dgvRiwayat.Columns.Contains("Merchant"))
                {
                    dgvRiwayat.Columns["Merchant"].Visible = false;
                }
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            new MainForm(userId, "").Show();
            this.Hide();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvRiwayat.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih transaksi yang ingin dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Mendapatkan ID dari baris yang dipilih (Kolom 'ID' harus sudah ditambahkan di LoadRiwayat)
                int transaksiId = Convert.ToInt32(dgvRiwayat.SelectedRows[0].Cells["ID"].Value);

                DialogResult konfirmasi = MessageBox.Show(
                    "Apakah kamu yakin ingin menghapus transaksi ini?",
                    "Konfirmasi Hapus",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (konfirmasi == DialogResult.Yes)
                {
                    using (MySqlConnection conn = new MySqlConnection("server=localhost;database=dompetin;uid=root;pwd=;"))
                    {
                        conn.Open();
                        // Pastikan nama kolom ID di database adalah transaksi_id
                        string deleteQuery = "DELETE FROM transactions WHERE transaksi_id = @id AND user_id = @user";
                        MySqlCommand cmd = new MySqlCommand(deleteQuery, conn);
                        cmd.Parameters.AddWithValue("@id", transaksiId);
                        cmd.Parameters.AddWithValue("@user", userId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Transaksi berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadRiwayat(); // Muat ulang data setelah penghapusan
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat menghapus: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHapusSemua_Click(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show(
                    "Apakah kamu yakin ingin menghapus SEMUA riwayat transaksi?",
                    "Konfirmasi Hapus Semua",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

            if (konfirmasi == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection("server=localhost;database=dompetin;uid=root;pwd=;"))
                    {
                        conn.Open();
                        string deleteAllQuery = "DELETE FROM transactions WHERE user_id = @id";
                        MySqlCommand cmd = new MySqlCommand(deleteAllQuery, conn);
                        cmd.Parameters.AddWithValue("@id", userId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Semua riwayat transaksi berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadRiwayat();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat menghapus semua data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            LoadRiwayat(txtCari.Text);
        }
    }
    
}
