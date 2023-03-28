using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSQLMusicApp
{
    internal class Track
    {
        public int ID { get; set; }
        public string Name { get; set; }    
        public int Number { get; set; }

        public string videoURL { get; set; }

        public string lyrics { get; set; }

    }
}
