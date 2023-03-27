using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSQLMusicApp
{
    internal class AlbumsDAO
    {
        //data source- where does the server live, localhost - MAMP server
        string connectionString = "datasource=localhost;port=3306;username=root;password=root;database=music;";

        public List<Album> getAllAlbums()
        {
            List<Album> returnThese = new List<Album>();

            //connect to the mysql server
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //define the sql statement to fetch all albums
            MySqlCommand command = new MySqlCommand("select ID, ALBUM_TITLE, ARTIST, YEAR, IMAGE_NAME, DESCRIPTION FROM ALBUMS;", connection); 

            //reader to 
            using(MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read()) {

                    Album album = new Album
                    {
                        ID = reader.GetInt32(0),
                        AlbumName = reader.GetString(1),    
                        ArtistName  = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        ImageUrl = reader.GetString(4), 
                        Description = reader.GetString(5),
                    };
                    returnThese.Add(album);

                }

            }
            connection.Close();

            return returnThese;

        }


        public List<Album> searchTitles(String searchTerm)
        {
            List<Album> returnThese = new List<Album>();

            //connect to the mysql server
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            String searchWildPhrase = "%" + searchTerm + "%";
            //define the sql statement to fetch all albums.  @search -  parameter query, avoid sql injections
            MySqlCommand command = new MySqlCommand();
           
            command.CommandText = "select ID, ALBUM_TITLE, ARTIST, YEAR, IMAGE_NAME, DESCRIPTION FROM ALBUMS where ALBUM_TITLE LIKE @search";
            command.Parameters.AddWithValue("@search", searchWildPhrase);
            command.Connection = connection;    
           
            //reader to 
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {

                    Album album = new Album
                    {
                        ID = reader.GetInt32(0),
                        AlbumName = reader.GetString(1),
                        ArtistName = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        ImageUrl = reader.GetString(4),
                        Description = reader.GetString(5),
                    };
                    returnThese.Add(album);

                }

            }
            connection.Close();

            return returnThese;

        }

        internal int addOneAlbum(Album album)
        {
           
            //connect to the mysql server
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //define the sql statement to fetch all albums
            MySqlCommand command = new MySqlCommand("INSERT INTO `albums`( `ALBUM_TITLE`, `ARTIST`, `YEAR`, `IMAGE_NAME`, `DESCRIPTION`) VALUES (@albumTitle, @artist,@year,@imageURL,@description)", connection);

            // @artist,@year,@imageURL,@description
            command.Parameters.AddWithValue("@albumTitle", album.AlbumName);

            command.Parameters.AddWithValue("@artist", album.ArtistName);

            command.Parameters.AddWithValue("@year", album.Year);

            command.Parameters.AddWithValue("@imageURL", album.ImageUrl);

            command.Parameters.AddWithValue("@description", album.Description);

            int newRows = command.ExecuteNonQuery();

            connection.Close();

            return newRows;
        }
    }
}
