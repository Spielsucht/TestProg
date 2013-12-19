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
    public interface IObserver
    {
        void textChange(IModelSetup model);
        void spheroConnectionResponse(IModelSetup s);
    }

    public interface IView
    {
        void setCoordinatorLabels(IController coor, IGetLabels gbl);
    }

    public partial class MainWindow : Form, IView, IObserver
    {
        private IController coordinator;
        private IGetLabels labels;

        public void setCoordinatorLabels(IController coor, IGetLabels gbl)
        {
            coordinator = coor;
            labels = gbl;
        }

        public MainWindow()
        {
            InitializeComponent();

            chbCalibration.Enabled = false;
            trbrCalibration.Enabled = false;
        }

        public void textChange(IModelSetup m)
        {
            string[] labelStrings = labels.getLabels();
            this.Invoke((MethodInvoker)delegate(){
                lbEmoStatus.Text = labelStrings[0];
                lbConnection.Text = labelStrings[2];
                lbHeadsetStatus.Text = "Headset Status: ";
            });
        }

        public void spheroConnectionResponse(IModelSetup s)
        {
            chbCalibration.Enabled = true;
            coordinator.setSpheroStatus(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            coordinator.stopEmoControl();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            coordinator.Dispose();
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Emotiv Pofile (*.emu)|*.emu";
            if (open.ShowDialog() == DialogResult.OK)
            {
                if (open.FileName != null)
                {
                    try
                    {
                        tbPofilePath.Text = open.FileName;
                        coordinator.loadEmoProfil(open.FileName);
                    }
                    catch (EmoEngineException ex)
                    {
                        MessageBox.Show(ex.Message + "\nBitte Emotiv Dongle überprüfen",
                            "Fehler beim Profilladen.",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message,
                            "Fehler",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void chbCalibration_CheckedChanged(object sender, EventArgs e)
        {
            if (chbCalibration.Checked)
            {
                trbrCalibration.Enabled = true;
                coordinator.setBackLED(true);
                
            }
            else
            {
                trbrCalibration.Enabled = false;
                coordinator.setBackLED(false);
            }
            trbrCalibration.Value = 180;
            coordinator.setHeading(0);
            coordinator.setUserControl("previousControl");
        }

        private void cbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                switch (cbDevices.SelectedIndex)
                {
                    case 0:
                        coordinator.connectToSphero();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                            "Fehler",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
            }
        }

        private void trbrCalibration_Scroll(object sender, EventArgs e)
        {
            coordinator.move(this ,0, trbrCalibration.Value);
        }

        private void RadioButtons_Checked(object sender, EventArgs e)
        {
            var RadioButton = groupBox1.Controls.OfType<RadioButton>().SingleOrDefault(rb => rb.Checked == true) as RadioButton;
            coordinator.setUserControl(RadioButton.Text);
        }

        private void MainWindow_KeyPress(object sender, KeyPressEventArgs e)
        {
            char key = e.KeyChar;
            switch (key)
            {
                case 'w':
                    coordinator.move(this, (float)0.4, 180);
                    break;
                case 's':
                    coordinator.move(this, (float)0.4, 0);
                    break;
                case 'a':
                    coordinator.move(this, (float)0.4, 270);
                    break;
                case 'd':
                    coordinator.move(this, (float)0.4, 90);
                    break;
                default:
                    coordinator.move(this, (float)0.0, 360);
                    break;
            }
        }
    }
}
