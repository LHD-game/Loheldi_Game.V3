using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRandomMove : MonoBehaviour
{

    Rigidbody rigid;
    public float nextMove1;//���� �ൿ��ǥ�� ������ ����
    public float nextMove2;

    float time = 5f;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        Debug.Log("1");
        Invoke("Thinking", time); // �ʱ�ȭ �Լ� �ȿ� �־ ����� �� ����(���� 1ȸ) nextMove������ �ʱ�ȭ �ǵ�����
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = new Vector3(nextMove1, rigid.velocity.y, nextMove2); //nextMove �� 0:���� -1:���� 1:������ ���� �̵� 
    }

    public void Thinking()
    {//���Ͱ� ������ �����ؼ� �Ǵ� (-1:�����̵� ,1:������ �̵� ,0:����  ���� 3���� �ൿ�� �Ǵ�)

        Debug.Log("2");
        //Random.Range : �ּ�<= ���� <�ִ� /������ ���� ���� ����(�ִ�� �����̹Ƿ� �����ؾ���)
        nextMove1 = Random.Range(-1f, 2f);
        nextMove2 = Random.Range(-1f, 2f);

        time = Random.Range(2f, 5f); //�����ϴ� �ð��� �������� �ο� 
        //Think(); : ����Լ� : �����̸� ���� ������ CPU����ȭ �ǹǷ� ����Լ��� ���� �׻� ���� ->Think()�� ���� ȣ���ϴ� ��� Invoke()���
        Invoke("Thinking", time); //�Ű������� ���� �Լ��� time���� �����̸� �ο��Ͽ� ����� 
    }
}