using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornDropDestroy : MonoBehaviour
{
    // Update is called once per frame
    void Update()               //도토리의 높이가 -15 이하면 사라지는 함수
    {
        if (this.transform.position.y < -15f)
        {
            Destroy(this.gameObject);
        }
    }
}
