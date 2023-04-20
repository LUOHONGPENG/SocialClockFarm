using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public LevelManager levelManager;
    public UIManager uiManager;

    public bool isUIPageOn = false;


    public override void Init()
    {
        StartCoroutine(IE_Init());
    }

    public IEnumerator IE_Init()
    {
        levelManager.Init();
        uiManager.Init();
        isUIPageOn = false;
        yield return new WaitForEndOfFrame();
        uiManager.ShowRecipe();
    }

    public void Update()
    {
        if (isUIPageOn)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }


        levelManager.TimeGo();
    }

}
