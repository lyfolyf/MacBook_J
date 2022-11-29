using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using IHalconHikvision;


namespace MacBook
{
    //delegate void ErrMsgHandler(string msg);
    delegate void ImageReadyHandler(uint camNo, Bitmap ho_Image);

    public class AcqFIFOManager
    {
        //internal event ErrMsgHandler OnErrMsg = null;
        internal event ImageReadyHandler OnImageReady = null;
        /// <summary>
        /// 1号相机
        /// </summary>
        public ImageProvider AcqFifo1
        {
            get { return mAcqFifo1; }
        }
        private ImageProvider mAcqFifo1 = null;
        private bool mConnection1 = false;
        /// <summary>
        /// 一号相机链接状态
        /// </summary>
        public bool Connection1
        {
            get { return mConnection1; }
            set { mConnection1=value; }
        }




        /// <summary>
        /// 1号相机
        /// </summary>
        public ImageProvider AcqFifo2
        {
            get { return mAcqFifo2; }
        }
        private ImageProvider mAcqFifo2 = null;
        private bool mConnection2 = false;
        /// <summary>
        /// 一号相机链接状态
        /// </summary>
        public bool Connection2
        {
            get { return mConnection2; }
            set { mConnection2 = value; }
        }

   

        /// <summary>
        /// 1号相机
        /// </summary>
        public ImageProvider AcqFifo3
        {
            get { return mAcqFifo3; }
        }
        private ImageProvider mAcqFifo3 = null;
        private bool mConnection3 = false;
        /// <summary>
        /// 一号相机链接状态
        /// </summary>
        public bool Connection3
        {
            get { return mConnection3; }
            set { mConnection3 = value; }
        }


        /// <summary>
        /// 1号相机
        /// </summary>
        public ImageProvider AcqFifo4
        {
            get { return mAcqFifo4; }
        }
        private ImageProvider mAcqFifo4 = null;
        private bool mConnection4 = false;
        /// <summary>
        /// 一号相机链接状态
        /// </summary>
        public bool Connection4
        {
            get { return mConnection4; }
            set { mConnection4 = value; }
        }
    

        /// <summary>
        /// 1号相机
        /// </summary>
        public ImageProvider AcqFifo5
        {
            get { return mAcqFifo5; }
        }
        private ImageProvider mAcqFifo5 = null;
        private bool mConnection5 = false;
        /// <summary>
        /// 一号相机链接状态
        /// </summary>
        public bool Connection5
        {
            get { return mConnection5; }
            set { mConnection5 = value; }
        }
 
        public ImageProvider AcqFifo6
        {
            get { return mAcqFifo6; }
        }
        private ImageProvider mAcqFifo6 = null;
        private bool mConnection6 = false;
        /// <summary>
        /// 一号相机链接状态
        /// </summary>
        public bool Connection6
        {
            get { return mConnection6; }
            set { mConnection6 = value; }
        }


        public ImageProvider AcqFifo7
        {
            get { return mAcqFifo7; }
        }
        private ImageProvider mAcqFifo7 = null;
        private bool mConnection7 = false;
        /// <summary>
        /// 一号相机链接状态
        /// </summary>
        public bool Connection7
        {
            get { return mConnection7; }
            set { mConnection7 = value; }
        }

   
        public ImageProvider AcqFifo8
        {
            get { return mAcqFifo8; }
        }
        private ImageProvider mAcqFifo8 = null;
        private bool mConnection8 = false;
        /// <summary>
        /// 一号相机链接状态
        /// </summary>
        public bool Connection8
        {
            get { return mConnection8; }
            set { mConnection8 = value; }
        }
       
        public ImageProvider AcqFifo9
        {
            get { return mAcqFifo9; }
        }
        private ImageProvider mAcqFifo9 = null;
        private bool mConnection9 = false;
        /// <summary>
        /// 一号相机链接状态
        /// </summary>
        public bool Connection9
        {
            get { return mConnection9; }
            set { mConnection9 = value; }
        }
       

        public ImageProvider AcqFifo10
        {
            get { return mAcqFifo10; }
        }
        private ImageProvider mAcqFifo10 = null;
        private bool mConnection10 = false;
        /// <summary>
        /// 一号相机链接状态
        /// </summary>
        public bool Connection10
        {
            get { return mConnection10; }
            set { mConnection10 = value; }
        }

       
        public ImageProvider AcqFifo11
        {
            get { return mAcqFifo11; }
        }
        private ImageProvider mAcqFifo11 = null;
        private bool mConnection11 = false;
        /// <summary>
        /// 一号相机链接状态
        /// </summary>
        public bool Connection11
        {
            get { return mConnection11; }
            set { mConnection11 = value; }
        }
        /// <summary>
        /// 初始化必要数据
        /// </summary>

