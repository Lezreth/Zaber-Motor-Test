namespace ZaberMotorTest
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnRefreshPortList = new System.Windows.Forms.Button();
            this.LstPorts = new System.Windows.Forms.ComboBox();
            this.GrpComPort = new System.Windows.Forms.GroupBox();
            this.TableComPort = new System.Windows.Forms.TableLayoutPanel();
            this.BtnConnect = new System.Windows.Forms.Button();
            this.LstLog = new System.Windows.Forms.ListBox();
            this.NumPosition = new System.Windows.Forms.NumericUpDown();
            this.LblMoveToPosition = new System.Windows.Forms.Label();
            this.BtnMoveAbsolute = new System.Windows.Forms.Button();
            this.BtnMoveRelative = new System.Windows.Forms.Button();
            this.NumMotorID = new System.Windows.Forms.NumericUpDown();
            this.LblMoveMotorNumber = new System.Windows.Forms.Label();
            this.LblAxisNumber = new System.Windows.Forms.Label();
            this.NumAxisID = new System.Windows.Forms.NumericUpDown();
            this.TableMain = new System.Windows.Forms.TableLayoutPanel();
            this.GrpMoveMotor = new System.Windows.Forms.GroupBox();
            this.TableMoveMotor = new System.Windows.Forms.TableLayoutPanel();
            this.ChkAsync = new JCS.ToggleSwitch();
            this.BtnHomeAxis = new System.Windows.Forms.Button();
            this.BtnHomeAll = new System.Windows.Forms.Button();
            this.GrpComPort.SuspendLayout();
            this.TableComPort.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumMotorID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumAxisID)).BeginInit();
            this.TableMain.SuspendLayout();
            this.GrpMoveMotor.SuspendLayout();
            this.TableMoveMotor.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnRefreshPortList
            // 
            this.BtnRefreshPortList.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.BtnRefreshPortList.Location = new System.Drawing.Point(165, 3);
            this.BtnRefreshPortList.Name = "BtnRefreshPortList";
            this.BtnRefreshPortList.Size = new System.Drawing.Size(75, 23);
            this.BtnRefreshPortList.TabIndex = 0;
            this.BtnRefreshPortList.Text = "Refresh";
            this.BtnRefreshPortList.UseVisualStyleBackColor = true;
            this.BtnRefreshPortList.Click += new System.EventHandler(this.BtnRefreshPortList_Click);
            // 
            // LstPorts
            // 
            this.LstPorts.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LstPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LstPorts.FormattingEnabled = true;
            this.LstPorts.Location = new System.Drawing.Point(84, 4);
            this.LstPorts.Name = "LstPorts";
            this.LstPorts.Size = new System.Drawing.Size(75, 21);
            this.LstPorts.TabIndex = 1;
            // 
            // GrpComPort
            // 
            this.GrpComPort.AutoSize = true;
            this.GrpComPort.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GrpComPort.Controls.Add(this.TableComPort);
            this.GrpComPort.Location = new System.Drawing.Point(3, 3);
            this.GrpComPort.Name = "GrpComPort";
            this.GrpComPort.Size = new System.Drawing.Size(249, 48);
            this.GrpComPort.TabIndex = 2;
            this.GrpComPort.TabStop = false;
            this.GrpComPort.Text = "Connect To Port";
            // 
            // TableComPort
            // 
            this.TableComPort.AutoSize = true;
            this.TableComPort.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TableComPort.ColumnCount = 3;
            this.TableComPort.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TableComPort.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TableComPort.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TableComPort.Controls.Add(this.BtnRefreshPortList, 2, 0);
            this.TableComPort.Controls.Add(this.LstPorts, 1, 0);
            this.TableComPort.Controls.Add(this.BtnConnect, 0, 0);
            this.TableComPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableComPort.Location = new System.Drawing.Point(3, 16);
            this.TableComPort.Name = "TableComPort";
            this.TableComPort.RowCount = 1;
            this.TableComPort.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableComPort.Size = new System.Drawing.Size(243, 29);
            this.TableComPort.TabIndex = 0;
            // 
            // BtnConnect
            // 
            this.BtnConnect.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.BtnConnect.Enabled = false;
            this.BtnConnect.Location = new System.Drawing.Point(3, 3);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(75, 23);
            this.BtnConnect.TabIndex = 1;
            this.BtnConnect.Text = "Connect";
            this.BtnConnect.UseVisualStyleBackColor = true;
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // LstLog
            // 
            this.LstLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LstLog.FormattingEnabled = true;
            this.LstLog.Location = new System.Drawing.Point(258, 3);
            this.LstLog.Name = "LstLog";
            this.TableMain.SetRowSpan(this.LstLog, 2);
            this.LstLog.Size = new System.Drawing.Size(539, 433);
            this.LstLog.TabIndex = 3;
            // 
            // NumPosition
            // 
            this.NumPosition.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.NumPosition.DecimalPlaces = 2;
            this.NumPosition.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.NumPosition.Location = new System.Drawing.Point(84, 55);
            this.NumPosition.Name = "NumPosition";
            this.NumPosition.Size = new System.Drawing.Size(75, 20);
            this.NumPosition.TabIndex = 4;
            // 
            // LblMoveToPosition
            // 
            this.LblMoveToPosition.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.LblMoveToPosition.AutoSize = true;
            this.LblMoveToPosition.Location = new System.Drawing.Point(28, 58);
            this.LblMoveToPosition.Name = "LblMoveToPosition";
            this.LblMoveToPosition.Size = new System.Drawing.Size(50, 13);
            this.LblMoveToPosition.TabIndex = 5;
            this.LblMoveToPosition.Text = "Move To";
            // 
            // BtnMoveAbsolute
            // 
            this.BtnMoveAbsolute.Location = new System.Drawing.Point(3, 81);
            this.BtnMoveAbsolute.Name = "BtnMoveAbsolute";
            this.BtnMoveAbsolute.Size = new System.Drawing.Size(75, 23);
            this.BtnMoveAbsolute.TabIndex = 6;
            this.BtnMoveAbsolute.Tag = "Absolute";
            this.BtnMoveAbsolute.Text = "Absolute";
            this.BtnMoveAbsolute.UseVisualStyleBackColor = true;
            this.BtnMoveAbsolute.Click += new System.EventHandler(this.BtnMoveMotor_Click);
            // 
            // BtnMoveRelative
            // 
            this.BtnMoveRelative.Location = new System.Drawing.Point(84, 81);
            this.BtnMoveRelative.Name = "BtnMoveRelative";
            this.BtnMoveRelative.Size = new System.Drawing.Size(75, 23);
            this.BtnMoveRelative.TabIndex = 7;
            this.BtnMoveRelative.Tag = "Relative";
            this.BtnMoveRelative.Text = "Relative";
            this.BtnMoveRelative.UseVisualStyleBackColor = true;
            this.BtnMoveRelative.Click += new System.EventHandler(this.BtnMoveMotor_Click);
            // 
            // NumMotorID
            // 
            this.NumMotorID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.NumMotorID.Location = new System.Drawing.Point(84, 3);
            this.NumMotorID.Name = "NumMotorID";
            this.NumMotorID.Size = new System.Drawing.Size(75, 20);
            this.NumMotorID.TabIndex = 8;
            this.NumMotorID.ValueChanged += new System.EventHandler(this.NumMotorNumber_ValueChanged);
            // 
            // LblMoveMotorNumber
            // 
            this.LblMoveMotorNumber.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.LblMoveMotorNumber.AutoSize = true;
            this.LblMoveMotorNumber.Location = new System.Drawing.Point(4, 6);
            this.LblMoveMotorNumber.Name = "LblMoveMotorNumber";
            this.LblMoveMotorNumber.Size = new System.Drawing.Size(74, 13);
            this.LblMoveMotorNumber.TabIndex = 9;
            this.LblMoveMotorNumber.Text = "Motor Number";
            // 
            // LblAxisNumber
            // 
            this.LblAxisNumber.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.LblAxisNumber.AutoSize = true;
            this.LblAxisNumber.Location = new System.Drawing.Point(12, 32);
            this.LblAxisNumber.Name = "LblAxisNumber";
            this.LblAxisNumber.Size = new System.Drawing.Size(66, 13);
            this.LblAxisNumber.TabIndex = 10;
            this.LblAxisNumber.Text = "Axis Number";
            // 
            // NumAxisID
            // 
            this.NumAxisID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.NumAxisID.Location = new System.Drawing.Point(84, 29);
            this.NumAxisID.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumAxisID.Name = "NumAxisID";
            this.NumAxisID.Size = new System.Drawing.Size(75, 20);
            this.NumAxisID.TabIndex = 11;
            this.NumAxisID.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // TableMain
            // 
            this.TableMain.ColumnCount = 2;
            this.TableMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TableMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableMain.Controls.Add(this.LstLog, 1, 0);
            this.TableMain.Controls.Add(this.GrpComPort, 0, 0);
            this.TableMain.Controls.Add(this.GrpMoveMotor, 0, 1);
            this.TableMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableMain.Location = new System.Drawing.Point(0, 0);
            this.TableMain.Name = "TableMain";
            this.TableMain.RowCount = 2;
            this.TableMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableMain.Size = new System.Drawing.Size(800, 439);
            this.TableMain.TabIndex = 12;
            // 
            // GrpMoveMotor
            // 
            this.GrpMoveMotor.AutoSize = true;
            this.GrpMoveMotor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GrpMoveMotor.Controls.Add(this.TableMoveMotor);
            this.GrpMoveMotor.Enabled = false;
            this.GrpMoveMotor.Location = new System.Drawing.Point(3, 57);
            this.GrpMoveMotor.Name = "GrpMoveMotor";
            this.GrpMoveMotor.Size = new System.Drawing.Size(168, 181);
            this.GrpMoveMotor.TabIndex = 12;
            this.GrpMoveMotor.TabStop = false;
            this.GrpMoveMotor.Text = "Move Motor";
            // 
            // TableMoveMotor
            // 
            this.TableMoveMotor.AutoSize = true;
            this.TableMoveMotor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TableMoveMotor.ColumnCount = 2;
            this.TableMoveMotor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TableMoveMotor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TableMoveMotor.Controls.Add(this.LblMoveMotorNumber, 0, 0);
            this.TableMoveMotor.Controls.Add(this.BtnMoveRelative, 1, 3);
            this.TableMoveMotor.Controls.Add(this.NumMotorID, 1, 0);
            this.TableMoveMotor.Controls.Add(this.LblAxisNumber, 0, 1);
            this.TableMoveMotor.Controls.Add(this.BtnMoveAbsolute, 0, 3);
            this.TableMoveMotor.Controls.Add(this.NumAxisID, 1, 1);
            this.TableMoveMotor.Controls.Add(this.NumPosition, 1, 2);
            this.TableMoveMotor.Controls.Add(this.LblMoveToPosition, 0, 2);
            this.TableMoveMotor.Controls.Add(this.ChkAsync, 0, 4);
            this.TableMoveMotor.Controls.Add(this.BtnHomeAxis, 0, 5);
            this.TableMoveMotor.Controls.Add(this.BtnHomeAll, 1, 5);
            this.TableMoveMotor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableMoveMotor.Location = new System.Drawing.Point(3, 16);
            this.TableMoveMotor.Name = "TableMoveMotor";
            this.TableMoveMotor.RowCount = 6;
            this.TableMoveMotor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableMoveMotor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableMoveMotor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableMoveMotor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableMoveMotor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableMoveMotor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableMoveMotor.Size = new System.Drawing.Size(162, 162);
            this.TableMoveMotor.TabIndex = 0;
            // 
            // ChkAsync
            // 
            this.ChkAsync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TableMoveMotor.SetColumnSpan(this.ChkAsync, 2);
            this.ChkAsync.Location = new System.Drawing.Point(3, 110);
            this.ChkAsync.Name = "ChkAsync";
            this.ChkAsync.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAsync.OffText = "Synchronous";
            this.ChkAsync.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAsync.OnText = "Asynchronous";
            this.ChkAsync.Size = new System.Drawing.Size(156, 19);
            this.ChkAsync.Style = JCS.ToggleSwitch.ToggleSwitchStyle.BrushedMetal;
            this.ChkAsync.TabIndex = 12;
            // 
            // BtnHomeAxis
            // 
            this.BtnHomeAxis.Location = new System.Drawing.Point(3, 135);
            this.BtnHomeAxis.Name = "BtnHomeAxis";
            this.BtnHomeAxis.Size = new System.Drawing.Size(75, 23);
            this.BtnHomeAxis.TabIndex = 13;
            this.BtnHomeAxis.Text = "Home Axis";
            this.BtnHomeAxis.UseVisualStyleBackColor = true;
            this.BtnHomeAxis.Click += new System.EventHandler(this.BtnHomeAxis_Click);
            // 
            // BtnHomeAll
            // 
            this.BtnHomeAll.Location = new System.Drawing.Point(84, 135);
            this.BtnHomeAll.Name = "BtnHomeAll";
            this.BtnHomeAll.Size = new System.Drawing.Size(75, 23);
            this.BtnHomeAll.TabIndex = 14;
            this.BtnHomeAll.Text = "Home All";
            this.BtnHomeAll.UseVisualStyleBackColor = true;
            this.BtnHomeAll.Click += new System.EventHandler(this.BtnHomeAll_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 439);
            this.Controls.Add(this.TableMain);
            this.Name = "FrmMain";
            this.Text = "Zaber Motor Test";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.GrpComPort.ResumeLayout(false);
            this.GrpComPort.PerformLayout();
            this.TableComPort.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NumPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumMotorID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumAxisID)).EndInit();
            this.TableMain.ResumeLayout(false);
            this.TableMain.PerformLayout();
            this.GrpMoveMotor.ResumeLayout(false);
            this.GrpMoveMotor.PerformLayout();
            this.TableMoveMotor.ResumeLayout(false);
            this.TableMoveMotor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnRefreshPortList;
        private System.Windows.Forms.ComboBox LstPorts;
        private System.Windows.Forms.GroupBox GrpComPort;
        private System.Windows.Forms.TableLayoutPanel TableComPort;
        private System.Windows.Forms.Button BtnConnect;
        private System.Windows.Forms.ListBox LstLog;
        private System.Windows.Forms.NumericUpDown NumPosition;
        private System.Windows.Forms.Label LblMoveToPosition;
        private System.Windows.Forms.Button BtnMoveAbsolute;
        private System.Windows.Forms.Button BtnMoveRelative;
        private System.Windows.Forms.NumericUpDown NumMotorID;
        private System.Windows.Forms.Label LblMoveMotorNumber;
        private System.Windows.Forms.Label LblAxisNumber;
        private System.Windows.Forms.NumericUpDown NumAxisID;
        private System.Windows.Forms.TableLayoutPanel TableMain;
        private System.Windows.Forms.GroupBox GrpMoveMotor;
        private System.Windows.Forms.TableLayoutPanel TableMoveMotor;
        private JCS.ToggleSwitch ChkAsync;
        private System.Windows.Forms.Button BtnHomeAxis;
        private System.Windows.Forms.Button BtnHomeAll;
    }
}

