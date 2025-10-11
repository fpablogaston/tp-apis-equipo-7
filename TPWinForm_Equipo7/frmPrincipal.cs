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
    public partial class frmPrincipal : Form
    {

        private List<Articulo> listaArticulos;
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void ocultarColumnas()
        {
            dgvMain.Columns["IdArticulo"].Visible = false;
            dgvMain.Columns["ImagenUrl"].Visible = false;
        }

        private void cargar()
        {

            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {

                listaArticulos = negocio.listar();

                dgvMain.DataSource = listaArticulos;
                dgvMain.Columns["Precio"].DefaultCellStyle.Format = "C2";
                dgvMain.Columns["Precio"].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("es-AR");
                ocultarColumnas();


                pbxMain.Load(listaArticulos[0].ImagenUrl);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxMain.Load(imagen);
            }
            catch (Exception)
            {
                pbxMain.Load("https://scontent.fepa5-1.fna.fbcdn.net/v/t1.6435-9/118319079_3384586298294379_8674936737719053103_n.jpg?_nc_cat=108&ccb=1-7&_nc_sid=833d8c&_nc_ohc=KWD4rmHRd9QQ7kNvwEmDoRF&_nc_oc=AdnQhaQNcWZ-j8HvlmjMUyJvmAXq6k3pL2Qw56EQTIbhhi7LVb4PIJaSwBcwanFcyUsQwZ3IIZb7ORFnOGNLiaV6&_nc_zt=23&_nc_ht=scontent.fepa5-1.fna&_nc_gid=tqWm5R2zPUiYdyqF0vnrkg&oh=00_AfavmsPRFlEM0imFpTAoelDz0usIe-K8KupUcwtHjDditA&oe=68ED6E10");


            }
        }

        private string ultimaImagen = "";
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void articulosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAgregarArticulos alta = new frmAgregarArticulos();
            alta.ShowDialog();
            cargar();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvMain.CurrentRow.DataBoundItem;
            frmAgregarArticulos modificar = new frmAgregarArticulos(seleccionado);
            modificar.ShowDialog();
            cargar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Seguro que desea eliminar el articulo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvMain.CurrentRow.DataBoundItem;
                    negocio.eliminarFisica(seleccionado.IdArticulo);
                    cargar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void administrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMarca ventana = new frmMarca();
            ventana.Show();
        }

        private void administrarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmCategoria ventana = new frmCategoria();
            ventana.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = txtFiltro.Text;

            if (filtro != "")
            {
                listaFiltrada = listaArticulos.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.CodigoArticulo.ToUpper().Contains(filtro.ToUpper()) || x.Descripcion.ToUpper().Contains(filtro.ToUpper()) || x.Marca.Descripcion.ToUpper().Contains(filtro.ToUpper()) || x.Categoria.Descripcion.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                listaFiltrada = listaArticulos;
            }

            dgvMain.DataSource = null;
            dgvMain.DataSource = listaFiltrada;
            ocultarColumnas();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
