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

        private void LoadRiwayat()
        {
            using (MySqlConnection conn = new MySqlConnection("server=localhost;database=dompetin;uid=root;pwd=;"))
            {
                conn.Open();
                string query = @"SELECT 
                                 transaksi_id AS 'ID',    /* <--- TAMBAHKAN INI */
                                 DATE_FORMAT(tanggal, '%Y-%m-%d %H:%i') AS 'Tanggal',
                                 tipe AS 'Tipe',
                                 CONCAT('Rp ', FORMAT(jumlah, 0)) AS 'Jumlah',
                                 keterangan AS 'Keterangan'
                               FROM transactions
                               WHERE user_id = @id
                               ORDER BY tanggal DESC";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", userId);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvRiwayat.DataSource = dt;

                // Agar tampilan tabel rapi
                dgvRiwayat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvRiwayat.ReadOnly = true;
                dgvRiwayat.AllowUserToAddRows = false;
                dgvRiwayat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // Sekarang kolom 'ID' sudah ada dan bisa disembunyikan
                if (dgvRiwayat.Columns.Contains("ID"))
                {
                    dgvRiwayat.Columns["ID"].Visible = false;
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
    }
    
}
