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
    public partial class FrmAlta : Form
    {
        private string entrenador;
        private Pokemon poke;
        private string url;
        
        public FrmAlta(string entrenador)
        {
            InitializeComponent();
            this.entrenador = entrenador;
        }

        private void FrmAlta_Load(object sender, EventArgs e)
        {
            TipoDAO t = new TipoDAO();
            cmbTipos.DataSource = t.Leer();
            txtEntrenador.Text = this.entrenador;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Recordar OpenFileDialog
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = ManejadorDB.initialDirectory;
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    this.url = openFileDialog.FileName;
                    txtDireccionImagen.Text = this.url;
                }
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (this.url != null && txtNombre.Text!= "" || txtNombre.Text != null) {
                PokemonDAO pokeDao = new PokemonDAO();
                poke = new Pokemon((int)nupId.Value, txtNombre.Text, cmbTipos.SelectedValue.ToString(), this.entrenador, this.url);
                if (pokeDao.Guardar(poke)) { MessageBox.Show("SE GUARDO el Pokemon correctamente"); }
                else { MessageBox.Show("NO SE GUARDO el Pokemon correctamente... "); }
                this.Close();
            }
        }
    }
}
