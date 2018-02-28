using System.Windows.Forms;

namespace KDRsReportExporter
{
    internal class PasswordForm : Form
    {
        private ContextMenuStrip contextMenuStrip1;
        private System.ComponentModel.IContainer components;
        private Button button1;
        private MaskedTextBox pwBox;

        public PasswordForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pwBox = new System.Windows.Forms.MaskedTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // pwBox
            //
            this.pwBox.Location = new System.Drawing.Point(87, 102);
            this.pwBox.Name = "pwBox";
            this.pwBox.PasswordChar = '*';
            this.pwBox.Size = new System.Drawing.Size(100, 20);
            this.pwBox.TabIndex = 0;
            //
            // contextMenuStrip1
            //
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            //
            // button1
            //
            this.button1.Location = new System.Drawing.Point(87, 141);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Finish";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            //
            // PasswordForm
            //
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pwBox);
            this.Name = "PasswordForm";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (pwBox.Text == "system11")
            {
                // The password is ok.
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                // The password is invalid.
                pwBox.Clear();
                MessageBox.Show("Inivalid password.");
                pwBox.Focus();
            }
        }
    }
}