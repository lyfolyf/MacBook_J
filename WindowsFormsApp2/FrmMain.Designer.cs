using System;
using System.Runtime.InteropServices;

namespace MacBook
{
    partial class FrmMain
    {

        [DllImport(@"RseeController.dll", EntryPoint = "RseeController_ConnectNet",
        CallingConvention = CallingConvention.Cdecl)]
        extern static uint RseeController_ConnectNet(string address, int port);

        [DllImport(@"RseeController.dll", EntryPoint = "RseeController_CloseNet",
               CallingConvention = CallingConvention.Cdecl)]
        extern static int RseeController_CloseNet(uint socket);

        [DllImport(@"RseeController.dll", EntryPoint = "RseeController_OpenCom",
               CallingConvention = CallingConvention.Cdecl)]
        extern static int RseeController_OpenCom(string com, int baud, bool overloop);

        [DllImport(@"RseeController.dll", EntryPoint = "RseeController_CloseCom",
               CallingConvention = CallingConvention.Cdecl)]
        extern static int RseeController_CloseCom(string com, int com_handle);

        [DllImport(@"RseeController.dll", EntryPoint = "RseeController_PM_D_8TE_BRTSetChannel",
               CallingConvention = CallingConvention.Cdecl)]
        extern static int RseeController_PM_D_8TE_BRTSetChannel(int com, uint socket, int channel, int range);

        [DllImport(@"RseeController.dll", EntryPoint = "RseeController_PM_D_8TE_PLSSetChannel",
               CallingConvention = CallingConvention.Cdecl)]
        extern static int RseeController_PM_D_8TE_PLSSetChannel(int com, uint socket, int channel, int time);

        [DllImport(@"RseeController.dll", EntryPoint = "RseeController_PM_D_8TE_BRTReadChannel",
               CallingConvention = CallingConvention.Cdecl)]
        extern static int RseeController_PM_D_8TE_BRTReadChannel(int com, uint socket, int channel);

        [DllImport(@"RseeController.dll", EntryPoint = "RseeController_PM_D_8TE_PLSReadChannel",
               CallingConvention = CallingConvention.Cdecl)]
        extern static int RseeController_PM_D_8TE_PLSReadChannel(int com, uint socket, int channel);

        [DllImport(@"RseeController.dll", EntryPoint = "RseeController_PM_D_8TE_SetIP",
               CallingConvention = CallingConvention.Cdecl)]
        extern static int RseeController_PM_D_8TE_SetIP(int com, uint socket, int add_1, int add_2, int add_3, int add_4, IntPtr arr);

        [DllImport(@"RseeController.dll", EntryPoint = "RseeController_PM_D_8TE_SetPort",
               CallingConvention = CallingConvention.Cdecl)]
        extern static int RseeController_PM_D_8TE_SetPort(int com, uint socket, int port, IntPtr arr);

        [DllImport(@"RseeController.dll", EntryPoint = "RseeController_PM_D_8TE_ChangeMode",
               CallingConvention = CallingConvention.Cdecl)]
        extern static int RseeController_PM_D_8TE_ChangeMode(int com, uint socket, int mode);

