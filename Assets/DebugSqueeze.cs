using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSqueeze : MonoBehaviour
{
    public void Squeeze(float value)
    {
        int time = 2;
        float timer = 0.0f;
        if(timer < time)
        {
            if (value > 0.1f)
            {
                //Debug.Log(gameObject.name + " is Squeezing for: " + value);
            }
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0.0f;
        }
    }
}
