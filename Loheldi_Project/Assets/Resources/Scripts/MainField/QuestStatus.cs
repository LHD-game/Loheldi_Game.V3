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

    public Sprite CompletButton;

    List<Dictionary<string, object>> Quest_Mail = new List<Dictionary<string, object>>();

    QuestDontDestroy QDD;
    // Start is called before the first frame update
    void Start()
    {
        QDD = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
        Quest_Mail = CSVReader.Read("Scripts/Quest/QuestMail");
        QuestIndexCheck();
        //GetButtons();     //����Ʈ �߰��Ǹ� ��� �ϰ��ֱ� �ϱ�
    }
    void GetButtons()
    {
        //Debug.Log("��ư�� �������� �����������");
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
            //QuestButtons[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = Quest_Mail[i]["QID"].ToString(); //��ưText�� QID�ִ� ��
            //QuestButtons[i].GetComponent<Button>().onClick.AddListener(QuestButtonClick);//������ �ʿ�����������
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
        child = Instantiate(PImag, new Vector3(QuestButtons[QuestStepNumber+1].transform.position.x, QuestButtons[QuestStepNumber].transform.position.y + 70, QuestButtons[QuestStepNumber].transform.position.z), Quaternion.Euler(0, 0, 0), GameObject.Find("Canvas").transform);
        child.transform.parent = QuestButtons[QuestStepNumber].GetComponent<Transform>();

        ButtonActive();

        Debug.Log("child = " + child.transform.position.x);
        float QFx = child.transform.position.x - 1500;
        Vector3 QF = new Vector3(-QFx, Qsr.content.localPosition.y, 0);

        Debug.Log("child = " + QF);
        Qsr.content.localPosition = QF;
        //QuestButtons[QuestStepNumber];
    }

    void ButtonActive()
    {
        for(int i = 0; i <= QuestStepNumber; i++)
        {
            QuestButtons[i].GetComponent<Button>().enabled = true;
            QuestButtons[i].GetComponent<Image>().sprite = CompletButton;
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
            Debug.Log("���Ͽ��� = " + QuestButtons[i].transform.GetChild(0).gameObject.GetComponent<Text>().text.Equals(Quest_Mail[i]["QID"].ToString()) +"\n"+"��ư QID���� = "+ QuestButtons[i].transform.GetChild(0).gameObject.GetComponent<Text>().text + "\n" + "���� QID���� = "+ Quest_Mail[i]["QID"].ToString());
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

}
