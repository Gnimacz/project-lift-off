using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
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
        static float _joystickX = 512;
        static float _joystickY = 512;

        private static bool handshakeDone = false;
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
                    serialPort.PortName = ports[1];
                    serialPort.Open();
                    Console.WriteLine("port " + serialPort.PortName + " opened");

                while (serialPort.IsOpen)
                {
                    string[] message = new string[9];
                    String a = "";
                    a = serialPort.ReadLine();
                    message = a.Split(' ');
                    //Console.WriteLine(message[1]);

                    if (message[0] == "1")
                    {
                        buttonPressed = 1;
                        

                    }
                    if (buttonPressed == 1 && message[0] == "0") { buttonPressed = 0; }

                    _joystickX = float.Parse(message[1]) / 100f;
                    _joystickY = float.Parse(message[2]) / 100f;


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

        public static float joystickX
        {
            get { return _joystickX; }
        }
        public static float joystickY
        {
            get { return _joystickY; }
        }
    }

    
}