        public string[] UserNameS = new string[11];

        public void InitAcqFIFOManager()
        {
            try
            {
                List<DeviceEnumerator.Device> mDevice = DeviceEnumerator.EnumerateDevices();
                foreach (DeviceEnumerator.Device device in mDevice)
                {
                    if (device.SerialNumber == SysConfig.m_CamSerialNum[0])
                    {
                     
                        mAcqFifo1 = new ImageProvider();
                        if (!mAcqFifo1.IsOpen)
                        {
                            UserNameS[0] = device.UserDefinedName;
                            mAcqFifo1.Open(device.Index);
                            
                        }
                    }

                    if (device.SerialNumber == SysConfig.m_CamSerialNum[1])
                    {
                        mAcqFifo2 = new ImageProvider();
                        if (!mAcqFifo2.IsOpen)
                        {
                            UserNameS[1] = device.UserDefinedName;
                            mAcqFifo2.Open(device.Index);
                        }
                    }

                    if (device.SerialNumber == SysConfig.m_CamSerialNum[2])
                    {
                        mAcqFifo3 = new ImageProvider();
                        if (!mAcqFifo3.IsOpen)
                        {
                            UserNameS[2] = device.UserDefinedName;
                            mAcqFifo3.Open(device.Index);
                        }
                        
                    }

                    if (device.SerialNumber == SysConfig.m_CamSerialNum[3])
                    {
                        mAcqFifo4 = new ImageProvider();
                        if (!mAcqFifo4.IsOpen)
                        {
                            UserNameS[3] = device.UserDefinedName;
                            mAcqFifo4.Open(device.Index);
                        }
                    }

                    if (device.SerialNumber == SysConfig.m_CamSerialNum[4])
                    {
                        mAcqFifo5 = new ImageProvider();
                        if (!mAcqFifo5.IsOpen)
                        {
                            UserNameS[4] = device.UserDefinedName;
                            mAcqFifo5.Open(device.Index);
                        }
                    }

                    if (device.SerialNumber == SysConfig.m_CamSerialNum[5])
                    {
                        mAcqFifo6 = new ImageProvider();
                        if (!mAcqFifo6.IsOpen)
                        {
                            UserNameS[5] = device.UserDefinedName;
                            mAcqFifo6.Open(device.Index);
                        }
                    }

                    if (device.SerialNumber == SysConfig.m_CamSerialNum[6])
                    {
                        mAcqFifo7 = new ImageProvider();
                        if (!mAcqFifo7.IsOpen)
                        {
                            UserNameS[6] = device.UserDefinedName;
                            mAcqFifo7.Open(device.Index);
                        }
                    }

                    if (device.SerialNumber == SysConfig.m_CamSerialNum[7])
                    {
                        mAcqFifo8 = new ImageProvider();
                        if (!mAcqFifo8.IsOpen)
                        {
                            UserNameS[7] = device.UserDefinedName;
                            mAcqFifo8.Open(device.Index);
                        }
                    }

                    if (device.SerialNumber == SysConfig.m_CamSerialNum[8])
                    {
                        mAcqFifo9 = new ImageProvider();
                        if (!mAcqFifo9.IsOpen)
                        {
                            UserNameS[8] = device.UserDefinedName;
                            mAcqFifo9.Open(device.Index);
                        }
                    }

                    if (device.SerialNumber == SysConfig.m_CamSerialNum[9])
                    {
                        mAcqFifo10 = new ImageProvider();
                        if (!mAcqFifo10.IsOpen)
                        {
                            UserNameS[9] = device.UserDefinedName;
                            mAcqFifo10.Open(device.Index);
                        }
                    }

                    if (device.SerialNumber == SysConfig.m_CamSerialNum[10])
                    {
                        mAcqFifo11 = new ImageProvider();
                        if (!mAcqFifo11.IsOpen)
                        {
                            UserNameS[10] = device.UserDefinedName;
                            mAcqFifo11.Open(device.Index);
                        }
                    }
                }
                if (mAcqFifo1 != null)
                {
                    mAcqFifo1.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(mAcqFifo1_Complete);
                    mConnection1 = true;
                    mAcqFifo1.LineDebouncerTime(100);
                }
                else
                {
                   ErrLog.WriteLogEx("相机丢失!");
                }

                if (mAcqFifo2 != null)
                {
                    mAcqFifo2.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(mAcqFifo2_Complete);
                    mConnection2 = true;
                    mAcqFifo2.LineDebouncerTime(100);
                }
                else
                {
                   ErrLog.WriteLogEx("相机丢失!");
                }


                if (mAcqFifo3 != null)
                {
                    mAcqFifo3.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(mAcqFifo3_Complete);
                    mConnection3 = true;
                    mAcqFifo3.LineDebouncerTime(100);
                }
                else
                {
                   ErrLog.WriteLogEx("相机丢失!");
                }

                if (mAcqFifo4 != null)
                {
                    mAcqFifo4.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(mAcqFifo4_Complete);
                    mConnection4 = true;
                    mAcqFifo4.LineDebouncerTime(100);
                }
                else
                {
                    ErrLog.WriteLogEx("相机丢失!");
                }

                if (mAcqFifo5 != null)
                {
                    mAcqFifo5.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(mAcqFifo5_Complete);
                    mConnection5 = true;
                    mAcqFifo5.LineDebouncerTime(100);
                }
                else
                {
                    ErrLog.WriteLogEx("相机丢失!");
                }

                if (mAcqFifo6 != null)
                {
                    mAcqFifo6.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(mAcqFifo6_Complete);
                    mConnection6 = true;
                    mAcqFifo6.LineDebouncerTime(100);
                }
                else
                {
                    ErrLog.WriteLogEx("相机丢失!");
                }

                if (mAcqFifo7 != null)
                {
                    mAcqFifo7.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(mAcqFifo7_Complete);
                    mConnection7 = true;
                    mAcqFifo7.LineDebouncerTime(100);
                }
                else
                {
                    ErrLog.WriteLogEx("相机丢失!");
                }

                if (mAcqFifo8 != null)
                {
                    mAcqFifo8.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(mAcqFifo8_Complete);
                    mConnection8 = true;
                    mAcqFifo8.LineDebouncerTime(100);
                }
                else
                {
                    ErrLog.WriteLogEx("相机丢失!");
                }

                if (mAcqFifo9 != null)
                {
                    mAcqFifo9.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(mAcqFifo9_Complete);
                    mConnection9 = true;
                    mAcqFifo9.LineDebouncerTime(100);
                }
                else
                {
                    ErrLog.WriteLogEx("相机丢失!");
                }

                if (mAcqFifo10 != null)
                {
                    mAcqFifo10.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(mAcqFifo10_Complete);
                    mConnection10 = true;
                    mAcqFifo10.LineDebouncerTime(100);
                }
                else
                {
                    ErrLog.WriteLogEx("相机丢失!");
                }

                if (mAcqFifo11 != null)
                {
                    mAcqFifo11.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(mAcqFifo11_Complete);
                    mConnection11 = true;
                    mAcqFifo11.LineDebouncerTime(100);
                }
                else
                {
                    ErrLog.WriteLogEx("相机丢失!");
                }
            }
            catch (System.Exception ex)
            {
                ErrLog.WriteLogEx("相机通信故障!---"+ex.ToString());
            }
        }

