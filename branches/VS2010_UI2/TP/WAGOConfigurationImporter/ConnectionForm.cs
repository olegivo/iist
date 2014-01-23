using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using WAGOConfigurationImporter.Properties;

namespace WAGOConfigurationImporter
{

    public partial class ConnectionForm : Form
    {
        private const string ConnectionString = "Data Source={0};Initial Catalog={1};Integrated Security=True";
        public event EventHandler<ConnectionEstablishedEventArgs> ConnectionEstablished;
        protected void OnConnectionEstablished(ConnectionEstablishedEventArgs e)
        {
            EventHandler<ConnectionEstablishedEventArgs> handler = ConnectionEstablished;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public ConnectionForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var cs = String.Format(ConnectionString, comboBox1.Text, textBox1.Text);
            if (Form1.TestConnectionString(cs))
            {
                ConnectionEstablishedEventArgs args = new ConnectionEstablishedEventArgs() { ConnectionString = cs };
                OnConnectionEstablished(args);
                Close();
            }
            else
            {
                MessageBox.Show("Строка подключения задана неверно!", "Ошибка", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
            }
            //MessageBox.Show("Подключение установлено!","Подключение настроено",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
        }

        private void ConnectionForm_Load(object sender, EventArgs e)
        {
            var servers = SqlDataSourceEnumerator.Instance.GetDataSources();
			comboBox1.DataSource = (from row in servers.AsEnumerable() where !string.IsNullOrEmpty(row.Field<string>("InstanceName")) select row.Field<string>("ServerName") + "\\" + row.Field<string>("InstanceName")).ToList<string>();
            
        }

    }
    public class ConnectionEstablishedEventArgs : EventArgs
    {
        public string ConnectionString { get; set; }
    }
}
