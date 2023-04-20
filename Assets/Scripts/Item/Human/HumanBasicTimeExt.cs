using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class HumanBasic
{
    private float timerGrow = GameGlobal.timeOneYear;
    private bool isDelaySchoolRed = false;

    public void TimeGo()
    {
        TimeGoCheckDrag();
        TimeGoData();
        TimeGoCheckRed();
    }

    private void TimeGoCheckDrag()
    {
        if (dragManager != null)
        {
            dragManager.TimeGoDrag();
        }
    }

    public void TimeGoCheckRed()
    {
        if (isInSchool && humanModel.Age > GameGlobal.ageMax_School)
        {
            isDelaySchoolRed = true;
        }
        else
        {
            isDelaySchoolRed = false;
        }

        if (isDelaySchoolRed)
        {
            srHuman.color = Color.red;
        }
        else
        {
            srHuman.color = Color.white;
        }
    }

    #region TimeGoData
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
        if (humanModel.Age > 80)
        {
            GameManager.Instance.levelManager.Retire(this);
        }
    }

    private void DataChange(float timeDelta)
    {
        if (isInSchool)
        {
            float rateSchool = (GameGlobal.rateYearEdu_School / GameGlobal.timeOneYear);
            humanModel.TimeGoRecordSchool(timeDelta, rateSchool);
            RefreshUI();
        }

        if (isInJob)
        {
            float rateCareer = 0;
            switch (currentSlot.slotID)
            {
                case 2001:
                    rateCareer = GameGlobal.rateYearCareer_Job[0];
                    break;
                case 2002:
                    rateCareer = GameGlobal.rateYearCareer_Job[1];
                    break;
                case 2003:
                    rateCareer = GameGlobal.rateYearCareer_Job[2];
                    break;
            }
            float rateCareerGrow = (rateCareer / GameGlobal.timeOneYear);
            humanModel.TimeGoRecordJob(timeDelta, rateCareerGrow);
            RefreshUI();
        }
    }
    #endregion


}
