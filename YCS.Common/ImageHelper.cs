using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ThoughtWorks.QRCode.Codec.Util;
using drawing = System.Drawing;

namespace YCS.Common
{
    /// <summary>
    /// 生成缩略图/水印类
    /// Create by Jimy
    /// </summary>
    public class ImageHelper
    {
        #region 生成缩略图
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode, ImageFormat format)
        {
            originalImagePath = Config.GetMapPath(originalImagePath);
            thumbnailPath = Config.GetMapPath(thumbnailPath);
            Image originalImage = Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形） 
                    if (ow > width || oh > height)
                    {
                        if ((double)originalImage.Width / (double)originalImage.Height > (double)width / (double)height)
                        {
                            oh = originalImage.Height;
                            ow = originalImage.Height * width / height;
                            y = 0;
                            x = (originalImage.Width - ow) / 2;
                        }
                        else
                        {
                            ow = originalImage.Width;
                            oh = originalImage.Width * height / width;
                            x = 0;
                            y = (originalImage.Height - oh) / 2;
                        }
                    }
                    else
                    {
                        //原图保存
                        towidth = ow;
                        toheight = oh;
                    }
                    break;
                case "CutScale"://指定高宽裁减按比例
                    int TW = originalImage.Width * height / originalImage.Height;
                    int TH = originalImage.Height * width / originalImage.Width;
                    if (ow > width)
                    {
                        if (TH > height)
                        {
                            towidth = TW;
                            toheight = height;
                        }
                        else
                        {
                            towidth = width;
                            toheight = TH;
                        }
                    }
                    else
                    {
                        if (oh > height)
                        {
                            towidth = TW;
                            toheight = height;
                        }
                        else
                        {
                            towidth = ow;
                            toheight = oh;
                        }
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            Image bitmap = new Bitmap(towidth, toheight);

            //新建一个画板
            Graphics g = Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                if (!File.Exists(@thumbnailPath))
                    bitmap.Save(thumbnailPath, format);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalData">图片二进制数据</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public static void MakeThumbnail(byte[] originalData, string thumbnailPath, int width, int height, string mode, ImageFormat format)
        {
            thumbnailPath = Config.GetMapPath(thumbnailPath);
            MemoryStream ms = new MemoryStream(originalData);
            Image originalImage = Image.FromStream(ms);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形） 
                    if (ow > width || oh > height)
                    {
                        if ((double)originalImage.Width / (double)originalImage.Height > (double)width / (double)height)
                        {
                            oh = originalImage.Height;
                            ow = originalImage.Height * width / height;
                            y = 0;
                            x = (originalImage.Width - ow) / 2;
                        }
                        else
                        {
                            ow = originalImage.Width;
                            oh = originalImage.Width * height / width;
                            x = 0;
                            y = (originalImage.Height - oh) / 2;
                        }
                    }
                    else
                    {
                        //原图保存
                        towidth = ow;
                        toheight = oh;
                    }
                    break;
                case "CutScale"://指定高宽裁减按比例
                    int TW = originalImage.Width * height / originalImage.Height;
                    int TH = originalImage.Height * width / originalImage.Width;
                    if (ow > width)
                    {
                        if (TH > height)
                        {
                            towidth = TW;
                            toheight = height;
                        }
                        else
                        {
                            towidth = width;
                            toheight = TH;
                        }
                    }
                    else
                    {
                        if (oh > height)
                        {
                            towidth = TW;
                            toheight = height;
                        }
                        else
                        {
                            towidth = ow;
                            toheight = oh;
                        }
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            Image bitmap = new Bitmap(towidth, toheight);

            //新建一个画板
            Graphics g = Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, format);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
        #endregion

        #region 在图片上生成文字、图片水印
        /// <summary>
        /// 文字水印
        /// </summary>
        /// <param name="originalData"></param>
        /// <param name="savePath"></param>
        /// <param name="watermarkText"></param>
        /// <param name="fontSize"></param>
        /// <param name="fontStyle"></param>
        /// <param name="color"></param>
        public static void WaterText(byte[] originalData, string savePath, string watermarkText, float fontSize, FontStyle fontStyle, drawing.Color color)
        {
            savePath = Config.GetMapPath(savePath);
            //创建一个图片对象用来装载要被添加水印的图片
            MemoryStream ms = new MemoryStream(originalData);
            Image initImage = Image.FromStream(ms);
            if (watermarkText != "")
            {
                using (Graphics gWater = Graphics.FromImage(initImage))
                {
                    Font fontWater = new Font("黑体", fontSize, fontStyle);
                    Brush brushWater = new SolidBrush(color);
                    gWater.DrawString(watermarkText, fontWater, brushWater, 10, 10);
                    gWater.Dispose();
                }
            }
            initImage.Save(savePath, ImageFormat.Jpeg);
        }
        /// <summary>
        /// 图片水印
        /// </summary>
        /// <param name="originalData"></param>
        /// <param name="savePath"></param>
        /// <param name="watermarkImage"></param>
        /// <param name="waterPos"></param>
        public static void WaterPic(byte[] originalData, string savePath, string watermarkImage, string waterPos)
        {
            savePath = Config.GetMapPath(savePath);
            //创建一个图片对象用来装载要被添加水印的图片
            MemoryStream ms = new MemoryStream(originalData);
            Image initImage = Image.FromStream(ms);
            //透明图片水印 
            if (watermarkImage != "")
            {
                if (File.Exists(watermarkImage))
                {
                    //获取水印图片 
                    using (Image wrImage = Image.FromFile(watermarkImage))
                    {
                        //水印绘制条件：原始图片宽高均大于或等于水印图片 
                        if (initImage.Width >= wrImage.Width && initImage.Height >= wrImage.Height)
                        {
                            Graphics gWater = Graphics.FromImage(initImage);

                            //透明属性 
                            ImageAttributes imgAttributes = new ImageAttributes();
                            ColorMap colorMap = new ColorMap();
                            colorMap.OldColor = drawing.Color.FromArgb(255, 0, 255, 0);
                            colorMap.NewColor = drawing.Color.FromArgb(0, 0, 0, 0);
                            ColorMap[] remapTable = { colorMap };
                            imgAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                            float[][] colorMatrixElements = {  
                                   new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f}, 
                                   new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f}, 
                                   new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f}, 
                                   new float[] {0.0f,  0.0f,  0.0f,  0.5f, 0.0f},//透明度:0.5 
                                   new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f} 
                                };

                            ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);
                            imgAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                            //水印位置及大小
                            Rectangle rec = new Rectangle();
                            if (waterPos == "LeftT")
                            {
                                rec.X = 0;
                                rec.Y = 0;
                            }
                            else if (waterPos == "LeftC")
                            {
                                rec.X = 0;
                                rec.Y = initImage.Height / 2 - wrImage.Height / 2;
                            }
                            else if (waterPos == "LeftB")
                            {
                                rec.X = 0;
                                rec.Y = initImage.Height - wrImage.Height;
                            }
                            else if (waterPos == "RightT")
                            {
                                rec.X = initImage.Width - wrImage.Width;
                                rec.Y = 0;
                            }
                            else if (waterPos == "RightC")
                            {
                                rec.X = initImage.Width - wrImage.Width;
                                rec.Y = initImage.Height / 2 - wrImage.Height / 2;
                            }
                            else if (waterPos == "RightB")
                            {
                                rec.X = initImage.Width - wrImage.Width;
                                rec.Y = initImage.Height - wrImage.Height;
                            }
                            else if (waterPos == "CenterT")
                            {
                                rec.X = initImage.Width / 2 - wrImage.Width / 2;
                                rec.Y = 0;
                            }
                            else if (waterPos == "CenterC")
                            {
                                rec.X = initImage.Width / 2 - wrImage.Width / 2;
                                rec.Y = initImage.Height / 2 - wrImage.Height / 2;
                            }
                            else if (waterPos == "CenterB")
                            {
                                rec.X = initImage.Width / 2 - wrImage.Width / 2;
                                rec.Y = initImage.Height - wrImage.Height;
                            }
                            else
                            {
                                rec.X = initImage.Width / 2 - wrImage.Width / 2;
                                rec.Y = initImage.Height / 2 - wrImage.Height / 2;
                            }

                            rec.Width = wrImage.Width;
                            rec.Height = wrImage.Height;
                            gWater.DrawImage(wrImage, rec, 0, 0, wrImage.Width, wrImage.Height, GraphicsUnit.Pixel, imgAttributes);

                            gWater.Dispose();
                        }

                        wrImage.Dispose();
                    }
                }
            }

            //保存 
            initImage.Save(savePath, ImageFormat.Jpeg);
        }
        #endregion

        #region 根据文字生成图片
        /// <summary>
        /// 根据文字生成图片
        /// 单考
        /// 2016-09-08
        /// </summary>
        /// <param name="text">文字内容</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="fontColor">字体颜色,如Brushes.Black</param>
        /// <param name="bgColor">背景颜色,如Brushes.Transparent(透明色)</param>
        /// <param name="savePath">保存路径,不用再Server.MapPath</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static string CreateTextPic(string text, string fontName, float fontSize, Brush fontColor, Brush bgColor, string savePath, string fileName)
        {
            try
            {
                int width = 0;
                int height = 0;
                Bitmap bmp;
                Graphics g;
                RectangleF rect;
                StringFormat format = new StringFormat(StringFormatFlags.NoClip);
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                Font font;

                if (string.IsNullOrEmpty(fontName))
                {
                    fontName = "宋体";
                }
                font = new Font(fontName, fontSize);

                bmp = new Bitmap(1, 1);
                g = Graphics.FromImage(bmp);
                SizeF sizef = g.MeasureString(text, font, PointF.Empty, format);

                width = sizef.Width.ToInt() + 1;
                height = sizef.Height.ToInt() + 1;
                rect = new RectangleF(0, 0, width, width);
                bmp.Dispose();
                bmp = new Bitmap(width, width);

                g = Graphics.FromImage(bmp);
                g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
                g.FillRectangle(bgColor, rect);
                g.DrawString(text, font, fontColor, rect, format);

                IOHelper.FolderCheck(Config.GetMapPath(savePath));
                Random rnd = new Random(Config.GetRandomSeed());
                string FilePath = savePath + fileName + ".png";

                bmp.Save(Config.GetMapPath(FilePath), ImageFormat.Png);
                return FilePath;
            }
            catch (Exception ex)
            {
                Config.Err(ex);
                return "";
            }
        }

        /// <summary>
        /// 根据文字生成图片
        /// </summary>
        /// <param name="text">文字内容</param>
        /// <param name="fontFilePath">字体文件路径,,不用再Server.MapPath</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="fontColor">字体颜色,如Brushes.Black</param>
        /// <param name="width">图片宽度,为0时以文字大小自适应大小</param>
        /// <param name="height">图片高度,为0时以文字大小自适应大小</param>
        /// <param name="bgColor">背景颜色,如Brushes.Transparent(透明色)</param>
        /// <param name="savePath">保存路径,不用再Server.MapPath</param>
        /// <returns></returns>
        public static string CreateTextPic(string text, string fontFilePath, string fontName, float fontSize, Brush fontColor, int width, int height, Brush bgColor, string savePath)
        {
            try
            {
                Bitmap bmp;
                Graphics g;
                RectangleF rect;
                StringFormat format = new StringFormat(StringFormatFlags.NoClip);
                Font font;
                if (!string.IsNullOrEmpty(fontFilePath) && File.Exists(Config.GetMapPath(fontFilePath)))
                {
                    //加载字体文件
                    PrivateFontCollection FM = new PrivateFontCollection();
                    FM.AddFontFile(Config.GetMapPath(fontFilePath));
                    FontFamily fontFam = FM.Families[0];
                    font = new Font(fontFam, fontSize);
                }
                else
                {
                    if (string.IsNullOrEmpty(fontName))
                    {
                        fontName = "宋体";
                    }
                    font = new Font(fontName, fontSize);
                }
                if (width == 0 || height == 0)
                {
                    bmp = new Bitmap(1, 1);
                    g = Graphics.FromImage(bmp);
                    SizeF sizef = g.MeasureString(text, font, PointF.Empty, format);

                    width = sizef.Width.ToInt() + 1;
                    height = sizef.Height.ToInt() + 1;
                    rect = new RectangleF(0, 0, width, height);
                    bmp.Dispose();

                    bmp = new Bitmap(width, height);
                }
                else
                {
                    rect = new RectangleF(0, 0, width, height);
                    bmp = new Bitmap(width, height);
                }

                g = Graphics.FromImage(bmp);
                g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
                g.FillRectangle(bgColor, rect);
                g.DrawString(text, font, fontColor, rect, format);

                IOHelper.FolderCheck(Config.GetMapPath(savePath));
                Random rnd = new Random(Config.GetRandomSeed());
                string FilePath = savePath + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + rnd.Next(1000, 9999) + ".png";

                bmp.Save(Config.GetMapPath(FilePath), ImageFormat.Png);
                return FilePath;
            }
            catch (Exception ex)
            {
                Config.Err(ex);
                return "";
            }
        }

        /// <summary>
        /// 根据文字生成图片
        /// </summary>
        /// <param name="text">文字内容</param>
        /// <param name="fontFilePath">字体文件路径,,不用再Server.MapPath</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="fontColor">字体颜色,如Brushes.Black</param>
        /// <param name="width">图片宽度,为0时以文字大小自适应大小</param>
        /// <param name="height">图片高度,为0时以文字大小自适应大小</param>
        /// <param name="bgColor">背景颜色,如Brushes.Transparent(透明色)</param>
        /// <param name="savePath">保存路径,不用再Server.MapPath</param>
        /// <param name="idTag">标记用，如"381_"</param>
        /// <returns></returns>
        public static string CreateTextPic2(string text, string fontFilePath, string fontName, float fontSize, Brush fontColor, int width, int height, Brush bgColor, string savePath, out int w, out int h, string idTag = "")
        {
            try
            {
                Bitmap bmp;
                Graphics g;
                RectangleF rect;
                StringFormat format = new StringFormat(StringFormatFlags.NoClip);
                Font font;
                if (!string.IsNullOrEmpty(fontFilePath) && File.Exists(Config.GetMapPath(fontFilePath)))
                {
                    //加载字体文件
                    PrivateFontCollection FM = new PrivateFontCollection();
                    FM.AddFontFile(Config.GetMapPath(fontFilePath));
                    FontFamily fontFam = FM.Families[0];
                    font = new Font(fontFam, fontSize);
                }
                else
                {
                    if (string.IsNullOrEmpty(fontName))
                    {
                        fontName = "宋体";
                    }
                    font = new Font(fontName, fontSize);
                }
                if (width == 0 || height == 0)
                {
                    bmp = new Bitmap(1, 1);
                    g = Graphics.FromImage(bmp);
                    SizeF sizef = g.MeasureString(text, font, PointF.Empty, format);

                    width = sizef.Width.ToInt() + 1;
                    height = sizef.Height.ToInt() + 1;
                    rect = new RectangleF(0, 0, width, height);
                    bmp.Dispose();

                    bmp = new Bitmap(width, height);
                }
                else
                {
                    rect = new RectangleF(0, 0, width, height);
                    bmp = new Bitmap(width, height);
                }

                g = Graphics.FromImage(bmp);
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
                g.FillRectangle(bgColor, rect);
                g.DrawString(text, font, fontColor, rect, format);

                IOHelper.FolderCheck(Config.GetMapPath(savePath));
                Random rnd = new Random(Config.GetRandomSeed());
                string FilePath = savePath + idTag + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + rnd.Next(1000, 9999) + ".png";

                bmp.Save(Config.GetMapPath(FilePath), ImageFormat.Png);
                w = width;
                h = height;
                return FilePath;
            }
            catch (Exception ex)
            {
                Config.Err(ex);
                w = 0;
                h = 0;
                return "";
            }
        }


        /// <summary>
        /// 根据文字生成图片
        /// </summary>
        /// <param name="text">文字内容</param>
        /// <param name="fontFilePath">字体文件路径,,不用再Server.MapPath</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="fontColor">字体颜色,如Brushes.Black</param>
        /// <param name="width">图片宽度,为0时以文字大小自适应大小</param>
        /// <param name="height">图片高度,为0时以文字大小自适应大小</param>
        /// <param name="bgColor">背景颜色,如Brushes.Transparent(透明色)</param>
        /// <param name="savePath">保存路径,不用再Server.MapPath</param>
        /// <param name="idTag">标记用，如"381_"</param>
        /// <returns></returns>
        public static string CreateTextPic4(string text, string fontFilePath, string fontName, float fontSize, Brush fontColor, int width, int height, Brush bgColor, string savePath, out int w, out int h, string idTag = "")
        {
            try
            {
                Bitmap bmp;
                Graphics g;
                RectangleF rect;
                StringFormat format = new StringFormat(StringFormatFlags.NoClip);
                Font font;
                if (!string.IsNullOrEmpty(fontFilePath) && File.Exists(Config.GetMapPath(fontFilePath)))
                {
                    //加载字体文件
                    PrivateFontCollection FM = new PrivateFontCollection();
                    FM.AddFontFile(Config.GetMapPath(fontFilePath));
                    FontFamily fontFam = FM.Families[0];
                    font = new Font(fontFam, fontSize);
                }
                else
                {
                    if (string.IsNullOrEmpty(fontName))
                    {
                        fontName = "宋体";
                    }
                    font = new Font(fontName, fontSize);
                }
                if (width == 0 || height == 0)
                {
                    bmp = new Bitmap(1, 1);
                    g = Graphics.FromImage(bmp);
                    SizeF sizef = g.MeasureString(text, font, PointF.Empty, format);

                    width = sizef.Width.ToInt() + 1;
                    height = sizef.Height.ToInt() + 1;
                    rect = new RectangleF(0, 0, width, height);
                    bmp.Dispose();

                    bmp = new Bitmap(width, height);
                }
                else
                {
                    rect = new RectangleF(0, 0, width, height);
                    bmp = new Bitmap(width, height);
                }

                g = Graphics.FromImage(bmp);
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
                g.FillRectangle(bgColor, rect);
                g.DrawString(text, font, fontColor, rect, format);

                IOHelper.FolderCheck(Config.GetMapPath(savePath));
                Random rnd = new Random(Config.GetRandomSeed());
                string FilePath = savePath + idTag + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + rnd.Next(1000, 9999) + ".jpg";

                bmp.Save(Config.GetMapPath(FilePath), ImageFormat.Png);
                w = width;
                h = height;
                return FilePath;
            }
            catch (Exception ex)
            {
                Config.Err(ex);
                w = 0;
                h = 0;
                return "";
            }
        }
        

        public static string CreateTextPic3(string text, System.Drawing.Color color1, System.Drawing.Color color2, string fontname, float fontsize, float width, float height, FontStyle fs, string savePath, string idTag = "")
        {
            if (string.IsNullOrEmpty(text.Trim()))
                return "";

            System.Drawing.Bitmap image = new System.Drawing.Bitmap(width.ToInt(), height.ToInt());
            Graphics g = Graphics.FromImage(image);

            try
            {
                //清空图片背景色
                g.Clear(System.Drawing.Color.Transparent);//背景色正常为黑色

                Font font = new System.Drawing.Font(fontname, fontsize, fs);
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), color1, color2, 1.2f, true);
                g.DrawString(text, font, brush, 2, 2);

                IOHelper.FolderCheck(Config.GetMapPath(savePath));
                Random rnd = new Random(Config.GetRandomSeed());
                string FilePath = savePath + idTag + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + rnd.Next(1000, 9999) + ".png";

                //System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                //HttpContext.Current.Response.ClearContent();
                //HttpContext.Current.Response.ContentType = "image/Jpeg";
                //HttpContext.Current.Response.BinaryWrite(ms.ToArray());
                image.Save(Config.GetMapPath(FilePath), System.Drawing.Imaging.ImageFormat.Png);
                return FilePath;
            }
            catch(Exception ex)
            {
                Config.Err(ex);
                return "";
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
        #endregion

        #region 生成二维码+logo
        /// <summary>
        /// 生成二维码+logo
        /// </summary>
        /// <param name="strData"></param>
        /// <param name="strSavePath"></param>
        /// <param name="strFileNameNoExt"></param>
        public static byte[] Create2DCode(string strData, int Scale)
        {
            try
            {
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qrCodeEncoder.QRCodeScale = Scale;
                qrCodeEncoder.QRCodeVersion = 0;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                drawing.Image image = qrCodeEncoder.Encode(strData, Encoding.UTF8);

                MemoryStream MStream = new MemoryStream();
                image.Save(MStream, ImageFormat.Png);
                byte[] fileContent = MStream.GetBuffer();
                image.Dispose();
                return fileContent;
            }
            catch (System.Exception ex)
            {
                Config.Err(ex);
                return null;
            }
        }

        public static string Create2DCode(string strData, string strLogo, string strSavePath, int Scale)
        {
            try
            {
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qrCodeEncoder.QRCodeScale = Scale;
                qrCodeEncoder.QRCodeVersion = 0;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                drawing.Image image = qrCodeEncoder.Encode(strData, Encoding.UTF8);

                MemoryStream MStream = new MemoryStream();
                image.Save(MStream, ImageFormat.Png);

                if (!string.IsNullOrEmpty(strLogo) && File.Exists(Config.GetMapPath(strLogo)))
                {
                    MemoryStream LogoMStream = new MemoryStream();
                    CombinImage(image, Config.GetMapPath(strLogo)).Save(LogoMStream, ImageFormat.Png);
                }

                IOHelper.FolderCheck(Config.GetMapPath(strSavePath));
                Random rnd = new Random(Config.GetRandomSeed());
                string FilePath = strSavePath + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + rnd.Next(1000, 9999) + ".png";
                image.Save(Config.GetMapPath(FilePath), ImageFormat.Png);
                return FilePath;
            }
            catch (System.Exception ex)
            {
                Config.Err(ex);
                return "";
            }
        }

        /// <summary>
        /// 生成二维码
        /// 单考
        /// 2016-09-07
        /// </summary>
        /// <param name="strData">数据</param>
        /// <param name="strLogo">中间图</param>
        /// <param name="strSavePath">保存路径</param>
        /// <param name="scale">大小</param>
        /// <param name="fileName">文件名</param>
        /// <returns>二维码路径</returns>
        public static string Create2DCode(string strData, string strLogo, string strSavePath, int scale, string fileName)
        {
            try
            {
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qrCodeEncoder.QRCodeScale = scale;
                qrCodeEncoder.QRCodeVersion = 0;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                drawing.Image image = qrCodeEncoder.Encode(strData, Encoding.UTF8);

                if (!string.IsNullOrEmpty(strLogo) && File.Exists(Config.GetMapPath(strLogo)))
                {
                    CombinImage(image, Config.GetMapPath(strLogo));
                }

                MemoryStream MStream = new MemoryStream();
                image.Save(MStream, ImageFormat.Png);

                IOHelper.FolderCheck(Config.GetMapPath(strSavePath));
                string FilePath = strSavePath + fileName + ".png";
                image.Save(Config.GetMapPath(FilePath), ImageFormat.Png);
                return FilePath;
            }
            catch (System.Exception ex)
            {
                Config.Err(ex);
                return "";
            }
        }

        /// <summary>  
        /// 调用此函数后使此两种图片合并，类似相册，有个  
        /// 背景图，中间贴自己的目标图片  
        /// </summary>  
        /// <param name="imgBack">粘贴的源图片</param>  
        /// <param name="destImg">粘贴的目标图片</param>  
        public static drawing.Image CombinImage(drawing.Image imgBack, string destImg)
        {
            drawing.Image img = drawing.Image.FromFile(destImg);        //照片图片    
            if (img.Height != 65 || img.Width != 65)
            {
                img = ResizeImage(img, 65, 65, 0);
            }
            Graphics g = Graphics.FromImage(imgBack);

            g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);

            //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);   
            //g.FillRectangle(System.Drawing.Brushes.White, imgBack.Width / 2 - img.Width / 2 - 1, imgBack.Width / 2 - img.Width / 2 - 1,1,1);//相片四周刷一层黑色边框  

            //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);  
            g.DrawImage(img, imgBack.Width / 2 - img.Width / 2, imgBack.Width / 2 - img.Width / 2, img.Width, img.Height);
            GC.Collect();
            return imgBack;
        }


        /// <summary>  
        /// Resize图片  
        /// </summary>  
        /// <param name="bmp">原始Bitmap</param>  
        /// <param name="newW">新的宽度</param>  
        /// <param name="newH">新的高度</param>  
        /// <param name="Mode">保留着，暂时未用</param>  
        /// <returns>处理以后的图片</returns>  
        public static drawing.Image ResizeImage(drawing.Image bmp, int newW, int newH, int Mode)
        {
            try
            {
                drawing.Image b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量  
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region 获取图片宽高
        /// <summary>
        /// 获取图片宽高
        /// </summary>
        /// <param name="strImagePath"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        public static void GetImageWH(string strImagePath, out int w, out int h)
        {
            w = 0;
            h = 0;
            if (!string.IsNullOrEmpty(strImagePath))
            {
                string strImage = Config.GetMapPath(strImagePath);
                if (File.Exists(strImage))
                {
                    Image image = Image.FromFile(strImage);
                    w = image.Width;
                    h = image.Height;

                    image.Dispose();
                }
            }

        }


        /// <summary>
        /// 获取图片宽高
        /// </summary>
        /// <param name="strImagePath"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        public static void GetImageWHD(string strImagePath, out float w, out float h, out float dpi)
        {
            w = 0;
            h = 0;
            dpi = 0f;
            if (!string.IsNullOrEmpty(strImagePath))
            {
                string strImage = Config.GetMapPath(strImagePath);
                if (File.Exists(strImage))
                {
                    Image image = Image.FromFile(strImage);
                    w = image.Width;
                    h = image.Height;
                    dpi = image.HorizontalResolution;
                }
            }

        }
        #endregion
    }
}
