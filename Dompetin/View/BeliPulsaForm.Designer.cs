namespace Dompetin.View
{
    partial class BeliPulsaForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.txtNomorHp = new System.Windows.Forms.TextBox();
            this.cmbProduk = new System.Windows.Forms.ComboBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnBeli = new System.Windows.Forms.Button();
            this.lblProvider = new System.Windows.Forms.Label();
            this.lblNama = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbProvider
            // 
            this.cmbProvider.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(215, 151);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(189, 30);
            this.cmbProvider.TabIndex = 0;
            // 
            // txtNomorHp
            // 
            this.txtNomorHp.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomorHp.Location = new System.Drawing.Point(215, 190);
            this.txtNomorHp.Name = "txtNomorHp";
            this.txtNomorHp.Size = new System.Drawing.Size(189, 29);
            this.txtNomorHp.TabIndex = 1;
            // 
            // cmbProduk
            // 
            this.cmbProduk.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProduk.FormattingEnabled = true;
            this.cmbProduk.Location = new System.Drawing.Point(215, 229);
            this.cmbProduk.Name = "cmbProduk";
            this.cmbProduk.Size = new System.Drawing.Size(189, 30);
            this.cmbProduk.TabIndex = 2;
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(112, 334);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 30);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnBeli
            // 
            this.btnBeli.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBeli.Location = new System.Drawing.Point(310, 334);
            this.btnBeli.Name = "btnBeli";
            this.btnBeli.Size = new System.Drawing.Size(75, 30);
            this.btnBeli.TabIndex = 4;
            this.btnBeli.Text = "Beli";
            this.btnBeli.UseVisualStyleBackColor = true;
            this.btnBeli.Click += new System.EventHandler(this.btnBeli_Click);
            // 
            // lblProvider
            // 
            this.lblProvider.AutoSize = true;
            this.lblProvider.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProvider.Location = new System.Drawing.Point(94, 151);
            this.lblProvider.Name = "lblProvider";
            this.lblProvider.Size = new System.Drawing.Size(66, 23);
            this.lblProvider.TabIndex = 24;
            this.lblProvider.Text = "Provider";
            // 
            // lblNama
            // 
            this.lblNama.AutoSize = true;
            this.lblNama.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNama.Location = new System.Drawing.Point(169, 73);
            this.lblNama.Name = "lblNama";
            this.lblNama.Size = new System.Drawing.Size(161, 23);
            this.lblNama.TabIndex = 23;
            this.lblNama.Text = "Beli Pulsa / Paket Data";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(91, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 23);
            this.label1.TabIndex = 25;
            this.label1.Text = "No Tlpn Tujuan";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(94, 232);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 23);
            this.label2.TabIndex = 26;
            this.label2.Text = "Paket / Pulsa";
            // 
            // BeliPulsaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(514, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblProvider);
            this.Controls.Add(this.lblNama);
            this.Controls.Add(this.btnBeli);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.cmbProduk);
            this.Controls.Add(this.txtNomorHp);
            this.Controls.Add(this.cmbProvider);
            this.Name = "BeliPulsaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BeliPulsaForm";
            this.Load += new System.EventHandler(this.BeliPulsaForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbProvider;
        private System.Windows.Forms.TextBox txtNomorHp;
        private System.Windows.Forms.ComboBox cmbProduk;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnBeli;
        private System.Windows.Forms.Label lblProvider;
        private System.Windows.Forms.Label lblNama;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}