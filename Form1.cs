using System;
using System.Windows.Forms;

namespace Store_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {

            //this.Hide();
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }
    }
}
