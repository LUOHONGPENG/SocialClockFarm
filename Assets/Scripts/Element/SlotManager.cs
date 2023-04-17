using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotType
{
    School,
    Job,
    Marriage
}

public class SlotManager : MonoBehaviour
{
    public SlotType slotType;
    public Transform tfPos;
    public bool isFilled = false;
    private int ageMin;
    private int ageMax;

    public void Init(SlotType slotType,int ageMin,int ageMax)
    {
        this.slotType = slotType;
        this.ageMin = ageMin;
        this.ageMax = ageMax;
        this.isFilled = false;
    }

    public bool CheckHumanValid(HumanData humanData)
    {
        if (isFilled)
        {
            return false;
        }
        if(humanData.Age < ageMin)
        {
            return false;
        }
        if (humanData.Age > ageMax)
        {
            return false;
        }
        return true;
    }

    public void SetLocalPos(Vector2 pos)
    {
        tfPos.localPosition = pos;
    }
}
