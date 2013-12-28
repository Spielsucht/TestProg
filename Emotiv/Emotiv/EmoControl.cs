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
            //engine.ExpressivEmoStateUpdated += new EmoEngine.ExpressivEmoStateUpdatedEventHandler(engine_ExpressivEmoStateUpdated);
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
            
        }

        private void engine_CognitivEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            EmoState es = e.emoState;
            EdkDll.EE_CognitivAction_t currAction = es.CognitivGetCurrentAction();
            switch (currAction)
            {
                case EdkDll.EE_CognitivAction_t.COG_NEUTRAL:
                    Console.WriteLine("Neutral (Cog)");
                    coordinator.move("cog", 0, 360);
                    break;
                case EdkDll.EE_CognitivAction_t.COG_PUSH:
                    Console.WriteLine("Push");
                    coordinator.move("cog", (float)0.4, 180);
                    break;
                case EdkDll.EE_CognitivAction_t.COG_PULL:
                    Console.WriteLine("Pull");
                    coordinator.move("cog", (float)0.4, 0);
                    break;
                case EdkDll.EE_CognitivAction_t.COG_LIFT:
                    break;
                case EdkDll.EE_CognitivAction_t.COG_DROP:
                    break;
                case EdkDll.EE_CognitivAction_t.COG_LEFT:
                    Console.WriteLine("Left");
                    coordinator.move("cog", (float)0.4, 270);
                    break;
                case EdkDll.EE_CognitivAction_t.COG_RIGHT:
                    Console.WriteLine("Right");
                    coordinator.move("cog", (float)0.4, 90);
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

        void engine_ExpressivEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            EmoState es = e.emoState;
            EdkDll.EE_ExpressivAlgo_t currLowerAction = es.ExpressivGetLowerFaceAction();
            EdkDll.EE_ExpressivAlgo_t currUpperAction = es.ExpressivGetUpperFaceAction();
            Console.WriteLine(currLowerAction.ToString());
            Console.WriteLine(currUpperAction.ToString());
            switch (currLowerAction)
            {
                case EdkDll.EE_ExpressivAlgo_t.EXP_NEUTRAL:
                    Console.WriteLine("Neutral (Exp)");
                    coordinator.move("exp", (float)0.0, 360);
                    break;
                case EdkDll.EE_ExpressivAlgo_t.EXP_BLINK:
                    break;
                case EdkDll.EE_ExpressivAlgo_t.EXP_WINK_LEFT:
                    break;
                case EdkDll.EE_ExpressivAlgo_t.EXP_WINK_RIGHT:
                    break;
                case EdkDll.EE_ExpressivAlgo_t.EXP_HORIEYE:
                    break;
                case EdkDll.EE_ExpressivAlgo_t.EXP_EYEBROW:
                    break;
                case EdkDll.EE_ExpressivAlgo_t.EXP_FURROW:
                    break;
                case EdkDll.EE_ExpressivAlgo_t.EXP_SMILE:
                    break;
                case EdkDll.EE_ExpressivAlgo_t.EXP_CLENCH:
                    break;
                case EdkDll.EE_ExpressivAlgo_t.EXP_LAUGH:
                    break;
                case EdkDll.EE_ExpressivAlgo_t.EXP_SMIRK_LEFT:
                    break;
                case EdkDll.EE_ExpressivAlgo_t.EXP_SMIRK_RIGHT:
                    break;
                default:
                    break;
            }
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
