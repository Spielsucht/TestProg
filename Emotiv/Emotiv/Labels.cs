using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emotiv
{
    public interface IGetLabels
    {
        void getLabels(out string[] labels, out int[] values);
        string[] getComboboxChosen();
        string[] getComboboxList();
    }

    public interface ISetLabels
    {
        void setDongleStatusText(string text);
        void emoStatusText(string text);
        void spheroStatusText(string text);
        void spheroSpeedText(string speed);
        void emoBatteryStatus(int currCharge);
        void setChosen(string[] chosen);
    }

    class Labels : IGetLabels, ISetLabels
    {
        private string dongleStatus;
        private string emoStatus;
        private string spheroStatus;
        private string speed;
        private string[] expList;
        private string[] expChosen;
        private int currCharge;

        public Labels()
        {
            currCharge = 0;
            expChosen = new string[]{ "keine", "keine", "keine", "keine" };
            expList = new string[] { "keine", "Zwinkern", "Blinzeln (links)", "Blinzeln (rechts)", "Augenbrauen", "Stirn Runzeln", "Lächeln", "Lachen", "Mundwinkel (links)", "Mundwinkel (rechts)" };
        }

        public void setDongleStatusText(string text)
        {
            dongleStatus = text;
        }

        public void emoStatusText(string text)
        {
            emoStatus = text;
        }

        public void spheroStatusText(string text)
        {
            spheroStatus = text;
        }

        public void spheroSpeedText(string number)
        {
            speed = number;
        }

        public void emoBatteryStatus(int currCharge)
        {
            this.currCharge = currCharge;
        }

        public void getLabels(out string[] labels, out int[] values)
        {
            labels = new string[] { dongleStatus, emoStatus, spheroStatus, speed};
            values = new int[] { currCharge };
        }

        public string[] getComboboxChosen()
        {
            return expChosen;
        }

        public string[] getComboboxList()
        {
            return expList;
        }

        public void setChosen(string[] chosen)
        {
            expChosen = chosen;
        }
    }
}
