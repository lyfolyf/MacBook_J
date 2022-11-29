using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

using IHalconHikvision;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using RestSharp;
using TcpSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JIALI_LED_CONTROLLER_CSHARP_LXF;
namespace MacBook
{
    public partial class FrmMain : Form
    {

        CvImageProcess imageProcess = new CvImageProcess();

        object[] lockObj = null;

        /// <summary>
        /// 计算平均RGB rect起点与终点
        /// </summary>
        System.Drawing.Point startpoint,endpoint;
        /// <summary>
        /// 是否绘制  计算平均RGB的ROI
        /// </summary>
        bool IsDraw = false;
        /// <summary>
        /// 串口通信句柄
        /// </summary>
        int Com_Handle = 0;
        private LED_CONTROLLER_256_Class ControllerA = new LED_CONTROLLER_256_Class(); //光源控制
        /// <summary>
        /// 是否运行状态
        /// </summary>
        private bool m_IsRun = false;
        private NativeTabControl NativeTabControl1;
        private NativeTabControl NativeTabControl2;
        /// <summary>
        /// 相机操作类
        /// </summary>
        private AcqFIFOManager acqFIFOManager = null;

        /// <summary>
        /// 当前客户端  与上位机通信 视觉作为客户端
        /// </summary>
        private Client client = null;
        /// <summary>
        /// 当前时间  图片命名时候保证时间统一
        /// </summary>
        private string curCtime = ""; 
        /// <summary>
        /// 当前 条码
        /// </summary>
        private string curCode = "";
        /// <summary>
        /// 根据条码 获得产品当前颜色
        /// </summary>
        private string curColor = "";
        /// <summary>
        ///  Http 发送内容库
        /// </summary>
        private RestClient restClient;

        #region 图片顺序名称
        //工位一
        private List<string> m_Mandrel_ImgName = null;
        private List<string> m_LCM1_ImgName = null;
        private List<string> m_LCM2_ImgName = null;
        private List<string> m_LCM3_ImgName = null;
        //工位二
        private List<string> m_TC1_ImgName = null;
        private List<string> m_TC2_ImgName = null;
        private List<string> m_TC3_ImgName = null;

        private List<string> m_Corner1_ImgName = null;
        private List<string> m_Corner2_ImgName = null;

        private List<string> m_Side1_ImgName = null;
        private List<string> m_Side2_ImgName = null;
        private List<string> m_Side3_ImgName = null;
        private List<string> m_Side4_ImgName = null;
        private List<string> m_Side5_ImgName = null;
        private List<string> m_Side6_ImgName = null;

        //工位三
        private List<string> m_BC1_ImgName = null;
        private List<string> m_BC2_ImgName = null;
        private List<string> m_BC3_ImgName = null;

        private List<string> m_DH1_ImgName = null;
        private List<string> m_DH2_ImgName = null;
        private List<string> m_DH3_ImgName = null;

        #endregion

        #region 图片存储发送  
        //S1
        List<byte[]> m_Mandrel_ListSendBytes = null;
        List<byte[]> m_LCM1_ListSendBytes = null;
        List<byte[]> m_LCM2_ListSendBytes = null;
        List<byte[]> m_LCM3_ListSendBytes = null;

        //S2
        List<byte[]> m_TC1_ListSendBytes = null;
        List<byte[]> m_TC2_ListSendBytes = null;
        List<byte[]> m_TC3_ListSendBytes = null;

        List<byte[]> m_Corner1_ListSendBytes = null;
        List<byte[]> m_Corner2_ListSendBytes = null;

        List<byte[]> m_Side1_ListSendBytes = null;
        List<byte[]> m_Side2_ListSendBytes = null;
        List<byte[]> m_Side3_ListSendBytes = null;
        List<byte[]> m_Side4_ListSendBytes = null;
        List<byte[]> m_Side5_ListSendBytes = null;
        List<byte[]> m_Side6_ListSendBytes = null;

        //s3
        List<byte[]> m_BC1_ListSendBytes = null;
        List<byte[]> m_BC2_ListSendBytes = null;
        List<byte[]> m_BC3_ListSendBytes = null;

        List<byte[]> m_DH1_ListSendBytes = null;
        List<byte[]> m_DH2_ListSendBytes = null;
        List<byte[]> m_DH3_ListSendBytes = null;


        //s1
        List<byte[]> m_Mandrel_ListSitchBitmap = null;
        List<byte[]> m_LCM1_ListSitchBitmap = null;
        List<byte[]> m_LCM2_ListSitchBitmap = null;
        List<byte[]> m_LCM3_ListSitchBitmap = null;

        //S2
        List<byte[]> m_TC1_ListSitchBitmap = null;
        List<byte[]> m_TC2_ListSitchBitmap = null;
        List<byte[]> m_TC3_ListSitchBitmap = null;

        List<byte[]> m_Corner1_ListSitchBitmap = null;
        List<byte[]> m_Corner2_ListSitchBitmap = null;

        List<byte[]> m_Side1_ListSitchBitmap = null;
        List<byte[]> m_Side2_ListSitchBitmap = null;
        List<byte[]> m_Side3_ListSitchBitmap = null;
        List<byte[]> m_Side4_ListSitchBitmap = null;
        List<byte[]> m_Side5_ListSitchBitmap = null;
        List<byte[]> m_Side6_ListSitchBitmap = null;

        //S3
        //s3
        List<byte[]> m_BC1_ListSitchBitmap = null;
        List<byte[]> m_BC2_ListSitchBitmap = null;
        List<byte[]> m_BC3_ListSitchBitmap = null;

        List<byte[]> m_DH1_ListSitchBitmap = null;
        List<byte[]> m_DH2_ListSitchBitmap = null;
        List<byte[]> m_DH3_ListSitchBitmap = null;
        #endregion

        public FrmMain()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer,true);
            InitializeComponent();
            panelSub.Visible = false;
            panelDebug.Visible = false;
            NativeTabControl1 = new NativeTabControl();
            NativeTabControl2 = new NativeTabControl();
            this.NativeTabControl1.AssignHandle(this.tabControl1.Handle);
            this.NativeTabControl2.AssignHandle(this.tabControl2.Handle);
            lockObj = new object[SysConfig.CamNum];

            ThreadPool.SetMaxThreads(16, 16);
            ThreadPool.SetMinThreads(8, 8);

