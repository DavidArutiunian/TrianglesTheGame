﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ToolbarController : MonoBehaviour
{
    private static readonly char DASH = '–';
    private static readonly float FPS = 60f;

    // public bool active = true;
    private int current = 0;
    // private int prev = 0;

    public GameObject RadialTimer;
    // public Text display;
    public Text score;
    public Text level;

    void Awake()
    {
        ResetTimer();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.instance.OnCountRestart += DoOnCountRestart;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Restart()
    {
        // SetActive();
        ResetTimerFill();
        StartCoroutine(CountdownStart());
        StartCoroutine(RadialTimerStart());
    }

    private float GetTimerStep()
    {
        return (1f / (float)GameStore.instance.timer) / (1f / Time.deltaTime);
    }

    private void DoOnCountRestart()
    {
        // Show();
        StopAllCoroutines();
        ResetTimer();
        Restart();
        UpdateScoreText();
        UpdateLevelText();
    }

    private IEnumerator RadialTimerStart()
    {
        while (IsRunning())
        {
            // if (!active)
            // {
            //     yield break;
            // }
            RadialTimer.GetComponent<Image>().fillAmount += GetTimerStep();
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator CountdownStart()
    {
        while (IsRunning())
        {
            // if (!active)
            // {
            //     yield break;
            // }
            // UpdateText();
            yield return new WaitForSeconds(1f);
            DecrementTimer();
        }
        // UpdateText();
        GameEvents.instance.TriggerCountEnd();
    }

    private void ResetTimer()
    {
        // prev = current;
        current = GameStore.instance.timer;
    }

    private void DecrementTimer()
    {
        current--;
    }

    private bool IsRunning()
    {
        return current > 0;
    }

    private void UpdateScoreText()
    {
        // if (GameStore.instance.win || GameStore.instance.loose)
        // {
        //     display.text = prev.ToString();
        // }
        // else
        // {
        //     display.text = current.ToString();
        // }
        score.text = GameStore.instance.score.ToString();
    }

    private void UpdateLevelText()
    {
        string weight = (GameStore.instance.GetAbsoluteWeight() + 1).ToString();
        string step = (GameStore.instance.step + 1).ToString();
        level.text = $"{weight}{DASH}{step}";
    }

    // private void SetActive()
    // {
    //     active = true;
    // }

    // private void SetInactive()
    // {
    //     active = false;
    // }

    public void HideTimer()
    {
        RadialTimer.SetActive(false);
    }

    public void ShowTimer()
    {
        RadialTimer.SetActive(true);
    }

    private void ResetTimerFill()
    {
        RadialTimer.GetComponent<Image>().fillAmount = 0f;
    }
}
