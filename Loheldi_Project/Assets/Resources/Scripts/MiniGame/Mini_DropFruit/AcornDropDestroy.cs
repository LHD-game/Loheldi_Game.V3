using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornDropDestroy : MonoBehaviour
{
    // Update is called once per frame
    void Update()               //���丮�� ���̰� -15 ���ϸ� ������� �Լ�
    {
        if (this.transform.position.y < -15f)
        {
            Destroy(this.gameObject);
        }
    }
}
