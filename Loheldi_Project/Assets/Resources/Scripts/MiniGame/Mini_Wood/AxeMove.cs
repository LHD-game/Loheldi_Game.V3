using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxeMove : MonoBehaviour
{
    public GameObject Axe;
    public GameObject Log;

    public GameObject Panel;                //���� ����� ������ ���� UI, ����� �̹����� ���ö� ���� UI
    public GameObject ReStartButton;        //���� �ٽý��� ��ư

    float Logx;
    float tilty;

    int WoodCount = 0;                      //���丮 ����(�ٱ��� ���¿�)

    [SerializeField]
    public static float nowTime = 30;       //���� �÷��� �ð�
    public static float LastShakeTime = 2;  //���������� ��鸰 �ð�

    public Text TimerText;                  //�ð����� �ؽ�Ʈ ������Ʈ
    public Text ScoreText;                      //���� �ؽ�Ʈ ������Ʈ
    public int Score;                      //���� �ؽ�Ʈ ������Ʈ

    public void Start()
    {
        Panel.SetActive(true);
        ReStartButton.SetActive(false);

        LogInstiate();
    }

    public void Update()
    {


        nowTime -= Time.deltaTime;                                  //���� Ÿ�̸ӿ��� �� �������� ���� �ð��� ��
        LastShakeTime -= Time.deltaTime;
        if (nowTime <= 0 || Score >= 3)                        //�ð� ������ �����ų� ���� ������ ȹ���ϸ�( ������Ÿ�Կ� �ӽ�, �и�����)
        {
            nowTime = 0;                                            //�ð� 0���� ����
            Panel.SetActive(true);                                  //���� ���� ������ �ǳ� ����
            ReStartButton.SetActive(true);                          //���� ������ ��ư �εε���
        }
        TimerText.text = string.Format(" : {0:N2}", nowTime);      //Ÿ�̸� UI�� �ݿ�
    }
    public void LogInstiate()
    {
        Logx = Random.Range(1300, -1300);          //���丮�� ������ ���� ���� ����
        Log.transform.localPosition = new Vector2 (Logx, Log.transform.localPosition.y);
    }
}
