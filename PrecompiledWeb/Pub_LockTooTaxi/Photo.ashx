<%@ WebHandler Language="C#" Class="Photo" %>

using System;
using System.Web;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using DataLockTooTaxi;

    public class Photo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            

            string theID;
            if (context.Request.QueryString["Id"] != null)
                theID = context.Request.QueryString["Id"].ToString();
            else
                throw new ArgumentException("No parameter specified");

            context.Response.ContentType = "image/jpeg";
            Stream strm = DisplayImage(theID, context);
            byte[] buffer = new byte[100];
            int byteSeq = strm.Read(buffer, 0, 100);

            while (byteSeq > 0)
            {
                context.Response.OutputStream.Write(buffer, 0, byteSeq);
                byteSeq = strm.Read(buffer, 0, 100);
            }
        }

        public Stream DisplayImage(string theID, HttpContext context)
        {
            User user = new User();
            try
            {
                byte[] img = user.GetUnUser(theID).Photo;

                if (img == null)
                {
                    FileStream fInfo = new FileStream(context.Server.MapPath("Icons/Inconnu.jpg"), FileMode.Open);
                    img = GenerateThumbnail(fInfo, "131", "155");
                    fInfo.Close();
                }
                return new MemoryStream(img);
            }
            catch
            {
                return null;
            }

        }



        private byte[] GenerateThumbnail(Stream fStream, string xLen, string yLen)
        {
            Image img = Image.FromStream(fStream);
            Image thumbnailImage = img.GetThumbnailImage(int.Parse(xLen), int.Parse(yLen), new Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);

            MemoryStream imageStream = new MemoryStream();
            thumbnailImage.Save(imageStream, System.Drawing.Imaging.ImageFormat.Jpeg);

            byte[] imageContent = new Byte[imageStream.Length];
            imageStream.Position = 0;
            imageStream.Read(imageContent, 0, (int)imageStream.Length);
            return imageContent;
        }

        public bool ThumbnailCallback()
        {
            return true;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }