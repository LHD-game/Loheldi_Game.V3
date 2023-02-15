using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRandomMove_Cube : MonoBehaviour
{
    void OnTriggerExit(Collider col)                            //Cube가 콜리더 범위를 벗어나면 작동
    {
        Debug.Log("범위 벗어남");
        Debug.Log(col.gameObject.name);
        Debug.Log(this.transform.parent.name);
        if (col.gameObject == this.transform.parent)              //만약 벗어난 오브젝트가 부모 오브젝트라면
        {
            Debug.Log("스크립트 불러옴");
            this.transform.parent.GetChild(1).GetComponent<NPCRandomMove>().MoveBox();      //MoveBox 다시 실행
        }
    }
}
