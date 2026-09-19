using Ookii.Dialogs.WinForms;
using Salem.Extensions;
using Salem.Extensions.External;
using Salem.Utils;
using System;
using System.Configuration;
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

        private void Form3_Load(object sender, EventArgs e) {
            
        }

        private void button1_Click(object sender, EventArgs e) {
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) {
            SpecialEventHandlers.SignedDecimalInput(textBox1, e);
        }
    }
}
