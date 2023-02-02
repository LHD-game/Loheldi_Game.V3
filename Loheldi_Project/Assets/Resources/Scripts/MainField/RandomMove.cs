using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : MonoBehaviour
{
    Rigidbody rigid;
    public float nextMovex;//행동지표를 결정할 변수
    public float nextMovez;//행동지표를 결정할 변수

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();

        Invoke("Think", Random.Range(1, 10));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //한 방향으로만 알아서 움직이게
        rigid.velocity = new Vector3(nextMovex, rigid.velocity.y, nextMovez);//왼쪽으로 가니까 -1, y축은 0을 넣으면 큰일남!
    }
    //행동지표를 바꿔줄 함수 생각 --> 랜덤클래스 활용 

    void Think()
    {
        nextMovex = Random.Range(-0.5f, 1); //-1이면 왼쪽, 0이면 멈추기,1이면 오른쪽으로이동
        nextMovez = Random.Range(-0.5f, 1); //-1이면 왼쪽, 0이면 멈추기,1이면 오른쪽으로이동

        Invoke("Think", 5);//재귀
    }
}