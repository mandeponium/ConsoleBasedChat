using System;
using System.Data.SqlClient;
using System.Threading;

namespace ConsoleBasedChat
{
    class Program
    {
		//Edit this connection string to be your database.
        public static string connectionString = "Data Source=*;Initial Catalog=*;Persist Security Info = True;User ID=*;Password=*;";
		
        public static DateTime msgTime;
        public static DateTime insertTime;
        public static string sqlFormattedDate = msgTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        public static string typedMessage = "";
        public static string windowsID = Environment.UserName;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to chat!");
            Console.WriteLine("Begin:");
            msgTime = DateTime.Now;

            do
            {
                while (Console.KeyAvailable == false)
                {
                    GetNewMessages();
                    Thread.Sleep(1000);
                }

                SendMessage();

            } while (true);
        }

        public static void SendMessage()
        {
            typedMessage = Console.ReadLine();
            DeletePrevConsoleLine();

            SqlConnection conn = new SqlConnection(connectionString);
            
            string stmt = "INSERT INTO dbo.TextMessage(MessageBody, MessageTime) VALUES(@message, @sqlFormattedDate)";
            SqlCommand cmd1 = new SqlCommand(stmt, conn);

            insertTime = DateTime.Now;
            sqlFormattedDate = insertTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

            conn.Open();
            cmd1.Parameters.AddWithValue("@message", windowsID + ": " + typedMessage);
            cmd1.Parameters.AddWithValue("@sqlFormattedDate", sqlFormattedDate);
            cmd1.ExecuteNonQuery();
            conn.Close();
        }

        public static void GetNewMessages()
        {

            sqlFormattedDate = msgTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT MessageBody from TextMessage WHERE MessageTime > '" + sqlFormattedDate + "'", conn);

            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            msgTime = DateTime.Now;

            while (reader.Read())
            {
                Console.WriteLine(reader.GetString(0));
            }
            conn.Close();
        }

        private static void DeletePrevConsoleLine()
        {
            if (Console.CursorTop == 0) return;
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

    }
}
