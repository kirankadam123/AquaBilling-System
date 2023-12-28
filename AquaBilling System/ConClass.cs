using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaBilling_System
{
   
        public static class ConnectionStringProvider
        {
            public static string ConnectionString { get; } = "Data Source=DESKTOP-IEK3SPK\\SQLEXPRESS;Initial Catalog=Aqua;Integrated Security=True";
        }

    
}

