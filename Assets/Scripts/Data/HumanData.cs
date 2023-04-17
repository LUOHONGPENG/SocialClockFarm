using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanData
{
    public int HumanID = -1;
    public int Age = 0;
    public bool isMarried = false;//Whether this people is married
    public float vEdu = 0;//Value about Education
    public float vFortune = 0;//Value about Job
    public float vStatus = 0;//Value about Social Status(Hide)

    public HumanData(int ID,float Status)
    {
        this.HumanID = ID;
        this.Age = 0;
        this.isMarried = false;
        this.vEdu = 0;
        this.vFortune = 0;
        this.vStatus = Status;
    }

    public void AgeGrow()
    {
        this.Age++;
    }

    public void SetValue(float vEdu, float vFortune)
    {
        this.vEdu = vEdu;
        this.vFortune = vFortune;
    }
}
