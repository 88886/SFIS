namespace moverect
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        ///   <summary> 
        ///   清理所有正在使用的资源。 
        ///   </summary> 
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region   Windows   窗体设计器生成的代码
        ///   <summary> 
        ///   设计器支持所需的方法   -   不要使用代码编辑器修改 
        ///   此方法的内容。 
        ///   </summary> 
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form2
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(560, 397);
            this.Name = "Form2";
            this.Text = "可拖动节点 ";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.myMouseUp);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.myMouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.myMouseMove);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Label label1;
    }
}