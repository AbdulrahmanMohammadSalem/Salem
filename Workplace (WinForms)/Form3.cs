using Ookii.Dialogs.WinForms;
using Salem.Extensions;
using Salem.Extensions.External;
using Salem.Utils;
using System;
using System.Configuration;

using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using Workplace__WinForms_.Properties;

namespace Workplace__WinForms_ {
    public partial class Form3 : Form {
        public Form3() {
            InitializeComponent();
        }

        private void fuiButton1_Click(object sender, EventArgs e) {



        }

        internal const string CONNECTION_STRING = "Server=.;Database=VehicleMakesDB;User ID=sa;Password=sa123456";


        private async void Form3_Load(object sender, EventArgs e) {
            const string QUERY = "select * from VehicleDetails";
            var _stopWatch = new Stopwatch();

            using (var _connection = new SqlConnection(CONNECTION_STRING))
            using (var _command = new SqlCommand(QUERY, _connection)) {
                await _connection.OpenAsync();

                using (var _reader = await _command.ExecuteReaderAsync()) {

                    var _dt = new DataTable();

                    _stopWatch.Start();
                    await _reader.LoadIntoTableAsync();

                    _stopWatch.Stop();
                    MessageBox.Show($"Time Elapsed = {_stopWatch.ElapsedMilliseconds}ms");
                    //dataGridView1.DataSource = _dt;
                }
            }
        }
    }
}
