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
    public GameObject GameOverPanel;
    public GameObject PausePanel;

    //��� ���
    public Text ResultTxt;
    public Text ResultCoinTxt;
    public Text ResultExpTxt;

    public float nowTime = 30;            //���� �÷��� �ð�
    public Text TimerText;                       //�ð����� �ؽ�Ʈ ������Ʈ

    public GameObject EventSystem;

    public enum STATE   //���� ���� ���� ����
    {
        START, STOP, IDLE, FAIL, CLEAR
    };
    static public STATE state;

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
        if (nowTime <= 0) //�ð� ������ �����Ÿ�
        {
            GameStart = true;
            state = STATE.FAIL;
        }
        if (EventSystem.GetComponent<AxeMove>().Score >= 3)   //���� ������ ȹ���ϸ�
        {
            GameStart = true;
            state = STATE.CLEAR;
        }
        if (state == STATE.IDLE)
        {
            nowTime -= Time.deltaTime;                                  //���� Ÿ�̸ӿ��� �� �������� ���� �ð��� ��
            TimerText.text = string.Format(" : {0:N2}", nowTime);       //Ÿ�̸� UI�� �ݿ�
        }
    }

    public void Welcome()   //���� �ʱ�ȭ �Լ�
    {
        state = STATE.START;
        GameStart = false;
        isPause = false;

        WelcomePanel.SetActive(true);
        GameOverPanel.SetActive(false);
        PausePanel.SetActive(false);
    }

    public void WoodSet()
    {
        nowTime = 30;
        WelcomePanel.SetActive(false);
        state = STATE.IDLE;
    }

    void Clear()
    {
        GameOverPanel.SetActive(true);
        GameStart = false;
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