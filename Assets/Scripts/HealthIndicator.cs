using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIndicator : MonoBehaviour
{
    [SerializeField]
    new Renderer renderer;
    private void OnEnable()
    {
        PlayerController.OnPlayerDamaged += changeColor;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerDamaged -= changeColor;
    }

    private void Start()
    {
        renderer.material.color = Color.green;
    }

    void changeColor(float value,float maxValue)
    {             //Porcentaje = (parte/max)*100
                   //Porcentaje/100 = parte/max
                   //(porcentaje/100)*max = parte
        float valuePercentage = (value / maxValue) * 100f;
        renderer.material.color = new Color(1- (valuePercentage / 100), (valuePercentage / 100), 0f);
    }
}
