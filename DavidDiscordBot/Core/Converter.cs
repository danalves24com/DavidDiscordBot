using DSharpPlus.Interactivity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Text;

namespace DavidDiscordBot.Core
{
    public class Converter
    {

        public String textToImage(List<MessageContext> data)
        {

            string fileName = Path.GetFileNameWithoutExtension("temp-textimage") + ".jpg";
            int run = 0;
            int length = data.ToArray().Length;
            int fontSize = length < 20 ? 20 - length : 1;
            foreach (MessageContext mc in data)
            {
                Console.WriteLine(mc);
                try
                {
                    Image p = null;
                    if (run > 0)
                    {
                        p = Image.FromFile(fileName);
                    }
                    String text = mc.User.Username + ": "+ mc.Message.Content;                    
                    Bitmap bitmap = new Bitmap(1, 1);
                    Font font = new Font("Arial", fontSize + run, FontStyle.Regular, GraphicsUnit.Pixel);
                    Graphics graphics = Graphics.FromImage(bitmap);
                    int width = (int)graphics.MeasureString(text, font).Width;
                    int height = (int)graphics.MeasureString(text, font).Height;
                    int imgH = run > 0 ? p.Height : (int)(0.6 * height);
                    int imgW = run > 0 ? p.Width : (int)(0.6 * width);
                    bitmap = new Bitmap(bitmap, new Size((int)(imgW + (0.4 * width)), height + imgH));
                    graphics = Graphics.FromImage(bitmap);
                    graphics.Clear(Color.Black);
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                    if (run > 0) graphics.DrawImage(p, (int)(width * 0.4), height);
                    graphics.DrawString(text, font, new SolidBrush(Color.White), 0, 0);
                    graphics.Flush();
                    graphics.Dispose();
                    if (run > 0) p.Dispose();
                    bitmap.Save(fileName, ImageFormat.Jpeg);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                run += 1;
            }
            Console.WriteLine("done");

            return fileName;

        }
    }
}
