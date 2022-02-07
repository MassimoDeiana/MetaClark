#define joyX A0
#define joyY A1

void setup() {
  Serial.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
  int xValue = analogRead(joyX);
  int yValue = analogRead(joyY);

//  //print the values with to plot or view
//  Serial.print(xValue);

  if (xValue > 510) {
    Serial.println(1);
    delay(100);
  }
  if (xValue < 500) {
    Serial.println(0);
    delay(100);
  }


  //Serial.println(yValue);
}
