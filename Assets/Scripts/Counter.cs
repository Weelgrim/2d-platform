using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Text counter;

    public float score;

    public void GetPointsforCoin()
    {
        score = score + 50f;
    }

    public void GetPointsforKill()
    {
        score = score + 100f;
    }
    public void ShowCounter()
    {
        counter.text = score.ToString();
    }
}
