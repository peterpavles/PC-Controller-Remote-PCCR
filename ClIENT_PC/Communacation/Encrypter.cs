using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClIENT_PC.Controller.Communacation
{
    class cryptage
    {
        private static int _cle = Parametre.CommCle;
        public static byte[] Crypt(byte[] Donne)
        {
            for (int i = 0; i < Donne.Length; i++)
            {
                Donne[i] ^= (byte)_cle;
            }
            return Donne;
        }
    }
}
