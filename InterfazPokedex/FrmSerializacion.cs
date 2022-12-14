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
    public partial class FrmSerializacion : Form
    {
        Pokemon poke = null;
        public FrmSerializacion(Pokemon p)
        {
            InitializeComponent();
            poke = p;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            string nombreArchivo = txtNombreArchivo.Text;
            if(txtNombreArchivo.Text != "")
            {
                if (rdbJSON.Checked == true) {
                    if (FrmPrincipal.equipo.Count() > 0){
                        Serializador<Pokemon>.SerializeJson(FrmPrincipal.equipo, nombreArchivo);
                    } else { Serializador<Pokemon>.SerializeJson(poke, nombreArchivo); }
                }
                else if (rdbXML.Checked == true) {
                    if (FrmPrincipal.equipo.Count() > 0)
                    {
                        Serializador<Pokemon>.SerializarAXml(FrmPrincipal.equipo, nombreArchivo);
                    } else { Serializador<Pokemon>.SerializarAXml(poke, nombreArchivo); }
                }
                else if (rdbTXT.Checked == true) {
                    if (FrmPrincipal.equipo.Count() > 0)
                    {
                        Serializador<Pokemon>.SerializarATxt(Pokemon.EquipoToString(FrmPrincipal.equipo), nombreArchivo);
                    } else { Serializador<Pokemon>.SerializarATxt(poke.ToString(), nombreArchivo); }
                }
            } else { MessageBox.Show("Nombre Inválido... verifique y vuelva a intentar!"); }
            this.Close();
        }
    }
}