        public ImageProvider this[uint Index]
        {
            get 
            {
                switch(Index)
                {
                    case 0:
                        return AcqFifo1;
                    case 1:
                        return AcqFifo2;
                    case 2:
                        return AcqFifo3;
                    case 3:
                        return AcqFifo4;
                    case 4:
                        return AcqFifo5;
                    case 5:
                        return AcqFifo6;
                    case 6:
                        return AcqFifo7;
                    case 7:
                        return AcqFifo8;
                    case 8:
                        return AcqFifo9;
                    case 9:
                        return AcqFifo10;
                    case 10:
                        return AcqFifo11;
                    default:
                        return null;
                     

                }

            }
        }


        #region Complete event

        void mAcqFifo1_Complete()
        {
            try
            {
                Bitmap bitmap = null;
                mAcqFifo1.GetCurrentImage (ref bitmap);
                if (bitmap != null)
                {
                    ImageReady(1, bitmap);
                }
            }
            catch (Exception ex)
            {
                ImageReady(1, null);
                //ErrLog.WriteLogEx("1号相机" + ex.Message);
            }
        }

        void mAcqFifo2_Complete()
        {
            try
            {
                Bitmap bitmap = null;
                mAcqFifo2.GetCurrentImage(ref bitmap);
                if (bitmap != null)
                {
                    ImageReady(2, bitmap);
                }
            }
            catch (Exception ex)
            {
                ImageReady(2, null);
               // ErrLog.WriteLogEx("2号相机" + ex.Message);
            }
        }

