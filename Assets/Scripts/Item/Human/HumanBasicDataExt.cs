using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class HumanBasic
{
    #region HumanModel
    public HumanModel humanModel;

    private float vCurrentEdu
    {
        get
        {
            if (humanModel != null)
            {
                return humanModel.vEdu;
            }
            return 0;
        }
    }

    private float vCurrentCareer
    {
        get
        {
            if (humanModel != null)
            {
                return humanModel.vCareer;
            }
            return 0;
        }
    }
    #endregion



    public bool isInSchool
    {
        get
        {
            if(currentSlot!=null && currentSlot.slotType == SlotType.Study)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool isInJob
    {
        get
        {
            if (currentSlot != null)
            {
                if(currentSlot.slotType == SlotType.Job)
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
    }




}
