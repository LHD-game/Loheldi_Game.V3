using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shaking : MonoBehaviour            //흔드는거 감지하는 함수
{
    public GameObject Acorn;                    //도토리 오브젝트(프리펩)
    public GameObject Acorns;                   //바구니 속 도토리 오브젝트
    float Dropx;                            //도토리 생성 범위
    float Dropy;
    float Dropz;

    float tiltx;                            //핸드폰의 기울기 값
    float tilty;
    float tiltz;

    bool xbool = false;                     //기울어진 축을 읽기위한 값
    bool ybool = false;
    bool zbool = false;

    public GameObject Drop;                 //생성될 도토리들의 부모 오브젝트
    public GameObject Panel;                //게임 종료시 진행을 막을 UI, 흔들라는 이미지가 나올때 나올 UI
    public GameObject PleaseShake;          //흔들라는 이미지 UI
    public GameObject ReStartButton;        //게임 다시시작 버튼


    int DropCount = 0;                      //도토리 갯수(바구니 리셋용)
    int DropCount2 = 0;                     //도토리 갯수(임시)
    int BasketScore = 0;                    //채운 바구니 갯수(점수)

    [SerializeField]
    public static float nowTime = 9999;       //게임 플레이 시간
    public static float LastShakeTime = 2;  //마지막으로 흔들린 시간

    public Text TimerText;                  //시간제한 텍스트 오브젝트
    public Text Score;                      //점수 텍스트 오브젝트

    public void Shake()                         //"흔들림"을 인식했을때
    {
        Panel.SetActive(false);                     //판넬 치우기
        PleaseShake.SetActive(false);               //흔들기 UI도 치우기
        LastShakeTime = 2;                          //마지막으로 흔들린 시간 초기화
        Dropx = Random.Range(0.5f, -0.5f);          //도토리가 생성될 범위 랜덤 설정
        Dropy = 1f;
        Dropz = Random.Range(0.5f, -0.5f);
        GameObject DropAcorn = Instantiate(Acorn, new Vector3(Dropx, Dropy, Dropz), Quaternion.identity);   //도토리 생성
        DropAcorn.transform.parent = Drop.transform;
        DropCount++;                                //도토리 갯수 카운트( 1은 바구니 리셋용, 2가 점수용 )
        DropCount2++;
        Acorns.transform.localPosition = new Vector3(-0.00011f, -0.00047f, Acorns.transform.localPosition.z + 0.00075f);     //바구니속 도토리 위치 설정
        if (DropCount >= 20)                        //바구니 리셋 함수
        {
            Handheld.Vibrate();                     //바구니 리셋시 진동
            Acorns.transform.localPosition = new Vector3(-0.00011f, -0.00047f, -0.01087f);                  //바구니속 도토리 위치 초기화
            DropCount = 0;                          //바구니 리셋용 도토리 갯수 카운트 초기화
            BasketScore++;                          //점수 올리기
            Score.text = BasketScore.ToString();    //점수 UI로 반영
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
    }

    void Update()
    {
        //변수 x, y, z 에 tilt센서 값을 입력
        tiltx = Input.acceleration.x;   //Input.acceleration : 모바일 디바이스의 Tilt값(벡터 아님, 읽기 전용, 축마다 범위는 1 ~ -1)
        tilty = Input.acceleration.y;
        tiltz = Input.acceleration.z;

        if (tiltx <= 0.2f && tiltx >= -0.2 && xbool == false)       //x축이 범위 밖으로 나감
        {
            xbool = true;                                       //x축 기울어짐
        }
        else if ((tiltx > 0.2f || tiltx < -0.2) && xbool == true)   //x축이 다시 범위 안으로 들어옴
        {
            xbool = false;                                      //x축 초기화
            Shake();                                            //핸드폰이 흔들렸음
        }

        if (tilty <= 0.2f && tilty >= -0.2 && ybool == false)       //y축 이하동일
        {
            ybool = true;
        }
        else if ((tilty > 0.2f || tilty < -0.2) && ybool == true)
        {
            ybool = false;
            Shake();
        }

        if (tiltz <= 0.2f && tiltz >= -0.2 && zbool == false)       //z축 이하동일
        {
            zbool = true;
        }
        else if ((tiltz > 0.2f || tiltz < -0.2) && zbool == true)
        {
            zbool = false;
            Shake();
        }

        nowTime -= Time.deltaTime;                                  //현재 타이머에서 전 프레임이 지난 시간을 뺌
        LastShakeTime -= Time.deltaTime;
        if (nowTime <= 0 || BasketScore >=3)                        //시간 제한이 끝나거나 일정 점수를 획득하면( 프로토타입용 임시, 분리예정)
        {
            nowTime = 0;                                            //시간 0으로 고정
            Panel.SetActive(true);                                  //게임 진행 정지용 판넬 등장
            ReStartButton.SetActive(true);                          //게임 제시작 버튼 두두둥장
        }
        if (LastShakeTime <= 0)                                     //일정시간동안 흔들리지 않았으면
        {
            Panel.SetActive(true);                                  //판넬UI등장
            PleaseShake.SetActive(true);                            //흔들어주세요
        }
        TimerText.text = string.Format(" : {0:N2}", nowTime);      //타이머 UI에 반영
    }
}
