using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRandomMove_Cube : MonoBehaviour
{
    public GameObject Range;
    public GameObject Owner;

    void OnTriggerExit(Collider col)                            //Cube�� �ݸ��� ������ ����� �۵�
    {
        if (col.gameObject == Range)              //���� ��� ������Ʈ�� �θ� ������Ʈ���
        {
            Debug.Log("��ũ��Ʈ �ҷ���");
            Owner.GetComponent<NPCRandomMove>().nextMove1 = Owner.GetComponent<NPCRandomMove>().nextMove1 * -3f;
            Owner.GetComponent<NPCRandomMove>().nextMove2 = Owner.GetComponent<NPCRandomMove>().nextMove2 * -3f;
            Owner.GetComponent<NPCRandomMove>().MoveBox();      //MoveBox �ٽ� ����
        }
    }
}
