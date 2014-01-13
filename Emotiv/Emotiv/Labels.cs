using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emotiv
{
    public interface IGetLabels
    {
        string[] getLabels();
    }

    public interface ISetLabels
    {
        void setDongleStatusText(string text);
        void emoStatusText(string text);
        void spheroStatusText(string text);
        void spheroSpeedText(string speed);
    }

    class Labels : IGetLabels, ISetLabels
    {
        private string dongleStatus;
        private string emoStatus;
        private string spheroStatus;
        private string speed;
        public int currCharge;
        public int maxCharge;

        public Labels()
        {
            currCharge = 0;
            maxCharge = 0;
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

        public string[] getLabels()
        {
            return new string[] { dongleStatus, emoStatus, spheroStatus, speed};
        }
    }
}
