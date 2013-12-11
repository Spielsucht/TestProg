using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpheroNET;

namespace Emotiv
{
    class SpheroControl
    {

        private SpheroConnector spheroConnector;
        private Sphero sphero = null;

        public SpheroControl()
        {
            spheroConnector = new SpheroConnector();
        }

        public bool connectToBall()
        {
            spheroConnector.Scan();
            List<string> deviceNames = spheroConnector.DeviceNames;
            if (deviceNames.Contains("Sphero-GGW"))
            {
                Console.WriteLine("Sphero Connected");
                sphero = spheroConnector.Connect(deviceNames.IndexOf("Sphero-GGW"));
                return true;
            }
            else
            {
                return false;
            }
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

        public void disconnectFormBall()
        {
            Console.WriteLine("Sphero Disconnected");
            spheroConnector.Close();
        }

        public void backLED(bool state)
        {
            if (state)
                sphero.SetBackLED(1.0);
            else
                sphero.SetBackLED(0.0);
        }

        public void moveBall(UInt16 heading, float speed)
        {
            sphero.PerformMove(heading, speed);
        }

        public void sleep()
        {
            sphero.Sleep();
        }
    }
}
