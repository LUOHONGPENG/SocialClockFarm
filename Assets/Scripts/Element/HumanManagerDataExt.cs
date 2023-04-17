using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class HumanManager
{
    #region HumanData
    public HumanData humanData;

    private float vCurrentEdu
    {
        get
        {
            if (humanData != null)
            {
                return humanData.vEdu;
            }
            return 0;
        }
        set
        {
            if (humanData != null)
            {
                humanData.vEdu = value;
            }
        }
    }

    private float vCurrentFortune
    {
        get
        {
            if (humanData != null)
            {
                return humanData.vFortune;
            }
            return 0;
        }
        set
        {
            if (humanData != null)
            {
                humanData.vFortune = value;
            }
        }
    }
    #endregion

    private float timerGrow = GameGlobal.timeOneYear;


    public bool isInSchool
    {
        get
        {
            if(currentSlot!=null && currentSlot.slotType == SlotType.School)
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
            return false;
        }
    }

    private void TimeGoData()
    {
        float timeDelta = Time.deltaTime;
        timerGrow -= timeDelta;
        if (timerGrow < 0)
        {
            timerGrow = GameGlobal.timeOneYear;
            AgeGrow();
        }
        DataChange(timeDelta);
    }

    private void AgeGrow()
    {
        humanData.AgeGrow();
        RefreshUI();
    }

    private void DataChange(float timeDelta)
    {
        if (isInSchool)
        {
            vCurrentEdu += GameGlobal.rateYearEdu_School * timeDelta;
            RefreshUI();
        }

        if (isInJob)
        {
            vCurrentFortune += GameGlobal.rateYearEdu_School * timeDelta;
            RefreshUI();
        }
    }
}
