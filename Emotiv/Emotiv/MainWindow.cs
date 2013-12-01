using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Emotiv
{
    public partial class MainWindow : Form
    {

        delegate void SetTextCallback(string text);
        private EmoControl control;

        public MainWindow()
        {
            control = new EmoControl(this);

            InitializeComponent();

            Thread emoControlThread = new Thread(new ThreadStart(control.run));
            emoControlThread.Start();
        }

        public void setLabelText(string setText)
        {
            if (this.label1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(setLabelText);
                this.Invoke(d, new object[] { setText });
            }
            else
            {
                this.label1.Text = setText;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            control.stopRunning();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            control.stopRunning();
        }
    }
}
