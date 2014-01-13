using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emotiv
{
    public interface IGetLabels
    {
        void getLabels(out string[] labels, out int[] values);
    }

    public interface ISetLabels
    {
        void setDongleStatusText(string text);
        void emoStatusText(string text);
        void spheroStatusText(string text);
        void spheroSpeedText(string speed);
        void emoBatteryStatus(int currCharge);
    }

    class Labels : IGetLabels, ISetLabels
    {
        private string dongleStatus;
        private string emoStatus;
        private string spheroStatus;
        private string speed;
        public int currCharge;

        public Labels()
        {
            currCharge = 0;
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
    }
}
