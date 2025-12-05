namespace Dompetin.View
{
    partial class TopUpForm
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
            this.lblJudul = new System.Windows.Forms.Label();
            this.txtNominal = new System.Windows.Forms.TextBox();
            this.btnTopUp = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblJudul
            // 
            this.lblJudul.AutoSize = true;
            this.lblJudul.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJudul.Location = new System.Drawing.Point(162, 196);
            this.lblJudul.Name = "lblJudul";
            this.lblJudul.Size = new System.Drawing.Size(188, 23);
            this.lblJudul.TabIndex = 0;
            this.lblJudul.Text = "Masukkan nominal Top up";
            // 
            // txtNominal
            // 
            this.txtNominal.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNominal.Location = new System.Drawing.Point(170, 246);
            this.txtNominal.Name = "txtNominal";
            this.txtNominal.Size = new System.Drawing.Size(164, 29);
            this.txtNominal.TabIndex = 1;
            this.txtNominal.TextChanged += new System.EventHandler(this.txtTopUp_TextChanged);
            // 
            // btnTopUp
            // 
            this.btnTopUp.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTopUp.Location = new System.Drawing.Point(211, 302);
            this.btnTopUp.Name = "btnTopUp";
            this.btnTopUp.Size = new System.Drawing.Size(75, 34);
            this.btnTopUp.TabIndex = 2;
            this.btnTopUp.Text = "Top Up";
            this.btnTopUp.UseVisualStyleBackColor = true;
            this.btnTopUp.Click += new System.EventHandler(this.btnTopUp_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(180, 420);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 23);
            this.lblStatus.TabIndex = 3;
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(30, 511);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 33);
            this.btnBack.TabIndex = 4;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(88, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 23);
            this.label1.TabIndex = 5;
            this.label1.Text = "Ewalet ? Dompetin Aja";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(136, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(366, 23);
            this.label2.TabIndex = 6;
            this.label2.Text = "Kenpa ? Karna Disini Aman , Damai & Tanpa Potongan";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(246, 610);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(200, 23);
            this.label6.TabIndex = 17;
            this.label6.Text = "Uang Kamu Aman Ko Disini";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(74, 576);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(305, 23);
            this.label5.TabIndex = 16;
            this.label5.Text = "Terimahkasi Telah Mengguanakan Dompetin";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Dompetin.Properties.Resources.lovepik_wallet_vector_elements_png_image_401487952_wh8601;
            this.pictureBox1.Location = new System.Drawing.Point(16, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(66, 62);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // TopUpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(514, 661);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnTopUp);
            this.Controls.Add(this.txtNominal);
            this.Controls.Add(this.lblJudul);
            this.Name = "TopUpForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TopUpForm";
            this.Load += new System.EventHandler(this.TopUpForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.TextBox txtNominal;
        private System.Windows.Forms.Button btnTopUp;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
    }
}