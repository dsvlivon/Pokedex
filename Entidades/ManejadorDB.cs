using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ManejadorDB
    {
        public static string initialDirectory = "C:\\Users\\L54556\\OneDrive - Kimberly-Clark\\Desktop\\DANIEL\\La Pokedex";
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader reader;

        public ManejadorDB()
        {
            comando = new SqlCommand();
            conexion = new SqlConnection("Data Source=.; Initial Catalog=Pokedex; Integrated Security=True;");
            comando.Connection = conexion;
        }
        public SqlConnection Conexion {get { return this.conexion; }}
        public SqlCommand Comando { get { return this.comando; }}
        public void SetComandoText(string c) { this.comando.CommandText = c; }
        public SqlDataReader Reader{ get { return this.reader; }  set { this.reader = value; } }

        public bool Ejecutar()
        {
            bool ret = false;
            try
            {
                this.Conexion.Open();
                ret = this.Comando.ExecuteNonQuery() >= 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error en la base de datos.", ex);//
            }
            finally
            {
                if (this.Conexion.State == System.Data.ConnectionState.Open)
                {
                    this.Conexion.Close();
                }
            }
            return ret;
        }
    }
}
