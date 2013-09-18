using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using WAGOConfigurationImporter.Properties;

namespace WAGOConfigurationImporter
{
    public partial class Form1 : Form
    {
        private XDocument _xWagoConfig;
        private ImportProcessor _importProcessor;

        public Form1()
        {
            _importProcessor = new ImportProcessor();
            InitializeComponent();
            this.textBox1.GotFocus +=
                (sender, args) => textBox1.Text = ("IP контроллера" == textBox1.Text) ? "" : textBox1.Text;
            this.configVisualizator1.wagoModuleSelected += delegate(object sender, SelectedModuleEventArgs args)
                {
                    //MessageBox.Show("hello");
                    label2.Text = args.SelectedModule.ModuleName;
                    label8.Text = args.SelectedModule.ModuleNo;
                    label9.Text = args.SelectedModule.PhysicalPosition.ToString();
                    label10.Text = args.SelectedModule.ChannelsCount.ToString();
                    label11.Text = args.SelectedModule.IoType.ToString();
                    label12.Text = args.SelectedModule.SignalType.ToString();
                };
            
            if (!TestConnectionString(Settings.Default.Plc27ConnectionString))
            {
                InvokeConnectionForm();
            }
        }

        public void InvokeConnectionForm()
        {
                var cf = new ConnectionForm();
                cf.ConnectionEstablished +=new EventHandler<ConnectionEstablishedEventArgs>(RewriteConnectionSettings);
                cf.Closed += delegate { Properties.Settings.Default.Reload(); };
                ConfigurationManager.RefreshSection("connectionStrings");
            cf.ShowDialog();
            //Application.Run(cf);
        }

        private void RewriteConnectionSettings(object sender, ConnectionEstablishedEventArgs connectionEstablishedEventArgs)
        {
            //updating config file
            var xmlDoc = new XmlDocument();
            //Loading the Config file
            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            if (xmlDoc.DocumentElement != null)
                foreach (XmlElement xElement in xmlDoc.DocumentElement)
                {
                    if (xElement.Name == "connectionStrings")
                    {
                        //setting the coonection string
                        if (xElement.FirstChild.Attributes != null)
                            xElement.FirstChild.Attributes[1].Value = connectionEstablishedEventArgs.ConnectionString;
                    }
                }
            //writing the connection string in config file
            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            //Properties.Settings.Default.Reload();
            
            
        }

