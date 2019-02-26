/* ==============================================================================
* 命名空间：Quick.Common.Bitmap
* 类 名 称：ImageUtils
* 创 建 者：Qing
* 创建时间：2019-01-06 20:21:55
* CLR 版本：4.0.30319.42000
* 保存的文件名：ImageUtils
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2019. All rights reserved
* ==============================================================================*/



using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using AngleSharp.Network.Default;

namespace Quick.Common.Images
{
    /// <summary>
    /// C#一些常用的图片操作方法：生成文字图片 合并图片等
    /// https://www.cnblogs.com/stulzq/p/6137715.html
    /// C#.net 2张图片合并输出
    /// https://blog.csdn.net/prospertu/article/details/51337999
    /// </summary>
    public class ImageUtils
    {
        /// <summary>
        /// 生成文字图片
        /// </summary>
        /// <param name="text"></param>
        /// <param name="isBold"></param>
        /// <param name="fontSize"></param>
        public static Image CreateImage(string text, bool isBold, int fontSize)
        {
            int wid = 400;
            int high = 200;
            Font font = isBold ? new Font("Arial", fontSize, FontStyle.Bold) : new Font("Arial", fontSize, FontStyle.Regular);
            //绘笔颜色
            SolidBrush brush = new SolidBrush(Color.Black);
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);
            Bitmap image = new Bitmap(wid, high);
            Graphics g = Graphics.FromImage(image);
            SizeF sizeF = g.MeasureString(text, font, PointF.Empty, format);//得到文本的宽高
            int width = (int)(sizeF.Width + 1);
            int height = (int)(sizeF.Height + 1);
            image.Dispose();
            image = new Bitmap(width, height);
            g = Graphics.FromImage(image);
            g.Clear(Color.White);//透明

            RectangleF rect = new RectangleF(0, 0, width, height);
            //绘制图片
            g.DrawString(text, font, brush, rect);
            //释放对象
            g.Dispose();
            return image;
        }

        /// <summary>  
        /// 上下拼接图片  
        /// </summary>  
        /// <param name="imgBack">上方图片</param>  
        /// <param name="img">下方图片</param>
        /// <param name="xDeviation">距离上方图片的左边距</param>
        /// <param name="yDeviation">距离上方图片的上边距</param>
        /// <returns></returns>
        public static Bitmap CombineImage(Image imgBack, Image img, int xDeviation = 0, int yDeviation = 0)
        {
            Bitmap bmp = new Bitmap(imgBack.Width, imgBack.Height + img.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高); 
            g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);     
            //g.FillRectangle(System.Drawing.Brushes.White, imgBack.Width / 2 - img.Width / 2 - 1, imgBack.Width / 2 - img.Width / 2 - 1,1,1);//相片四周刷一层黑色边框    
            //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);    
            g.DrawImage(img, imgBack.Width / 2 - img.Width / 2 + xDeviation, imgBack.Height + yDeviation, img.Width, img.Height);
            GC.Collect();
            return bmp;
        }

        /// <summary>  
        /// 重叠覆盖图片  
        /// </summary>  
        /// <param name="imgBack">底层图片</param>  
        /// <param name="img">顶层图片</param>
        /// <param name="xDeviation">距离底层图片的左边距</param>
        /// <param name="yDeviation">距离底层图片的上边距</param>
        /// <returns></returns>
        public static Bitmap CoverImage(Image imgBack, Image img, int xDeviation = 0, int yDeviation = 0)
        {
            Bitmap bmp = new Bitmap(imgBack.Width, imgBack.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);  
            g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);    
            //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);    
            g.DrawImage(img, xDeviation, yDeviation, img.Width, img.Height);
            GC.Collect();
            return bmp;
        }

        /// <summary>  
        /// Resize图片  
        /// </summary>  
        /// <param name="bmp">原始Bitmap</param>  
        /// <param name="newW">新的宽度</param>  
        /// <param name="newH">新的高度</param>  
        /// <param name="mode">保留着，暂时未用</param>  
        /// <returns>处理以后的图片</returns>  
        public static Image ResizeImage(Image bmp, int newW, int newH, int mode)
        {
            try
            {
                Image b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);

                // 插值算法的质量    
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height),
                    GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        }

        /* MemoryStream保存到图片：
         *  Image imgBack = Image.FromFile(imgBackPath);
         *  Image img = Image.FromFile(imgPath);
         *  Bitmap bmp = CombineImage(imgBack, img);
         *  MemoryStream ms = new MemoryStream();
         *  bmp.Save(ms, ImageFormat.Png);
         */
        
        /// <summary>
        /// 保存图片到本地
        /// </summary>
        /// <param name="map"></param>
        /// <param name="savePath"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string SaveImage(Bitmap map,string savePath,ImageFormat format)
        {
            if(format == null)
                format  = ImageFormat.Png;
            map.Save(savePath, format);
            return savePath;
        }

        /// <summary>  
        /// 重叠覆盖图片并保存为第三张图片 
        /// </summary>  
        /// <param name="imgBackPath">底层图片</param>  
        /// <param name="imgPath">顶层图片</param>
        /// <param name="savePath">保存路径：</param>
        /// <param name="xDeviation">距离底层图片的左边距</param>
        /// <param name="yDeviation">距离底层图片的上边距</param>
        /// <returns></returns>
        public static string CoverAndSaveImage(string imgBackPath,string imgPath,string savePath ,int xDeviation = 0, int yDeviation = 0)
        {
            Image imgBack = Image.FromFile(imgBackPath);
            Image img = Image.FromFile(imgPath);
            Bitmap bmp = CombineImage(imgBack, img);
            if(!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);
            string fileName = savePath + Guid.NewGuid() + ".png";
            bmp.Save(fileName, ImageFormat.Png);
            return fileName;
        }
    }
}
