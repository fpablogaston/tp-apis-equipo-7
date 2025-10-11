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
    public partial class frmCategoria : Form
    {
        public frmCategoria()
        {
            InitializeComponent();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CategoriaManager categoriaManager = new CategoriaManager();
            categoriaManager.ShowDialog();
            cargar();
        }

        private void cargar()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            try
            {
                List<Categoria> listaCategorias = negocio.listar();
                dgvCategoria.DataSource = null;
                dgvCategoria.DataSource = listaCategorias;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvCategoria.CurrentRow != null)
            {
                Categoria categoriaSeleccionada = (Categoria)dgvCategoria.CurrentRow.DataBoundItem;
                CategoriaManager categoriaManager = new CategoriaManager(categoriaSeleccionada);
                categoriaManager.ShowDialog();
                cargar();
            }
            else
            {
                MessageBox.Show("Seleccione una marca para modificar.");
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                CategoriaNegocio negocio = new CategoriaNegocio();
                Categoria seleccionado;
                try
                {
                    DialogResult respuesta = MessageBox.Show("¿Seguro que desea eliminar la categoría?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (respuesta == DialogResult.Yes)
                    {
                        seleccionado = (Categoria)dgvCategoria.CurrentRow.DataBoundItem;
                        negocio.eliminar(seleccionado.IdCategoria);
                        cargar();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("La categoría no puede ser eliminada, existe en artículos.");
                }
            }
        }

        private void frmCategoria_Load(object sender, EventArgs e)
        {
            cargar();
        }
    }
}
