using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace Emotiv
{
    public interface IController : IDisposable
    {
        void stopEmoControl();
        void connectToSphero();
        void loadEmoProfil(string path);
        void setHeading(UInt16 heading);
        void setEmoDongleLabel(string status);
        void setSpheroStatus(bool status);
        void setUserControl(string radioButtonName);
        void setSpeed(int value);
        void setBackLED(bool state);
        void expressivControl(string upper, string lower);
        void cognitivControl(EdkDll.EE_CognitivAction_t action);
        void keyControl(char key);
        void caliBall(int heading);
        void headsetStatus(int currCharge, int maxCharge, int gyroX, int gyroY, int headsetOn);
        void comboboxChange(ComboBox cb, string[] chosen, string[] list);
    }

    class Coordinator : IController
    {
        private SpheroModel spheroModel;
        private EmoView emoView;
        private IView window;
        private Model model;
        private Labels labels;

        public Coordinator(IView mw, Model mo)
        {
            labels = new Labels();
            window = mw;
            model = mo;
            emoView = new EmoView(this);
            mw.setCoordinatorLabels(this, (IGetLabels)labels);
            model.attach((IObserver)window);
            model.setLabelsClass((ISetLabels) labels);
        }

        public void stopEmoControl()
        {
            emoView.stopRunning();
        }

        public void loadEmoProfil(string path)
        {
            emoView.onLoadProfile(path);
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
            else
            {
                //spheroModel.connectToBall();
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
                    model.setGyro(0, 0);
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

                UInt16 heading = 0;
                if (iHeading <= 180)
                {
                    heading = (UInt16)(180 - iHeading);
                }
                else if (iHeading > 180 && iHeading < 360)
                {
                    heading = (UInt16)(359 + 180 - iHeading);
                }
                else
                {
                    heading = model.lastHeading;
                }
                model.lastHeading = heading;
                spheroModel.moveBall(speed, heading);
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
                string[] chosen = model.getChosenExp();
                foreach (string action in lowerAndUpper)
                {
                    if (action != "EXP_NEUTRAL")
                    {
                        if (action == chosen[0])
	                    {
		                     heading = 0;
	                    }
                        else if (action == chosen[1])
	                    {
		                     heading = 180;
	                    }
                        else if (action == chosen[2])
	                    {
		                     heading = 270;
	                    }
                        else if (action == chosen[3])
	                    {
		                     heading = 90;
	                    }
                        else
	                    {
                            heading = -1;
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
                if (maxCharge > 0 && currCharge >=0)
                {
                    charge = (currCharge * 100) / 5;
                }

                if (System.Math.Abs(maxX) > System.Math.Abs(maxY))
                {
                    if (maxX >= 200)
                    {
                        move("gyro", 90);
                    }
                    else if (maxX <= -200)
                    {
                        move("gyro", 270);
                    }
                    else
                    {
                        move("gyro", 360);
                    }
                }
                else if (System.Math.Abs(maxX) < System.Math.Abs(maxY))
                {
                    if (maxY >= 150)
                    {
                        move("gyro", 0);
                    }
                    else if (maxY <= -150)
                    {
                        move("gyro", 180);
                    }
                    else
                    {
                        move("gyro", 360);
                    }
                }
            }
            model.setEmoUpdate(charge);
            model.setGyro(maxX, maxY);
        }

        public void comboboxChange(ComboBox cb, string[] chosen, string[] list)
        {
            string chose = cb.SelectedItem.ToString();
            string[] chosenExpName = new string[4];
            switch (cb.Name)
            {
                case "cbForword":
                    chosen[0] = chose;
                    break;
                case "cbBackword":
                    chosen[1] = chose;
                    break;
                case "cbLeft":
                    chosen[2] = chose;
                    break;
                case "cbRight":
                    chosen[3] = chose;
                    break;
                default:
                    break;
            }
            List<string> cbForward = createList(0, chosen, list);
            List<string> cbBachword = createList(1, chosen, list);
            List<string> cbLeft = createList(2, chosen, list);
            List<string> cbRight = createList(3, chosen, list);

            for (int i = 0; i < chosen.Length; i++)
            {
                switch (chosen[i])
                {
                    case "Zwinkern": chosenExpName[i] = "EXP_BLINK"; break;
                    case "Blinzeln (links)":chosenExpName[i] = "EXP_WINK_LEFT"; break;
                    case "Blinzeln (rechts)": chosenExpName[i] = "EXP_WINK_RIGHT"; break;
                    case "Augenbrauen": chosenExpName[i] = "EXP_EYEBROW"; break;
                    case "Stirn Runzeln": chosenExpName[i] = "EXP_FURROW"; break;
                    case "Lächeln": chosenExpName[i] = "EXP_SMILE"; break;
                    case "Lachen": chosenExpName[i] = "EXP_LAUGH"; break;
                    case "Mundwinkel (links)": chosenExpName[i] = "EXP_SMIRK_LEFT"; break;
                    case "Mundwinkel (rechts)": chosenExpName[i] = "EXP_SMIRK_RIGHT"; break;
                    default: chosenExpName[i] = null; break;
                }
            }

            model.setComboBoxLists(cbForward, cbBachword, cbLeft, cbRight, chosen, chosenExpName);
        }

        private List<string> createList(int chosenIndex, string[] chosen, string[] list)
        {
            List<string> newlist = new List<string>();

            foreach (var item in list)
            {
                if (item != "keine")
                {
                    if (!chosen.Contains(item) || item == chosen[chosenIndex])
                    {
                        newlist.Add(item);
                    }
                }
                else
                {
                    newlist.Add("keine");
                }
            }
            return newlist;
        }

        public void Dispose()
        {
            if (model.getSpheroConStatus())
            {
                spheroModel.sleep();
            }
            emoView.stopRunning();
        }
    }
}
