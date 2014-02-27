using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emotiv
{
    public delegate void ModelTextHandler<IModelSetup>(IModelSetup sender);
    public delegate void ComboBoxChange<IModelSetup>(List<List<string>> lists, string[] chosen);

    public interface IModel
    {
        void setEmoDongleLabel(string status);
        void setSpheroConStatus(bool status);
        void setUserControl(string radioButtonName);
        void setSpheroSpeed(string snumber, float fnumber);
        void setEmoUpdate(int currCharge);
        void setGyro(int gyroX, int gyroY);

        void getGyro(out int gyroX, out int gyroY);
        bool getSpheroConStatus();
        string getUserControl();
        float getSpeed();
        void setComboBoxLists(List<string> listForword, List<string> listBackword, List<string> listLeft, List<string> listRight, string[] chosen, string[] chosenExpName);
        string[] getChosenExp();
        UInt16 lastHeading { get; set; }
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
        private float speed;
        private int gyroMaxX;
        private int gyroMaxY;
        private string[] expChosenName;
        private UInt16 _lastHeading;

        public event ModelTextHandler<Model> textChange;
        public event ComboBoxChange<Model> comboBoxChange;

        public Model()
        {
            spheroConnectStatus = false;
            expChosenName = new string[4];
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

        public void setUserControl(string controlName)
        {
            userControl = controlName;
        }

        public string getUserControl()
        {
            return userControl;
        }

        public void setSpheroSpeed(string snumber, float fnumber)
        {
            labels.spheroSpeedText(snumber);
            speed = fnumber;
            textChange(this);
        }

        public float getSpeed()
        {
            return speed;
        }

        public void setEmoUpdate(int currCharge)
        {
            labels.emoBatteryStatus(currCharge);
            textChange(this);
        }

        public void setGyro(int gyroX, int gyroY)
        {
            gyroMaxX = gyroX;
            gyroMaxY = gyroY;
        }

        public void getGyro(out int gyroX, out int gyroY)
        {
            gyroX = gyroMaxX;
            gyroY = gyroMaxY;
        }

        public UInt16 lastHeading
        {
            get { return _lastHeading; }
            set { _lastHeading = value; }
        }

        public void setLabelsClass(ISetLabels slb)
        {
            labels = slb;
        }

        public void setComboBoxLists(List<string> listForword, List<string> listBackword, List<string> listLeft, List<string> listRight, string[] chosen, string[] chosenExpName)
        {
            labels.setChosen(chosen);
            expChosenName = chosenExpName;
            comboBoxChange(new List<List<string>> {listForword, listBackword, listLeft, listRight}, chosen);
        }

        public string[] getChosenExp()
        {
            return expChosenName;
        }

        public void attach(IObserver obs)
        {
            textChange += new ModelTextHandler<Model>(obs.textChange);
            comboBoxChange += new ComboBoxChange<Model>(obs.comboBoxChange);
        }
    }
}
