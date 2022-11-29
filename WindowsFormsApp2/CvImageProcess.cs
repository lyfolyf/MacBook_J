using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCvSharp;
using OpenCvSharp.Extensions;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace MacBook
{
    public class CvImageProcess
    {
        /// <summary>
        /// opencv图片旋转
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="surface"></param>
        /// <returns></returns>
        public Mat MatRotation(Mat mat, string dire)
        {
            switch (dire)
            {

                case "X":
                    mat = mat.Transpose(); //逆时针旋转 90度
                    mat = mat.Flip(FlipMode.X);
                    break;

                case "XX":
                    mat = mat.Flip(FlipMode.X);//上下镜像 
                    break;

                case "Y":
                    mat = mat.Transpose(); //  顺时针旋转 90度
                    mat = mat.Flip(FlipMode.Y);
                    break;

                case "YY":
                    mat = mat.Flip(FlipMode.Y);//左右镜像
                    break;

                case "XY":
                    mat = mat.Flip(FlipMode.XY);
                    break;

                case "Z":
                    //不作处理
                    break;

                default:
                    break;
            }
            return mat;
        }

        public Bitmap Rotation(Bitmap map, int dire)
        {

            //RotateFlipType.RotateNoneFlipNone:  无变化    0(None)
            //RotateFlipType.Rotate90FlipNone:  顺时针 90   1(R90+)
            //RotateFlipType.Rotate180FlipNone:  顺时针 180 2(R180+)
            //RotateFlipType.Rotate270FlipNone:  逆时针 90  3(R90-)
            //RotateFlipType.RotateNoneFlipX:  X镜像        4(FX)
            //RotateFlipType.Rotate90FlipX:   顺时针 90  再X镜像 5(R90+FX)
            //RotateFlipType.Rotate180FlipX:  Y镜像           6(FY)
            //RotateFlipType.Rotate270FlipX:  逆时针 90  再Y镜像  7(R90-FY)


            switch ((RotateFlipType)(dire))
            {
                case RotateFlipType.RotateNoneFlipNone: //  0  不作变化
                     map.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                    break;

                case RotateFlipType.Rotate90FlipNone://1  顺时针旋转90
                    map.RotateFlip(RotateFlipType.Rotate90FlipNone); 
                    break;

                case RotateFlipType.Rotate180FlipNone://2 顺时针 180
                    map.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;

                case RotateFlipType.Rotate270FlipNone:// 3 逆时针 90
                    map.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;

                case RotateFlipType.RotateNoneFlipX:  // 4 X镜像
                    map.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    break;

                case RotateFlipType.Rotate90FlipX: // 5 顺时针 90  再X镜像
                    map.RotateFlip(RotateFlipType.Rotate90FlipX);
                    break;

                case RotateFlipType.Rotate180FlipX: //6  Y镜像
                    map.RotateFlip(RotateFlipType.Rotate180FlipX);
                    break;

                case RotateFlipType.Rotate90FlipY: //  7 逆时针 90  再Y镜像
                    map.RotateFlip(RotateFlipType.Rotate90FlipY);
                    break;
                    


            }
            return map;
        }

        /// <summary>
        /// 根据图片名字 获得Http上传时所需 content
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string ContentName(string s)
        {
            string[] ss = s.Split('.');
            string content = string.Format("{0}-{1}-{2}-{3}", ss[6], ss[8], ss[1], ss[7]);
            return content.Replace('_', '-');
        }



        public (int r, int g, int b) MeanImage(Bitmap map)
        {
          
           int  w = map.Width;
           int h = map.Height;
            int r = 0, g = 0, b = 0;
            Task.Run(() =>
            {
                for (int i = 0; i < map.Width; i++)
                {
                    for (int j = 0; j < map.Height; j++)
                    {
                        Color col = map.GetPixel(i, j);
                        r += col.R;
                        g += col.G;
                        b += col.B;
                    }
                }

            }).ContinueWith((o) =>
           {
               r = r / (w * h);
               g = g / (w * h);
               b = b / (w * h);
               //return (r,g,b);
           });
            return (r, g, b);
        }

        /// <summary>
    /// 计算平均RGB
    /// </summary>
    /// <param name="mat"></param>
    /// <returns></returns>
        public (int r, int g, int b) MatMeanImage(Mat mat)
        {
            if (mat == null)
            {
                return (999, 999, 999);
            }
            Scalar s = Cv2.Mean(mat);
            return ((int)s.Val2, (int)s.Val1, (int)s.Val0);


        }

        /// <summary>
        /// CV拼图
        /// </summary>
        /// <param name="lis"></param>
        /// <param name="wCount"></param>
        /// <param name="hCount"></param>
        /// <returns></returns>
        public Bitmap MatContact(List<byte[]> lis, int wCount, int hCount)
        {
            List<Mat> mats = new List<Mat>();
            for (int i = 0; i < hCount; i++)
            {
                List<Mat> hmats = new List<Mat>();
                for (int j = 0; j < wCount; j++)
                {
                    hmats.Add(Mat.FromImageData(lis[j + i * wCount], ImreadModes.Color));
                }
                Mat mat = new Mat();
                Cv2.HConcat(hmats, mat);
                mats.Add(mat);
            }
            Mat matS = new Mat();
            Cv2.VConcat(mats, matS);
            Bitmap map = BitmapConverter.ToBitmap(matS);
            return map;
        }

        /// <summary>
        /// C#图片拼接
        /// </summary>
        /// <param name="Imgs"></param>
        /// <param name="wCount"> 宽度方向数量 </param>
        /// <param name="hCount">高度方向数量</param>
        /// <returns></returns>
        public Image SplicingImage(Image[] Imgs, int wCount, int hCount)//
        {
            int imgHeight = 0, imgWidth = 0;
            imgWidth = Imgs[0].Width * wCount;
            imgHeight = Imgs[0].Height * hCount;
            Bitmap joinedBitmap = new Bitmap(imgWidth, imgHeight);
            Graphics graph = Graphics.FromImage(joinedBitmap);
            for (int j = 0; j < hCount; j++)
            {
                for (int i = 0; i < wCount; i++)
                {
                    graph.DrawImage(Imgs[i], i * Imgs[0].Width, j * Imgs[0].Height, Imgs[0].Width * (i + 1), Imgs[0].Height * (j + 1));
                }
            }
            return joinedBitmap;
        }

        /// <summary>
        /// 图片保存
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="imagePath"></param>
        public void BytesToFile(byte[] buffer, string imagePath)
        {
            if (buffer != null)
            {
                FileStream fs = new FileStream(imagePath, FileMode.Create);
                fs.Write(buffer, 0, buffer.Length);
                fs.Flush();
                fs.Close();
            }

        }

        /// <summary>
        /// 颜色 名称转化简写
        /// </summary>
        /// <param name="productColor"></param>
        /// <returns></returns>
        public string ConvertColorCode(string productColor)
        {
            switch (productColor)
            {
                case "DeepBlue":
                    productColor = "D";
                    break;

                case "Bassalt":
                    productColor = "B";
                    break;

                case "Gray":
                    productColor = "R";
                    break;

                case "Gold":
                    productColor = "G";
                    break;

                case "Silver":
                    productColor = "S";
                    break;

            }
            return productColor;
        }

        /// <summary>
        /// 尺寸压缩
        /// </summary>
        /// <param name="ImageOriginal"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public Mat MatResizeImage(Bitmap ImageOriginal)
        {

            Mat mat = BitmapConverter.ToMat(ImageOriginal);
            OpenCvSharp.Size opencvSize = new OpenCvSharp.Size(ImageOriginal.Width / 2, ImageOriginal.Height / 2);
            Mat sizeMat = new Mat();
            Cv2.Resize(mat, sizeMat, opencvSize);
            mat.Dispose();
            return sizeMat;
        }

        /// <summary>
        /// 基于OPENCV质量压缩
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public byte[] MatCompressImage(Mat mat, int JpegQuality)
        {
            ImageEncodingParam encodingParam = new ImageEncodingParam(ImwriteFlags.JpegQuality, JpegQuality);
            byte[] buf;
            Cv2.ImEncode(".jpg", InputArray.Create(mat), out buf, encodingParam);
            return buf;
        }

        /// <summary>
        /// 获得JPG编格式
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        private static ImageCodecInfo GetEncoder(string mimeType = "image/jpeg")
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

        /// <summary>
        /// C# 图片尺寸压缩
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public Bitmap ResizeImage(Bitmap img)
        {
            try
            {
                System.Drawing.Size newSize = new System.Drawing.Size(img.Width / 2, img.Height / 2);
                Bitmap outBmp = new Bitmap(newSize.Width, newSize.Height);

                Graphics g = Graphics.FromImage(outBmp);
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, new Rectangle(0, 0, newSize.Width, newSize.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);

                //g.Dispose();
                //img.Dispose();

                return outBmp;
            }
            catch (Exception ex)
            {
                ErrLog.WriteLogEx(ex.ToString());
                return null;

            }

        }
        /// <summary>
        /// 质量压缩
        /// </summary>
        /// <param name="map"></param>
        /// <param name="JpegQuality"></param>
        /// <returns></returns>
        public byte[] CompressImage(Bitmap map, int JpegQuality)
        {
            try
            {
                ImageFormat imgFormat = map.RawFormat;
                EncoderParameters encoderParams = new EncoderParameters();
                encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, JpegQuality);
                ImageCodecInfo codecInfo = GetEncoder();
                MemoryStream ms = new MemoryStream();
                lock (ms)
                {
                    map.Save(ms, codecInfo, encoderParams);
                    map.Dispose();
                    return ms.ToArray();
                }
                //return Image.FromStream(ms); 也可返回 image 图片
            }
            catch (Exception ex)
            {
                ErrLog.WriteLogEx(ex.ToString());
                return null;
            }

        }

    }
}
