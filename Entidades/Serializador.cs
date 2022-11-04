using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Entidades
{
    public static class Serializador <T>
    {
        public static string ruta = ManejadorDB.initialDirectory + @"\Media\Archivos\";
        #region Xml
        public static void SerializarAXml(T objeto, string nombre)
        {
            XmlTextWriter writer = null;
            string rutaAbsoluta = ruta + nombre + ".xml";
            try
            {
                writer = new XmlTextWriter(rutaAbsoluta, Encoding.UTF8);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, objeto);
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new ArchivosException("Error: Directorio no encontrado.", ex);
            }
            catch (Exception ex)
            {
                throw new ArchivosException("Error inesperado", ex);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
        public static void SerializarAXml(List<T> lista, string nombre)
        {
            XmlTextWriter writer = null;
            string rutaAbsoluta = ruta + nombre + ".xml";
            try
            {
                writer = new XmlTextWriter(rutaAbsoluta, Encoding.UTF8);
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                serializer.Serialize(writer, lista);
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new ArchivosException("Error: Directorio no encontrado.", ex);
            }
            catch (Exception ex)
            {
                throw new ArchivosException("Error inesperado", ex);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
        public static T DeserializarXml(string ruta)
        {
            XmlTextReader reader = null;
            T objeto = default(T);
            try
            {
                reader = new XmlTextReader(ruta);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                objeto = (T)serializer.Deserialize(reader);
            }
            catch (Exception ex)
            {
                throw new ArchivosException("Ocurrió un error al tratar de deserializar el archivo XML.", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return objeto;
        }
        #endregion

        #region Txt
        //public static string LeerTxt(string ruta, string nombre)
        //{
        //    StreamReader streamRead = null;
        //    try
        //    {
        //        streamRead = new StreamReader(ruta);
        //        string text = string.Empty;
        //        string newLine = streamRead.ReadLine();
        //        while (newLine != null)
        //        {
        //            text += newLine + "\n";
        //            newLine = streamRead.ReadLine();
        //        }
        //        return text;
        //    }
        //    finally
        //    {
        //        if (streamRead != null)
        //        {
        //            streamRead.Close();
        //        }
        //    }
        //}

        public static void SerializarATxt(string texto, string nombre)
        {
            StreamWriter streamWriter = null;
            string rutaAbsoluta = ruta + @"\" + nombre + ".txt";
            try
            {
                streamWriter = new StreamWriter(rutaAbsoluta, false);
                streamWriter.WriteLine(texto);
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Close();
                }
            }
        }
        #endregion

        # region Json
        public static void SerializeJson(T objeto, string nombre)
        {
            string rutaAbsoluta = ruta + nombre +".json";
            string jsonString = JsonSerializer.Serialize(objeto);
            try { 
                File.WriteAllText(rutaAbsoluta, jsonString);
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new ArchivosException("Error: Directorio no encontrado.", ex);
            }
            catch (Exception ex)
            {
                throw new ArchivosException("Error inesperado", ex);
            }
        }
        public static void SerializeJson(List<T> lista, string nombre)
        {
            string rutaAbsoluta = ruta + nombre + ".json";
            //int i = lista.Count();
            //string jsonString = "{\n";
            //foreach (var item in lista)
            //{
            //    jsonString = jsonString + JsonSerializer.Serialize(item);
            //    i--;
            //    if (i != 0) { jsonString = jsonString + ","; }
            //}
            //jsonString = jsonString + "\n}";
            string jsonString = JsonSerializer.Serialize(lista);
            try
            {
                File.WriteAllText(rutaAbsoluta, jsonString);
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new ArchivosException("Error: Directorio no encontrado.", ex);
            }
            catch (Exception ex)
            {
                throw new ArchivosException("Error inesperado", ex);
            }
        }
        # endregion
    }
}