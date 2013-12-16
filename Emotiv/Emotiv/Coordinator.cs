using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.Windows.Forms;

namespace Emotiv
{
    public interface IController : IDisposable
    {
        void startEmoControl();
        void stopEmoControl();
        void connectToSphero();
        void setBackLED(bool state);
        void loadEmoProfil(string path);
        void setHeading(UInt16 heading);
        void setEmoDongleLabel(string status);
        void setSpheroStatus(bool status);
        void setUserControl(string radioButtonName);
        void move(object sender, float speed, int iHeading);
    }

    class Coordinator : IController
    {
        private SpheroModel spheroModel;
        private EmoControl emoControl;
        private IView window;
        private Model model;
        private Labels labels;

        private BackgroundWorker emoWorker;

        public Coordinator(IView mw, Model mo)
        {
            labels = new Labels();
            window = mw;
            model = mo;
            emoControl = new EmoControl(this);
            emoWorker = new BackgroundWorker();
            mw.setCoordinatorLabels(this, (IGetLabels)labels);
            model.attach((IObserver)window);
            model.setLabelsClass((ISetLabels) labels);
            startEmoControl();
        }

        public void startEmoControl()
        {
            if (!emoWorker.IsBusy)
            {
                emoWorker.DoWork += new DoWorkEventHandler(emoWorker_DoWork);
                emoWorker.RunWorkerAsync();
                Console.WriteLine("Worker working");
            }
        }

        public void stopEmoControl()
        {
            emoControl.stopRunning();
        }

        private void emoWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            emoControl.run();
        }

        public void loadEmoProfil(string path)
        {
            emoControl.onLoadProfile(path);
        }

        public void setEmoDongleLabel(string status)
        {
            model.setEmoDongleLabel(status);
        }

        public void connectToSphero()
        {
            if (!model.getSpheroConStatus())
            {
                spheroModel = new SpheroModel();
                spheroModel.attach((IObserver)window);
                spheroModel.setLabelsClass((ISetLabels)labels);
                spheroModel.connectToBall();
            }
        }

        public void setBackLED(bool state)
        {
            spheroModel.backLED(state);
        }

        public void setHeading(UInt16 heading)
        {
            spheroModel.setHeading(heading);
        }

        public void setSpheroStatus(bool status)
        {
            model.setSpheroConStatus(status);
        }

        public void setUserControl(string radioButtonName)
        {
            model.setUserControl(radioButtonName);
        }

        public void move(object sender, float speed, int iHeading)
        {
            string type = sender.GetType().Name;
            if (((type == model.getUserControl()) || (speed == 0)) && model.getSpheroConStatus()) //
            {
                spheroModel.moveBall(speed, iHeading);
            }
        }

        public void Dispose()
        {
            if (model.getSpheroConStatus())
            {
                spheroModel.sleep();
            }
            if (emoWorker.IsBusy)
            {
                emoControl.stopRunning();
            }
        }
    }
}
