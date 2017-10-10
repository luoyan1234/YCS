using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace YCS.Common
{
    /// <summary>
    /// 彩色验证码
    /// Create by Jimy
    /// </summary>
    public class VryImgGen
    {
        public VryImgGen()
        {
            rnd = new Random(unchecked((int)DateTime.Now.Ticks));
        }

        #region 中文字符串
        /// <summary>
        /// 中文字符串
        /// </summary>
        public static string ChineseChars = String.Empty;
        #endregion

        #region 英文与数字字符串
        /// <summary>
        /// 英文与数字串
        /// </summary>
        protected static readonly string EnglishOrNumChars = "1234567890";
        #endregion

        #region 全局随机数生成器
        /// <summary>
        /// 全局随机数生成器
        /// </summary>
        private Random rnd;
        #endregion

        #region 验证码长度(默认4个验证码的长度)
        int length = 4;
        /// <summary>
        /// 验证码长度(默认4个验证码的长度)
        /// </summary>
        public int Length
        {
            get { return length; }
            set { length = value; }
        }
        #endregion

        #region 验证码字体大小(为了显示扭曲效果，默认15像素，可以自行修改)
        int fontSize = 15;
        /// <summary>
        /// 验证码字体大小(为了显示扭曲效果，默认15像素，可以自行修改)
        /// </summary>
        public int FontSize
        {
            get { return fontSize; }
            set { fontSize = value; }
        }
        #endregion

        #region 边框补(默认2像素)
        int padding = 2;
        /// <summary>
        /// 边框补(默认2像素)
        /// </summary>
        public int Padding
        {
            get { return padding; }
            set { padding = value; }
        }
        #endregion

        #region 是否输出燥点(默认输出)
        bool chaos = true;
        /// <summary>
        /// 是否输出燥点(默认输出)
        /// </summary>
        public bool Chaos
        {
            get { return chaos; }
            set { chaos = value; }
        }
        #endregion

        #region 输出燥点的颜色(默认灰色)
        Color chaosColor = Color.LightGray;
        /// <summary>
        /// 输出燥点的颜色(默认灰色)
        /// </summary>
        public Color ChaosColor
        {
            get { return chaosColor; }
            set { chaosColor = value; }
        }
        #endregion

        #region 自定义背景色(默认白色)
        Color backgroundColor = Color.White;
        /// <summary>
        /// 自定义背景色(默认白色)
        /// </summary>
        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }
        #endregion

        #region 自定义随机颜色数组
        Color[] colors = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
        /// <summary>
        /// 自定义随机颜色数组
        /// </summary>
        public Color[] Colors
        {
            get { return colors; }
            set { colors = value; }
        }
        #endregion

        #region 自定义字体数组
        string[] fonts = { "Arial", "Verdana" };
        /// <summary>
        /// 自定义字体数组
        /// </summary>
        public string[] Fonts
        {
            get { return fonts; }
            set { fonts = value; }
        }
        #endregion

        #region 产生波形滤镜效果TwistImage

        private const double PI = 3.1415926535897932384626433832795;
        private const double PI2 = 6.283185307179586476925286766559;

        /// <summary>
        /// 正弦曲线Wave扭曲图片
        /// </summary>
        /// <param name="srcBmp">图片路径</param>
        /// <param name="bXDir">如果扭曲则选择为True</param>
        /// <param name="nMultValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>
        /// <param name="dPhase">波形的起始相位，取值区间[0-2*PI)</param>
        /// <returns></returns>
        public Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);

            // 将位图背景填充为白色
            Graphics graph = Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();

            double dBaseAxisLen = bXDir ? (double)destBmp.Height : (double)destBmp.Width;

            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = bXDir ? (PI2 * (double)j) / dBaseAxisLen : (PI2 * (double)i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);

                    // 取得当前点的颜色
                    int nOldX = 0, nOldY = 0;
                    nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                    Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                     && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }

            return destBmp;
        }
        #endregion

        #region 生成校验码图片
        /// <summary>
        /// 生成校验码图片
        /// </summary>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        public Bitmap CreateImage(string code)
        {
            int fSize = FontSize;
            int fWidth = fSize + Padding;

            int imageWidth = (int)(code.Length * fWidth) + 4 + Padding * 2;
            int imageHeight = fSize + Padding * 4;

            Bitmap image = new Bitmap(imageWidth, imageHeight);

            Graphics g = Graphics.FromImage(image);

            g.Clear(BackgroundColor);

            //给背景添加随机生成的燥点
            if (this.Chaos)
            {

                Pen pen = new Pen(ChaosColor, 0);
                int c = Length * 10;

                for (int i = 0; i < c; i++)
                {
                    int x = rnd.Next(image.Width);
                    int y = rnd.Next(image.Height);

                    g.DrawRectangle(pen, x, y, 1, 1);
                }
            }

            int left = 0, top = 0, top1 = 1, top2 = 1;

            int n1 = (imageHeight - FontSize - Padding * 2);
            int n2 = n1 / 4;
            top1 = n2;
            top2 = n2 * 2;

            Font f;
            Brush b;

            int cindex, findex;

            //随机字体和颜色的验证码字符
            for (int i = 0; i < code.Length; i++)
            {
                cindex = rnd.Next(Colors.Length - 1);
                findex = rnd.Next(Fonts.Length - 1);

                f = new Font(Fonts[findex], fSize, FontStyle.Bold);
                b = new SolidBrush(Colors[cindex]);

                if (i % 2 == 1)
                {
                    top = top2;
                }
                else
                {
                    top = top1;
                }

                left = i * fWidth;

                g.DrawString(code.Substring(i, 1), f, b, left, top);
            }

            //画一个边框 边框颜色为Color.Gainsboro
            g.DrawRectangle(new Pen(Color.Gainsboro, 0), 0, 0, image.Width - 1, image.Height - 1);
            g.Dispose();

            //产生波形
            image = TwistImage(image, true, 2, 4);

            return image;
        }
        #endregion

        #region 生成随机字符码
        /// <summary>
        /// 生成随机字符码
        /// </summary>
        /// <param name="codeLen">字符串长度</param>
        /// <param name="zhCharsCount">中文字符数</param>
        /// <returns></returns>
        public string CreateVerifyCode(int codeLen, int zhCharsCount)
        {
            char[] chs = new char[codeLen];

            int index;
            for (int i = 0; i < zhCharsCount; i++)
            {
                index = rnd.Next(0, codeLen);
                if (chs[index] == '\0')
                    chs[index] = CreateZhChar();
                else
                    --i;
            }
            for (int i = 0; i < codeLen; i++)
            {
                if (chs[i] == '\0')
                    chs[i] = CreateEnOrNumChar();
            }

            return new string(chs, 0, chs.Length);
        }

        /// <summary>
        /// 生成英文或数字字符
        /// </summary>
        /// <returns></returns>
        protected char CreateEnOrNumChar()
        {
            return EnglishOrNumChars[rnd.Next(0, EnglishOrNumChars.Length)];
        }

        /// <summary>
        /// 生成汉字字符
        /// </summary>
        /// <returns></returns>
        protected char CreateZhChar()
        {
            //若提供了汉字集，查询汉字集选取汉字
            if (ChineseChars.Length > 0)
            {
                return ChineseChars[rnd.Next(0, ChineseChars.Length)];
            }
            //若没有提供汉字集，则根据《GB2312简体中文编码表》编码规则构造汉字
            else
            {
                byte[] bytes = new byte[2];

                //第一个字节值在0xb0, 0xf7之间
                bytes[0] = (byte)rnd.Next(0xb0, 0xf8);
                //第二个字节值在0xa1, 0xfe之间
                bytes[1] = (byte)rnd.Next(0xa1, 0xff);

                //根据汉字编码的字节数组解码出中文汉字
                string str1 = Encoding.GetEncoding("gb2312").GetString(bytes);

                return str1[0];
            }
        }
        #endregion
    }
}
