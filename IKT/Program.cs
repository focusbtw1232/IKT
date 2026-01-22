using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Users
{
    internal class Program
    {
        public static string ConnectionString = "server=localhost;database=users;user=root;password=";
        public static MySqlConnection Conn = new MySqlConnection(ConnectionString);


        public static void InsertData(string name, string email, string password)
        {
            Conn.Open();
            string sql = "INSERT INTO `datas`(`Name`, `Email`, `Password`) VALUES (@name, @email, @password)";

            MySqlCommand cmd = new MySqlCommand(sql, Conn);


            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);



            cmd.ExecuteNonQuery();

            Conn.Close();

        }

        public static void ReadData()
        {
            Conn.Open();
            string sql = "SELECT * FROM datas";

            MySqlCommand cmd = new MySqlCommand(sql, Conn);

            MySqlDataReader dr = cmd.ExecuteReader();


            while (dr.Read())
            {
                Console.WriteLine($"Id: {dr.GetInt32(0)}, Name: {dr.GetString(1)}, Email: {dr.GetString(2)}, Password: {dr.GetString(3)},  RegTime: {dr.GetDateTime(5)}");
                Console.WriteLine("-------------------------------------------------------------------------------------------");

            }
            Console.ReadKey();

            Conn.Close();
        }

        public static void UpdateUser(int id, string name, string email, string password)
        {
            Conn.Open();
            string sql = "UPDATE `datas` SET `Name`='@name',`Email`='@email',`Password`='@password' WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(sql, Conn);


            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        public static void DeleteUser(int id)
        {
            Conn.Open();

            string sql = "DELETE FROM datas WHERE Id = @id";

            MySqlCommand cmd = new MySqlCommand(sql, Conn);

            cmd.Parameters.AddWithValue("@id", id);




            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        static void Main(string[] args)
        {

            while (true)
            {

                Console.WriteLine("1. Lekérdezés");
                Console.WriteLine("2. Beszúrás");
                Console.WriteLine("3. Módosítás");
                Console.WriteLine("4. Törlés");
                Console.WriteLine("5. Kilépés");
                Console.Write("Válassz menüpontot: ");
                byte menu = byte.Parse(Console.ReadLine());


                switch (menu)
                {
                    case 1: Console.Clear(); ReadData(); Console.Clear(); break;
                    case 2:
                        #region bekeres
                        Console.Clear();

                        Console.Write($"Kérem a nevet: ");
                        string name = Console.ReadLine();

                        Console.Write($"Kérem az emailt: ");
                        string email = Console.ReadLine();

                        Console.Write($"Kérem a jelszót: ");
                        string password = Console.ReadLine();
                        #endregion
                        InsertData(name, email, password); Console.Clear(); break;
                    case 3:
                        Console.Clear();
                        #region bekeres
                        Console.Clear();

                        Console.Write($"Kérem az azonosítót: ");
                        int id1 = int.Parse(Console.ReadLine());

                        Console.Write($"Kérem a nevet: ");
                        string name1 = Console.ReadLine();

                        Console.Write($"Kérem az emailt: ");
                        string email1 = Console.ReadLine();

                        Console.Write($"Kérem a jelszót: ");
                        string pass = Console.ReadLine();
                        #endregion
                        UpdateUser(id1, name1, email1, pass); Console.Clear(); break;
                    case 4:
                        Console.Clear();
                        #region bekeres
                        Console.Write("Kérem a törlendő user Id-jét: ");
                        int id = int.Parse(Console.ReadLine());
                        #endregion
                        DeleteUser(id); Console.Clear(); break;
                    case 5: System.Environment.Exit(0); break;
                    default: Console.Clear(); Console.WriteLine("Nincs ilyen menüpont! A felsorolt menüpontok közül válasszon!"); break;
                }
            }



        }
    }
}

