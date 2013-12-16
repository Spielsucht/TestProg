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

        private void engine_CognitivEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            EmoState es = e.emoState;
            EdkDll.EE_CognitivAction_t currAction = es.CognitivGetCurrentAction();
            switch (currAction)
            {
                case EdkDll.EE_CognitivAction_t.COG_NEUTRAL:
                    Console.WriteLine("Neutral");
                    coordinator.move(this, 0, 360);
                    break;
                case EdkDll.EE_CognitivAction_t.COG_PUSH:
                    Console.WriteLine("Push");
                    coordinator.move(this, (float)0.3, 0);
                    break;
                case EdkDll.EE_CognitivAction_t.COG_PULL:
                    Console.WriteLine("Pull");
                    coordinator.move(this, (float)0.3, 180);
                    break;
                case EdkDll.EE_CognitivAction_t.COG_LIFT:
                    break;
                case EdkDll.EE_CognitivAction_t.COG_DROP:
                    break;
                case EdkDll.EE_CognitivAction_t.COG_LEFT:
                    Console.WriteLine("Left");
                    coordinator.move(this, (float)0.3, 90);
                    break;
                case EdkDll.EE_CognitivAction_t.COG_RIGHT:
                    Console.WriteLine("Right");
                    coordinator.move(this, (float)0.3, 270);
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
