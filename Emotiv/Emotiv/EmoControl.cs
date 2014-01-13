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
        private uint userID;
        private IController coordinator;

        public EmoControl(IController coor)
        {
            coordinator = coor;
            this.engine = EmoEngine.Instance;

            engine.EmoEngineConnected += new EmoEngine.EmoEngineConnectedEventHandler(engine_EmoEngineConnected);
            engine.EmoEngineDisconnected += new EmoEngine.EmoEngineDisconnectedEventHandler(engine_EmoEngineDisconnected);
            engine.UserAdded += new EmoEngine.UserAddedEventHandler(engine_UserAdded);
            engine.UserRemoved += new EmoEngine.UserRemovedEventHandler(engine_UserRemoved);

            engine.EmoStateUpdated += new EmoEngine.EmoStateUpdatedEventHandler(engine_EmoStateUpdated);
            engine.CognitivEmoStateUpdated += new EmoEngine.CognitivEmoStateUpdatedEventHandler(engine_CognitivEmoStateUpdated);
            engine.ExpressivEmoStateUpdated += new EmoEngine.ExpressivEmoStateUpdatedEventHandler(engine_ExpressivEmoStateUpdated);
        }

        public void onLoadProfile(string path)
        {
            engine.LoadUserProfile(userID, path);
            profile = engine.GetUserProfile(userID);
            engine.SetUserProfile(userID, profile);
            Console.WriteLine("Profil loaded");
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
            coordinator.setEmoDongleLabel("Dongle aktiv");
            userID = e.userId;
            profile = EmoEngine.Instance.GetUserProfile(userID); // Creates a new profile
            profile.GetBytes();
            Console.WriteLine("user added ({0})", e.userId);
        }
        private void engine_UserRemoved(object sender, EmoEngineEventArgs e)
        {
            coordinator.setEmoDongleLabel("Dongle inaktiv");
            Console.WriteLine("user removed");
        }

        private void engine_EmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            int x = 0, y = 0;
            int max = 0, curr = 0;
            EmoState es = e.emoState;
            EdkDll.EE_SignalStrength_t signal = es.GetWirelessSignalStatus();
            if (es.GetTimeFromStart() > 5 && signal != EdkDll.EE_SignalStrength_t.NO_SIGNAL)
            {
                engine.HeadsetGetGyroDelta(userID, out x, out y);
                es.GetBatteryChargeLevel(out curr, out max);
                Console.WriteLine("X: "+ x.ToString() + " Y: "+ y.ToString() +"");
                coordinator.headsetStatus(curr, max, x, y, 1);
            }
            else if (signal == EdkDll.EE_SignalStrength_t.NO_SIGNAL)
            {
                int headsetStatus = es.GetHeadsetOn();
                coordinator.headsetStatus(curr, max, x, y, 1);
            }
        }

        private void engine_CognitivEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            EmoState es = e.emoState;
            coordinator.cognitivControl(es.CognitivGetCurrentAction());
        }

        void engine_ExpressivEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            EmoState es = e.emoState;
            coordinator.expressivControl(es.ExpressivGetUpperFaceAction().ToString(),es.ExpressivGetLowerFaceAction().ToString());
        }

        public void stopRunning()
        {
            this.running = false;
        }

        public void run()
        {
            running = true;
            engine.Connect();
            EmoState es = new EmoState();
            coordinator.setEmoDongleLabel("Dongle inaktiv");
            while (running)
            {
                Thread.Sleep(1); // Lowers the CPU usaged by ~95% (or more)
                engine.ProcessEvents(1000);
            }
            engine.Disconnect();
        }
    }
}
