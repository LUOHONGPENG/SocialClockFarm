using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetireUIManager : MonoBehaviour
{
    [Header("BasicUI")]
    public GameObject objPopup;
    public Button btnClose;

    [Header("Score")]
    public Text codeScore;

    [Header("Comment")]
    public Transform tfComment;
    public GameObject pfComment;


    private HumanManager storedHuman;

    public void Init()
    {
        btnClose.onClick.RemoveAllListeners();
        btnClose.onClick.AddListener(HidePopup);
    }

    public void ShowPopup(HumanManager human)
    {
        this.storedHuman = human;
        HumanData humanData = human.humanData;

        int vScore = CalculateScore(humanData);
        codeScore.text = vScore.ToString();

        objPopup.SetActive(true);
    }

    public void HidePopup()
    {
        if (storedHuman != null)
        {
            GameManager.Instance.levelManager.DeleteHuman(storedHuman);
        }
        objPopup.SetActive(false);
        GameManager.Instance.isTimeStop = false;
    }

    #region Generate Score & Comment
    public void CreateComment(string strComment, int vScore)
    {
        GameObject objComment = GameObject.Instantiate(pfComment, tfComment);
        RetireUIComment itemComment = objComment.GetComponent<RetireUIComment>();
        itemComment.Init(strComment, vScore);
    }

    public int CalculateScore(HumanData humanData)
    {
        string strComment = "";
        int totalScore = 0;
        int tempScore = 0;
        //Education
        if (humanData.vEdu > 60)
        {
            strComment = "High Education. So clean!";
        }
        else if (humanData.vEdu >= 20)
        {
            strComment = "Normal Education. Not bad!";
        }
        else
        {
            strComment = "Poor Education. Dirty!";
        }
        tempScore = -300 + Mathf.RoundToInt(humanData.vEdu) * 15;
        CreateComment(strComment, tempScore);
        totalScore += tempScore;


        //Income
        if (humanData.vFortune > 60)
        {
            strComment = "High Income. Smooth taste!";
        }
        else if (humanData.vFortune >= 30)
        {
            strComment = "Normal Income. Not bad!";
        }
        else
        {
            strComment = "Low Income. Woody taste!";
        }
        tempScore = -600 + Mathf.RoundToInt(humanData.vFortune) * 20;
        CreateComment(strComment, tempScore);
        totalScore += tempScore;

        //Marriage
        if (humanData.isMarried)
        {
            if (humanData.vMarryAge >= 40)
            {
                strComment = "Too Late to Marry!";
                tempScore = -200;
            }
            else if (humanData.vMarryAge >= 30)
            {
                strComment = "Married.";
                tempScore = 200;
            }
            else
            {
                strComment = "Quick Marry.";
                tempScore = 500;
            }
        }
        else
        {
            strComment = "Single?";
            tempScore = -1000;
        }
        CreateComment(strComment, tempScore);
        totalScore += tempScore;

        //Gap Year
        if(humanData.vLastStudyAge > 0 && humanData.vFirstWorkAge - humanData.vLastStudyAge > 1)
        {
            int vGapYear = Mathf.FloorToInt(humanData.vFirstWorkAge - humanData.vLastStudyAge);
            strComment = string.Format("How Dare You Gap {0} Year?!", vGapYear);
            tempScore = -200 * vGapYear;
            CreateComment(strComment, tempScore);
            totalScore += tempScore;
        }

        //Delay Graduation
        if (humanData.vDelayGraduationYear > 1f)
        {
            int vDelay = Mathf.FloorToInt(humanData.vDelayGraduationYear);
            strComment = string.Format("Delay Graduation?!");
            tempScore = -100 * vDelay;
            CreateComment(strComment, tempScore);
            totalScore += tempScore;
        }

        return totalScore;
    }


    #endregion
}
