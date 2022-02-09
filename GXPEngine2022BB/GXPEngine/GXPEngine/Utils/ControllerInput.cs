using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using SharpDX.XInput;
using SharpDX.DirectInput;
using SharpDX.IO;
using SharpDX;
using XInput;
using System.IO.Ports;


namespace GXPEngine
{
    public static class ControllerInput
    {


        public static void GetControllerState()
        {
            String[] ports = SerialPort.GetPortNames();
            Console.WriteLine(ports.Length);
            foreach (String port in ports)
            {
                Console.WriteLine("found port: " + port);

            }

            return;
            
            
        }
    }

    
}
