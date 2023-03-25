using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSQLMusicApp
{
    
    internal class Album
    {
        public int ID { get; set; } 
        public string AlbumName { get; set; }               
        public string ArtistName { get; set; }
        public int Year { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
}
