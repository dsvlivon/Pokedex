using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterfazPokedex
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string entrenador="";
            if (rdbAsh.Checked) { entrenador = "Ash"; }
            else if (rdbBrock.Checked) { entrenador = "Brock"; }
            if (rdbMisty.Checked) { entrenador = "Misty"; }
            FrmPrincipal frmPrincipal = new FrmPrincipal(entrenador);
            frmPrincipal.ShowDialog(); 
        }
    }
}
