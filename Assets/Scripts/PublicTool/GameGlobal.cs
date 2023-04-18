using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameGlobal
{
    public static float timeOneYear = 1f;//Define the time of one year
    public static float[] arrayEduLevel = {0, 50f, 80f };//Rate of Education

    public static float rateYearEdu_School = 4f;//rate of Education Grow
    public static float[] rateYearFortune_Job = { 1f, 4f, 8f };//Rate of Fortune

    public static int ageMin_School = 8;
    public static int ageMax_School = 28;

    public static int ageMin_Job = 26;
    public static int ageMax_Job = 60;

    public static int ageMin_Marriage = 22;
    public static int ageMax_Marriage = 40;

    public static int ageMin_Retire = 60;

}
