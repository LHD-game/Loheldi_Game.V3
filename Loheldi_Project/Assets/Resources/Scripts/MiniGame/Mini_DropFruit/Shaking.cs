using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shaking : MonoBehaviour            //���°� �����ϴ� �Լ�
{
    public GameObject Acorn;                    //���丮 ������Ʈ(������)
    public GameObject Acorns;                   //�ٱ��� �� ���丮 ������Ʈ
    float Dropx;                            //���丮 ���� ����
    float Dropy;
    float Dropz;

    float tiltx;                            //�ڵ����� ���� ��
    float tilty;
    float tiltz;

    bool xbool = false;                     //������ ���� �б����� ��
    bool ybool = false;
    bool zbool = false;

    public GameObject Drop;                 //������ ���丮���� �θ� ������Ʈ
    public GameObject Panel;                //���� ����� ������ ���� UI, ����� �̹����� ���ö� ���� UI
    public GameObject PleaseShake;          //����� �̹��� UI
    public GameObject ReStartButton;        //���� �ٽý��� ��ư


    int DropCount = 0;                      //���丮 ����(�ٱ��� ���¿�)
    int DropCount2 = 0;                     //���丮 ����(�ӽ�)
    int BasketScore = 0;                    //ä�� �ٱ��� ����(����)

    [SerializeField]
    public static float nowTime = 9999;       //���� �÷��� �ð�
    public static float LastShakeTime = 2;  //���������� ��鸰 �ð�

    public Text TimerText;                  //�ð����� �ؽ�Ʈ ������Ʈ
    public Text Score;                      //���� �ؽ�Ʈ ������Ʈ

    public void Shake()                         //"��鸲"�� �ν�������
    {
        Panel.SetActive(false);                     //�ǳ� ġ���
        PleaseShake.SetActive(false);               //���� UI�� ġ���
        LastShakeTime = 2;                          //���������� ��鸰 �ð� �ʱ�ȭ
        Dropx = Random.Range(0.5f, -0.5f);          //���丮�� ������ ���� ���� ����
        Dropy = 1f;
        Dropz = Random.Range(0.5f, -0.5f);
        GameObject DropAcorn = Instantiate(Acorn, new Vector3(Dropx, Dropy, Dropz), Quaternion.identity);   //���丮 ����
        DropAcorn.transform.parent = Drop.transform;
        DropCount++;                                //���丮 ���� ī��Ʈ( 1�� �ٱ��� ���¿�, 2�� ������ )
        DropCount2++;
        Acorns.transform.localPosition = new Vector3(-0.00011f, -0.00047f, Acorns.transform.localPosition.z + 0.00075f);     //�ٱ��ϼ� ���丮 ��ġ ����
        if (DropCount >= 20)                        //�ٱ��� ���� �Լ�
        {
            Handheld.Vibrate();                     //�ٱ��� ���½� ����
            Acorns.transform.localPosition = new Vector3(-0.00011f, -0.00047f, -0.01087f);                  //�ٱ��ϼ� ���丮 ��ġ �ʱ�ȭ
            DropCount = 0;                          //�ٱ��� ���¿� ���丮 ���� ī��Ʈ �ʱ�ȭ
            BasketScore++;                          //���� �ø���
            Score.text = BasketScore.ToString();    //���� UI�� �ݿ�
        }
    }

    private void Start()
    {
        DropCount = 0;
        DropCount2 = 0;
        BasketScore = 0;
        nowTime = 30;
        Panel.SetActive(true);
        PleaseShake.SetActive(true);
        ReStartButton.SetActive(false);
    }

    void Update()
    {
        //���� x, y, z �� tilt���� ���� �Է�
        tiltx = Input.acceleration.x;   //Input.acceleration : ����� ����̽��� Tilt��(���� �ƴ�, �б� ����, �ึ�� ������ 1 ~ -1)
        tilty = Input.acceleration.y;
        tiltz = Input.acceleration.z;

        if (tiltx <= 0.2f && tiltx >= -0.2 && xbool == false)       //x���� ���� ������ ����
        {
            xbool = true;                                       //x�� ������
        }
        else if ((tiltx > 0.2f || tiltx < -0.2) && xbool == true)   //x���� �ٽ� ���� ������ ����
        {
            xbool = false;                                      //x�� �ʱ�ȭ
            Shake();                                            //�ڵ����� ������
        }

        if (tilty <= 0.2f && tilty >= -0.2 && ybool == false)       //y�� ���ϵ���
        {
            ybool = true;
        }
        else if ((tilty > 0.2f || tilty < -0.2) && ybool == true)
        {
            ybool = false;
            Shake();
        }

        if (tiltz <= 0.2f && tiltz >= -0.2 && zbool == false)       //z�� ���ϵ���
        {
            zbool = true;
        }
        else if ((tiltz > 0.2f || tiltz < -0.2) && zbool == true)
        {
            zbool = false;
            Shake();
        }

        nowTime -= Time.deltaTime;                                  //���� Ÿ�̸ӿ��� �� �������� ���� �ð��� ��
        LastShakeTime -= Time.deltaTime;
        if (nowTime <= 0 || BasketScore >=3)                        //�ð� ������ �����ų� ���� ������ ȹ���ϸ�( ������Ÿ�Կ� �ӽ�, �и�����)
        {
            nowTime = 0;                                            //�ð� 0���� ����
            Panel.SetActive(true);                                  //���� ���� ������ �ǳ� ����
            ReStartButton.SetActive(true);                          //���� ������ ��ư �εε���
        }
        if (LastShakeTime <= 0)                                     //�����ð����� ��鸮�� �ʾ�����
        {
            Panel.SetActive(true);                                  //�ǳ�UI����
            PleaseShake.SetActive(true);                            //�����ּ���
        }
        TimerText.text = string.Format(" : {0:N2}", nowTime);      //Ÿ�̸� UI�� �ݿ�
    }
}
