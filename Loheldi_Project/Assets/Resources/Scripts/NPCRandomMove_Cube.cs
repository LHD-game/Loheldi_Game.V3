using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRandomMove_Cube : MonoBehaviour
{
    public GameObject Range;
    public GameObject Owner;

    void OnTriggerExit(Collider col)                            //Cube가 콜리더 범위를 벗어나면 작동
    {
        if (col.gameObject == Range)              //만약 벗어난 오브젝트가 부모 오브젝트라면
        {
            Debug.Log("스크립트 불러옴");
            Owner.GetComponent<NPCRandomMove>().nextMove1 = Owner.GetComponent<NPCRandomMove>().nextMove1 * -3f;
            Owner.GetComponent<NPCRandomMove>().nextMove2 = Owner.GetComponent<NPCRandomMove>().nextMove2 * -3f;
            Owner.GetComponent<NPCRandomMove>().MoveBox();      //MoveBox 다시 실행
        }
    }
}
