using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WoodGameManager : MonoBehaviour
{
    public static bool GameStart;

    private bool isPause = false; //true일때 pause 상태

    public GameObject WelcomePanel;
    public GameObject DifficultyPanel;
    public GameObject GameOverPanel;
    public GameObject PausePanel;
    public GameObject HPDisablePanel;

    //결과 출력
    public Text ResultTxt;
    public Text ResultCoinTxt;
    public Text ResultExpTxt;

    public float nowTime = 30;                   //게임 플레이 시간
    public Text TimerText;                       //시간제한 텍스트 오브젝트

    public GameObject EventSystem;

    public enum STATE   //현재 게임 상태 저장
    {
        SELECT, START, STOP, IDLE, FAIL, CLEAR
    };
    public STATE state;

    void Start()
    {
        Welcome();
    }

    void Update()
    {
        if (GameStart && !isPause)
        {
            switch (state)
            {
                case STATE.SELECT:
                    DifficultySelect();
                    break;
                case STATE.START:   //게임 시작
                    WoodSet();
                    break;
                case STATE.STOP:    //게임 멈춤
                    GamePause();
                    break;
                case STATE.FAIL:    //시간이 다 되어 게임오버
                    StageFail();
                    break;
                case STATE.CLEAR:    //시간이 다 되어 게임오버
                    Clear();
                    break;
                case STATE.IDLE:    //기본 상태
                    break;
            }
        }
        if (state == STATE.IDLE && nowTime <= 0) //시간 제한이 끝나거면
        {
            GameStart = true;
            state = STATE.FAIL;
        }
        if (state == STATE.IDLE && EventSystem.GetComponent<AxeMove>().Score >= EventSystem.GetComponent<AxeMove>().ScoreMax && nowTime <= 0)   //일정 점수를 획득하고, 시간이 0 이하이면
        {
            GameStart = true;
            state = STATE.CLEAR;
        }
        if (state == STATE.IDLE)
        {
            nowTime -= Time.deltaTime;                                  //현재 타이머에서 전 프레임이 지난 시간을 뺌
            TimerText.text = string.Format("{0:N2}", nowTime);          //타이머 UI에 반영
            if (nowTime <= 0)
            {
                TimerText.text = "0";
            }
        }
    }

    public void Welcome()   //게임 초기화 함수
    {
        GameStart = false;
        isPause = false;
        EventSystem.GetComponent<AxeMove>().Score = 0;

        WelcomePanel.SetActive(true);
        DifficultyPanel.SetActive(true);
        GameOverPanel.SetActive(false);
        PausePanel.SetActive(false);
    }

    public void DifficultySelect()
    {
        int now_hp = PlayerPrefs.GetInt("HP");

        if (now_hp > 0)  //현재 hp가 0보다 크다면
        {
            //hp 1 감소
            PlayInfoManager.GetHP(-1);
            WelcomePanel.SetActive(false);
            DifficultyPanel.SetActive(true);
            state = STATE.SELECT;
        }
        else    //0 이하라면: 게임 플레이 불가
        {
            // hp가 부족합니다! 팝업 띄우기
            HPDisablePanel.SetActive(true);
        }
    }

    public void WoodSet()
    {
        nowTime = 30;
        DifficultyPanel.SetActive(false);
        WelcomePanel.SetActive(false);
        state = STATE.IDLE;
    }

    void Clear()
    {
        GameStart = false;
        GameOverPanel.SetActive(true);
        GameResult();
    }
    public void StageFail()
    {
        GameStart = false;
        GameOverPanel.SetActive(true);
        GameResult();
    }

    void GameResult()   //점수에 따른 보상 획득 메소드
    {
        float get_exp = 10f;
        int get_coin = 5;

        PlayInfoManager.GetExp(get_exp);
        PlayInfoManager.GetCoin(get_coin);

        //보상 결과를 화면에 띄움
        ResultCoinTxt.text = get_coin.ToString();
        ResultExpTxt.text = get_exp.ToString();
    }

    public void GamePause() //일시 정지 누르면 isPause = true, 재개 누르면 isPause = false
    {
        isPause = !isPause;
        if (isPause)
        {
            state = STATE.STOP;
            PausePanel.SetActive(true);
        }
        else
        {
            state = STATE.IDLE;
            PausePanel.SetActive(false);
        }
    }
}