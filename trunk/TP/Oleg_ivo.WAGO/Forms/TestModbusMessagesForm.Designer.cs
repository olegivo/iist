namespace Oleg_ivo.WAGO.Forms
{
    partial class TestModbusMessagesForm
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
            this.components = new System.ComponentModel.Container();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.lbPorts = new System.Windows.Forms.ListBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tbConnectLog = new System.Windows.Forms.TextBox();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemClear = new System.Windows.Forms.ToolStripMenuItem();
            this.grConnect = new System.Windows.Forms.GroupBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnConfig = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.cbFlowControl = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbStopBits = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbParity = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbData = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbBaudrate = new System.Windows.Forms.ComboBox();
            this.grDataInput = new System.Windows.Forms.GroupBox();
            this.tbInputLog = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.tbSendData = new System.Windows.Forms.TextBox();
            this.grDataOutput = new System.Windows.Forms.GroupBox();
            this.tbOutputLog = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.serialPortConnect = new Oleg_ivo.WAGO.SerialPortConnect();
            this.contextMenu.SuspendLayout();
            this.grConnect.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.grDataInput.SuspendLayout();
            this.grDataOutput.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "Обнаружено устройство";
            this.notifyIcon1.Visible = true;
            // 
            // lbPorts
            // 
            this.lbPorts.FormattingEnabled = true;
            this.lbPorts.Location = new System.Drawing.Point(3, 29);
            this.lbPorts.Name = "lbPorts";
            this.lbPorts.Size = new System.Drawing.Size(114, 43);
            this.lbPorts.TabIndex = 0;
            this.lbPorts.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbPorts_MouseDoubleClick);
            this.lbPorts.SelectedIndexChanged += new System.EventHandler(this.lbPorts_SelectedIndexChanged);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(3, 78);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(114, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tbConnectLog
            // 
            this.tbConnectLog.ContextMenuStrip = this.contextMenu;
            this.tbConnectLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbConnectLog.Location = new System.Drawing.Point(0, 0);
            this.tbConnectLog.Multiline = true;
            this.tbConnectLog.Name = "tbConnectLog";
            this.tbConnectLog.ReadOnly = true;
            this.tbConnectLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbConnectLog.Size = new System.Drawing.Size(273, 136);
            this.tbConnectLog.TabIndex = 2;
            this.tbConnectLog.Leave += new System.EventHandler(this.tb_Leave);
            this.tbConnectLog.Enter += new System.EventHandler(this.tb_Enter);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemClear});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(135, 26);
            // 
            // menuItemClear
            // 
            this.menuItemClear.Name = "menuItemClear";
            this.menuItemClear.Size = new System.Drawing.Size(134, 22);
            this.menuItemClear.Text = "Очистить";
            this.menuItemClear.Click += new System.EventHandler(this.menuItemClear_Click);
            // 
            // grConnect
            // 
            this.grConnect.Controls.Add(this.splitContainer2);
            this.grConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grConnect.Location = new System.Drawing.Point(0, 0);
            this.grConnect.Name = "grConnect";
            this.grConnect.Size = new System.Drawing.Size(561, 155);
            this.grConnect.TabIndex = 4;
            this.grConnect.TabStop = false;
            this.grConnect.Text = "Connecting";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 16);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btnConfig);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.btnDisconnect);
            this.splitContainer2.Panel1.Controls.Add(this.cbFlowControl);
            this.splitContainer2.Panel1.Controls.Add(this.btnConnect);
            this.splitContainer2.Panel1.Controls.Add(this.lbPorts);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.cbStopBits);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            this.splitContainer2.Panel1.Controls.Add(this.cbParity);
            this.splitContainer2.Panel1.Controls.Add(this.label4);
            this.splitContainer2.Panel1.Controls.Add(this.cbData);
            this.splitContainer2.Panel1.Controls.Add(this.label5);
            this.splitContainer2.Panel1.Controls.Add(this.cbBaudrate);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tbConnectLog);
            this.splitContainer2.Size = new System.Drawing.Size(555, 136);
            this.splitContainer2.SplitterDistance = 279;
            this.splitContainer2.SplitterWidth = 3;
            this.splitContainer2.TabIndex = 8;
            // 
            // btnConfig
            // 
            this.btnConfig.Location = new System.Drawing.Point(3, 3);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(114, 23);
            this.btnConfig.TabIndex = 8;
            this.btnConfig.Text = "Config";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(122, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Baudrate";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(3, 103);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(114, 23);
            this.btnDisconnect.TabIndex = 4;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // cbFlowControl
            // 
            this.cbFlowControl.FormattingEnabled = true;
            this.cbFlowControl.Location = new System.Drawing.Point(192, 101);
            this.cbFlowControl.Margin = new System.Windows.Forms.Padding(2);
            this.cbFlowControl.Name = "cbFlowControl";
            this.cbFlowControl.Size = new System.Drawing.Size(84, 21);
            this.cbFlowControl.TabIndex = 7;
            this.cbFlowControl.SelectedValueChanged += new System.EventHandler(this.cbFlowControl_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(122, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Data";
            // 
            // cbStopBits
            // 
            this.cbStopBits.FormattingEnabled = true;
            this.cbStopBits.Location = new System.Drawing.Point(192, 76);
            this.cbStopBits.Margin = new System.Windows.Forms.Padding(2);
            this.cbStopBits.Name = "cbStopBits";
            this.cbStopBits.Size = new System.Drawing.Size(84, 21);
            this.cbStopBits.TabIndex = 7;
            this.cbStopBits.SelectedValueChanged += new System.EventHandler(this.cbStopBits_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(122, 54);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Parity";
            // 
            // cbParity
            // 
            this.cbParity.FormattingEnabled = true;
            this.cbParity.Location = new System.Drawing.Point(192, 52);
            this.cbParity.Margin = new System.Windows.Forms.Padding(2);
            this.cbParity.Name = "cbParity";
            this.cbParity.Size = new System.Drawing.Size(84, 21);
            this.cbParity.TabIndex = 7;
            this.cbParity.SelectedValueChanged += new System.EventHandler(this.cbParity_SelectedValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(122, 79);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Stop bits";
            // 
            // cbData
            // 
            this.cbData.FormattingEnabled = true;
            this.cbData.Location = new System.Drawing.Point(192, 28);
            this.cbData.Margin = new System.Windows.Forms.Padding(2);
            this.cbData.Name = "cbData";
            this.cbData.Size = new System.Drawing.Size(84, 21);
            this.cbData.TabIndex = 7;
            this.cbData.SelectedValueChanged += new System.EventHandler(this.cbData_SelectedValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(122, 103);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Flow control";
            // 
            // cbBaudrate
            // 
            this.cbBaudrate.FormattingEnabled = true;
            this.cbBaudrate.Location = new System.Drawing.Point(192, 3);
            this.cbBaudrate.Margin = new System.Windows.Forms.Padding(2);
            this.cbBaudrate.Name = "cbBaudrate";
            this.cbBaudrate.Size = new System.Drawing.Size(84, 21);
            this.cbBaudrate.TabIndex = 7;
            this.cbBaudrate.SelectedValueChanged += new System.EventHandler(this.cbBaudrate_SelectedValueChanged);
            // 
            // grDataInput
            // 
            this.grDataInput.Controls.Add(this.tbInputLog);
            this.grDataInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grDataInput.Location = new System.Drawing.Point(0, 0);
            this.grDataInput.Name = "grDataInput";
            this.grDataInput.Size = new System.Drawing.Size(264, 311);
            this.grDataInput.TabIndex = 5;
            this.grDataInput.TabStop = false;
            this.grDataInput.Text = "Data Input";
            // 
            // tbInputLog
            // 
            this.tbInputLog.ContextMenuStrip = this.contextMenu;
            this.tbInputLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbInputLog.Location = new System.Drawing.Point(3, 16);
            this.tbInputLog.Multiline = true;
            this.tbInputLog.Name = "tbInputLog";
            this.tbInputLog.ReadOnly = true;
            this.tbInputLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbInputLog.Size = new System.Drawing.Size(258, 292);
            this.tbInputLog.TabIndex = 2;
            this.tbInputLog.Leave += new System.EventHandler(this.tb_Leave);
            this.tbInputLog.Enter += new System.EventHandler(this.tb_Enter);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(462, 2);
            this.btnSend.Margin = new System.Windows.Forms.Padding(2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(90, 24);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tbSendData
            // 
            this.tbSendData.Location = new System.Drawing.Point(3, 6);
            this.tbSendData.Margin = new System.Windows.Forms.Padding(2);
            this.tbSendData.Name = "tbSendData";
            this.tbSendData.Size = new System.Drawing.Size(456, 20);
            this.tbSendData.TabIndex = 4;
            this.tbSendData.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbSendData_KeyUp);
            // 
            // grDataOutput
            // 
            this.grDataOutput.Controls.Add(this.tbOutputLog);
            this.grDataOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grDataOutput.Location = new System.Drawing.Point(0, 0);
            this.grDataOutput.Name = "grDataOutput";
            this.grDataOutput.Size = new System.Drawing.Size(294, 311);
            this.grDataOutput.TabIndex = 5;
            this.grDataOutput.TabStop = false;
            this.grDataOutput.Text = "Data Output";
            // 
            // tbOutputLog
            // 
            this.tbOutputLog.ContextMenuStrip = this.contextMenu;
            this.tbOutputLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbOutputLog.Location = new System.Drawing.Point(3, 16);
            this.tbOutputLog.Multiline = true;
            this.tbOutputLog.Name = "tbOutputLog";
            this.tbOutputLog.ReadOnly = true;
            this.tbOutputLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbOutputLog.Size = new System.Drawing.Size(288, 292);
            this.tbOutputLog.TabIndex = 2;
            this.tbOutputLog.Leave += new System.EventHandler(this.tb_Leave);
            this.tbOutputLog.Enter += new System.EventHandler(this.tb_Enter);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grDataInput);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grDataOutput);
            this.splitContainer1.Size = new System.Drawing.Size(561, 311);
            this.splitContainer1.SplitterDistance = 264;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 6;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.grConnect);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(561, 502);
            this.splitContainer3.SplitterDistance = 155;
            this.splitContainer3.SplitterWidth = 3;
            this.splitContainer3.TabIndex = 7;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.tbSendData);
            this.splitContainer4.Panel1.Controls.Add(this.btnSend);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer4.Size = new System.Drawing.Size(561, 344);
            this.splitContainer4.SplitterDistance = 30;
            this.splitContainer4.SplitterWidth = 3;
            this.splitContainer4.TabIndex = 7;
            // 
            // serialPortConnect
            // 
            this.serialPortConnect.BaudRate = 0;
            this.serialPortConnect.DataBits = 0;
            this.serialPortConnect.Parity = System.IO.Ports.Parity.None;
            this.serialPortConnect.PortName = null;
            this.serialPortConnect.StopBits = System.IO.Ports.StopBits.None;
            this.serialPortConnect.PinChanged += new System.IO.Ports.SerialPinChangedEventHandler(this.serialPortConnect_PinChanged);
            this.serialPortConnect.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortConnect_DataReceived);
            this.serialPortConnect.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.serialPortConnect_ErrorReceived);
            this.serialPortConnect.LogMessage += new Oleg_ivo.WAGO.SerialPortConnectMessageEventHandler(this.serialPortConnect_LogMessage);
            // 
            // TestModbusMessagesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 502);
            this.Controls.Add(this.splitContainer3);
            this.Name = "TestModbusMessagesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TestModbusMessagesForm";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenu.ResumeLayout(false);
            this.grConnect.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.grDataInput.ResumeLayout(false);
            this.grDataInput.PerformLayout();
            this.grDataOutput.ResumeLayout(false);
            this.grDataOutput.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ListBox lbPorts;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox tbConnectLog;
        private System.Windows.Forms.GroupBox grConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.GroupBox grDataInput;
        private System.Windows.Forms.GroupBox grDataOutput;
        private SerialPortConnect serialPortConnect;
        private System.Windows.Forms.TextBox tbSendData;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox tbInputLog;
        private System.Windows.Forms.TextBox tbOutputLog;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem menuItemClear;
        private System.Windows.Forms.ComboBox cbBaudrate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbData;
        private System.Windows.Forms.ComboBox cbParity;
        private System.Windows.Forms.ComboBox cbStopBits;
        private System.Windows.Forms.ComboBox cbFlowControl;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Button btnConfig;
    }
}