#include <Arduino.h>
#include <Wire.h>
#include <WiFiNINA.h>
#include <ArduinoHttpClient.h>
#include "arduino_secrets.h"


// Enter SSID/password in the secret tab
char ssid[] = SECRET_SSID;
char pass[] = SECRET_PASS;

// Server details
char server[] = "jsonplaceholder.typicode.com";
int port = 80;

// Initialize the Ethernet client library
WiFiClient wifi;
HttpClient client = HttpClient(wifi, server, port);
int status = WL_IDLE_STATUS;

void setup() {
  Serial.begin(9600);
  
  // Check WiFi module status
  if (WiFi.status() == WL_NO_MODULE) {
    Serial.println("WiFi module not found");
    while (true) delay(1);
  }
  
  // Check WiFi module firmware version
  if (WiFi.firmwareVersion() < WIFI_FIRMWARE_LATEST_VERSION) {
    Serial.println("Please upgrade the firmware");
  }

  // Attempt to connect to Wifi network:
  while (status != WL_CONNECTED) {
    // Connect to WPA/WPA2 network:
    status = WiFi.begin(ssid, pass);
  }

  // Print SSID
  Serial.print("Connected to ");
  Serial.println(WiFi.SSID());

  // print IP
  Serial.print("IP Address is ");
  Serial.println(WiFi.localIP());
}

void loop() {
  Serial.println("making POST request");
  String postData = "{\"title\": \"foo\",\"body\": \"bar\",\"userId\": 1}";

  client.beginRequest();
  client.post("/posts/");
  client.sendHeader("Content-Type", "application/json; charset=UTF-8");
  client.beginBody();
  client.print(postData);
  client.endRequest();

  // read the status code and body of the response
  int statusCode = client.responseStatusCode();
  String response = client.responseBody();

  Serial.print("Status code: ");
  Serial.println(statusCode);
  Serial.print("Response: ");
  Serial.println(response);

  Serial.println("Wait five seconds");
  delay(5000);
}
