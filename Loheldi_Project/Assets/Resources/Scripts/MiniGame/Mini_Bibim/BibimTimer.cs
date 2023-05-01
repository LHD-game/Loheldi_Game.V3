using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BibimTimer : MonoBehaviour
{
    public Mini_BibimbabMainScript Bibim;

    private static Timer _instance;
    public Slider[] WaitS;

    public static Timer instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Timer>();
            }
            return _instance;
        }
    }

    public float time_current = 90;
    private float time_current_tmp;
    public bool isRun = false;
    public bool isPause = false;
    int gLevel = 0;

    [SerializeField]
    private Text timeTxt;

    private void Update()
    {
        if (isRun && !isPause)
        {
            CheckTimer();
        }
    }

    private void CheckTimer()
    {
        time_current -= Time.deltaTime;
        timeTxt.text = $"{time_current:N1}";
        if (time_current <= 0)
        {
            EndTimer();

        }

    }

    public void EndTimer()
    {
        timeTxt.text = $"{time_current:N1}";
        isRun = false;
        Bibim.GameEnd();
    }


    public void StartTimer()
    {
        gLevel = 0;
        time_current = 0;
        time_current_tmp = 0;
        timeTxt.text = $"{time_current:N1}";
        isRun = true;
        isPause = false;
        Time.timeScale = 1f;
    }

    public void PauseTimer()  //일시정지
    {
        if (!isPause)
        {
            Bibim.PausePanel.SetActive(true);
            isPause = true;
            Time.timeScale = 0f;
        }
        else
        {
            Bibim.PausePanel.SetActive(false);
            isPause = false;
            Time.timeScale = 1f;
        }

    }

    public IEnumerator WaitTimer(int i)
    {
        WaitS[i].value = WaitS[i].maxValue;
        while (WaitS[i].value > 0)
        {
            WaitS[i].value -= 0.1f;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        Debug.Log(i + "타임아웃");
        Bibim.ResetGuest(i);
    }
}
