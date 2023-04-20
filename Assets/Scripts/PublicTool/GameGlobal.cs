using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameGlobal
{
    public static float timeOneYear = 1.5f;//Define the time of one year
    public static float[] arrayEduLevel = {0, 50f, 80f };//Rate of Education

    public static float rateYearEdu_School = 4f;//rate of Education Grow
    public static float[] rateYearCareer_Job = { 1f, 2f, 4f };//Rate of Career

    public static int ageMin_School = 8;
    public static int ageMax_School = 28;

    public static int ageMin_Job = 26;
    public static int ageMax_Job = 60;

    public static int ageMin_Marriage = 22;
    public static int ageMax_Marriage = 35;

    public static int ageMin_Retire = 60;

}
