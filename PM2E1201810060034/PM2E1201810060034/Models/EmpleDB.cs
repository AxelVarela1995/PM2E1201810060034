using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PM2E1201810060034.Models
{
    class EmpleDB
    {
        [PrimaryKey, AutoIncrement]

        public int id { get; set; }

        [MaxLength(100)]
        public double latitud { get; set; }
        [MaxLength(100)]
        public double longitud { get; set; }

        public String descripcion { get; set; }

        public String base64 { get; set; }

    }
}
