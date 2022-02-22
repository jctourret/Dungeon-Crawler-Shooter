using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Holster : MonoBehaviour
{
    public void flagWeapon(GameObject snappedWeapon)
    {
        DontDestroyOnLoad(snappedWeapon.transform.parent.gameObject);
        Debug.Log(snappedWeapon.transform.parent.gameObject.name);
    }

    public void deflagWeapon(GameObject snappedWeapon)
    {
        SceneManager.MoveGameObjectToScene(snappedWeapon,SceneManager.GetActiveScene());
    }
}
