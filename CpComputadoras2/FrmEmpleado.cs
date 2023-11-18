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
    public partial class FrmEmpleado : Form
    {
        FrmPrincipal frmPrincipal;
        bool esNuevo = false;
        public FrmEmpleado(FrmPrincipal frmPrincipal)
        {
            InitializeComponent();
            this.frmPrincipal = frmPrincipal;
        }
        private void listar()
        {
            var empleados = EmpleadoCln.listarPa(txtParametro.Text.Trim());
            dgvListaEmpleados.DataSource = empleados;
            dgvListaEmpleados.Columns["id"].Visible = false;
            dgvListaEmpleados.Columns["estado"].Visible = false;
            dgvListaEmpleados.Columns["cedulaIdentidad"].HeaderText = "Cédula de Identidad";
            dgvListaEmpleados.Columns["nombres"].HeaderText = "Nombres";
            dgvListaEmpleados.Columns["apellidos"].HeaderText = "Apellidos";
            dgvListaEmpleados.Columns["direccion"].HeaderText = "Dirección";
            dgvListaEmpleados.Columns["celular"].HeaderText = "N° de Celular";
            dgvListaEmpleados.Columns["cargo"].HeaderText = "Cargo";
            dgvListaEmpleados.Columns["usuarioRegistro"].HeaderText = "Usuario";
            dgvListaEmpleados.Columns["fechaRegistro"].HeaderText = "Fecha de Registro";
            btnEditar.Enabled = empleados.Count > 0;
            btnEliminar.Enabled = empleados.Count > 0;
            if (empleados.Count > 0) dgvListaEmpleados.Rows[0].Cells["cedulaIdentidad"].Selected = true;
        }

        private void txtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) listar();
        }

        private void FrmEmpleado_Load(object sender, EventArgs e)
        {
            Size = new Size(916, 390);
            listar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            Size = new Size(916, 593);
            txtCedulaIdentidad.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            Size = new Size(961, 593);

            int index = dgvListaEmpleados.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvListaEmpleados.Rows[index].Cells["id"].Value);
            var empleado = EmpleadoCln.get(id);
            txtCedulaIdentidad.Text = empleado.cedulaIdentidad;
            txtNombres.Text = empleado.nombres;
            txtApellidos.Text = empleado.apellidos;
            txtDireccion.Text = empleado.direccion;
            txtCelular.Text = empleado.celular.ToString();
            cbxCargo.Text = empleado.cargo;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvListaEmpleados.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvListaEmpleados.Rows[index].Cells["id"].Value);
            string cedulaIdentidad = dgvListaEmpleados.Rows[index].Cells["cedulaIdentidad"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro que desea dar de baja al empleado con el C.I {cedulaIdentidad}?",
                "::: CompumundoBAC - Mensaje :::", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                EmpleadoCln.eliminar(id, "SIS457");
                listar();
                MessageBox.Show("Empleado dado de baja correctamente", "::: CompumundoBAC - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(961, 390);
            limpiar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listar();
        }
        private void limpiar()
        {
            txtCedulaIdentidad.Text = string.Empty;
            txtNombres.Text = string.Empty;
            txtApellidos.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtCelular.Text = string.Empty;
            cbxCargo.SelectedIndex = -1;
        }
        private bool validar()
        {
            bool esValido = true;
            erpCedulaIdentidad.SetError(txtCedulaIdentidad, "");
            erpNombres.SetError(txtNombres, "");
            erpApellidos.SetError(txtApellidos, "");
            erpDireccion.SetError(txtDireccion, "");
            erpCelular.SetError(txtCelular, "");
            erpCargo.SetError(cbxCargo, "");
            if (string.IsNullOrEmpty(txtCedulaIdentidad.Text))
            {
                esValido = false;
                erpCedulaIdentidad.SetError(txtCedulaIdentidad, "El campo cédula de identidad es obligatorio.");
            }
            if (string.IsNullOrEmpty(txtNombres.Text))
            {
                esValido = false;
                erpNombres.SetError(txtNombres, "El campo nombres es obligatorio.");
            }
            if (string.IsNullOrEmpty(txtApellidos.Text))
            {
                esValido = false;
                erpApellidos.SetError(txtApellidos, "El campo apellidos es obligatorio.");
            }
            if (string.IsNullOrEmpty(txtDireccion.Text))
            {
                esValido = false;
                erpDireccion.SetError(txtDireccion, "El campo direción es obligatorio.");
            }
            if (string.IsNullOrEmpty(txtCelular.Text))
            {
                esValido = false;
                erpCelular.SetError(txtCelular, "El campo celular es obligatorio.");
            }
            if (string.IsNullOrEmpty(cbxCargo.Text))
            {
                esValido = false;
                erpCargo.SetError(cbxCargo, "El campo cargo es obligatorio.");
            }
            return esValido;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var empleado = new Empleado();
                empleado.cedulaIdentidad = txtCedulaIdentidad.Text.Trim();
                empleado.nombres = txtNombres.Text.Trim();
                empleado.apellidos= txtApellidos.Text.Trim();
                empleado.direccion = txtDireccion.Text.Trim();
                empleado.celular = Convert.ToInt32(txtCelular.Text);
                empleado.cargo = cbxCargo.Text;
                empleado.usuarioRegistro = "LabSIS457";

                if (esNuevo)
                {
                    empleado.fechaRegistro = DateTime.Now;
                    empleado.estado = 1;
                    EmpleadoCln.insertar(empleado);
                }
                else
                {
                    int index = dgvListaEmpleados.CurrentCell.RowIndex;
                    empleado.id = Convert.ToInt32(dgvListaEmpleados.Rows[index].Cells["id"].Value);
                    EmpleadoCln.actualizar(empleado);
                }
                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("Empleado guardado correctamente", "::: Compumundo - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FrmEmpleado_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmPrincipal.Visible = true;
        }
    }
}
