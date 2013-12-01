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
        private MainWindow window;

        public EmoControl(MainWindow window)
        {
            this.window = window;
            engine = EmoEngine.Instance;

            engine.EmoEngineConnected += new EmoEngine.EmoEngineConnectedEventHandler(engine_EmoEngineConnected);
            engine.EmoEngineDisconnected += new EmoEngine.EmoEngineDisconnectedEventHandler(engine_EmoEngineDisconnected);
            engine.UserAdded += new EmoEngine.UserAddedEventHandler(engine_UserAdded);
            engine.UserRemoved += new EmoEngine.UserRemovedEventHandler(engine_UserRemoved);
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
            Profile profile = EmoEngine.Instance.GetUserProfile(0);
            profile.GetBytes();
            window.setLabelText("Dongle activ");
        }
        private void engine_UserRemoved(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("user removed");
            window.setLabelText("Dongle removed");
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
                engine.ProcessEvents(1000);
            }
            engine.Disconnect();
        }
    }
}
