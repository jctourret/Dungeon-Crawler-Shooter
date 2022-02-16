using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGrip : MonoBehaviour
{
    public void grip(bool value)
    {
        if (value)
        {
            Debug.Log(value);
        }
    }
}
