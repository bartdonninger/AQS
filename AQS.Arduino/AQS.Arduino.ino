#include <Arduino.h>
#include <Wire.h>
#include "Adafruit_SHT31.h"
#include "Adafruit_SGP30.h"
#include <RTCZero.h>
#include <TimeLib.h>
#include <WiFiNINA.h>
#include "arduino_secrets.h"
#include <ArduinoHttpClient.h>
#include <Reading.h>

Reading reading1("a", 0, "38");

// SHT31
Adafruit_SHT31 sht31 = Adafruit_SHT31();
float temperature(0.0);
float humidity(0.0);

// SGP30
Adafruit_SGP30 sgp30;
uint16_t tVOC(0);
uint16_t eCO2(0);


/* return absolute humidity [mg/m^3] with approximation formula
* @param temperature [°C]
* @param humidity [%RH]
*/
uint32_t getAbsoluteHumidity(float temperature, float humidity) {
    // approximation formula from Sensirion SGP30 Driver Integration chapter 3.15
    const float absoluteHumidity = 216.7f * ((humidity / 100.0f) * 6.112f * exp((17.62f * temperature) / (243.12f + temperature)) / (273.15f + temperature)); // [g/m^3]
    const uint32_t absoluteHumidityScaled = static_cast<uint32_t>(1000.0f * absoluteHumidity); // [mg/m^3]
    return absoluteHumidityScaled;
}


// RTC
RTCZero rtc;


// Enter SSID/password in the secret tab
char ssid[] = SECRET_SSID;
char pass[] = SECRET_PASS;
int status = WL_IDLE_STATUS;


// Multitasking
unsigned long previousMillisSensors(0);
unsigned long previousMillisBaseline(0);
const int intervalSensors(10000);
const int intervalBaseline(3600000);

// AQS Api
char serverAddress[] = "192.168.178.60";
int serverPort = 32769;
char aqsDeviceId[] = "3FA85F64-5717-4562-B3FC-2C963F66AFA6";

WiFiClient wifi;
HttpClient client = HttpClient(wifi, serverAddress, serverPort);

void setup() {
  Serial.begin(9600);
  
  // Connect to SHT31 sensor, set to 0x45 for alternate i2c address
  if (!sht31.begin(0x44)) { 
    Serial.println("SHT31 sensor not found");
    while (true) delay(1);
  }
  
  // Connect to SGP30 sensor
//  if (!sgp30.begin()){
//    Serial.println("SGP30 sensor not found");
//    while (true) delay(1);
//  }

  // If you have a baseline measurement from before you can assign it to start, to 'self-calibrate'
  //sgp30.setIAQBaseline(0x8E68, 0x8F41);
  
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
    // wait 10 seconds for connection:
    delay(5000);
  }
  
  // Initialize RTC
  rtc.begin();
  // Set the date and time
  unsigned long epoch(0);
  int numberOfTries = 0, maxTries = 5;
  while ((epoch == 0) && (numberOfTries < maxTries))
  {
    epoch = WiFi.getTime();
    numberOfTries++;
  }
  if (numberOfTries == maxTries) {
    Serial.print("NTP unreachable");
    while (true) delay(1);
  }
  else {
    // Add 2 hours for UTC+1 and DST
    epoch += 7200;
    rtc.setEpoch(epoch);
  }

  // Wait for serial connection for direct USB connection devices
  while (!Serial)
    delay(10);

  // print the SSID of the network you're attached to:
  Serial.print("SSID: ");
  Serial.println(WiFi.SSID());

  // print your WiFi shield's IP address:
  IPAddress ip = WiFi.localIP();
  Serial.print("IP Address: ");
  Serial.println(ip);
}

void loop() {
  // Get current millis() value
  unsigned long currentMillis = millis();
  
  // Determine if the interval has passed
  if (currentMillis - previousMillisSensors > intervalSensors )
  {
    // Save the current millis() value as the previous millis() value
    previousMillisSensors = currentMillis;

    readSht31Sensor();

    Serial.println("making POST request");
    String contentType = "application/json";
    String postData = "{\"deviceId\": \"3FA85F64-5717-4562-B3FC-2C963F66AFA6\", \"readingType\": 0, \"value\": \"38\"}";

    Serial.println(postData);
    
    client.post("/api/reading", contentType, postData);
    //client.get("/api/reading");

    // read the status code and body of the response
    int statusCode = client.responseStatusCode();
    String response = client.responseBody();
  
    Serial.print("Status code: ");
    Serial.println(statusCode);
    Serial.print("Response: ");
    Serial.println(response);

    Serial.println("Wait ten seconds");
    delay(10000);
  }
  
  // Determine if the interval has passed
//  if (currentMillis - previousMillisBaseline > intervalBaseline )
//  {
//    // Save the current millis() value as the previous millis() value
//    previousMillisBaseline = currentMillis;
//    
//    getSgp30Baseline();
//  }
  
//  Serial.print(temperature);
//  Serial.print(" ");
//  Serial.print(humidity);
//  Serial.print(" ");
//  Serial.print(tVOC);
//  Serial.print(" ");
//  Serial.print(eCO2);
  //Serial.print(" ");
  //Serial.println(intervalSensors);
}


void readSht31Sensor()
{
  temperature = sht31.readTemperature();
  humidity = sht31.readHumidity();
}


void readSgp30Sensor()
{
  // If you have a temperature / humidity sensor, you can set the absolute humidity to enable the humditiy compensation for the air quality signals
  sgp30.setHumidity(getAbsoluteHumidity(temperature, humidity));
  // Perform measurement
  sgp30.IAQmeasure();
  tVOC = sgp30.TVOC;
  eCO2 = sgp30.eCO2;
}


void getSgp30Baseline()
{
  uint16_t TVOC_base, eCO2_base;
  sgp30.getIAQBaseline(&eCO2_base, &TVOC_base);
}


void printDate()
{
  // Print date...
    Serial.println(" ");
    print2digits(rtc.getDay());
    Serial.print("/");
    print2digits(rtc.getMonth());
    Serial.print("/");
    print2digits(rtc.getYear());
    Serial.print(" ");
  
    // ...and time
    print2digits(rtc.getHours());
    Serial.print(":");
    print2digits(rtc.getMinutes());
    Serial.print(":");
    print2digits(rtc.getSeconds());
    Serial.println(" ");
}

void print2digits(int number) {
  if (number < 10) {
    Serial.print("0"); // print a 0 before if the number is < than 10
  }
  Serial.print(number);
}
