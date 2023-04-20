using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotConditionUI : MonoBehaviour
{
    public Text txAge;
    public Text txEdu;
    public GameObject objEdu;

    public void InitUI(SlotCondition condition)
    {
        if (condition.ageMax > 100)
        {
            txAge.text = string.Format("{0}+", condition.ageMin);
        }
        else
        {
            txAge.text = string.Format("{0}-{1}", condition.ageMin, condition.ageMax);
        }

        if (condition.eduMin > 0)
        {
            objEdu.SetActive(true);
            txEdu.text = string.Format(">{0}%", condition.eduMin);
        }
        else
        {
            objEdu.SetActive(false);
        }
    }
}
