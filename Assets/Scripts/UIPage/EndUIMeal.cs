using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndUIMeal : MonoBehaviour
{
    public Text txMealScore;

    public void Init(int vScore)
    {
        txMealScore.text = vScore.ToString();
    }
}
