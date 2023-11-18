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
        FrmAutenticacion frmAutenticacion;
        public FrmPrincipal(FrmAutenticacion frmAutenticacion)
        {
            InitializeComponent();
            this.frmAutenticacion = frmAutenticacion;
        }

        private void btnCerrarApp_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnProdcutos_Click(object sender, EventArgs e)
        {
            Visible = false;
            new FrmProducto(this).ShowDialog();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            Visible = false;
            new FrmCliente(this).ShowDialog();
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            Visible = false;
            new FrmEmpleado(this).ShowDialog();
        }

        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmAutenticacion.Visible = true;
        }
    }
}
