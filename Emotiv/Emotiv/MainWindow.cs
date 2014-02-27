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
        void spheroConnectionResponse(IModelSetup sender, bool status);
        void comboBoxChange(List<List<string>> lists, string[] chosen);
    }

    public interface IView
    {
        void setCoordinatorLabels(IController coor, IGetLabels gbl);
    }

    public partial class MainWindow : Form, IView, IObserver
    {
        private IController coordinator;
        private IGetLabels labels;
        private List<ComboBox> comboBoxes;
        private bool boxChange = true;

        public void setCoordinatorLabels(IController coor, IGetLabels gbl)
        {
            coordinator = coor;
            labels = gbl;

            comboBoxes = new List<ComboBox>();
            comboBoxes.Add(cbForword);
            comboBoxes.Add(cbBackword);
            comboBoxes.Add(cbLeft);
            comboBoxes.Add(cbRight);

            foreach (ComboBox box in comboBoxes)
            {
                box.DataSource = labels.getComboboxList();
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            chbCalibration.Enabled = false;
            trbrCalibration.Enabled = false;
        }

        public void textChange(IModelSetup m)
        {
            string[] labelStrings;
            int[] values;
            labels.getLabels(out labelStrings, out values);
            try
            {
                this.Invoke((MethodInvoker)delegate()
                    {
                        lbEmoStatus.Text = labelStrings[0];
                        lbConnection.Text = labelStrings[2];
                        lbHeadsetStatus.Text = "Headset Status: " + labelStrings[1];
                        lbSpeed.Text = "Geschw.: " + labelStrings[3] + "%";
                        pbHeadsetBattery.Value = values[0];
                    });
            }
            catch (Exception)
            {
                
            }
        }

        public void spheroConnectionResponse(IModelSetup sender, bool status)
        {
            chbCalibration.Enabled = status;
            coordinator.setSpheroStatus(status);
        }

        public void comboBoxChange(List<List<string>> lists, string[] chosen)
        {
            for (int i = 0; i < 4; i++)
			{
                comboBoxes[i].DataSource = lists[i];
                comboBoxes[i].Text = chosen[i];
			}
            boxChange = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            coordinator.stopEmoControl();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            trbrSpeed_Scroll(this, new EventArgs());
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
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
                coordinator.setUserControl("cali");
            }
            else
            {
                trbrCalibration.Enabled = false;
                coordinator.setBackLED(false);
                RadioButtons_Checked(this, new EventArgs());
            }
            trbrCalibration.Value = 180;
            coordinator.setHeading(0);
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
                cbDevices.Text = null;
            }
        }

        private void trbrCalibration_Scroll(object sender, EventArgs e)
        {
            coordinator.caliBall(trbrCalibration.Value);
        }

        private void trbrSpeed_Scroll(object sender, EventArgs e)
        {
            coordinator.setSpeed(trbrSpeed.Value);
        }

        private void RadioButtons_Checked(object sender, EventArgs e)
        {
            var RadioButton = groupBox1.Controls.OfType<RadioButton>().SingleOrDefault(rb => rb.Checked == true) as RadioButton;
            if (RadioButton != null)
            {
                coordinator.setUserControl(RadioButton.Text);
            }
        }

        private void MainWindow_KeyPress(object sender, KeyPressEventArgs e)
        {
            coordinator.keyControl(e.KeyChar);
        }

        private void radioKeyboard_KeyUp(object sender, KeyEventArgs e)
        {
            coordinator.keyControl('h');
        }

        private void comboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox cbSender = (ComboBox)sender;
            if (cbSender.Visible && boxChange)
            {
                boxChange = false;
                coordinator.comboboxChange(cbSender, labels.getComboboxChosen(), labels.getComboboxList());
            }
        }

        private void btGyroRes_Click(object sender, EventArgs e)
        {
            coordinator.setUserControl("Gyroskop");
        }
    }
}
