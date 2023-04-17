using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Human")]
    public Transform tfHuman;
    [HideInInspector]
    public List<HumanManager> listHuman = new List<HumanManager>();
    public List<HumanData> listHumanData = new List<HumanData>();
    public GameObject pfHuman;

    [Header("Career")]
    public CareerManager careerSchool;

    private bool isInit = false;

    public void Init()
    {
        listHumanData.Clear();
        listHuman.Clear();

        InitHuman();
        InitCareer();

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
        HumanData humanData = new HumanData(listHumanData.Count, 50f);
        listHumanData.Add(humanData);
        CreateNewHumanPrefab(humanData);
    }

    public void CreateNewHumanPrefab(HumanData humanData)
    {
        GameObject objHuman = GameObject.Instantiate(pfHuman, tfHuman);
        HumanManager itemHuman = objHuman.GetComponent<HumanManager>();
        itemHuman.Init(humanData);
        listHuman.Add(itemHuman);
        RefreshHumanPos();
    }

    public void RefreshHumanPos()
    {
        int totalNum = listHuman.Count;
        for(int i = 0; i < listHuman.Count; i++)
        {
            Vector2 targetPos = PublicTool.CalculatePosDelta(totalNum, i, 2f);
            listHuman[i].SetLocalPos(targetPos);
        }
    }
    #endregion

    #region CareerControl

    public void InitCareer()
    {
        careerSchool.Init(CareerType.School);
    }

    #endregion


    #region TimeControl

    public void TimeGo()
    {
        if (!isInit)
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
