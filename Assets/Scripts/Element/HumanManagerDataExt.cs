using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class HumanManager
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

    private float vCurrentFortune
    {
        get
        {
            if (humanModel != null)
            {
                return humanModel.vFortune;
            }
            return 0;
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
        humanModel.AgeGrow();
        RefreshUI();
    }

    private void DataChange(float timeDelta)
    {
        if (isInSchool)
        {
            float rateSchool =  (GameGlobal.rateYearEdu_School/GameGlobal.timeOneYear);
            humanModel.TimeGoRecordSchool(timeDelta, rateSchool);
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
            float rateFortuneGrow = (rateFortune / GameGlobal.timeOneYear);
            humanModel.TimeGoRecordJob(timeDelta, rateFortuneGrow);
            RefreshUI();
        }
    }
}