            for (int i = 0; i < lockObj.Length; i++)
            {
                lockObj[i] = new object();
            }
        }

        #region tabcontrol控件美化

        private class NativeTabControl : NativeWindow
        {
            protected override void WndProc(ref Message m)
            {
                if ((m.Msg == TCM_ADJUSTRECT))
                {
                    RECT rc = (RECT)m.GetLParam(typeof(RECT));
                    //Adjust these values to suit, dependant upon Appearance
                    rc.Left -= 4;
                    rc.Right += 4;
                    rc.Top -= 4;
                    rc.Bottom += 4;
                    
                    Marshal.StructureToPtr(rc, m.LParam, true);
                }
                base.WndProc(ref m);
            }

            private const Int32 TCM_FIRST = 0x1300;
            private const Int32 TCM_ADJUSTRECT = (TCM_FIRST + 40);
            private struct RECT
            {
                public Int32 Left;
                public Int32 Top;
                public Int32 Right;
                public Int32 Bottom;
            }

        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            SolidBrush BackBrush = new SolidBrush(Color.FromArgb(21, 17, 13));
            //标签文字填充颜色
            SolidBrush FrontBrush = new SolidBrush(Color.White);
            StringFormat StringF = new StringFormat();
            //设置文字对齐方式
            StringF.Alignment = StringAlignment.Center;
            StringF.LineAlignment = StringAlignment.Center;

            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                //获取标签头工作区域
                Rectangle Rec = tabControl1.GetTabRect(i);

                //绘制标签头背景颜色

                e.Graphics.FillRectangle(BackBrush, Rec);
                //绘制标签头文字
                e.Graphics.DrawString(tabControl1.TabPages[i].Text, new Font("宋体", 12), FrontBrush, Rec, StringF);
            }
        }

        private void tabControl2_DrawItem(object sender, DrawItemEventArgs e)
        {
            SolidBrush BackBrush = new SolidBrush(Color.FromArgb(21, 17, 13));
            //标签文字填充颜色
            SolidBrush FrontBrush = new SolidBrush(Color.White);
            StringFormat StringF = new StringFormat();
            //设置文字对齐方式
            StringF.Alignment = StringAlignment.Center;
            StringF.LineAlignment = StringAlignment.Center;

            for (int i = 0; i < tabControl2.TabPages.Count; i++)
            {
                //获取标签头工作区域
                Rectangle Rec = tabControl2.GetTabRect(i);

                //绘制标签头背景颜色

                e.Graphics.FillRectangle(BackBrush, Rec);
                //绘制标签头文字
                e.Graphics.DrawString(tabControl2.TabPages[i].Text, new Font("宋体", 8), FrontBrush, Rec, StringF);
            }
        }


        #endregion


        #region 控件事件

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);  
        }

        private void btnRunDebug_Click(object sender, EventArgs e)
        {
            if (btnRunDebug.Text == "Run")
            {
                m_IsRun = false;
                btnRunDebug.Text = "Debug";
                panelSub.Visible = true;
                panelDebug.Visible = true;
                //ClearList();
            }
            else
            {
                m_IsRun = true;
                btnRunDebug.Text = "Run";
                panelSub.Visible = false;
                panelDebug.Visible = false;
               // ClearList();
            }
        }



        #endregion

        #region 上位机TCP通信
        private void InitTcp()
        {
            client = new Client("", 8000);
            client.TcpConnected += Client_TcpConnected;
            client.TcpDateReceived += Client_TcpDateReceived;
            client.TcpDateSend += Client_TcpDateSend;
            client.Connect();
        }

        private void Client_TcpDateSend(object sender, TcpDateEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new TcpDateSendEventHandler(Client_TcpDateSend), sender, e);
                return;
            }

        }

        private void DisplayLog(string msg)
        {
            rtbLog.AppendText(DateTime.Now.ToString()+":" +string.Format("收到条码{0},产品颜色{1}", curCode, curColor)+Environment.NewLine);
        }


        private string GetCommand(string s)
        {
            string result = "";
            if (s.StartsWith("START:PRODUCT"))
            {
                result = "TrigStart";
            }
            else if (s.StartsWith("END:PRODUCT"))
            {
                result = "TrigEnd";
            }
            else if (s.StartsWith("START:DEFECT"))
            {
                result = "DEFECTSTART";
            }
            else if (s.StartsWith("END:DEFECT"))
            {
                result = "DEFECTEnd";
            }
            return result;
        }

        private void Client_TcpDateReceived(object sender, TcpDateEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new TcpDateReceivedEventHandler(Client_TcpDateReceived), sender, e);
                return;
            }
            try
            {
                string command = GetCommand(e.Message);

                switch (command)
                {
                    case "TrigStart":
                        ClearList(); //收到开始信号  清除所有队列数据
                        curCode = e.Message.Split(',')[2]; //当前条码 
                        curColor = e.Message.Split(',')[1]; //当前条码 
                        DisplayLog(string.Format("收到条码{0},产品颜色{1},开始触发", curCode, curColor));
                        ErrLog.WriteLogData(string.Format("收到条码{0},产品颜色{1},开始触发", curCode, curColor));
                        curCtime = DateTime.Now.ToString("yyyyMMdd.HHmmssfff");
                    
                        break;

                    case "TrigEnd":
                        //拍摄结束判断  图片数量  
                        DisplayLog("拍摄结束!");
                        ErrLog.WriteLogData("拍摄结束!");
                        if (m_Mandrel_ListSendBytes.Count != SysConfig.numMandrelImage && m_LCM3_ListSendBytes.Count != SysConfig.numLCMImage && m_LCM2_ListSendBytes.Count != SysConfig.numLCMImage && m_LCM1_ListSendBytes.Count != SysConfig.numLCMImage)
                        {
                            //工位1  mandrel 相机 拍摄4张    LCM三个相机 每个拍摄28
                            string msg = m_Mandrel_ListSendBytes.Count == SysConfig.numMandrelImage ? "": "Mandrel相机拍摄异常"+ m_Mandrel_ListSendBytes.Count.ToString()+"  ";
                            msg += m_LCM3_ListSendBytes.Count == SysConfig.numLCMImage ? "" : "LCM3相机拍摄异常-" + m_LCM3_ListSendBytes.Count.ToString() + "  ";
                            msg += m_LCM2_ListSendBytes.Count == SysConfig.numLCMImage ? "" : "LCM2相机拍摄异常-" + m_LCM2_ListSendBytes.Count.ToString() + "  ";
                            msg += m_LCM1_ListSendBytes.Count == SysConfig.numLCMImage ? "" : "LCM1相机拍摄异常-" + m_LCM1_ListSendBytes.Count.ToString() + "  ";
                            DisplayLog(msg);
                            ErrLog.WriteLogData("拍摄异常!"+ msg);
                            
                        }
                        else
                        {

                            ImageStructS1();
                            GetSitchLCM(); 
                        }


                        //if (m_TC1_ListSendBytes.Count != SysConfig.numTCImage && m_TC2_ListSendBytes.Count != SysConfig.numTCImage && m_TC3_ListSendBytes.Count != SysConfig.numTCImage && m_Corner1_ListSendBytes.Count != SysConfig.numConrnerImage && m_Corner2_ListSendBytes.Count != SysConfig.numConrnerImage)
                        //{
                        //    //工位二 TC三个相机  每个相机拍摄 16  corner 两个相机 每个相机 4
                        //    string msg = m_TC1_ListSendBytes.Count == SysConfig.numTCImage ? "" : "Mandrel相机拍摄异常-" + m_TC1_ListSendBytes.Count.ToString() + "  ";
                        //    msg += m_TC2_ListSendBytes.Count == SysConfig.numTCImage ? "" : "LCM3相机拍摄异常-" + m_TC2_ListSendBytes.Count.ToString() + "  ";
                        //    msg += m_TC3_ListSendBytes.Count == SysConfig.numTCImage ? "" : "LCM2相机拍摄异常-" + m_TC3_ListSendBytes.Count.ToString() + "  ";
                        //    msg += m_Corner1_ListSendBytes.Count == SysConfig.numConrnerImage ? "" : "Corner1相机拍摄异常-" + m_Corner1_ListSendBytes.Count.ToString() + "  ";
                        //    msg += m_Corner2_ListSendBytes.Count == SysConfig.numConrnerImage ? "" : "Corner2相机拍摄异常-" + m_Corner2_ListSendBytes.Count.ToString() + "  ";
                        //    DisplayLog(msg);
                        //    ErrLog.WriteLogData("拍摄异常!" + msg);
                        //    return;
                        //}

                        //if (m_Side1_ListSendBytes.Count != SysConfig.numSideImage && m_Side2_ListSendBytes.Count != SysConfig.numSideImage && m_Side3_ListSendBytes.Count != SysConfig.numSideImage && m_Side4_ListSendBytes.Count != SysConfig.numSideImage && m_Side5_ListSendBytes.Count != SysConfig.numSideImage && m_Side6_ListSendBytes.Count != SysConfig.numSideImage)
                        //{
                        //    //工位二 side 6三个相机 每个相机拍摄 10
                        //    string  msg  = m_Side1_ListSendBytes.Count == SysConfig.numSideImage ? "" : "Side1相机拍摄异常-"  + m_Side1_ListSendBytes.Count.ToString() + "  ";
                        //            msg += m_Side2_ListSendBytes.Count == SysConfig.numSideImage ? "" : "Side2相机拍摄异常-" + m_Side2_ListSendBytes.Count.ToString() + "  ";
                        //            msg += m_Side3_ListSendBytes.Count == SysConfig.numSideImage ? "" : "Side3相机拍摄异常-" + m_Side3_ListSendBytes.Count.ToString() + "  ";
                        //            msg += m_Side4_ListSendBytes.Count == SysConfig.numSideImage ? "" : "Side4相机拍摄异常-" + m_Side4_ListSendBytes.Count.ToString() + "  ";
                        //            msg += m_Side5_ListSendBytes.Count == SysConfig.numSideImage ? "" : "Side5相机拍摄异常-" + m_Side5_ListSendBytes.Count.ToString() + "  ";
                        //            msg += m_Side6_ListSendBytes.Count == SysConfig.numSideImage ? "" : "Side6相机拍摄异常-" + m_Side6_ListSendBytes.Count.ToString() + "  ";
                        //    DisplayLog(msg);
                        //    ErrLog.WriteLogData("拍摄异常!" + msg);
                        //    return;
                        //}

                        //ImageStructS2();
                        //GetSitchCorner_Side();
                        //GetSitchTC();

                        //if (m_TC1_ListSendBytes.Count != SysConfig.numTCImage && m_TC2_ListSendBytes.Count != SysConfig.numTCImage && m_TC3_ListSendBytes.Count != SysConfig.numTCImage && m_DH1_ListSendBytes .Count != SysConfig.numTCImage && m_DH2_ListSendBytes.Count != SysConfig.numTCImage && m_DH3_ListSendBytes.Count != SysConfig.numTCImage)
                        //{
                        //    //工位三 BC三个相机 每个相机12  DH三个相机 每个相机12
                        //    string msg = m_TC1_ListSendBytes.Count == SysConfig.numSideImage ? "" : "Side1相机拍摄异常-" + m_TC1_ListSendBytes.Count.ToString() + "  ";
                        //    msg += m_TC2_ListSendBytes.Count == SysConfig.numSideImage ? "" : "Side2相机拍摄异常-" + m_TC2_ListSendBytes.Count.ToString() + "  ";
                        //    msg += m_TC3_ListSendBytes.Count == SysConfig.numSideImage ? "" : "Side3相机拍摄异常-" + m_TC3_ListSendBytes.Count.ToString() + "  ";
                        //    msg += m_DH1_ListSendBytes.Count == SysConfig.numSideImage ? "" : "Side4相机拍摄异常-" + m_DH1_ListSendBytes.Count.ToString() + "  ";
                        //    msg += m_DH2_ListSendBytes.Count == SysConfig.numSideImage ? "" : "Side5相机拍摄异常-" + m_DH2_ListSendBytes.Count.ToString() + "  ";
                        //    msg += m_DH3_ListSendBytes.Count == SysConfig.numSideImage ? "" : "Side6相机拍摄异常-" + m_DH3_ListSendBytes.Count.ToString() + "  ";
                        //    DisplayLog(msg);
                        //    ErrLog.WriteLogData("拍摄异常!" + msg);
                        //}
                        //else
                        //{
                        //    ImageStructS3();
                        //    GetSitchBC_DH();
                        //}
                        break;

                    default:
                        break;
                }

            }
            catch (Exception ex)
            {


            }
        }

        private void Client_TcpConnected(object sender, TcpDateEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new TcpConnectedEventHandler(Client_TcpConnected), sender, e);
                return;
            }

        }

        #endregion


        #region 自定义方法

        private void InitLigts()
        {
            try
            {
                //开嘉利光源
                ControllerA.ConnectController(19200, SysConfig.PortName.Split('.')[0], 8);
                //开锐视光源
                Com_Handle = RseeController_OpenCom(SysConfig.PortName.Split('.')[1],19200,true);

            }
            catch (Exception ex)
            {

                
            }
        }


        private void AllImageSave(List<byte[]> lis, List<string> ImgName, string pathSection)
        {
            string savepath = "D:\\MaskImage\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + curCode + "\\"+pathSection;
            if (!Directory.Exists(savepath))
            {
                Directory.CreateDirectory(savepath);
            }
            for (int i = 0; i < lis.Count; i++)
            {
                string imagename = string.Format(ImgName[i], curColor, curCode, curCtime);
                imageProcess.BytesToFile(lis[i], savepath+"\\"+ imagename);
                Thread.Sleep(10);
            }
          
        }

 
        #region 3站 图片按照顺序打包上传

        private void ImageStructS1()
        {
            try
            {
                List<ImageStruct> ImageStructs1_1 = new List<ImageStruct>();

                List<ImageStruct> ImageStructs1_2 = new List<ImageStruct>();

                List<ImageStruct> ImageStructs1_3 = new List<ImageStruct>();

                List<ImageStruct> ImageStructs1_4 = new List<ImageStruct>();
                //J413.S.GY67GL69CW.CosmeticAOI.20220813.233337822.LCM.Coaxial.1_1.jpg


                for (int i = 0; i < 7; i++)
                {

                    if (!m_LCM1_ImgName[i].Contains("Bar"))
                    {
                        ImageStruct imageStruct1 = new ImageStruct();
                        imageStruct1.byteImage = m_LCM1_ListSendBytes[i];
                        imageStruct1.name = string.Format(m_LCM1_ImgName[i], curColor, curCode, curCtime);
                        ImageStructs1_1.Add(imageStruct1);
                    }
                    if (!m_LCM1_ImgName[i+7].Contains("Bar"))
                    {
                            ImageStruct imageStruct2 = new ImageStruct();
                            imageStruct2.byteImage = m_LCM1_ListSendBytes[i + 7];
                            imageStruct2.name = string.Format(m_LCM1_ImgName[i + 7], curColor, curCode, curCtime);
                            ImageStructs1_2.Add(imageStruct2);
                     }
                    if (!m_LCM1_ImgName[i + 14].Contains("Bar"))
                    {
                        ImageStruct imageStruct3 = new ImageStruct();
                        imageStruct3.byteImage = m_LCM1_ListSendBytes[i + 14];
                        imageStruct3.name = string.Format(m_LCM1_ImgName[i + 14], curColor, curCode, curCtime);
                        ImageStructs1_3.Add(imageStruct3);
                    }
                    if (!m_LCM1_ImgName[i + 21].Contains("Bar"))
                    {
                        ImageStruct imageStruct4 = new ImageStruct();
                        imageStruct4.byteImage = m_LCM1_ListSendBytes[i + 21];
                        imageStruct4.name = string.Format(m_LCM1_ImgName[i + 21], curColor, curCode, curCtime);
                        ImageStructs1_4.Add(imageStruct4);
                    }


                }

                for (int i = 0; i < 7; i++)
                {

                    if (!m_LCM2_ImgName[i].Contains("Bar"))
                    {
                        ImageStruct imageStruct1 = new ImageStruct();
                        imageStruct1.byteImage = m_LCM2_ListSendBytes[i];
                        imageStruct1.name = string.Format(m_LCM2_ImgName[i], curColor, curCode, curCtime);
                        ImageStructs1_1.Add(imageStruct1);
                    }
                    if (!m_LCM2_ImgName[i + 7].Contains("Bar"))
                    {
                        ImageStruct imageStruct2 = new ImageStruct();
                        imageStruct2.byteImage = m_LCM2_ListSendBytes[i + 7];
                        imageStruct2.name = string.Format(m_LCM2_ImgName[i + 7], curColor, curCode, curCtime);
                        ImageStructs1_2.Add(imageStruct2);
                    }
                    if (!m_LCM2_ImgName[i + 14].Contains("Bar"))
                    {
                        ImageStruct imageStruct3 = new ImageStruct();
                        imageStruct3.byteImage = m_LCM2_ListSendBytes[i + 14];
                        imageStruct3.name = string.Format(m_LCM2_ImgName[i + 14], curColor, curCode, curCtime);
                        ImageStructs1_3.Add(imageStruct3);
                    }
                    if (!m_LCM2_ImgName[i + 21].Contains("Bar"))
                    {
                        ImageStruct imageStruct4 = new ImageStruct();
                        imageStruct4.byteImage = m_LCM2_ListSendBytes[i + 21];
                        imageStruct4.name = string.Format(m_LCM2_ImgName[i + 21], curColor, curCode, curCtime);
                        ImageStructs1_4.Add(imageStruct4);
                    }

                }

                for (int i = 0; i < 7; i++)
                {

                    if (!m_LCM3_ImgName[i].Contains("Bar"))
                    {
                        ImageStruct imageStruct1 = new ImageStruct();
                        imageStruct1.byteImage = m_LCM3_ListSendBytes[i];
                        imageStruct1.name = string.Format(m_LCM3_ImgName[i], curColor, curCode, curCtime);
                        ImageStructs1_1.Add(imageStruct1);
                    }
                    if (!m_LCM3_ImgName[i + 7].Contains("Bar"))
                    {
                        ImageStruct imageStruct2 = new ImageStruct();
                        imageStruct2.byteImage = m_LCM3_ListSendBytes[i + 7];
                        imageStruct2.name = string.Format(m_LCM3_ImgName[i + 7], curColor, curCode, curCtime);
                        ImageStructs1_2.Add(imageStruct2);
                    }
                    if (!m_LCM3_ImgName[i + 14].Contains("Bar"))
                    {
                        ImageStruct imageStruct3 = new ImageStruct();
                        imageStruct3.byteImage = m_LCM3_ListSendBytes[i + 14];
                        imageStruct3.name = string.Format(m_LCM3_ImgName[i + 14], curColor, curCode, curCtime);
                        ImageStructs1_3.Add(imageStruct3);
                    }
                    if (!m_LCM3_ImgName[i + 21].Contains("Bar"))
                    {
                        ImageStruct imageStruct4 = new ImageStruct();
                        imageStruct4.byteImage = m_LCM3_ListSendBytes[i + 21];
                        imageStruct4.name = string.Format(m_LCM3_ImgName[i + 21], curColor, curCode, curCtime);
                        ImageStructs1_4.Add(imageStruct4);
                    }
                }


                ImageStruct imageStructM1 = new ImageStruct();
                imageStructM1.name = string.Format(m_Mandrel_ImgName[0], curColor, curCode, curCtime);
                imageStructM1.byteImage = m_Mandrel_ListSendBytes[0];
                ImageStructs1_1.Add(imageStructM1);

                ImageStruct imageStructM2 = new ImageStruct();
                imageStructM2.name = string.Format(m_Mandrel_ImgName[1], curColor, curCode, curCtime);
                imageStructM2.byteImage = m_Mandrel_ListSendBytes[1];
                ImageStructs1_2.Add(imageStructM2);

                ImageStruct imageStructM3 = new ImageStruct();
                imageStructM3.name = string.Format(m_Mandrel_ImgName[2], curColor, curCode, curCtime);
                imageStructM3.byteImage = m_Mandrel_ListSendBytes[2];
                ImageStructs1_3.Add(imageStructM3);

                ImageStruct imageStructM4 = new ImageStruct();
                imageStructM4.name = string.Format(m_Mandrel_ImgName[3], curColor, curCode, curCtime);
                imageStructM4.byteImage = m_Mandrel_ListSendBytes[3];
                ImageStructs1_4.Add(imageStructM4);

                //保存所有图片
                //Task.Run(() =>
                //{
                //    AllImageSave(m_LCM1_ListSendBytes, m_LCM1_ImgName, "LCM_All");
                //});

                //Task.Run(() =>
                //{
                //    AllImageSave(m_LCM2_ListSendBytes, m_LCM2_ImgName, "LCM_All");
                //});

                //Task.Run(() =>
                //{
                //    AllImageSave(m_LCM3_ListSendBytes, m_LCM3_ImgName, "LCM_All");
                //});

                //Task.Run(() =>
                //{
                //    AllImageSave(m_Mandrel_ListSendBytes, m_Mandrel_ImgName, "Mandrel_All");
                //});




                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate{
                    HttpSendImage(ImageStructs1_1);
                }));

                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                {
                    HttpSendImage(ImageStructs1_2);
                }));

                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                {
                    HttpSendImage(ImageStructs1_3);
                }));

                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                {
                    HttpSendImage(ImageStructs1_4);
                }));
            }
            catch (Exception ex)
            {

               
            }
        }

        private void ImageStructS2()
        {
            try
            {
                List<ImageStruct> ImageStructs1_1 = new List<ImageStruct>();

                List<ImageStruct> ImageStructs1_2 = new List<ImageStruct>();

                List<ImageStruct> ImageStructs1_3 = new List<ImageStruct>();

                List<ImageStruct> ImageStructs1_4 = new List<ImageStruct>();

                for (int i = 0; i < 4; i++)
                {
                    if (!m_TC1_ImgName[i].Contains("Bar"))
                    {
                        ImageStruct imageStruct1 = new ImageStruct();
                        imageStruct1.byteImage = m_TC1_ListSendBytes[i];
                        imageStruct1.name = string.Format(m_TC1_ImgName[i], curColor, curCode, curCtime);
                        ImageStructs1_1.Add(imageStruct1);
                    }

                    if (!m_TC1_ImgName[i+4].Contains("Bar"))
                    {
                        ImageStruct imageStruct1 = new ImageStruct();
                        imageStruct1.byteImage = m_TC1_ListSendBytes[i + 4];
                        imageStruct1.name = string.Format(m_TC1_ImgName[i + 4], curColor, curCode, curCtime);
                        ImageStructs1_2.Add(imageStruct1);
                    }

                    if (!m_TC1_ImgName[i + 8].Contains("Bar"))
                    {
                        ImageStruct imageStruct1 = new ImageStruct();
                        imageStruct1.byteImage = m_TC1_ListSendBytes[i + 8];
                        imageStruct1.name = string.Format(m_TC1_ImgName[i + 8], curColor, curCode, curCtime);
                        ImageStructs1_3.Add(imageStruct1);
                    }

                    if (!m_TC1_ImgName[i + 12].Contains("Bar"))
                    {
                        ImageStruct imageStruct1 = new ImageStruct();
                        imageStruct1.byteImage = m_TC1_ListSendBytes[i + 12];
                        imageStruct1.name = string.Format(m_TC1_ImgName[i + 12], curColor, curCode, curCtime);
                        ImageStructs1_4.Add(imageStruct1);
                    }
                }

                for (int i = 0; i < 4; i++)
                {
                    if (!m_TC2_ImgName[i].Contains("Bar"))
                    {
                        ImageStruct imageStruct1 = new ImageStruct();
                        imageStruct1.byteImage = m_TC1_ListSendBytes[i];
                        imageStruct1.name = string.Format(m_TC2_ImgName[i], curColor, curCode, curCtime);
                        ImageStructs1_1.Add(imageStruct1);
                    }

                    if (!m_TC2_ImgName[i + 4].Contains("Bar"))
                    {
                        ImageStruct imageStruct1 = new ImageStruct();
                        imageStruct1.byteImage = m_TC1_ListSendBytes[i + 4];
                        imageStruct1.name = string.Format(m_TC2_ImgName[i + 4], curColor, curCode, curCtime);
                        ImageStructs1_2.Add(imageStruct1);
                    }

                    if (!m_TC2_ImgName[i + 8].Contains("Bar"))
                    {
                        ImageStruct imageStruct1 = new ImageStruct();
                        imageStruct1.byteImage = m_TC1_ListSendBytes[i + 8];
                        imageStruct1.name = string.Format(m_TC2_ImgName[i + 8], curColor, curCode, curCtime);
                        ImageStructs1_3.Add(imageStruct1);
                    }

                    if (!m_TC2_ImgName[i + 12].Contains("Bar"))
                    {
                        ImageStruct imageStruct1 = new ImageStruct();
                        imageStruct1.byteImage = m_TC1_ListSendBytes[i + 12];
                        imageStruct1.name = string.Format(m_TC2_ImgName[i + 12], curColor, curCode, curCtime);
                        ImageStructs1_4.Add(imageStruct1);
                    }
                }

                for (int i = 0; i < 4; i++)
                {
                    if (!m_TC3_ImgName[i].Contains("Bar"))
                    {
                        ImageStruct imageStruct1 = new ImageStruct();
                        imageStruct1.byteImage = m_TC1_ListSendBytes[i];
                        imageStruct1.name = string.Format(m_TC3_ImgName[i], curColor, curCode, curCtime);
                        ImageStructs1_1.Add(imageStruct1);
                    }

                    if (!m_TC3_ImgName[i + 4].Contains("Bar"))
                    {
                        ImageStruct imageStruct1 = new ImageStruct();
                        imageStruct1.byteImage = m_TC1_ListSendBytes[i + 4];
                        imageStruct1.name = string.Format(m_TC3_ImgName[i + 4], curColor, curCode, curCtime);
                        ImageStructs1_2.Add(imageStruct1);
                    }

                    if (!m_TC3_ImgName[i + 8].Contains("Bar"))
                    {
                        ImageStruct imageStruct1 = new ImageStruct();
                        imageStruct1.byteImage = m_TC1_ListSendBytes[i + 8];
                        imageStruct1.name = string.Format(m_TC3_ImgName[i + 8], curColor, curCode, curCtime);
                        ImageStructs1_3.Add(imageStruct1);
                    }

                    if (!m_TC3_ImgName[i + 12].Contains("Bar"))
                    {
                        ImageStruct imageStruct1 = new ImageStruct();
                        imageStruct1.byteImage = m_TC1_ListSendBytes[i + 12];
                        imageStruct1.name = string.Format(m_TC3_ImgName[i + 12], curColor, curCode, curCtime);
                        ImageStructs1_4.Add(imageStruct1);
                    }
                }

                //Task.Run(() =>
                //{
                //    AllImageSave(m_TC1_ListSendBytes, m_TC1_ImgName, "TC_All");
                //});

                //Task.Run(() =>
                //{
                //    AllImageSave(m_TC2_ListSendBytes, m_TC2_ImgName, "TC_All");
                //});

                //Task.Run(() =>
                //{
                //    AllImageSave(m_TC3_ListSendBytes, m_TC3_ImgName, "TC_All");
                //});



                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate {
                    HttpSendImage(ImageStructs1_1);
                }));

                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                {
                    HttpSendImage(ImageStructs1_2);
                }));

                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                {
                    HttpSendImage(ImageStructs1_3);
                }));

                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                {
                    HttpSendImage(ImageStructs1_4);
                }));

                List<ImageStruct> ImageStructsCorner = new List<ImageStruct>();
               
                for (int i = 0; i < 2; i++)
                {
                    ImageStruct imageStructCorner1 = new ImageStruct();
                    imageStructCorner1.byteImage = m_Corner1_ListSendBytes[i];
                    imageStructCorner1.name = string.Format(m_Corner1_ImgName[i], curColor, curCode, curCtime);
                    ImageStructsCorner.Add(imageStructCorner1);
                }

                for (int i = 0; i < 2; i++)
                {
                    ImageStruct imageStructCorner1 = new ImageStruct();
                    imageStructCorner1.byteImage = m_Corner2_ListSendBytes[i];
                    imageStructCorner1.name = string.Format(m_Corner2_ImgName[i], curColor, curCode, curCtime);
                    ImageStructsCorner.Add(imageStructCorner1);
                }

                for (int i = 2; i < 4; i++)
                {
                    ImageStruct imageStructCorner1 = new ImageStruct();
                    imageStructCorner1.byteImage = m_Corner1_ListSendBytes[i];
                    imageStructCorner1.name = string.Format(m_Corner1_ImgName[i], curColor, curCode, curCtime);
                    ImageStructsCorner.Add(imageStructCorner1);
                }

                for (int i = 2; i < 4; i++)
                {
                    ImageStruct imageStructCorner1 = new ImageStruct();
                    imageStructCorner1.byteImage = m_Corner2_ListSendBytes[i];
                    imageStructCorner1.name = string.Format(m_Corner2_ImgName[i], curColor, curCode, curCtime);
                    ImageStructsCorner.Add(imageStructCorner1);
                }


                //Task.Run(() =>
                //{
                //    AllImageSave(m_Corner1_ListSendBytes, m_Corner1_ImgName, "Corner_All");
                //});
                //Task.Run(() =>
                //{
                //    AllImageSave(m_Corner2_ListSendBytes, m_Corner2_ImgName, "Corner_All");
                //});


                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                {
                    HttpSendImage(ImageStructsCorner);
                }));


                List<ImageStruct> ImageStructsSide1 = new List<ImageStruct>();
                List<ImageStruct> ImageStructsSide2 = new List<ImageStruct>();
                List<ImageStruct> ImageStructsSide3 = new List<ImageStruct>();
                List<ImageStruct> ImageStructsSide4 = new List<ImageStruct>();
                List<ImageStruct> ImageStructsSide5 = new List<ImageStruct>();

                ImageStructsSide1.Add(new ImageStruct(m_Side1_ListSendBytes[0], string.Format(m_Side1_ImgName[0], curColor, curCode, curCtime)));
                ImageStructsSide1.Add(new ImageStruct(m_Side2_ListSendBytes[0], string.Format(m_Side2_ImgName[0], curColor, curCode, curCtime)));
                ImageStructsSide1.Add(new ImageStruct(m_Side3_ListSendBytes[0], string.Format(m_Side3_ImgName[0], curColor, curCode, curCtime)));
                ImageStructsSide1.Add(new ImageStruct(m_Side1_ListSendBytes[1], string.Format(m_Side1_ImgName[1], curColor, curCode, curCtime)));
                ImageStructsSide1.Add(new ImageStruct(m_Side2_ListSendBytes[1], string.Format(m_Side2_ImgName[1], curColor, curCode, curCtime)));
                ImageStructsSide1.Add(new ImageStruct(m_Side3_ListSendBytes[1], string.Format(m_Side3_ImgName[1], curColor, curCode, curCtime)));

                ImageStructsSide1.Add(new ImageStruct(m_Side4_ListSendBytes[0], string.Format(m_Side4_ImgName[0], curColor, curCode, curCtime)));
                ImageStructsSide1.Add(new ImageStruct(m_Side5_ListSendBytes[0], string.Format(m_Side5_ImgName[0], curColor, curCode, curCtime)));
                ImageStructsSide1.Add(new ImageStruct(m_Side6_ListSendBytes[0], string.Format(m_Side6_ImgName[0], curColor, curCode, curCtime)));
                ImageStructsSide1.Add(new ImageStruct(m_Side4_ListSendBytes[1], string.Format(m_Side4_ImgName[1], curColor, curCode, curCtime)));
                ImageStructsSide1.Add(new ImageStruct(m_Side5_ListSendBytes[1], string.Format(m_Side5_ImgName[1], curColor, curCode, curCtime)));
                ImageStructsSide1.Add(new ImageStruct(m_Side6_ListSendBytes[1], string.Format(m_Side6_ImgName[1], curColor, curCode, curCtime)));


                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                {
                    HttpSendImage(ImageStructsSide1);
                }));


                ImageStructsSide2.Add(new ImageStruct(m_Side1_ListSendBytes[2], string.Format(m_Side1_ImgName[2], curColor, curCode, curCtime)));
                ImageStructsSide2.Add(new ImageStruct(m_Side2_ListSendBytes[2], string.Format(m_Side2_ImgName[2], curColor, curCode, curCtime)));
                ImageStructsSide2.Add(new ImageStruct(m_Side3_ListSendBytes[2], string.Format(m_Side3_ImgName[2], curColor, curCode, curCtime)));
                ImageStructsSide2.Add(new ImageStruct(m_Side1_ListSendBytes[3], string.Format(m_Side1_ImgName[3], curColor, curCode, curCtime)));
                ImageStructsSide2.Add(new ImageStruct(m_Side2_ListSendBytes[3], string.Format(m_Side2_ImgName[3], curColor, curCode, curCtime)));
                ImageStructsSide2.Add(new ImageStruct(m_Side3_ListSendBytes[3], string.Format(m_Side3_ImgName[3], curColor, curCode, curCtime)));

                ImageStructsSide2.Add(new ImageStruct(m_Side4_ListSendBytes[2], string.Format(m_Side4_ImgName[2], curColor, curCode, curCtime)));
                ImageStructsSide2.Add(new ImageStruct(m_Side5_ListSendBytes[2], string.Format(m_Side5_ImgName[2], curColor, curCode, curCtime)));
                ImageStructsSide2.Add(new ImageStruct(m_Side6_ListSendBytes[2], string.Format(m_Side6_ImgName[2], curColor, curCode, curCtime)));
                ImageStructsSide2.Add(new ImageStruct(m_Side4_ListSendBytes[3], string.Format(m_Side4_ImgName[3], curColor, curCode, curCtime)));
                ImageStructsSide2.Add(new ImageStruct(m_Side5_ListSendBytes[3], string.Format(m_Side5_ImgName[3], curColor, curCode, curCtime)));
                ImageStructsSide2.Add(new ImageStruct(m_Side6_ListSendBytes[3], string.Format(m_Side6_ImgName[3], curColor, curCode, curCtime)));
                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                {
                    HttpSendImage(ImageStructsSide2);
                }));

                ImageStructsSide3.Add(new ImageStruct(m_Side1_ListSendBytes[4], string.Format(m_Side1_ImgName[4], curColor, curCode, curCtime)));
                ImageStructsSide3.Add(new ImageStruct(m_Side2_ListSendBytes[4], string.Format(m_Side2_ImgName[4], curColor, curCode, curCtime)));
                ImageStructsSide3.Add(new ImageStruct(m_Side3_ListSendBytes[4], string.Format(m_Side3_ImgName[4], curColor, curCode, curCtime)));
                ImageStructsSide3.Add(new ImageStruct(m_Side1_ListSendBytes[5], string.Format(m_Side1_ImgName[5], curColor, curCode, curCtime)));
                ImageStructsSide3.Add(new ImageStruct(m_Side2_ListSendBytes[5], string.Format(m_Side2_ImgName[5], curColor, curCode, curCtime)));
                ImageStructsSide3.Add(new ImageStruct(m_Side3_ListSendBytes[5], string.Format(m_Side3_ImgName[5], curColor, curCode, curCtime)));

                ImageStructsSide3.Add(new ImageStruct(m_Side4_ListSendBytes[4], string.Format(m_Side4_ImgName[4], curColor, curCode, curCtime)));
                ImageStructsSide3.Add(new ImageStruct(m_Side5_ListSendBytes[4], string.Format(m_Side5_ImgName[4], curColor, curCode, curCtime)));
                ImageStructsSide3.Add(new ImageStruct(m_Side6_ListSendBytes[4], string.Format(m_Side6_ImgName[4], curColor, curCode, curCtime)));
                ImageStructsSide3.Add(new ImageStruct(m_Side4_ListSendBytes[5], string.Format(m_Side4_ImgName[5], curColor, curCode, curCtime)));
                ImageStructsSide3.Add(new ImageStruct(m_Side5_ListSendBytes[5], string.Format(m_Side5_ImgName[5], curColor, curCode, curCtime)));
                ImageStructsSide3.Add(new ImageStruct(m_Side6_ListSendBytes[5], string.Format(m_Side6_ImgName[5], curColor, curCode, curCtime)));
                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                {
                    HttpSendImage(ImageStructsSide3);
                }));

                ImageStructsSide4.Add(new ImageStruct(m_Side1_ListSendBytes[6], string.Format(m_Side1_ImgName[6], curColor, curCode, curCtime)));
                ImageStructsSide4.Add(new ImageStruct(m_Side2_ListSendBytes[6], string.Format(m_Side2_ImgName[6], curColor, curCode, curCtime)));
                ImageStructsSide4.Add(new ImageStruct(m_Side3_ListSendBytes[6], string.Format(m_Side3_ImgName[6], curColor, curCode, curCtime)));
                ImageStructsSide4.Add(new ImageStruct(m_Side1_ListSendBytes[7], string.Format(m_Side1_ImgName[7], curColor, curCode, curCtime)));
                ImageStructsSide4.Add(new ImageStruct(m_Side2_ListSendBytes[7], string.Format(m_Side2_ImgName[7], curColor, curCode, curCtime)));
                ImageStructsSide4.Add(new ImageStruct(m_Side3_ListSendBytes[7], string.Format(m_Side3_ImgName[7], curColor, curCode, curCtime)));

                ImageStructsSide4.Add(new ImageStruct(m_Side4_ListSendBytes[6], string.Format(m_Side4_ImgName[6], curColor, curCode, curCtime)));
                ImageStructsSide4.Add(new ImageStruct(m_Side5_ListSendBytes[6], string.Format(m_Side5_ImgName[6], curColor, curCode, curCtime)));
                ImageStructsSide4.Add(new ImageStruct(m_Side6_ListSendBytes[6], string.Format(m_Side6_ImgName[6], curColor, curCode, curCtime)));
                ImageStructsSide4.Add(new ImageStruct(m_Side4_ListSendBytes[7], string.Format(m_Side4_ImgName[7], curColor, curCode, curCtime)));
                ImageStructsSide4.Add(new ImageStruct(m_Side5_ListSendBytes[7], string.Format(m_Side5_ImgName[7], curColor, curCode, curCtime)));
                ImageStructsSide4.Add(new ImageStruct(m_Side6_ListSendBytes[7], string.Format(m_Side6_ImgName[7], curColor, curCode, curCtime)));
                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                {
                    HttpSendImage(ImageStructsSide4);
                }));

                ImageStructsSide5.Add(new ImageStruct(m_Side1_ListSendBytes[8], string.Format(m_Side1_ImgName[8], curColor, curCode, curCtime)));
                ImageStructsSide5.Add(new ImageStruct(m_Side2_ListSendBytes[8], string.Format(m_Side2_ImgName[8], curColor, curCode, curCtime)));
                ImageStructsSide5.Add(new ImageStruct(m_Side3_ListSendBytes[8], string.Format(m_Side3_ImgName[8], curColor, curCode, curCtime)));
                ImageStructsSide5.Add(new ImageStruct(m_Side1_ListSendBytes[9], string.Format(m_Side1_ImgName[9], curColor, curCode, curCtime)));
                ImageStructsSide5.Add(new ImageStruct(m_Side2_ListSendBytes[9], string.Format(m_Side2_ImgName[9], curColor, curCode, curCtime)));
                ImageStructsSide5.Add(new ImageStruct(m_Side3_ListSendBytes[9], string.Format(m_Side3_ImgName[9], curColor, curCode, curCtime)));

                ImageStructsSide5.Add(new ImageStruct(m_Side4_ListSendBytes[8], string.Format(m_Side4_ImgName[8], curColor, curCode, curCtime)));
                ImageStructsSide5.Add(new ImageStruct(m_Side5_ListSendBytes[8], string.Format(m_Side5_ImgName[8], curColor, curCode, curCtime)));
                ImageStructsSide5.Add(new ImageStruct(m_Side6_ListSendBytes[8], string.Format(m_Side6_ImgName[8], curColor, curCode, curCtime)));
                ImageStructsSide5.Add(new ImageStruct(m_Side4_ListSendBytes[9], string.Format(m_Side4_ImgName[9], curColor, curCode, curCtime)));
                ImageStructsSide5.Add(new ImageStruct(m_Side5_ListSendBytes[9], string.Format(m_Side5_ImgName[9], curColor, curCode, curCtime)));
                ImageStructsSide5.Add(new ImageStruct(m_Side6_ListSendBytes[9], string.Format(m_Side6_ImgName[9], curColor, curCode, curCtime)));
                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                {
                    HttpSendImage(ImageStructsSide5);
                }));

                //Task.Run(() =>
                //{
                //    AllImageSave(m_Side1_ListSendBytes, m_Side1_ImgName, "Side_All");
                //});
                //Task.Run(() =>
                //{
                //    AllImageSave(m_Side2_ListSendBytes, m_Side2_ImgName, "Side_All");
                //});
                //Task.Run(() =>
                //{
                //    AllImageSave(m_Side3_ListSendBytes, m_Side3_ImgName, "Side_All");
                //});
                //Task.Run(() =>
                //{
                //    AllImageSave(m_Side4_ListSendBytes, m_Side4_ImgName, "Side_All");
                //});
                //Task.Run(() =>
                //{
                //    AllImageSave(m_Side5_ListSendBytes, m_Side5_ImgName, "Side_All");
                //});
                //Task.Run(() =>
                //{
                //    AllImageSave(m_Side6_ListSendBytes, m_Side6_ImgName, "Side_All");
                //});
            }
            catch (Exception ex)
            {

                ErrLog.WriteLogEx(ex.ToString());
            }
        }

        private void ImageStructS3()
        {
            List<ImageStruct> ImageStructs1_1 = new List<ImageStruct>();

            List<ImageStruct> ImageStructs1_2 = new List<ImageStruct>();

            List<ImageStruct> ImageStructs1_3 = new List<ImageStruct>();

            List<ImageStruct> ImageStructs1_4 = new List<ImageStruct>();

            for (int i = 0; i < 3; i++)
            {
                if (! m_DH1_ImgName[i].Contains("Bar"))
                {
                    ImageStruct imageStruct1 = new ImageStruct();
                    imageStruct1.byteImage = m_DH1_ListSendBytes[i];
                    imageStruct1.name = string.Format(m_DH1_ImgName[i], curColor, curCode, curCtime);
                    ImageStructs1_1.Add(imageStruct1);
                }

                if (!m_DH1_ImgName[i].Contains("Bar"))
                {
                    ImageStruct imageStruct1 = new ImageStruct();
                    imageStruct1.byteImage = m_DH1_ListSendBytes[i+3];
                    imageStruct1.name = string.Format(m_DH1_ImgName[i+3], curColor, curCode, curCtime);
                    ImageStructs1_2.Add(imageStruct1);
                }

                if (!m_DH1_ImgName[i].Contains("Bar"))
                {
                    ImageStruct imageStruct1 = new ImageStruct();
                    imageStruct1.byteImage = m_DH1_ListSendBytes[i + 6];
                    imageStruct1.name = string.Format(m_DH1_ImgName[i + 6], curColor, curCode, curCtime);
                    ImageStructs1_3.Add(imageStruct1);
                }

                if (!m_DH1_ImgName[i].Contains("Bar"))
                {
                    ImageStruct imageStruct1 = new ImageStruct();
                    imageStruct1.byteImage = m_DH1_ListSendBytes[i + 9];
                    imageStruct1.name = string.Format(m_DH1_ImgName[i + 9], curColor, curCode, curCtime);
                    ImageStructs1_4.Add(imageStruct1);
                }
            }


            for (int i = 0; i < 3; i++)
            {
                if (!m_DH2_ImgName[i].Contains("Bar"))
                {
                    ImageStruct imageStruct1 = new ImageStruct();
                    imageStruct1.byteImage = m_DH2_ListSendBytes[i];
                    imageStruct1.name = string.Format(m_DH2_ImgName[i], curColor, curCode, curCtime);
                    ImageStructs1_1.Add(imageStruct1);
                }

                if (!m_DH2_ImgName[i].Contains("Bar"))
                {
                    ImageStruct imageStruct1 = new ImageStruct();
                    imageStruct1.byteImage = m_DH2_ListSendBytes[i + 3];
                    imageStruct1.name = string.Format(m_DH2_ImgName[i + 3], curColor, curCode, curCtime);
                    ImageStructs1_2.Add(imageStruct1);
                }

                if (!m_DH2_ImgName[i].Contains("Bar"))
                {
                    ImageStruct imageStruct1 = new ImageStruct();
                    imageStruct1.byteImage = m_DH2_ListSendBytes[i + 6];
                    imageStruct1.name = string.Format(m_DH2_ImgName[i + 6], curColor, curCode, curCtime);
                    ImageStructs1_3.Add(imageStruct1);
                }

                if (!m_DH2_ImgName[i].Contains("Bar"))
                {
                    ImageStruct imageStruct1 = new ImageStruct();
                    imageStruct1.byteImage = m_DH2_ListSendBytes[i + 9];
                    imageStruct1.name = string.Format(m_DH2_ImgName[i + 9], curColor, curCode, curCtime);
                    ImageStructs1_4.Add(imageStruct1);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (!m_DH3_ImgName[i].Contains("Bar"))
                {
                    ImageStruct imageStruct1 = new ImageStruct();
                    imageStruct1.byteImage = m_DH3_ListSendBytes[i];
                    imageStruct1.name = string.Format(m_DH3_ImgName[i], curColor, curCode, curCtime);
                    ImageStructs1_1.Add(imageStruct1);
                }

                if (!m_DH3_ImgName[i].Contains("Bar"))
                {
                    ImageStruct imageStruct1 = new ImageStruct();
                    imageStruct1.byteImage = m_DH3_ListSendBytes[i + 3];
                    imageStruct1.name = string.Format(m_DH3_ImgName[i + 3], curColor, curCode, curCtime);
                    ImageStructs1_2.Add(imageStruct1);
                }

                if (!m_DH3_ImgName[i].Contains("Bar"))
                {
                    ImageStruct imageStruct1 = new ImageStruct();
                    imageStruct1.byteImage = m_DH3_ListSendBytes[i + 6];
                    imageStruct1.name = string.Format(m_DH3_ImgName[i + 6], curColor, curCode, curCtime);
                    ImageStructs1_3.Add(imageStruct1);
                }

                if (!m_DH3_ImgName[i].Contains("Bar"))
                {
                    ImageStruct imageStruct1 = new ImageStruct();
                    imageStruct1.byteImage = m_DH3_ListSendBytes[i + 9];
                    imageStruct1.name = string.Format(m_DH3_ImgName[i + 9], curColor, curCode, curCtime);
                    ImageStructs1_4.Add(imageStruct1);
                }
            }

            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate {
                HttpSendImage(ImageStructs1_1);
            }));

            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
            {
                HttpSendImage(ImageStructs1_2);
            }));

            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
            {
                HttpSendImage(ImageStructs1_3);
            }));
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
            {
                HttpSendImage(ImageStructs1_4);
            }));

            //Task.Run(() =>
            //{
            //    AllImageSave(m_DH1_ListSendBytes, m_DH1_ImgName, "DH_All");
            //});
            //Task.Run(() =>
            //{
            //    AllImageSave(m_DH2_ListSendBytes, m_DH2_ImgName, "DH_All");
            //});
            //Task.Run(() =>
            //{
            //    AllImageSave(m_DH3_ListSendBytes, m_DH3_ImgName, "DH_All");
            //});

            List<ImageStruct> ImageStructs1_5 = new List<ImageStruct>();

            List<ImageStruct> ImageStructs1_6 = new List<ImageStruct>();

            List<ImageStruct> ImageStructs1_7 = new List<ImageStruct>();

            List<ImageStruct> ImageStructs1_8 = new List<ImageStruct>();

            for (int i = 0; i < 3; i++)
            {
                if (!m_BC3_ImgName[i].Contains("Bar"))
                {
                    ImageStruct imageStruct1 = new ImageStruct();
                    imageStruct1.byteImage = m_BC1_ListSendBytes[i];
                    imageStruct1.name = string.Format(m_BC1_ImgName[i], curColor, curCode, curCtime);
                    ImageStructs1_5.Add(imageStruct1);
                }

                if (!m_BC1_ImgName[i].Contains("Bar"))
                {
                    ImageStruct imageStruct1 = new ImageStruct();
                    imageStruct1.byteImage = m_BC1_ListSendBytes[i + 3];
                    imageStruct1.name = string.Format(m_BC1_ImgName[i + 3], curColor, curCode, curCtime);
                    ImageStructs1_6.Add(imageStruct1);
                }

                if (!m_BC1_ImgName[i].Contains("Bar"))
                {
                    ImageStruct imageStruct1 = new ImageStruct();
                    imageStruct1.byteImage = m_BC1_ListSendBytes[i + 6];
                    imageStruct1.name = string.Format(m_BC1_ImgName[i + 6], curColor, curCode, curCtime);
                    ImageStructs1_7.Add(imageStruct1);
                }

                if (!m_BC1_ImgName[i].Contains("Bar"))
                {
                    ImageStruct imageStruct1 = new ImageStruct();
                    imageStruct1.byteImage = m_BC1_ListSendBytes[i + 9];
                    imageStruct1.name = string.Format(m_BC1_ImgName[i + 9], curColor, curCode, curCtime);
                    ImageStructs1_8.Add(imageStruct1);
                }
            }


            for (int i = 0; i < 3; i++)
            {
                if (!m_BC2_ImgName[i].Contains("Bar"))
                {
                    ImageStruct imageStruct1 = new ImageStruct();
                    imageStruct1.byteImage = m_BC2_ListSendBytes[i];
                    imageStruct1.name = string.Format(m_BC2_ImgName[i], curColor, curCode, curCtime);
                    ImageStructs1_5.Add(imageStruct1);
                }

                if (!m_BC2_ImgName[i].Contains("Bar"))
                {
                    ImageStruct imageStruct1 = new ImageStruct();
                    imageStruct1.byteImage = m_BC2_ListSendBytes[i + 3];
                    imageStruct1.name = string.Format(m_BC2_ImgName[i + 3], curColor, curCode, curCtime);
                    ImageStructs1_6.Add(imageStruct1);
                }

                if (!m_BC2_ImgName[i].Contains("Bar"))
                {
                    ImageStruct imageStruct1 = new ImageStruct();
                    imageStruct1.byteImage = m_BC2_ListSendBytes[i + 6];
                    imageStruct1.name = string.Format(m_BC2_ImgName[i + 6], curColor, curCode, curCtime);
                    ImageStructs1_7.Add(imageStruct1);
                }

                if (!m_BC2_ImgName[i].Contains("Bar"))
                {
                    ImageStruct imageStruct1 = new ImageStruct();
                    imageStruct1.byteImage = m_BC2_ListSendBytes[i + 9];
                    imageStruct1.name = string.Format(m_BC2_ImgName[i + 9], curColor, curCode, curCtime);
                    ImageStructs1_8.Add(imageStruct1);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (!m_BC3_ImgName[i].Contains("Bar"))
                {
                    ImageStruct imageStruct1 = new ImageStruct();
                    imageStruct1.byteImage = m_BC3_ListSendBytes[i];
                    imageStruct1.name = string.Format(m_BC3_ImgName[i], curColor, curCode, curCtime);
                    ImageStructs1_5.Add(imageStruct1);
                }

                if (!m_BC3_ImgName[i].Contains("Bar"))
                {
                    ImageStruct imageStruct1 = new ImageStruct();
                    imageStruct1.byteImage = m_BC3_ListSendBytes[i + 3];
                    imageStruct1.name = string.Format(m_BC3_ImgName[i + 3], curColor, curCode, curCtime);
                    ImageStructs1_6.Add(imageStruct1);
                }

                if (!m_BC3_ImgName[i].Contains("Bar"))
                {
                    ImageStruct imageStruct1 = new ImageStruct();
                    imageStruct1.byteImage = m_BC3_ListSendBytes[i + 6];
                    imageStruct1.name = string.Format(m_BC3_ImgName[i + 6], curColor, curCode, curCtime);
                    ImageStructs1_7.Add(imageStruct1);
                }

                if (!m_BC3_ImgName[i].Contains("Bar"))
                {
                    ImageStruct imageStruct1 = new ImageStruct();
                    imageStruct1.byteImage = m_BC3_ListSendBytes[i + 9];
                    imageStruct1.name = string.Format(m_BC3_ImgName[i + 9], curColor, curCode, curCtime);
                    ImageStructs1_8.Add(imageStruct1);
                }
            }

            //Task.Run(() =>
            //{
            //    AllImageSave(m_BC1_ListSendBytes, m_BC1_ImgName, "BC_All");
            //});
            //Task.Run(() =>
            //{
            //    AllImageSave(m_BC2_ListSendBytes, m_BC2_ImgName, "BC_All");
            //});
            //Task.Run(() =>
            //{
            //    AllImageSave(m_BC3_ListSendBytes, m_BC3_ImgName, "BC_All");
            //});


            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
            {
                HttpSendImage(ImageStructs1_5);
            }));

            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
            {
                HttpSendImage(ImageStructs1_6);
            }));

            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
            {
                HttpSendImage(ImageStructs1_7);
            }));
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
            {
                HttpSendImage(ImageStructs1_8);
            }));
        }
       
        #endregion
        private string GetUUID(RestResponse response)
        {
            string uuid = string.Empty;
            if (response.Content != null)
            {
                try
                {
                    JsonDocument jsonDoc = JsonDocument.Parse(response.Content);
                    uuid = jsonDoc.RootElement.GetProperty("mac_uuid").GetString();
                }
                catch
                {
                    uuid = "Cannot find mac_uuid in content";
                }
            }

            return uuid;
        }

        /// <summary>
        /// 获得结果后 上传给上位机
        /// </summary>
        /// <param name="response"></param>
        private void SendResult(RestResponse response)
        {
            if (response.Content != null)
            {
                try
                {

                    NetworkStream ns = client.Stream;
                    var paramSN = response.Request.Parameters.TryFind("serialnumber");
                    string sn = paramSN.Value as string;
                    JObject jsonObj = (JObject)JToken.Parse(response.Content);
                    jsonObj.AddFirst(new JProperty("sn", sn));
                    Byte[] sendBytes = Encoding.UTF8.GetBytes(jsonObj.ToString());
                    ns.Write(sendBytes, 0, sendBytes.Length);
                    ns.Flush();
                }
                catch (Exception ex)
                {
                    
                }
            }
        }

        /// <summary>
        /// TC拼接
        /// </summary>
        private void GetSitchTC()
        {
            try
            {
                //J413.{0}.{1}.CosmeticAOI.{2}.TC.Polarize.Pass_Fail.jpg
                //J413.{0}.{1}.CosmeticAOI.{2}.TC.Universal_Metal.Pass_Fail.jpg
                    List<byte[]> ImageSitchsMeta = new List<byte[]>();
                    List<byte[]> ImageSitchPolarize = new List<byte[]>();
                    for (int i = 0; i < 4; i++)
                    {
                        ImageSitchsMeta.Add(m_TC1_ListSitchBitmap[i*4]);
                        ImageSitchPolarize.Add(m_TC1_ListSitchBitmap[i*4+2]);
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        ImageSitchsMeta.Add(m_TC2_ListSitchBitmap[i * 4]);
                        ImageSitchPolarize.Add(m_TC2_ListSitchBitmap[i * 4 + 2]);
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        ImageSitchsMeta.Add(m_TC3_ListSitchBitmap[i * 4]);
                        ImageSitchPolarize.Add(m_TC3_ListSitchBitmap[i * 4 + 2]);
                    }

                    Task<Bitmap> task1 = Task.Run(() => imageProcess.MatContact(ImageSitchsMeta, 4, 3));
                    task1.ContinueWith((o) =>
                    {
                        picDebug.Invoke(new MethodInvoker(delegate
                        {
                            if (o.Result != null && o.Result.GetType().Name == "Bitmap")
                            {
                                //保存 拼接后图片
                                // o.Result.Save(string.Format(""));
                            }
                        }));
                    });

                    Task<Bitmap> task2 = Task.Run(() => imageProcess.MatContact(ImageSitchPolarize, 4, 3));
                    task2.ContinueWith((o) =>
                    {
                        picDebug.Invoke(new MethodInvoker(delegate
                        {
                            if (o.Result != null && o.Result.GetType().Name == "Bitmap")
                            {
                                //保存 拼接后图片
                                // o.Result.Save(string.Format(""));
                            }
                        }));
                    });
            }
            catch (Exception ex)
            {

               
            }
        }

        /// <summary>
        /// LCM拼接 
        /// </summary>
        private void GetSitchLCM()
        {
            try
            {
                //J413.{0}.{1}.CosmeticAOI.{2}.LCM.Coaxial.Pass_Fail.jpg
                //J413.{0}.{1}.CosmeticAOI.{2}.LCM.Pattern_1.Pass_Fail.jpg
                //J413.{0}.{1}.CosmeticAOI.{2}.LCM.Pattern_3.Pass_Fail.jpg

                List<byte[]> ImageSitchsCoaxial = new List<byte[]>();
                List<byte[]> ImageSitchPattern_1 = new List<byte[]>();
                List<byte[]> ImageSitchPattern_3 = new List<byte[]>();

                for (int i = 0; i < 4; i++)
                {
                    ImageSitchsCoaxial.Add(m_TC1_ListSitchBitmap[i * 7]);
                    ImageSitchPattern_1.Add(m_TC1_ListSitchBitmap[i * 7 + 3]);
                    ImageSitchPattern_3.Add(m_TC1_ListSitchBitmap[i * 7 + 5]);
                }

                for (int i = 0; i < 4; i++)
                {
                    ImageSitchsCoaxial.Add(m_TC2_ListSitchBitmap[i * 7]);
                    ImageSitchPattern_1.Add(m_TC2_ListSitchBitmap[i * 7 + 3]);
                    ImageSitchPattern_3.Add(m_TC2_ListSitchBitmap[i * 7 + 5]);
                }

                for (int i = 0; i < 4; i++)
                {
                    ImageSitchsCoaxial.Add(m_TC3_ListSitchBitmap[i * 7]);
                    ImageSitchPattern_1.Add(m_TC3_ListSitchBitmap[i * 7 + 3]);
                    ImageSitchPattern_3.Add(m_TC3_ListSitchBitmap[i * 7 + 5]);
                }


                Task<Bitmap> task1 = Task.Run(() => imageProcess.MatContact(ImageSitchsCoaxial, 4, 3));
                task1.ContinueWith((o) =>
                {
                    picDebug.Invoke(new MethodInvoker(delegate
                    {
                        if (o.Result != null && o.Result.GetType().Name == "Bitmap")
                        {
                            //保存 拼接后图片
                            // o.Result.Save(string.Format(""));
                        }
                    }));
                });

                Task<Bitmap> task2 = Task.Run(() => imageProcess.MatContact(ImageSitchPattern_1, 4, 3));
                task2.ContinueWith((o) =>
                {
                    picDebug.Invoke(new MethodInvoker(delegate
                    {
                        if (o.Result != null && o.Result.GetType().Name == "Bitmap")
                        {
                            //保存 拼接后图片
                            // o.Result.Save(string.Format(""));
                        }
                    }));
                });

                Task<Bitmap> task3 = Task.Run(() => imageProcess.MatContact(ImageSitchPattern_3, 4, 3));
                task3.ContinueWith((o) =>
                {
                    picDebug.Invoke(new MethodInvoker(delegate
                    {
                        if (o.Result != null && o.Result.GetType().Name == "Bitmap")
                        {
                            //保存 拼接后图片
                            // o.Result.Save(string.Format(""));
                        }
                    }));
                });
            }
            catch (Exception ex)
            {

                
            }
        }

        /// <summary>
        ///  SIde  拼图疑问
        /// </summary>
        private void GetSitchCorner_Side()
        {
            //J413.{0}.{1}.CosmeticAOI.{2}.LCM.Corner.Polarize.Pass_Fail.jpg
            //J413.{0}.{1}.CosmeticAOI.{2}.LCM.Side1.Pass_Fail.jpg
            //J413.{0}.{1}.CosmeticAOI.{2}.LCM.Side2.Pass_Fail.jpg
            //J413.{0}.{1}.CosmeticAOI.{2}.LCM.Side3.Pass_Fail.jpg
            //J413.{0}.{1}.CosmeticAOI.{2}.LCM.Side4.Pass_Fail.jpg

            List<byte[]> ImageSitchsCorner = new List<byte[]>();
            ImageSitchsCorner.Add(m_Corner1_ListSitchBitmap[0]);
            ImageSitchsCorner.Add(m_Corner1_ListSitchBitmap[2]);
            ImageSitchsCorner.Add(m_Corner2_ListSitchBitmap[0]);
            ImageSitchsCorner.Add(m_Corner2_ListSitchBitmap[2]);

            Task<Bitmap> task = Task.Run(() => imageProcess.MatContact(ImageSitchsCorner, 2, 2));
            task.ContinueWith((o) =>
            {
                picDebug.Invoke(new MethodInvoker(delegate
                {
                    if (o.Result != null && o.Result.GetType().Name == "Bitmap")
                    {
                        //保存 拼接后图片
                         //o.Result.Save(string.Format("J413.{0}.{1}.CosmeticAOI.{2}.LCM.Corner.Polarize.Pass_Fail.jpg",curColor,curCode,curCtime));
                    }
                }));
            });




        }

        /// <summary>
        /// DH  BC  拼图
        /// </summary>
        private void GetSitchBC_DH()
        {
            try
            {
                //J413.{0}.{1}.CosmeticAOI.{2}.BC.Polarize.Pass_Fail.jpg
                //J413.{0}.{1}.CosmeticAOI.{2}.BC.Universal.Pass_Fail.jpg
                //J413.{0}.{1}.CosmeticAOI.{2}.DH.Polarize.Pass_Fail.jpg
                //J413.{0}.{1}.CosmeticAOI.{2}.DH.Universal.Pass_Fail.jpg
                List<byte[]> ImageSitchsBCUniversal = new List<byte[]>();
                List<byte[]> ImageSitchBCPolarize = new List<byte[]>();

                for (int i = 0; i < 4; i++)
                {
                    ImageSitchBCPolarize.Add(m_BC1_ListSitchBitmap[i*3]);
                    ImageSitchsBCUniversal.Add(m_BC1_ListSitchBitmap[i * 3+2]);
                }

                for (int i = 0; i < 4; i++)
                {
                    ImageSitchBCPolarize.Add(m_BC2_ListSitchBitmap[i * 3]);
                    ImageSitchsBCUniversal.Add(m_BC2_ListSitchBitmap[i * 3 + 2]);
                }

                for (int i = 0; i < 4; i++)
                {
                    ImageSitchBCPolarize.Add(m_BC3_ListSitchBitmap[i * 3]);
                    ImageSitchsBCUniversal.Add(m_BC3_ListSitchBitmap[i * 3 + 2]);
                }

                Task<Bitmap> task1 = Task.Run(() => imageProcess.MatContact(ImageSitchBCPolarize, 4, 3));
                task1.ContinueWith((o) =>
                {
                    picDebug.Invoke(new MethodInvoker(delegate
                    {
                        if (o.Result != null && o.Result.GetType().Name == "Bitmap")
                        {
                            //保存 拼接后图片
                            // o.Result.Save(string.Format(""));
                        }
                    }));
                });


                Task<Bitmap> task2 = Task.Run(() => imageProcess.MatContact(ImageSitchsBCUniversal, 4, 3));
                task2.ContinueWith((o) =>
                {
                    picDebug.Invoke(new MethodInvoker(delegate
                    {
                        if (o.Result != null && o.Result.GetType().Name == "Bitmap")
                        {
                            //保存 拼接后图片
                            // o.Result.Save(string.Format(""));
                        }
                    }));
                });



                List<byte[]> ImageSitchsDHUniversal = new List<byte[]>();
                List<byte[]> ImageSitcDHPolarize = new List<byte[]>();

                for (int i = 0; i < 4; i++)
                {
                    ImageSitcDHPolarize.Add(m_DH1_ListSitchBitmap[i * 3]);
                    ImageSitchsDHUniversal.Add(m_DH1_ListSitchBitmap[i * 3 + 2]);
                }

                for (int i = 0; i < 4; i++)
                {
                    ImageSitcDHPolarize.Add(m_DH2_ListSitchBitmap[i * 3]);
                    ImageSitchsDHUniversal.Add(m_DH2_ListSitchBitmap[i * 3 + 2]);
                }

                for (int i = 0; i < 4; i++)
                {
                    ImageSitcDHPolarize.Add(m_DH3_ListSitchBitmap[i * 3]);
                    ImageSitchsDHUniversal.Add(m_DH3_ListSitchBitmap[i * 3 + 2]);
                }

                Task<Bitmap> task3 = Task.Run(() => imageProcess.MatContact(ImageSitchsDHUniversal, 4, 3));
                task3.ContinueWith((o) =>
                {
                    picDebug.Invoke(new MethodInvoker(delegate
                    {
                        if (o.Result != null && o.Result.GetType().Name == "Bitmap")
                        {
                            //保存 拼接后图片
                            // o.Result.Save(string.Format(""));
                        }
                    }));
                });


                Task<Bitmap> task4 = Task.Run(() => imageProcess.MatContact(ImageSitcDHPolarize, 4, 3));
                task4.ContinueWith((o) =>
                {
                    picDebug.Invoke(new MethodInvoker(delegate
                    {
                        if (o.Result != null && o.Result.GetType().Name == "Bitmap")
                        {
                            //保存 拼接后图片
                             // o.Result.Save(string.Format(""));
                        }
                    }));
                });



            }
            catch (Exception ex)
            {

                
            }
        }

        private string GetPathSection(string s)
        {
            string section = "";
            if (s.Contains("TC"))
            {
                section = "TC_All";
            }
            else if (s.Contains("Mandrel"))
            {
                section = "Mandrel_All";
            }
            else if (s.Contains("LCM"))
            {
                section = "LCM_All";
            }
            else if (s.Contains("BC"))
            {
                section = "BC_All";
            }
            else if (s.Contains("DH"))
            {
                section = "DH_All";
            }
            else if (s.Contains("Corner"))
            {
                section = "Corner_All";
            }
            else if (s.Contains("Side"))
            {
                section = "Side_All";
            }
            return section;
        }

        /// <summary>
        /// /Http 上传
        /// </summary>
        /// <param name="li"></param>
        private void HttpSendImage(List<ImageStruct> li)
        {

            string savepath = "E:\\MaskImage\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + curCode + "\\";
            if (!Directory.Exists(savepath))
            {
                Directory.CreateDirectory(savepath);
            }
            for (int i = 0; i < li.Count; i++)
            {
                string pathsection = GetPathSection(li[i].name);
                imageProcess.BytesToFile(li[i].byteImage,savepath+pathsection+"\\"+li[i].name);
            }

            #region 发送

            //List<string> contentlist = new List<string>();
            ////J413.S.GY67GL69CW.CosmeticAOI.20220813.233339837.Mandrel.Universal.1_1.jpg
            //string timeStamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.ffffffzz00");
            //RestRequest request = new RestRequest("", Method.Post);
            //request.Timeout = 10000; // unit ms
            //request.AddParameter("serialnumber", curCode);
            //request.AddParameter("vendor", "LEADMLFLAG");
            //request.AddParameter("timestamp", timeStamp);
            //request.AddParameter("use_sample", "0");
            //request.AddParameter("test_mode", 1);

            //float totalSize = 0;
            //for (int i = 0; i < li.Count; i++)
            //{
            //     string contents = imageProcess.ContentName(li[i].name);
            //     contentlist.Add(contents);
            //     request.AddParameter("contents", contents);
            //    if (li[i].byteImage != null)
            //    {
            //        request.AddFile(contents, li[i].byteImage, li[i].name);
            //        totalSize += li[i].byteImage.Length;
            //    }
              
            //}
            //totalSize = totalSize / (1024 * 1024);
            //Stopwatch singleWatch = Stopwatch.StartNew();
            //RestResponse response = restClient.Execute(request);
            //ErrLog.WriteLogData(string.Format("发送图片包:{0}MB", totalSize.ToString("F3")));
           
            //foreach (var file in request.Files)
            //{
            //    ErrLog.WriteLogData(string.Format("图片名: {0}", file.FileName));
            //}
            //int statusCode = (int)response.StatusCode;
            //if (response != null && (statusCode == 200 || statusCode == 201))
            //{
            //    singleWatch.Stop();
            //    ErrLog.WriteLogData(string.Format("发送{0}张图片成功, 耗时：{1} ms", li.Count, singleWatch.ElapsedMilliseconds));
            //    ErrLog.WriteLogData(response.Content);
            //    //发送成功 
            //    SendResult(response);
                
            //    var judgeList = MLJsonParser.Parse(JObject.Parse(response.Content));
            //    foreach ((string sn, string contents, string decision) in judgeList)
            //    {
            //        if (decision == "FAIL")
            //        {
            //            if (contentlist.Contains(contents))
            //            {
            //                int index = contentlist.FindIndex( con => con == contents);
            //                if (index > 0)
            //                {
            //                    //NG保存
            //                    Task.Run( ()=> {
            //                        imageProcess.BytesToFile(li[index].byteImage, SysConfig.ImageSavePath + li[index].name);
            //                    });
            //                } 
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    if (response != null)
            //    {
            //        string uuid = GetUUID(response);
            //        if (response.StatusCode == HttpStatusCode.RequestTimeout)
            //        {
            //            //发送超时
            //            ErrLog.WriteLogData("图片上传发送超时");
            //        }

            //    }

            //    if (response != null && (int)response.StatusCode == 472)
            //    {
            //        RestResponse retransmitResponse = restClient.Execute(request);

            //        Thread.Sleep(1000);
            //        if (retransmitResponse != null && retransmitResponse.StatusCode == HttpStatusCode.OK)
            //        {
            //            ErrLog.WriteLogData("图片重传成功");
            //        }
            //        else
            //        {
            //            ErrLog.WriteLogData("图片重传失败");
            //        }
            //    }
            //}
          #endregion
        

        }

        /// <summary>
        /// 发送队列清除
        /// </summary>
        private void ClearList()
        {
            lock (this)
            {
                //S1站相机
                m_Mandrel_ListSendBytes.Clear();
                m_LCM1_ListSendBytes.Clear();
                m_LCM2_ListSendBytes.Clear();
                m_LCM3_ListSendBytes.Clear();

                m_Mandrel_ListSitchBitmap.Clear();
                m_LCM1_ListSitchBitmap.Clear();
                m_LCM2_ListSitchBitmap.Clear();
                m_LCM3_ListSitchBitmap.Clear();

                //S2
                m_TC1_ListSendBytes.Clear();
                m_TC2_ListSendBytes.Clear();
                m_TC3_ListSendBytes.Clear();

                m_Corner1_ListSendBytes.Clear();
                m_Corner2_ListSendBytes.Clear();

                m_Side1_ListSendBytes.Clear();
                m_Side2_ListSendBytes.Clear();
                m_Side3_ListSendBytes.Clear();
                m_Side4_ListSendBytes.Clear();
                m_Side5_ListSendBytes.Clear();
                m_Side6_ListSendBytes.Clear();

                //s3
                m_BC1_ListSendBytes.Clear();
                m_BC2_ListSendBytes.Clear();
                m_BC3_ListSendBytes.Clear();

                m_DH1_ListSendBytes.Clear();
                m_DH2_ListSendBytes.Clear();
                m_DH3_ListSendBytes.Clear();
                //s1
                m_Mandrel_ListSitchBitmap.Clear();
                m_LCM1_ListSitchBitmap.Clear();
                m_LCM2_ListSitchBitmap.Clear();
                m_LCM3_ListSitchBitmap.Clear();

                //S2
                m_TC1_ListSitchBitmap.Clear();
                m_TC2_ListSitchBitmap.Clear();
                m_TC3_ListSitchBitmap.Clear();

                m_Corner1_ListSitchBitmap.Clear();
                m_Corner2_ListSitchBitmap.Clear();

                m_Side1_ListSitchBitmap.Clear();
                m_Side2_ListSitchBitmap.Clear();
                m_Side3_ListSitchBitmap.Clear();
                m_Side4_ListSitchBitmap.Clear();
                m_Side5_ListSitchBitmap.Clear();
                m_Side6_ListSitchBitmap.Clear();

                //S3
                m_BC1_ListSitchBitmap.Clear();
                m_BC2_ListSitchBitmap.Clear();
                m_BC3_ListSitchBitmap.Clear();

                m_DH1_ListSitchBitmap.Clear();
                m_DH2_ListSitchBitmap.Clear();
                m_DH3_ListSitchBitmap.Clear();
                GC.Collect();
                
            }
        }


        /// <summary>
        /// 图片名称加载   变量初始化
        /// </summary>
        private void Init()
        {
            try
            {
                restClient = new RestClient(string.Format("http://{0}:{1}/v1/inspect", "IP", "Port"));

                startpoint = new System.Drawing.Point(0, 0);
                endpoint = new System.Drawing.Point(0, 0);


                m_Mandrel_ListSendBytes = new List<byte[]>();
                m_LCM1_ListSendBytes = new List<byte[]>();
                m_LCM2_ListSendBytes = new List<byte[]>();
                m_LCM3_ListSendBytes = new List<byte[]>();


                m_Corner1_ListSendBytes = new List<byte[]>();
                m_Corner2_ListSendBytes = new List<byte[]>();

                m_Side1_ListSendBytes = new List<byte[]>();
                m_Side2_ListSendBytes = new List<byte[]>();
                m_Side3_ListSendBytes = new List<byte[]>();
                m_Side4_ListSendBytes = new List<byte[]>();
                m_Side5_ListSendBytes = new List<byte[]>();
                m_Side6_ListSendBytes = new List<byte[]>();

                //s3
                m_BC1_ListSendBytes = new List<byte[]>();
                m_BC2_ListSendBytes = new List<byte[]>();
                m_BC3_ListSendBytes = new List<byte[]>();

                m_DH1_ListSendBytes = new List<byte[]>();
                m_DH2_ListSendBytes = new List<byte[]>();
                m_DH3_ListSendBytes = new List<byte[]>();
                //s1
                m_Mandrel_ListSitchBitmap = new List<byte[]>();
                m_LCM1_ListSitchBitmap = new List<byte[]>();
                m_LCM2_ListSitchBitmap = new List<byte[]>();
                m_LCM3_ListSitchBitmap = new List<byte[]>();

                //S2
                m_TC1_ListSitchBitmap = new List<byte[]>();
                m_TC2_ListSitchBitmap = new List<byte[]>();
                m_TC3_ListSitchBitmap = new List<byte[]>();

                m_Corner1_ListSitchBitmap = new List<byte[]>();
                m_Corner2_ListSitchBitmap = new List<byte[]>();

                m_Side1_ListSitchBitmap = new List<byte[]>();
                m_Side2_ListSitchBitmap = new List<byte[]>();
                m_Side3_ListSitchBitmap = new List<byte[]>();
                m_Side4_ListSitchBitmap = new List<byte[]>();
                m_Side5_ListSitchBitmap = new List<byte[]>();
                m_Side6_ListSitchBitmap = new List<byte[]>();

                //S3
                //s3
                m_BC1_ListSitchBitmap = new List<byte[]>();
                m_BC2_ListSitchBitmap = new List<byte[]>();
                m_BC3_ListSitchBitmap = new List<byte[]>();

                m_DH1_ListSitchBitmap = new List<byte[]>();
                m_DH2_ListSitchBitmap = new List<byte[]>();
                m_DH3_ListSitchBitmap = new List<byte[]>();


                //工位一
                m_Mandrel_ImgName = new List<string>();
                 m_LCM1_ImgName = new List<string>();
                m_LCM2_ImgName = new List<string>();
                m_LCM3_ImgName = new List<string>();
                //工位二
                m_TC1_ImgName = new List<string>();
                m_TC2_ImgName = new List<string>();
                 m_TC3_ImgName = new List<string>();

                 m_Corner1_ImgName = new List<string>();
                 m_Corner2_ImgName = new List<string>();

                 m_Side1_ImgName = new List<string>();
                 m_Side2_ImgName = new List<string>();
                 m_Side3_ImgName = new List<string>();
                 m_Side4_ImgName = new List<string>();
                 m_Side5_ImgName = new List<string>();
                 m_Side6_ImgName = new List<string>();

                //工位三
                 m_BC1_ImgName = new List<string>();
                 m_BC2_ImgName = new List<string>();
                 m_BC3_ImgName = new List<string>();

                 m_DH1_ImgName = new List<string>();
                 m_DH2_ImgName = new List<string>();
                 m_DH3_ImgName = new List<string>();

                #region 工位一图片读取名称
                using (FileStream stream = File.OpenRead(Application.StartupPath + "\\ImgName\\工位1_ImgName.ini"))
                 {
                       using (StreamReader reader = new StreamReader(stream, Encoding.Default))
                       {
                            while (reader.Peek() != -1)
                            {
                                string str = reader.ReadLine();
                                if (str.Length > 20)
                                {
                                    if (m_Mandrel_ImgName.Count < 4)
                                    {
                                        m_Mandrel_ImgName.Add(str);
                                        continue;
                                    }
                                    if (m_LCM1_ImgName.Count < 28)
                                    {
                                        m_LCM1_ImgName.Add(str);
                                        continue;
                                    }
                                    if (m_LCM2_ImgName.Count < 28)
                                    {
                                        m_LCM2_ImgName.Add(str);
                                        continue;
                                    }
                                    if (m_LCM3_ImgName.Count < 28)
                                    {
                                        m_LCM3_ImgName.Add(str);
                                        continue;
                                    }
                                }
                            }
                       }
                }
                #endregion

                if (m_Mandrel_ImgName.Count != 4 && m_LCM1_ImgName.Count != 28 && m_LCM2_ImgName.Count != 28 && m_LCM3_ImgName.Count != 28)
                {
                    //工位一加载图片名称错误
                }

                #region 工位二图片读取名称
                using (FileStream stream = File.OpenRead(Application.StartupPath + "\\ImgName\\工位2_ImgName.ini"))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.Default))
                    {
                        while (reader.Peek() != -1)
                        {
                            string str = reader.ReadLine();
                            if (str.Length > 20)
                            {
                                if (m_TC1_ImgName.Count < 16)
                                {
                                    m_TC1_ImgName.Add(str);
                                    continue;
                                }
                                if (m_TC2_ImgName.Count < 16)
                                {
                                    m_TC2_ImgName.Add(str);
                                    continue;
                                }
                                if (m_TC3_ImgName.Count < 16)
                                {
                                    m_TC3_ImgName.Add(str);
                                    continue;
                                }
                                
                                if (m_Corner1_ImgName.Count < 4)
                                {
                                    m_Corner1_ImgName.Add(str);
                                    continue;
                                }

                                if (m_Corner2_ImgName.Count < 4)
                                {
                                    m_Corner2_ImgName.Add(str);
                                    continue;
                                }


                                if (m_Side1_ImgName.Count < 10)
                                {
                                    m_Side1_ImgName.Add(str);
                                    continue;
                                }
                                if (m_Side2_ImgName.Count < 10)
                                {
                                    m_Side2_ImgName.Add(str);
                                    continue;
                                }
                                if (m_Side3_ImgName.Count < 10)
                                {
                                    m_Side3_ImgName.Add(str);
                                    continue;
                                }
                                if (m_Side4_ImgName.Count < 10)
                                {
                                    m_Side4_ImgName.Add(str);
                                    continue;
                                }
                                if (m_Side5_ImgName.Count < 10)
                                {
                                    m_Side5_ImgName.Add(str);
                                    continue;
                                }
                                if (m_Side6_ImgName.Count < 10)
                                {
                                    m_Side6_ImgName.Add(str);
                                    continue;
                                }
                            }
                        }
                    }
                }
                #endregion

                if (m_TC1_ImgName.Count != 16 && m_TC2_ImgName.Count != 16 && m_TC3_ImgName.Count != 16 && m_Corner1_ImgName.Count != 4 && m_Corner2_ImgName.Count != 4)
                {
                    //工位二加载图片名称错误
                }

                if (m_Side1_ImgName.Count != 10 && m_Side2_ImgName.Count != 10 && m_Side3_ImgName.Count != 10 && m_Side4_ImgName.Count != 10 && m_Side5_ImgName.Count != 10 && m_Side6_ImgName.Count != 10)
                {
                    //工位二加载图片名称错误
                }

                #region 工位三图片读取名称
                using (FileStream stream = File.OpenRead(Application.StartupPath + "\\ImgName\\工位3_ImgName.ini"))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.Default))
                    {
                        while (reader.Peek() != -1)
                        {
                            string str = reader.ReadLine();
                            if (str.Length > 20)
                            {
                                if (m_BC1_ImgName.Count < 12)
                                {
                                    m_BC1_ImgName.Add(str);
                                    continue;
                                }
                                if (m_BC2_ImgName.Count < 12)
                                {
                                    m_BC2_ImgName.Add(str);
                                    continue;
                                }
                                if (m_BC3_ImgName.Count < 12)
                                {
                                    m_BC3_ImgName.Add(str);
                                    continue;
                                }

                                if (m_DH1_ImgName.Count < 12)
                                {
                                    m_DH1_ImgName.Add(str);
                                    continue;
                                }
                                if (m_DH2_ImgName.Count < 12)
                                {
                                    m_DH2_ImgName.Add(str);
                                    continue;
                                }
                                if (m_DH3_ImgName.Count < 12)
                                {
                                    m_DH3_ImgName.Add(str);
                                    continue;
                                }
                            }
                        }
                    }
                }
                #endregion

                if (m_BC1_ImgName.Count != 12 && m_BC1_ImgName.Count != 12 && m_BC1_ImgName.Count != 12 && m_DH3_ImgName.Count != 12 && m_DH2_ImgName.Count != 12 && m_DH1_ImgName.Count != 12)
                {
                    //工位二加载图片名称错误
                }
            }
            catch (Exception ex)
            {

                
            }
        }
      
        /// <summary>
        /// 图片显示
        /// </summary>
        /// <param name="cam"></param>
        /// <param name="map"></param>
        private void DisplayImage(uint cam, Bitmap map)
        {
            PictureBox picbox = Controls.Find("pictureBox" + cam.ToString(),true)[0] as PictureBox;
            if (picbox != null)
            {
                Bitmap map1 = imageProcess.Rotation(map, SysConfig.m_Rotation[Convert.ToInt32(cam) - 1]);
                map.Dispose();
                picbox.Image = map1;
               
                picbox.Invalidate();
            }
        }



        /// <summary>
        /// 相机初始化设置
        /// </summary>
        private void InitCam()
        {
            try
            {
                acqFIFOManager = new AcqFIFOManager();
                acqFIFOManager.InitAcqFIFOManager();

                for (uint i = 0; i < SysConfig.CamNum; i++)
                {
                    acqFIFOManager.SetGain(i, (float)SysConfig.m_CamGain[(int)i]);
                    acqFIFOManager[i].CfgSetWB( Convert.ToUInt32(SysConfig.m_CamRGB[(int)i][0]), Convert.ToUInt32(SysConfig.m_CamRGB[(int)i][1]), Convert.ToUInt32(SysConfig.m_CamRGB[(int)i][2]));
                    acqFIFOManager[i].SetROIXYWidthHeight(Convert.ToUInt32(SysConfig.m_CamOffset[(int)i][0]), Convert.ToUInt32(SysConfig.m_CamOffset[(int)i][1]), Convert.ToUInt32(SysConfig.m_CamOffset[(int)i][2]), Convert.ToUInt32(SysConfig.m_CamOffset[(int)i][3]));
                   
                    acqFIFOManager.SetTriggerModel(Convert.ToUInt32(i + 1), true);
                    acqFIFOManager.StartAcquire(Convert.ToUInt32(i + 1));
                    Thread.Sleep(50);
                }

                #region MyRegion
                //acqFIFOManager.SetGain(1,(float)SysConfig.Gain1);
                //acqFIFOManager.AcqFifo1.CfgSetWB(Convert.ToUInt32(SysConfig.RGB1[0]) , Convert.ToUInt32(SysConfig.RGB1[1]), Convert.ToUInt32(SysConfig.RGB1[2]));
                //acqFIFOManager.AcqFifo1.SetROIXYWidthHeight(Convert.ToUInt32(SysConfig.offset1XY[0]), Convert.ToUInt32(SysConfig.offset1XY[1]), Convert.ToUInt32(SysConfig.offset1XY[2]), Convert.ToUInt32(SysConfig.offset1XY[3]));
                //acqFIFOManager.SetTriggerModel(1,true);
                //acqFIFOManager.StartAcquire(1);

                //acqFIFOManager.SetGain(2, (float)SysConfig.Gain2);
                //acqFIFOManager.AcqFifo2.CfgSetWB(Convert.ToUInt32(SysConfig.RGB2[0]), Convert.ToUInt32(SysConfig.RGB2[1]), Convert.ToUInt32(SysConfig.RGB2[2]));
                //acqFIFOManager.AcqFifo2.SetROIXYWidthHeight(Convert.ToUInt32(SysConfig.offset2XY[0]), Convert.ToUInt32(SysConfig.offset2XY[1]), Convert.ToUInt32(SysConfig.offset2XY[2]), Convert.ToUInt32(SysConfig.offset2XY[3]));
                //acqFIFOManager.SetTriggerModel(2, true);
                //acqFIFOManager.StartAcquire(2);

                //acqFIFOManager.SetGain(3, (float)SysConfig.Gain3);
                //acqFIFOManager.AcqFifo3.CfgSetWB(Convert.ToUInt32(SysConfig.RGB3[0]), Convert.ToUInt32(SysConfig.RGB3[1]), Convert.ToUInt32(SysConfig.RGB3[2]));
                //acqFIFOManager.AcqFifo3.SetROIXYWidthHeight(Convert.ToUInt32(SysConfig.offset3XY[0]), Convert.ToUInt32(SysConfig.offset3XY[1]), Convert.ToUInt32(SysConfig.offset3XY[2]), Convert.ToUInt32(SysConfig.offset3XY[3]));
                //acqFIFOManager.SetTriggerModel(3, true);
                //acqFIFOManager.StartAcquire(3);

                //acqFIFOManager.SetGain(4, (float)SysConfig.Gain4);
                //acqFIFOManager.AcqFifo4.CfgSetWB(Convert.ToUInt32(SysConfig.RGB4[0]), Convert.ToUInt32(SysConfig.RGB4[1]), Convert.ToUInt32(SysConfig.RGB4[2]));
                //acqFIFOManager.AcqFifo4.SetROIXYWidthHeight(Convert.ToUInt32(SysConfig.offset4XY[0]), Convert.ToUInt32(SysConfig.offset4XY[1]), Convert.ToUInt32(SysConfig.offset4XY[2]), Convert.ToUInt32(SysConfig.offset4XY[3]));
                //acqFIFOManager.SetTriggerModel(4, true);
                //acqFIFOManager.StartAcquire(4);

                //acqFIFOManager.SetGain(5, (float)SysConfig.Gain5);
                //acqFIFOManager.AcqFifo5.CfgSetWB(Convert.ToUInt32(SysConfig.RGB5[0]), Convert.ToUInt32(SysConfig.RGB5[1]), Convert.ToUInt32(SysConfig.RGB5[2]));
                //acqFIFOManager.AcqFifo5.SetROIXYWidthHeight(Convert.ToUInt32(SysConfig.offset5XY[0]), Convert.ToUInt32(SysConfig.offset5XY[1]), Convert.ToUInt32(SysConfig.offset5XY[2]), Convert.ToUInt32(SysConfig.offset5XY[3]));
                //acqFIFOManager.SetTriggerModel(5, true);
                //acqFIFOManager.StartAcquire(5);

                //acqFIFOManager.SetGain(6, (float)SysConfig.Gain6);
                //acqFIFOManager.AcqFifo6.CfgSetWB(Convert.ToUInt32(SysConfig.RGB6[0]), Convert.ToUInt32(SysConfig.RGB6[1]), Convert.ToUInt32(SysConfig.RGB6[2]));
                //acqFIFOManager.AcqFifo6.SetROIXYWidthHeight(Convert.ToUInt32(SysConfig.offset6XY[0]), Convert.ToUInt32(SysConfig.offset6XY[1]), Convert.ToUInt32(SysConfig.offset6XY[2]), Convert.ToUInt32(SysConfig.offset6XY[3]));
                //acqFIFOManager.SetTriggerModel(6, true);
                //acqFIFOManager.StartAcquire(6);

                //acqFIFOManager.SetGain(7, (float)SysConfig.Gain7);
                //acqFIFOManager.AcqFifo7.CfgSetWB(Convert.ToUInt32(SysConfig.RGB7[0]), Convert.ToUInt32(SysConfig.RGB7[1]), Convert.ToUInt32(SysConfig.RGB7[2]));
                //acqFIFOManager.AcqFifo7.SetROIXYWidthHeight(Convert.ToUInt32(SysConfig.offset7XY[0]), Convert.ToUInt32(SysConfig.offset7XY[1]), Convert.ToUInt32(SysConfig.offset7XY[2]), Convert.ToUInt32(SysConfig.offset7XY[3]));
                //acqFIFOManager.SetTriggerModel(7, true);
                //acqFIFOManager.StartAcquire(7);

                //acqFIFOManager.SetGain(8, (float)SysConfig.Gain8);
                //acqFIFOManager.AcqFifo8.CfgSetWB(Convert.ToUInt32(SysConfig.RGB8[0]), Convert.ToUInt32(SysConfig.RGB8[1]), Convert.ToUInt32(SysConfig.RGB8[2]));
                //acqFIFOManager.AcqFifo8.SetROIXYWidthHeight(Convert.ToUInt32(SysConfig.offset8XY[0]), Convert.ToUInt32(SysConfig.offset8XY[1]), Convert.ToUInt32(SysConfig.offset8XY[2]), Convert.ToUInt32(SysConfig.offset8XY[3]));
                //acqFIFOManager.SetTriggerModel(8, true);
                //acqFIFOManager.StartAcquire(8);


                //acqFIFOManager.SetGain(9, (float)SysConfig.Gain9);
                //acqFIFOManager.AcqFifo9.CfgSetWB(Convert.ToUInt32(SysConfig.RGB9[0]), Convert.ToUInt32(SysConfig.RGB9[1]), Convert.ToUInt32(SysConfig.RGB9[2]));
                //acqFIFOManager.AcqFifo9.SetROIXYWidthHeight(Convert.ToUInt32(SysConfig.offset9XY[0]), Convert.ToUInt32(SysConfig.offset9XY[1]), Convert.ToUInt32(SysConfig.offset9XY[2]), Convert.ToUInt32(SysConfig.offset9XY[3]));
                //acqFIFOManager.SetTriggerModel(9, true);
                //acqFIFOManager.StartAcquire(9);

                //acqFIFOManager.SetGain(10, (float)SysConfig.Gain10);
                //acqFIFOManager.AcqFifo10.CfgSetWB(Convert.ToUInt32(SysConfig.RGB10[0]), Convert.ToUInt32(SysConfig.RGB10[1]), Convert.ToUInt32(SysConfig.RGB10[2]));
                //acqFIFOManager.AcqFifo10.SetROIXYWidthHeight(Convert.ToUInt32(SysConfig.offset10XY[0]), Convert.ToUInt32(SysConfig.offset10XY[1]), Convert.ToUInt32(SysConfig.offset10XY[2]), Convert.ToUInt32(SysConfig.offset10XY[3]));
                //acqFIFOManager.SetTriggerModel(10, true);
                //acqFIFOManager.StartAcquire(10);

                //acqFIFOManager.SetGain(11, (float)SysConfig.Gain11);
                //acqFIFOManager.AcqFifo11.CfgSetWB(Convert.ToUInt32(SysConfig.RGB11[0]), Convert.ToUInt32(SysConfig.RGB11[1]), Convert.ToUInt32(SysConfig.RGB11[2]));
                //acqFIFOManager.AcqFifo11.SetROIXYWidthHeight(Convert.ToUInt32(SysConfig.offset11XY[0]), Convert.ToUInt32(SysConfig.offset11XY[1]), Convert.ToUInt32(SysConfig.offset11XY[2]), Convert.ToUInt32(SysConfig.offset11XY[3]));
                //acqFIFOManager.SetTriggerModel(11, true);
                //acqFIFOManager.StartAcquire(11);
                #endregion
                acqFIFOManager.OnImageReady += AcqFIFOManager_OnImageReady;
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 相机图片事件
        /// </summary>
        /// <param name="camNo"></param>
        /// <param name="ho_Image"></param>
        private void AcqFIFOManager_OnImageReady(uint camNo, Bitmap ho_Image)
        {
            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new ImageReadyHandler(AcqFIFOManager_OnImageReady), camNo, ho_Image);
                    return;
                }
                if (!m_IsRun)
                {
                    Bitmap map1 = imageProcess.Rotation(ho_Image, SysConfig.m_Rotation[Convert.ToInt32(camNo) - 1]);
                    picDebug.Image = map1;
                    ho_Image.Dispose();
                    return;
                }
                //DisplayImage(camNo, ho_Image);
                switch (camNo)
                {

                    case 1:
                        //发送压缩为 75%质量图像   拼图先尺寸压缩为原来的50  再质量压缩为原来的 50%

                            Task.Run( ()=> {
                                Monitor.Enter(lockObj[Convert.ToInt32(camNo) - 1]);
                                Bitmap map1 = imageProcess.Rotation(ho_Image, SysConfig.m_Rotation[Convert.ToInt32(camNo) - 1]);
                                pictureBox1.Image = map1;
                                m_Mandrel_ListSendBytes.Add(imageProcess.MatCompressImage(map1.ToMat(), 75));
                                ho_Image.Dispose();
                                map1.Dispose();
                                Monitor.Exit(lockObj[Convert.ToInt32(camNo) - 1]);

                            });



                        //s2
                        //    Bitmap map = imageProcess.Rotation(ho_Image, SysConfig.m_Rotation[Convert.ToInt32(camNo)-1]);
                        //    m_TC1_ListSendBytes.Add(imageProcess.MatCompressImage(map.ToMat(),75));
                        //    m_TC1_ListSitchBitmap.Add(imageProcess.MatCompressImage(imageProcess.ResizeImage(map).ToMat(), 50));
                        //    ho_Image.Dispose();
                        //    map.Dispose();




                        //s3
                        //    Bitmap map = imageProcess.Rotation(ho_Image, SysConfig.m_Rotation[Convert.ToInt32(camNo)-1]);
                        //    m_BC1_ListSendBytes.Add(imageProcess.MatCompressImage(map.ToMat(),75));
                        //    m_BC1_ListSitchBitmap.Add(imageProcess.CompressImage(imageProcess.ResizeImage(ho_Image), 50));
                        //    ho_Image.Dispose();
                        //    map.Dispose();


                        break;

                    case 2:
                        Task.Run(() =>
                        {
                            Monitor.Enter(lockObj[Convert.ToInt32(camNo) - 1]);
                            Bitmap map2 = imageProcess.Rotation(ho_Image, SysConfig.m_Rotation[Convert.ToInt32(camNo) - 1]);
                            pictureBox2.Image = map2;
                            m_LCM1_ListSendBytes.Add(imageProcess.MatCompressImage(map2.ToMat(), 75));
                            m_LCM1_ListSitchBitmap.Add(imageProcess.MatCompressImage(imageProcess.MatResizeImage(map2), 50));
                            map2.Dispose();
                            ho_Image.Dispose();
                            Monitor.Exit(lockObj[Convert.ToInt32(camNo) - 1]);
                        });

                        //S2
                        //m_TC2_ListSendBytes.Add(imageProcess.MatCompressImage(map2.ToMat(), 75));
                        //m_TC2_ListSitchBitmap.Add(imageProcess.MatCompressImage(imageProcess.ResizeImage(map2).ToMat(), 50));

                        //S3
                        //m_BC2_ListSendBytes.Add(imageProcess.MatCompressImage(map2.ToMat(), 75));
                        //m_BC2_ListSitchBitmap.Add(imageProcess.MatCompressImage(imageProcess.ResizeImage(map2).ToMat(), 50));
                        break;

                    case 3:
                        Task.Run(() =>
                        {
                            Monitor.Enter(lockObj[Convert.ToInt32(camNo) - 1]);
                            Bitmap map3 = imageProcess.Rotation(ho_Image, SysConfig.m_Rotation[Convert.ToInt32(camNo) - 1]);
                            pictureBox3.Image = map3;
                            m_LCM2_ListSendBytes.Add(imageProcess.MatCompressImage(map3.ToMat(), 75));
                            m_LCM2_ListSitchBitmap.Add(imageProcess.MatCompressImage(imageProcess.MatResizeImage(map3), 50));
                            map3.Dispose();
                            ho_Image.Dispose();
                            Monitor.Exit(lockObj[Convert.ToInt32(camNo) - 1]);
                        });




                        //S2
                        //m_TC3_ListSendBytes.Add(imageProcess.MatCompressImage(map3.ToMat(), 75));
                        //m_TC3_ListSitchBitmap.Add(imageProcess.MatCompressImage(imageProcess.ResizeImage(map3).ToMat(), 50));


                        //S3
                        //m_BC3_ListSendBytes.Add(imageProcess.MatCompressImage(map3.ToMat(), 75));
                        //m_BC3_ListSitchBitmap.Add(imageProcess.MatCompressImage(imageProcess.ResizeImage(map3).ToMat(), 50));



                        break;

                    case 4:
                        Task.Run(() =>
                        {
                            Monitor.Enter(lockObj[Convert.ToInt32(camNo) - 1]);
                            Bitmap map4 = imageProcess.Rotation(ho_Image, SysConfig.m_Rotation[Convert.ToInt32(camNo) - 1]);
                            pictureBox4.Image = map4;
                            m_LCM3_ListSendBytes.Add(imageProcess.MatCompressImage(map4.ToMat(), 75));
                            m_LCM3_ListSitchBitmap.Add(imageProcess.MatCompressImage(imageProcess.MatResizeImage(map4), 50));
                            map4.Dispose();
                            ho_Image.Dispose();
                            Monitor.Exit(lockObj[Convert.ToInt32(camNo) - 1]);
                        });



                        //S2
                        //m_Corner1_ListSendBytes.Add(imageProcess.MatCompressImage(map4.ToMat(), 75));
                        //m_Corner1_ListSitchBitmap.Add(imageProcess.MatCompressImage(imageProcess.ResizeImage(map4).ToMat(), 50));

                        //S3
                        //m_DH1_ListSendBytes.Add(imageProcess.MatCompressImage(map4.ToMat(), 75));
                        //m_DH1_ListSitchBitmap.Add(imageProcess.MatCompressImage(imageProcess.ResizeImage(map4).ToMat(), 50));

                        break;

                    case 5:
                        Task.Run(() =>
                        {
                            Monitor.Enter(lockObj[Convert.ToInt32(camNo) - 1]);
                            Bitmap map5 = imageProcess.Rotation(ho_Image, SysConfig.m_Rotation[Convert.ToInt32(camNo) - 1]);

                            m_LCM3_ListSendBytes.Add(imageProcess.MatCompressImage(map5.ToMat(), 75));
                            m_LCM3_ListSitchBitmap.Add(imageProcess.MatCompressImage(imageProcess.MatResizeImage(map5), 50));
                            map5.Dispose();
                            ho_Image.Dispose();
                            Monitor.Exit(lockObj[Convert.ToInt32(camNo) - 1]);
                        });


                        //S2
                        //m_Corner2_ListSendBytes.Add(imageProcess.MatCompressImage(map5.ToMat(), 75));
                        //m_Corner2_ListSitchBitmap.Add(imageProcess.MatCompressImage(imageProcess.ResizeImage(map5).ToMat(), 50));

                        //S3
                        //m_DH2_ListSendBytes.Add(imageProcess.MatCompressImage(map5.ToMat(), 75));
                        //m_DH2_ListSitchBitmap.Add(imageProcess.MatCompressImage(imageProcess.ResizeImage(map5).ToMat(), 50));

                        break;
                    case 6:
                        Task.Run(() =>
                        {
                            Monitor.Enter(lockObj[Convert.ToInt32(camNo) - 1]);
                            Bitmap map6 = imageProcess.Rotation(ho_Image, SysConfig.m_Rotation[Convert.ToInt32(camNo) - 1]);
                            m_Side1_ListSendBytes.Add(imageProcess.MatCompressImage(map6.ToMat(), 75));
                            m_Side1_ListSitchBitmap.Add(imageProcess.MatCompressImage(imageProcess.MatResizeImage(map6), 50));
                            map6.Dispose();
                            ho_Image.Dispose();
                            Monitor.Exit(lockObj[Convert.ToInt32(camNo) - 1]);
                        });
                        //S2


                        //S3
                        //m_DH3_ListSendBytes.Add(imageProcess.MatCompressImage(map6.ToMat(), 75));
                        //m_DH3_ListSitchBitmap.Add(imageProcess.MatCompressImage(imageProcess.ResizeImage(map6).ToMat(), 50));

                        break;
                    case 7:
                        Task.Run(() =>
                        {
                            Monitor.Enter(lockObj[Convert.ToInt32(camNo) - 1]);
                            Bitmap map7 = imageProcess.Rotation(ho_Image, SysConfig.m_Rotation[Convert.ToInt32(camNo) - 1]);
                            m_Side2_ListSendBytes.Add(imageProcess.MatCompressImage(map7.ToMat(), 75));
                            m_Side2_ListSitchBitmap.Add(imageProcess.MatCompressImage(imageProcess.MatResizeImage(map7), 50));
                            map7.Dispose();
                            ho_Image.Dispose();
                            Monitor.Exit(lockObj[Convert.ToInt32(camNo) - 1]);
                        });


                        break;
                    case 8:
                        Task.Run(() =>
                        {
                            Monitor.Enter(lockObj[Convert.ToInt32(camNo) - 1]);
                            Bitmap map8 = imageProcess.Rotation(ho_Image, SysConfig.m_Rotation[Convert.ToInt32(camNo) - 1]);
                            m_Side3_ListSendBytes.Add(imageProcess.MatCompressImage(map8.ToMat(), 75));
                            m_Side3_ListSitchBitmap.Add(imageProcess.MatCompressImage(imageProcess.MatResizeImage(map8), 50));
                            map8.Dispose();
                            ho_Image.Dispose();
                            Monitor.Exit(lockObj[Convert.ToInt32(camNo) - 1]);
                        });
                        break;

                    case 9:
                        Task.Run(() =>
                        {
                            Monitor.Enter(lockObj[Convert.ToInt32(camNo) - 1]);
                            Bitmap map9 = imageProcess.Rotation(ho_Image, SysConfig.m_Rotation[Convert.ToInt32(camNo) - 1]);
                            m_Side4_ListSendBytes.Add(imageProcess.MatCompressImage(map9.ToMat(), 75));
                            m_Side4_ListSitchBitmap.Add(imageProcess.MatCompressImage(imageProcess.MatResizeImage(map9), 50));
                            map9.Dispose();
                            ho_Image.Dispose();
                            Monitor.Exit(lockObj[Convert.ToInt32(camNo) - 1]);
                        });

                        break;
                    case 10:
                        Task.Run(() =>
                        {
                            Monitor.Enter(lockObj[Convert.ToInt32(camNo) - 1]);
                            Bitmap map10 = imageProcess.Rotation(ho_Image, SysConfig.m_Rotation[Convert.ToInt32(camNo) - 1]);
                            m_Side5_ListSendBytes.Add(imageProcess.MatCompressImage(map10.ToMat(), 75));
                            m_Side5_ListSitchBitmap.Add(imageProcess.MatCompressImage(imageProcess.MatResizeImage(map10), 50));
                            map10.Dispose();
                            ho_Image.Dispose();
                            Monitor.Exit(lockObj[Convert.ToInt32(camNo) - 1]);
                        });
                        break;

                    case 11:
                        Task.Run(() =>
                        {
                            Monitor.Enter(lockObj[Convert.ToInt32(camNo) - 1]);
                            Bitmap map11 = imageProcess.Rotation(ho_Image, SysConfig.m_Rotation[Convert.ToInt32(camNo) - 1]);
                            m_Side6_ListSendBytes.Add(imageProcess.MatCompressImage(map11.ToMat(), 75));
                            m_Side6_ListSitchBitmap.Add(imageProcess.MatCompressImage(imageProcess.MatResizeImage(map11), 50));
                            map11.Dispose();
                            ho_Image.Dispose();
                            Monitor.Exit(lockObj[Convert.ToInt32(camNo) - 1]);
                        });

                        break;

                    default:
                        break;

                }
            }
            catch (Exception ex)
            {

                ErrLog.WriteLogEx(ex.ToString());
            }

        }

        #endregion

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //Init();
            //for (int i = 0; i < SysConfig.CamNum; i++)
            //{
            //    if (acqFIFOManager!= null && !string.IsNullOrEmpty(acqFIFOManager.UserNameS[i]))
            //    {
            //        comCurCameIndex.Items.Add(acqFIFOManager.UserNameS[i]);
            //    }
            //}
            Bitmap map = new Bitmap("C:\\DLL\\1.bmp");
            picDebug.Image = map;
        }



        private void numExposureTime_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (acqFIFOManager != null)
                {
                    acqFIFOManager.StopAcquire( (uint)comCurCameIndex.SelectedIndex);
                    Thread.Sleep(50);
                    acqFIFOManager.SetExposure((uint)comCurCameIndex.SelectedIndex, Convert.ToDouble(numExposureTime.Value));
                    Thread.Sleep(50);
                    acqFIFOManager.StartAcquire((uint)comCurCameIndex.SelectedIndex);
                }
            }
            catch (Exception ex)
            {

              
            }
        }

        /// <summary>
        /// 嘉利光源亮度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numJIALI_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > 0)
            {
                ControllerA.SetIntensity(numericUpDown1.Value.ToString(),numJIALI.Value.ToString());
            }
        }
        /// <summary>
        /// 锐视光源亮度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numRSEE_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown2.Value > 0)
            {
                RseeController_PM_D_8TE_BRTSetChannel(Com_Handle, 0, Convert.ToInt32(numericUpDown2.Value), Convert.ToInt32(numRSEE.Value));
                
            }
        }
        /// <summary>
        /// 设置相机软触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chSetSoftTrig_CheckedChanged(object sender, EventArgs e)
        {
            if (chSetSoftTrig.Checked)
            {
                if (acqFIFOManager != null)
                {
                    //设置软触发
                    acqFIFOManager.StopAcquire((uint)comCurCameIndex.SelectedIndex);
                    Thread.Sleep(50);
                    acqFIFOManager.TriggerSource((uint)comCurCameIndex.SelectedIndex, 7);
                    Thread.Sleep(50);
                    acqFIFOManager.StartAcquire((uint)comCurCameIndex.SelectedIndex);
                }
            }
            else
            {
                if (acqFIFOManager != null)
                {
                    //设置硬触发
                    acqFIFOManager.StopAcquire((uint)comCurCameIndex.SelectedIndex);
                    acqFIFOManager.TriggerSource((uint)comCurCameIndex.SelectedIndex, 0);
                    acqFIFOManager.StartAcquire((uint)comCurCameIndex.SelectedIndex);
                }
            }
        }
        /// <summary>
        /// 当前相机软触发  拍照一次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSoftTrig_Click(object sender, EventArgs e)
        {
            if (acqFIFOManager != null)
            {
                if (chSetSoftTrig.Checked)
                {
                    acqFIFOManager.SoftTriggerOnce((uint)comCurCameIndex.SelectedIndex);
                }
            }
        }
        /// <summary>
        /// 视频模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRealVedio_Click(object sender, EventArgs e)
        {
            if (acqFIFOManager != null)
            {
                if (!chSetSoftTrig.Checked)
                {
                    if (btnRealVedio.Text == "Vedio")
                    {
                        chSetSoftTrig.Enabled = comCurCameIndex.Enabled = btnSoftTrig.Enabled = false;
                        btnRealVedio.Text = "RealVedio";
                        acqFIFOManager.StopAcquire((uint)comCurCameIndex.SelectedIndex);
                        Thread.Sleep(100);
                        acqFIFOManager.SetTriggerModel((uint)comCurCameIndex.SelectedIndex, false);
                        Thread.Sleep(100);
                        acqFIFOManager.StartAcquire((uint)comCurCameIndex.SelectedIndex);
                    }
                    else
                    {
                        btnRealVedio.Text = "Vedio";
                        chSetSoftTrig.Enabled = comCurCameIndex.Enabled = btnSoftTrig.Enabled = true;
                        acqFIFOManager.StopAcquire((uint)comCurCameIndex.SelectedIndex);
                        Thread.Sleep(100);
                        acqFIFOManager.SetTriggerModel((uint)comCurCameIndex.SelectedIndex, true);
                        Thread.Sleep(100);
                        acqFIFOManager.StartAcquire((uint)comCurCameIndex.SelectedIndex);
                    }
                }
            }
        }

        private void chOffline_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chOffline.Checked)
                {
                    ErrLog.WriteLogData("选择离线拼图!");
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("拼图失败!");
                
            }
        }

        /// <summary>
        /// 执行拼图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSitchImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (chOffline.Checked)
                {
                    //离线拼图
                    if (btnRealVedio.Text == "RealVedio")
                    {
                        return;
                    }
                    if (string.IsNullOrEmpty(comModel.Text))
                    {
                        return;
                    }
                    List<byte[]> liDicImage = new List<byte[]>();
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "|*.bmp;*.jpg";
                    openFileDialog.Multiselect = true;
                    DialogResult dr = openFileDialog.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        string[] files = openFileDialog.FileNames;
                        if (files.Length > 0)
                        {
                            for (int i = 0; i < files.Length; i++)
                            {
                                FileStream fs = new FileStream(files[i], FileMode.Open);
                                Bitmap bitmap = (Bitmap)Bitmap.FromStream(fs);
                                liDicImage.Add(imageProcess.MatCompressImage(imageProcess.MatResizeImage(bitmap), 50));
                                fs.Flush();
                                fs.Close();
                            }
                        }
                    }
                    int w = Convert.ToInt32(comModel.Text.Split('X')[0]);
                    int h = Convert.ToInt32(comModel.Text.Split('X')[1]);


                    //Task<Bitmap> task22 = new Task<Bitmap>( ()=> imageProcess.MatContact(liDicImage, w, h));


                    Task<Bitmap> task = Task.Run(() => imageProcess.MatContact(liDicImage, w, h));
                    task.ContinueWith((o) =>
                    {
                        picDebug.Invoke(new MethodInvoker(delegate
                        {
                            if (o.Result != null && o.Result.GetType().Name == "Bitmap")
                            {
                                picDebug.Image = o.Result as Bitmap;
                                chOffline.Checked = false;
                               
                            }
                        }));
                    });
                }
                else
                {
                    //在线拼图
                    
                }
            }
            catch (Exception ex)
            {

                
            }
        }


        #region Roi绘制  计算平均 RGB

        private void picDebug_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsDraw)
            {
                if (picDebug.Image != null)
                {

                  
                    //Image image = picDebug.Image;
                    //Graphics g = picDebug.CreateGraphics();
                    //g.Clear(Color.Transparent);
                    //picDebug.Image = image;
                    //g.Dispose();
                  
                }
                startpoint = new System.Drawing.Point(e.X, e.Y);
                endpoint = new System.Drawing.Point(e.X, e.Y);
            }
        }

        private void picDebug_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void picDebug_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (IsDraw)
                {
                    endpoint.X = e.X;
                    endpoint.Y = e.Y;


                    OpenCvSharp.Point p1 = new OpenCvSharp.Point(startpoint.X, startpoint.Y);
                    OpenCvSharp.Point p2 = new OpenCvSharp.Point(endpoint.X, endpoint.Y);


                    Bitmap copy = new Bitmap(picDebug.Image);
                    
                    Graphics g = Graphics.FromImage(copy);
                
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;//消除锯齿



                    int x1 = copy.Width * startpoint.X / picDebug.Width;
                    int y1 = copy.Height * startpoint.Y / picDebug.Height;
                    int x2 = copy.Width * endpoint.X / picDebug.Width;
                    int y2 = copy.Height * endpoint.Y / picDebug.Height;

                    Rectangle rect = new Rectangle(x1, y1, x2 - x1, y2 - y1);
                    g.DrawRectangle(new Pen(Color.Red, 3), rect);

                    picDebug.Image = copy;




                    Mat mat = BitmapConverter.ToMat((Bitmap)picDebug.Image);
                    Rect rectRoi = new Rect(x1, y1, x2 - x1, y2 - y1);





                    Mat matRoi = new Mat(mat, rectRoi);

                    (int r, int gg, int b) = imageProcess.MatMeanImage(matRoi);
                    if (r != 999)
                    {
                        string s = string.Format("R:{0} G:{1} B:{2}", r, gg, b);
                        g.DrawString(s, new Font("宋体", 20), Brushes.Red, x1, y1 - 60);
                    }

                    g.Dispose();
                    mat.Dispose();
                    matRoi.Dispose();
                    GC.Collect();
                }
            }
            catch (Exception ex)
            {

                
            }
            
        }



        private void label11_Click(object sender, EventArgs e)
        {
            try
            {
                if (acqFIFOManager != null && btnRealVedio.Text == "Vedio")
                {
                    acqFIFOManager.StopAcquire((uint)comCurCameIndex.SelectedIndex);
                    acqFIFOManager[(uint)comCurCameIndex.SelectedIndex].SetROIXYWidthHeight(Convert.ToUInt32(tbow.Text), Convert.ToUInt32(tboh.Text), Convert.ToUInt32(tbW.Text), Convert.ToUInt32(tbH.Text));
                    SysConfig.INIJobConfig.IniWriteValue("Cam" + (comCurCameIndex.SelectedIndex+1).ToString(), "Offset", string.Format("{0},{1},{2},{3}", tbow.Text, tboh.Text, tbW.Text, tbH.Text));
                    acqFIFOManager.StartAcquire((uint)comCurCameIndex.SelectedIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置失败!");

            }
        }
        /// <summary>
        ///  设置相机ROI区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label10_Click(object sender, EventArgs e)
        {
            try
            {

                    if (acqFIFOManager != null && btnRealVedio.Text == "Vedio")
                    {
                        acqFIFOManager.StopAcquire((uint)comCurCameIndex.SelectedIndex );
                        acqFIFOManager[(uint)comCurCameIndex.SelectedIndex].SetROIXYWidthHeight(Convert.ToUInt32(tbow.Text), Convert.ToUInt32(tboh.Text), Convert.ToUInt32(tbW.Text), Convert.ToUInt32(tbH.Text));
                        SysConfig.INIJobConfig.IniWriteValue("Cam" + (comCurCameIndex.SelectedIndex + 1).ToString(), "Offset", string.Format("{0},{1},{2},{3}", tbow.Text, tboh.Text, tbW.Text, tbH.Text));
                        acqFIFOManager.StartAcquire((uint)comCurCameIndex.SelectedIndex);
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置失败!");
                
            }
        }

        private void comCurCameIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                tbH.Text = acqFIFOManager[(uint)comCurCameIndex.SelectedIndex].ImgHeight.ToString();
                tbW.Text = acqFIFOManager[(uint)comCurCameIndex.SelectedIndex].ImgWidth.ToString();
            }
            catch (Exception ex)
            {
                ErrLog.WriteLogEx(ex.ToString());
               
            }

        }

        private void chRGb_CheckedChanged(object sender, EventArgs e)
        {
            IsDraw = chRGb.Checked;
        }

        /// <summary>
        /// 图片是否需要旋转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comRot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comCurCameIndex.Text != null && comRot.Text != null)
            {
                SysConfig.m_Rotation[comCurCameIndex.SelectedIndex] = comRot.SelectedIndex;
                SysConfig.INIJobConfig.IniWriteValue("Cam" + (comCurCameIndex.SelectedIndex+1).ToString(), "Rotation", comRot.SelectedIndex.ToString());
            }
        }

        #endregion

        /// <summary>
        /// 白平衡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWhiteBalance_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnRealVedio.Text == "RealVedio")
                {
                    if (acqFIFOManager != null)
                    {
                        (uint r, uint g, uint b) = acqFIFOManager.AutoWhiteBalance((uint)comCurCameIndex.SelectedIndex);
                        acqFIFOManager.SetWhiteBalance((uint)comCurCameIndex.SelectedIndex, r, g, b);
                        SysConfig.INIJobConfig.IniWriteValue("Cam" + (comCurCameIndex.SelectedIndex+1).ToString(), "RGB", string.Format("{0},{1},{2}", r, g, b)); 
          
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("白平衡失败!");
            }
        }
    }

    public class ImageStruct
    {
        public byte[] byteImage = null;
        public string name = "";
        public ImageStruct()
        {

        }

        public ImageStruct(byte[] byteImage, string name)
        {
            this.byteImage = byteImage;
            this.name = name;
        }

    }
}
