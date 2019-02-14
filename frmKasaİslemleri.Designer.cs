namespace _14542513HayriyeCingöz_restoran_
{
    partial class frmKasaİslemleri
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.DataTable1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSet1 = new _14542513HayriyeCingöz_restoran_.DataSet1();
            this.rpaylik = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnAylik = new System.Windows.Forms.Button();
            this.btnzrapor = new System.Windows.Forms.Button();
            this.DataTable1TableAdapter = new _14542513HayriyeCingöz_restoran_.DataSet1TableAdapters.DataTable1TableAdapter();
            this.rpGünlük = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCıkıs = new System.Windows.Forms.Button();
            this.btnGeriDon = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // DataTable1BindingSource
            // 
            this.DataTable1BindingSource.DataMember = "DataTable1";
            this.DataTable1BindingSource.DataSource = this.DataSet1;
            // 
            // DataSet1
            // 
            this.DataSet1.DataSetName = "DataSet1";
            this.DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // rpaylik
            // 
            reportDataSource2.Name = "DataSet1";
            reportDataSource2.Value = this.DataTable1BindingSource;
            this.rpaylik.LocalReport.DataSources.Add(reportDataSource2);
            this.rpaylik.LocalReport.ReportEmbeddedResource = "_14542513HayriyeCingöz_restoran_.Report1.rdlc";
            this.rpaylik.Location = new System.Drawing.Point(291, 79);
            this.rpaylik.Name = "rpaylik";
            this.rpaylik.Size = new System.Drawing.Size(782, 510);
            this.rpaylik.TabIndex = 0;
            // 
            // btnAylik
            // 
            this.btnAylik.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAylik.Location = new System.Drawing.Point(68, 116);
            this.btnAylik.Name = "btnAylik";
            this.btnAylik.Size = new System.Drawing.Size(217, 81);
            this.btnAylik.TabIndex = 1;
            this.btnAylik.Text = "Aylık Rapor";
            this.btnAylik.UseVisualStyleBackColor = true;
            this.btnAylik.Click += new System.EventHandler(this.btnAylik_Click);
            // 
            // btnzrapor
            // 
            this.btnzrapor.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnzrapor.Location = new System.Drawing.Point(68, 239);
            this.btnzrapor.Name = "btnzrapor";
            this.btnzrapor.Size = new System.Drawing.Size(217, 81);
            this.btnzrapor.TabIndex = 1;
            this.btnzrapor.Text = "Günlük Rapor";
            this.btnzrapor.UseVisualStyleBackColor = true;
            this.btnzrapor.Click += new System.EventHandler(this.btnzrapor_Click);
            // 
            // DataTable1TableAdapter
            // 
            this.DataTable1TableAdapter.ClearBeforeFill = true;
            // 
            // rpGünlük
            // 
            this.rpGünlük.LocalReport.ReportEmbeddedResource = "_14542513HayriyeCingöz_restoran_.Report2.rdlc";
            this.rpGünlük.Location = new System.Drawing.Point(291, 79);
            this.rpGünlük.Name = "rpGünlük";
            this.rpGünlük.Size = new System.Drawing.Size(782, 510);
            this.rpGünlük.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(566, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "AYLIK RAPOR";
            // 
            // btnCıkıs
            // 
            this.btnCıkıs.BackColor = System.Drawing.Color.Transparent;
            this.btnCıkıs.BackgroundImage = global::_14542513HayriyeCingöz_restoran_.Properties.Resources.çıkış2;
            this.btnCıkıs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCıkıs.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnCıkıs.Location = new System.Drawing.Point(98, 579);
            this.btnCıkıs.Name = "btnCıkıs";
            this.btnCıkıs.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnCıkıs.Size = new System.Drawing.Size(60, 62);
            this.btnCıkıs.TabIndex = 18;
            this.btnCıkıs.UseVisualStyleBackColor = false;
            this.btnCıkıs.Click += new System.EventHandler(this.btnCıkıs_Click);
            // 
            // btnGeriDon
            // 
            this.btnGeriDon.BackColor = System.Drawing.Color.Transparent;
            this.btnGeriDon.BackgroundImage = global::_14542513HayriyeCingöz_restoran_.Properties.Resources.ge;
            this.btnGeriDon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGeriDon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnGeriDon.Location = new System.Drawing.Point(22, 579);
            this.btnGeriDon.Name = "btnGeriDon";
            this.btnGeriDon.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnGeriDon.Size = new System.Drawing.Size(57, 62);
            this.btnGeriDon.TabIndex = 19;
            this.btnGeriDon.UseVisualStyleBackColor = false;
            this.btnGeriDon.Click += new System.EventHandler(this.btnGeriDon_Click);
            // 
            // frmKasaİslemleri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::_14542513HayriyeCingöz_restoran_.Properties.Resources.arkaplan;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1099, 663);
            this.Controls.Add(this.btnCıkıs);
            this.Controls.Add(this.btnGeriDon);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rpGünlük);
            this.Controls.Add(this.btnzrapor);
            this.Controls.Add(this.btnAylik);
            this.Controls.Add(this.rpaylik);
            this.Name = "frmKasaİslemleri";
            this.Text = "frmKasaİslemleri";
            this.Load += new System.EventHandler(this.frmKasaİslemleri_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rpaylik;
        private System.Windows.Forms.Button btnAylik;
        private System.Windows.Forms.Button btnzrapor;
        private System.Windows.Forms.BindingSource DataTable1BindingSource;
        private DataSet1 DataSet1;
        private DataSet1TableAdapters.DataTable1TableAdapter DataTable1TableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer rpGünlük;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCıkıs;
        private System.Windows.Forms.Button btnGeriDon;
    }
}