using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDateTime
{
    [SerializeField]
    long ticks;
    bool initialized;
    DateTime dateTime;

    public DateTime DateTime
    {
        get
        {
            if (!initialized)
            {
                dateTime = new DateTime(ticks);
                initialized = true;
            }
            
            return dateTime;
        }
        set
        {
            ticks = value.Ticks;
            dateTime = value;

            initialized = true;
        }
    }

    public static TimeSpan operator -(DateTime dateTime1, SerializableDateTime dateTime2)
    {
        return dateTime1 - dateTime2.dateTime;
    }

    public SerializableDateTime(DateTime dateTime)
    {
        ticks = dateTime.Ticks;
        this.dateTime = dateTime;

        initialized = true;
    }
}
