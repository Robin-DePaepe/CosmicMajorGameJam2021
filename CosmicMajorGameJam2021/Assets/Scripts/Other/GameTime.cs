using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameTime //time object for in game events, to be set in game standard time
{
    public int hours;
    public int minutes;
    public float seconds;

    public void convert()
    {
        if (seconds >= 60)
        {
            int minAdd = Mathf.RoundToInt(seconds) / 60;
            minutes += minAdd;
            seconds -=minAdd*60;
        }
        if (minutes >= 60)
        {
            int hourAdd = minutes / 60;
            hours += hourAdd;
            minutes -=hourAdd*60;
        }
        
    }
    public GameTime()
    {
        seconds = 0;
        hours = 0;
        minutes = 0;
    }

    public GameTime(float s, int m = 0, int h = 0)
    {
        seconds = s;
        hours = h;
        minutes = m;
        convert();
    }

    public static GameTime operator +(GameTime gt1, GameTime gt2)
    {
        GameTime result=new GameTime(gt1.seconds + gt2.seconds, gt1.minutes + gt2.minutes, gt1.hours + gt2.hours);
        result.convert();
        return result;

    }

    public static GameTime operator -(GameTime gt1,GameTime gt2)=> new GameTime(Mathf.Abs(gt1.seconds - gt2.seconds), Mathf.Abs(gt1.minutes - gt2.minutes), 
        Mathf.Abs(gt1.hours - gt2.hours));

    public static bool operator >=(GameTime gt1, GameTime gt2)
    {
        if (gt1 != null && gt2 != null)
        {
            return (gt1.OnlySeconds() >= gt2.OnlySeconds());
        }

        return false;
    }
    
    public static bool operator<=(GameTime gt1, GameTime gt2)
    {
        if (gt1 != null && gt2 != null)
        {
            
            return  (gt1.OnlySeconds() <= gt2.OnlySeconds());
            
        }
        return false;
    }

    public int OnlySeconds()
    {
        return hours * 3600 + minutes * 60 + (int)seconds;
    }

    public void addDeltaTime()
    {
        seconds += TimeManager.main.ConvertRealTimeToGameTime(Time.deltaTime);
        convert();
    }

}
