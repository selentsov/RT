namespace RangeTrainer
{
    partial class RangeSettingMenu
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
            this.textBoxRaiseRange = new System.Windows.Forms.TextBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.textBoxCallRange = new System.Windows.Forms.TextBox();
            this.labelRaiseRange = new System.Windows.Forms.Label();
            this.labelCallAction = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxRaiseRange
            // 
            this.textBoxRaiseRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxRaiseRange.Location = new System.Drawing.Point(12, 43);
            this.textBoxRaiseRange.Name = "textBoxRaiseRange";
            this.textBoxRaiseRange.Size = new System.Drawing.Size(578, 26);
            this.textBoxRaiseRange.TabIndex = 0;
            // 
            // buttonStart
            // 
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStart.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.buttonStart.Location = new System.Drawing.Point(218, 173);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(142, 27);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "S T A R T";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // textBoxCallRange
            // 
            this.textBoxCallRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxCallRange.Location = new System.Drawing.Point(12, 110);
            this.textBoxCallRange.Name = "textBoxCallRange";
            this.textBoxCallRange.Size = new System.Drawing.Size(578, 26);
            this.textBoxCallRange.TabIndex = 2;
            // 
            // labelRaiseRange
            // 
            this.labelRaiseRange.AutoSize = true;
            this.labelRaiseRange.Font = new System.Drawing.Font("HelveticaNeueLT Pro 55 Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRaiseRange.Location = new System.Drawing.Point(8, 9);
            this.labelRaiseRange.Name = "labelRaiseRange";
            this.labelRaiseRange.Size = new System.Drawing.Size(50, 19);
            this.labelRaiseRange.TabIndex = 3;
            this.labelRaiseRange.Text = "Raise";
            // 
            // labelCallAction
            // 
            this.labelCallAction.AutoSize = true;
            this.labelCallAction.Font = new System.Drawing.Font("HelveticaNeueLT Pro 55 Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCallAction.Location = new System.Drawing.Point(8, 82);
            this.labelCallAction.Name = "labelCallAction";
            this.labelCallAction.Size = new System.Drawing.Size(38, 19);
            this.labelCallAction.TabIndex = 4;
            this.labelCallAction.Text = "Call";
            // 
            // StartMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(603, 212);
            this.Controls.Add(this.labelCallAction);
            this.Controls.Add(this.labelRaiseRange);
            this.Controls.Add(this.textBoxCallRange);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.textBoxRaiseRange);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StartMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StartMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxRaiseRange;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.TextBox textBoxCallRange;
        private System.Windows.Forms.Label labelRaiseRange;
        private System.Windows.Forms.Label labelCallAction;


    }
}