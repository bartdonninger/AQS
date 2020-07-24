/*
  Reading.h - Class representing a reading
  sent to the AQS api.
*/
#ifndef Reading_h
#define Reading_h

#include "Arduino.h"

class Reading
{
  public:
    Reading(String deviceId, int type, String value);
  private:
    String _deviceId;
    int _type;
    String _value;
};

#endif Reading_h
