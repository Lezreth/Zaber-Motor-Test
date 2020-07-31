using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zaber.Motion;
using Zaber.Motion.Ascii;

namespace ZaberMotorTest
{
    public partial class FrmMain : Form
    {
        #region Properties

        /// <summary>
        /// Port control class for controlling Zaber motors.
        /// </summary>
        private ZaberPortControl PortControl { get; set; } = null;

        #endregion

        //?  ----------------------------------------------------------------------------------------------------

        #region Delegates

        /// <summary>
        /// Delegate for asynchronously writing an entry to the log.
        /// </summary>
        /// <param name="Entry">Entry to write.</param>
        private delegate void WriteToLogDelegate(string Entry);

        #endregion

        //?  ----------------------------------------------------------------------------------------------------

        #region Constructor, FormLoad, FormClosing

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Library.ToggleDeviceDbStore(true, "./ZaberDeviceDB");

            RefreshPortList();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClosePort();
        }

        #endregion

        //?  ----------------------------------------------------------------------------------------------------

        #region Methods

        /// <summary>
        /// Refresh the list of available serial ports
        /// </summary>
        private void RefreshPortList()
        {
            BtnConnect.Enabled = false;
            LstPorts.Enabled = false;

            LstPorts.Items.Clear();
            LstPorts.Items.AddRange(ZaberPortControl.GetSerialPorts());
            if (LstPorts.Items.Count > 0)
            {
                LstPorts.SelectedIndex = 0;
                LstPorts.Enabled = true;
                BtnConnect.Enabled = true;
            }
            else
            {
                LstPorts.Items.Add("None Found");
                LstPorts.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Open the selected port.
        /// </summary>
        private void OpenPort()
        {
            if (LstPorts.SelectedIndex < 0) { return; }

            //  Connect to the port
            BtnConnect.Text = Properties.Resources.Disconnect;
            LstPorts.Enabled = false;

            try
            { PortControl = new ZaberPortControl(LstPorts.SelectedItem.ToString(), true); }
            catch (Exception e)
            {
                BtnConnect.Text = Properties.Resources.Connect;
                LstPorts.Enabled = true;
                _ = MessageBox.Show("Could not connect to the specified port." + Environment.NewLine + Environment.NewLine + "Error Message:" + Environment.NewLine + e);
                return;
            }

            _ = LstLog.Items.Add("----------");
            _ = LstLog.Items.Add("Connected to " + LstPorts.SelectedItem);


            _ = LstLog.Items.Add("Found " + PortControl.MotorCount + " devices");

            NumMotorID.Maximum = PortControl.MotorCount - 1;
            NumAxisID.Maximum = PortControl.AxisCount(0);

            for (int i = 0; i < PortControl.MotorCount; i++)
            {
                _ = LstLog.Items.Add("Homing all axes for motor " + i + "...");
                PortControl.Home(i);
                LstLog.Items[LstLog.Items.Count - 1] += "Done";
            }

            NumMotorID.Value = 1 <= NumMotorID.Maximum ? 1 : NumMotorID.Maximum;
            NumAxisID.Value = 1 <= NumAxisID.Maximum ? 1 : NumAxisID.Maximum;
            GrpMoveMotor.Enabled = true;
        }

        /// <summary>
        /// Close the connected port.
        /// </summary>
        private void ClosePort()
        {
            if (PortControl == null) { return; }

            PortControl.Dispose();
            PortControl = null;

            BtnConnect.Text = Properties.Resources.Connect;
            LstPorts.Enabled = true;
            GrpMoveMotor.Enabled = false;

        }

        /// <summary>
        /// Asynchronous accessor for writing entries to the log.
        /// </summary>
        /// <param name="Entry">Entry to write to the log.</param>
        private void WriteToLogAsync(string Entry)
        {
            if (InvokeRequired)
            {
                _ = BeginInvoke(new WriteToLogDelegate(WriteToLogAsync), Entry);
                return;
            }

            _ = LstLog.Items.Add(Entry);
        }

        #endregion

        //?  ----------------------------------------------------------------------------------------------------

        #region UI Event Handlers

        private void BtnRefreshPortList_Click(object sender, EventArgs e)
        {
            ClosePort();
            RefreshPortList();
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (BtnConnect.Text == "Connect")
            {
                //  Connect to the selected port.
                OpenPort();
            }
            else
            {
                //  Disconnect from the port.
                ClosePort();
            }
        }

        private void BtnMoveMotor_Click(object sender, EventArgs e)
        {
            _ = LstLog.Items.Add("Moving motor " + NumMotorID.Value + ", axis " + NumAxisID.Value + " to the " + NumPosition.Value + "mm position...");

            MotorReturn MotorInfo = new MotorReturn();

            int MotorID = (int)NumMotorID.Value;
            int AxisID = (int)NumAxisID.Value;
            double Position = (double)NumPosition.Value;
            DateTime Now = DateTime.Now;

            switch ((sender as Button).Tag.ToString())
            {
                case "Absolute":
                    if (ChkAsync.Checked)
                    { MotorInfo = PortControl.MoveMotorAsync(MotorID, AxisID, Position, ZaberPortControl.MoveMethod.Absolute, Units.Length_Millimetres); }
                    else
                    { MotorInfo.MotionException = PortControl.MoveMotor(MotorID, AxisID, Position, ZaberPortControl.MoveMethod.Absolute, Units.Length_Millimetres); }
                    break;
                case "Relative":
                    if (ChkAsync.Checked)
                    { MotorInfo = PortControl.MoveMotorAsync(MotorID, AxisID, Position, ZaberPortControl.MoveMethod.Relative, Units.Length_Millimetres); }
                    else
                    { MotorInfo.MotionException = PortControl.MoveMotor(MotorID, AxisID, Position, ZaberPortControl.MoveMethod.Relative, Units.Length_Millimetres); }
                    break;
                default:
                    break;
            }

            if (MotorInfo.MotionException == null)
            { LstLog.Items[LstLog.Items.Count - 1] += "Done"; }
            else
            { _ = LstLog.Items.Add(MotorInfo.MotionException.ToString()); }

            if (MotorInfo.TaskAsync != null)
            {
                Task AsyncFinished = MotorInfo.TaskAsync.ContinueWith((i) =>
                {
                    TimeSpan Difference = DateTime.Now.Subtract(Now);
                    WriteToLogAsync("Asynchronous motor movement took " + DateTime.Now.Subtract(Now));
                }, TaskScheduler.Current);
            }
            else
            { _ = LstLog.Items.Add("Synchronous motor movement took " + DateTime.Now.Subtract(Now)); }

            _ = NumPosition.Focus();
        }

        private void NumMotorNumber_ValueChanged(object sender, EventArgs e)
        {
            NumAxisID.Maximum = PortControl.AxisCount((int)NumMotorID.Value);
        }

        private void BtnHomeAxis_Click(object sender, EventArgs e)
        {
            if (PortControl.Library == ZaberPortControl.LibraryType.ASCII)
            { _ = LstLog.Items.Add("Homing axis " + (int)NumAxisID.Value + " for motor " + (int)NumMotorID.Value + "..."); }
            else
            { _ = LstLog.Items.Add("Homing motor " + (int)NumMotorID.Value + "..."); }

            PortControl.Home((int)NumMotorID.Value, (int)NumAxisID.Value);
            LstLog.Items[LstLog.Items.Count - 1] += "Done";
        }

        private void BtnHomeAll_Click(object sender, EventArgs e)
        {
            if (PortControl.Library == ZaberPortControl.LibraryType.ASCII)
            {
                _ = LstLog.Items.Add("Homing all axes for motor " + (int)NumMotorID.Value + "...");
                PortControl.HomeAllAxes((int)NumMotorID.Value);
            }
            else
            { PortControl.Home((int)NumMotorID.Value, (int)NumAxisID.Value); }

            LstLog.Items[LstLog.Items.Count - 1] += "Done";
        }

        #endregion
    }
}
