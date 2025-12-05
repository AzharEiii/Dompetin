using Dompetin.Controller_Dompet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dompetin
{
    public partial class Form1 : Form
    {
        DompetController dompet = new DompetController();
        ValidasiController validasi = new ValidasiController();
        public Form1()
        {
            dompet = new DompetController();
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if(!validasi.ValidasiLogin(txt_uname.Text, txt_pass.Text))
            {
                return;
            }

            string user = txt_uname.Text.Trim();
            string pass = txt_pass.Text.Trim();

            bool sukses = dompet.Login(user, pass);

            if (sukses)
            {
                MessageBox.Show("Login berhasil!");

                // Ambil data user dari database
                int userId = dompet.GetUserId(user);
                string namaUser = dompet.GetNamaUser(user);

                // Kirim ke MainForm
                Dompetin.View.MainForm mainForm = new Dompetin.View.MainForm(userId, namaUser);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Login gagal, periksa username dan password!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Dompetin.View.Sigin().Show();
            this.Hide();
        }
    }
}
