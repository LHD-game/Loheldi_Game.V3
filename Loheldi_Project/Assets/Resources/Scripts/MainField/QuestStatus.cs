using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
//using UnityEngine.UIElements;

public class QuestStatus : MonoBehaviour
{
    public int QuestStepNumber;
    public int TestQuestStepNumber;
    public GameObject[] QuestButtons;
    public GameObject[] TestQuestButtons;
    public GameObject PImag;
    public GameObject ButtonParent;
    GameObject[] ButtonParents;
    public ScrollRect Qsr;
    public ScrollRect TestQsr;
    GameObject child;

    public GameObject QSPanel;
    public GameObject ReQButton;
    public Text QIDText;
    public Text TitleText;
    public Text ContentText;
    public Text FromText;

    public GameObject[] PresentButtons;
    public GameObject PresentButton;
    public Sprite CompleteButton;
    int j = 0;

    public List<GameObject> QuestButtonList = new List<GameObject>();
    int Qnum;

    List<Dictionary<string, object>> Quest_Mail = new List<Dictionary<string, object>>();

    QuestDontDestroy QDD;
    public QuestLoad QuestLoad;

    private float scrollValue = 0;
    private bool LetCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        //QDD = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
        //Quest_Mail = CSVReader.Read("Scripts/Quest/QuestMail");
        //QuestIndexCheck();
        //GetButtons();     //퀘스트 추가되면 열어서 일괄넣기 하기
    }
    void GetButtons()  //인스펙터에 버튼넣는 야매 함수
    {
        Debug.Log("버튼들 가져오기 샤라라라랄랄라");
        ButtonParents = new GameObject[QuestButtons.Length];
        for (int i = 0; i < QuestButtons.Length; i++)
        {
            ButtonParents[i] = ButtonParent.transform.GetChild(i).gameObject;
        }

        Debug.Log("ButtonL = " + QuestButtons.Length);
        Debug.Log("ButtonParentsL = " + ButtonParents.Length);
        for (int i = 0; i < QuestButtons.Length - 1; i++)
        {
            //Debug.Log("ButtonN = " + i);
            //QuestButtons[i] = ButtonParents[i].transform.GetChild(0).gameObject;
            QuestButtons[i].name = "Button"+i.ToString();
        } 
        for (int i = 0; i < QuestButtons.Length-1; i++)  //버튼에 QID넣는 for문
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

    public void TestPlayerStepCheck()
    {
        GameObject click = EventSystem.current.currentSelectedGameObject;
        Debug.Log(click.gameObject.name);
        TestQuestStepNumber = Int32.Parse(click.gameObject.name);
        Transform ButtonT = TestQuestButtons[TestQuestStepNumber].GetComponent<Transform>();
        RectTransform ButtonRT = TestQuestButtons[TestQuestStepNumber].transform.parent.GetComponent<RectTransform>();
        child = Instantiate(PImag, new Vector3(ButtonRT.position.x, ButtonRT.position.y, ButtonRT.position.z), Quaternion.Euler(0, 0, 0), GameObject.Find("Canvas").transform);
        child.transform.SetParent(ButtonT);

        //ButtonActive();

        float scrollValue = (TestQuestButtons[TestQuestStepNumber].transform.localPosition.x / (TestQsr.content.rect.width - TestQsr.GetComponent<RectTransform>().rect.width));


        Debug.Log("보이고 싶은거 위치 Origin = " + QuestButtons[QuestStepNumber].transform.localPosition.x);
        Debug.Log("보이고 싶은거 위치 Rext= " + ButtonRT.position.x);
        Debug.Log("보이는 위치 = " + scrollValue);

        TestQsr.horizontalNormalizedPosition= scrollValue;

    }

    public void TestResetValue()
    {
        TestQsr.horizontalNormalizedPosition = 0;
    }
    public void PlayerStepCheck()
    {
        if (scrollValue == 0)
        {
            Transform ButtonT = QuestButtons[QuestStepNumber + 1].GetComponent<Transform>();
            RectTransform ButtonRT = QuestButtons[QuestStepNumber + 1].transform.parent.GetComponent<RectTransform>();
            child = Instantiate(PImag, new Vector3(ButtonRT.position.x, ButtonRT.position.y, ButtonRT.position.z), Quaternion.Euler(0, 0, 0), GameObject.Find("Canvas").transform);
            child.transform.SetParent(ButtonT);

            Debug.Log("보이고 싶은거 위치 Origin = " + QuestButtons[QuestStepNumber].transform.parent.localPosition.x);
            Debug.Log("보이고 싶은거 위치 Rext= " + ButtonRT.position.x);
            Debug.Log("보이는 위치 = " + scrollValue);
            //float scrollValue = QuestButtons[QuestStepNumber].transform.localPosition.x / (Qsr.content.rect.width - Qsr.GetComponent<RectTransform>().rect.width);
            scrollValue = QuestButtons[QuestStepNumber - 1].transform.parent.localPosition.x / (Qsr.content.rect.width - Qsr.GetComponent<RectTransform>().rect.width);
        }


        Qsr.horizontalNormalizedPosition = scrollValue;


        if (!LetCheck)
        {
            QDD = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
            Quest_Mail = CSVReader.Read("Scripts/Quest/QuestMail");
            //QuestIndexCheck();
            ButtonActive();

            int i = 0;
            foreach (GameObject a in QuestButtonList)
            {
                PresentCheck(i++);
            }
            LetCheck = false;
        }
        for (int i = 0; i < QuestButtons.Length - 1 ; i++)
        {
            InstantiatePresentButton(QuestButtons[i].transform.parent.gameObject);
        }
    }

    void ButtonActive()
    {
        for(int i = 0; i <= QuestStepNumber; i++)
        {
            //Debug.Log(QuestButtons[i].GetComponent<RectTransform>().position.x);
            QuestButtons[i].GetComponent<Button>().enabled = true;
            QuestButtons[i].GetComponent<Image>().sprite = CompleteButton;
        }
    }
    public void ResetQS()
    {
        //Destroy(child);
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
            //Debug.Log("동일여부 = " + QuestButtons[i].transform.GetChild(0).gameObject.GetComponent<Text>().text.Equals(Quest_Mail[i]["QID"].ToString()) +"\n"+"버튼 QID숫자 = "+ QuestButtons[i].transform.GetChild(0).gameObject.GetComponent<Text>().text + "\n" + "메일 QID숫자 = "+ Quest_Mail[i]["QID"].ToString());
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

    public void ReQuestButton()
    {
        QDD.ReQuest = true;
        QDD.QuestNF = false;
        PlayerPrefs.SetInt("LastQTime", 0);
        PlayerPrefs.SetString("QuestPreg", QDD.QuestIndex);
        QDD.LastDay = 0;


        QuestLoad.QuestLoadStart();
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

    public void InstantiatePresentButton(GameObject gameobject)
    {
        String[] Num = gameobject.name.Split('-');
        String[] NextNum = gameobject.name.Split('-');
        int i = 0;
        int result;

        if (Int32.TryParse(Num[0], out result))
        {
            if (gameobject.name == "0-1")
            {
                PresentButtons[j] = Instantiate(PresentButton, gameobject.transform);
                PresentButtons[j].GetComponent<Button>().onClick.AddListener(delegate () { GetPresentButton(PresentButtons[j].gameObject); });
            }
            if (Int32.Parse(Num[0]) % 3 == 0 && Num[0] != "1" && Num[0] != "0" && Num[0] != "33")
            {
                while (gameobject.transform.parent.transform.GetChild(i) != gameobject.transform)
                {
                    i++;
                }
                i++;
                NextNum = gameobject.transform.parent.transform.GetChild(i).name.Split('-');
                if (Int32.Parse(Num[1]) >= Int32.Parse(NextNum[1]))
                {
                    PresentButtons[j] = Instantiate(PresentButton, gameobject.transform);
                    PresentButtons[j].GetComponent<Button>().onClick.AddListener(delegate { GetPresentButton(PresentButtons[j].gameObject); });
                }
            } 
            else if(Num[0] == "33")
            {
                PresentButtons[j] = Instantiate(PresentButton, gameobject.transform);
                PresentButtons[j].GetComponent<Button>().onClick.AddListener(delegate { GetPresentButton(PresentButtons[j].gameObject); });
            }
        }
    }

    public void GetPresentButton(GameObject gameobject)
    {
        String[] Num = gameobject.transform.parent.name.Split('-');

        Debug.Log("붑");
        Save_Basic.LoadQuestPresentInfo();

        gameobject.GetComponent<Image>().sprite = CompleteButton;
        gameobject.GetComponent<Button>().enabled = false;

        Param param = new Param();
        param.Add("Q" + Int32.Parse(Num[0]), true);

        Where where = new Where();
        where.Equal("Q" + Int32.Parse(Num[0]), true);
        var bro = Backend.GameData.GetMyData("QUEST_PRESENT", where);
        string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

        var bro2 = Backend.GameData.UpdateV2("QUEST_PRESENT", rowIndate, Backend.UserInDate, param);

        if (bro2.IsSuccess())
        {
            Debug.Log("GetPresent 성공. QUEST_PRESENT 업데이트 되었습니다.");
        }
        else
        {
            Debug.Log("GetPresent 실패.");
        }
    }
}
