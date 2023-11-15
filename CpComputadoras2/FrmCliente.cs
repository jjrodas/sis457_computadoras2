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
        bool esNuevo = false;
        
        public FrmCliente()
        {
            InitializeComponent();
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
            dgvListaClientes.Columns["primerApellido"].HeaderText = "Apellido Paterno";
            dgvListaClientes.Columns["segundoApellido"].HeaderText = "Apellido Materno";
            dgvListaClientes.Columns["celular"].HeaderText = "Celular";
            dgvListaClientes.Columns["usuarioRegistro"].HeaderText = "Usuario";
            dgvListaClientes.Columns["fechaRegistro"].HeaderText = "Fecha de Registro";
            dgvListaClientes.Columns["estado"].Visible = false;
            btnEditar.Enabled = clientes.Count > 0;
            btnEliminar.Enabled = clientes.Count > 0;
            if (clientes.Count > 0) dgvListaClientes.Rows[0].Cells["nombre"].Selected = true;
        }

        private bool validar()
        {
            bool esValido = true;
            erpNombres.SetError(txtNombre, "");
            erpprimerApellido.SetError(primerApellido, "");
            erpcedulaIdentidad.SetError(CedulaIdentidad, "");

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                esValido = false;
                erpNombre.SetError(txtNombre, "El campo nombre es obligatorio.");
            }
            if (string.IsNullOrEmpty(primerApellido.Text))
            {
                esValido = false;
                erpDescripcion.SetError(primerApellido, "El campo descripción es obligatorio.");
            }

            if (string.IsNullOrEmpty(CedulaIdentidad.Text))
            {
                esValido = false;
                erpDescripcion.SetError(CedulaIdentidad, "El campo descripción es obligatorio.");
            }
            return esValido;
        }

        private void limpiarCliente()
        {
            txtNombre.Text = string.Empty;
            primerApellido.Text = string.Empty;
            segundoApellido.Text = string.Empty;
            CedulaIdentidad.Text = string.Empty;
            celular.Text = string.Empty;
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
            Size = new Size(961, 600);
            txtNombre.Focus();
        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            Size = new Size(961, 418);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            Size = new Size(961, 600);

            int index = dgvListaClientes.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvListaClientes.Rows[index].Cells["id"].Value);
            var cliente = ClienteCln.get(id);
            txtNombre.Text = cliente.nombres;
            primerApellido.Text = cliente.primerApellido;
            segundoApellido.Text = cliente.segundoApellido;
            CedulaIdentidad.Text = cliente.cedulaIdentidad;
            celular.Text = cliente.celular;

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
            string nombres = dgvListaClientes.Rows[index].Cells["nombre"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro que desea dar de baja al cliente {nombres}?",
                "::: Compumundo - Mensaje :::", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                ClientesCln.eliminar(id, "SIS457");
                listar();
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
                cliente.nombres = txtNombre.Text.Trim();
                cliente.primerApellido = primerApellido.Text.Trim();
                cliente.segundoApellido = segundoApellido.Text.Trim();
                cliente.cedulaIdentidad = CedulaIdentidad.Text.Trim();
                cliente.celular = celular.Text.Trim();
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
    }
}
