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
                cmbFiltro.Show(); nudMin.Hide(); nudMax.Hide(); rtbQuery.Hide();
                cmbFiltro.DataSource = p.LeerListaDeEntrenadores();
            }
            else if (filtro == PokemonDAO.Filtro.tipo)
            {
                cmbFiltro.Show(); nudMin.Hide(); nudMax.Hide(); rtbQuery.Hide();
                TipoDAO t = new TipoDAO();
                cmbFiltro.DataSource = t.Leer();
            }
            else if (filtro == PokemonDAO.Filtro.rango)
            {
                cmbFiltro.Hide(); nudMin.Show(); nudMax.Show(); rtbQuery.Hide();
            }
            else { cmbFiltro.Hide(); nudMin.Hide(); nudMax.Hide(); rtbQuery.Show();
                rtbQuery.Text = "SELECT * FROM dbo.Pokemon WHERE nombre = 'Alakazam'";
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            PokemonDAO p = new PokemonDAO();
            string criterio = "";
            string query = "";
            if (cmbFiltro.SelectedItem != null) { criterio = cmbFiltro.SelectedItem.ToString(); }
             if (rtbQuery.Text!= null) { query = rtbQuery.Text; }
            team = p.LeerConFiltro(query, criterio, (int)nudMin.Value, (int)nudMax.Value, filtro); 
           
            FrmPrincipal.equipo = team;
            this.Close();
        }
    }
}
