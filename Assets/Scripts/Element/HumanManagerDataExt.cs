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
            if (currentSlot != null)
            {
                switch (currentSlot.slotType)
                {
                    case SlotType.Job0:
                    case SlotType.Job1:
                    case SlotType.Job2:
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
            vCurrentEdu += (GameGlobal.rateYearEdu_School/GameGlobal.timeOneYear) * timeDelta;
            humanData.TimeGoRecordSchool(timeDelta);
            RefreshUI();
        }

        if (isInJob)
        {
            float rateFortune = 0;
            switch (currentSlot.slotType)
            {
                case SlotType.Job0:
                    rateFortune = GameGlobal.rateYearFortune_Job[0];
                    break;
                case SlotType.Job1:
                    rateFortune = GameGlobal.rateYearFortune_Job[1];
                    break;
                case SlotType.Job2:
                    rateFortune = GameGlobal.rateYearFortune_Job[2];
                    break;
            }
            vCurrentFortune += (rateFortune / GameGlobal.timeOneYear) * timeDelta;
            humanData.TimeGoRecordJob(timeDelta);
            RefreshUI();
        }
    }
}
