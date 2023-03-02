using System;
using System.Data.SqlClient;

namespace ConsoleApp9
{
    internal class Program
    {
        static string path = @"Data Source=KOMPUTER\SQLEXPRESS;
                              Initial Catalog=master;
                              Integrated Security=SSPI;";


        static void Main(string[] args)
        {
            SqlConnection connection= new SqlConnection(path);
            connection.Open();  
            CreateDB(connection);
            connection.ChangeDatabase("mur");  
            CreateTableCat(connection);
            connection.Close();
        }

        private static void CreateTableCat(SqlConnection connection)
        {
            try
            {
                string create = @"create table Cats
                                    (
                                    [id] int identity(1,1) not null primary key,
                                    [age] int,
                                    [price] money not null,
                                    [color] varchar(50) not null
                                    );";
                SqlCommand command = new SqlCommand(create, connection);
                command.ExecuteNonQuery();

            } catch(SqlException ex) { 
                Console.WriteLine(ex.Message);
               // Console.WriteLine("Table already created");
            }

        }

        private static void CreateDB(SqlConnection connection)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "create database mur"; //name cmd
                cmd.Connection = connection;
                cmd.ExecuteNonQuery(); //play command
                Console.WriteLine("Done");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Base already created");
            }
        }
    }
}
