using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxeMove : MonoBehaviour
{
    public GameObject Axe;                       //����UI
    public GameObject Log;                       //�볪��UI

    public GameObject Wood_Log;                  //�볪��(��ü)
    public GameObject Wood_Log_Separate;         //�볪��(�μ����� ������)
    GameObject Wood_Log_Separate_temp;           //�볪��(�μ����� ������ �ӽ�����)

    bool start = true;

    public GameObject Panel;                     //���� ����� ������ ���� UI, ����� �̹����� ���ö� ���� UI
    public GameObject ReStartButton;             //���� �ٽý��� ��ư

    float Logx;                                  //�볪�� UI x��
    float tilty;                                 //�¿� ����� ��

    public Text ScoreText;                       //���� �ؽ�Ʈ ������Ʈ
    public int Score;                            //���� �ؽ�Ʈ ������Ʈ

    public void Start()
    {
        Panel.SetActive(true);
        ReStartButton.SetActive(false);

        Wood_Log_Separate_temp = Instantiate(Wood_Log_Separate, Wood_Log_Separate.transform);
        LogInstiate();
    }

    public void Update()
    {
        Axe.transform.localPosition = new Vector2(tilty * 1300f, Axe.transform.localPosition.y);

        if (Axe.transform.localPosition.x == Log.transform.localPosition.x)
        {
            Score++;
            ScoreText.text = Score.ToString();
            if (!start)
            {
                Wood_Log_Separate_temp.transform.GetChild(0).gameObject.GetComponent<MeshCollider>().enabled = false;
                Wood_Log_Separate_temp.transform.GetChild(1).gameObject.GetComponent<MeshCollider>().enabled = false;
            }
            Wood_Log.SetActive(true);
            Wood_Log_Separate_temp = Instantiate(Wood_Log_Separate, Wood_Log.transform);
            LogInstiate();
            start = false;
        }
    }
    public void LogInstiate()
    {
        Logx = Random.Range(1300f, -1300f);                         //���丮�� ������ ���� ���� ����
        Log.transform.localPosition = new Vector2 (Logx, Log.transform.localPosition.y);
    }
}
