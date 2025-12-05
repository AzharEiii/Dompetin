CREATE DATABASE dompetin;
USE dompetin;

CREATE TABLE users (
    user_id INT AUTO_INCREMENT PRIMARY KEY,
    nama VARCHAR(100) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    `password` VARCHAR(255) NOT NULL,
    no_hp VARCHAR(20),
    saldo DECIMAL(15,2) DEFAULT 0,
    foto_profil VARCHAR(255),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE merchants (
    merchant_id INT AUTO_INCREMENT PRIMARY KEY,
    nama_merchant VARCHAR(100) NOT NULL,
    kategori VARCHAR(50),
    deskripsi TEXT
);

CREATE TABLE transactions (
    transaksi_id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT NOT NULL,
    merchant_id INT NULL,
    tipe ENUM('TopUp','Transfer','Pembayaran') NOT NULL,
    jumlah DECIMAL(15,2) NOT NULL,
    keterangan VARCHAR(255),
    tanggal TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES users(user_id),
    FOREIGN KEY (merchant_id) REFERENCES merchants(merchant_id)
);

-- Jika ingin ubah tipe kolom foto_profil jadi LONGBLOB
ALTER TABLE users MODIFY foto_profil LONGBLOB;

INSERT INTO merchants (nama_merchant, kategori, deskripsi) VALUES
('Telkomsel', 'Telekomunikasi', 'Layanan pulsa dan paket data Telkomsel'),
('XL Axiata', 'Telekomunikasi', 'Layanan pulsa dan paket data XL');
