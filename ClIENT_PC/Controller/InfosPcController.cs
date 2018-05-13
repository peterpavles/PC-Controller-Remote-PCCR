using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ClIENT_PC.Controller
{
    class InfosPcController : ControllerBase
    {
        public string InfosPc()
        {

            string pc = SystemInformation();
            string processeur = DeviceInformation("Win32_Processor");
            string local = DeviceInformation("Win32_LogicalDisk");
            string video = DeviceInformation("Win32_VideoController");
            List<string> ListInfosPC = new List<string>
            {
                pc,
                processeur,
                local,
                video,
            };
            return serializer.Serialize(new { pc = pc, processor = processeur, local = local, video = video });
        }
        public string getUqGuid()
        {
            return FingerPrint.Value();
        }
        public string getOs()
        {
            //Get Operating system information.
            OperatingSystem os = Environment.OSVersion;
            //Get version information about the os.
            Version vs = os.Version;

            //Variable to hold our return value
            string operatingSystem = "";

            if (os.Platform == PlatformID.Win32NT)
            {
                switch (vs.Major)
                {
                    case 3:
                        operatingSystem = "NT 3.51";
                        break;
                    case 4:
                        operatingSystem = "NT 4.0";
                        break;
                    case 5:
                        if (vs.Minor == 0)
                            operatingSystem = "2000";
                        else
                            operatingSystem = "XP";
                        break;
                    case 6:
                        if (vs.Minor == 0)
                            operatingSystem = "Vista";
                        else
                            operatingSystem = "7";
                        break;
                    case 8:
                        if (vs.Minor == 0)
                            operatingSystem = "8";
                        else
                            operatingSystem = "8.1";
                        break;
                    case 10:
                        operatingSystem = "10";
                        
                        break;
                    default:
                        operatingSystem = "Unknow";
                        break;
                }
            }
            //Make sure we actually got something in our OS check
            //We don't want to just return " Service Pack 2" or " 32-bit"
            //That information is useless without the OS version.
            if (operatingSystem != "")
            {
                //Got something.  Let's prepend "Windows" and get more info.
                operatingSystem = "Windows " + operatingSystem;
                //See if there's a service pack installed.
                if (os.ServicePack != "")
                {
                    //Append it to the OS name.  i.e. "Windows XP Service Pack 3"
                    operatingSystem += " " + os.ServicePack;
                }
                //Append the OS architecture.  i.e. "Windows XP Service Pack 3 32-bit"
                //operatingSystem += " " + getOSArchitecture().ToString() + "-bit";
            }
            //Return the information we've gathered.
            return operatingSystem;
        }

        public string getMemoire()
        {
            double totalCapacite = 0;
            ObjectQuery objectQuery = new ObjectQuery("SELECT * from Win32_PhysicalMemory");
            ManagementObjectSearcher rechercheur = new ManagementObjectSearcher(objectQuery);
            ManagementObjectCollection mocs = rechercheur.Get();

            foreach (ManagementObject moc in mocs)
            {
                totalCapacite += System.Convert.ToDouble(moc.GetPropertyValue("Capacity"));
            }

            return (totalCapacite / 1024 / 1024).ToString();
        }

        /*public string getOs()
        {
            string result = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem");
            foreach (ManagementObject os in searcher.Get())
            {
                result = os["Caption"].ToString();
                break;
            }
            return result;
        }*/
        public string getProcesseur()
        {
            string infoSortie = null;
            ManagementObjectSearcher rechercheur = new ManagementObjectSearcher("SELECT maxclockspeed,  datawidth, name, manufacturer FROM Win32_Processor");
            ManagementObjectCollection objCol = rechercheur.Get();
            foreach (ManagementObject mgtObject in objCol)
            {
                infoSortie = (Convert.ToDecimal(mgtObject["maxclockspeed"]) / 1000).ToString() + "GHz ";
                infoSortie += mgtObject["datawidth"].ToString() + "bit ";
                infoSortie += mgtObject["name"].ToString();
            }
            return infoSortie;
        }
        private string SystemInformation()
        {
            StringBuilder StringBuilder1 = new StringBuilder(string.Empty);
            try
            {
                StringBuilder1.AppendFormat("Operation System:  {0}", Environment.OSVersion);
                if (Environment.Is64BitOperatingSystem)
                    StringBuilder1.AppendFormat(" 64 Bit\r\n");
                else
                    StringBuilder1.AppendFormat(" 32 Bit\r\n");
                StringBuilder1.AppendFormat("SystemDirectory:  {0}\r\n", Environment.SystemDirectory);

                StringBuilder1.AppendFormat("ProcessorCount:  {0}\r\n", Environment.ProcessorCount);
                StringBuilder1.AppendFormat("UserDomainName:  {0}\r\n", Environment.UserDomainName);
                StringBuilder1.AppendFormat("UserName: {0}\r\n", Environment.UserName);
                //Drives
                StringBuilder1.AppendFormat("LogicalDrives:\r\n");
                foreach (System.IO.DriveInfo DriveInfo1 in System.IO.DriveInfo.GetDrives())
                {
                    try
                    {
                        StringBuilder1.AppendFormat("\t Drive: {0}\r\n\t VolumeLabel: {1}\r\n\t DriveType: {2}\r\n\t DriveFormat: {3}\r\n\t TotalSize: {4}\r\n\t AvailableFreeSpace: {5}\r\n",
                            DriveInfo1.Name, DriveInfo1.VolumeLabel, DriveInfo1.DriveType, DriveInfo1.DriveFormat, DriveInfo1.TotalSize, DriveInfo1.AvailableFreeSpace);
                    }
                    catch
                    {
                    }
                }
                StringBuilder1.AppendFormat("SystemPageSize:  {0}\r\n", Environment.SystemPageSize);
                StringBuilder1.AppendFormat("Version:  {0}\r\n", Environment.Version);
            }
            catch
            {
            }
            return StringBuilder1.ToString();
        }
        private string DeviceInformation(string stringIn)
        {
            StringBuilder StringBuilder1 = new StringBuilder(string.Empty);
            try
            {
                ManagementClass ManagementClass1 = new ManagementClass(stringIn);
                ManagementObjectCollection ManagemenobjCol = ManagementClass1.GetInstances();
                PropertyDataCollection properties = ManagementClass1.Properties;
                foreach (ManagementObject obj in ManagemenobjCol)
                {
                    foreach (PropertyData property in properties)
                    {
                        try
                        {
                            StringBuilder1.AppendLine(property.Name + ":  " + obj.Properties[property.Name].Value.ToString());
                        }
                        catch
                        {
                            //Add codes to manage more informations
                        }
                    }
                    StringBuilder1.AppendLine();
                }
            }
            catch
            {
                //Win 32 Classes Which are not defined on client system
            }
            return StringBuilder1.ToString();
        }
        public string getDriver()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            List<string> Listdriver = new List<string> { };
            foreach (DriveInfo drive in drives)
            {
                double fspc = 0.0;
                double tspc = 0.0;
                double percent = 0.0;

                fspc = drive.TotalFreeSpace;
                tspc = drive.TotalSize;
                percent = (fspc / tspc) * 100;
                float num = (float)percent;
                string groupe = string.Format("Drive:{0} With {1} % free", drive.Name, num) +
                string.Format("Space Reamining:{0}", drive.AvailableFreeSpace) +
                string.Format("Percent Free Space:{0}", percent) +
                string.Format("Space used:{0}", drive.TotalSize) +
                string.Format("Type: {0}", drive.DriveType);
                Listdriver.Add(groupe);
            }
            
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(new { drivers = Listdriver });
        }
    }

    public class FingerPrint
    {
        private static string fingerPrint = string.Empty;
        public static string Value()
        {
            if (string.IsNullOrEmpty(fingerPrint))
            {
                fingerPrint = GetHash(
                    "CPU >> " + cpuId() +
                    "\nBIOS >> " + biosId() +
                    "\nBASE >> " + baseId() +
                    //+"\nDISK >> "+ diskId() + "\nVIDEO >> " + 
                    videoId() +
                    "\nMAC >> " + macId()
                                     );
            }
            return fingerPrint;
        }
        private static string GetHash(string s)
        {
            MD5 sec = new MD5CryptoServiceProvider();
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] bt = enc.GetBytes(s);
            return GetHexString(sec.ComputeHash(bt));
        }
        private static string GetHexString(byte[] bt)
        {
            string s = string.Empty;
            for (int i = 0; i < bt.Length; i++)
            {
                byte b = bt[i];
                int n, n1, n2;
                n = (int)b;
                n1 = n & 15;
                n2 = (n >> 4) & 15;
                if (n2 > 9)
                    s += ((char)(n2 - 10 + (int)'A')).ToString();
                else
                    s += n2.ToString();
                if (n1 > 9)
                    s += ((char)(n1 - 10 + (int)'A')).ToString();
                else
                    s += n1.ToString();
                if ((i + 1) != bt.Length && (i + 1) % 2 == 0) s += "-";
            }
            return s;
        }

        #region Original Device ID Getting Code
        //Return a hardware identifier
        private static string identifier
        (string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = "";
            System.Management.ManagementClass mc =
        new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString() == "True")
                {
                    //Only get the first one
                    if (result == "")
                    {
                        try
                        {
                            result = mo[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return result;
        }
        //Return a hardware identifier
        private static string identifier(string wmiClass, string wmiProperty)
        {
            string result = "";
            System.Management.ManagementClass mc =
        new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                //Only get the first one
                if (result == "")
                {
                    try
                    {
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch
                    {
                    }
                }
            }
            return result;
        }
        private static string cpuId()
        {
            //Uses first CPU identifier available in order of preference
            //Don't get all identifiers, as it is very time consuming
            string retVal = identifier("Win32_Processor", "UniqueId");
            if (retVal == "") //If no UniqueID, use ProcessorID
            {
                retVal = identifier("Win32_Processor", "ProcessorId");
                if (retVal == "") //If no ProcessorId, use Name
                {
                    retVal = identifier("Win32_Processor", "Name");
                    if (retVal == "") //If no Name, use Manufacturer
                    {
                        retVal = identifier("Win32_Processor", "Manufacturer");
                    }
                    //Add clock speed for extra security
                    retVal += identifier("Win32_Processor", "MaxClockSpeed");
                }
            }
            return retVal;
        }
        //BIOS Identifier
        private static string biosId()
        {
            return identifier("Win32_BIOS", "Manufacturer")
            + identifier("Win32_BIOS", "SMBIOSBIOSVersion")
            + identifier("Win32_BIOS", "IdentificationCode")
            + identifier("Win32_BIOS", "SerialNumber")
            + identifier("Win32_BIOS", "ReleaseDate")
            + identifier("Win32_BIOS", "Version");
        }
        //Main physical hard drive ID
        private static string diskId()
        {
            return identifier("Win32_DiskDrive", "Model")
            + identifier("Win32_DiskDrive", "Manufacturer")
            + identifier("Win32_DiskDrive", "Signature")
            + identifier("Win32_DiskDrive", "TotalHeads");
        }
        //Motherboard ID
        private static string baseId()
        {
            return identifier("Win32_BaseBoard", "Model")
            + identifier("Win32_BaseBoard", "Manufacturer")
            + identifier("Win32_BaseBoard", "Name")
            + identifier("Win32_BaseBoard", "SerialNumber");
        }
        //Primary video controller ID
        private static string videoId()
        {
            return identifier("Win32_VideoController", "DriverVersion")
            + identifier("Win32_VideoController", "Name");
        }
        //First enabled network card ID
        private static string macId()
        {
            return identifier("Win32_NetworkAdapterConfiguration",
                "MACAddress", "IPEnabled");
        }
        #endregion
    }
}
