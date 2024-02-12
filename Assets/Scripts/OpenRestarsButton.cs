using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenRestarsButton : MonoBehaviour
{
    public GameObject restartButton;

    public void riseRestarButton()
    {
        restartButton.SetActive(true);
    }

}
