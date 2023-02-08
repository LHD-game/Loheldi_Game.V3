using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRandomMove : MonoBehaviour
{

    Rigidbody rigid;
    public float nextMove1;//다음 행동지표를 결정할 변수
    public float nextMove2;

    float time = 5f;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        Debug.Log("1");
        Invoke("Thinking", time); // 초기화 함수 안에 넣어서 실행될 때 마다(최초 1회) nextMove변수가 초기화 되도록함
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = new Vector3(nextMove1, rigid.velocity.y, nextMove2); //nextMove 에 0:멈춤 -1:왼쪽 1:오른쪽 으로 이동 
    }

    public void Thinking()
    {//몬스터가 스스로 생각해서 판단 (-1:왼쪽이동 ,1:오른쪽 이동 ,0:멈춤  으로 3가지 행동을 판단)

        Debug.Log("2");
        //Random.Range : 최소<= 난수 <최대 /범위의 랜덤 수를 생성(최대는 제외이므로 주의해야함)
        nextMove1 = Random.Range(-1f, 2f);
        nextMove2 = Random.Range(-1f, 2f);

        time = Random.Range(2f, 5f); //생각하는 시간을 랜덤으로 부여 
        //Think(); : 재귀함수 : 딜레이를 쓰지 않으면 CPU과부화 되므로 재귀함수쓸 때는 항상 주의 ->Think()를 직접 호출하는 대신 Invoke()사용
        Invoke("Thinking", time); //매개변수로 받은 함수를 time초의 딜레이를 부여하여 재실행 
    }
}