        void mAcqFifo3_Complete()
        {
            try
            {
                Bitmap bitmap = null;
                mAcqFifo3.GetCurrentImage(ref bitmap);
                if (bitmap != null)
                {
                    ImageReady(3, bitmap);
                }
            }
            catch (Exception ex)
            {
                ImageReady(3, null);
                ErrLog.WriteLogEx("3号相机" + ex.Message);
            }
        }

        void mAcqFifo4_Complete()
        {
            try
            {
                Bitmap bitmap = null;
                mAcqFifo4.GetCurrentImage(ref bitmap);
                if (bitmap != null)
                {
                    ImageReady(4, bitmap);
                }
            }
            catch (Exception ex)
            {
                ImageReady(4, null);
                ErrLog.WriteLogEx("4号相机" + ex.Message);
            }
        }

        void mAcqFifo5_Complete()
        {
            try
            {
                Bitmap bitmap = null;
                mAcqFifo5.GetCurrentImage(ref bitmap);
                if (bitmap != null)
                {
                    ImageReady(5, bitmap);
                }
            }
            catch (Exception ex)
            {
                ImageReady(5, null);
                ErrLog.WriteLogEx("5号相机" + ex.Message);
            }
        }

        void mAcqFifo6_Complete()
        {
            try
            {
                Bitmap bitmap = null;
                mAcqFifo6.GetCurrentImage(ref bitmap);
                if (bitmap != null)
                {
                    ImageReady(6, bitmap);
                }
            }
            catch (Exception ex)
            {
                ImageReady(6, null);
                ErrLog.WriteLogEx("6号相机" + ex.Message);
            }
        }

        void mAcqFifo7_Complete()
        {
            try
            {
                Bitmap bitmap = null;
                mAcqFifo7.GetCurrentImage(ref bitmap);
                if (bitmap != null)
                {
                    ImageReady(7, bitmap);
                }
            }
            catch (Exception ex)
            {
                ImageReady(7, null);
                ErrLog.WriteLogEx("7号相机" + ex.Message);
            }
        }

        void mAcqFifo8_Complete()
        {
            try
            {
                Bitmap bitmap = null;
                mAcqFifo8.GetCurrentImage(ref bitmap);
                if (bitmap != null)
                {
                    ImageReady(8, bitmap);
                }
            }
            catch (Exception ex)
            {
                ImageReady(8, null);
                ErrLog.WriteLogEx("8号相机" + ex.Message);
            }
        }


        void mAcqFifo9_Complete()
        {
            try
            {
                Bitmap bitmap = null;
                mAcqFifo9.GetCurrentImage(ref bitmap);
                if (bitmap != null)
                {
                    ImageReady(9, bitmap);
                }
            }
            catch (Exception ex)
            {
                ImageReady(9, null);
                ErrLog.WriteLogEx("9号相机" + ex.Message);
            }
        }


        void mAcqFifo10_Complete()
        {
            try
            {
                Bitmap bitmap = null;
                mAcqFifo10.GetCurrentImage(ref bitmap);
                if (bitmap != null)
                {
                    ImageReady(10, bitmap);
                }
            }
            catch (Exception ex)
            {
                ImageReady(10, null);
                ErrLog.WriteLogEx("10号相机" + ex.Message);
            }
        }

        void mAcqFifo11_Complete()
        {
            try
            {
                Bitmap bitmap = null;
                mAcqFifo11.GetCurrentImage(ref bitmap);
                if (bitmap != null)
                {
                    ImageReady(11, bitmap);
                }
            }
            catch (Exception ex)
            {
                ImageReady(11, null);
                ErrLog.WriteLogEx("11号相机" + ex.Message);
            }
        }

