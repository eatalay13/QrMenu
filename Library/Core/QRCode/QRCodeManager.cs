using QRCoder;
using System.Drawing;
using System.IO;

namespace Core.QRCode
{
    public class QRCodeManager
    {
        public byte[] GenerateQrCode(string value)
        {
            using QRCodeGenerator qrGenerator = new QRCodeGenerator();
            using QRCodeData qrCodeData = qrGenerator.CreateQrCode(value, QRCodeGenerator.ECCLevel.Q);
            using QRCoder.QRCode qrCode = new QRCoder.QRCode(qrCodeData);

            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            using MemoryStream stream = new MemoryStream();
            qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

            return stream.ToArray();
        }
    }
}
