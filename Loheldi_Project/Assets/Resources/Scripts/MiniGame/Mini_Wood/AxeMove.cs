using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxeMove : MonoBehaviour
{
    public GameObject Axe;                       //도끼UI
    public GameObject Log;                       //통나무UI
    public GameObject Effect;                       //도끼UI

    public GameObject Wood_Log;                  //통나무(본체)
    public GameObject Wood_Log_Separate;         //통나무(부서지는 프리팹)
    GameObject Wood_Log_Separate_temp;           //통나무(부서지는 프리팹 임시지정)

    bool start = true;

    public GameObject Panel;                     //게임 종료시 진행을 막을 UI, 흔들라는 이미지가 나올때 나올 UI
    public GameObject ReStartButton;             //게임 다시시작 버튼

    int AllowArea = 100;                         //허용범위(도끼가 통나무와 겹치는 범위)
    float Logx;                                  //통나무 UI x값
    float tilty;                                 //좌우 기울임 값

    public float AllowTime;                             //겹친 시간
    public float AllowTimeMax;
    public Slider AllowSlider;                          //겹친 시간을 표시하는 슬라이더

    public Text ScoreText;                              //점수 텍스트 오브젝트
    public int Score = 0;                               //점수 텍스트 오브젝트
    public int ScoreMax = 0;                            //점수 텍스트 오브젝트

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

            AllowTime += Time.deltaTime;   //겹친 시간 증가
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
            AllowTime -= Time.deltaTime;   //겹친 시간 증가
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
