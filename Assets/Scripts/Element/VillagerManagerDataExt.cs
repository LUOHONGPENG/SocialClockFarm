using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class VillagerManager
{
    #region VillagerData
    public VillagerData thisData;

    private float vCurrentEdu
    {
        get
        {
            if (thisData != null)
            {
                return thisData.vEdu;
            }
            return 0;
        }
        set
        {
            if (thisData != null)
            {
                thisData.vEdu = value;
            }
        }
    }

    private float vCurrentJob
    {
        get
        {
            if (thisData != null)
            {
                return thisData.vJob;
            }
            return 0;
        }
        set
        {
            if (thisData != null)
            {
                thisData.vJob = value;
            }
        }
    }
    #endregion

    private float timerGrow = GameGlobal.timeOneYear;


    public bool isInSchool
    {
        get
        {
            return false;
        }
    }

    public bool isInJob
    {
        get
        {
            return false;
        }
    }

    public void TimeGoVillager()
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
        thisData.AgeGrow();
    }

    private void DataChange(float timeDelta)
    {
        if (isInSchool)
        {
            vCurrentEdu += GameGlobal.rateYearEdu_School * timeDelta;
        }

        if (isInJob)
        {
            vCurrentJob += GameGlobal.rateYearEdu_School * timeDelta;
        }
    }
}
