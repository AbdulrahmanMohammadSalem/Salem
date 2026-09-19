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

            
            var c = comboBox1.ItemHeight;
        }

        private void Form3_Load(object sender, EventArgs e) {
            
        }

        private void comboBox1_FontChanged(object sender, EventArgs e) {
            label1.Text = $"Font Height = {comboBox1.Font.Height}, Item Height = {comboBox1.ItemHeight}, SendMessage Result = {Salem.PInvoke.User32.SendMessage(comboBox1.Handle, 0x0154, IntPtr.Zero, IntPtr.Zero)}";
        }
            
        private void fuiButton1_Click(object sender, EventArgs e) {
            comboBox1_FontChanged(null, null);
        }
        }

    public class c : ComboBox {
        public c() {
            
        }
    }
}
