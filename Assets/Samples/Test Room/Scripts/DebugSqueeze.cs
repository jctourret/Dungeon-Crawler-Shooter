using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSqueeze : MonoBehaviour
{
    public void Squeeze(float value)
    {
        Debug.Log(gameObject.name + " is Squeezing for: " + value);

    }
}
