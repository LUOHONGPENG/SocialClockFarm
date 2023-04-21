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
        BubbleTipEffect itemBubble = objBubble.GetComponent<BubbleTipEffect>();
        switch (slotType)
        {
            case SlotType.Study:
                itemBubble.transform.localPosition = new Vector2(-516, 46);
                break;
            case SlotType.Job:
                itemBubble.transform.localPosition = new Vector2(345, 43);
                break;
            case SlotType.Retire:
                itemBubble.transform.localPosition = new Vector2(600, -83);
                break;
        }
        itemBubble.Init(strEffect);
    }


}
