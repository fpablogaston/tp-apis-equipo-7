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
    public partial class CategoriaManager : Form
    {

        private Categoria categoria = null;

        public CategoriaManager(Categoria categoria)
        {
            InitializeComponent();
            this.categoria = categoria;
        }

        public CategoriaManager()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Debe ingresar un nombre");
                return;
            }


            CategoriaNegocio negocio = new CategoriaNegocio();
            try
            {
                if (categoria == null)
                    categoria = new Categoria();

                categoria.Descripcion = textBox1.Text;

                if (categoria.IdCategoria == 0)
                {
                    negocio.agregar(categoria);
                    MessageBox.Show("Categoria agregada exitosamente");
                }

                else
                {
                    negocio.modificar(categoria);
                    MessageBox.Show("Categoria modificada exitosamente");
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al guardar los cambios, por favor intente nuevamente.");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CategoriaManager_Load(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            List<Categoria> categorias = negocio.listar();
            try
            {

                if (categoria != null && categoria.IdCategoria != 0)
                {
                    Text = "Modificar Marca";
                    textBox1.Text = categoria.Descripcion;
                    btnAceptar.Text = "Modificar";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar la información de la categoria, intente nuevamente.");
            }
        }
    }
}
