using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct HexCoordinate  {

    [SerializeField]
    private int x, z;

    public int X
    {
        get
        {
            return x;
        }
    }
           
    public int Z
    {
        get
        {
            return z;
        }
    }

    public int Y
    {
        get
        {
            return -X - Z;
        }
    }

    public static HexCoordinate GetOffSetCoordinate(int _tmpx, int _tmpz)
    {
        return new HexCoordinate(_tmpx, _tmpz);
    }

    public HexCoordinate(int _x, int _z)
    {
        x = _x;
        z = _z;
    }

    public override string ToString()
    {
        return "(" +
            X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
    }

    public string ToStringOnSeparateLines()
    {
        return X.ToString() + "\n" + Y.ToString() + "\n" + Z.ToString();
    }

    
}
