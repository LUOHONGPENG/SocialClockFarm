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

    public bool CheckHumanValid(HumanModel humanModel)
    {
        if (isFilled)
        {
            return false;
        }
        if(slotType == SlotType.Marriage && humanModel.isMarried)
        {
            return false;
        }
        if(humanModel.Age < ageMin)
        {
            return false;
        }
        if (humanModel.Age > ageMax)
        {
            return false;
        }
        if(humanModel.vEdu < limitEdu)
        {
            return false;
        }
        if (humanModel.vFortune < limitFortune)
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
