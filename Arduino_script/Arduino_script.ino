/*
  Button

  Turns on and off a light emitting diode(LED) connected to digital pin 13,
  when pressing a pushbutton attached to pin 2.

  The circuit:
  - LED attached from pin 13 to ground through 220 ohm resistor
  - pushbutton attached to pin 2 from +5V
  - 10K resistor attached to pin 2 from ground

  - Note: on most Arduinos there is already an LED on the board
    attached to pin 13.

  created 2005
  by DojoDave <http://www.0j0.org>
  modified 30 Aug 2011
  by Tom Igoe

  This example code is in the public domain.

  https://www.arduino.cc/en/Tutorial/BuiltInExamples/Button
*/

// constants won't change. They're used here to set pin numbers:
const int buttonPin = 4;     // the number of the pushbutton pin
const int ledPin =  12;      // the number of the LED pin

const int AnalogXPin = A1;
const int AnalogYPin = A0;

// variables will change:
int buttonState = 0;         // variable for reading the pushbutton status
float joystickX = 0;         //variable for reading the joystick X-axis
float joystickY = 0;         //variable for reading the joystick Y-axis

const int numReadings = 10;

int readingsY[numReadings];      // the readingsY from the analog input
int readIndexY = 0;              // the index of the current reading
int totalY = 0;                  // the running totalY
int averageY = 0;                // the averageY

int readingsX[numReadings];      // the readingsY from the analog input
int readIndexX = 0;              // the index of the current reading
int totalX = 0;                  // the running totalY
int averageX = 0;                // the averageY



void setup() {
  // initialize the LED pin as an output:
  pinMode(ledPin, OUTPUT);
  // initialize the pushbutton pin as an input:
  pinMode(buttonPin, INPUT);

  pinMode(AnalogXPin, INPUT);
  pinMode(AnalogYPin, INPUT);


}

void loop() {
 
  String s;

  // read the state of the pushbutton value:
  buttonState = digitalRead(buttonPin);

  // check if the pushbutton is pressed. If it is, the buttonState is HIGH:
  if (buttonState == HIGH) {
    // turn LED on:
    digitalWrite(ledPin, HIGH);
    // s.concat("B1");
  } else {
    digitalWrite(ledPin, LOW);
  }
  s.concat(buttonState);
  s.concat(" ");

  //joystickX = analogRead(AnalogXPin);
  joystickX = AnalogSmoothX(AnalogXPin);
  s.concat(mapfloat(joystickX, 0, 1024, -1, 1));
  s.concat(" ");
  
  //joystickY = analogRead(AnalogYPin);
  joystickY = AnalogSmoothY(AnalogYPin);
  s.concat(mapfloat(joystickY, 0, 1024, 1, -1));

  Serial.println(s);
  s = "";

}

float mapfloat(float x, float in_min, float in_max, float out_min, float out_max)
{
  return (float)(x - in_min) * (out_max - out_min) / (float)(in_max - in_min) + out_min;
}

float AnalogSmoothX(float Input){
  // subtract the last reading:
  totalX = totalX - readingsX[readIndexX];
  // read from the sensor:
  readingsX[readIndexX] = analogRead(Input);
  // add the reading to the totalX:
  totalX = totalX + readingsX[readIndexX];
  // advance to the next position in the array:
  readIndexX = readIndexX + 1;

  // if we're at the end of the array...
  if (readIndexX >= numReadings) {
    // ...wrap around to the beginning:
    readIndexX = 0;
  }

  // calculate the averageX:
  averageX = totalX / numReadings;
  return (float)averageX;
}

float AnalogSmoothY(float Input){
  // subtract the last reading:
  totalY = totalY - readingsY[readIndexY];
  // read from the sensor:
  readingsY[readIndexY] = analogRead(Input);
  // add the reading to the totalY:
  totalY = totalY + readingsY[readIndexY];
  // advance to the next position in the array:
  readIndexY = readIndexY + 1;

  // if we're at the end of the array...
  if (readIndexY >= numReadings) {
    // ...wrap around to the beginning:
    readIndexY = 0;
  }

  // calculate the averageY:
  averageY = totalY / numReadings;
  return (float)averageY;
}


