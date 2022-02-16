
int led = 13;
int cpt = 0;
String c;
String status;

int INA = 3;
int INB = 9;
int IN1 = 4;
int IN2 = 5;
int IN3 = 6;
int IN4 = 7;



void setup() {
  Serial.begin(9600);
  pinMode(led, OUTPUT);

  pinMode(INA, OUTPUT);
  pinMode(INB, OUTPUT);
  pinMode(IN1, OUTPUT);
  pinMode(IN2, OUTPUT);
  pinMode(IN3, OUTPUT);
  pinMode(IN4, OUTPUT);
}

void loop() {

  if(Serial.available() > 0)
  {
    c = Serial.readString();
    Serial.println(c);

    deplacement(c);

    Serial.print("status : ");
    Serial.println(status);
  }

}

void deplacement(String input)
{
    if(c == "HAUT\n" && status != "HAUT")
    {
        setDirectionHaut();
        status = "HAUT";
        Serial.println("commande HAUT");
        setDeplacementOn();
    }

    if(c == "BAS\n" && status != "BAS")
    {
        setDirectionBas();
        status = "BAS";
        Serial.println("commande BAS");
        setDeplacementOn();
    }

    if(c == "TOURNERGAUCHE\n" && status != "TOURNERGAUCHE")
    {
        setDirectionTournerGauche();
        status = "TOURNERGAUCHE";
        Serial.println("commande TOURNERGAUCHE");
        setDeplacementOn();
    }

    if(c == "TOURNERDROITE\n" && status != "TOURNERDROITE")
    {
        setDirectionTournerDroite();
        status = "TOURNERDROITE";
        Serial.println("commande TOURNERDROITE");
        setDeplacementOn();
    }

    if(status != "OFF" && input != "HAUT\n" && input != "BAS\n" && input != "TOURNERGAUCHE\n" && input != "TOURNERDROITE\n")
    {

        setDirectionOff();
        status = "OFF";
        Serial.println("Commande inconnue");
        setDeplacementOff();
    }
}



void setDirectionHaut(){
    digitalWrite(IN1, HIGH);
    digitalWrite(IN2, LOW);
    digitalWrite(IN3, HIGH);
    digitalWrite(IN4, LOW);
}

void setDirectionBas(){
    digitalWrite(IN1, LOW);
    digitalWrite(IN2, HIGH);
    digitalWrite(IN3, LOW);
    digitalWrite(IN4, HIGH);
}

void setDirectionTournerGauche(){
    digitalWrite(IN1, HIGH);
    digitalWrite(IN2, LOW);
    digitalWrite(IN3, LOW);
    digitalWrite(IN4, HIGH);
}

void setDirectionTournerDroite(){
    digitalWrite(IN1, LOW);
    digitalWrite(IN2, HIGH);
    digitalWrite(IN3, HIGH);
    digitalWrite(IN4, LOW);
}

void setDirectionOff(){
    digitalWrite(IN1, LOW);
    digitalWrite(IN2, LOW);
    digitalWrite(IN3, LOW);
    digitalWrite(IN4, LOW);
}


void setDeplacementOn(){
    digitalWrite(INA, HIGH);
    digitalWrite(INB, HIGH);
}

void setDeplacementOff(){
    digitalWrite(INA, LOW);
    digitalWrite(INB, LOW);
}
