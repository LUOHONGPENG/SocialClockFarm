using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public LevelManager levelManager;
    public EffectUIManager effectUIManager;
    public UIManager uiManager;
    public SoundManager soundManager;

    public bool isDoubleSpeed = false;
    public bool isUIPageOn = false;


    public override void Init()
    {
        StartCoroutine(IE_Init());
    }

    public IEnumerator IE_Init()
    {
        levelManager.Init();
        uiManager.Init();
        soundManager.Init();

        isDoubleSpeed = false;
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
            if (isDoubleSpeed)
            {
                Time.timeScale = 2f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
        levelManager.TimeGo();
    }

}
