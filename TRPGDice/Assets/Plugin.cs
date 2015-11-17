using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
public class plugins
{
    [DllImport("native_activity", CharSet = CharSet.Ansi)]

    private static extern bool Test();
    public bool ISss()
    {
        try
        {
            return Test();
        }
        catch (Exception e)
        {
            return true;
        }
    }
}
