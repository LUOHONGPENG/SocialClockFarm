using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SlotManager : MonoBehaviour
{
    public SlotType slotType;
    public int slotID;
    public Transform tfPos;
    public bool isFilled = false;

    private int ageMin;
    private int ageMax;
    private float limitEdu;
    private float limitFortune;


    public void Init(SlotType slotType,int ageMin,int ageMax,float limitEdu=0, float limitFortune = 0)
    {
        this.slotType = slotType;
        this.ageMin = ageMin;
        this.ageMax = ageMax;
        this.limitEdu = limitEdu;
        this.limitFortune = limitFortune;

        this.isFilled = false;
    }

    public void SetID(int ID)
    {
        this.slotID = ID;
    }

    public bool CheckHumanValid(HumanData humanData)
    {
        if (isFilled)
        {
            return false;
        }
        if(slotType == SlotType.Marriage && humanData.isMarried)
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
        if(humanData.vEdu < limitEdu)
        {
            return false;
        }
        if (humanData.vFortune < limitFortune)
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
