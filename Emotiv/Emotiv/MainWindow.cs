using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Emotiv
{
    public partial class MainWindow : Form
    {

        delegate void SetTextCallback(string text);
        private EmoControl control;
        private BackgroundWorker emoWorker;
        public delegate void loadProfile(string path);
        public event loadProfile onLoadProfile;

        public MainWindow()
        {
            InitializeComponent();
            control = new EmoControl(this);
            emoWorker = new BackgroundWorker();

            startEmoControl();
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

        private void startEmoControl()
        {
            if (!emoWorker.IsBusy)
            {
                emoWorker.DoWork += new DoWorkEventHandler(emoWorker_DoWork);
                emoWorker.RunWorkerAsync();
            }
        }
        private void emoWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            control.run();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            control.stopRunning();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            control.stopRunning();
            emoWorker.Dispose();
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Emotiv Pofile (*.emu)|*.emu";
            if (open.ShowDialog() == DialogResult.OK)
            {
                if (open.FileName != null)
                {
                    tbPofilePath.Text = open.FileName;
                    this.onLoadProfile(open.FileName);
                }
            }
        }
    }
}
