using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WoodGameManager : MonoBehaviour
{
    public static bool GameStart;

    private bool isPause = false; //true�϶� pause ����

    public GameObject WelcomePanel;
    public GameObject DifficultyPanel;
    public GameObject GameOverPanel;
    public GameObject PausePanel;
    public GameObject HPDisablePanel;

    //��� ���
    public Text ResultTxt;
    public Text ResultCoinTxt;
    public Text ResultExpTxt;

    public float nowTime = 30;                   //���� �÷��� �ð�
    public Text TimerText;                       //�ð����� �ؽ�Ʈ ������Ʈ

    public GameObject EventSystem;

    public enum STATE   //���� ���� ���� ����
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
                case STATE.START:   //���� ����
                    WoodSet();
                    break;
                case STATE.STOP:    //���� ����
                    GamePause();
                    break;
                case STATE.FAIL:    //�ð��� �� �Ǿ� ���ӿ���
                    StageFail();
                    break;
                case STATE.CLEAR:    //�ð��� �� �Ǿ� ���ӿ���
                    Clear();
                    break;
                case STATE.IDLE:    //�⺻ ����
                    break;
            }
        }
        if (state == STATE.IDLE && nowTime <= 0) //�ð� ������ �����Ÿ�
        {
            GameStart = true;
            state = STATE.FAIL;
        }
        if (state == STATE.IDLE && EventSystem.GetComponent<AxeMove>().Score >= EventSystem.GetComponent<AxeMove>().ScoreMax)   //���� ������ ȹ���ϸ�
        {
            GameStart = true;
            state = STATE.CLEAR;
        }
        if (state == STATE.IDLE)
        {
            nowTime -= Time.deltaTime;                                  //���� Ÿ�̸ӿ��� �� �������� ���� �ð��� ��
            TimerText.text = string.Format("{0:N2}", nowTime);       //Ÿ�̸� UI�� �ݿ�
        }
    }

    public void Welcome()   //���� �ʱ�ȭ �Լ�
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

        if (now_hp > 0)  //���� hp�� 0���� ũ�ٸ�
        {
            //hp 1 ����
            PlayInfoManager.GetHP(-1);
            WelcomePanel.SetActive(false);
            DifficultyPanel.SetActive(true);
            state = STATE.SELECT;
        }
        else    //0 ���϶��: ���� �÷��� �Ұ�
        {
            // hp�� �����մϴ�! �˾� ����
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

    void GameResult()   //������ ���� ���� ȹ�� �޼ҵ�
    {
        float get_exp = 10f;
        int get_coin = 5;

        PlayInfoManager.GetExp(get_exp);
        PlayInfoManager.GetCoin(get_coin);

        //���� ����� ȭ�鿡 ���
        ResultCoinTxt.text = get_coin.ToString();
        ResultExpTxt.text = get_exp.ToString();
    }

    public void GamePause() //�Ͻ� ���� ������ isPause = true, �簳 ������ isPause = false
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