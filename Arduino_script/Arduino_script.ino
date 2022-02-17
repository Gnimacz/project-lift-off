

// constants won't change. They're used here to set pin numbers:
const int buttonPin = 4;     // the number of the pushbutton pin
const int ledPin =  12;      // the number of the LED pin

const int AnalogXPin1 = A4;
const int AnalogYPin1 = A5;
const int AnalogXPin2 = A6;
const int AnalogYPin2 = A7;
const int shootButtonL = 13;
const int shootButtonR = 9;

// variables will change:
int D2State = 0;             // variable for reading the pushbutton status of D2
int D3State = 0;             // variable for reading the pushbutton status of D3
int D4State = 0;             // variable for reading the pushbutton status of D4
int D5State = 0;             // variable for reading the pushbutton status of D5
int D6State = 0;             // variable for reading the pushbutton status of D6
int D7State = 0;             // variable for reading the pushbutton status of D7
int D8State = 0;             // variable for reading the pushbutton status of D8
int D12State = 0;            // variable for reading the pushbutton status of D12
int shootButtonLState = 0;    // variable for reading the pushbutton status of D13
int shootButtonRState = 0;    // variable for reading the pushbutton status of D9


float joystickX1 = 0;         //variable for reading the joystick X-axis
float joystickY1 = 0;         //variable for reading the joystick Y-axis
float joystickX2 = 0;         //variable for reading the joystick X-axis
float joystickY2 = 0;         //variable for reading the joystick Y-axis

const int numReadings = 10;

int readingsY1[numReadings];      // the readingsY from the analog input
int readIndexY1 = 0;              // the index of the current reading
int totalY1 = 0;                  // the running totalY
int averageY1 = 0;                // the averageY

int readingsX1[numReadings];      // the readingsY from the analog input
int readIndexX1 = 0;              // the index of the current reading
int totalX1 = 0;                  // the running totalY
int averageX1 = 0;                // the averageY

int readingsY2[numReadings];      // the readingsY from the analog input
int readIndexY2 = 0;              // the index of the current reading
int totalY2 = 0;                  // the running totalY
int averageY2 = 0;                // the averageY

int readingsX2[numReadings];      // the readingsY from the analog input
int readIndexX2 = 0;              // the index of the current reading
int totalX2 = 0;                  // the running totalY
int averageX2 = 0;                // the averageY



void setup() {
  // initialize the LED pin as an output:
  // pinMode(ledPin, OUTPUT);
  // initialize the pushbutton pin as an input:
  pinMode(2, INPUT);
  pinMode(3, INPUT);
  pinMode(4, INPUT);
  pinMode(5, INPUT);
  pinMode(6, INPUT);
  pinMode(7, INPUT);
  pinMode(8, INPUT);
  pinMode(12, INPUT_PULLUP);
  pinMode(shootButtonL, INPUT_PULLUP);
  pinMode(shootButtonR, INPUT_PULLUP);
  

  pinMode(AnalogXPin1, INPUT);
  pinMode(AnalogYPin1, INPUT);

  pinMode(AnalogXPin2, INPUT);
  pinMode(AnalogYPin2, INPUT);
    


}

