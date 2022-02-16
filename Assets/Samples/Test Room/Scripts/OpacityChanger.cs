using UnityEngine;

public class OpacityChanger : MonoBehaviour
{
    public GameObject meshToChange;

    protected MeshRenderer materialRenderer;

    protected void OnEnable()
    {
        materialRenderer = meshToChange.GetComponentInChildren<MeshRenderer>();
    }

    public void ChangeOpacity(float value)
    {
        Debug.Log(gameObject.name + "is being called with value: " + value);
        Color currentColor = materialRenderer.material.color;
        currentColor.a = 1f - value;
        materialRenderer.material.color = currentColor;
    }
}