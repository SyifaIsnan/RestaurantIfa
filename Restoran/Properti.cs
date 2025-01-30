using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoran
{
    internal class Properti
    {

        public static SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-18L8S2S;Initial Catalog=Restoran;Integrated Security=True");

        public static int employeeid = 0;

        public static bool number(string number)
        {
            return double.TryParse(number, out _);
        }

        public string UserRole { get; set; }

    }


}

