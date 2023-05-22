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

    public GameObject[] PresentButtons = new GameObject[100];
    public GameObject PresentButton;
    public Sprite CompleteButton;
    int j = 0;
    int k = 0;
    public bool FirstLoad = true;

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

            /*Debug.Log("보이고 싶은거 위치 Origin = " + QuestButtons[QuestStepNumber].transform.parent.localPosition.x);
            Debug.Log("보이고 싶은거 위치 Rext= " + ButtonRT.position.x);
            Debug.Log("보이는 위치 = " + scrollValue);*/
            //float scrollValue = QuestButtons[QuestStepNumber].transform.localPosition.x / (Qsr.content.rect.width - Qsr.GetComponent<RectTransform>().rect.width);
            if (QuestStepNumber - 1 <0)
                QuestStepNumber+=1;
                scrollValue = QuestButtons[QuestStepNumber - 1].transform.parent.localPosition.x / (Qsr.content.rect.width - Qsr.GetComponent<RectTransform>().rect.width);
        }


        Qsr.horizontalNormalizedPosition = scrollValue;


        if (!LetCheck)
        {
            QDD = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
            Quest_Mail = CSVReader.Read("Scripts/Quest/QuestMail");
            //QuestIndexCheck();
            ButtonActive();
            LetCheck = false;
        }
        Save_Basic.LoadQuestPresentInfo();

        for (int i = 0; i < PresentButtons.Length - 1; i++) {
            Destroy(PresentButtons[i]);
        }

        j = 0;
        for (int i = 0; i < QuestButtons.Length - 1; i++)
        {
            Debug.Log(PlayerPrefs.GetString("QuestPreg") + "  " + QuestButtons[i].transform.parent.gameObject.name);
            if (PlayerPrefs.GetString("QuestPreg") == QuestButtons[i].transform.parent.gameObject.name)
            {
                break;
            }
            InstantiatePresentButton(QuestButtons[i].transform.parent.gameObject);
        }
        k = 1;
        FirstLoad = false;
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

    public void InstantiatePresentButton(GameObject gameobject)
    {

        Debug.Log(j);
        bool OK = false;
        String[] Num = gameobject.name.Split('_');
        String[] NextNum = gameobject.name.Split('_');
        int i = 0;
        int result;

        if (Int32.TryParse(Num[0], out result))
        {
            if (gameobject.name == "0_1")
            {
                PresentButtons[j] = Instantiate(PresentButton, gameobject.transform);
                int temp = j;
                PresentButtons[temp].GetComponent<Button>().onClick.AddListener(delegate () { GetPresentButton(PresentButtons[temp].gameObject); });
                OK = true;
                j++;
            }
            if (Int32.Parse(Num[0]) % 3 == 0 && Num[0] != "1" && Num[0] != "0" && Num[0] != "33")
            {
                while (gameobject.transform.parent.transform.GetChild(i) != gameobject.transform)
                {
                    i++;
                }
                i++;
                NextNum = gameobject.transform.parent.transform.GetChild(i).name.Split('_');
                if (Int32.Parse(Num[0]) >= Int32.Parse(NextNum[0]))
                {
                    PresentButtons[j] = Instantiate(PresentButton, gameobject.transform);
                    int temp = j;
                    PresentButtons[temp].GetComponent<Button>().onClick.AddListener(delegate () { GetPresentButton(PresentButtons[temp].gameObject); });
                    OK = true;
                    j++;
                }
            } 
            else if(Num[0] == "33")
            {
                PresentButtons[j] = Instantiate(PresentButton, gameobject.transform);
                int temp = j;
                PresentButtons[temp].GetComponent<Button>().onClick.AddListener(delegate () { GetPresentButton(PresentButtons[temp].gameObject); });
                OK = true;
                j++;
            }

            if (OK)
            {
                if ("true" == PlayerPrefs.GetString("Q" + Int32.Parse(Num[0])))
                {
                    gameobject.transform.GetChild(1 + k).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/FieldUI/Complete");
                    gameobject.transform.GetChild(1 + k).GetComponent<Button>().enabled = false;
                    Debug.Log(gameobject.transform.GetChild(1));
                }
            }

        }
    }

    public void GetPresentButton(GameObject gameobject)
    {
        String[] Num = gameobject.transform.parent.name.Split('_');

        Param param = new Param();
        param.Add("Q" + Int32.Parse(Num[0]), "true");

        var bro = Backend.GameData.Get("QUEST_PRESENT", new Where());
        string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

        var bro2 = Backend.GameData.UpdateV2("QUEST_PRESENT", rowIndate, Backend.UserInDate, param);

        switch (gameobject.transform.parent.name)
        {
            case "0_1":
                PlayInfoManager.GetCoin(50);
                Debug.Log("Coin 50");
                break;
            case "3_4":
                PlayInfoManager.GetHP(1);
                Debug.Log("HP 1");
                break;
            case "6_2":
                BuyItemBtn("1010103");
                break;
            case "9_2":
                PlayInfoManager.GetCoin(50);
                break;
            case "12_2":
                PlayInfoManager.GetHP(1);
                break;
            case "15_1":
                BuyItemBtn("1010106");
                break;
            case "18_1":
                PlayInfoManager.GetCoin(60);
                break;
            case "21_1":
                PlayInfoManager.GetHP(2);
                break;
            case "24_2":
                BuyItemBtn("1010206");
                break;
            case "27_1":
                PlayInfoManager.GetCoin(70);
                break;
            case "30_1":
                PlayInfoManager.GetHP(2);
                break;
            case "33_1":
                PlayInfoManager.GetCoin(100);
                break;
            default:
                break;
        }

        if (bro2.IsSuccess())
        {
            Debug.Log("Q" + Int32.Parse(Num[0]) + " " + "true");
            Debug.Log("GetPresent 성공. QUEST_PRESENT 업데이트 되었습니다.");

            gameobject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/FieldUI/Complete");
            gameobject.GetComponent<Button>().enabled = false;
        }
        else
        {
            Debug.Log("GetPresent 실패.");
        }
    }


    public void BuyItemBtn(String iCode)
    {
        //Inventory 테이블 불러와서, 여기에 해당하는 아이템과 일치하는 코드가 있을 경우 개수를 1증가시켜서 업데이트

        Where where = new Where();
        where.Equal("ICode", iCode);
        var bro = Backend.GameData.GetMyData("INVENTORY", where);

        if (bro.IsSuccess() == false)
        {
            Debug.Log("요청 실패");
        }
        else
        {
            JsonData rows = bro.GetReturnValuetoJSON()["rows"];
            //없을 경우 아이템 행 추가
            if (rows.Count <= 0)
            {
                Param param = new Param();
                param.Add("ICode", iCode);
                param.Add("Amount", 1);

                var insert_bro = Backend.GameData.Insert("INVENTORY", param);

                if (insert_bro.IsSuccess())
                {
                    Debug.Log("아이템 받기 완료: " + iCode);
                }
                else
                {
                    Debug.Log("아이템 받기 오류");
                }
            }
            //있을 경우 해당 아이템 indate찾고, 개수 수정
            else
            {
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                int item_amount = (int)bro.FlattenRows()[0]["Amount"];
                item_amount++;
                Debug.Log(item_amount);

                Param param = new Param();
                param.Add("ICode", iCode);
                param.Add("Amount", item_amount);

                var update_bro = Backend.GameData.UpdateV2("INVENTORY", rowIndate, Backend.UserInDate, param);
                if (update_bro.IsSuccess())
                {
                    Debug.Log("아이템 받기 완료: " + iCode);
                }
                else
                {
                    Debug.Log("아이템 받기 오류");
                }
            }
        }
    }
}
