using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Point 
{
    public int x, y;

    public Point (int x,int y)
    {
        this.x = x;
        this.y = y;
    }
    public override string ToString()
    {
        return string.Format("x:{0}, y:{1}", x, y);
    }
    public override bool Equals(object obj)
    {
        if (!(obj is Point))
            return false;

        var pt = (Point)obj;
        return pt.x == x && pt.y == y;
    }
    public override int GetHashCode()
    {
        return x.GetHashCode() ^ y.GetHashCode() << 2;
    }
    public static bool operator ==(Point p1, Point p2)
    {
        return p1.Equals(p2);
    }
    public static bool operator !=(Point p1, Point p2)
    {
        return !(p1 == p2);
    }
    public static Point Zero
    {
        get { return new Point(0, 0); }
    }
}
