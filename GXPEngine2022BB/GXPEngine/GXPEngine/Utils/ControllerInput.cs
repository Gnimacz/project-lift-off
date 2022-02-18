using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Threading;
using GXPEngine;
using GXPEngine.Core;
using System.IO.Ports;


namespace GXPEngine {
    public static class ControllerInput {
        static SerialPort serialPort;
        static bool _controllerConnected = false;

        public static int D2State = 0; // variable for reading the pushbutton status of D2
        public static int D3State = 0; // variable for reading the pushbutton status of D3
        public static int D4State = 0; // variable for reading the pushbutton status of D4
        public static int D5State = 0; // variable for reading the pushbutton status of D5
        public static int D6State = 0; // variable for reading the pushbutton status of D6
        public static int D7State = 0; // variable for reading the pushbutton status of D7
        public static int D8State = 0; // variable for reading the pushbutton status of D8
        public static int D12State = 0; // variable for reading the pushbutton status of D8
        public static int D13State = 0; // variable for reading the pushbutton status of D13
        public static int D9State = 0; // variable for reading the pushbutton status of D9
        static float _joystickX = 512;
        static float _joystickY = 512;
        static float _secondaryJoystickX = 512;
        static float _secondaryJoystickY = 512;

        public static void GetControllerState() {
            try {
                String[] ports = SerialPort.GetPortNames();
                if (ports == null) {
                    throw new Exception("Ports is null");
                }

                foreach (String port in ports) {
                    Console.WriteLine("found port: " + port);
                }

                serialPort = new SerialPort();
                serialPort.BaudRate = 9600;
                serialPort.PortName = ports[0];
                serialPort.Open();
                Console.WriteLine("port " + serialPort.PortName + " opened\nIf the controls don't work. Try switching the com port number at line 45 in ControllerInput.cs");

                if (serialPort.IsOpen) {
                    _controllerConnected = true;
                }

                while (_controllerConnected) {
                    string[] message = new string[15];
                    String a = "";
                    a = serialPort.ReadLine();
                    message = a.Split(' ');

                    D2State = int.Parse(message[0]); // variable for reading the pushbutton status of D2
                    D3State = int.Parse(message[1]); // variable for reading the pushbutton status of D3
                    D4State = int.Parse(message[2]); // variable for reading the pushbutton status of D4
                    D5State = int.Parse(message[3]); // variable for reading the pushbutton status of D5
                    D6State = int.Parse(message[4]); // variable for reading the pushbutton status of D6
                    D7State = int.Parse(message[5]); // variable for reading the pushbutton status of D7
                    D8State = int.Parse(message[6]); // variable for reading the pushbutton status of D8
                    D12State = int.Parse(message[7]); // variable for reading the pushbutton status of D8
                    D13State = int.Parse(message[8]);
                    D9State = int.Parse(message[9]);

                    _joystickX = float.Parse(message[10]) / 100f;
                    _joystickY = float.Parse(message[11]) / 100f;

                    _secondaryJoystickX = float.Parse(message[12]) / 100f;
                    _secondaryJoystickY = float.Parse(message[13]) / 100f;
                }
            }
            catch (Exception) {
                Console.WriteLine("No Device Connected!\nSwitching controls to keyboard mode\nIf there is an arduino connected, try changing the value on line 45 of the ControllerInput.cs script to either a 0, 1 or 2.");
                serialPort.Close();
                _controllerConnected = false;
                Console.WriteLine(_controllerConnected);
                return;
            }


            return;
        }

        public static void ClosePort() {
            serialPort.Close();
        }

        public static bool controllerConnected {
            get { return _controllerConnected; }
        }

        public static int Button2 {
            get { return D2State; }
        }

        public static int Button3 {
            get { return D3State; }
        }

        public static int Button4 {
            get { return D4State; }
        }

        public static int Button5 {
            get { return D5State; }
        }

        public static int Button6 {
            get { return D6State; }
        }

        public static int Button7 {
            get { return D7State; }
        }

        public static int Button8 {
            get { return D8State; }
        }

        public static int Button12 {
            get { return D12State; }
        }

        public static int Button13 {
            get { return D13State; }
        }

        public static int Button9 {
            get { return D9State; }
        }


        public static float joystickX {
            get { return _joystickX; }
        }

        public static float joystickY {
            get { return _joystickY; }
        }

        public static float secondaryJoystickX {
            get { return _secondaryJoystickX; }
        }

        public static float secondaryJoystickY {
            get { return _secondaryJoystickY; }
        }
    }
}