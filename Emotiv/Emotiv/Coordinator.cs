using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emotiv
{
    class Coordinator
    {
        private bool ctrSphero = false;
        private SpheroControler spheroControler;

        public Coordinator()
        {
            spheroControler = new SpheroControler();
        }

        public void connectToSphero()
        {
            if (spheroControler.connectToBall())
            {
                ctrSphero = true;
            }
        }

        public void move()
        {

        }

        ~Coordinator()
        {
            spheroControler.disconnectFormBall();
        }
    }
}
