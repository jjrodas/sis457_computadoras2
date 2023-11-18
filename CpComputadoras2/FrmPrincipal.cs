using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpComputadoras2
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void btnCerrarApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnProdcutos_Click(object sender, EventArgs e)
        {
            new FrmProducto().ShowDialog();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            new FrmCliente().ShowDialog();
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            new FrmEmpleado().ShowDialog();
        }
    }
}
