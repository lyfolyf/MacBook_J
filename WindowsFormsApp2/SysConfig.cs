#define side 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacBook
{
    class SysConfig
    {


        /// <summary>
        /// 文件配置
        /// </summary>
        public static INI INIConfig = null;
        public static INI INIJobConfig = null;
        /// <summary>
        /// 通信ip地址 与端口
        /// </summary>
        public static string mPort = ""; //主机IP
        public static string mHostIP = "";

        public const int CamNum = 4; //工站相机数量
        public static List<string> m_CamSerialNum = new List<string>();
        public static List<double> m_CamGain = new List<double>();
        public static List<string[]> m_CamOffset = new List<string[]>();
        public static List<string[]> m_CamRGB = new List<string[]>();
        public static List<int> m_Rotation = new List<int>();
        /// <summary>
        /// 相机序列号   曝光    增益
        /// </summary>


        public const int numLCMImage = 28; //LCM相机拍摄数量
        public const int numMandrelImage = 4; //LCM相机拍摄数量
        public const int numTCImage = 16; //LCM相机拍摄数量
        public const int numSideImage = 10; //LCM相机拍摄数量
        public const int numConrnerImage = 4; //LCM相机拍摄数量
        public const int numBCImage = 12; //LCM相机拍摄数量
        public const int numDHImage = 12; //LCM相机拍摄数量

        /// <summary>
        /// 摄像孔是否引用
        /// </summary>
        public static bool CamHoleUse = false;
        public static string comClass = ""; //光源型号

        public static string PortName = "COM1";
        public static int BaudRate = 19200;
        public static int DataBits = 8;


        public static bool ImageSave = false; //图片保存
        public static string ImageSavePath = ""; //图片保存

        public static bool IsDebug = false;
        public static string DefaultJob = "JOB1";
        public static void SysLoadConfig()
        {
            try
            {
                INIConfig = new INI(AppDomain.CurrentDomain.BaseDirectory + "Config\\Config.ini");


                mHostIP = INIConfig.IniReadValue("System", "HostIP");
                mPort = INIConfig.IniReadValue("System", "HPort");

                comClass = INIConfig.IniReadValue("System", "ComClass");

                ImageSave = Convert.ToBoolean(INIConfig.IniReadValue("System", "ImageSave"));

                ImageSavePath = INIConfig.IniReadValue("System", "ImageSavePath");


                DefaultJob = INIConfig.IniReadValue("System", "DefaultJob");

                PortName = INIConfig.IniReadValue("SerPort", "PortName");
                int.TryParse(INIConfig.IniReadValue("SerPort", "BaudRate"),out BaudRate);
                int.TryParse(INIConfig.IniReadValue("SerPort", "DataBits"), out DataBits);


                for (int i = 0; i < CamNum; i++)
                {
                   
                }
                LoadCamParam();

            }
            catch (Exception ex)
            {
                ErrLog.WriteLogEx(ex.ToString());
            }

        }

        public static void LoadCamParam()
        {

            INIJobConfig = new INI(AppDomain.CurrentDomain.BaseDirectory +"Job\\"+ SysConfig.DefaultJob + "\\JobConfig.ini");

            for (int i = 0; i < CamNum; i++)
            {
                m_CamSerialNum.Add(INIJobConfig.IniReadValue("Cam" + (i + 1).ToString(), "CamSerNum"));
                m_CamOffset.Add(INIJobConfig.IniReadValue("Cam"+(i+1).ToString(), "Offset").Split(','));
                m_CamRGB.Add(INIJobConfig.IniReadValue("Cam"+(i+1).ToString(), "RGB").Split(','));
                m_CamGain.Add(Convert.ToDouble(INIJobConfig.IniReadValue("Cam" + (i + 1), "Gain")));
                m_Rotation.Add(Convert.ToInt32(INIJobConfig.IniReadValue("Cam" + (i + 1), "Rotation")));
            }
        }
    }
}
