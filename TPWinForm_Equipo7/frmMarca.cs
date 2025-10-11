using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPWinForm_Equipo7
{
    public partial class frmMarca : Form
    {
        public frmMarca()
        {
            InitializeComponent();
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MarcaManager marcaManager = new MarcaManager();
            marcaManager.ShowDialog();
            cargar();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvMarca.CurrentRow != null)
            {
                Marca marcaSeleccionada = (Marca)dgvMarca.CurrentRow.DataBoundItem;
                MarcaManager marcaManager = new MarcaManager(marcaSeleccionada);
                marcaManager.ShowDialog();
                cargar();
            }
            else
            {
                MessageBox.Show("Seleccione una marca para modificar.");
            }

        }

        private void frmMarcas_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            try
            {
                List<Marca> listaMarcas = negocio.listar();
                dgvMarca.DataSource = null;
                dgvMarca.DataSource = listaMarcas;

            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar la marca. Intente nuevamente");
            }

        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MarcaNegocio negocio = new MarcaNegocio();
            Marca seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Seguro que desea eliminar la marca?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Marca)dgvMarca.CurrentRow.DataBoundItem;
                    negocio.eliminar(seleccionado.IdMarca);
                    cargar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar la marca. Existen articulos relacionados.");
            }
        }
    }
}
