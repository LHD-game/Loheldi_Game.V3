using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class ALodingTxt : MonoBehaviour
{
    public Transform Player;
    public Transform Nari;

    public Text chatName;
    public Text chatTxt;
    //���� ���� �ߴ°�
    public Text Mailcontent;
    public Text[] QuizButton = new Text[3];

    public GameObject[] SelecButton = new GameObject[5];
    public Text[] SelecButtonTxt = new Text[5];

    public InputField videocheckTxT;
    public InputField ParentscheckTxt;
    public string parentscheckTxTNum;
    public GameObject ErrorWin;      //ȨƮ��������
    public GameObject ClearWin;     //����Ŭ����
    public GameObject ParentsErrorWin;  //��������
    public GameObject ParentscheckUI;  //����â

    public GameObject Main_UI;
    public GameObject NPCButtons;

    public GameObject Arrow;
    public GameObject block;        //�ѱ���� �Ǿ� ��
    public GameObject Chat;
    public GameObject ChatWin;
    public GameObject chatCanvus;
    public GameObject MailCanvus;


    public GameObject Ride;

    public GameObject SoundEffectManager;

    public int NPCButton = 0;
    public string LoadTxt;

    public List<Dictionary<string, object>> data_Dialog = new List<Dictionary<string, object>>();
    public string FileAdress;                // ��ũ��Ʈ ���� ��ġ
    public string cuttoonFileAdress;         // ���� ���� ��ġ

    public string Num;                       //��ũ��Ʈ ��ȣ
    public int j;                                  //data_Dialog �ٰ���
    public int c = 0;                              //���� �̹��� ��ȣ
    public int l;                            //�ߴ� �̹��� ��ȣ

    public int o=0;  //�����̵� ��ȣ
    [SerializeField]
    private GameObject CCImage;     //ĳ���� �̹���
    public static Sprite[] CCImageList;
    static Image spriteR;

    public GameObject cuttoon;        //���� �̹���
    public Sprite[] cuttoonImageList;
    static Image cuttoonspriteR;

    public UIButton JumpButtons;
    public Interaction Inter;


    public QuestDontDestroy DontDestroy;
    public QuestScript Quest;
    public QuestLoad QuestLoad;
    public NpcButtonClick NpcButton;

    string PlayerName;
    private void Awake()
    {
        ChatWin.SetActive(true);

        //fade_in_out = GameObject.Find("EventSystem").GetComponent<Fadeln>();
        CCImageList = Resources.LoadAll<Sprite>("Sprites/CCImage/CImage"); //�̹��� ���

        parentscheckTxTNum = PlayerPrefs.GetString("ParentsNo");
        PlayerName = PlayerPrefs.GetString("Nickname");

        DontDestroy = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();


        DontDestroy.LastDay = PlayerPrefs.GetInt("LastQTime");

        if (SceneManager.GetActiveScene().name == "MainField")     //���� �ʵ忡 ���� ���� ���
        {
            Player.position = DontDestroy.gameObject.transform.position;
            DontDestroy.LastDay = PlayerPrefs.GetInt("LastQTime");

            string[] QQ = PlayerPrefs.GetString("QuestPreg").Split('_');
            string[] qq = PlayerPrefs.GetString("WeeklyQuestPreg").Split('_');

            string[] q_qid = DontDestroy.QuestIndex.Split('_');
            if (Int32.Parse(QQ[0]) > 3) ;
            else
                Ride.SetActive(false);
            //�ָ�üũ
            DateTime nowDT = DateTime.Now;
            if (nowDT.DayOfWeek == DayOfWeek.Saturday)
                DontDestroy.SDA = true;
            else if (nowDT.DayOfWeek == DayOfWeek.Sunday)
                DontDestroy.weekend = true;
            else
                DontDestroy.weekend = false;
            DontDestroy.ToDay = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));   //����Ʈ�� ���ó�¥ ���� */

            if (Int32.Parse(QQ[0]) == 0)
            {
                QuestLoad.QuestLoadStart();
            }
            else if (DontDestroy.SDA)
            {
                return;
            }
            else
            {
                if (!DontDestroy.weekend)
                {
                    if (Int32.Parse(QQ[0]) < 21)
                    {
                        if (DontDestroy.ToDay != DontDestroy.LastDay)
                            QuestLoad.QuestLoadStart();
                    }
                }
                else if (DontDestroy.weekend)
                {
                    if (Int32.Parse(qq[0]) < 25)
                    {
                        if (DontDestroy.ToDay != DontDestroy.LastDay)
                            QuestLoad.QuestLoadStart();
                    }
                }
            }
        }

    }
    public void NewChat()
    {
        Input.multiTouchEnabled = false;
        //PlayerCamera.SetActive(true);
        //Debug.Log("����3");
        data_Dialog = CSVReader.Read(FileAdress);
        for (int k = 0; k <= data_Dialog.Count; k++)
        {
            if (data_Dialog[k]["scriptNumber"].ToString().Equals(Num))
            {
                j = k;
                if (DontDestroy.tutorialLoading)
                    j = 14;
                chatCanvus.SetActive(true);
                Line();
                //Debug.Log("����4");
                break;
            }
            else
            {
                continue;
            }
        }
        Main_UI.SetActive(false);
    }

    public void changeMoment()  //�÷��̾� �̵�, ī�޶� ����
    {
        switch (o)
        {
            case 1:
                Player.transform.position = new Vector3(-145.300003f, 12.6158857f, -21.80023f);
                break;
            case 2:
                cuttoon.SetActive(false);
                Player.transform.position = new Vector3(45f, 5f, 40f);
                break;
            case 3:
                if (DontDestroy.tutorialLoading)
                    Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                DontDestroy.tutorialLoading = false;
                break;
            case 4:
                Player.transform.position = new Vector3(103.51342f, 15.7201061f, 165.103439f);
                Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                break;
            case 5:
                Player.transform.position = new Vector3(-44.7900009f, 5.319489f, 79.5400085f);
                Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                break;
            case 6:
                Player.transform.position = new Vector3(288.572632f, 5.31948948f, 98.3887405f);
                Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                break;
            case 7:
                Player.transform.position = new Vector3(255, 5.31949139f, 101.299973f);
                Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                break;
            case 8:
                Player.transform.position = new Vector3(69.9799881f, 5.67073011f, -16.2484417f);
                Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                break;
            case 9:
                Player.transform.position = new Vector3(-46f, 5.57700014f, -13.6999998f);
                Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                break;
            case 10:
                Player.transform.position = new Vector3(317.426666f, 5.67073059f, 25.669136f);
                Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                break;
            case 11:
                Player.transform.position = new Vector3(45f, 5f, 40f);
                Nari.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                break;
            case 12:
                PlayerPrefs.SetString("QuestPreg", DontDestroy.QuestIndex);
                PlayInfoManager.GetQuestPreg();
                //Save_Log.instance.SaveQEndLog();    //����Ʈ ���� �α� ���
                SceneLoader.instance.GotoMainField();
                break;
            case 13:
                Player.transform.position = new Vector3(-139.300003f, 12.6158857f, -21.80023f);
                Player.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                GameObject Parents = GameObject.Find("parents(Clone)");
                Parents.transform.position = Player.transform.position + new Vector3(-6, 0, 0);
                Parents.transform.rotation = Quaternion.Euler(new Vector3(0, 150, 0));
                break;
            default:
                break;
        }
    }
    GameObject mouth; //��ġ�� ��
    public void QuestSubChoice()
    {
        Debug.Log("Ÿ��" + data_Dialog[j]["scriptType"].ToString());
        if (false) ;
        else if (data_Dialog[j]["scriptType"].ToString().Equals("Move"))  //������
        {
            if (data_Dialog[j]["scriptNumber"].ToString().Equals("24_1"))
                o = 13;
            else if (DontDestroy.tutorialLoading)
                o = 3;
            else
                o += 1;

            changeMoment();
            if (o == 12)
                return;
            else
            {
                j += 1;
                Line();
            }

        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("ReloadEnd"))  //main����
        {
            QuestEnd();
            StartCoroutine(reload());
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("over"))  //main����
        {
            PlayerPrefs.SetString("QuestPreg", DontDestroy.QuestIndex);
            SceneLoader.instance.GotoMainField();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("cuttoonE"))
        {
            Cuttoon();
            ChatWin.SetActive(false);
            Invoke("ChatEnd", 2f);
            Invoke("QuestEnd", 2f);
        } //���� ���̱�
        else if (data_Dialog[j]["scriptType"].ToString().Equals("cuttoon"))
        {
            Cuttoon();
            ChatWin.SetActive(false);
            Invoke("scriptLine", 2f);   //������ �� ��ũ��Ʈ ���
        } //���� ���̱�
        else if (data_Dialog[j]["scriptType"].ToString().Equals("Dcuttoon"))
        {
            scriptLine();
        } //���� ���̱�
        else if (data_Dialog[j]["scriptType"].ToString().Equals("Panorama"))
        {
            c = 0;
            ChatWin.SetActive(false);
            InvokeRepeating("Panorama", 0f, 2f);
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("Panoramas"))
        {
            c = 0;
            ChatWin.SetActive(false);
            InvokeRepeating("Panorama", 0f, 1f);
        } //���� ���̱�
        else if (data_Dialog[j]["scriptType"].ToString().Equals("note"))        //����Ʈ�߰��ֵ�
        {
            if (data_Dialog[j]["cuttoon"].ToString().Equals("0"))
            {
                j++;
                ChatWin.SetActive(false);
                //Note.SetActive(true);
            }
            else
            {
                j++;
                ChatWin.SetActive(false);
            }
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("noteFinish"))        //����Ʈ�߰��ֵ�
        {
            //Note.transform.Find("Button").gameObject.SetActive(false);
            //Draw.FinishWrite();
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("noteEnd"))
        {
            //Note.SetActive(false);
            //ChatWin.SetActive(true);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("nutrient"))        //����Ʈ�߰��ֵ�
        {
            cuttoon.SetActive(false);
            j++;
            ChatWin.SetActive(false);
            //nutrient.SetActive(true);
            Debug.Log("nut");
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("nutrientEnd"))
        {
            j++;
            //nutrient.SetActive(false);
            Cuttoon();
            ChatWin.SetActive(false);
            Invoke("scriptLine", 2f);   //������ �� ��ũ��Ʈ ���
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("train"))
        {
            ChatWin.SetActive(false);
            //Value.SetActive(true);
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("trainEnd"))
        {
            //Value.SetActive(false);
            scriptLine();

        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("LookAt"))
        {
            Transform NPC = GameObject.Find(Inter.NameNPC).transform;
            Vector3 targetPositionNPC;
            targetPositionNPC = new Vector3(Player.transform.position.x, NPC.position.y, Player.transform.position.z);
            StartCoroutine(JumpButtons.NPCturn(NPC, targetPositionNPC));
            if (Quest.note)
            {
                Quest.note = false;
                GameObject[] objs = GameObject.FindGameObjectsWithTag("note");
                for (int i = 0; i < objs.Length; i++)
                    Destroy(objs[i]);
            }
            else if (data_Dialog[j]["scriptNumber"].ToString().Equals("19_1"))
            {
                Transform NPC2 = GameObject.Find("Nari").transform;
                StartCoroutine(JumpButtons.NPCturn(NPC2, targetPositionNPC));
            }
            Invoke("stopCorou", 1f);
            //������ �� ��ũ��Ʈ ���
        }
    }
    void stopCorou()
    {
        JumpButtons.Nstop = false;
        scriptLine();
    }
    void stopCorout()
    {
        JumpButtons.Nstop = false;
    }

    public void Line()  //�ٳѱ� (scriptType�� ���� �ɷ���)
    {
        if (data_Dialog[j]["SoundEffect"].ToString().Equals("Null"))
        { }
        else
        {
            string SoundName = data_Dialog[j]["SoundEffect"].ToString();
            SoundEffectManager.GetComponent<SoundEffect>().Sound(SoundName);
        }
        block.SetActive(true);

        if (data_Dialog[j]["scriptType"].ToString().Equals("end")) //��ȭ ��
        {
            if (data_Dialog[j]["name"].ToString().Equals("end"))
            {
                QuestEnd();
            }
            ChatEnd();

        }
        else
        {
            if (!data_Dialog[j]["scriptType"].ToString().Equals("nomal"))
            {
                //Debug.Log(data_Dialog[j]["scriptType"].ToString());
                QuestSubChoice();
            }
            else
            {
                cuttoon.SetActive(false);
                scriptLine();
            }
            if (data_Dialog[j]["scriptNumber"].ToString().Equals("0_1"))
                Main_UI.SetActive(false);

        }
        //if (move)
        //    changeMoment();
    }


    public void scriptLine()  //��ũ��Ʈ ���� �� (� �̹���+ �̸�+ �ߴ� �ؽ�Ʈ)
    {
        spriteR = CCImage.GetComponent<Image>();
        l = Int32.Parse(data_Dialog[j]["image"].ToString());
        if (l == 9)
            CCImage.SetActive(false);
        else
        {
            CCImage.SetActive(true);
            spriteR.sprite = CCImageList[l];
        }

        LoadTxt = data_Dialog[j]["dialog"].ToString().Replace("P_name", PlayerName);//���ð� ��������
        LoadTxt = LoadTxt.Replace("<n>", "\n");
        if (data_Dialog[j]["name"].ToString().Equals("���ΰ�"))
            chatName.text = PlayerName;
        else
            chatName.text = data_Dialog[j]["name"].ToString();

        StartCoroutine(_typing());
        Arrow.SetActive(false);
        j++;
    }

    public void Cuttoon()
    {
        c = Int32.Parse(data_Dialog[j]["cuttoon"].ToString());
        cuttoon.SetActive(true);
        //Debug.Log("��������=" + cuttoonImageList.Length);
        cuttoonspriteR = cuttoon.GetComponent<Image>();
        cuttoonspriteR.sprite = cuttoonImageList[c];
    }

    public void ChatEnd() //����
    {
        Input.multiTouchEnabled = true;
        chatCanvus.SetActive(false);
        ChatWin.SetActive(false);
        Arrow.SetActive(false);
        NPCButtons.SetActive(false);
        chatName.text = " ";
        Main_UI.SetActive(true);
        c = 0;
        //PlayerCamera.SetActive(false);
        NPCButton = 0;
        ButtonsFalse();
        if (Inter.NameNPC.Equals("WallMirror") || Inter.NameNPC.Equals("GachaMachine") || Inter.NameNPC.Equals("ThankApplesTree"))
        { stopCorou(); }
        else if (DontDestroy.QuestIndex.Equals("8_1") && Inter.NameNPC.Equals("Mei"))
        { stopCorou(); }
        else if (DontDestroy.QuestIndex.Equals("13_1") && Inter.NameNPC.Equals("Suho"))
        { stopCorou(); }
        else
        {
            Vector3 targetPositionNPC;
            targetPositionNPC = new Vector3(JumpButtons.Ntransform.transform.position.x, JumpButtons.NPC.position.y, JumpButtons.Ntransform.transform.position.z - 1);

            StartCoroutine(JumpButtons.NPCturn(JumpButtons.NPC, targetPositionNPC));
            Invoke("stopCorout", 1f);
        }
    }


    public void Buttons()      //npc��ȭ ��ȣ�ۿ� ������ ��
    {
        if (Inter.NameNPC.Equals(DontDestroy.ButtonPlusNpc))
            NPCButton += 1;

        NPCButtons.SetActive(true);
        for (int i = 0; i < NPCButton; i++)
        {
            string selecNumber = "select" + (i + 1).ToString();
            SelecButton[i].SetActive(true);
            SelecButtonTxt[i].text = data_Dialog[j - 1][selecNumber].ToString();
        }
    }
    public void ButtonsFalse()      //npc��ȭ ��ȣ�ۿ� ������ ��
    {
        NPCButtons.SetActive(false);
        for (int i = 0; i < SelecButton.Length; i++)
        {
            string selecNumber = "select" + (i + 1).ToString();
            SelecButton[i].SetActive(false);
        }
    }


    public bool typingSkip = true;
    public void typingSkip_()
    {
        if (chatTxt.text.Length > 3)
        {
            typingSkip = false;
        }
    }
    IEnumerator _typing()  //Ÿ���� ȿ��
    {
        typingSkip = true;
        if (!ChatWin.activeSelf)
            ChatWin.SetActive(true);

        chatTxt.text = " ";
        yield return new WaitForSecondsRealtime(0.1f);
        for (int i = 0; i < LoadTxt.Length + 1; i++)
        {
            if (typingSkip)
            {
                chatTxt.text = LoadTxt.Substring(0, i);
                yield return new WaitForSecondsRealtime(0.03f);
            }
            else
                break;
        }
        chatTxt.text = LoadTxt;

        yield return new WaitForSecondsRealtime(0.2f);
        Arrow.SetActive(true);
    }


    private IEnumerator reload()
    {
        yield return new WaitForEndOfFrame();
        SceneLoader.instance.GotoMainField();
    }
    public void QuestEnd()
    {
        if (SceneManager.GetActiveScene().name == "Quiz") ;
        else
            Save_Log.instance.SaveQEndLog();    //����Ʈ ���� �α� ���

        DontDestroy.ButtonPlusNpc = "";
        //Quest.Load.QuestMail = false;

        if (DontDestroy.weekend)
            PlayerPrefs.SetString("WeeklyQuestPreg", DontDestroy.QuestIndex); //�ָ�
        else
            PlayerPrefs.SetString("QuestPreg", DontDestroy.QuestIndex);


        if (data_Dialog[j]["dialog"].ToString().Equals("end"))
        {
            PlayerPrefs.SetInt("LastQTime", DontDestroy.ToDay);
            NpcButton.Chat.EPin.SetActive(false);
            DontDestroy.LastDay = DontDestroy.ToDay;
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "MainField")
                QuestLoad.QuestLoadStart();
        }

        PlayInfoManager.GetQuestPreg();
    }

    public void ParentsCheck()
    {
        if (ParentscheckTxt.text.Equals(parentscheckTxTNum))
        {
            if (DontDestroy.weekend)
                PlayerPrefs.SetString("WeeklyQuestPreg", DontDestroy.QuestIndex); //�ָ�
            else
                PlayerPrefs.SetString("QuestPreg", DontDestroy.QuestIndex);
            PlayerPrefs.SetInt("LastQTime", DontDestroy.ToDay);
            DontDestroy.LastDay = DontDestroy.ToDay;
            DontDestroy.From = " ";
            ParentscheckUI.SetActive(false);
            ParentscheckTxt.text = null;
            DontDestroy.ButtonPlusNpc = "";
            PlayInfoManager.GetQuestPreg();
            NpcButton.CheckQuest();
            ClearWin.SetActive(true);
            NpcButton.Chat.EPin.SetActive(false);
        }
        else
        {
            ParentsErrorWin.SetActive(true);
        }
    }
}
