using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CatsShop
{

    internal class DBManager: IDisposable   
    {
        public void Dispose()
        {
            connect_.Close();
        }

        static string path = @"Data Source=KOMPUTER\SQLEXPRESS;
                              Initial Catalog=mur;
                              Integrated Security=SSPI;";

        private SqlConnection connect_;

        public DBManager()
        {
            connect_ = new SqlConnection(path);
            connect_.Open();
        }

        public void Add(int age, decimal price, string color)
        {
            var command = new SqlCommand(@"insert into Cats
                                          values(
                                         @age, @price, @color
                                         );", connect_);

            command.Parameters.Add(@"age", sqlDbType: System.Data.SqlDbType.Int, sizeof(int)).Value = age;
            command.Parameters.Add(@"price", System.Data.SqlDbType.Money, sizeof(decimal)).Value = price;
            command.Parameters.Add(@"color", System.Data.SqlDbType.NVarChar, color.Length).Value = color;
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            var command = new SqlCommand("delete from Cats where id=@id",connect_);
            command.Parameters.Add("@id", System.Data.SqlDbType.Int,sizeof(int)).Value=id;
            command.ExecuteNonQuery();  
        }


        public Dictionary<int, string> GetAllCats() {
            var command = new SqlCommand("select * from Cats", connect_);
            var list = new Dictionary<int, string>(1000);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    int age = reader.GetInt32(1);
                    decimal price = reader.GetDecimal(2);
                    string color = reader.GetString(3);

                    list.Add(id, $"age={age}, price={price}, color={color}");
                }
            }
            return list;
        }

    }
}
