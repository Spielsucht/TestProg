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
        void setSpeed(int value);
        void expressivControl(string upper, string lower);
        void cognitivControl(EdkDll.EE_CognitivAction_t action);
        void keyControl(char key);
        void caliBall(int heading);
        void headsetStatus(int currCharge, int maxCharge, int gyroX, int gyroY, int headsetOn);
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

        public void setUserControl(string controlName)
        {
            switch (controlName)
            {
                case "Gedanken":
                    model.setUserControl("cog");
                    break;
                case "Mimik":
                    model.setUserControl("exp");
                    break;
                case "Tastatur":
                    model.setUserControl("key");
                    break;
                case "Gyroskop":
                    model.setUserControl("gyro");
                    // set gyro null
                    break;
                case "cali":
                    model.setUserControl("cali");
                    break;
                default:
                    model.setUserControl(null);
                    break;
            }
        }

        private void move(string sender, int iHeading)
        {
            float speed;
            if (sender == model.getUserControl() && model.getSpheroConStatus())
            {
                if (sender == "cali" || iHeading == 360)
                {
                    speed = 0.0f;
                }
                else
                {
                    speed = model.getSpeed();
                }
                spheroModel.moveBall(speed, iHeading);
            }
        }

        public void setSpeed(int value)
        {
            float fspeed = (float)value / 10;
            string sspeed = (value * 10).ToString();
            model.setSpheroSpeed(sspeed, fspeed);
        }

        public void keyControl(char key)
        {
            int heading = 360;
            switch (key)
            {
                case 'w':
                    heading = 180;
                    break;
                case 's':
                    heading = 0;
                    break;
                case 'a':
                    heading = 270;
                    break;
                case 'd':
                    heading = 90;
                    break;
                default:
                    heading = 360;
                    break;
            }
            move("key", heading);
        }

        public void caliBall(int heading)
        {
            move("cali", heading);
        }

        public void cognitivControl(EdkDll.EE_CognitivAction_t action)
        {
            int heading = 360;
            switch (action)
            {
                case EdkDll.EE_CognitivAction_t.COG_NEUTRAL:
                    Console.WriteLine("Neutral (Cog)");
                    heading = 360;
                    break;
                case EdkDll.EE_CognitivAction_t.COG_PUSH:
                    Console.WriteLine("Push");
                    heading = 180;
                    break;
                case EdkDll.EE_CognitivAction_t.COG_PULL:
                    Console.WriteLine("Pull");
                    heading = 0;
                    break;
                case EdkDll.EE_CognitivAction_t.COG_LEFT:
                    Console.WriteLine("Left");
                    heading = 270;
                    break;
                case EdkDll.EE_CognitivAction_t.COG_RIGHT:
                    Console.WriteLine("Right");
                    heading = 90;
                    break;
                default:
                    heading = -1;
                    break;
            }
            if (heading >= 0)
            {
                move("cog", heading);
            }
            
        }

        public void expressivControl (string upper, string lower)
        {
            int heading = 360;
            string[] lowerAndUpper = { lower, upper };
            if (lowerAndUpper[0] == lowerAndUpper[1])
            {
                heading = 360;
            }
            else if (lowerAndUpper.Count(action => action.Contains("EXP_NEUTRAL") == true) == 1)
            {
                foreach (string action in lowerAndUpper)
                {
                    if (action != "EXP_NEUTRAL")
                    {
                        switch (action)
                        {
                            case "EXP_EYEBROW":
                                heading = 0;
                                break;
                            case "EXP_SMILE":
                                heading = 180;
                                break;
                            case "EXP_SMIRK_LEFT":
                                heading = 270;
                                break;
                            case "EXP_SMIRK_RIGHT":
                                heading = 90;
                                break;
                            default:
                                heading = -1;
                                break;
                        } 
                    }
                }
            }
            else
            {
                heading = 360;
            }
            if (heading >= 0)
            {
                Console.WriteLine(upper + " , " + lower + " , " + heading.ToString());
                move("exp", heading);
            }
        }

        public void headsetStatus(int currCharge, int maxCharge, int gyroX, int gyroY, int headsetOn)
        {
            int charge = 0;
            int maxX = 0;
            int maxY = 0;
            if (headsetOn > 0)
            {
                model.getGyro(out maxX, out maxY);
                maxX += gyroX;
                maxY += gyroY;
                if (maxCharge > 0)
                {
                    charge = (currCharge * 100) / 5;
                }
            }
            model.setEmoUpdate(charge, maxX, maxY);
        }

        public void setGyroValues(int x, int y)
        {
            
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
