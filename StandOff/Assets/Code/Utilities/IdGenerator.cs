using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to generate ID's for objects
/// </summary>
public static class IdGenerator 
{
    public static Guid Generate()
    {
        return System.Guid.NewGuid();
    }
}
