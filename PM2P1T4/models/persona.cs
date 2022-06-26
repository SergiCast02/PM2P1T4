using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM2P1T4.models
{
    public class persona
    {
        [AutoIncrement, PrimaryKey]
        public int id { get; set; }
        public String nombre { get; set; }
        public String descripcion { get; set; }
        public Byte[] foto { get; set; }
    }
}
