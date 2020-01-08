using System;
using UnityEngine;
using UnityEngine.UI;
using System.Text;


#region EXTENSION_CLASS
public static class Extension
{
    #region TEMP_VARIABLE_HOLDER
    static Color _tempColor = new Color();
    #endregion

    #region EXTENSION_METHODS
    /// <summary>
    /// Return false if value is zero, else Return true.
    /// </summary>
    public static bool BoolChecker(this int val)
    {
        Predicate<int> predicate = ans => ans > 0;
        return predicate(val);
    }

    /// <summary>
    /// Round-Off Vector3 to Int.
    /// </summary>
    public static Vector3 RoundOffVector(this Vector3 vector3)
    {
        vector3.x = Mathf.RoundToInt(vector3.x);
        vector3.y = Mathf.RoundToInt(vector3.y);
        vector3.z = Mathf.RoundToInt(vector3.z);
        return vector3;
    }

    /// <summary>
    /// Round-Off Vector2 to Int.
    /// </summary>
    public static Vector2 RoundOffVector(this Vector2 vector2)
    {
        vector2.x = Mathf.RoundToInt(vector2.x);
        vector2.y = Mathf.RoundToInt(vector2.y);
        return vector2;
    }

    /// <summary>
    /// Return value between in new scale.
    /// </summary>
    public static float MapInNewScale(this float value, float oldMin, float oldMax, float newMin, float newMax)
    {
        return (newMax - newMin) * ((value - oldMin) / (oldMax - oldMin)) + newMin;
    }

    /// <summary>
    /// Set alpha value between 0 to 1.
    /// </summary>
    public static void SetAlphaValue(this Image image, float alphaValue)
    {
        _tempColor = image.color;
        _tempColor.a = alphaValue;
        image.color = _tempColor;
    }

    /// <summary>
    /// Set alpha value to 0.
    /// </summary>
    public static void SetAlphaValueZero(this Image image) => image.SetAlphaValue(0);

    /// <summary>
    /// Set alpha value to 1.
    /// </summary>
    public static void SetAlphaValueOne(this Image image) => image.SetAlphaValue(1);

    /// <summary>
    /// Return Bounds of an Ortographic Camera
    /// </summary>
    public static Bounds OrthographicBounds(this Camera camera)
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(camera.transform.position, new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }

    /// <summary>
    /// Convert Texture into Sprite.
    /// </summary>
    public static Sprite ConvertTexToSprite(this Texture2D tex)
    {
        return Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

    /// <summary>
    /// Make Data of API into format of JSON.
    /// </summary>
    public static string FormattingForAPI(this string response, string additionStr)
    {
        string tempHolder = "{\"" + additionStr + "\":" + response + "}";
        return tempHolder;
    }

    /// <summary>
    /// Encode the plain text into Base64.
    /// </summary>
    public static string Base64Encode(this string plainText)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }

    /// <summary>
    /// Decode the string from Base64.
    /// </summary>
    public static string Base64Decode(this string base64EncodedData)
    {
        var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }

    /// <summary>
    /// Wait for Few Seconds Before Performing the Action.
    /// </summary>
    public static void WaitForTime(this MonoBehaviour behaviour, Action action, float waitTime)
    {
        behaviour.StartCoroutine(WaitBeforeAction(action, waitTime));
    }
    #endregion

    static System.Collections.IEnumerator WaitBeforeAction(Action action, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }
}
#endregion