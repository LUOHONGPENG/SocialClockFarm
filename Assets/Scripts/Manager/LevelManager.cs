using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Villager")]
    public Transform tfVillager;
    [HideInInspector]
    public List<VillagerManager> listVillager = new List<VillagerManager>();
    public List<VillagerData> listVillagerData = new List<VillagerData>();
    public GameObject pfVillager;

    [Header("Career")]
    public CareerManager careerSchool;

    private bool isInit = false;

    public void Init()
    {
        listVillagerData.Clear();
        listVillager.Clear();

        InitVillager();
        InitCareer();

        isInit = true;
    }

    #region VillagerControl
    public void InitVillager()
    {
        for(int i = 0; i < 3; i++)
        {
            CreateNewVillager();
        }
    }

    public void CreateNewVillager()
    {
        VillagerData villagerData = new VillagerData(listVillagerData.Count, 50f);
        listVillagerData.Add(villagerData);
        CreateNewVillagerPrefab(villagerData);
    }

    public void CreateNewVillagerPrefab(VillagerData villagerData)
    {
        GameObject objVillager = GameObject.Instantiate(pfVillager, tfVillager);
        VillagerManager itemVillager = objVillager.GetComponent<VillagerManager>();
        itemVillager.Init(villagerData);
        listVillager.Add(itemVillager);
        RefreshVillagerPos();
    }

    public void RefreshVillagerPos()
    {
        int totalNum = listVillager.Count;
        for(int i = 0; i < listVillager.Count; i++)
        {
            Vector2 targetPos = PublicTool.CalculatePosDelta(totalNum, i, 2f);
            listVillager[i].SetLocalPos(targetPos);
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

        for(int i = 0; i < listVillager.Count; i++)
        {
            listVillager[i].TimeGo();
        }
    }


    #endregion

}
