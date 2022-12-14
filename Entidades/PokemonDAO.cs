using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class PokemonDAO:ManejadorDB, iCRUDInterface<Pokemon>
    {
        public enum Filtro { entrenador, tipo, rango, personalizado };
        public bool Guardar(Pokemon p)
        {
            string comando = "INSERT INTO dbo.Pokemon " +
            "(id, nombre, tipo, entrenador, urlImagen) VALUES (@id, @nombre, @tipo, @entrenador, @urlImagen);";
            //Reader.Close();
            this.Comando.CommandText = comando;

            Comando.Parameters.Clear();
            int id = p.Id;
            int type = p.Tipo;
            string nombre = p.Nombre;
            string trianer = p.Entrenador;
            string url = p.UrlImagen;

            Comando.Parameters.AddWithValue("@id", id);//ok
            Comando.Parameters.AddWithValue("@nombre", nombre);//ok
            Comando.Parameters.AddWithValue("@tipo", type);
            Comando.Parameters.AddWithValue("@entrenador", trianer);
            Comando.Parameters.AddWithValue("@urlImagen", url);

            return Ejecutar();
        }
        public List<Pokemon> LeerConFiltro(string auxQuery, string criterio, int min, int max, PokemonDAO.Filtro f)
        {
            List<Pokemon> l = new List<Pokemon>();
            Pokemon item = null;
            string query = "";
            if (f == PokemonDAO.Filtro.entrenador) { 
                query = $"SELECT * FROM dbo.Pokemon WHERE entrenador = '{criterio}'"; 
            } else if (f == PokemonDAO.Filtro.tipo) {
                int tipo = Pokemon.ObtenerTipoIndice(criterio);
                query = $"SELECT * FROM dbo.Pokemon WHERE tipo = '{tipo}'";
            } else if (f == PokemonDAO.Filtro.rango) {
                query = $"SELECT * FROM dbo.Pokemon WHERE id BETWEEN {min} AND {max}";
            } else {
                query = auxQuery;
            }
            
            Comando.Parameters.Clear();
            Comando.CommandText = query;
            try
            {
                Conexion.Open();
                Reader = Comando.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        item = new Pokemon(
                             Convert.ToInt32(Reader["id"]),
                             Reader["nombre"].ToString(),
                             Convert.ToInt32(Reader["tipo"]),
                             Reader["entrenador"].ToString(),
                             Reader["urlImagen"].ToString()
                         );
                        l.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                throw new ArchivosException($"ERROR EN  LeerConFiltro() - {e.Message} - {e.GetBaseException()}");
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open) { Conexion.Close(); }
                Reader.Close();
            }
            return l;
        }
        public Pokemon Leer(string? nombre, int? id)
        {
            Pokemon item = null;
            if (nombre != null && id is null) {
                Comando.CommandText = $"SELECT * FROM dbo.Pokemon WHERE nombre = '{nombre}'";
            }
            else { Comando.CommandText = $"SELECT * FROM dbo.Pokemon WHERE id = {id}"; }
            try
            {
                Conexion.Open();
                Reader = Comando.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        item = new Pokemon(
                            Convert.ToInt32(Reader["id"]),
                            Reader["nombre"].ToString(),
                            Convert.ToInt32(Reader["tipo"]),
                            Reader["entrenador"].ToString(),
                            Reader["urlImagen"].ToString()
                        );
                    }
                }
            }
            catch (Exception e)
            {
                throw new ArchivosException($"ERROR EN  Leer() - {e.Message} - {e.GetBaseException()}");
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open) { Conexion.Close(); }
                Reader.Close();
            }
            return item;
        }    
        public bool Borrar(int id)
        {
            Comando.CommandText = "DELETE FROM dbo.Pokemon WHERE ID = @id";
            try
            {
                Comando.Parameters.Clear();
                Comando.Parameters.AddWithValue("@id", id);
                return Ejecutar();
            }
            catch (Exception e)
            {
                throw new ArchivosException($"ERROR EN  Borrar() - {e.Message} - {e.GetBaseException()}");
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open) { Conexion.Close(); }
            }
        }
        public bool Modificar(Pokemon p)
        {
            //    ManejadorDB.Comando.CommandText = "UPDATE dbo.Persona " +
            //       $"SET nombre = @nombre, apellido= @apellido WHERE ID = {p.ID}";

            //    ManejadorDB.Comando.Parameters.Clear();
            //    ManejadorDB.Comando.Parameters.AddWithValue("@nombre", p.Nombre);
            //    ManejadorDB.Comando.Parameters.AddWithValue("@apellido", p.Apellido);

            //    return ManejadorDB.Ejecutar();
            //}
            //public static bool Borrar(int id)
            //{
            //    ManejadorDB.Comando.CommandText = "DELETE FROM dbo.Persona WHERE ID = @id";
            //    try
            //    {
            //        ManejadorDB.Comando.Parameters.Clear();
            //        ManejadorDB.Comando.Parameters.AddWithValue("@id", id);
            //        return ManejadorDB.Ejecutar();
            //    }
            //    catch (Exception e)
            //    {
            //        throw new ArchivosException($"ERROR EN  ObtenerProductos() - {e.Message} - {e.GetBaseException()}");
            //    }
            //    finally
            //    {
            //        if (ManejadorDB.Conexion.State == System.Data.ConnectionState.Open) { ManejadorDB.Conexion.Close(); }
            //    }



            return false;
        }
        public List<String> LeerListaDeEntrenadores()
        {
            List<String> l = new List<String>();            
            Comando.CommandText = $"SELECT DISTINCT entrenador FROM dbo.Pokemon";
            try
            {
                Conexion.Open();
                Reader = Comando.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        l.Add(Reader["entrenador"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                throw new ArchivosException($"ERROR EN  LeerListaDeEntrenadores() - {e.Message} - {e.GetBaseException()}");
            }
            finally
            {
                if (Conexion.State == System.Data.ConnectionState.Open) { Conexion.Close(); }
                Reader.Close();
            }
            return l;
        }
        
    }
}
