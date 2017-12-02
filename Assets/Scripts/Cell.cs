using UnityEngine;
using System;
using System.Collections;

public class Cell : MonoBehaviour
{
    public int column;
	public int row;

    public void Initialize(int column, int row)
    {
        this.column = column;
        this.row = row;
    }

    public override string ToString()
    {
        return "(" + (char)((char)'a' + column) + ", " + row + ")";
    }
}
