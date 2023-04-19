using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanData
{
    public int HumanID = -1;
    public int Age = 0;
    public float vEdu = 0;//Value about Education
    public float vFortune = 0;//Value about Job
    public bool isMarried = false;//Whether this people is married
    public int vMarryAge = -1;
    //Special Value
    public int vFirstStudyAge = -1;
    public int vLastStudyAge = -1;
    public int vFirstWorkAge = -1;
    public float vDelayGraduationYear = 0;

    public HumanData(int ID,float Status)
    {
        this.HumanID = ID;
        this.Age = 0;
        this.isMarried = false;
        this.vEdu = 0;
        this.vFortune = 0;
    }

    public void AgeGrow()
    {
        this.Age++;
    }

    public void TimeGoRecordSchool(float timeDelta,float rateEdu)
    {
        //MarkYear
        if (vFirstStudyAge < 0)
        {
            vFirstStudyAge = Age;
        }
        if (vLastStudyAge < Age)
        {
            vLastStudyAge = Age;
        }
        //DelayGraduation
        if (Age > GameGlobal.ageMax_School)
        {
            vDelayGraduationYear += timeDelta / GameGlobal.timeOneYear;
        }

        vEdu += rateEdu * timeDelta;
        if (vEdu > 100f)
        {
            vEdu = 100f;
        }
    }

    public void TimeGoRecordJob(float timeDelta,float rateFortune)
    {
        //MarkYear
        if (vFirstStudyAge < 0)
        {
            vFirstStudyAge = Age;
        }

        vFortune += rateFortune * timeDelta;
        if (vFortune > 100f)
        {
            vFortune = 100f;
        }
    }

    public void RecordMarried()
    {
        isMarried = true;
        vMarryAge = Age;
    }
}