        #endregion
        /// <summary>
        /// 启动获取图像
        /// </summary>
        /// <param name="camNo">相机编号</param>
        public void StartAcquire(uint camNo)
        {
            this[camNo]?.Start();
            //switch (camNo)
            //{
            //    case 1:
            //        if (mAcqFifo1 != null)
            //        {
            //            mAcqFifo1.Start();
            //        }
            //        break;

            //    case 2:
            //        if (mAcqFifo2 != null)
            //        {
            //            mAcqFifo2.Start();
            //        }
            //        break;


            //    case 3:
            //        if (mAcqFifo3 != null)
            //        {
            //            mAcqFifo3.Start();

            //        }
            //        break;

            //    case 4:
            //        if (mAcqFifo4 != null)
            //        {
            //            mAcqFifo4.Start();
            //        }
            //        break;

            //    case 5:
            //        if (mAcqFifo5 != null)
            //        {
            //            mAcqFifo5.Start();
            //        }
            //        break;

            //    case 6:
            //        if (mAcqFifo6 != null)
            //        {
            //            mAcqFifo6.Start();
            //        }
            //        break;

            //    case 7:
            //        if (mAcqFifo7 != null)
            //        {
            //            mAcqFifo7.Start();
            //        }
            //        break;

            //    case 8:
            //        if (mAcqFifo8 != null)
            //        {
            //            mAcqFifo8.Start();
            //        }
            //        break;

            //    case 9:
            //        if (mAcqFifo9 != null)
            //        {
            //            mAcqFifo9.Start();
            //        }
            //        break;

            //    case 10:
            //        if (mAcqFifo10 != null)
            //        {
            //            mAcqFifo10.Start();
            //        }
            //        break;

            //    case 11:
            //        if (mAcqFifo11 != null)
            //        {
            //            mAcqFifo11.Start();
            //        }
            //        break;
            //    default: break;
            //}
        }
        /// <summary>
        /// 获取图像
        /// </summary>
        /// <param name="camNo">相机编号</param>
        public void GainAcquire(uint camNo)
        {
            switch (camNo)
            {
                case 1:
                    mAcqFifo1.Trigger(false);
                    mAcqFifo1.OneShot();
                    break;

                case 2:
                    mAcqFifo2.Trigger(false);
                    mAcqFifo2.OneShot();
                    break;

                case 3:
                    mAcqFifo3.Trigger(false);
                    mAcqFifo3.OneShot();
                    break;

                case 4:
                    mAcqFifo4.Trigger(false);
                    mAcqFifo4.OneShot();
                    break;

                case 5:
                    mAcqFifo5.Trigger(false);
                    mAcqFifo5.OneShot();
                    break;

                default: break;
            }
        }
        /// <summary>
        /// 停止获取图像
        /// </summary>
        /// <param name="camNo">相机编号</param>
        public void StopAcquire(uint camNo)
        {

            this[camNo]?.Stop();
            //switch (camNo)
            //{
            //    case 1:
            //        if (mAcqFifo1 != null)
            //        {
            //            mAcqFifo1.Stop();
            //        }
            //        break;

            //    case 2:
            //        if (mAcqFifo2 != null)
            //        {
            //            mAcqFifo2.Stop();
            //        }
            //        break;

            //    case 3:
            //        if (mAcqFifo3 != null)
            //        {
            //            mAcqFifo3.Stop();
            //        }
            //        break;
            //    case 4:
            //        if (mAcqFifo4 != null)
            //        {
            //            mAcqFifo4.Stop();
            //        }
            //        break;

            //    case 5:
            //        if (mAcqFifo5 != null)
            //        {
            //            mAcqFifo5.Stop();
            //        }
            //        break;

            //    case 6:
            //        if (mAcqFifo6 != null)
            //        {
            //            mAcqFifo6.Stop();
            //        }
            //        break;

            //    case 7:
            //        if (mAcqFifo7 != null)
            //        {
            //            mAcqFifo7.Stop();
            //        }
            //        break;

            //    case 8:
            //        if (mAcqFifo8 != null)
            //        {
            //            mAcqFifo8.Stop();
            //        }
            //        break;

            //    case 9:
            //        if (mAcqFifo9 != null)
            //        {
            //            mAcqFifo9.Stop();
            //        }
            //        break;

            //    case 10:
            //        if (mAcqFifo10 != null)
            //        {
            //            mAcqFifo10.Stop();
            //        }
            //        break;

            //    case 11:
            //        if (mAcqFifo11 != null)
            //        {
            //            mAcqFifo11.Stop();
            //        }
            //        break;

            //    default: break;
            //}
        }
        /// <summary>
        /// 设置出发模式
        /// </summary>
        /// <param name="camNo">相机编号</param>
        /// <param name="triggerModel">触发模式</param>
        public void SetTriggerModel(uint camNo, bool triggerModel)
        {
            this[camNo]?.Trigger(triggerModel);
            //switch (camNo)
            //{
            //    case 1:
            //        if (mAcqFifo1 != null)
            //            mAcqFifo1.Trigger(triggerModel);
            //        break;

            //    case 2:
            //        if (mAcqFifo2 != null)
            //            mAcqFifo2.Trigger(triggerModel);
            //        break;

            //    case 3:
            //        if (mAcqFifo3 != null)
            //            mAcqFifo3.Trigger(triggerModel);
            //        break;

            //    case 4:
            //        if (mAcqFifo4 != null)
            //            mAcqFifo4.Trigger(triggerModel);
            //        break;

            //    case 5:
            //        if (mAcqFifo5 != null)
            //            mAcqFifo5.Trigger(triggerModel);
            //        break;

            //    case 6:
            //        if (mAcqFifo6 != null)
            //            mAcqFifo6.Trigger(triggerModel);
            //        break;

            //    case 7:
            //        if (mAcqFifo7 != null)
            //            mAcqFifo7.Trigger(triggerModel);
            //        break;

            //    case 8:
            //        if (mAcqFifo8 != null)
            //            mAcqFifo8.Trigger(triggerModel);
            //        break;

            //    case 9:
            //        if (mAcqFifo5 != null)
            //            mAcqFifo5.Trigger(triggerModel);
            //        break;

            //    case 10:
            //        if (mAcqFifo9 != null)
            //            mAcqFifo9.Trigger(triggerModel);
            //        break;

            //    case 11:
            //        if (mAcqFifo11 != null)
            //            mAcqFifo11.Trigger(triggerModel);
            //        break;

            //    default: break;
            //}
        }
        /// <summary>
        /// 设置曝光时间mSecs
        /// </summary>
        /// <param name="camNo">相机编号</param>
        /// <param name="exposure">曝光时间mSecs</param>
        public void SetExposure(uint camNo, double exposure)
        {
            uint exp = Convert.ToUInt32(exposure);
            this[camNo]?.ExposureTime(exp); //mSecs
            //switch (camNo)
            //{
            //    case 1:
            //        if (mAcqFifo1 != null)
            //            mAcqFifo1.ExposureTime(exp); //mSecs
            //        break;

            //    case 2:
            //        if (mAcqFifo2 != null)
            //            mAcqFifo2.ExposureTime(exp); //mSecs
            //        break;

            //    case 3:
            //        if (mAcqFifo3 != null)
            //            mAcqFifo3.ExposureTime(exp); //mSecs
            //        break;

            //    case 4:
            //        if (mAcqFifo4 != null)
            //            mAcqFifo4.ExposureTime(exp); //mSecs
            //        break;

            //    case 5:
            //        if (mAcqFifo5 != null)
            //            mAcqFifo5.ExposureTime(exp); //mSecs
            //        break;

            //    case 6:
            //        if (mAcqFifo6 != null)
            //            mAcqFifo6.ExposureTime(exp); //mSecs
            //        break;

            //    case 7:
            //        if (mAcqFifo7 != null)
            //            mAcqFifo7.ExposureTime(exp); //mSecs
            //        break;

            //    case 8:
            //        if (mAcqFifo8 != null)
            //            mAcqFifo8.ExposureTime(exp); //mSecs
            //        break;

            //    case 9:
            //        if (mAcqFifo9 != null)
            //            mAcqFifo9.ExposureTime(exp); //mSecs
            //        break;

            //    case 10:
            //        if (mAcqFifo10 != null)
            //            mAcqFifo10.ExposureTime(exp); //mSecs
            //        break;

            //    case 11:
            //        if (mAcqFifo11 != null)
            //            mAcqFifo11.ExposureTime(exp); //mSecs
            //        break;

            //    default: break;
            //}
        }

