using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxeMove : MonoBehaviour
{
    public GameObject Axe;                       //����UI
    public GameObject Log;                       //�볪��UI
    public GameObject Effect;                       //����UI

    public GameObject Wood_Log;                  //�볪��(��ü)
    public GameObject Wood_Log_Separate;         //�볪��(�μ����� ������)
    GameObject Wood_Log_Separate_temp;           //�볪��(�μ����� ������ �ӽ�����)

    bool start = true;

    public GameObject Panel;                     //���� ����� ������ ���� UI, ����� �̹����� ���ö� ���� UI
    public GameObject ReStartButton;             //���� �ٽý��� ��ư

    int AllowArea = 100;                         //������(������ �볪���� ��ġ�� ����)
    float Logx;                                  //�볪�� UI x��
    float tilty;                                 //�¿� ����� ��

    public float AllowTime;                             //��ģ �ð�
    public float AllowTimeMax;
    public Slider AllowSlider;                          //��ģ �ð��� ǥ���ϴ� �����̴�

    public Text ScoreText;                              //���� �ؽ�Ʈ ������Ʈ
    public int Score = 0;                               //���� �ؽ�Ʈ ������Ʈ
    public int ScoreMax = 0;                            //���� �ؽ�Ʈ ������Ʈ

    public GameObject WoodGameManager;

    public void Start()
    {
        Effect.SetActive(false);
        Panel.SetActive(true);
        AllowTime = 0;

        Wood_Log_Separate_temp = Instantiate(Wood_Log_Separate, Wood_Log_Separate.transform);
        if (Axe.transform.localPosition.x + AllowArea >= Log.transform.localPosition.x && Axe.transform.localPosition.x - AllowArea <= Log.transform.localPosition.x)
        {
            Wood_Log_Separate_temp = Instantiate(Wood_Log_Separate, Wood_Log_Separate.transform); if (Axe.transform.localPosition.x + AllowArea >= Log.transform.localPosition.x && Axe.transform.localPosition.x - AllowArea <= Log.transform.localPosition.x)
            {
                Wood_Log_Separate_temp = Instantiate(Wood_Log_Separate, Wood_Log_Separate.transform);
            }
        }

            LogInstiate();
    }

    public void Update()
    {
        Axe.transform.localPosition = new Vector2(tilty * 1300f, Axe.transform.localPosition.y);

        AllowSlider.value = AllowTime;
        if (Axe.transform.localPosition.x + AllowArea >= Log.transform.localPosition.x && Axe.transform.localPosition.x - AllowArea <= Log.transform.localPosition.x)
        {
            Effect.SetActive(true);

            AllowTime += Time.deltaTime;   //��ģ �ð� ����
            if (AllowTime >= AllowSlider.maxValue)
            {
                Score++;
                ScoreText.text = Score.ToString();
                if (!start)
                {
                    Wood_Log_Separate_temp.transform.GetChild(0).gameObject.GetComponent<MeshCollider>().enabled = false;
                    Wood_Log_Separate_temp.transform.GetChild(1).gameObject.GetComponent<MeshCollider>().enabled = false;
                }
                Wood_Log.SetActive(true);
                Wood_Log_Separate_temp = Instantiate(Wood_Log_Separate, Wood_Log.transform);
                LogInstiate();
                start = false;
                AllowTime = 0;
            }
        }
        else
        {
            Effect.SetActive(false);
            AllowTime -= Time.deltaTime;   //��ģ �ð� ����
            if(AllowTime <= 0)
            {
                AllowTime = 0;
            }
        }
    }
    public void LogInstiate()
    {
        Logx = Random.Range(1200f, -1200f);
        Log.transform.localPosition = new Vector2 (Logx, Log.transform.localPosition.y);
    }

    public void DifficultyWoodEasy()
    {
        AllowSlider.maxValue = 2;
        ScoreMax = 3;
        WoodGameManager.GetComponent<WoodGameManager>().WoodSet();
    }
    public void DifficultyWoodNormal()
    {
        AllowSlider.maxValue = 3;
        ScoreMax = 4;
        WoodGameManager.GetComponent<WoodGameManager>().WoodSet();
    }
    public void DifficultyWoodHard()
    {
        AllowSlider.maxValue = 4;
        ScoreMax = 5;
        WoodGameManager.GetComponent<WoodGameManager>().WoodSet();
    }
}
