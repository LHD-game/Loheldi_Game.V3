using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shaking : MonoBehaviour            //���°� �����ϴ� �Լ�
{
    public GameObject Acorn;                    //���丮 ������Ʈ(������)
    public GameObject Acorns;                   //�ٱ��� �� ���丮 ������Ʈ
    float Dropx;                            //���丮 ���� ����
    float Dropy;
    float Dropz;

    float tiltx;                            //�ڵ����� ���� ��
    float tilty;
    float tiltz;

    bool xbool = false;                     //������ ���� �б����� ��
    bool ybool = false;
    bool zbool = false;

    public GameObject Drop;                 //������ ���丮���� �θ� ������Ʈ
    public GameObject Panel;                //���� ����� ������ ���� UI, ����� �̹����� ���ö� ���� UI
    public GameObject PleaseShake;          //����� �̹��� UI
    public GameObject ReStartButton;        //���� �ٽý��� ��ư


    public static bool GameStart;

    private bool isPause = false; //true�϶� pause ����

    public GameObject WelcomePanel;
    public GameObject GameOverPanel;
    public GameObject PausePanel;

    //��� ���
    public Text ResultTxt;
    public Text ResultCoinTxt;
    public Text ResultExpTxt;

    public float nowTime = 30;                   //���� �÷��� �ð�
    public Text TimerText;                       //�ð����� �ؽ�Ʈ ������Ʈ
    public static float LastShakeTime = 2;       //���������� ��鸰 �ð�
    public Text Score;                           //���� �ؽ�Ʈ ������Ʈ

    public bool SquirrelMoveBool = false;
    public GameObject SquirrelMove;
    public GameObject BasketMove;

    public GameObject EventSystem;

    public enum STATE   //���� ���� ���� ����
    {
        START, STOP, IDLE, FAIL, CLEAR
    };
    static public STATE state;

    int DropCount = 0;                      //���丮 ����(�ٱ��� ���¿�)
    int DropCount2 = 0;                     //���丮 ����(�ӽ�)
    int BasketScore = 0;                    //ä�� �ٱ��� ����(����)

    public void Shake()                         //"��鸲"�� �ν�������
    {
        Panel.SetActive(false);                     //�ǳ� ġ���
        PleaseShake.SetActive(false);               //���� UI�� ġ���
        LastShakeTime = 2;                          //���������� ��鸰 �ð� �ʱ�ȭ
        Dropx = Random.Range(0.5f, -0.5f);          //���丮�� ������ ���� ���� ����
        Dropy = 1f;
        Dropz = Random.Range(0.5f, -0.5f);
        GameObject DropAcorn = Instantiate(Acorn, new Vector3(Dropx, Dropy, Dropz), Quaternion.identity);   //���丮 ����
        DropAcorn.transform.parent = Drop.transform;
        DropCount++;                                //���丮 ���� ī��Ʈ( 1�� �ٱ��� ���¿�, 2�� ������ )
        DropCount2++;
        Acorns.transform.localPosition = new Vector3(-0.00011f, -0.00047f, Acorns.transform.localPosition.z + 0.00075f);     //�ٱ��ϼ� ���丮 ��ġ ����
        if (DropCount >= 20)                        //�ٱ��� ���� �Լ�
        {
            SquirrelMoveBool = true;
            Vibration.Vibrate();                     //�ٱ��� ���½� ����
            Acorns.transform.localPosition = new Vector3(-0.00011f, -0.00047f, -0.01087f);                  //�ٱ��ϼ� ���丮 ��ġ �ʱ�ȭ
            BasketScore++;                          //���� �ø���
            Score.text = BasketScore.ToString();    //���� UI�� �ݿ�
            DropCount = 0;                          //�ٱ��� ���¿� ���丮 ���� ī��Ʈ �ʱ�ȭ
        }
    }

    private void Start()
    {
        DropCount = 0;
        DropCount2 = 0;
        BasketScore = 0;
        nowTime = 30;
        Panel.SetActive(true);
        PleaseShake.SetActive(true);
        ReStartButton.SetActive(false);
        GameOverPanel.SetActive(false);
        BasketMove.SetActive(false);
    }

    void Update()
    {
        if (GameStart && !isPause)
        {
            switch (state)
            {
                case STATE.START:   //���� ����
                    ShakeSet();
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
        if (BasketScore >= 3)   //���� ������ ȹ���ϸ�
        {
            GameStart = true;
            state = STATE.CLEAR;
        }
        if (state == STATE.IDLE)
        {
            nowTime -= Time.deltaTime;                                  //���� Ÿ�̸ӿ��� �� �������� ���� �ð��� ��
            TimerText.text = string.Format(" : {0:N2}", nowTime);       //Ÿ�̸� UI�� �ݿ�

            //���� x, y, z �� tilt���� ���� �Է�
            tiltx = Input.acceleration.x;   //Input.acceleration : ����� ����̽��� Tilt��(���� �ƴ�, �б� ����, �ึ�� ������ 1 ~ -1)
            tilty = Input.acceleration.y;
            tiltz = Input.acceleration.z;

            if (tiltx <= 0.2f && tiltx >= -0.2 && xbool == false)       //x���� ���� ������ ����
            {
                xbool = true;                                       //x�� ������
            }
            else if ((tiltx > 0.2f || tiltx < -0.2) && xbool == true)   //x���� �ٽ� ���� ������ ����
            {
                xbool = false;                                      //x�� �ʱ�ȭ
                Shake();                                            //�ڵ����� ������
            }

            if (tilty <= 0.2f && tilty >= -0.2 && ybool == false)       //y�� ���ϵ���
            {
                ybool = true;
            }
            else if ((tilty > 0.2f || tilty < -0.2) && ybool == true)
            {
                ybool = false;
                Shake();
            }

            if (tiltz <= 0.2f && tiltz >= -0.2 && zbool == false)       //z�� ���ϵ���
            {
                zbool = true;
            }
            else if ((tiltz > 0.2f || tiltz < -0.2) && zbool == true)
            {
                zbool = false;
                Shake();
            }

            nowTime -= Time.deltaTime;                                  //���� Ÿ�̸ӿ��� �� �������� ���� �ð��� ��
            LastShakeTime -= Time.deltaTime;
            if (nowTime <= 0 || BasketScore >= 3)                        //�ð� ������ �����ų� ���� ������ ȹ���ϸ�( ������Ÿ�Կ� �ӽ�, �и�����)
            {
                nowTime = 0;                                            //�ð� 0���� ����
                Panel.SetActive(true);                                  //���� ���� ������ �ǳ� ����
                ReStartButton.SetActive(true);                          //���� ������ ��ư �εε���
            }
            if (LastShakeTime <= 0)                                     //�����ð����� ��鸮�� �ʾ�����
            {
                Panel.SetActive(true);                                  //�ǳ�UI����
                PleaseShake.SetActive(true);                            //�����ּ���
            }
            TimerText.text = string.Format(" : {0:N2}", nowTime);       //Ÿ�̸� UI�� �ݿ�
        }
        if (SquirrelMoveBool)                                           //���丮 ���İ��� �ٶ��� ������
        {
            Vector3 temp = SquirrelMove.transform.position;
            temp.z += 0.005f;                                           //�ٶ��� �ӵ�
            SquirrelMove.transform.position = temp;
            if (SquirrelMove.transform.position.z >= -0.21)             //�ٶ��㰡 �ٱ��Ͽ� ��������� �ٶ��㰡 �� �ٱ��� ����
            {
                BasketMove.SetActive(true);
            }
            if (SquirrelMove.transform.position.z >= 3)                 //�ٶ��㰡 ���� ������ ������ �ʱ�ȭ
            {
                temp.z = -3f;                                           //�ٶ��㰡 �ʱ�ȭ�� (ó��)��ġ
                SquirrelMove.transform.position = temp;
                BasketMove.SetActive(false);
                SquirrelMoveBool = false;
            }
        }
    }

    public void Welcome()
    {
        DropCount = 0;
        DropCount2 = 0;
        BasketScore = 0;
        nowTime = 30;
        Panel.SetActive(true);
        PleaseShake.SetActive(true);
        ReStartButton.SetActive(false);
        GameOverPanel.SetActive(false);
        BasketMove.SetActive(false);
    }

    public void ShakeSet()
    {
        nowTime = 30;
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
