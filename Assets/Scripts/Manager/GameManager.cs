using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public LevelManager levelManager;
    public UIManager uiManager;

    public bool isTimeStop = false;


    public override void Init()
    {
        levelManager.Init();
        uiManager.Init();

        isTimeStop = false;
    }

    public void Update()
    {
        levelManager.TimeGo();
    }

}