        /// <summary>
        /// 设置触发通道
        /// </summary>
        /// <param name="camNo"></param>
        /// <param name="NUM"></param>
        public void TriggerSource(uint camNo,uint NUM)
        {

            this[camNo]?.TriggerSource(NUM);
            //switch (camNo)
            //{
            //    case 1:
            //        if (mAcqFifo1 != null)
            //            mAcqFifo1.TriggerSource(NUM);
            //        break;

            //    case 2:
            //        if (mAcqFifo2 != null)
            //            mAcqFifo2.TriggerSource(NUM);
            //        break;

            //    case 3:
            //        if (mAcqFifo3 != null)
            //            mAcqFifo3.TriggerSource(NUM);
            //        break;

            //    case 4:
            //        if (mAcqFifo4 != null)
            //            mAcqFifo4.TriggerSource(NUM);
            //        break;

            //    case 5:
            //        if (mAcqFifo5 != null)
            //            mAcqFifo5.TriggerSource(NUM);
            //        break;

            //    case 6:
            //        if (mAcqFifo6 != null)
            //            mAcqFifo6.TriggerSource(NUM);
            //        break;

            //    case 7:
            //        if (mAcqFifo7 != null)
            //            mAcqFifo7.TriggerSource(NUM);
            //        break;

            //    case 8:
            //        if (mAcqFifo8 != null)
            //            mAcqFifo8.TriggerSource(NUM);
            //        break;

            //    case 9:
            //        if (mAcqFifo9 != null)
            //            mAcqFifo9.TriggerSource(NUM);
            //        break;

            //    case 10:
            //        if (mAcqFifo10 != null)
            //            mAcqFifo10.TriggerSource(NUM);
            //        break;

            //    case 11:
            //        if (mAcqFifo11 != null)
            //            mAcqFifo11.TriggerSource(NUM);
            //        break;

            //    default: break;
            //}
        }

