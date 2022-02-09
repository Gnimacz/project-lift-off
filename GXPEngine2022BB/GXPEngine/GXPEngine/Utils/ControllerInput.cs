using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GXPEngine;
using GXPEngine.Core;
using System.IO.Ports;



namespace GXPEngine
{
    public static class ControllerInput
    {
        static SerialPort serialPort;

        public static int buttonPressed = 0;


        public static void GetControllerState()
        {
            try
            {
                String[] ports = SerialPort.GetPortNames();
                if (ports == null)
                {
                    throw new Exception("Ports is null");
                }
                foreach (String port in ports)
                {
                    Console.WriteLine("found port: " + port);

                }

                serialPort = new SerialPort();
                serialPort.BaudRate = 9600;
                serialPort.PortName = ports[0];
                serialPort.Open();

                while (serialPort.IsOpen)
                {
                    String a = serialPort.ReadExisting();
                    if (a == "1")
                    {
                        buttonPressed = 1;
                        Thread.Sleep(10);

                    }
                    if (buttonPressed == 1 && a == "1") { buttonPressed = 0; }


                }
            }
            catch (Exception)
            {
                Console.WriteLine("No Device Connected!\nPlease connect your arduino controller");
                serialPort.Close();
                return;
            }
            

            
            return;
            
            
        }

        public static void ClosePort()
        {
            serialPort.Close();
        }
    }

    
}
