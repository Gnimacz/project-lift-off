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

        public static int D2State = 0;             // variable for reading the pushbutton status of D2
        public static int D3State = 0;             // variable for reading the pushbutton status of D3
        public static int D4State = 0;             // variable for reading the pushbutton status of D4
        public static int D5State = 0;             // variable for reading the pushbutton status of D5
        public static int D6State = 0;             // variable for reading the pushbutton status of D6
        public static int D7State = 0;             // variable for reading the pushbutton status of D7
        public static int D8State = 0;             // variable for reading the pushbutton status of D8
        static float _joystickX = 512;
        static float _joystickY = 512;

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
                    string[] message = new string[10];
                    String a = "";
                    a = serialPort.ReadLine();
                    message = a.Split(' ');

                    D2State = int.Parse(message[0]);             // variable for reading the pushbutton status of D2
                    D3State = int.Parse(message[1]);             // variable for reading the pushbutton status of D3
                    D4State = int.Parse(message[2]);             // variable for reading the pushbutton status of D4
                    D5State = int.Parse(message[3]);             // variable for reading the pushbutton status of D5
                    D6State = int.Parse(message[4]);             // variable for reading the pushbutton status of D6
                    D7State = int.Parse(message[5]);             // variable for reading the pushbutton status of D7
                    D8State = int.Parse(message[6]);             // variable for reading the pushbutton status of D8

                    _joystickX = float.Parse(message[7]) / 100f;
                    _joystickY = float.Parse(message[8]) / 100f;



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

        public static int Button2
        {
            get { return D2State; }
        }
        public static int Button3
        {
            get { return D3State; }
        }
        public static int Button4
        {
            get { return D4State; }
        }
        public static int Button5
        {
            get { return D5State; }
        }
        public static int Button6
        {
            get { return D6State; }
        }
        public static int Button7
        {
            get { return D7State; }
        }
        public static int Button8
        {
            get { return D8State; }
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
