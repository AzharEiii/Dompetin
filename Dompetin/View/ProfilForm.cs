using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dompetin.View
{
    public partial class ProfilForm : Form
    {

        private int userId;
        public ProfilForm(int id)
        {
            InitializeComponent();
            userId = id;
        }

        private void lblNama_Click(object sender, EventArgs e)
        {

        }

        private void ProfilForm_Load(object sender, EventArgs e)
        {
            LoadProfil();
        }

        private void LoadProfil()
        {
            using (MySqlConnection conn = new MySqlConnection("server=localhost;database=dompetin;uid=root;pwd=;"))
            {
                conn.Open();

                string query = "SELECT nama, email, no_hp, saldo, foto_profil FROM users WHERE user_id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", userId);
                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    lblNama.Text = "Nama     : " + dr["nama"].ToString();
                    lblEmail.Text = "Email    : " + dr["email"].ToString();
                    lblNoHp.Text = "No. HP   : " + dr["no_hp"].ToString();
                    lblSaldo.Text = "Saldo    : Rp " + Convert.ToDecimal(dr["saldo"]).ToString("N0");

                    // tampilkan foto dari DB (LONGBLOB)
                    if (dr["foto_profil"] != DBNull.Value)
                    {
                        byte[] imgBytes = (byte[])dr["foto_profil"];
                        using (MemoryStream ms = new MemoryStream(imgBytes))
                        {
                            picFoto.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        // kalau belum ada foto
                        picFoto.Image = null;
                    }

                    picFoto.SizeMode = PictureBoxSizeMode.Zoom;
                }

                dr.Close();
                conn.Close();
            }
        }

        private void lblUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Gambar (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
            ofd.Title = "Pilih Foto Profil";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filePath = ofd.FileName;
                byte[] imgBytes = File.ReadAllBytes(filePath); // baca gambar sebagai byte array

                using (MySqlConnection conn = new MySqlConnection("server=localhost;database=dompetin;uid=root;pwd=;"))
                {
                    conn.Open();
                    string query = "UPDATE users SET foto_profil = @foto WHERE user_id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@foto", imgBytes);
                    cmd.Parameters.AddWithValue("@id", userId);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                // tampilkan gambar langsung
                using (MemoryStream ms = new MemoryStream(imgBytes))
                {
                    picFoto.Image = Image.FromStream(ms);
                }

                picFoto.SizeMode = PictureBoxSizeMode.Zoom;
                MessageBox.Show("Foto profil berhasil diperbarui!");
            }


        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new MainForm(userId, "").Show();
            this.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
        "Apakah Anda yakin ingin logout?",
        "Konfirmasi Logout",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    );

            if (result == DialogResult.Yes)
            {
                MessageBox.Show("Kami menunggu kedatangan Anda kembali 😊", "Logout", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Kembali ke form login
                Dompetin.Form1 loginForm = new Dompetin.Form1();
                loginForm.Show();

                // Tutup form profil dan form utama
                this.Hide();
                foreach (Form form in Application.OpenForms.Cast<Form>().ToList())
                {
                    if (form is Dompetin.View.MainForm)
                    {
                        form.Close();
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateProfil updateForm = new UpdateProfil(userId);

            // Tampilkan form edit
            updateForm.ShowDialog();

            // Refresh data di form profil setelah form update ditutup (jika ada perubahan)
            LoadProfil();
        }
    }
}
