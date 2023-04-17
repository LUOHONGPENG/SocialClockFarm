using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerData
{
    public int VillagerID = -1;
    public int Age = 0;
    public bool isMarried = false;//Whether this people is married
    public float vEdu = 0;//Value about Education
    public float vJob = 0;//Value about Job
    public float vStatus = 0;//Value about Social Status(Hide)

    public VillagerData(int ID,float Status)
    {
        this.VillagerID = ID;
        this.Age = 0;
        this.isMarried = false;
        this.vEdu = 0;
        this.vJob = 0;
        this.vStatus = Status;
    }

    public void AgeGrow()
    {
        this.Age++;
    }

    public void SetValue(float vEdu, float vJob)
    {
        this.vEdu = vEdu;
        this.vJob = vJob;
    }
}
