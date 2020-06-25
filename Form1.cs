using System;
using System.IO;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Net;
//using System.Web.Script.Serialization;// .Extensions;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Timers;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace NG_Monitor
{
    public partial class Form1 : Form
    {
        const int TYPE_NONE = 0;
        const int TYPE_HUB = 68;
        const int TYPE_SD = 73;
        const int TYPE_DER = 74;
        const int TYPE_FI3 = 83;

        const int CALICRATION_FI3 = 3950;

        enum Stage
        {
            STAGE0_NONE,
            STAGE1_TYPE,
            STAGE2_ID,
            STAGE4_CLBR,
        }
        static Stage m_stage;

        byte[] m_buffer = new byte[100];
        int length;
        static int nEventCntr;
        byte[] m_RomVer = new byte[4] { 0, 0, 0, 0 };
        string m_sFactory;
        //string m_sWorker;
        string m_sHW;
        string m_sType;
        string m_sTypeTitle;
        byte m_nType;
        int m_nMaxpIn;
        int m_localLat;
        int m_localLon;
        int m_nAllocID;
        double m_fConst;
        int m_selIndex;
        int m_nCalibVal;
        private static System.Timers.Timer gpsTimer;
        private static System.Timers.Timer ackTimer;
        bool m_bDefaultReused;
        private Color backgroundColor;
        int DbgMode = 0;
        //static int m_bInProcess;
        enum Versions { None, Old_Ver, New_Ver };
        string m_sSWVer;
        string m_sHubVer;

        struct SensorType
        {
            public byte m_nID;
            public string m_sShortName, m_sFullName;
            public int m_iMinValue, m_iMaxValue;
        }

        //sensor<-->monitor protocol
        //0xA1 / 0xA6
        unsafe public struct Stage1//_PayloadStage1
        {
            public byte m_Header;
            public byte m_size;
            public Int16 m_battery;
            public Int32    m_version;
            public UInt64   m_MacAddr;
            public byte     m_rssi;
        }

        unsafe public struct Stage2
        {
            public byte m_Header;
            public byte m_size;
            public Int16 m_data;
            public byte m_typeEcho;
        }

        unsafe public struct Stage3
        {
            public byte m_Header;
            public byte m_size;
            public byte m_id1;
            public byte m_id2;
            public byte m_id3;
            public byte m_id4;
            public byte m_type;
        }


        List<SensorType> TypesArray;

        public Form1()
        {
            InitializeComponent();
            
            try
            {
                string[] args = Environment.GetCommandLineArgs();
                if (args.Length > 1)
                {
                    if (args[1].Equals("DBG"))
                        DbgMode = 1;
                    if (args[1].Equals("misha"))
                        DbgMode = 2;
                    if (args[1].Equals("reused"))
                        DbgMode = 3;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show( e.Message);
            }
            
            nEventCntr = 0;
            m_stage = Stage.STAGE0_NONE;
            m_bDefaultReused = false;
            //string s;
//            m_ver = Versions.None;
            
            //AllTypeArr = new SensorType[10];
            TypesArray = new List<SensorType>();
            foreach (SensorType sen in TypesArray)
            {
                comboTypes.Items.Add(sen.m_sFullName);         
            }
            // The name of the key must include a valid root.
            const string userRoot = "HKEY_CURRENT_USER";
            const string subkey = "Software\\PhytechProduction";
            const string keyName = userRoot + "\\" + subkey;

            m_sFactory = (string)Registry.GetValue(keyName, "FactoryName", 0);    
            string s1 = (string)Registry.GetValue(keyName, "Reused", "0");
            if (s1 == "1")
            {
                m_bDefaultReused = true;
                checkReused.Checked = true;
            }
            else
                m_bDefaultReused = false;
            LoadVersionNum();
        
            /*
            //get SW latest version from staging server

            string uriStagingString = @"https://phytoweb-staging.herokuapp.com/activeadmin/sensor_versions/latest_version?user_id=1091&api_token=FrAnazu5rt67";
            string uriString = @"http://plantbeat.phytech.com/activeadmin/sensor_versions/latest_version?user_id=1091&api_token=FrAnazu5rt67";            
            try
            {
                WebClient client = new WebClient();
                // Optionally specify an encoding for uploading and downloading strings.
                client.Encoding = System.Text.Encoding.UTF8;
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                if (DbgMode == 1)
                    s = client.DownloadString(uriStagingString);
                else
                    s = client.DownloadString(uriString);//  .UploadValues(uriString, myNameValueCollection);
                m_sSWVer = s;
                txtOficialVer.Text = m_sSWVer;
            }
            catch (WebException we)
            {
                MessageBox.Show(we.Message);
            }
            */
        }

        //get SW latest version from staging server
        private void LoadVersionNum()
        {
            string uriProdString = @"http://plantbeat.phytech.com/activeadmin/hardware_versions/latest_version?hardware_type=SENSOR&api_token=FrAnazu5rt67";
            string uriStagingString = @"https://phytoweb-staging.herokuapp.com/activeadmin/hardware_versions/latest_version?hardware_type=SENSOR&api_token=FrAnazu5rt67";
            string uriProdHub = @"http://plantbeat.phytech.com/activeadmin/hardware_versions/latest_version?hardware_type=LOGGER&api_token=FrAnazu5rt67";//todo - change to HUB
            try
            {
                WebClient client = new WebClient();
                // Optionally specify an encoding for uploading and downloading strings.
                client.Encoding = System.Text.Encoding.UTF8;
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                if (DbgMode == 1)
                    m_sSWVer = client.DownloadString(uriStagingString);
                else
                    m_sSWVer = client.DownloadString(uriProdString);
                AddText(richTextBox1, "Official sensor Version: " + m_sSWVer);
                txtOficialVer.Text = m_sSWVer;
                m_sHubVer = client.DownloadString(uriProdHub);
                AddText(richTextBox1, "Official HUB Version: " + m_sHubVer);
            }
            catch (WebException we)
            {
                MessageBox.Show(we.Message);
            }
        }

        //private void SetTimer()
        //{
        //    // Create a timer with a two second interval.
        //    gpsTimer = new System.Timers.Timer(180000);
        //    // Hook up the Elapsed event for the timer. 
        //    gpsTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
        //    //ElapsedEventHandler(OnTimedEvent);// timerEndProc_Tick;//OnTimedEvent;
        //    gpsTimer.AutoReset = false;
        //    gpsTimer.Enabled = true;
        //}

        //private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        //{
        //    SetText(statusText, "FAIL");
        //    SetColor(statusText, Color.Red);
        //    AddText(richTextBox1, "Timeout to get GPS. Something is wrong ");
        //    gpsTimer.Enabled = false;
        //    m_stage = 0;
        //    gpsTimer.Dispose();
        //    //AddText(richTextBox1, "settimer ");
        //}

        private void SetTimer4Ack()
        {
            // Create a timer with a two second interval.
            ackTimer = new System.Timers.Timer(5000);
            // Hook up the Elapsed event for the timer. 
            ackTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedAckEvent);
            //ElapsedEventHandler(OnTimedEvent);// timerEndProc_Tick;//OnTimedEvent;
            ackTimer.AutoReset = false;
            ackTimer.Enabled = true;
        }

        private void OnTimedAckEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            SetText(statusText, "FAIL");
            SetColor(statusText, Color.Red);
            AddText(richTextBox1, "Timeout to get OK.");
            ackTimer.Enabled = false;
            m_stage = Stage.STAGE0_NONE;
            ackTimer.Dispose();
            //AddText(richTextBox1, "settimer ");
        }
        private void OpenPort_Click(object sender, EventArgs e)
        {
            if (comboPorts.Text == "")
            {
                MessageBox.Show("No selected Port");
                return;
            }
            if (OpenPortBtn.Text.CompareTo("Open Port") == 0)
            {
                serialPort1.PortName = comboPorts.Text;
                serialPort1.DtrEnable = true;
                //port.PortName = "COM3";
                //port.BaudRate = 19200;
                //port.Parity = Parity.None;
                //port.DataBits = 8;
                //port.StopBits = StopBits.One;
                try
                {
                    serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived_1);
                    nEventCntr++;
                    serialPort1.Open();
                    //pictureBox1.Image = Monitor.Properties.Resources.Lock_Unlock_icon;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Open PORT");
                }
                if (!serialPort1.IsOpen)
                    MessageBox.Show("Cant open PORT");
                else
                    OpenPortBtn.Text = "Close Port";
            }
            else
            {
                try
                {
                    serialPort1.Close();
                    serialPort1.DataReceived -= new SerialDataReceivedEventHandler(serialPort1_DataReceived_1);
                    nEventCntr--;
                    OpenPortBtn.Text = "Open Port";
                    //pictureBox1.Image = Monitor.Properties.Resources.Lock_Lock_icon;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Close PORT");
                }
            }
        }

        delegate void SetTextCallback(Control c, string text);

        private void SetText(Control c, string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            //if (this.textID.InvokeRequired)
            if (c.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { c, text });
            }
            else
            {
                c.Text = text;
            }
        }

        delegate void SetColorCallback(Control c, Color clr);

        private void SetColor(Control c, Color clr)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            //if (this.textID.InvokeRequired)
            if (c.InvokeRequired)
            {
                SetColorCallback d = new SetColorCallback(SetColor);
                this.Invoke(d, new object[] { c, clr });
            }
            else
            {
                c.BackColor = clr;
            }
        }
        delegate void AddTextCallback(RichTextBox c, string text);

        private void AddText(RichTextBox c, string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            //if (this.textID.InvokeRequired)
            try
            {
                if (c.InvokeRequired)
                {
                    AddTextCallback d = new AddTextCallback(AddText);
                    this.Invoke(d, new object[] { c, text });
                }
                else
                {
                    string s = DateTime.Now.ToString() + ": " + text + "\r\n";
                    c.AppendText(s/*text*/);
                    //c.Text += text;
                    c.SelectionStart = c.Text.Length;
                    c.ScrollToCaret();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        static Stage1 GetMsg1(byte[] data)
        {
            unsafe
            {
                fixed (byte* map = &data[0])
                {
                    return *(Stage1*)map;
                }
            }
            
        }

        static Stage2 GetMsg2(byte[] data)
        {
            unsafe
            {
                fixed (byte* map = &data[0])
                {
                    return *(Stage2*)map;
                }
            }
        }

        static Stage3 GetMsg3(byte[] data)
        {
            unsafe
            {
                fixed (byte* map = &data[0])
                {
                    return *(Stage3*)map;
                }
            }
        }


        private bool UnpackBuffer(int l)
        {
            //int n = 0;
            //int i = 0;//, size;
            int value, p;
            if (ackTimer != null)
            {
                ackTimer.Enabled = false;
                ackTimer.Dispose();
            }
            if (m_stage == Stage.STAGE0_NONE)
            {
                // new connection recognized:
                Stage1 msg1 =  GetMsg1(m_buffer);
                if ((msg1.m_Header != 0xA1) && (msg1.m_Header != 0xA6) && (msg1.m_Header != 0xBA))
                    return false;
//                AddText(c, string.Format("sizeof Stage1 is: {0}", sizeof(msg1)));
                AddText(richTextBox1, string.Format("batery: {0}, rssi: {1} Version: ", msg1.m_battery, (int)msg1.m_rssi));
                AddText(richTextBox1, string.Format("Mac Address: {0}", msg1.m_MacAddr));
                ResetAllTexts();
                if (msg1.m_Header == 0xBA)
                {
                    AddText(richTextBox1, "Check 4 updates ");
                    return false;
                }
                else
                    AddText(richTextBox1, "New Generation Unit Ask for ID ");
                value = msg1.m_battery;
                SetText(textBat, value.ToString());
                SetText(labelMac, string.Format("Mac Address: {0}", msg1.m_MacAddr.ToString()));
                
                //index += 2;
                if ((value < 3000))// && (DbgMode != 1))
                {
                    AddText(richTextBox1, string.Format("Battery too low. Quit process. ({0}) ", value));
                    //return false;
                }
                m_RomVer = BitConverter.GetBytes(msg1.m_version);
                ParseVersion(1);
                if (m_nType == TYPE_HUB)
                {
                    if (m_sHubVer != textVersion.Text)
                    {
                        AddText(richTextBox1, "Software version doesn't match required one. Quit process. ");
                        //return false;
                    }
                }
                else
                    if ((textVersion.Text != m_sSWVer) && (DbgMode != 3))
                    {
                        AddText(richTextBox1, "Software version doesn't match required one. Quit process. ");
                        //return false;
                    }
                    p = ((int)msg1.m_rssi - 260) / 2;
                //(m_buffer[10] - 260) / 2;
                SetText(textPin, p.ToString());
                if (p < m_nMaxpIn)
                {
                    AddText(richTextBox1, "Pin too low. Quit process. ");
                    //return false;
                }

                if (m_nType == TYPE_HUB)
                {
                    m_stage = Stage.STAGE2_ID;
                    return true;                   
                }
                else
                {
                    m_stage = Stage.STAGE1_TYPE;
                    AddText(richTextBox1, "sending to sensor the Type ");
                }
                //serialPort1.DataReceived -= new SerialDataReceivedEventHandler(serialPort1_DataReceived_1);
                //nEventCntr--;
            }
            else
                if (m_stage == Stage.STAGE1_TYPE)
            {
                Stage2 s2 = GetMsg2(m_buffer);
                if (s2.m_Header != 0xA3)
                {
                    AddText(richTextBox1, "Header doesnt fit"+s2.m_Header.ToString());
                    //m_bInProcess = 0;
                    //return false;
                }
                if (s2.m_typeEcho != m_nType)
                {
                    AddText(richTextBox1, "type doesnt fit");
                    //m_bInProcess = 0;
                    //return false;
                }
                value = s2.m_data;//BitConverter.ToInt16(m_buffer, 4);
                SetText(textValue, value.ToString());
                AddText(richTextBox1, "Got measure from sensor ");
                //AddText(richTextBox1, Buff2Log(true, length));
                if ((value < TypesArray[m_selIndex].m_iMinValue) || (value > TypesArray[m_selIndex].m_iMaxValue))
                {
                    AddText(richTextBox1, "Measurement is out of expected range. Quit process.  ");
 //                   m_stage = Stage.STAGE0_NONE;
                    return false;
                }
                // for FI#=3 - calc calibration and send it before ID

                //remove next 7 lines just for test
                if (m_nType == TYPE_FI3)
                {
                    m_nCalibVal = CALICRATION_FI3 - value;
                    AddText(richTextBox1, "Calibration Value is:" + m_nCalibVal.ToString() + " ");
                    m_stage = Stage.STAGE4_CLBR;
                }
                else
                {
                    m_stage = Stage.STAGE2_ID;
                    AddText(richTextBox1, "sending to sensor the new ID and Type ");
                }                
            }
            else
            if (m_stage == Stage.STAGE2_ID)
            {
                CheckACK();
            }
            else
                if (m_stage == Stage.STAGE4_CLBR)
                {
                AddText(richTextBox1, "Got calib from sensor ");
                    AddText(richTextBox1, Buff2Log(true, length));
                    if (GetCheckSum(6) != m_buffer[6])
                        return false;
                    //removenext 10 linesonly for fi check
                    value = BitConverter.ToInt16(m_buffer, 2);
                    if (value != m_nCalibVal)
                    {
                        AddText(richTextBox1, "Calibration value is wrong ");
//                        m_stage = Stage.STAGE0_NONE;
                        return false;
                    }
                    value = BitConverter.ToInt16(m_buffer, 4);
                    if (value != CALICRATION_FI3)
                    {
                        AddText(richTextBox1, "Final value is wrong ");
 //                       m_stage = Stage.STAGE0_NONE;
                        return false;
                    }
                    AddText(richTextBox1, "value after Calibration is:" + value + " ");
                    m_stage = Stage.STAGE2_ID;
                    AddText(richTextBox1, "sending to sensor the new ID and Type ");
                }

//            SendCommand();
//            SetTimer4Ack();
                    
            return true;              
        }

        bool CheckACK()
        {
            AddText(richTextBox1, "check ack ");
            AddText(richTextBox1, Buff2Log(true, 7));

            Stage3 st = GetMsg3(m_buffer);
            byte[] m_EchoId = new byte[] { st.m_id1, st.m_id2, st.m_id3, st.m_id4 };
            UInt32 id =  BitConverter.ToUInt32(m_EchoId, 0);
//            AddText(richTextBox1, string.Format("header: {0} id: {1}", st.m_Header, id));
            if (m_nType != TYPE_HUB)
            {
                if (st.m_Header != 0xA5)
                {
                    AddText(richTextBox1, "wrong header");
                    return false;
                }
            }
            else            
            {
                 if (st.m_Header != 0xA8)
                    return false;
            }
            // if got the sameID as sent
            if (id == Convert.ToInt32(textID.Text))
            {
                {
                    //AddText(richTextBox1, Buff2Log(true, 7));
                    AddText(richTextBox1, "Successfully set up identity ");
                    PrintSticker();
                    SendSensorInfo();
                    SetText(statusText, "PASS");
                    SetColor(statusText, Color.Green);
                    m_stage = Stage.STAGE0_NONE;
                    checkReused.Checked = m_bDefaultReused;
                    return true;
                }
            }
            else
                return false;
        }

        
        private void ClearReadBuf()
        {
            for (int i = 0; i < 20; i++)
                m_buffer[i] = 0;
        }

        
        private void SendCommand()
        {
            byte index = 0;
            if (serialPort1.IsOpen == false)
                return;
            if (m_nType == 0)
            {
                //MessageBox.Show("You must select type of sensor!");
                AddText(richTextBox1, "You must select type of sensor! ");
                return;
            }
            
            int bufSize = 0;
            //m_buffer[0] = 0xA2;
            if (m_stage == Stage.STAGE1_TYPE)
            {
                m_buffer[index++] = 0xA2;
                m_buffer[index++] = 6;
                Buffer.BlockCopy(BitConverter.GetBytes(Convert.ToInt16(textBat.Text)), 0, m_buffer, index, 2);
                index += 2;
                m_buffer[index++] = m_nType;
                m_buffer[index++] = 0;//for cs
                bufSize = index;

            }
            else
                 if (m_stage == Stage.STAGE2_ID)
            {
                AddText(richTextBox1, "Generate ID from server ");
                if (GenerateID() == false)
                {
                    AddText(richTextBox1, "Unable to generate ID. Quit process ");
                    m_stage = Stage.STAGE0_NONE;
                    return ;
                }
                if (m_nType == TYPE_HUB)
                {
                    m_buffer[index++] = 0xA7;
                    m_buffer[index++] = 9;
                    Buffer.BlockCopy(BitConverter.GetBytes(Convert.ToInt16(textBat.Text)), 0, m_buffer, index, 2);
                    index += 2; ;
                    Buffer.BlockCopy(BitConverter.GetBytes(Convert.ToInt32(textID.Text)), 0, m_buffer, index, 4);
                    index += 4;
                    //checksum- add in gate
                    m_buffer[index++] = 0;
                    bufSize = index;
//                    m_buffer[10] = GetCheckSum(10);
                    bufSize = 9;
                }
                else
                {
                    m_buffer[index++] = 0xA4;
                    m_buffer[index++] = 8;
                    Buffer.BlockCopy(BitConverter.GetBytes(Convert.ToInt32(textID.Text)), 0, m_buffer, index, 4);
                    index += 4;
                    m_buffer[index++] = m_nType;
                    //checksum- add in gate
                    m_buffer[index++] = 0;
                    bufSize = index;
                }
            }
            else
                 if (m_stage == Stage.STAGE4_CLBR)// calibrate the FI
            {
                Buffer.BlockCopy(BitConverter.GetBytes(m_nCalibVal), 0, m_buffer, 1, 2);
                Buffer.BlockCopy(BitConverter.GetBytes(Convert.ToInt16(textBat.Text)), 0, m_buffer, 3, 2);
                m_buffer[5] = m_nType;
                m_buffer[6] = GetCheckSum(6);
            }
            try
            {
                //Thread.Sleep(1000);
                AddText(richTextBox1, "now send");
                //serialPort1.DiscardInBuffer();
                serialPort1.Write(m_buffer, 0, bufSize);
                //serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived_1);
                //nEventCntr++;
                AddText(richTextBox1, Buff2Log(false, bufSize));
               serialPort1.DiscardOutBuffer();
               //clear buffer
               ClearReadBuf();
                SetTimer4Ack();
            }
           catch (Exception x)
           {
               MessageBox.Show("Exception data sending");
               MessageBox.Show(x.Message);
           }
        }

        private byte GetCheckSum(int len)
        {
            byte check_sum = 0;

            for (int i = 0; i < len; i++)
            {
                check_sum += m_buffer[i];// *buff++;                
            }
            return (check_sum);
            //return 1;
        }        

        private void ParseVersion(int index)
        {
            // save rom version
            //for (int i = 0; i < 4; i++)
            //    m_RomVer[i] = m_buffer[index + i];
            string sVer = new string((char)m_RomVer[0], 1);
            sVer += '.';
            sVer += Convert.ToString(m_RomVer[1]);
            sVer += '.';
            sVer += Convert.ToString(m_RomVer[2]);
            sVer += '.';
            sVer += Convert.ToString(m_RomVer[3]);
            SetText(textVersion, sVer);
        }

  /*      private byte[] StrtoBytes(string str, int len)
        {
            byte[] myBytes = new byte[len];
            int i, n = str.Length;
            if (len < n)
                n = len;
            for (i = 0; i < n; i++)
                myBytes[i] = Convert.ToByte(str[i]);
            if (n < len)
            {
                myBytes[n] = (byte)'#';
                for (i = n + 1; i < len; i++)
                    myBytes[i] = 0;
            }
            return myBytes;
        }*/

        private string Buff2Log(bool Rx, int len)
        {
            string s;
            if (len > 20)
                len = 20;
            //char[] tmp = new char[45];
            if (Rx)
            {
                     s = new string('>', 2);
            }
            else
                s = new string('<', 2);

            //if (Rx)
            //    for (int i = 0; i < len; i++)            
            //        s += Convert.ToChar(m_buffer[i]);
            //else
                for (int i = 0; i < len; i++)
                {                
                    s += Convert.ToString(m_buffer[i]);
                    s += ',';                
                }
            s += " ";
            return s;
        }

        private string Bytes2Str(int len)
        {
            char[] tmp = new char[32];

            for (int i = 0; i < len; i++)
                if (m_buffer[i + 8] != (byte)'#')
                    tmp[i] = Convert.ToChar(m_buffer[i + 8]);
                else
                    break;
            string s = new string(tmp);
            return s;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            labelMac.Text = "";
            string[] ports = SerialPort.GetPortNames();
            if (ports.GetLength(0) > 0)
            {
                for (int i = 0; i < ports.GetLength(0); i++)
                    comboPorts.Items.Insert(i, ports[i]);
                comboPorts.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Can't find any PORT to connect with");
            }
            
            backgroundColor = statusText.BackColor;

            m_nMaxpIn = 0;
            string s = System.IO.File.ReadAllText("pin.txt");
            string[] separators = { "\n", "\r" }; 
            string[] data = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            if (data.Length > 0)
            {
                m_nMaxpIn = Convert.ToInt16(data[0]);
                if (m_nMaxpIn > 0)
                    m_nMaxpIn *= -1;
                txtPinLmt.Text = "pIn Limit is: " + m_nMaxpIn.ToString();
                if (data.Length >= 2)
                {
                    m_localLat = Convert.ToInt16(data[1]);
                    m_localLon = Convert.ToInt16(data[2]);
                }
            }
            if (data.Length > 1)
                m_fConst = Convert.ToDouble(data[1]);
            else
                m_fConst = 0.0;
            //////////////////////////////////////////
            try
            {
                comboTypes.Items.Clear();
                StreamReader sr = File.OpenText("TypesDef.txt");
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] sarr = line.Split(',');
                    byte id;
                    byte.TryParse(sarr[0], out id);
                    TypesArray.Add(new SensorType { m_nID = id, m_sShortName = sarr[1], m_sFullName = sarr[2], m_iMinValue = Convert.ToInt32(sarr[3]), m_iMaxValue = Convert.ToInt32(sarr[4]) });
                    comboTypes.Items.Add(sarr[2]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
//            checkBoxNotGenerae.Visible = (DbgMode == 1);
        }

        private void ClrLogBtn_Click(object sender, EventArgs e)
        {
            richTextBox1.ResetText();
        }

        private void serialPort1_DataReceived_1(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(100);
            try
            {
                length = serialPort1.BytesToRead;
                //if (length > 25)
                //    return;
                //if (length < 7)
                //    return;
                if (length > 100)
                    length = 100;
                serialPort1.Read(m_buffer, 0, length);// length);
                serialPort1.DiscardInBuffer();
                if (length == 0)
                    return;
                string s = "DataReceived" + Buff2Log(false, length);
                //AddText(richTextBox1, "new data from sensor: ");
                AddText(richTextBox1, s);
                if (UnpackBuffer(length) == true)
                {
                    if (m_stage != Stage.STAGE0_NONE)
                    {
                        Thread th4 = new Thread(new ThreadStart(SendCommand));
                        th4.Start();
                    }
                }
                else
                    m_stage = Stage.STAGE0_NONE;
                ////if (Check4GPS() == false)
                //{
                //    if (m_bInProcess == 0)
                //        UnpackBuffer(length);
                //    else
                //        if (m_bInProcess == 1)
                //            UnpackBuffer(length);
                //        else
                //            if (m_bInProcess == 2)
                //                CheckACK();
                //            else
                //                //if (m_bInProcess == 3)
                //                //    Check4GPS();
                //                //else
                //                    if (m_bInProcess == 4)
                //                        UnpackBuffer(length);

                //}
                ClearReadBuf();
            }
            catch (Exception x)
            {
                //MessageBox.Show("Exception data recieved");
                MessageBox.Show(x.Message, "Exception data recieved");
            }
            //m_bGotAnswer = true;

        }

        private void IDGnrtrBtn_Click(object sender, EventArgs e)
        {
                GenerateID();
        }
                      
        private bool GenerateID()
        {
            int n = 1;
           //SetText(textID, "500555");
            //return;
            //            string uriString = "http://46.101.79.233:3001/activeadmin/sensors_allocations.json?user_id=1580&api_token=nz-nLBTpvQL4N-3GTZBz";
            //                   http://130.211.199.116:3000/activeadmin/sensors_allocations.json?user_id=1091&api_token=FrAnazu5rt67&sensors_allocation={wireless:1,allocations_number:1}
            string uriString = @"http://plantbeat.phytech.com/activeadmin/sensors_allocations.json?user_id=1091&api_token=FrAnazu5rt67";
            //string uriString = @"http://130.211.199.116:3000/activeadmin/sensors_allocations.json?user_id=1091&api_token=FrAnazu5rt67";
            try
            {
                WebClient client = new WebClient();
                // Optionally specify an encoding for uploading and downloading strings.
                client.Encoding = System.Text.Encoding.UTF8;
                client.Headers.Add("Content-Type","application/x-www-form-urlencoded");
                //uriString = urifirstPart + textID.Text.ToString() + urilastPart;
                // Create a new NameValueCollection instance to hold some  parameters to be posted to the URL.
                NameValueCollection myNameValueCollection = new NameValueCollection();

                // Add necessary parameter/value pairs to the name/value container.
                //                myNameValueCollection.Add("utf8", "%E2%9C%93");
              
                myNameValueCollection.Add("sensors_allocation[allocations_number]", Convert.ToString(n));   //m_numSen.ToString());
                myNameValueCollection.Add("sensors_allocation[wireless]", "1");
                myNameValueCollection.Add("commit", "Create Sensors allocation");

                // 'The Upload(String,NameValueCollection)' implicitly method sets HTTP POST as the request method.             
                byte[] responseArray = client.UploadValues(uriString, myNameValueCollection);
                string reply = Encoding.UTF8.GetString(responseArray, 0, responseArray.Length); 
                // Upload the data.
                //string reply = client.UploadString(uriString, data);
                string[] separators = { ",", "[", "]",":","{","}" }; //, "?", ";", ":", " " };
                string[] IDs = reply.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                
                int n1,n2;
                if (int.TryParse(IDs[3], out n1) && int.TryParse(IDs[1], out n2))
                {
                    SetText(textID, IDs[3]);
                    m_nAllocID = n2;
                    string smsg = string.Format("generate ID {0} ", n1);
                    AddText(richTextBox1, smsg);
                }
                else
                    return false;
//                textID.Text = IDs[0];
                return true;
                // Disply the server's response.
                //MessageBox.Show(responseArray.ToString());
                //.WriteLine(reply);
            }
            catch (WebException we)
            {
                MessageBox.Show(we.Message);
            }
           return false;
        }
       
        public void SendSensorInfo()
        {
            /*
            * "factory":"reuven"
            * "worker_identifier":"shluki"
            * "software_version":"r234"
            * "hardware_version":"shalom"
            * "sensor_type":23
            * "measuring_value":3.0
            * "battery_value":3.0
            * "rssi_value":3.0
            * "hardware_type":"Sensor"
            * */
            string s = "";
            if (checkReused.Checked == true)
                s = "REUSED SENSOR";
            string strAddress = String.Format(@"http://plantbeat.phytech.com/activeadmin/sensors_allocations/{0}.json?user_id=1091&api_token=FrAnazu5rt67", m_nAllocID);
            String strLine;// = "authtoken=4b8873e7a46d5cf77a027bda4feb3fe4&scope=crmapi&wfTrigger=true&xmlData=<Products><row no=\"1\">";
            strLine = String.Format("&factory={0}&worker_identifier={1}&software_version={2}&hardware_version={3}", m_sFactory, s, textVersion.Text, m_sHW);
            strLine += String.Format("&sensor_type={0}&measuring_value={1}&battery_value={2}&rssi_value={3}&hardware_type=Sensor", m_nType, textValue.Text, textBat.Text, textPin.Text);
            try
            {
                AddText(richTextBox1, "Sending sensor parameter to server... ");
                WebRequest request = WebRequest.Create(strAddress);
                request.Method = "PATCH";
                byte[] byteArray = Encoding.UTF8.GetBytes(strLine);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
                //return responseFromServer;
                if (responseFromServer.Contains("true") == true)
                    AddText(richTextBox1, "Sensor Information was sent successfully ");
                else
                    AddText(richTextBox1, "Failed to send Sensor Information ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }
        
        private void PrintSticker()
        {            
            AddText(richTextBox1, "Print...");
            new Print(textID.Text, m_sTypeTitle, m_nType, NG_Monitor.Properties.Resources.Aus_rcm/*, barcode qrCode.GetGraphic(1, Color.Black, Color.White, null, 0, 0), Monitor.Properties.Resources.Logo*/);
        }

        private void prntBtn_Click(object sender, EventArgs e)
         {
             PrintSticker();  
         }

        private void ResetAllTexts()
        {
            SetText(textBat, "");
            SetText(textVersion ,"");
            if (checkBoxNotGenerae.Checked != true)
                SetText(textID ,"");
            SetText(textPin, "");
            SetText(statusText ,"");
            SetText(textValue, "");
            statusText.BackColor = backgroundColor;
            m_stage = Stage.STAGE0_NONE;
         
//            m_ver = Versions.None;
            if (gpsTimer != null)
            {
                gpsTimer.Enabled = false;
                gpsTimer.Dispose();
            }
        }

         private void rstBtn_Click(object sender, EventArgs e)
         {
             try
             {
                 if (nEventCntr <= 0)
                 {
                     serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived_1);
                     nEventCntr++;
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message);
             }
             ResetAllTexts();
         }

         private void comboTypes_SelectedIndexChanged(object sender, EventArgs e)
         {
            //GetSelectedItem(comboTypes);
  //          string sPath;
             string s = comboTypes.SelectedItem.ToString();
            m_selIndex = comboTypes.SelectedIndex;
            comboHardware.SelectedIndex = 0;

             if (s == null)
             {
                 //MessageBox.Show("You must select type of sensor!");
                 return;
             }
             foreach (SensorType sen in TypesArray)
                if (s == sen.m_sFullName)
                {
                    m_sType = s;
                    m_nType = sen.m_nID;
                    m_sTypeTitle = sen.m_sShortName;
//                    sPath = m_sTypeTitle + ".jpg";
//                    pictureBox1.ImageLocation = sPath;
                    switch (m_nType)
                    {
                        case TYPE_SD:
                            pictureBox1.Image = NG_Monitor.Properties.Resources.SD;
                            break;
                        case TYPE_DER:
                            pictureBox1.Image = NG_Monitor.Properties.Resources.DER;
                            break;
                        case TYPE_FI3:
                            pictureBox1.Image = NG_Monitor.Properties.Resources.FI3;
                            break;
                        //default:
                        //    AddText(richTextBox1, "unknown type");

                    }
                    pictureBox1.Refresh();
                }

        }

        private void comboHardware_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_sHW = comboHardware.SelectedItem.ToString();
        }
    }
}

