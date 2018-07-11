#include <SPI.h>
#include <WiFi.h>
#include <LiquidCrystal.h>

char ssid[] = "java.lang.NullPointerException";     //  your network SSID (name) 
char pass[] = "ch1l@qu1l3s";    // your network password
int keyIndex = 0;            // your network key Index number (needed only for WEP)

//Maximum 10 messages of 32 characters. Display is 16x2
char msgQueue[320];
char lastCharReceived;
bool isRequestBody = false;
int i = 0;
int k = 0;

int status = WL_IDLE_STATUS;
// if you don't want to use DNS (and reduce your sketch size)
// use the numeric IP instead of the name for the server:
//IPAddress server(74,125,232,128);  // numeric IP for Google (no DNS)
char server[] = "academicos.com.mx";    // name address for Google (using DNS)

// Initialize the Ethernet client library
// with the IP address and port of the server 
// that you want to connect to (port 80 is default for HTTP):
WiFiClient client;

// initialize the library by associating any needed LCD interface pin
// with the arduino pin number it is connected to
const int rs = 12, en = 11, d4 = 5, d5 = 4, d6 = 3, d7 = 2;
LiquidCrystal lcd(rs, en, d4, d5, d6, d7);

void setup() {
  //Initialize serial and wait for port to open:
  Serial.begin(115200);

  // set up the LCD's number of columns and rows:
  lcd.begin(16, 2);
  printBanner();
  blankMsgQueue();
  
  // check for the presence of the shield:
  if (WiFi.status() == WL_NO_SHIELD) {
    Serial.println("WiFi shield not present");
    lcd.clear();
    lcd.println("No WiFi shield");
    // don't continue:
    while(true);
  } 
  
  // attempt to connect to Wifi network:
  while (status != WL_CONNECTED) { 
    Serial.print("Attempting to connect to SSID: ");
    lcd.clear();
    lcd.println("Connecting wifi...");
    Serial.println(ssid);
    // Connect to WPA/WPA2 network. Change this line if using open or WEP network:    
    status = WiFi.begin(ssid, pass);
    delay(5000);
  } 
  Serial.println("Connected to wifi");
  lcd.clear();
  lcd.println("Connected to wifi");
  printWifiStatus();
}

void loop() {
  printBanner();
  requestMessages();
  delay(1000);
  waitForResponse();
  displayMsgs();
  delay(5000);
}

void printBanner() {
  lcd.clear();
  lcd.print("Digital OnUs!!");
  //Point to the second row
  lcd.setCursor(0, 1);
  lcd.print("Fetching msg...");
}

void requestMessages() {
  Serial.println("\nStarting connection to server to fetch messages...");
  // if you get a connection, report back via serial:
  if (client.connect(server, 80)) {
    Serial.println("connected to server");
    // Make a HTTP request:
    client.println("GET /msg.txt HTTP/1.1");
    client.println("Host: academicos.com.mx");
    client.println("Connection: close");
    client.println();
  } else {
    Serial.println("ERROR connecting to server");
  }
}

void waitForResponse() {
  isRequestBody = false;
  lastCharReceived = 0;
  i = 0;
  while(true) {
    if (isRequestBody) {
      enqueueResponse();
    } else {
      removeHeaders();
    }

    //If client got disconnected stop the client
    if (!client.connected() && !client.available()) {
      client.stop();
      return;
    }
  }
}

void removeHeaders() {
  while (client.available()) {
    char c = client.read();
    Serial.write(c);
    if (lastCharReceived == '\n' && c == '\r') {
      //Headers are done
      isRequestBody = true;
      return;
      //Serial.println("**Separator of Header - Body**");
    }
    lastCharReceived = c;
  }
}

void enqueueResponse() {
  //From here to end is the body of the response
  while (client.available()) {
    char c = client.read();
    Serial.write(c);
    if (c != '\r' && c != '\n') {
      msgQueue[i] = c;
      i++;
    }
  }
}

void displayMsgs() {
  k = 0;
  while (true) {
    displayMsg();
    delay(3000);
    if (k >= 320 || msgQueue[k] == '\0') {
      break;
    }
  }
  
  blankMsgQueue();
}

void displayMsg() {
  char c;
  lcd.clear();
  
  //Get the name
  while (true) {
    c = msgQueue[k];
    k++;
    if (c == ':') {
      break;
    }
    lcd.print(c);
  }

  //Point to the second row
  lcd.setCursor(0, 1);

  //Get the msg txt
  while (true) {
    c = msgQueue[k];
    k++;
    if (c == ';' || c == '\0') {
      break;
    }
    lcd.print(c);
  }
}

void blankMsgQueue() {
  for (int j = 0; j < 320; j++) {
    msgQueue[i] = '\0';//null all
  }
}

void printWifiStatus() {
  // print the SSID of the network you're attached to:
  Serial.print("SSID: ");
  Serial.println(WiFi.SSID());

  // print your WiFi shield's IP address:
  IPAddress ip = WiFi.localIP();
  Serial.print("IP Address: ");
  Serial.println(ip);

  // print the received signal strength:
  long rssi = WiFi.RSSI();
  Serial.print("signal strength (RSSI):");
  Serial.print(rssi);
  Serial.println(" dBm");
}
