using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : MonoBehaviour
{
    Rigidbody rigid;
    public float nextMovex;//�ൿ��ǥ�� ������ ����
    public float nextMovez;//�ൿ��ǥ�� ������ ����

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();

        Invoke("Think", Random.Range(1, 10));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�� �������θ� �˾Ƽ� �����̰�
        rigid.velocity = new Vector3(nextMovex, rigid.velocity.y, nextMovez);//�������� ���ϱ� -1, y���� 0�� ������ ū�ϳ�!
    }
    //�ൿ��ǥ�� �ٲ��� �Լ� ���� --> ����Ŭ���� Ȱ�� 

    void Think()
    {
        nextMovex = Random.Range(-0.5f, 1); //-1�̸� ����, 0�̸� ���߱�,1�̸� �����������̵�
        nextMovez = Random.Range(-0.5f, 1); //-1�̸� ����, 0�̸� ���߱�,1�̸� �����������̵�

        Invoke("Think", 5);//���
    }
}