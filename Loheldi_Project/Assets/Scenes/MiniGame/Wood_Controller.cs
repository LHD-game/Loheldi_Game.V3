using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Wood_Controller : MonoBehaviour
{
    public GameObject Aim;
    public GameObject Target;

    float tiltx;                                        //핸드폰의 기울기 값
    bool xbool = false;                             //기울어진 축을 읽기위한 값

    bool limit = true;

    public GameObject Panel;                //게임 종료
    public GameObject ReStartButton;        //게임 다시시작 버튼
    int Score = -1;                    //점수   

    [SerializeField]
    public static float nowTime = 30;       //게임 플레이 시간

    public Text TimerText;                  //시간제한 텍스트 오브젝트
    public Text ScoreText;                      //점수 텍스트 오브젝트



    void Start()
    {

    }

    void Update()
    {
        if(Target.transform.localPosition.x == Aim.transform.localPosition.x){
            Target.transform.localPosition = new Vector3(Random.Range(-375,375),233,0); //Target 랜덤이동
            Score++;                          //점수 올리기
            ScoreText.text = Score.ToString();    //점수 UI로 반영
        }

        
        tiltx = Input.acceleration.x;
        if((tiltx >= 0.2f || tiltx <= -0.2f) && Mathf.Abs(Aim.transform.localPosition.x) <= Mathf.Abs(375)){      //기울었을 때 이동
        this.transform.position = this.transform.position + new Vector3(tiltx*5,0,0);       //이동
        }

        if(Input.GetKey(KeyCode.RightArrow) && Aim.transform.localPosition.x < 375){                                                   //키보드 이동
        Aim.transform.position = Aim.transform.position + new Vector3(1,0,0);
        }
        if(Input.GetKey(KeyCode.LeftArrow) && Aim.transform.localPosition.x > -375){
        Aim.transform.position = Aim.transform.position + new Vector3(-1,0,0);
        }

        nowTime -= Time.deltaTime;                                  //현재 타이머에서 전 프레임이 지난 시간을 뺌
        if (nowTime <= 0 || Score >=20)                        //시간 제한이 끝나거나 일정 점수를 획득하면( 프로토타입용 임시, 분리예정)
        {
            nowTime = 0;
            Panel.SetActive(true);                                  //게임 진행 정지
            ReStartButton.SetActive(true);                          //게임 재시작 버튼
        }
        TimerText.text = string.Format(" :  {0:N2}", nowTime);      //타이머 UI
    }
}
