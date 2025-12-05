using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dompetin.Controller_Dompet
{
    internal class ValidasiController
    {

        public bool ValidasiLogin(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Username / No HP tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Password tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // ✅ VALIDASI NOMINAL (untuk TopUp / Transfer)
        public bool ValidasiNominal(string nominal, decimal min = 20000)
        {
            if (string.IsNullOrWhiteSpace(nominal))
            {
                MessageBox.Show("Nominal tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(nominal, out decimal nilai) || nilai <= 0)
            {
                MessageBox.Show("Nominal harus berupa angka positif!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (nilai < min)
            {
                MessageBox.Show($"Nominal minimal adalah Rp {min:N0}!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // ✅ VALIDASI TRANSFER TUJUAN
        public bool ValidasiTujuan(string tujuan)
        {
            if (string.IsNullOrWhiteSpace(tujuan))
            {
                MessageBox.Show("Masukkan email atau nomor HP penerima!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // cek format email (opsional)
            if (tujuan.Contains("@") && !Regex.IsMatch(tujuan, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Format email penerima tidak valid!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // cek format no HP (opsional)
            if (Regex.IsMatch(tujuan, @"^[0-9]+$") && (tujuan.Length < 8 || tujuan.Length > 15))
            {
                MessageBox.Show("Nomor HP penerima tidak valid!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // ✅ VALIDASI NAMA (untuk Profil)
        public bool ValidasiNama(string nama)
        {
            if (string.IsNullOrWhiteSpace(nama))
            {
                MessageBox.Show("Nama tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (nama.Length < 2)
            {
                MessageBox.Show("Nama minimal 2 karakter!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Regex.IsMatch(nama, @"^[a-zA-Z\s]+$"))
            {
                MessageBox.Show("Nama hanya boleh berisi huruf dan spasi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // ✅ VALIDASI NO HP (untuk Profil)
        public bool ValidasiNoHp(string noHp)
        {
            if (string.IsNullOrWhiteSpace(noHp))
            {
                MessageBox.Show("Nomor HP tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Regex.IsMatch(noHp, @"^[0-9]+$"))
            {
                MessageBox.Show("Nomor HP hanya boleh berisi angka!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (noHp.Length < 9 || noHp.Length > 15)
            {
                MessageBox.Show("Nomor HP tidak valid (panjang 9-15 digit)!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // ✅ VALIDASI REGISTER / SIGN UP
        public bool ValidasiRegister(string nama, string email, string password, string noHp)
        {
            // Nama wajib diisi
            if (string.IsNullOrWhiteSpace(nama))
            {
                MessageBox.Show("Nama tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (nama.Length < 2)
            {
                MessageBox.Show("Nama minimal 2 karakter!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Email wajib diisi dan format valid
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Email tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Format email tidak valid!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Password wajib diisi dan minimal 6 karakter
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Password tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Password minimal 6 karakter!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Nomor HP wajib diisi dan harus angka
            if (string.IsNullOrWhiteSpace(noHp))
            {
                MessageBox.Show("Nomor HP tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(noHp, @"^[0-9]+$"))
            {
                MessageBox.Show("Nomor HP hanya boleh berisi angka!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (noHp.Length < 9 || noHp.Length > 15)
            {
                MessageBox.Show("Nomor HP harus antara 9 - 15 digit!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        public bool ValidasiEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Email tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Format email tidak valid!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

    }
}
