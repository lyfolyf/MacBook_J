using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using PylonC.NET;
using IHalconHikvision;
using HalconDotNet;

namespace WFA
{
    //delegate void ErrMsgHandler(string msg);
    delegate void ImageReadyHandler(uint camNo, HObject ho_Image);

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
        /// <summary>
        /// 初始化必要数据
        /// </summary>
        public void InitAcqFIFOManager()
        {
            try
            {
                List<DeviceEnumerator.Device> mDevice = DeviceEnumerator.EnumerateDevices();
                foreach (DeviceEnumerator.Device device in mDevice)
                {
                    if (device.SerialNumber == SysConfig.mCam1SerNum)
                    {
                        mAcqFifo1 = new ImageProvider();
                        if (!mAcqFifo1.IsOpen)
                        {
                            mAcqFifo1.Open(device.Index);
                        }
                    }

                    if (device.SerialNumber == SysConfig.mCam2SerNum)
                    {
                        mAcqFifo2 = new ImageProvider();
                        if (!mAcqFifo2.IsOpen)
                        {
                            mAcqFifo2.Open(device.Index);
                        }
                    }

                    if (device.SerialNumber == SysConfig.mCam3SerNum)
                    {
                        mAcqFifo3 = new ImageProvider();
                        if (!mAcqFifo3.IsOpen)
                        {
                            mAcqFifo3.Open(device.Index);
                        }
                    }

                    if (device.SerialNumber == SysConfig.mCam4SerNum)
                    {
                        mAcqFifo4 = new ImageProvider();
                        if (!mAcqFifo4.IsOpen)
                        {
                            mAcqFifo4.Open(device.Index);
                        }
                    }

                    if (device.SerialNumber == SysConfig.mCam5SerNum)
                    {
                        mAcqFifo5 = new ImageProvider();
                        if (!mAcqFifo5.IsOpen)
                        {
                            mAcqFifo5.Open(device.Index);
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
            }
            catch (System.Exception ex)
            {
                ErrLog.WriteLogEx("相机通信故障!---"+ex.ToString());
            }
        }

        #region Complete event

        void mAcqFifo1_Complete()
        {
            try
            {
                HObject ho_Image;
                HOperatorSet.GenEmptyObj(out ho_Image);
                mAcqFifo1.GetCurrentImage (ref ho_Image);
                if (ho_Image != null)
                {
                    ImageReady(1, ho_Image);
                }
            }
            catch (Exception ex)
            {
                ImageReady(1, null);
                ErrLog.WriteLogEx("1号相机" + ex.Message);
            }
        }





        void mAcqFifo2_Complete()
        {
            try
            {
                HObject ho_Image;
                HOperatorSet.GenEmptyObj(out ho_Image);
                mAcqFifo2.GetCurrentImage(ref ho_Image);
                if (ho_Image != null)
                {
                    ImageReady(2, ho_Image);
                }
            }
            catch (Exception ex)
            {
                ImageReady(2, null);
                ErrLog.WriteLogEx("2号相机" + ex.Message);
            }
        }


        void mAcqFifo3_Complete()
        {
            try
            {
                HObject ho_Image;
                HOperatorSet.GenEmptyObj(out ho_Image);
                mAcqFifo3.GetCurrentImage(ref ho_Image);
                if (ho_Image != null)
                {
                    ImageReady(3, ho_Image);
                }
            }
            catch (Exception ex)
            {
                ImageReady(3, null);
                ErrLog.WriteLogEx("1号相机" + ex.Message);
            }
        }


        void mAcqFifo4_Complete()
        {
            try
            {
                HObject ho_Image;
                HOperatorSet.GenEmptyObj(out ho_Image);
                mAcqFifo4.GetCurrentImage(ref ho_Image);
                if (ho_Image != null)
                {
                    ImageReady(4, ho_Image);
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
                HObject ho_Image;
                HOperatorSet.GenEmptyObj(out ho_Image);
                mAcqFifo5.GetCurrentImage(ref ho_Image);
                if (ho_Image != null)
                {
                   ImageReady(5, ho_Image);
                }
            }
            catch (Exception ex)
            {
                ImageReady(5, null);
                ErrLog.WriteLogEx("5号相机" + ex.Message);
            }
        }
        #endregion
        /// <summary>
        /// 启动获取图像
        /// </summary>
        /// <param name="camNo">相机编号</param>
        public void StartAcquire(uint camNo)
        {
            switch (camNo)
            {
                case 1:
                    if (mAcqFifo1 != null)
                    {
                        mAcqFifo1.Start();
                    }
                    break;

                case 2:
                    if (mAcqFifo2 != null)
                    {
                        mAcqFifo2.Start();
                    }
                    break;


                case 3:
                    if (mAcqFifo3 != null)
                    {
                        mAcqFifo3.Start();
                    }
                    break;

                case 4:
                    if (mAcqFifo4 != null)
                    {
                        mAcqFifo4.Start();
                    }
                    break;

                case 5:
                    if (mAcqFifo5 != null)
                    {
                        mAcqFifo5.Start();
                    }
                    break;
                default: break;
            }
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
            switch (camNo)
            {
                case 1:
                    if (mAcqFifo1 != null)
                    {
                        mAcqFifo1.Stop();
                    }
                    break;

                case 2:
                    if (mAcqFifo2 != null)
                    {
                        mAcqFifo2.Stop();
                    }
                    break;

                case 3:
                    if (mAcqFifo3 != null)
                    {
                        mAcqFifo3.Stop();
                    }
                    break;
                case 4:
                    if (mAcqFifo4 != null)
                    {
                        mAcqFifo4.Stop();
                    }
                    break;

                case 5:
                    if (mAcqFifo5 != null)
                    {
                        mAcqFifo5.Stop();
                    }
                    break;
      
                default: break;
            }
        }
        /// <summary>
        /// 设置出发模式
        /// </summary>
        /// <param name="camNo">相机编号</param>
        /// <param name="triggerModel">触发模式</param>
        public void SetTriggerModel(uint camNo, bool triggerModel)
        {
            switch (camNo)
            {
                case 1:
                    if (mAcqFifo1 != null)
                        mAcqFifo1.Trigger(triggerModel);
                    break;

                case 2:
                    if (mAcqFifo2 != null)
                        mAcqFifo2.Trigger(triggerModel);
                    break;

                case 3:
                    if (mAcqFifo3 != null)
                        mAcqFifo3.Trigger(triggerModel);
                    break;

                case 4:
                    if (mAcqFifo4 != null)
                        mAcqFifo4.Trigger(triggerModel);
                    break;

                case 5:
                    if (mAcqFifo5 != null)
                        mAcqFifo5.Trigger(triggerModel);
                    break;

                default: break;
            }
        }
        /// <summary>
        /// 设置曝光时间mSecs
        /// </summary>
        /// <param name="camNo">相机编号</param>
        /// <param name="exposure">曝光时间mSecs</param>
        public void SetExposure(uint camNo, double exposure)
        {
            uint exp = Convert.ToUInt32(exposure);
            switch (camNo)
            {
                case 1:
                    if (mAcqFifo1 != null)
                        mAcqFifo1.ExposureTime(exp); //mSecs
                    break;

                case 2:
                    if (mAcqFifo2 != null)
                        mAcqFifo2.ExposureTime(exp); //mSecs
                    break;

                case 3:
                    if (mAcqFifo3 != null)
                        mAcqFifo3.ExposureTime(exp); //mSecs
                    break;

                case 4:
                    if (mAcqFifo4 != null)
                        mAcqFifo4.ExposureTime(exp); //mSecs
                    break;

                case 5:
                    if (mAcqFifo5 != null)
                        mAcqFifo5.ExposureTime(exp); //mSecs
                    break;

                default: break;
            }
        }

        public void SetGain(uint camNo, float gainvalue)
        {
            switch (camNo)
            {
                case 1:
                    if (mAcqFifo1 != null)
                        mAcqFifo1.Gain(gainvalue);
                    break;

                case 2:
                    if (mAcqFifo2 != null)
                        mAcqFifo2.Gain(gainvalue);
                    break;

                case 3:
                    if (mAcqFifo3 != null)
                        mAcqFifo3.Gain(gainvalue);
                    break;

                case 4:
                    if (mAcqFifo4 != null)
                        mAcqFifo4.Gain(gainvalue);
                    break;

                case 5:
                    if (mAcqFifo5 != null)
                        mAcqFifo5.Gain(gainvalue);
                    break;

                default: break;
            }
        }


        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="camNo"></param>
        /// <param name="ho_Image"></param>
        private void ImageReady(uint camNo, HObject ho_Image)
        {
            if (OnImageReady != null)
                OnImageReady(camNo, ho_Image);
        }
        /// <summary>
        /// 释放资源，关闭相机
        /// </summary>
        public void AcqFIFOManagerFree()
        {
            if (mAcqFifo1 != null)
                mAcqFifo1.Close();
            if (mAcqFifo2 != null)
                mAcqFifo2.Close();
            if (mAcqFifo3 != null)
                mAcqFifo3.Close();
            if (mAcqFifo4 != null)
                mAcqFifo4.Close();
            if (mAcqFifo5 != null)
                mAcqFifo5.Close();
        }
    }
}
