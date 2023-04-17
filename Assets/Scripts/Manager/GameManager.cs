using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public LevelManager levelManager;

    public void Start()
    {
        Init();
    }

    public void Init()
    {
        levelManager.Init();
    }

    public void Update()
    {
        levelManager.TimeGo();
    }

}
