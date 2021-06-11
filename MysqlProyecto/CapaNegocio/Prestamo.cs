using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace MysqlProyect.CapaNegocio
{
    public class Prestamo : IPrestamo

    {
        private static string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
        private static MySqlConnection conexion = new MySqlConnection(cadena);
        public void Actualizar(string codAutor, string codLibro)
        {
            string consulta = "update tprestamo set CodLibro = @codLibro where  CodAutor = @codAutor ";
            MySqlCommand comando = new MySqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("@CodAutor", codAutor);
            comando.Parameters.AddWithValue("@CodLibro", codLibro);
            conexion.Open();
            conexion.Close();
        }

        public void Agregar(string codAutor, string codLibro, string fechaPrestamo)
        {
            string consulta = "Insert into tprestamo values(@codAutor,@codlibro,@fechaPrestamo)";
            MySqlCommand comando = new MySqlCommand(consulta, conexion);

            // envio de parametros
            comando.Parameters.AddWithValue("@codlibro", codLibro);
            comando.Parameters.AddWithValue("@codAutor", codAutor);
            comando.Parameters.AddWithValue("@fechaPrestamo", fechaPrestamo);
           
        }

        public DataTable Buscar(string texto, string criterio)
        {
            string consulta = " SELECT CodAutor, CodLibro, FechaPrestamo FROM tprestamo WHERE CodLibro LIKE '" + texto + "' LIMIT 1";
            MySqlCommand comando = new MySqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("LIKE", texto);
            comando.Parameters.AddWithValue("where", criterio);
            conexion.Open();
            DataTable tabla = new DataTable();
            conexion.Close();
            return tabla;
        }

        public void Eliminar(string fechaPrestamo)
        {
            string consulta = "delete from tprestamo where fechaprestamo ='" + fechaPrestamo + "'";
            MySqlCommand comando = new MySqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("fechaprestamo", fechaPrestamo);
            conexion.Open();
            byte i = Convert.ToByte(comando.ExecuteNonQuery());
            conexion.Close();
        }
    

        public DataTable Listar()
        {
            string consulta = "select * from tprestamo";
            MySqlCommand comando = new MySqlCommand(consulta, conexion);   // lleva la consulta a la base de datos 
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);      // trae la consulta 
            DataTable tabla = new DataTable();
            adapter.Fill(tabla);
            return tabla;
        }
    }
}