        [DllImport(@"RseeController.dll", EntryPoint = "RseeController_PM_D_8TE_ReadInfo",
               CallingConvention = CallingConvention.Cdecl)]
        extern static int RseeController_PM_D_8TE_ReadInfo(int com, uint socket, IntPtr arr);

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panelLeft = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.panelSub = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnTools = new System.Windows.Forms.Button();
            this.btnRunDebug = new System.Windows.Forms.Button();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.panelDebug = new System.Windows.Forms.Panel();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tbW = new System.Windows.Forms.TextBox();
            this.tbH = new System.Windows.Forms.TextBox();
            this.tbow = new System.Windows.Forms.TextBox();
            this.tboh = new System.Windows.Forms.TextBox();
            this.chRGb = new System.Windows.Forms.CheckBox();
            this.comRot = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numJIALI = new System.Windows.Forms.NumericUpDown();
            this.numRSEE = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnRealVedio = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comCurCameIndex = new System.Windows.Forms.ComboBox();
            this.numExposureTime = new System.Windows.Forms.NumericUpDown();
            this.chSetSoftTrig = new System.Windows.Forms.CheckBox();
            this.btnSoftTrig = new System.Windows.Forms.Button();
            this.btnWhiteBalance = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comModel = new System.Windows.Forms.ComboBox();
            this.comStyle = new System.Windows.Forms.ComboBox();
            this.chOffline = new System.Windows.Forms.CheckBox();
            this.btnSitchImage = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.panelLog = new System.Windows.Forms.Panel();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.panelImage = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.picDebug = new System.Windows.Forms.PictureBox();
            this.panelLeft.SuspendLayout();
            this.panelSub.SuspendLayout();
            this.panelDebug.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numJIALI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRSEE)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numExposureTime)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panelLog.SuspendLayout();
            this.panelImage.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDebug)).BeginInit();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(7)))), ((int)(((byte)(13)))));
            this.panelLeft.Controls.Add(this.btnExit);
            this.panelLeft.Controls.Add(this.panelSub);
            this.panelLeft.Controls.Add(this.btnRunDebug);
            this.panelLeft.Controls.Add(this.panelLogo);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(101, 814);
            this.panelLeft.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(17)))), ((int)(((byte)(23)))));
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnExit.Location = new System.Drawing.Point(0, 769);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(101, 45);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panelSub
            // 
            this.panelSub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(27)))), ((int)(((byte)(33)))));
            this.panelSub.Controls.Add(this.button4);
            this.panelSub.Controls.Add(this.button3);
            this.panelSub.Controls.Add(this.button2);
            this.panelSub.Controls.Add(this.button1);
            this.panelSub.Controls.Add(this.btnTools);
            this.panelSub.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSub.Location = new System.Drawing.Point(0, 155);
            this.panelSub.Name = "panelSub";
            this.panelSub.Size = new System.Drawing.Size(101, 265);
            this.panelSub.TabIndex = 2;
            // 
            // button4
            // 
            this.button4.Dock = System.Windows.Forms.DockStyle.Top;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button4.Location = new System.Drawing.Point(0, 180);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(101, 45);
            this.button4.TabIndex = 4;
            this.button4.Text = "Tool";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Top;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button3.Location = new System.Drawing.Point(0, 135);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(101, 45);
            this.button3.TabIndex = 3;
            this.button3.Text = "Tool";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Top;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button2.Location = new System.Drawing.Point(0, 90);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 45);
            this.button2.TabIndex = 2;
            this.button2.Text = "Tool";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Location = new System.Drawing.Point(0, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 45);
            this.button1.TabIndex = 1;
            this.button1.Text = "Tool";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnTools
            // 
            this.btnTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTools.FlatAppearance.BorderSize = 0;
            this.btnTools.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTools.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnTools.Location = new System.Drawing.Point(0, 0);
            this.btnTools.Name = "btnTools";
            this.btnTools.Size = new System.Drawing.Size(101, 45);
            this.btnTools.TabIndex = 0;
            this.btnTools.Text = "Tool";
            this.btnTools.UseVisualStyleBackColor = true;
            // 
            // btnRunDebug
            // 
            this.btnRunDebug.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(17)))), ((int)(((byte)(23)))));
            this.btnRunDebug.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRunDebug.FlatAppearance.BorderSize = 0;
            this.btnRunDebug.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRunDebug.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRunDebug.Location = new System.Drawing.Point(0, 110);
            this.btnRunDebug.Name = "btnRunDebug";
            this.btnRunDebug.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnRunDebug.Size = new System.Drawing.Size(101, 45);
            this.btnRunDebug.TabIndex = 1;
            this.btnRunDebug.Text = "Run";
            this.btnRunDebug.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRunDebug.UseVisualStyleBackColor = false;
            this.btnRunDebug.Click += new System.EventHandler(this.btnRunDebug_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(27)))), ((int)(((byte)(33)))));
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(101, 110);
            this.panelLogo.TabIndex = 0;
            // 
            // panelDebug
            // 
            this.panelDebug.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.panelDebug.Controls.Add(this.tabControl2);
            this.panelDebug.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelDebug.Location = new System.Drawing.Point(1143, 0);
            this.panelDebug.Name = "panelDebug";
            this.panelDebug.Size = new System.Drawing.Size(263, 814);
            this.panelDebug.TabIndex = 2;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl2.ItemSize = new System.Drawing.Size(72, 24);
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(263, 814);
            this.tabControl2.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl2.TabIndex = 0;
            this.tabControl2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl2_DrawItem);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(17)))), ((int)(((byte)(13)))));
            this.tabPage3.Controls.Add(this.groupBox4);
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 28);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(255, 782);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Cam";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tableLayoutPanel5);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox4.Location = new System.Drawing.Point(3, 368);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(249, 97);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Image";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 4;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label11, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label12, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.label13, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.tbW, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.tbH, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.tbow, 2, 1);
            this.tableLayoutPanel5.Controls.Add(this.tboh, 3, 1);
            this.tableLayoutPanel5.Controls.Add(this.chRGb, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.comRot, 1, 2);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(243, 77);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 25);
            this.label10.TabIndex = 0;
            this.label10.Text = "Width";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(63, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 25);
            this.label11.TabIndex = 0;
            this.label11.Text = "Height";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(123, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 25);
            this.label12.TabIndex = 0;
            this.label12.Text = "offerW";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(183, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 25);
            this.label13.TabIndex = 0;
            this.label13.Text = "offerH";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbW
            // 
            this.tbW.Location = new System.Drawing.Point(3, 28);
            this.tbW.Name = "tbW";
            this.tbW.Size = new System.Drawing.Size(49, 21);
            this.tbW.TabIndex = 1;
            // 
            // tbH
            // 
            this.tbH.Location = new System.Drawing.Point(63, 28);
            this.tbH.Name = "tbH";
            this.tbH.Size = new System.Drawing.Size(49, 21);
            this.tbH.TabIndex = 1;
            // 
            // tbow
            // 
            this.tbow.Location = new System.Drawing.Point(123, 28);
            this.tbow.Name = "tbow";
            this.tbow.Size = new System.Drawing.Size(49, 21);
            this.tbow.TabIndex = 1;
            this.tbow.Text = "0";
            // 
            // tboh
            // 
            this.tboh.Location = new System.Drawing.Point(183, 28);
            this.tboh.Name = "tboh";
            this.tboh.Size = new System.Drawing.Size(49, 21);
            this.tboh.TabIndex = 1;
            this.tboh.Text = "0";
            // 
            // chRGb
            // 
            this.chRGb.AutoSize = true;
            this.chRGb.Location = new System.Drawing.Point(3, 53);
            this.chRGb.Name = "chRGb";
            this.chRGb.Size = new System.Drawing.Size(42, 16);
            this.chRGb.TabIndex = 2;
            this.chRGb.Text = "RGB";
            this.chRGb.UseVisualStyleBackColor = true;
            this.chRGb.CheckedChanged += new System.EventHandler(this.chRGb_CheckedChanged);
            // 
            // comRot
            // 
            this.comRot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comRot.FormattingEnabled = true;
            this.comRot.Items.AddRange(new object[] {
            "0(None)",
            "1(R90+)",
            "2(R180+)",
            "3(R90-)",
            "4(FX)",
            "5(R90+FX)",
            "6(FY)",
            "7(R90-FY)"});
            this.comRot.Location = new System.Drawing.Point(63, 53);
            this.comRot.Name = "comRot";
            this.comRot.Size = new System.Drawing.Size(54, 20);
            this.comRot.TabIndex = 3;
            this.comRot.SelectedIndexChanged += new System.EventHandler(this.comRot_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox2.Location = new System.Drawing.Point(3, 217);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(249, 151);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Light";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label4, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label5, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.numericUpDown1, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.numericUpDown2, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.numJIALI, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.numRSEE, 2, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(243, 131);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 43);
            this.label3.TabIndex = 0;
            this.label3.Text = "Vendor";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(83, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 43);
            this.label4.TabIndex = 0;
            this.label4.Text = "Line";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(164, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 43);
            this.label5.TabIndex = 0;
            this.label5.Text = "Value";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(3, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 43);
            this.label6.TabIndex = 0;
            this.label6.Text = "JIALI";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(3, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 45);
            this.label7.TabIndex = 0;
            this.label7.Text = "RSEE";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown1.Font = new System.Drawing.Font("宋体", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numericUpDown1.Location = new System.Drawing.Point(83, 46);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(75, 38);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown2.Font = new System.Drawing.Font("宋体", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numericUpDown2.Location = new System.Drawing.Point(83, 89);
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(75, 38);
            this.numericUpDown2.TabIndex = 1;
            this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numJIALI
            // 
            this.numJIALI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numJIALI.Font = new System.Drawing.Font("宋体", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numJIALI.Location = new System.Drawing.Point(164, 46);
            this.numJIALI.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numJIALI.Name = "numJIALI";
            this.numJIALI.Size = new System.Drawing.Size(76, 38);
            this.numJIALI.TabIndex = 1;
            this.numJIALI.ValueChanged += new System.EventHandler(this.numJIALI_ValueChanged);
            // 
            // numRSEE
            // 
            this.numRSEE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numRSEE.Font = new System.Drawing.Font("宋体", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numRSEE.Location = new System.Drawing.Point(164, 89);
            this.numRSEE.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numRSEE.Name = "numRSEE";
            this.numRSEE.Size = new System.Drawing.Size(76, 38);
            this.numRSEE.TabIndex = 1;
            this.numRSEE.ValueChanged += new System.EventHandler(this.numRSEE_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(249, 214);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cam";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnRealVedio, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.comCurCameIndex, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.numExposureTime, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.chSetSoftTrig, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnSoftTrig, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnWhiteBalance, 1, 3);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(220, 191);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btnRealVedio
            // 
            this.btnRealVedio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRealVedio.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRealVedio.Location = new System.Drawing.Point(3, 144);
            this.btnRealVedio.Name = "btnRealVedio";
            this.btnRealVedio.Size = new System.Drawing.Size(104, 44);
            this.btnRealVedio.TabIndex = 5;
            this.btnRealVedio.Text = "Vedio";
            this.btnRealVedio.UseVisualStyleBackColor = true;
            this.btnRealVedio.Click += new System.EventHandler(this.btnRealVedio_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 47);
            this.label1.TabIndex = 0;
            this.label1.Text = "SerialNumber";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(3, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 47);
            this.label2.TabIndex = 0;
            this.label2.Text = "ExposureTime";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comCurCameIndex
            // 
            this.comCurCameIndex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comCurCameIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comCurCameIndex.Font = new System.Drawing.Font("宋体", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comCurCameIndex.FormattingEnabled = true;
            this.comCurCameIndex.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11"});
            this.comCurCameIndex.Location = new System.Drawing.Point(113, 3);
            this.comCurCameIndex.Name = "comCurCameIndex";
            this.comCurCameIndex.Size = new System.Drawing.Size(104, 35);
            this.comCurCameIndex.TabIndex = 1;
            this.comCurCameIndex.SelectedIndexChanged += new System.EventHandler(this.comCurCameIndex_SelectedIndexChanged);
            // 
            // numExposureTime
            // 
            this.numExposureTime.Font = new System.Drawing.Font("宋体", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numExposureTime.Location = new System.Drawing.Point(113, 50);
            this.numExposureTime.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numExposureTime.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numExposureTime.Name = "numExposureTime";
            this.numExposureTime.Size = new System.Drawing.Size(104, 38);
            this.numExposureTime.TabIndex = 2;
            this.numExposureTime.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numExposureTime.ValueChanged += new System.EventHandler(this.numExposureTime_ValueChanged);
            // 
            // chSetSoftTrig
            // 
            this.chSetSoftTrig.AutoSize = true;
            this.chSetSoftTrig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chSetSoftTrig.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chSetSoftTrig.Location = new System.Drawing.Point(3, 97);
            this.chSetSoftTrig.Name = "chSetSoftTrig";
            this.chSetSoftTrig.Size = new System.Drawing.Size(104, 41);
            this.chSetSoftTrig.TabIndex = 3;
            this.chSetSoftTrig.Text = "SoftTrig";
            this.chSetSoftTrig.UseVisualStyleBackColor = true;
            this.chSetSoftTrig.CheckedChanged += new System.EventHandler(this.chSetSoftTrig_CheckedChanged);
            // 
            // btnSoftTrig
            // 
            this.btnSoftTrig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSoftTrig.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSoftTrig.Location = new System.Drawing.Point(113, 97);
            this.btnSoftTrig.Name = "btnSoftTrig";
            this.btnSoftTrig.Size = new System.Drawing.Size(104, 41);
            this.btnSoftTrig.TabIndex = 4;
            this.btnSoftTrig.Text = "TRIG";
            this.btnSoftTrig.UseVisualStyleBackColor = true;
            this.btnSoftTrig.Click += new System.EventHandler(this.btnSoftTrig_Click);
            // 
            // btnWhiteBalance
            // 
            this.btnWhiteBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnWhiteBalance.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnWhiteBalance.Location = new System.Drawing.Point(113, 144);
            this.btnWhiteBalance.Name = "btnWhiteBalance";
            this.btnWhiteBalance.Size = new System.Drawing.Size(104, 44);
            this.btnWhiteBalance.TabIndex = 6;
            this.btnWhiteBalance.Text = "Balance";
            this.btnWhiteBalance.UseVisualStyleBackColor = true;
            this.btnWhiteBalance.Click += new System.EventHandler(this.btnWhiteBalance_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(17)))), ((int)(((byte)(13)))));
            this.tabPage4.Controls.Add(this.groupBox3);
            this.tabPage4.Location = new System.Drawing.Point(4, 28);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(255, 782);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "ImageProcess";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel4);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(249, 158);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "图片拼接";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel4.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label9, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.comModel, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.comStyle, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.chOffline, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.btnSitchImage, 1, 2);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(243, 138);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(139, 46);
            this.label8.TabIndex = 0;
            this.label8.Text = "拼图模式";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(3, 46);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(139, 46);
            this.label9.TabIndex = 0;
            this.label9.Text = "拼图方法";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comModel
            // 
            this.comModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comModel.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comModel.FormattingEnabled = true;
            this.comModel.Items.AddRange(new object[] {
            "2X2",
            "2X3",
            "3X3",
            "4X3",
            "3X4"});
            this.comModel.Location = new System.Drawing.Point(148, 3);
            this.comModel.Name = "comModel";
            this.comModel.Size = new System.Drawing.Size(92, 29);
            this.comModel.TabIndex = 1;
            // 
            // comStyle
            // 
            this.comStyle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comStyle.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comStyle.FormattingEnabled = true;
            this.comStyle.Items.AddRange(new object[] {
            "LCM",
            "Corner",
            "TC",
            "Side",
            "DH",
            "BC"});
            this.comStyle.Location = new System.Drawing.Point(148, 49);
            this.comStyle.Name = "comStyle";
            this.comStyle.Size = new System.Drawing.Size(92, 29);
            this.comStyle.TabIndex = 1;
            // 
            // chOffline
            // 
            this.chOffline.AutoSize = true;
            this.chOffline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chOffline.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chOffline.Location = new System.Drawing.Point(3, 95);
            this.chOffline.Name = "chOffline";
            this.chOffline.Size = new System.Drawing.Size(139, 40);
            this.chOffline.TabIndex = 2;
            this.chOffline.Text = "Offline";
            this.chOffline.UseVisualStyleBackColor = true;
            this.chOffline.CheckedChanged += new System.EventHandler(this.chOffline_CheckedChanged);
            // 
            // btnSitchImage
            // 
            this.btnSitchImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSitchImage.Location = new System.Drawing.Point(148, 95);
            this.btnSitchImage.Name = "btnSitchImage";
            this.btnSitchImage.Size = new System.Drawing.Size(92, 40);
            this.btnSitchImage.TabIndex = 3;
            this.btnSitchImage.Text = "Run";
            this.btnSitchImage.UseVisualStyleBackColor = true;
            this.btnSitchImage.Click += new System.EventHandler(this.btnSitchImage_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(17)))), ((int)(((byte)(13)))));
            this.tabPage5.Location = new System.Drawing.Point(4, 28);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(255, 782);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "Setting";
            // 
            // panelLog
            // 
            this.panelLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.panelLog.Controls.Add(this.rtbLog);
            this.panelLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelLog.Location = new System.Drawing.Point(101, 681);
            this.panelLog.Name = "panelLog";
            this.panelLog.Size = new System.Drawing.Size(1042, 133);
            this.panelLog.TabIndex = 4;
            // 
            // rtbLog
            // 
            this.rtbLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(17)))), ((int)(((byte)(13)))));
            this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLog.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rtbLog.Location = new System.Drawing.Point(0, 0);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(1042, 133);
            this.rtbLog.TabIndex = 0;
            this.rtbLog.Text = "";
            // 
            // panelImage
            // 
            this.panelImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(17)))), ((int)(((byte)(13)))));
            this.panelImage.Controls.Add(this.tabControl1);
            this.panelImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelImage.Location = new System.Drawing.Point(101, 0);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(1042, 681);
            this.panelImage.TabIndex = 5;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.ItemSize = new System.Drawing.Size(96, 24);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1042, 681);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(17)))), ((int)(((byte)(13)))));
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1034, 649);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Display";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox4, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1028, 643);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(506, 314);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Location = new System.Drawing.Point(517, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(507, 314);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox3.Location = new System.Drawing.Point(4, 325);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(506, 314);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox4.Location = new System.Drawing.Point(517, 325);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(507, 314);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 0;
            this.pictureBox4.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(17)))), ((int)(((byte)(13)))));
            this.tabPage2.Controls.Add(this.picDebug);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1034, 649);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Debug";
            // 
            // picDebug
            // 
            this.picDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picDebug.Location = new System.Drawing.Point(3, 3);
            this.picDebug.Name = "picDebug";
            this.picDebug.Size = new System.Drawing.Size(1028, 643);
            this.picDebug.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDebug.TabIndex = 0;
            this.picDebug.TabStop = false;
            this.picDebug.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picDebug_MouseDown);
            this.picDebug.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picDebug_MouseMove);
            this.picDebug.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picDebug_MouseUp);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(7)))), ((int)(((byte)(13)))));
            this.ClientSize = new System.Drawing.Size(1406, 814);
            this.Controls.Add(this.panelImage);
            this.Controls.Add(this.panelLog);
            this.Controls.Add(this.panelDebug);
            this.Controls.Add(this.panelLeft);
            this.Name = "FrmMain";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.panelLeft.ResumeLayout(false);
            this.panelSub.ResumeLayout(false);
            this.panelDebug.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numJIALI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRSEE)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numExposureTime)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.panelLog.ResumeLayout(false);
            this.panelImage.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picDebug)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panelSub;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnTools;
        private System.Windows.Forms.Button btnRunDebug;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Panel panelDebug;
        private System.Windows.Forms.Panel panelLog;
        private System.Windows.Forms.Panel panelImage;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox picDebug;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comCurCameIndex;
        private System.Windows.Forms.NumericUpDown numExposureTime;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numJIALI;
        private System.Windows.Forms.NumericUpDown numRSEE;
        private System.Windows.Forms.CheckBox chSetSoftTrig;
        private System.Windows.Forms.Button btnSoftTrig;
        private System.Windows.Forms.Button btnRealVedio;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comModel;
        private System.Windows.Forms.ComboBox comStyle;
        private System.Windows.Forms.CheckBox chOffline;
        private System.Windows.Forms.Button btnSitchImage;
        private System.Windows.Forms.Button btnWhiteBalance;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbW;
        private System.Windows.Forms.TextBox tbH;
        private System.Windows.Forms.TextBox tbow;
        private System.Windows.Forms.TextBox tboh;
        private System.Windows.Forms.CheckBox chRGb;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.ComboBox comRot;
    }
}

