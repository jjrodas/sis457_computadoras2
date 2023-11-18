using CadComputadoras2;
using ClnComputadoras2;
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
    public partial class FrmCliente : Form
    {
        FrmPrincipal frmPrincipal;
        bool esNuevo = false;
        
        public FrmCliente(FrmPrincipal frmPrincipal)
        {
            InitializeComponent();
            this.frmPrincipal = frmPrincipal;
        }

        private void lblPrincipal_Click(object sender, EventArgs e)
        {

        }
        private void listarCliente()
        {
            var clientes = ClienteCln.listarPa(txtParametro.Text.Trim());
            dgvListaClientes.DataSource = clientes;
            dgvListaClientes.Columns["id"].Visible = false;
            dgvListaClientes.Columns["cedulaIdentidad"].HeaderText = "CI";
            dgvListaClientes.Columns["nombres"].HeaderText = "Nombres";
            dgvListaClientes.Columns["apellidos"].HeaderText = "Apellidos";
            dgvListaClientes.Columns["telefono"].HeaderText = "N° de Teléfono";
            dgvListaClientes.Columns["usuarioRegistro"].HeaderText = "Usuario";
            dgvListaClientes.Columns["fechaRegistro"].HeaderText = "Fecha de Registro";
            dgvListaClientes.Columns["estado"].Visible = false;
            btnEditar.Enabled = clientes.Count > 0;
            btnEliminar.Enabled = clientes.Count > 0;
            if (clientes.Count > 0) dgvListaClientes.Rows[0].Cells["nombres"].Selected = true;
        }

        private bool validar()
        {
            bool esValido = true;
            erpNombres.SetError(txtApellidos, "");
            erpApellidos.SetError(txtNombres, "");
            erpCedulaIdentidad.SetError(txtCedulaIdentidad, "");
            erpTelefono.SetError(txtTelefono, "");

            if (string.IsNullOrEmpty(txtApellidos.Text))
            {
                esValido = false;
                erpNombres.SetError(txtApellidos, "El campo nombres es obligatorio.");
            }
            if (string.IsNullOrEmpty(txtNombres.Text))
            {
                esValido = false;
                erpApellidos.SetError(txtNombres, "El campo apellidos es obligatorio.");
            }

            if (string.IsNullOrEmpty(txtCedulaIdentidad.Text))
            {
                esValido = false;
                erpCedulaIdentidad.SetError(txtCedulaIdentidad, "El campo cédula de identidad es obligatorio.");
            }
            if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                esValido = false;
                erpTelefono.SetError(txtTelefono, "El campo número de teléfono es obligatorio.");
            }
            return esValido;
        }

        private void limpiarCliente()
        {
            txtNombres.Text = string.Empty;
            txtApellidos.Text = string.Empty;
            txtCedulaIdentidad.Text = string.Empty;
            txtTelefono.Text = string.Empty;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listarCliente();
        }

        private void lblBusqueda_Click(object sender, EventArgs e)
        {

        }

        private void txtParametro_TextChanged(object sender, EventArgs e)
        {

        }

        private void gbxLista_Enter(object sender, EventArgs e)
        {

        }

        private void pnlAcciones_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gbxDatos_Enter(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            Size = new Size(916, 600);
            txtCedulaIdentidad.Focus();
        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            Size = new Size(916, 418);
            listarCliente();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            Size = new Size(916, 600);

            int index = dgvListaClientes.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvListaClientes.Rows[index].Cells["id"].Value);
            var cliente = ClienteCln.get(id);
            txtCedulaIdentidad.Text = cliente.cedulaIdentidad;
            txtNombres.Text = cliente.apellidos;
            txtApellidos.Text = cliente.nombres;
            txtTelefono.Text = cliente.telefono;

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void segundoApellido_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvListaClientes.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvListaClientes.Rows[index].Cells["id"].Value);
            string nombres = dgvListaClientes.Rows[index].Cells["nombres"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro que desea dar de baja al cliente {nombres}?",
                "::: Compumundo - Mensaje :::", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                ClienteCln.eliminar(id, "SIS457");
                listarCliente();
                MessageBox.Show("Cliente dado de baja correctamente", "::: Compumundo - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
         }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var cliente = new Cliente();
                cliente.cedulaIdentidad = txtCedulaIdentidad.Text.Trim();
                cliente.nombres = txtNombres.Text.Trim();
                cliente.apellidos = txtApellidos.Text.Trim();
                cliente.telefono = txtTelefono.Text.Trim();
                cliente.usuarioRegistro = "LabSIS457";

                if (esNuevo)
                {
                    cliente.fechaRegistro = DateTime.Now;
                    cliente.estado = 1;
                    ClienteCln.insertar(cliente);
                }
                else
                {
                    int index = dgvListaClientes.CurrentCell.RowIndex;
                    cliente.id = Convert.ToInt32(dgvListaClientes.Rows[index].Cells["id"].Value);
                    ClienteCln.actualizar(cliente);
                }
                listarCliente();
                btnCancelar.PerformClick();
                MessageBox.Show("Cliente guardado correctamente", "::: Compumundo - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(961, 418);
            limpiarCliente();

        }

        private void FrmCliente_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmPrincipal.Visible = true;
        }
    }
}
