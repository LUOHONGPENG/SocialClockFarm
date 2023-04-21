using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectUIManager : MonoBehaviour
{
    public Transform tfEffect;
    public GameObject pfBubbleTip;

    public void InitBubble(SlotType slotType,string strEffect)
    {
        GameObject objBubble = GameObject.Instantiate(pfBubbleTip, tfEffect);

    }


}
