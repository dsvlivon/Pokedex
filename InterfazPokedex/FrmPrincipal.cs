using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;

namespace InterfazPokedex
{
    public partial class FrmPrincipal : Form
    {        
        private string entrenador;
        private Pokemon poke = null;
        public static List<Pokemon> equipo;
        private int index;
        public FrmPrincipal(string entrenador)
        {
            InitializeComponent();
            this.entrenador = entrenador;
            equipo = new List<Pokemon>();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            int i;
            if (this.entrenador == "Ash") { i = 0; }
            else if (this.entrenador == "Brock") { i = 1; }
            else if (this.entrenador == "Misty") { i = 2; }
            else { i = 4; }
            btnAnterior.Enabled = false;
            btnSiguiente.Enabled = false;
            this.picEntrenador.Image = imageList1.Images[i];
            this.index = 0;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmAlta frmAlta = new FrmAlta(this.entrenador);
            frmAlta.ShowDialog();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            PokemonDAO p = new PokemonDAO();
            if (Int32.TryParse(txtBusqueda.Text, out int id))
            {
                this.poke = p.Leer(null, id);
            }
            else { this.poke = p.Leer(txtBusqueda.Text, null); }
            if (this.poke is null) { MessageBox.Show("No existe el regist  ro!"); }
            else
            {
                ManejadorPantalla();
            }
        }
        private void btnPorEntrenador_Click(object sender, EventArgs e)
        {
            index = 0;
            LanzarForm(PokemonDAO.Filtro.entrenador);
        }
        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal.equipo.Count() > 0)
            {
                if (this.index == FrmPrincipal.equipo.Count() - 1) { this.index = 0; }
                else { index ++; }
                this.poke = FrmPrincipal.equipo[index];
            } ManejadorPantalla();
        }
        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (FrmPrincipal.equipo.Count() > 0)
            {
                if (this.index == 0) { this.index = FrmPrincipal.equipo.Count() - 1; }
                else { index--; }
                this.poke = FrmPrincipal.equipo[index];
            }
            ManejadorPantalla();
        }
        private void btnPorTipo_Click(object sender, EventArgs e)
        {
            index = 0;
            LanzarForm(PokemonDAO.Filtro.tipo);
        }
        private void btnPorRango_Click(object sender, EventArgs e)
        {
            index = 0;
            LanzarForm(PokemonDAO.Filtro.rango);
        }
        private void btnPersonalizada_Click(object sender, EventArgs e)
        {
            index = 0;
            LanzarForm(PokemonDAO.Filtro.personalizado);
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            PokemonDAO p = new PokemonDAO();
            if (this.poke != null)
            {
                var confirmar = MessageBox.Show($"Se eliminara el registro, {this.poke.Id} esta de acuerdo ?", "Confirmar Borrar!!", MessageBoxButtons.YesNo);
                if (confirmar == DialogResult.Yes)
                {
                    p.Borrar(poke.Id);
                }
                this.poke = null;
                ManejadorPantalla();
            }
        }
        private void btnSerializacion_Click(object sender, EventArgs e)
        {
            FrmSerializacion frmserial = new FrmSerializacion(poke);
            frmserial.ShowDialog();
        }

        private void ManejadorPantalla() {
            if (this.poke is null)
            {
                lblIdTexto.Text = "ID: ";
                lblNombre.Text = "Nombre: ";
                lblTipo.Text = "Tipo: ";
                lblEntrenadorPokemon.Text = "Entrenador: ";
                picPokemon.BackgroundImage = null;
            } else
            {
                lblIdTexto.Text = "ID: " + this.poke.Id.ToString();
                lblNombre.Text = "Nombre: " + this.poke.Nombre;
                lblTipo.Text = "Tipo: " + Pokemon.ObtenerTipoDescripcion(this.poke.Tipo);
                lblEntrenadorPokemon.Text = "Entrenador: " + this.poke.Entrenador;
                picPokemon.BackgroundImageLayout = ImageLayout.Stretch;
                picPokemon.BackgroundImage = Image.FromFile(this.poke.UrlImagen);
            }
        }
        private void LanzarForm(PokemonDAO.Filtro f)
        {
            FrmFiltro frmFiltro = new FrmFiltro(f);
            frmFiltro.ShowDialog();
            btnAnterior.Enabled = true;
            btnSiguiente.Enabled = true;
            if (FrmPrincipal.equipo.Count() > 0)
            {
                this.poke = FrmPrincipal.equipo[index];
            }
            else { this.poke = null; }
            ManejadorPantalla();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            this.poke = null;
            equipo = new List<Pokemon>();
            ManejadorPantalla();
            txtBusqueda.Text = "";
        }
    }
}
