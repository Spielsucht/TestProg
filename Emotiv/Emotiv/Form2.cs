using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emotiv;
using System.Threading;

namespace Emotiv
{
    public partial class Form2 : Form
    {
        private EmoEngine engine;

        public Form2()
        {
            engine = EmoEngine.Instance;
            InitializeComponent();
            initialEmoEventHandler(engine);
        }

        private void checkDongle()
        {
            engine.Connect();
            EmoState state = new EmoState();
        }

        private void checkHeadset()
        {

        }

        private void checkSignal()
        {

        }

        private void initialEmoEventHandler(EmoEngine engine)
        {
            engine.
        }
    }
}
