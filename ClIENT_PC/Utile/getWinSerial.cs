using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClIENT_PC.Controller.utile
{
    class getWinSerial
    {
        public static string GetSerial()
        {
            string strKey = "";
            try
            {
                RegistryKey RegKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion", false);
                byte[] bytDPID = (byte[])RegKey.GetValue("DigitalProductID");
                byte[] bytKey = new byte[15];

                Array.Copy(bytDPID, 52, bytKey, 0, 15);

                string strChar = "BCDFGHJKMPQRTVWXY2346789";
                for (int j = 0; j <= 24; j++)
                {
                    short nCur = 0;
                    for (int i = 14; i >= 0; i += -1)
                    {
                        nCur = Convert.ToInt16(nCur * 256 ^ bytKey[i]);
                        bytKey[i] = Convert.ToByte(Convert.ToInt32(nCur / 24));
                        nCur = Convert.ToInt16(nCur % 24);
                    }
                    strKey = strChar.Substring(nCur, 1) + strKey;
                }
                for (int i = 4; i >= 1; i += -1)
                {
                    strKey = strKey.Insert(i * 5, "-");
                }
            }
            catch
            {
                return "****-****-****-****";
            }

            return strKey;
        }
    }
}
