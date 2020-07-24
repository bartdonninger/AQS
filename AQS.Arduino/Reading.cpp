/*
  Reading.cpp - Class representing a reading
  sent to the AQS api.
*/

#include "Arduino.h"
#include "Reading.h"

Reading::Reading(String deviceId, int type, String value)
{
  _deviceId = deviceId;
  _type = type;
  _value = value;
}
