using SegundaPracticaJorgeSalineroSanchez.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SegundaPracticaJorgeSalineroSanchez.Repositories
{
    #region
    //CREATE PROCEDURE SP_INSERT_COMIC(@NOMBRE NVARCHAR(50),@IMAGEN NVARCHAR(500),@DESCRIPCION NVARCHAR(50))
    //AS
    //DECLARE @IDCOMIC INT
    //SET @IDCOMIC = (SELECT MAX(IDCOMIC)+1 FROM COMICS);
    //INSERT INTO COMICS VALUES(@IDCOMIC, @NOMBRE, @IMAGEN, @DESCRIPCION)
    //go


    #endregion
    public class RepositoryComicSql : IRepositoryComic
    {
        private SqlConnection cn;
        private SqlCommand com;
        private SqlDataAdapter adapter;
        private DataTable tablaComics;

        public RepositoryComicSql()
        {
            string connectionString = @"Data Source=DESKTOP-AIUEHVJ\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2023";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = cn;
           
            string sql = "select * from comics";
            this.adapter = new SqlDataAdapter(sql,connectionString);
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
            SqlParameter pamnombre = new SqlParameter("@NOMBRE", comic.Nombre);
            this.com.Parameters.Add(pamnombre);
            SqlParameter pamimagen = new SqlParameter("@IMAGEN", comic.Imagen);
            this.com.Parameters.Add(pamimagen);
            SqlParameter pamdescripcion = new SqlParameter("@DESCRIPCION", comic.Descripcion);
            this.com.Parameters.Add(pamdescripcion);

            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
    }
}
