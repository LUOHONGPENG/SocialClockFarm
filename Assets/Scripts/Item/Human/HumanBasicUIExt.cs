using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public partial class HumanBasic
{
    [Header("UI")]
    public Text txAge;
    public Image imgFillEdu;
    public Text codeEdu;
    public Image imgFillCareer;
    public Text codeCareer;

    #region UIControl

    public void RefreshUI()
    {
        txAge.text = humanModel.Age.ToString();
        imgFillEdu.fillAmount = vCurrentEdu / 100f;
        imgFillCareer.fillAmount = vCurrentCareer / 100f;
        codeEdu.text = string.Format("{0}%", Mathf.RoundToInt(vCurrentEdu));
        codeCareer.text = string.Format("{0}%", Mathf.RoundToInt(vCurrentCareer));

    }
    #endregion 
}
