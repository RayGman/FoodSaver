using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private Timer timer;
    public void Stop(float gameTime)
    {
        if (gameTime == 1f)
        {
            timer.StartTimer();
        }
        else { Time.timeScale = 0f; }
    }
}
