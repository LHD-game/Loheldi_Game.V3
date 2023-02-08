using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRandomMove : MonoBehaviour
{

    Rigidbody rigid;
    public int nextMove1;//���� �ൿ��ǥ�� ������ ����
    public int nextMove2;

    float time = 5f;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        Debug.Log("1");
        Invoke("Thinking", time); // �ʱ�ȭ �Լ� �ȿ� �־ ����� �� ����(���� 1ȸ) nextMove������ �ʱ�ȭ �ǵ�����
    }

    void FixedUpdate()
    {
        //Move
        rigid.velocity = new Vector3(nextMove1, rigid.velocity.y, nextMove2); //nextMove �� 0:���� -1:���� 1:������ ���� �̵� 


        //Platform check(�� ���� ���������� �ڵ��� ���ؼ� ������ Ž��)


        //�ڽ��� �� ĭ �� ������ Ž���ؾ��ϹǷ� position.x + nextMove(-1,1,0�̹Ƿ� ������)
        Vector3 frontVec = new Vector3(rigid.position.x + nextMove1 * 1f, rigid.position.y, rigid.position.z + nextMove2 * 1f);

        //��ĭ �� �κоƷ� ������ ray�� ��
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

        int layerMask = 3;

        //���̸� ���� ���� ������Ʈ�� Ž�� 
        bool raycastbool = Physics.Raycast(frontVec, Vector3.down, 1, layerMask);

        //Ž���� ������Ʈ�� null : �� �տ� ������ ����
        if (!raycastbool)
        {
            Invoke("Thinking", time);
            nextMove1 = nextMove1 * (-1); //�츮�� ���� ������ �ٲپ� �־����� Think�� ��� ���߾����
            nextMove2 = nextMove2 * (-1); //�츮�� ���� ������ �ٲپ� �־����� Think�� ��� ���߾����
            CancelInvoke(); //think�� ��� ���� �� �����
        }

    }

    public void Thinking()
    {//���Ͱ� ������ �����ؼ� �Ǵ� (-1:�����̵� ,1:������ �̵� ,0:����  ���� 3���� �ൿ�� �Ǵ�)

        Debug.Log("2");
        //Random.Range : �ּ�<= ���� <�ִ� /������ ���� ���� ����(�ִ�� �����̹Ƿ� �����ؾ���)
        nextMove1 = Random.Range(-1, 1);
        nextMove1 = Random.Range(-1, 1);

        time = Random.Range(2f, 5f); //�����ϴ� �ð��� �������� �ο� 
        //Think(); : ����Լ� : �����̸� ���� ������ CPU����ȭ �ǹǷ� ����Լ��� ���� �׻� ���� ->Think()�� ���� ȣ���ϴ� ��� Invoke()���
        Invoke("Thinking", time); //�Ű������� ���� �Լ��� time���� �����̸� �ο��Ͽ� ����� 
    }
}