namespace Ram
{
    partial class Start
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
            this.Btn_OpenRAM = new System.Windows.Forms.Button();
            this.Btn_OpenTest = new System.Windows.Forms.Button();
            this.Btn_OpenService = new System.Windows.Forms.Button();
            this.Btn_OpenPack = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_OpenRAM
            // 
            this.Btn_OpenRAM.Location = new System.Drawing.Point(47, 12);
            this.Btn_OpenRAM.Name = "Btn_OpenRAM";
            this.Btn_OpenRAM.Size = new System.Drawing.Size(139, 35);
            this.Btn_OpenRAM.TabIndex = 0;
            this.Btn_OpenRAM.Text = "记录信息";
            this.Btn_OpenRAM.UseVisualStyleBackColor = true;
            this.Btn_OpenRAM.Click += new System.EventHandler(this.Btn_OpenRAM_Click);
            // 
            // Btn_OpenTest
            // 
            this.Btn_OpenTest.Location = new System.Drawing.Point(47, 53);
            this.Btn_OpenTest.Name = "Btn_OpenTest";
            this.Btn_OpenTest.Size = new System.Drawing.Size(139, 35);
            this.Btn_OpenTest.TabIndex = 1;
            this.Btn_OpenTest.Text = "测试信息录入";
            this.Btn_OpenTest.UseVisualStyleBackColor = true;
            this.Btn_OpenTest.Click += new System.EventHandler(this.Btn_OpenTest_Click);
            // 
            // Btn_OpenService
            // 
            this.Btn_OpenService.Location = new System.Drawing.Point(47, 94);
            this.Btn_OpenService.Name = "Btn_OpenService";
            this.Btn_OpenService.Size = new System.Drawing.Size(139, 35);
            this.Btn_OpenService.TabIndex = 2;
            this.Btn_OpenService.Text = "维修信息录入";
            this.Btn_OpenService.UseVisualStyleBackColor = true;
            this.Btn_OpenService.Click += new System.EventHandler(this.Btn_OpenService_Click);
            // 
            // Btn_OpenPack
            // 
            this.Btn_OpenPack.Location = new System.Drawing.Point(47, 135);
            this.Btn_OpenPack.Name = "Btn_OpenPack";
            this.Btn_OpenPack.Size = new System.Drawing.Size(139, 35);
            this.Btn_OpenPack.TabIndex = 3;
            this.Btn_OpenPack.Text = "包装信息录入";
            this.Btn_OpenPack.UseVisualStyleBackColor = true;
            this.Btn_OpenPack.Click += new System.EventHandler(this.Btn_OpenPack_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(47, 176);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 38);
            this.button1.TabIndex = 4;
            this.button1.Text = "导出报表";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 234);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Btn_OpenPack);
            this.Controls.Add(this.Btn_OpenService);
            this.Controls.Add(this.Btn_OpenTest);
            this.Controls.Add(this.Btn_OpenRAM);
            this.Name = "Start";
            this.Text = "开始界面";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_OpenRAM;
        private System.Windows.Forms.Button Btn_OpenTest;
        private System.Windows.Forms.Button Btn_OpenService;
        private System.Windows.Forms.Button Btn_OpenPack;
        private System.Windows.Forms.Button button1;
    }
}