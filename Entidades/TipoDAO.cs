using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public  class TipoDAO:ManejadorDB
    {
        public List<String> Leer()
        {
            List<String> l = new List<String>();
            this.Comando.CommandText = "SELECT * FROM dbo.Tipos";
            try
            {
                this.Conexion.Open();
                this.Reader = this.Comando.ExecuteReader();
                if (this.Reader.HasRows)
                {
                    while (this.Reader.Read())
                    {
                        l.Add(this.Reader["nombre"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                throw new ArchivosException($"ERROR EN  ObtenerProductos() - {e.Message} - {e.GetBaseException()}");
            }
            finally
            {
                if (this.Conexion.State == System.Data.ConnectionState.Open) { this.Conexion.Close(); }
                this.Reader.Close();
                this.Comando.CommandText = "caca";
            }
            return l;
        }
        
    }
}
