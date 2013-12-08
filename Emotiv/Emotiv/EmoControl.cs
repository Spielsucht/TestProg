using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emotiv;
using System.Windows.Forms;
using System.Threading;

namespace Emotiv
{
    class EmoControl
    {
        static Profile profile;
        private bool running = true;
        private EmoEngine engine;
        private IntPtr es;
        private uint userID;
        private MainWindow window;
        private Coordinator coordinator;


        public EmoControl(MainWindow window)
        {
            this.window = window;
            this.engine = EmoEngine.Instance;
            this.es = EdkDll.ES_Create();
            this.coordinator = new Coordinator();

            window.onLoadProfile += new MainWindow.loadProfile(window_onLoadProfile);

            engine.EmoEngineConnected += new EmoEngine.EmoEngineConnectedEventHandler(engine_EmoEngineConnected);
            engine.EmoEngineDisconnected += new EmoEngine.EmoEngineDisconnectedEventHandler(engine_EmoEngineDisconnected);
            engine.UserAdded += new EmoEngine.UserAddedEventHandler(engine_UserAdded);
            engine.UserRemoved += new EmoEngine.UserRemovedEventHandler(engine_UserRemoved);

            engine.EmoStateUpdated += new EmoEngine.EmoStateUpdatedEventHandler(engine_EmoStateUpdated);
            engine.CognitivEmoStateUpdated += new EmoEngine.CognitivEmoStateUpdatedEventHandler(engine_CognitivEmoStateUpdated);
        }

        private void window_onLoadProfile(string path)
        {
            try
            {
                engine.LoadUserProfile(userID, path);
                profile = engine.GetUserProfile(userID);
                engine.SetUserProfile(userID, profile);
            }
            catch (EmoEngineException e)
            {
                MessageBox.Show(e.Message + "\nBitte Emotiv Dongle überprüfen",
                    "Fehler beim Profilladen",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,
                    "Fehler",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void engine_EmoEngineDisconnected(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("Disconnected");
        }
        private void engine_EmoEngineConnected(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("Connected");
        }

        private void engine_UserAdded(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("user added ({0})", e.userId);
            userID = e.userId;
            profile = EmoEngine.Instance.GetUserProfile(userID); // Creates a new profile
            profile.GetBytes();
            window.setLabelText("Dongle activ");
        }
        private void engine_UserRemoved(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("user removed");
            window.setLabelText("Dongle removed");
        }

        private void engine_CognitivEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            EmoState es = e.emoState;
            EdkDll.EE_CognitivAction_t currAction = es.CognitivGetCurrentAction();
            switch (currAction)
            {
                case EdkDll.EE_CognitivAction_t.COG_NEUTRAL:
                    Console.WriteLine("Neutral");
                    break;
                case EdkDll.EE_CognitivAction_t.COG_PUSH:
                    Console.WriteLine("Push");
                    break;
                case EdkDll.EE_CognitivAction_t.COG_PULL:
                    Console.WriteLine("Pull");
                    break;
                case EdkDll.EE_CognitivAction_t.COG_LIFT:
                    break;
                case EdkDll.EE_CognitivAction_t.COG_DROP:
                    break;
                case EdkDll.EE_CognitivAction_t.COG_LEFT:
                    Console.WriteLine("Left");
                    break;
                case EdkDll.EE_CognitivAction_t.COG_RIGHT:
                    Console.WriteLine("Right");
                    break;
                case EdkDll.EE_CognitivAction_t.COG_ROTATE_LEFT:
                    break;
                case EdkDll.EE_CognitivAction_t.COG_ROTATE_RIGHT:
                    break;
                case EdkDll.EE_CognitivAction_t.COG_ROTATE_CLOCKWISE:
                    break;
                case EdkDll.EE_CognitivAction_t.COG_ROTATE_COUNTER_CLOCKWISE:
                    break;
                case EdkDll.EE_CognitivAction_t.COG_ROTATE_FORWARDS:
                    break;
                case EdkDll.EE_CognitivAction_t.COG_ROTATE_REVERSE:
                    break;
                case EdkDll.EE_CognitivAction_t.COG_DISAPPEAR:
                    break;
                default:
                    break;
            }
        }
        private void engine_EmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {            
            int currCharge;
            int maxCharge;
            EdkDll.ES_GetBatteryChargeLevel(this.es ,out currCharge, out maxCharge);
            Console.WriteLine("CurrCharge {0}, MaxCharge {1}", currCharge, maxCharge);
        }

        public void stopRunning()
        {
            this.running = false;
        }

        public void run()
        {
            window.setLabelText("Dongle removed");
            running = true;
            engine.Connect();
            while (running)
            {
                Thread.Sleep(1); // Lowers the CPU usaged by ~95% (or more)
                engine.ProcessEvents(1000);
            }
            engine.Disconnect();
        }
    }
}
