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
    public partial class FrmFiltro : Form
    {
        List<Pokemon> team;
        public PokemonDAO.Filtro filtro;
        public FrmFiltro(PokemonDAO.Filtro f)
        {
            InitializeComponent();
            PokemonDAO p = new PokemonDAO();
            this.team = new List<Pokemon>();
            filtro = f;
            if (filtro == PokemonDAO.Filtro.entrenador)
            {
                cmbFiltro.Show(); nudMin.Hide(); nudMax.Hide();
                cmbFiltro.DataSource = p.LeerListaDeEntrenadores();
            }
            else if (filtro == PokemonDAO.Filtro.tipo)
            {
                cmbFiltro.Show(); nudMin.Hide(); nudMax.Hide();
                TipoDAO t = new TipoDAO();
                cmbFiltro.DataSource = t.Leer();
            }
            else if (filtro == PokemonDAO.Filtro.rango)
            {
                cmbFiltro.Hide(); nudMin.Show(); nudMax.Show();
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            PokemonDAO p = new PokemonDAO();
            string criterio = ""; 
            if(cmbFiltro.SelectedItem != null) { criterio = cmbFiltro.SelectedItem.ToString(); }
            team = p.LeerConFiltro(criterio, (int)nudMin.Value, (int)nudMax.Value, filtro); 
           
            FrmPrincipal.equipo = team;
            this.Close();
        }
    }
}
