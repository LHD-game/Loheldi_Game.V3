using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxeMove : MonoBehaviour
{
    public GameObject Axe;                       //도끼UI
    public GameObject Log;                       //통나무UI

    public GameObject Wood_Log;                  //통나무(본체)
    public GameObject Wood_Log_Separate;         //통나무(부서지는 프리팹)
    GameObject Wood_Log_Separate_temp;           //통나무(부서지는 프리팹 임시지정)

    bool start = true;

    public GameObject Panel;                     //게임 종료시 진행을 막을 UI, 흔들라는 이미지가 나올때 나올 UI
    public GameObject ReStartButton;             //게임 다시시작 버튼

    float Logx;                                  //통나무 UI x값
    float tilty;                                 //좌우 기울임 값

    public Text ScoreText;                       //점수 텍스트 오브젝트
    public int Score;                            //점수 텍스트 오브젝트

    public void Start()
    {
        Panel.SetActive(true);
        ReStartButton.SetActive(false);

        Wood_Log_Separate_temp = Instantiate(Wood_Log_Separate, Wood_Log_Separate.transform);
        LogInstiate();
    }

    public void Update()
    {
        Axe.transform.localPosition = new Vector2(tilty * 1300f, Axe.transform.localPosition.y);

        if (Axe.transform.localPosition.x == Log.transform.localPosition.x)
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
        }
    }
    public void LogInstiate()
    {
        Logx = Random.Range(1300f, -1300f);                         //도토리가 생성될 범위 랜덤 설정
        Log.transform.localPosition = new Vector2 (Logx, Log.transform.localPosition.y);
    }
}
