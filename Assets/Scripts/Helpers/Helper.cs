using UnityEngine;

public class Helper
{
    public static TextMesh CreateWorldText(string text, Transform parent, Vector3 localPosition, int fontSize, Color fontColor, TextAlignment alignment, TextAnchor anchor)
    {
        GameObject go = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = go.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = go.GetComponent<TextMesh>();

        textMesh.anchor = anchor;
        textMesh.alignment = alignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = fontColor;
        //textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }

    public static Vector3 GetMouseWorlPosition(Camera cam = null)
    {
        if (cam == null)
            cam = Camera.main;

        Vector3 vec = cam.ScreenToWorldPoint(Input.mousePosition);
        return vec;
    }

    public static Vector2 RandomVector(Vector2 pointOne, Vector2 pointTwo)
    {
        return new Vector2(Random.Range((float)pointOne.x, (float)pointTwo.x),
            Random.Range((float)pointOne.y, (float)pointTwo.y));
    }

    public static Vector3 RandomVector(Vector3 pointOne, Vector3 pointTwo)
    {
        return new Vector3(Random.Range((float)pointOne.x, (float)pointTwo.x),
            Random.Range((float)pointOne.y, (float)pointTwo.y),
            Random.Range((float)pointOne.z, (float)pointTwo.z));
    }

    #region CUSTOM_LERP_METHODS
    public static float ULerp(float from, float to, float val)
    {
        return (1 - val) * from + val * to;
    }

    public static float UInverseLerp(float from, float to, float val)
    {
        return (val - from) / (to - from);
    }

    public static float Map(float current1, float current2, float target1, float target2, float val)
    {
        return ULerp(target1, target2, UInverseLerp(current1, current2, val));
    }
    #endregion
        
    public static string JwtSecurityTokenHandler(string token)
    {
        var parts = token.Split('.');
        if (parts.Length > 2)
        {
            var decode = parts[1];
            var padLength = 4 - decode.Length % 4;
            if (padLength < 4)
            {
                decode += new string('=', padLength);
            }
            var bytes = Convert.FromBase64String(decode);
            return Encoding.ASCII.GetString(bytes);
        }
        else
            return null;
    }
}





#region DEBUGGER
public class Debbuger
{
    public static void DebugInColor(string message, Colors color)
    {
        Debug.Log("<color=" + color.ToString() + ">" + message + "</color>");
    }

    public static void DebugInColor(string message, Colors color, GameObject go)
    {
        Debug.Log("<color=" + color.ToString() + ">" + message + "</color>", go);
    }
}

public enum Colors { red, yellow, green, blue, cyan, magenta }
#endregion
