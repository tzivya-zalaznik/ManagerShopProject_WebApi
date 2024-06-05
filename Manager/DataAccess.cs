using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Manager
{
    class DataAccess
    {
        public static void CreateItem(string connectionString)
        {
            string letter;
            string toContinue = "y";
            while (toContinue == "y")
            {
                Console.WriteLine("press p to create a product and c for category");
                letter = Console.ReadLine();
                if (letter == "p")
                {
                    InsertProduct(connectionString);
                }
                if (letter == "c")
                {
                    InsertCategory(connectionString);
                }

                Console.WriteLine("would you want to continue? y/n");
                toContinue = Console.ReadLine();
            }
            Console.WriteLine("Categories:");
            ReadCategory(connectionString);

            Console.WriteLine("Products:");
            ReadProduct(connectionString);
            Console.Read();
        }
        public static int InsertProduct(string connectionString)
        {
            string ProductName, Price, CategoryId, Description, ImageUrl;

            Console.WriteLine("Insert ProductName");
            ProductName = Console.ReadLine();

            Console.WriteLine("Insert Price");
            Price = Console.ReadLine();

            Console.WriteLine("Insert CategoryId");
            CategoryId = Console.ReadLine();

            Console.WriteLine("Insert Description");
            Description = Console.ReadLine();

            Console.WriteLine("Insert ImageUrl");
            ImageUrl = Console.ReadLine();

            string query = "INSERT INTO Products(ProductName, Price, CategoryId, Description, ImageUrl)" +
                            "VALUES (@ProductName, @Price, @CategoryId, @Description, @ImageUrl)";

            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cmd.Parameters.Add("@ProductName", SqlDbType.VarChar, 30).Value = ProductName;
                cmd.Parameters.Add("@Price", SqlDbType.Float).Value = Price;
                cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar, 300).Value = Description;
                cmd.Parameters.Add("@ImageUrl", SqlDbType.VarChar, 40).Value = ImageUrl;

                cn.Open();
                int rowsEffected = cmd.ExecuteNonQuery();
                cn.Close();

                return rowsEffected;
            }
        }

        public static int InsertCategory(string connectionString)
        {
            string CategoryName;

            Console.WriteLine("Insert CategoryName");
            CategoryName = Console.ReadLine();

            string query = "INSERT INTO Categories(CategoryName)" +
                            "VALUES (@CategoryName)";

            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar, 30).Value = CategoryName;

                cn.Open();
                int rowsEffected = cmd.ExecuteNonQuery();
                cn.Close();

                return rowsEffected;

            }
        }
        static public void ReadProduct(string connectionString)
        {
            string queryString = "select * from Products";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}",
                            reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                //Console.ReadLine();
            }
        }

        static public void ReadCategory(string connectionString)
        {
            string queryString = "select * from Categories";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine("\t{0}\t{1}",
                            reader[0], reader[1]);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                //Console.ReadLine();
            }
        }
    }
}
