using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxeMove : MonoBehaviour
{
    public GameObject Axe;
    public GameObject Log;

    public GameObject Panel;                //게임 종료시 진행을 막을 UI, 흔들라는 이미지가 나올때 나올 UI
    public GameObject ReStartButton;        //게임 다시시작 버튼

    float Logx;
    float tilty;

    int WoodCount = 0;                      //도토리 갯수(바구니 리셋용)

    [SerializeField]
    public static float nowTime = 30;       //게임 플레이 시간
    public static float LastShakeTime = 2;  //마지막으로 흔들린 시간

    public Text TimerText;                  //시간제한 텍스트 오브젝트
    public Text ScoreText;                      //점수 텍스트 오브젝트
    public int Score;                      //점수 텍스트 오브젝트

    public void Start()
    {
        Panel.SetActive(true);
        ReStartButton.SetActive(false);

        LogInstiate();
    }

    public void Update()
    {


        nowTime -= Time.deltaTime;                                  //현재 타이머에서 전 프레임이 지난 시간을 뺌
        LastShakeTime -= Time.deltaTime;
        if (nowTime <= 0 || Score >= 3)                        //시간 제한이 끝나거나 일정 점수를 획득하면( 프로토타입용 임시, 분리예정)
        {
            nowTime = 0;                                            //시간 0으로 고정
            Panel.SetActive(true);                                  //게임 진행 정지용 판넬 등장
            ReStartButton.SetActive(true);                          //게임 제시작 버튼 두두둥장
        }
        TimerText.text = string.Format(" : {0:N2}", nowTime);      //타이머 UI에 반영
    }
    public void LogInstiate()
    {
        Logx = Random.Range(1300, -1300);          //도토리가 생성될 범위 랜덤 설정
        Log.transform.localPosition = new Vector2 (Logx, Log.transform.localPosition.y);
    }
}
