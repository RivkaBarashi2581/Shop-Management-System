using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class Manager : Form
    {
        public Manager()
        {
            InitializeComponent();
            Text = "מנהל";
        }

        private void Manager_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenController(Types.CUSTOMER);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenController(Types.PRODUCT);
        }

        private void Sales_Click(object sender, EventArgs e)
        {
                        OpenController(Types.SALE);
        }

        private void OpenController(Types type)
        {
            this.Hide();
            var ctrl = new ManagerController(type);
            ctrl.FormClosed += (s, a) => this.Show();
            ctrl.Show();
        }

      
    }
}
