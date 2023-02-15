using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRandomMove_Cube : MonoBehaviour
{
    void OnTriggerExit(Collider col)
    {
        Debug.Log("범위 벗어남");
        Debug.Log(col.gameObject.name);
        Debug.Log(this.transform.parent.name);
        if (col.gameObject.name == this.transform.parent.name)
        {
            Debug.Log("스크립트 불러옴");
            this.transform.parent.GetChild(1).GetComponent<NPCRandomMove>().MoveBox();
        }
    }
}
