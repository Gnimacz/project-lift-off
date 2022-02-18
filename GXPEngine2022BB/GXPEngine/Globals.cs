using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

namespace GXPEngine
{
    public static class Globals
    {
        static Globals() { }

        private static bool reset;

        public static bool shouldReset
        {
            get { return reset; }
            set { reset = value; }
        }

    }
}
