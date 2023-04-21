using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleTipEffect : MonoBehaviour
{
    public Text txBubble;

    public void Init(string strBubble)
    {
        txBubble.text = strBubble.ToString();

        Destroy(this.gameObject, 2f);
    }
}
