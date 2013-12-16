using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emotiv
{
    public delegate void ModelTextHandler<IModelSetup>(IModelSetup sender);

    public interface IModel
    {
        void setEmoDongleLabel(string status);
        void setSpheroConStatus(bool status);
        bool getSpheroConStatus();
        void setUserControl(string radioButtonName);
        string getUserControl();
    }

    public interface IModelSetup
    {
        void setLabelsClass(ISetLabels slb);
        void attach(IObserver obs);
    }

    class Model : IModel, IModelSetup
    {
        private bool spheroConnectStatus;
        private ISetLabels labels;
        private string userControl;
        private string previousControl = null;

        public event ModelTextHandler<Model> textChange;

        public Model()
        {
            spheroConnectStatus = false;
        }

        public void setEmoDongleLabel(string status)
        {
            labels.setDongleStatusText(status);
            textChange(this);
        }

        public void setSpheroConStatus(bool bStatus)
        {
            spheroConnectStatus = bStatus;
        }

        public bool getSpheroConStatus()
        {
            return spheroConnectStatus;
        }

        public void setUserControl(string radioButtonName)
        {
            if (radioButtonName == "Emotiv")
            {
                userControl = "EmoControl";
            }
            else if (radioButtonName == "Tastatur")
            {
                userControl = "MainWindow";
            }
            else if (radioButtonName == "previousControl")
            {
                string tmp = userControl;
                userControl = previousControl;
                previousControl = tmp;
            }
            else
            {
                userControl = null;
            }
        }

        public string getUserControl()
        {
            return userControl;
        }

        public void setLabelsClass(ISetLabels slb)
        {
            labels = slb;
        }

        public void attach(IObserver obs)
        {
            textChange += new ModelTextHandler<Model>(obs.textChange);
        }
    }
}