void loop() {
 
  String s;


  int D2State = digitalRead(2);             //Read the pushbutton status of D2
  s.concat(D2State);                         //add the state of the pushbutton to the message for GXP
  s.concat(" ");                             //add a space so the code in GXP can split it
  
  int D3State = digitalRead(3);             //Read the pushbutton status of D3
  s.concat(D3State);                         //add the state of the pushbutton to the message for GXP
  s.concat(" ");                             //add a space so the code in GXP can split it

  int D4State = digitalRead(4);             //Read the pushbutton status of D4
  s.concat(D4State);                         //add the state of the pushbutton to the message for GXP
  s.concat(" ");                             //add a space so the code in GXP can split it
  
  int D5State = digitalRead(5);             //Read the pushbutton status of D5
  s.concat(D5State);                         //add the state of the pushbutton to the message for GXP
  s.concat(" ");                             //add a space so the code in GXP can split it
  
  int D6State = digitalRead(6);             //Read the pushbutton status of D6
  s.concat(D6State);                         //add the state of the pushbutton to the message for GXP
  s.concat(" ");                             //add a space so the code in GXP can split it
  
  int D7State = digitalRead(7);             //Read the pushbutton status of D7
  s.concat(D7State);                         //add the state of the pushbutton to the message for GXP
  s.concat(" ");                             //add a space so the code in GXP can split it
  
  int D8State = digitalRead(8);             //Read the pushbutton status of D8  
  s.concat(D8State);                         //add the state of the pushbutton to the message for GXP
  s.concat(" ");                             //add a space so the code in GXP can split it


  int D12State = digitalRead(12);             //Read the pushbutton status of D12  
  if(D12State == 1){                          //if statement for inverting the value of D12
    D12State = 0;
  }
  else{ D12State = 1; }
  s.concat(D12State);                         //add the state of the pushbutton to the message for GXP
  s.concat(" ");                             //add a space so the code in GXP can split it

  int shootButtonLState = digitalRead(shootButtonL);    //Read the pushbutton status of D13
  if (shootButtonLState == 1){                         //If statement to inverse it for parity sake
      shootButtonLState = 0;
    }
   else{
      shootButtonLState = 1;
    }
  s.concat(shootButtonLState);                //add the state of the pushbutton to the message for GXP
  s.concat(" ");                             //add a space so the code in GXP can split it

  int shootButtonRState = digitalRead(shootButtonR);    //Read the pushbutton status of D13
  if (shootButtonRState == 1){                         //If statement to inverse it for parity sake
      shootButtonRState = 0;
    }
   else{
      shootButtonRState = 1;
    }
  s.concat(shootButtonRState);                //add the state of the pushbutton to the message for GXP
  s.concat(" ");                             //add a space so the code in GXP can split it


  
  


  joystickX1 = AnalogSmoothX1(AnalogXPin1);
  s.concat(mapfloat(joystickX1, 0, 1024, 1, -1));
  s.concat(" ");
  

  joystickY1 = AnalogSmoothY1(AnalogYPin1);
  s.concat(mapfloat(joystickY1, 0, 1024, 1, -1));
  s.concat(" ");


  joystickX2 = analogRead(AnalogXPin2);//smoothX2(AnalogXPin2);
  s.concat(mapfloat(joystickX2, 0, 1024, 1, -1));
//s.concat(joystickX2);
  s.concat(" ");
  

  joystickY2 = analogRead(AnalogYPin2);//smoothY2(AnalogYPin2);
  s.concat(mapfloat(joystickY2, 0, 1024, 1, -1));
//  s.concat(joystickY2);

  Serial.println(s);
  s = "";

}

float mapfloat(float x, float in_min, float in_max, float out_min, float out_max)
{
  return (float)(x - in_min) * (out_max - out_min) / (float)(in_max - in_min) + out_min;
}

float AnalogSmoothX1(float Input){
  // subtract the last reading:
  totalX1 = totalX1 - readingsX1[readIndexX1];
  // read from the sensor:
  readingsX1[readIndexX1] = analogRead(Input);
  // add the reading to the totalX:
  totalX1 = totalX1 + readingsX1[readIndexX1];
  // advance to the next position in the array:
  readIndexX1 = readIndexX1 + 1;

  // if we're at the end of the array...
  if (readIndexX1 >= numReadings) {
    // ...wrap around to the beginning:
    readIndexX1 = 0;
  }

  // calculate the averageX:
  averageX1 = totalX1 / numReadings;
  return (float)averageX1;
}

float AnalogSmoothY1(float Input){
  // subtract the last reading:
  totalY1 = totalY1 - readingsY1[readIndexY1];
  // read from the sensor:
  readingsY1[readIndexY1] = analogRead(Input);
  // add the reading to the totalY:
  totalY1 = totalY1 + readingsY1[readIndexY1];
  // advance to the next position in the array:
  readIndexY1 = readIndexY1 + 1;

  // if we're at the end of the array...
  if (readIndexY1 >= numReadings) {
    // ...wrap around to the beginning:
    readIndexY1 = 0;
  }

  // calculate the averageY:
  averageY1 = totalY1 / numReadings;
  return (float)averageY1;
}

float smoothX2(float Input){
  // subtract the last reading:
  totalX2 = totalX2 - readingsX2[readIndexX2];
  // read from the sensor:
  readingsX2[readIndexX2] = analogRead(Input);
  // add the reading to the totalX:
  totalX2 = totalX2 + readingsX2[readIndexX2];
  // advance to the next position in the array:
  readIndexX2 = readIndexX2 + 1;

  // if we're at the end of the array...
  if (readIndexX2 >= numReadings) {
    // ...wrap around to the beginning:
    readIndexX2 = 0;
  }

  // calculate the averageX:
  averageX2 = totalX2 / numReadings;
  return (float)averageX2;
}

float smoothY2(float Input){
  // subtract the last reading:
  totalY2 = totalY2 - readingsY2[readIndexY2];
  // read from the sensor:
  readingsY2[readIndexY2] = analogRead(Input);
  // add the reading to the totalY:
  totalY2 = totalY2 + readingsY2[readIndexY2];
  // advance to the next position in the array:
  readIndexY2 = readIndexY2 + 1;

  // if we're at the end of the array...
  if (readIndexY2 >= numReadings) {
    // ...wrap around to the beginning:
    readIndexY2 = 0;
  }

  // calculate the averageY:
  averageY2 = totalY2 / numReadings;
  return (float)averageY2;
}
