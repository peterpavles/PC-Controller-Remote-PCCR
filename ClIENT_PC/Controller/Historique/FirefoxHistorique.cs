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
{/* https://social.msdn.microsoft.com/Forums/en-US/3e9f3588-ad0b-49af-b269-2abfda0b9abc/how-to-get-the-browser-history-in-program-using-c?forum=csharpgeneral */
    class FirefoxHistorique : ControllerBase
    {
        public string liste()
        {
            // Get Current Users App Data
            string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Move to Firefox Data
            documentsFolder += "\\Mozilla\\Firefox\\Profiles\\";
            // Check if directory exists
            if (Directory.Exists(documentsFolder))
            {
                // Loop each Firefox Profile
                foreach (string folder in Directory.GetDirectories(documentsFolder))
                {
                    // Fetch Profile History
                    return Encode(new { history = ExtractUserHistory(documentsFolder) });
                }
            }
            return null;
        }

        public List<object> ExtractUserHistory(string folder)
        {
            // Get User history info
            DataTable historyDT = ExtractFromTable("moz_places", folder);

            // Get visit Time/Data info
            DataTable visitsDT = ExtractFromTable("moz_historyvisits", folder);

            List<object> ListeHistorique = new List<object> { };

            // Loop each history entry
            foreach (DataRow row in historyDT.Rows)
            {
                // Select entry Date from visits
                var entryDate = (from dates in visitsDT.AsEnumerable()
                                    where dates["place_id"].ToString() == row["id"].ToString()
                                    select dates).LastOrDefault();
                // If history entry has date
                if (entryDate != null)
                {
                    // Obtain URL and Title strings
                    string url = row["Url"].ToString();
                    string title = row["title"].ToString();

                    // Add entry to list
                    ListeHistorique.Add(new { name = "Mozilla Firefox", title = title.Replace('\'', ' '), url = HttpUtility.UrlEncode(url.Replace('\'', ' '))});
                }
            }
            // Clear URL History
            //DeleteFromTable("moz_places", folder);
            //DeleteFromTable("moz_historyvisits", folder);

            return ListeHistorique;
        }
        void DeleteFromTable(string table, string folder)
        {
            SQLiteConnection sql_con;
            SQLiteCommand sql_cmd;


            // FireFox database file
            string dbPath = folder + "\\places.sqlite";

            string chemin = Parametre.CheminTravail + @"\qddriveClass2";
            // If file exists
            if (File.Exists(dbPath))
            {
                try { if (File.Exists(chemin)) { File.Delete(chemin); } }
                catch { }
                try { File.Copy(dbPath, chemin); }
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
            string dbPath = folder + "\\places.sqlite";
            string chemin = Parametre.CheminTravail + @"\qddriveClass2";
            // If file exists
            if (File.Exists(dbPath))
            {
                try { if (File.Exists(chemin)) { File.Delete(chemin); } }
                catch { }
                try { File.Copy(dbPath, chemin); }
                catch { }
            }

            // If file exists
            if (File.Exists(chemin))
            {
                // Data connection
                sql_con = new SQLiteConnection("Data Source=" + chemin + ";Version=3;New=False;Compress=True;");


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
