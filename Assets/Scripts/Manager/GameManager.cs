using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public LevelManager levelManager;

    public bool isTimeStop = false;

    public void Start()
    {
        Init();
    }

    public void Init()
    {
        levelManager.Init();
        isTimeStop = false;
    }

    public void Update()
    {
        levelManager.TimeGo();
    }

}
