namespace WorldMap
{
    partial class RiverCountriesForm
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
            this.lblRiver = new System.Windows.Forms.Label();
            this.clbCountries = new System.Windows.Forms.CheckedListBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblRiver
            // 
            this.lblRiver.AutoSize = true;
            this.lblRiver.Location = new System.Drawing.Point(3, 33);
            this.lblRiver.Name = "lblRiver";
            this.lblRiver.Size = new System.Drawing.Size(44, 16);
            this.lblRiver.TabIndex = 0;
            this.lblRiver.Text = "Daryo";
           // this.lblRiver.Click += new System.EventHandler(this.lblRiver_Click);
            // 
            // clbCountries
            // 
            this.clbCountries.CheckOnClick = true;
            this.clbCountries.FormattingEnabled = true;
            this.clbCountries.Location = new System.Drawing.Point(-3, 52);
            this.clbCountries.Name = "clbCountries";
            this.clbCountries.Size = new System.Drawing.Size(791, 327);
            this.clbCountries.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(34, 385);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(105, 59);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Belgilash";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(189, 385);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(93, 59);
            this.btnSelectAll.TabIndex = 3;
            this.btnSelectAll.Text = "Hammasini belgilash";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(330, 385);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(96, 59);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Hammasini olish";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // RiverCountriesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.clbCountries);
            this.Controls.Add(this.lblRiver);
            this.Name = "RiverCountriesForm";
            this.Text = "RiverCountriesForm";
            this.Load += new System.EventHandler(this.RiverCountriesForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRiver;
        private System.Windows.Forms.CheckedListBox clbCountries;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnClear;
    }
}