using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndUIManager : MonoBehaviour
{
    public GameObject objPopup;

    public Text txTotalScore;
    public Button btnRestart;
    public Transform tfMeal;
    public GameObject pfMeal;

    public void Init()
    {
        btnRestart.onClick.RemoveAllListeners();
        btnRestart.onClick.AddListener(delegate() {        
            HidePopup();
            SceneManager.LoadScene("Main");
        });
    }

    public void ShowPopup()
    {
        List<int> listScore = GameManager.Instance.levelManager.listMealScore;
        for (int i = 0; i < listScore.Count; i++)
        {
            GameObject objMeal = GameObject.Instantiate(pfMeal, tfMeal);
            EndUIMeal itemMeal = objMeal.GetComponent<EndUIMeal>();
            itemMeal.Init(listScore[i]);
        }

        txTotalScore.text = GameManager.Instance.levelManager.totalScore.ToString();
        objPopup.SetActive(true);
    }
    public void HidePopup()
    {
        objPopup.SetActive(false);
        GameManager.Instance.isUIPageOn = false;
    }
}