        /// <summary>
        /// 软触发拍照一次
        /// </summary>
        /// <param name="camNo"></param>
        /// <param name="NUM"></param>
        public void  SoftTriggerOnce(uint camNo)
        {

            this[camNo]?.OneShot();
            //switch (camNo)
            //{
            //    case 1:
            //        if (mAcqFifo1 != null)
            //            mAcqFifo1.OneShot();
            //        break;

            //    case 2:
            //        if (mAcqFifo2 != null)
            //            mAcqFifo2.OneShot();
            //        break;

            //    case 3:
            //        if (mAcqFifo3 != null)
            //            mAcqFifo3.OneShot();
            //        break;

            //    case 4:
            //        if (mAcqFifo4 != null)
            //            mAcqFifo4.OneShot();
            //        break;

            //    case 5:
            //        if (mAcqFifo5 != null)
            //            mAcqFifo5.OneShot();
            //        break;

            //    case 6:
            //        if (mAcqFifo6 != null)
            //            mAcqFifo6.OneShot();
            //        break;

            //    case 7:
            //        if (mAcqFifo7 != null)
            //            mAcqFifo7.OneShot();
            //        break;

            //    case 8:
            //        if (mAcqFifo8 != null)
            //            mAcqFifo8.OneShot();
            //        break;

            //    case 9:
            //        if (mAcqFifo9 != null)
            //            mAcqFifo9.OneShot();
            //        break;

            //    case 10:
            //        if (mAcqFifo10 != null)
            //            mAcqFifo10.OneShot();
            //        break;

            //    case 11:
            //        if (mAcqFifo11 != null)
            //            mAcqFifo11.OneShot();
            //        break;

            //    default: break;
            //}
        }

        /// <summary>
        /// 设置 相机曝光源  1 硬件脉宽曝光   0  时间曝光模式
        /// </summary>
        /// <param name="camNo"></param>
        /// <param name="mode"></param>
        public void SetExposureMode(uint camNo,uint mode)
        {
            this[camNo]?.ExposureSource(mode);
        }


        public (uint r,uint  g, uint  b) AutoWhiteBalance(uint camNo)
        {
            uint r =0, g=0, b=0;
            switch (camNo)
            {
                case 1:
                    if (mAcqFifo1 != null)
                        mAcqFifo1.CfgAutoWB(out r,out  g, out  b);
                    break;

                case 2:
                    if (mAcqFifo2 != null)
                        mAcqFifo2.CfgAutoWB(out r, out g, out b);
                    break;

                case 3:
                    if (mAcqFifo3 != null)
                        mAcqFifo3.CfgAutoWB(out r, out g, out b);
                    break;

                case 4:
                    if (mAcqFifo4 != null)
                        mAcqFifo4.CfgAutoWB(out r, out g, out b);
                    break;

                case 5:
                    if (mAcqFifo5 != null)
                        mAcqFifo5.CfgAutoWB(out r, out g, out b);
                    break;

                case 6:
                    if (mAcqFifo6 != null)
                        mAcqFifo6.CfgAutoWB(out r, out g, out b);
                    break;

                case 7:
                    if (mAcqFifo7 != null)
                        mAcqFifo7.CfgAutoWB(out r, out g, out b);
                    break;

                case 8:
                    if (mAcqFifo8 != null)
                        mAcqFifo8.CfgAutoWB(out r, out g, out b);
                    break;

                case 9:
                    if (mAcqFifo9 != null)
                        mAcqFifo9.CfgAutoWB(out r, out g, out b);
                    break;

                case 10:
                    if (mAcqFifo10 != null)
                        mAcqFifo10.CfgAutoWB(out r, out g, out b);
                    break;

                case 11:
                    if (mAcqFifo11 != null)
                        mAcqFifo11.CfgAutoWB(out r, out g, out b);
                    break;

                default: break;
            }

            return (r, g, b);
        }


