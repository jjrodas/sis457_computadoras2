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
    public partial class FrmCategoria : Form
    {
        bool esNuevo = false;
        public FrmCategoria()
        {
            InitializeComponent();
        }

        private void gbxDatos_Enter(object sender, EventArgs e)
        {

        }
        private void listar()
        {
            var clientes = CategoriaCln.listarPa(txtParametro.Text.Trim());
            dgvListaCategorias.DataSource = clientes;
            dgvListaCategorias.Columns["id"].Visible = false;
            dgvListaCategorias.Columns["estado"].Visible = false;
            dgvListaCategorias.Columns["nombre"].HeaderText = "Nombre";
            dgvListaCategorias.Columns["descripcion"].HeaderText = "Descripción";
            dgvListaCategorias.Columns["usuarioRegistro"].HeaderText = "Usuario";
            dgvListaCategorias.Columns["fechaRegistro"].HeaderText = "Fecha de Registro";
            btnEditar.Enabled = clientes.Count > 0;
            btnEliminar.Enabled = clientes.Count > 0;
            if (clientes.Count > 0) dgvListaCategorias.Rows[0].Cells["nombre"].Selected = true;
        }

        private void txtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) listar();
        }

        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            Size = new Size(961, 431);
            listar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            Size = new Size(961, 593);
            txtNombre.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            Size = new Size(961, 593);

            int index = dgvListaCategorias.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvListaCategorias.Rows[index].Cells["id"].Value);
            var cliente = CategoriaCln.get(id);
            txtNombre.Text = cliente.nombre;
            txtDescripcion.Text = cliente.descripcion;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvListaCategorias.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvListaCategorias.Rows[index].Cells["id"].Value);
            string nombre = dgvListaCategorias.Rows[index].Cells["nombre"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro que desea dar de baja la categoría {nombre}?",
                "::: Compumundo - Mensaje :::", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                CategoriaCln.eliminar(id, "SIS457");
                listar();
                MessageBox.Show("Categoría dada de baja correctamente", "::: Compumundo - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(961, 431);
            limpiar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listar();
        }

        private void limpiar()
        {
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
        }

        private bool validar()
        {
            bool esValido = true;
            erpNombre.SetError(txtNombre, "");
            erpDescripcion.SetError(txtDescripcion, "");
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                esValido = false;
                erpNombre.SetError(txtNombre, "El campo nombre es obligatorio.");
            }
            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                esValido = false;
                erpDescripcion.SetError(txtDescripcion, "El campo descripción es obligatorio.");
            }
            return esValido;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var cliente = new Categoria();
                cliente.nombre = txtNombre.Text.Trim();
                cliente.descripcion = txtDescripcion.Text.Trim();
                cliente.usuarioRegistro = "LabSIS457";

                if (esNuevo)
                {
                    cliente.fechaRegistro = DateTime.Now;
                    cliente.estado = 1;
                    CategoriaCln.insertar(cliente);
                }
                else
                {
                    int index = dgvListaCategorias.CurrentCell.RowIndex;
                    cliente.id = Convert.ToInt32(dgvListaCategorias.Rows[index].Cells["id"].Value);
                    CategoriaCln.actualizar(cliente);
                }
                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("Categoría guardada correctamente", "::: Compumundo - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