        /// <summary>
        /// Проверка возможности подключения через данный connectionString
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static bool TestConnectionString(string connectionString)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    return (conn.State == ConnectionState.Open);
                }
                catch
                {
                    return false;
                }
            }
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
                {
                    InitialDirectory = "c:\\",
                    Filter = @"WAGO-I/O-Check(*.xml)|*.xml;|All files (*.*)|*.*",
                    FilterIndex = 1,
                    RestoreDirectory = true
                };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Stream myStream = null;
                    if ((myStream = openFileDialog.OpenFile()) != null)
                    {
                        _xWagoConfig = new XDocument();
                        using (myStream)
                        {
                            
                            _xWagoConfig = XDocument.Load(myStream);
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: Невозможно произвести чтение из файла:\n" + ex.Message);
                }
                _importProcessor.ParceXML(_xWagoConfig);
                //moduleInfos = ImportProcessor.ParceXML(xWagoConfig);
                TreeViewBuilder(_importProcessor.modulesInfos);
                //TODO: Обновить боковую панель с детальной информацией о выбранном модуле

            }

        }



        private void Form1_Load(object sender, EventArgs e)
        {

            // TODO: This line of code loads data into the 'plc27DataSet.PhysicalChannel' table. You can move, or remove it, as needed.
            this.physicalChannelTableAdapter.Fill(this.plc27DataSet.PhysicalChannel);
            this.fieldBusNodeTableAdapter1.Fill(this.plc27DataSet.FieldBusNode);
          
            this.addressPart1ComboBox.DataSource = (from row in plc27DataSet.FieldBusNode.AsEnumerable()
                                                    select row.Field<string>("addressPart1")).ToList<string>();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_xWagoConfig == null)
            {
                MessageBox.Show("Не загружен xml файл с конфигурацией","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error); 
                return;
            }

            try
            {
                var rowsAffected = new int();
                var insertedNodeId = new int();
                if (radioButton1.Checked/*добавить*/)
                {
                    var newFieldBusNodeRow = _importProcessor.CreateNewFieldBusNodeRow(this.plc27DataSet, textBox1.Text);
                    this.plc27DataSet.FieldBusNode.Rows.Add(newFieldBusNodeRow);
                    //var no = this.plc27DataSet.FieldBusNode.SingleOrDefault(x => x.AddressPart1 == this.textBox1.Text);
                    this.fieldBusNodeTableAdapter1.Update(this.plc27DataSet);
                    insertedNodeId = plc27DataSet.FieldBusNode[plc27DataSet.FieldBusNode.Rows.IndexOf(newFieldBusNodeRow)].Id;

                }
                else if (radioButton2.Checked/*заменить*/)
                {
                    
                    insertedNodeId =
                        plc27DataSet.FieldBusNode.FirstOrDefault(x => x.AddressPart1 == (string) this.addressPart1ComboBox.SelectedItem)
                                    .Id;
                    ClearLogicalChannels(insertedNodeId);
                }
                else throw new Exception("Не задан узел полевой шины");

                List<int> gids = _importProcessor.InsertPhysicalChannels(physicalChannelTableAdapter, insertedNodeId);
                rowsAffected = _importProcessor.InsertLogicalChannels(logicalChannelTableAdapter1, gids, insertedNodeId);

                MessageBox.Show(String.Format("Успешно импортировано {0} каналов", rowsAffected), "Импорт произведен",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.physicalChannelTableAdapter.Update(this.plc27DataSet);
            }

            catch (Exception ex)
            {

                MessageBox.Show("Ошибка вставки новой конфигурации: " + ex.Message, "Ошибка", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }



        }

        /// <summary>
        /// Построения дерева "контроллер и список модулей" для импортированого xml
        /// </summary>
        /// <param name="moduleInfos"></param>
        public void TreeViewBuilder(List<ModuleInfo> moduleInfos)
        {
            if (moduleInfos!=null && moduleInfos.Count > 0)
            {
                treeView1.BeginUpdate();
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(new TreeNode(moduleInfos.ElementAt(0).ModuleName));
                
                TreeNode[] nodeTree = new TreeNode[]{};
                for (int i = 1; i < moduleInfos.Count-1; i++)
                {
                    var moduleInfo = moduleInfos[i];
                    treeView1.Nodes[0].Nodes.Add(new TreeNode(moduleInfo.ModuleNo));
                }

                treeView1.ExpandAll();
                treeView1.EndUpdate();
            }
            else
            {
                MessageBox.Show("В файле конфигурации отсутсвует информация о контроллере!", "Предупреждение",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var logForm = new LogicalChannelsForm();

            if ((Application.OpenForms["LogicalChannelsForm"]) == null)
                logForm.Show();
            else
            {
                Application.OpenForms["LogicalChannelsForm"].BringToFront();
            }
            
        }


        /// <summary>
        /// Очистка логических каналов для команды "Заменить"
        /// </summary>
        /// <param name="fieldNodelId"></param>
        public void ClearLogicalChannels(int fieldNodelId)
        {
            //удаление логических каналов (т.к. они ссылаються на удаляемый физический)

                this.logicalChannelTableAdapter1.DeleteLogicalChannelReferences(fieldNodelId);
            
            //удаление физического канала
                this.physicalChannelTableAdapter.DeletePhysicalChannel(fieldNodelId); 
            
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            InvokeConnectionForm();
        }





    }
}
