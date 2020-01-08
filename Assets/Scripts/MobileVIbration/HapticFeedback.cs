using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
public enum UIFeedbackType{
    Selection = 0,
    ImpactLight,
    ImpactMedium,
    ImpactHeavy,
    Success,
    Warning,
    Error
}
public class HapticFeedback 
{
    public static IVibrator vibrator;

    public static void Generate(UIFeedbackType type)
    {
        if(vibrator==null)
        {
            if(RuntimePlatform.Android==Application.platform)
            {
                vibrator = new IAndroidVibrator();
            }
            else if(RuntimePlatform.IPhonePlayer == Application.platform)
            {
                vibrator = new IPhoneVibrator();
            }
            else
            {
                Debug.Log("You are in the editor.");
                return;
            }
        }

        vibrator.Vibrate(type);
    }
}




public interface IVibrator
{
    void Vibrate(UIFeedbackType type);
}



public class IPhoneVibrator:IVibrator
{
    public void Vibrate(UIFeedbackType type)
    {
        GenerateFeedback((int)type);
    }
    [DllImport("__Internal")]
    private static extern void GenerateFeedback(int type);
}

public class IAndroidVibrator:IVibrator
{
    public void Vibrate(UIFeedbackType type)
    {
        switch(type)
        {
            case UIFeedbackType.ImpactLight:
                AndroidVibrationPlugin.Vibrate(10, 160);
                break;
            case UIFeedbackType.ImpactMedium:
                AndroidVibrationPlugin.Vibrate(10, 210);
                break;
            case UIFeedbackType.ImpactHeavy:
                AndroidVibrationPlugin.Vibrate(10, 255);
                break;
        }
    }
}
