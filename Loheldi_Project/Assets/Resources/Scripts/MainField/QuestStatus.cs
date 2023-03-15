using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class QuestStatus : MonoBehaviour
{
    public int QuestStepNumber;
    public GameObject[] QuestButtons;
    public GameObject PImag;
    public GameObject ButtonParent;
    GameObject[] ButtonParents;
    public ScrollRect Qsr;
    GameObject child;

    public GameObject QSPanel;
    public GameObject ReQButton;
    public Text QIDText;
    public Text TitleText;
    public Text ContentText;
    public Text FromText;

    public Sprite CompleteButton;

    public List<GameObject> QuestButtonList = new List<GameObject>();
    int Qnum;

    List<Dictionary<string, object>> Quest_Mail = new List<Dictionary<string, object>>();

    QuestDontDestroy QDD;
    // Start is called before the first frame update
    void Start()
    {
        QDD = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
        Quest_Mail = CSVReader.Read("Scripts/Quest/QuestMail");
        QuestIndexCheck();
        //GetButtons();     //퀘스트 추가되면 열어서 일괄넣기 하기
    }

    void GetButtons()  //인스펙터에 버튼넣는 야매 함수
    {
        //Debug.Log("버튼들 가져오기 샤라라라랄랄라");
        //GameObject ButtonParent = GameObject.Find("QuestContent");
        //GameObject[] ButtonParents = new GameObject[QuestButtons.Length];
        ButtonParents = new GameObject[QuestButtons.Length];
        for (int i = 0; i < QuestButtons.Length; i++)
        {
            ButtonParents[i] = ButtonParent.transform.GetChild(i).gameObject;
        }

        Debug.Log("ButtonL = " + QuestButtons.Length);
        Debug.Log("ButtonParentsL = " + ButtonParents.Length);
        int j = 0;
        for (int i = QuestButtons.Length - 1; i > -1; i--)
        {
            Debug.Log("ButtonN = " + i);
            QuestButtons[j] = ButtonParents[i].transform.GetChild(0).gameObject;
            j++;
        } 
        for (int i = 0; i < QuestButtons.Length; i++)
        {
            //QuestButtons[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = Quest_Mail[i]["QID"].ToString(); //버튼Text에 QID넣는 용
            //QuestButtons[i].GetComponent<Button>().onClick.AddListener(QuestButtonClick);//언젠간 필요하지않을까
        }
    }

    public void QuestIndexCheck()
    {
        string QID = PlayerPrefs.GetString("QuestPreg");
        for (int i = 0; i < QuestButtons.Length; i++)
        {
            if (Quest_Mail[i]["QID"].Equals(QID))
            {
                QuestStepNumber = i;
                return;
            }
        }
    }

    public void PlayerStepCheck()
    {
        Debug.Log("QuestStepNumber = " + QuestStepNumber);
        child = Instantiate(PImag, new Vector3(QuestButtons[QuestStepNumber+1].transform.position.x, QuestButtons[QuestStepNumber+1].transform.position.y, QuestButtons[QuestStepNumber].transform.position.z), Quaternion.Euler(0, 0, 0), GameObject.Find("Canvas").transform);
        child.transform.parent = QuestButtons[QuestStepNumber+1].GetComponent<Transform>();

        ButtonActive();

        Debug.Log("child = " + child.transform.position.x);
        float QFx = child.transform.position.x - 1500;
        Vector3 QF = new Vector3(-QFx, Qsr.content.localPosition.y, 0);

        Debug.Log("child = " + QF);
        Qsr.content.localPosition = QF;
        //QuestButtons[QuestStepNumber];

        int i = 0;
        foreach (GameObject a in QuestButtonList)
        {
            PresentCheck(i++);
        }
    }

    void ButtonActive()
    {
        for(int i = 0; i <= QuestStepNumber; i++)
        {
            QuestButtons[i].GetComponent<Button>().enabled = false;
            QuestButtons[i].GetComponent<Image>().sprite = CompleteButton;
        }
    }
    public void ResetQS()
    {
        Destroy(child);
        Qsr.content.localPosition = new Vector3(0, Qsr.content.localPosition.y, 0); ;
    }

    public void QuestButtonClick()
    {
        GameObject click = EventSystem.current.currentSelectedGameObject;
        string[] QQ = click.transform.GetChild(0).gameObject.GetComponent<Text>().text.Split('_');
        if (Int32.Parse(QQ[0]) < 1 || Int32.Parse(QQ[1]) > 1 || !QDD.ReQuest)
        {
            ReQButton.SetActive(false);
        }
        else
        {
            ReQButton.SetActive(true);
        }
        for (int i = 0; i < QuestButtons.Length; i++)
        {
            Debug.Log("동일여부 = " + QuestButtons[i].transform.GetChild(0).gameObject.GetComponent<Text>().text.Equals(Quest_Mail[i]["QID"].ToString()) +"\n"+"버튼 QID숫자 = "+ QuestButtons[i].transform.GetChild(0).gameObject.GetComponent<Text>().text + "\n" + "메일 QID숫자 = "+ Quest_Mail[i]["QID"].ToString());
            if (click.transform.GetChild(0).gameObject.GetComponent<Text>().text.Equals(Quest_Mail[i]["QID"].ToString()))
            {
                QIDText.text = Quest_Mail[i]["QID"].ToString();
                TitleText.text = Quest_Mail[i]["QName"].ToString(); ;
                ContentText.text = Quest_Mail[i]["Content"].ToString().Replace("<n>","\n");
                FromText.text = Quest_Mail[i]["From"].ToString();
                QDD.QuestIndex = QIDText.text;
                break;
            }
        }
        QSPanel.SetActive(true);
    }

    private void PresentCheck(int num)
    {
        Save_Basic.LoadQuestPresentInfo();
        if (PlayerPrefs.GetInt("Q" + num) == 1)
        {
            QuestButtonList[num].GetComponent<Image>().sprite = CompleteButton;
            QuestButtonList[num].GetComponent<Button>().enabled = false;
        }
    }

    public void GetPresent(GameObject gameobject)
    {
        Save_Basic.LoadQuestPresentInfo();

        switch (gameobject.name)
        {
            case "ButtonBse" :
                Qnum = 00;
                break;
            case "ButtonBse (5)":
                Qnum = 01;
                break;
            case "ButtonBse (12)":
                Qnum = 04;
                break;
            case "ButtonBse (19)":
                Qnum = 07;
                break;
            case "ButtonBse (24)":
                Qnum = 10;
                break;
            case "ButtonBse (31)":
                Qnum = 13;
                break;
            case "ButtonBse (35)":
                Qnum = 16;
                break;
            case "ButtonBse (40)":
                Qnum = 19;
                break;
            case "ButtonBse (44)":
                Qnum = 22;
                break;
            case "ButtonBse (48)":
                Qnum = 25;
                break;
            case "ButtonBse (52)":
                Qnum = 28;
                break;
            case "ButtonBse (57)":
                Qnum = 31;
                break;
            case "ButtonBse (61)":
                Qnum = 34;
                break;
            default:
                break;
        }

        QuestButtonList[0].GetComponent<Image>().sprite = CompleteButton;
        QuestButtonList[0].GetComponent<Button>().enabled = false;

        Param param = new Param();
        param.Add("Q" + Qnum, 1);

        var bro = Backend.GameData.Get("QUEST_PRESENT", new Where());
        string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

        var bro2 = Backend.GameData.UpdateV2("QUEST_PRESENT", rowIndate, Backend.UserInDate, param);

        if (bro2.IsSuccess())
        {
            Debug.Log("오예~");
            Debug.Log("GetPresent 성공. QUEST_PRESENT 업데이트 되었습니다.");
        }
        else
        {
            Debug.Log("GetPresent 실패.");
        }
    }
}