        public void SetWhiteBalance(uint camNo, uint r, uint g, uint b)
        {

            this[camNo]?.CfgSetWB(r, g, b);

            //switch (camNo)
            //{
            //    case 1:
            //        if (mAcqFifo1 != null)
            //            mAcqFifo1.CfgSetWB( r,  g,  b);
            //        break;

            //    case 2:
            //        if (mAcqFifo2 != null)
            //            mAcqFifo2.CfgSetWB(r, g, b);
            //        break;

            //    case 3:
            //        if (mAcqFifo3 != null)
            //            mAcqFifo3.CfgSetWB(r, g, b);
            //        break;

            //    case 4:
            //        if (mAcqFifo4 != null)
            //            mAcqFifo4.CfgSetWB(r, g, b);
            //        break;

            //    case 5:
            //        if (mAcqFifo5 != null)
            //            mAcqFifo5.CfgSetWB(r, g, b);
            //        break;

            //    case 6:
            //        if (mAcqFifo6 != null)
            //            mAcqFifo6.CfgSetWB(r, g, b);
            //        break;

            //    case 7:
            //        if (mAcqFifo7 != null)
            //            mAcqFifo7.CfgSetWB(r, g, b);
            //        break;

            //    case 8:
            //        if (mAcqFifo8 != null)
            //            mAcqFifo8.CfgSetWB(r, g, b);
            //        break;

            //    case 9:
            //        if (mAcqFifo9 != null)
            //            mAcqFifo9.CfgSetWB(r, g, b);
            //        break;

            //    case 10:
            //        if (mAcqFifo10 != null)
            //            mAcqFifo10.CfgSetWB(r, g, b);
            //        break;

            //    case 11:
            //        if (mAcqFifo11 != null)
            //            mAcqFifo11.CfgSetWB(r, g, b);
            //        break;

            //    default: break;
            //}
        }


        public void SetGain(uint camNo, float gainvalue)
        {
         
               this[camNo]?.Gain(gainvalue);
            
            //switch (camNo)
            //{
            //    case 1:
            //        if (mAcqFifo1 != null)
            //            mAcqFifo1.Gain(gainvalue);
            //        break;

            //    case 2:
            //        if (mAcqFifo2 != null)
            //            mAcqFifo2.Gain(gainvalue);
            //        break;

            //    case 3:
            //        if (mAcqFifo3 != null)
            //            mAcqFifo3.Gain(gainvalue);
            //        break;

            //    case 4:
            //        if (mAcqFifo4 != null)
            //            mAcqFifo4.Gain(gainvalue);
            //        break;

            //    case 5:
            //        if (mAcqFifo5 != null)
            //            mAcqFifo5.Gain(gainvalue);
            //        break;

            //    case 6:
            //        if (mAcqFifo6 != null)
            //            mAcqFifo6.Gain(gainvalue);
            //        break;

            //    case 7:
            //        if (mAcqFifo7 != null)
            //            mAcqFifo7.Gain(gainvalue);
            //        break;

            //    case 8:
            //        if (mAcqFifo8 != null)
            //            mAcqFifo8.Gain(gainvalue);
            //        break;

            //    case 9:
            //        if (mAcqFifo9 != null)
            //            mAcqFifo9.Gain(gainvalue);
            //        break;

            //    case 10:
            //        if (mAcqFifo10 != null)
            //            mAcqFifo10.Gain(gainvalue);
            //        break;

            //    case 11:
            //        if (mAcqFifo11 != null)
            //            mAcqFifo11.Gain(gainvalue);
            //        break;

            //    default: break;
            //}
        }


        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="camNo"></param>
        /// <param name="ho_Image"></param>
        private void ImageReady(uint camNo, Bitmap ho_Image)
        {
                OnImageReady?.Invoke(camNo, ho_Image);
        }
        /// <summary>
        /// 释放资源，关闭相机
        /// </summary>
        public void AcqFIFOManagerFree()
        {

            for (uint i = 1; i < SysConfig.CamNum+1; i++)
            {
                this[i]?.Close();
            }
            //if (mAcqFifo1 != null)
            //    mAcqFifo1.Close();
            //if (mAcqFifo2 != null)
            //    mAcqFifo2.Close();
            //if (mAcqFifo3 != null)
            //    mAcqFifo3.Close();
            //if (mAcqFifo4 != null)
            //    mAcqFifo4.Close();
            //if (mAcqFifo5 != null)
            //    mAcqFifo5.Close();

            //if (mAcqFifo6 != null)
            //    mAcqFifo6.Close();
            //if (mAcqFifo7 != null)
            //    mAcqFifo7.Close();
            //if (mAcqFifo8 != null)
            //    mAcqFifo8.Close();
            //if (mAcqFifo9 != null)
            //    mAcqFifo9.Close();
            //if (mAcqFifo10 != null)
            //    mAcqFifo10.Close();

            //if (mAcqFifo11 != null)
            //    mAcqFifo11.Close();
        }
    }
}
