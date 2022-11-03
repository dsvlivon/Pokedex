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
        public FrmFiltro()
        {
            InitializeComponent();
            cmbFiltro.DataSource = new List<String> { "Ash", "Brock", "Misty" };//habria q cambiarlo x una query q traiga los entrenadores q tienen pokemones
            this.team = new List<Pokemon>();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            string trainer = cmbFiltro.SelectedItem.ToString();
            PokemonDAO p = new PokemonDAO();
            team = p.LeerPorEntrenador(trainer);
            FrmPrincipal.equipo = team;
            this.Close();
        }
    }
}
