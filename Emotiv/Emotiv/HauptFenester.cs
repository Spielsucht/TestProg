using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emotiv;

namespace Emotiv
{
    public partial class HauptFenester : Form
    {
        Form window2;
        public HauptFenester()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            window2 = new Form2();
            window2.Show();
        }

        private void button2_Click(object sender, EmoEngineEventArgs e)
        {

        }
    }
}
