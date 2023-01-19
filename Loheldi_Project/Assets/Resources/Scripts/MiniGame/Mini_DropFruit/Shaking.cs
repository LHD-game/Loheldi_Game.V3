using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaking : MonoBehaviour
{
    public GameObject Acorn;
    float Dropx;
    float Dropy;
    float Dropz;

    float tiltx;
    float tilty;
    float tiltz;

    bool tiltbool = false;
    bool tempbool = false;

    public void Shake()
    {
        Dropx = Random.Range(0.5f, -0.5f);
        Dropy = 1f;
        Dropz = Random.Range(0.5f, -0.5f);
        Instantiate(Acorn, new Vector3(Dropx, Dropy, Dropz), Quaternion.identity);
    }
    void Update()
    {
        //변수 x, y, z 에 tilt센서 값을 입력
        tiltx = Input.acceleration.x;   //Input.acceleration : 모바일 디바이스의 Tilt값(벡터 아님, 읽기 전용, 축마다 범위는 1 ~ -1)
        tilty = Input.acceleration.y;
        tiltz = Input.acceleration.z;

        if (tiltbool)
        {
            Shake();
            tiltbool = false;
        }
        else
        {
            if((tiltx <= 0.2f && tiltx >= -0.2) || (tilty <= 0.2f && tilty >= -0.2) || (tiltz <= 0.2f && tiltz >= -0.2) && !tempbool)
            {
                tempbool = true;
            }
            else if((tiltx >= 0.2f || tiltx <= -0.2) || (tilty >= 0.2f || tilty <= -0.2) || (tiltz >= 0.2f || tiltz <= -0.2) && tempbool)
            {
                tempbool = false;
                Shake();
            }
        }
    }
}
