using UnityEngine;


public interface IValue
{
    float x { get; set; }
    float y { get; set; }
    float z { get; set; }
}

public class Bound
{
    public IValue max;
    public IValue min;

    public Bound()
    {
        max = new Values();
        min = new Values();
    }

    public Bound(Vector2 Max, Vector2 Min)
    {
        max = new Values
        {
            x = Max.x,
            y = Max.y
        };
        min = new Values
        {
            x = Min.x,
            y = Min.y
        };
    }

    public Bound(Vector3 Max, Vector3 Min)
    {
        max = new Values
        {
            x = Max.x,
            y = Max.y,
            z = Max.z
        };
        min = new Values
        {
            x = Min.x,
            y = Min.y,
            z = Min.z
        };
    }

    private class Values : IValue
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
    }
}