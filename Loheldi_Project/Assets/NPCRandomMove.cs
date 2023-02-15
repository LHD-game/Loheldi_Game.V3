using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRandomMove : MonoBehaviour
{

    Rigidbody rigid;
    
    public float nextMove1;                   //박스 생성 위치값 (1 = x, 2 = y)
    public float nextMove2;

    float time = 5f;                          //각 행동에 드는 시간

    public float SmoothTime = 0.1f;           //보간도
    private Vector3 velocity = Vector3.zero;  //NPC가 가질 가속도값

    public GameObject boxorigin;              //박스 프리팹(뭐든 상관 없음)
    public GameObject Box;                    //생성될 박스(박스 위치로 이동함)

    bool Move = false;                        //현재 이동, 멈춤을 확인할 값

    void Awake()
    {
        nextMove1 = Random.Range(-1f, 2f);    //생성 위치 랜덤
        nextMove2 = Random.Range(-1f, 2f);
        Box = Instantiate(boxorigin, new Vector3(this.gameObject.transform.position.x + nextMove1, -5.4f, this.gameObject.transform.position.z + nextMove2), Quaternion.identity);
        Box.transform.parent = this.transform.parent;
        rigid = GetComponent<Rigidbody>();    //↑ NPC주변에 랜덤한 범위에 Box생성
        Invoke("Stop", time);                 //처음에는 Stop Invoke 실행
    }

    void FixedUpdate()
    {
        if (Move)
        {

            this.gameObject.transform.position = Vector3.SmoothDamp(transform.position, Box.transform.position, ref velocity, SmoothTime);     //해당위치로 NPC천천히 이동
            transform.LookAt(new Vector3(Box.transform.position.x, transform.position.y, Box.transform.position.z));                           //Box위치를 보고있기
            if (Vector3.Distance(this.transform.position, Box.transform.position) <= 0.2f)                                                     //박스위치와 본인위치의 거리를 비교해서 도착하면 자동으로 멈춤
            {
                Move = false;                  //움직이지 않음
                CancelInvoke();                //Move Invoke 정지
                Invoke("Stop", 0f);            //Stop Invoke 즉시 실행
            }
        }
    }

    public void MoveBox()
    {
        Move = true;                           //움직임

        nextMove1 = Random.Range(-1f, 2f);     //생성 위치 랜덤
        nextMove2 = Random.Range(-1f, 2f);

        Box.transform.position = new Vector3(this.gameObject.transform.position.x + nextMove1, -5.4f, this.gameObject.transform.position.z + nextMove2);
                                               //↑ NPC주변에 랜덤한 범위에 Box생성
        time = Random.Range(5f, 8f);           //움직이는 시간을 랜덤으로 부여 
        CancelInvoke();                        //Stop Invoke 정지
        Invoke("Stop", time);                  //time시간 뒤 Stop Invoke 실행 (time 시간동안 이동)
    }
    public void Stop()
    {
        Move = false;                          //움직이지 않음
        rigid.velocity = new Vector3(0,0,0);   //가속도 초기화
        time = Random.Range(5f, 8f);           //멈춰있는 시간을 랜덤으로 부여 
        CancelInvoke();                        //Move Invoke 정지
        Invoke("MoveBox", time);               //time시간 뒤 Move Invoke 실행
    }
}