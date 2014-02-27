using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpheroNET;
using System.ComponentModel;

namespace Emotiv
{
    public delegate void SpheroConnectionFinished<IModelSetup>(IModelSetup sender, bool status);

    class SpheroModel : IModelSetup
    {
        public event SpheroConnectionFinished<SpheroModel> finished;
        public event ModelTextHandler<SpheroModel> textChange; 
        private SpheroConnector spheroConnector;
        private Sphero sphero = null;
        private BackgroundWorker connectSpheroWorker;
        private ISetLabels labels;

        public SpheroModel()
        {
            spheroConnector = new SpheroConnector();

            connectSpheroWorker = new BackgroundWorker();
            connectSpheroWorker.DoWork += new DoWorkEventHandler(connectSpheroWorker_DoWork);
            connectSpheroWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(connectSpheroWorker_RunWorkerCompleted);
        }

        public void attach(IObserver obs)
        {
            finished += new SpheroConnectionFinished<SpheroModel>(obs.spheroConnectionResponse);
            textChange += new ModelTextHandler<SpheroModel>(obs.textChange);
        }

        public void setLabelsClass(ISetLabels slb)
        {
            labels = slb;
        }

        public void connectToBall()
        {
            if (sphero == null)
            {
                connectSpheroWorker.RunWorkerAsync();
            }
            else
            {
                spheroConnector.Close();
                sphero = null;
                finished(this, false);
                connectToBall();
            }
        }

        private void connectSpheroWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            labels.spheroStatusText("Searching for Device");
            textChange(this);
            spheroConnector.Scan();
            List<string> deviceNames = spheroConnector.DeviceNames;
            if (deviceNames.Contains("Sphero-GGW"))
            {
                labels.spheroStatusText("Connecting");
                textChange(this);
                sphero = spheroConnector.Connect(deviceNames.IndexOf("Sphero-GGW"));
                Console.WriteLine("Sphero Connected");
            }
            else
            {
                labels.spheroStatusText("No Sphero Found");
                textChange(this);
            }
        }

        private void connectSpheroWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null && sphero != null)
            {
                labels.spheroStatusText("Connected");
                finished(this, true);
            }
            else
            {
                labels.spheroStatusText("Connection Failed");
            }
            textChange(this);
        }

        public void setRGBColor(int red, int green, int blue)
        {
            if ((red >= 0 && red <= 255) && (green >= 0 && green <= 255) && (blue >= 0 && blue <= 255))
            {
                sphero.SetRGBLEDOutput((byte)red, (byte)green, (byte)blue); 
            }
            else
            {
                throw new IndexOutOfRangeException("Einer der Farbwerte ist außerhalb des Wertebereichs (0-255).");
            }
        }

        public void disconnectFromBall()
        {
            if (sphero != null)
            {
                Console.WriteLine("Sphero Disconnected");
                spheroConnector.Close();
                sphero = null;
            }
        }

        public void backLED(bool state)
        {
            if (state)
                sphero.SetBackLED(1.0);
            else
                sphero.SetBackLED(0.0);
        }

        public void setHeading(UInt16 heading)
        {
            sphero.SetHeading(heading);
        }

        public void moveBall(float speed, UInt16 heading)
        {
            sphero.PerformMove(heading, speed);
        }

        public void sleep()
        {
            sphero.Sleep();
        }
    }
}
