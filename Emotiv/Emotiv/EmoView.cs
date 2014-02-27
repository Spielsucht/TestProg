using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emotiv;
using System.Windows.Forms;
using System.Timers;

namespace Emotiv
{
    class EmoView
    {
        private static Profile profile;
        private EmoEngine engine;
        private uint userID;
        private IController coordinator;
        private static System.Timers.Timer emoTimer;
        private static bool Showed { get; set; } 

        public EmoView(IController coor)
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

            engine.Connect();

            emoTimer = new System.Timers.Timer();
            emoTimer.Interval = 5;
            emoTimer.Elapsed += new System.Timers.ElapsedEventHandler(onTimedEvent);
            emoTimer.Enabled = true;

            Showed = false;
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
            if (profile == null)
            {
                profile = EmoEngine.Instance.GetUserProfile(userID);
                profile.GetBytes();
            }
            Console.WriteLine("user added ({0})", e.userId);
            emoTimer.Interval = 5;
        }

        private void engine_UserRemoved(object sender, EmoEngineEventArgs e)
        {
            coordinator.setEmoDongleLabel("Dongle inaktiv");
            Console.WriteLine("user removed");
            emoTimer.Interval = 250;
            engine_EmoStateUpdated(this, new EmoStateUpdatedEventArgs(userID, new EmoState()));
        }

        private void engine_EmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            int x = 0, y = 0;
            int max = 0, curr = 0;
            EmoState es = e.emoState;
            EdkDll.EE_SignalStrength_t signal = es.GetWirelessSignalStatus();
            int headsetStatus = es.GetHeadsetOn();
            try
            {
                if (es.GetTimeFromStart() > 5 && signal != EdkDll.EE_SignalStrength_t.NO_SIGNAL)
                {
                    engine.HeadsetGetGyroDelta(userID, out x, out y);
                    es.GetBatteryChargeLevel(out curr, out max);
                    Console.WriteLine("X: " + x.ToString() + " Y: " + y.ToString() + "");
                }
                else if (signal == EdkDll.EE_SignalStrength_t.NO_SIGNAL)
                {
                    if (Showed == false)
                    {
                        Showed = true;
                        if (System.Windows.Forms.MessageBox.Show("Fehler","Es besteht keine Verbidnung mehr zu dem Headset.", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                            Showed = false;
                    }
                }
            }
            catch (EmoEngineException ex)
            {
                if (Showed == false)
                {
                    Showed = true;
                    if (System.Windows.Forms.MessageBox.Show(ex.Message, "Fehler beim Emotiv Headset.", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                        Showed = false;
                }
                
            }
            coordinator.headsetStatus(curr, max, x, y, headsetStatus);
        }

        private void engine_CognitivEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            EmoState es = e.emoState;
            coordinator.cognitivControl(es.CognitivGetCurrentAction());
        }

        private void engine_ExpressivEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            EmoState es = e.emoState;
            coordinator.expressivControl(es.ExpressivGetUpperFaceAction().ToString(),es.ExpressivGetLowerFaceAction().ToString());
        }

        public void stopRunning()
        {
            if (emoTimer.Enabled)
            {
                emoTimer.Stop();
                engine.Disconnect();
            }
        }

        private void onTimedEvent(object source, ElapsedEventArgs e)
        {
            emoTimer.Stop();
            engine.ProcessEvents();
            emoTimer.Enabled = true;
        }
    }
}
