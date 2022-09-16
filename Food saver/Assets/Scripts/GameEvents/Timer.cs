using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private GameObject panenlStartTimer;
    [SerializeField] private GameObject foodPrefabs;
    [SerializeField] private GameObject bonusPrefab;
    [SerializeField] private GameObject gamePlay;

    private Text timerText;
    private int timer;

    private void Awake()
    {
        timer = 3;
        Time.timeScale = 0f;       
        timerText = panenlStartTimer.GetComponentInChildren<Text>();        
        gamePlay.SetActive(true);
        StartTimer();
    }

    public void StartTimer()
    {
        panenlStartTimer.SetActive(true);
        FoodIgnore();
        StartCoroutine(TimerToPlay());
    }

    private void FoodIgnore()
    {
        foreach (Transform child in foodPrefabs.transform)
        {
            child.gameObject.layer = 2;
        }
        bonusPrefab.layer = 2;
    }

    IEnumerator TimerToPlay()
    {
        for (int i = timer; i > 0; i--)
        {
            timerText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }

        foreach (Transform child in foodPrefabs.transform)
        {
            child.gameObject.layer = 0;
        }

        bonusPrefab.layer = 0;

        panenlStartTimer.SetActive(false);
        Time.timeScale = 1f;

        StopCoroutine(TimerToPlay());
    }
}
