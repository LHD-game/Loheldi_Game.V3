using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxeMove : MonoBehaviour
{
    public GameObject AxeUI;                     //����UI
    public GameObject LogUI;                     //�볪��UI
    public GameObject Effect;                    //����ƮUI

    public GameObject Wood_Log;                  //�볪��(��ü)
    public GameObject Wood_Log_Separate;         //�볪��(�μ����� ������)
    GameObject Wood_Log_Separate_temp;           //�볪��(�μ����� ������ �ӽ�����)

    bool start = true;
    bool CorountionBool = false;

    public GameObject Panel;                     //���� ����� ������ ���� UI, ����� �̹����� ���ö� ���� UI
    public GameObject ReStartButton;             //���� �ٽý��� ��ư

    int AllowArea = 100;                         //������(������ �볪���� ��ġ�� ����)
    float Logx;                                  //�볪�� UI x��
    public float tilty;                                 //�¿� ����� ��
    public Text text;

    public float AllowTime;                             //��ģ �ð�
    public float AllowTimeMax;
    public Slider AllowSlider;                          //��ģ �ð��� ǥ���ϴ� �����̴�

    public Text ScoreText;                              //���� �ؽ�Ʈ ������Ʈ
    public int Score = 0;                               //���� �ؽ�Ʈ ������Ʈ
    public int ScoreMax = 0;                            //���� �ؽ�Ʈ ������Ʈ

    public Animator Axe;                                 //����
    public Animator PlayerArmature;                      //�볪��

    public GameObject WoodGameManager;

    public void Start()
    {
        Effect.SetActive(false);
        Panel.SetActive(true);
        AllowTime = 0;

        Wood_Log_Separate_temp = Instantiate(Wood_Log_Separate, Wood_Log_Separate.transform);
        if (AxeUI.transform.localPosition.x + AllowArea >= LogUI.transform.localPosition.x && AxeUI.transform.localPosition.x - AllowArea <= LogUI.transform.localPosition.x)
        {
            Wood_Log_Separate_temp = Instantiate(Wood_Log_Separate, Wood_Log_Separate.transform); if (AxeUI.transform.localPosition.x + AllowArea >= LogUI.transform.localPosition.x && AxeUI.transform.localPosition.x - AllowArea <= LogUI.transform.localPosition.x)
            {
                Wood_Log_Separate_temp = Instantiate(Wood_Log_Separate, Wood_Log_Separate.transform);
            }
        }

            LogInstiate();
    }

    public void Update()
    {
        tilty = Input.acceleration.y;
        AxeUI.transform.localPosition = new Vector2( tilty * 1300f, AxeUI.transform.localPosition.y);
        text.text = tilty.ToString();

        AllowSlider.value = AllowTime;
        if (AxeUI.transform.localPosition.x + AllowArea >= LogUI.transform.localPosition.x && AxeUI.transform.localPosition.x - AllowArea <= LogUI.transform.localPosition.x)
        {
            Effect.SetActive(true);

            if (!CorountionBool)
            {
                AllowTime += Time.deltaTime;   //��ģ �ð� ����
            }
            if (AllowTime >= AllowSlider.maxValue)
            {
                AllowTime = 0;
                CorountionBool = true;
                PlayerArmature.SetBool("AxeMove", true);
                Axe.SetBool("AxeMove", true);
                StartCoroutine(ChopWood());
            }
        }
        else
        {
            Effect.SetActive(false);
            AllowTime -= Time.deltaTime;   //��ģ �ð� ����
            if(AllowTime <= 0)
            {
                AllowTime = 0;
            }
        }
    }
    public void LogInstiate()
    {
        Logx = Random.Range(1200f, -1200f);
        LogUI.transform.localPosition = new Vector2 (Logx, LogUI.transform.localPosition.y);
    }

    public void DifficultyWoodEasy()
    {
        AllowSlider.maxValue = 2;
        ScoreMax = 3;
        WoodGameManager.GetComponent<WoodGameManager>().WoodSet();
    }
    public void DifficultyWoodNormal()
    {
        AllowSlider.maxValue = 3;
        ScoreMax = 4;
        WoodGameManager.GetComponent<WoodGameManager>().WoodSet();
    }
    public void DifficultyWoodHard()
    {
        AllowSlider.maxValue = 4;
        ScoreMax = 5;
        WoodGameManager.GetComponent<WoodGameManager>().WoodSet();
    }
    IEnumerator ChopWood()
    {
        yield return new WaitForSeconds(0.6f);
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
        PlayerArmature.SetBool("AxeMove", false);
        Axe.SetBool("AxeMove", false);
        CorountionBool = false;
        yield break;
    }
}
