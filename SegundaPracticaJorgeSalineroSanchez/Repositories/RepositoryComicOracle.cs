using Oracle.ManagedDataAccess.Client;
using SegundaPracticaJorgeSalineroSanchez.Models;
using System.Collections.Generic;
using System;
using System.Data;
using System.Drawing;

namespace SegundaPracticaJorgeSalineroSanchez.Repositories
{
    public class RepositoryComicOracle : IRepositoryComic
    {
        #region
        //CREATE OR REPLACE PROCEDURE SP_INSERT_COMIC(P_NOMBRE COMICS.NOMBRE%TYPE, P_IMAGEN COMICS.IMAGEN%TYPE, P_DESCRIPCION COMICS.DESCRIPCION%TYPE)
        //AS
        //        P_IDCOMIC INT;
        //BEGIN

        //SELECT MAX(IDCOMIC)+1 INTO P_IDCOMIC FROM COMICS;
        //        INSERT INTO COMICS VALUES(P_IDCOMIC, P_NOMBRE, P_IMAGEN, P_DESCRIPCION);
        //        END;

        #endregion
        private OracleConnection cn;
        private OracleCommand com;
        private OracleDataAdapter adapter;
        private DataTable tablaComics;

        public RepositoryComicOracle() 
        { 
            string connectionString = @"Data Source=LOCALHOST:1521/XE; Persist Security Info=True;User Id=SYSTEM;Password=oracle";
            this.cn = new OracleConnection(connectionString);
            this.com=new OracleCommand();
            this.com.Connection = cn;
            string sql = "select * from comics";
            this.adapter = new OracleDataAdapter(sql, connectionString);
            this.tablaComics = new DataTable();
            this.adapter.Fill(tablaComics);
        }

        public List<Comic> GetComics()
        {
            var consulta = from datos in tablaComics.AsEnumerable()
                           select new Comic
                           {
                               IdComic = datos.Field<int>("IDCOMIC"),
                               Nombre = datos.Field<string>("NOMBRE"),
                               Imagen = datos.Field<string>("IMAGEN"),
                               Descripcion = datos.Field<string>("DESCRIPCION")
                           };
            return consulta.ToList();
        }

        public void InsertarComic(Comic comic)
        {
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_INSERT_COMIC";
            OracleParameter pamnombre = new OracleParameter(":NOMBRE", comic.Nombre);
            this.com.Parameters.Add(pamnombre);
             OracleParameter pamimagen = new OracleParameter(":IMAGEN", comic.Imagen);
            this.com.Parameters.Add(pamimagen);
             OracleParameter pamdescripcion = new OracleParameter(":DESCRIPCION", comic.Descripcion);
            this.com.Parameters.Add(pamdescripcion);
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();

        }
    }
}
