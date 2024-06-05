using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Manager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connString = "Data Source=srv2\\pupils;Initial Catalog=AdoNetUsers_214956807;Integrated Security=True;";

            //DataAccess.InsertData(connString);
            DataAccess.CreateItem(connString);
        }
    }
}