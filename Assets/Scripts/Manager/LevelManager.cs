using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Human")]
    public Transform tfHuman;
    [HideInInspector]
    public List<HumanManager> listHuman = new List<HumanManager>();
    public List<HumanModel> listHumanModel = new List<HumanModel>();
    public GameObject pfHuman;

    [Header("Career")]
    public CareerManager careerSchool;
    public CareerManager careerJob0;
    public CareerManager careerJob1;
    public CareerManager careerJob2;

    [Header("Marriage")]
    public List<MarriageManager> listMarriage = new List<MarriageManager>();
    public Transform tfMarriage;
    public GameObject pfMarriage;
    private int countID_marriage;

    [Header("Retirement")]
    public RetireManager retireManager;


    private bool isInit = false;

    public void Init()
    {
        listHumanModel.Clear();
        listHuman.Clear();

        InitHuman();
        InitCareer();
        InitMarriage();
        retireManager.Init();

        isInit = true;
    }

    #region HumanControl
    public void InitHuman()
    {
        for(int i = 0; i < 3; i++)
        {
            CreateNewHuman();
        }
    }

    public void CreateNewHuman()
    {
        HumanModel humanModel = new HumanModel(listHumanModel.Count, 50f);
        listHumanModel.Add(humanModel);
        CreateNewHumanPrefab(humanModel);
    }

    public void CreateNewHumanPrefab(HumanModel humanModel)
    {
        GameObject objHuman = GameObject.Instantiate(pfHuman, tfHuman);
        HumanManager itemHuman = objHuman.GetComponent<HumanManager>();
        itemHuman.Init(humanModel);
        listHuman.Add(itemHuman);
        RefreshHumanPos();
    }

    public void DeleteHuman(HumanManager human)
    {
        if (human != null)
        {
            Destroy(human.gameObject);
            listHuman.Remove(human);
            RefreshHumanPos();
        }
    }

    public void RefreshHumanPos()
    {
        int totalNum = listHuman.Count;
        for(int i = 0; i < listHuman.Count; i++)
        {
            Vector2 targetPos = PublicTool.CalculatePosDelta(totalNum, i, 2f,8);
            listHuman[i].transform.localPosition = targetPos;
            listHuman[i].SetHumanSlot();
        }
    }
    #endregion

    #region CareerControl
    public void InitCareer()
    {
        careerSchool.Init(SlotType.School);
        careerJob0.Init(SlotType.Job0);
        careerJob1.Init(SlotType.Job1);
        careerJob2.Init(SlotType.Job2);
    }
    #endregion

    #region MarriageControl

    public void InitMarriage()
    {
        listMarriage.Clear();
        for(int i = 0; i < 2; i++)
        {
            CreateMarriage();
        }
    }

    public void CreateMarriage()
    {
        GameObject objMarriage = GameObject.Instantiate(pfMarriage, tfMarriage);
        MarriageManager itemMarriage = objMarriage.GetComponent<MarriageManager>();
        itemMarriage.Init(countID_marriage);
        listMarriage.Add(itemMarriage);
        RefreshMarriagePos();
    }

    public void RefreshMarriagePos()
    {
        int totalNum = listMarriage.Count;
        for (int i = 0; i < listMarriage.Count; i++)
        {
            Vector2 targetPos = PublicTool.CalculatePosDelta(totalNum, i, 2f,5);
            listMarriage[i].transform.localPosition = targetPos;
        }
    }
    public void ReachMarriage(int ID)
    {
        MarriageManager targetMarriage = null;
        foreach(var marriage in listMarriage)
        {
            if (marriage.marriageID == ID)
            {
                targetMarriage = marriage;
            }
        }

        if (targetMarriage != null)
        {
            Destroy(targetMarriage.gameObject);
            listMarriage.Remove(targetMarriage);
            CreateMarriage();
            CreateNewHuman();
        }

    }


    #endregion

    #region RetireControl

    public void Retire(HumanManager human)
    {
        GameManager.Instance.uiManager.ShowRetire(human);
    }

    #endregion

    #region TimeControl

    public void TimeGo()
    {
        if (!isInit||GameManager.Instance.isTimeStop)
        {
            return;
        }

        for(int i = 0; i < listHuman.Count; i++)
        {
            listHuman[i].TimeGo();
        }
    }


    #endregion

}
