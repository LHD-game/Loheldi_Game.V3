using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.ProBuilder.MeshOperations;

public class LodingTxt : MonoBehaviour
{
    public Transform Player;
    public Transform Nari;
    public Transform NariMom;


    public Text chatName;
    //public Text QuizName;
    public Text chatTxt;
    //���� ���� �ߴ°�
    public Text Mailcontent;
    //public Text QuizTxt;
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
    public GameObject Button;
    public GameObject NPCButtons;
    public GameObject OImage;
    public GameObject XImage;
    public GameObject Quiz;
    public Material[] Quiz_material;
    [SerializeField]
    private Material[] material;

    public GameObject Arrow;
    public GameObject block;        //�ѱ���� �Ǿ� ��
    public Color color;
    public GameObject Chat;
    public GameObject ChatWin;
    public GameObject chatCanvus;
    public GameObject shopCanvus;
    public GameObject MailCanvus;

    public GameObject movie;
    public GameObject DrawUI;
    public GameObject Note;
    public GameObject Value;
    //public GameObject IMessage;
    public GameObject KeyToDream;
    public GameObject AppleTree;
    public GameObject MasterOfMtLife;
    public GameObject screenShot;
    public GameObject screenShotExit;
    public GameObject nutrient;
    public GameObject LoveNature;
    public GameObject BMI;
    public GameObject BMItalk;
    public GameObject Nanum;
    public GameObject Jewel;
    public GameObject Healthy;

    public GameObject Ride;
    public GameObject Bike;
    public GameObject BikeNPC;

    public GameObject AppleTreeObj;
    public GameObject Kangteagom;

    public GameObject SoundEffectManager;
    GameObject SoundManager;

    public int NPCButton = 0;
    public string LoadTxt;
    private string[] Xdialog = {"�ٽ� �ѹ� �����غ�.","�ƽ����� Ʋ�Ⱦ�.","��!","�ٽ� �����غ�"};

    public List<Dictionary<string, object>> data_Dialog = new List<Dictionary<string, object>>();
    public string FileAdress;                // ��ũ��Ʈ ���� ��ġ
    public string cuttoonFileAdress;         // ���� ���� ��ġ

    public string Num;                       //��ũ��Ʈ ��ȣ
    public int j;                                  //data_Dialog �ٰ���
    public int c = 0;                              //���� �̹��� ��ȣ
    public int l;                            //�ߴ� �̹��� ��ȣ
    string Answer;               //���� ��ư �ν�

    public bool tutoFinish = false;
    public bool tuto;
    public bool tutoclick;

    public GameObject CCImage;     //ĳ���� �̹���
    public static Sprite[] CCImageList;
    static Image spriteR;

    public GameObject cuttoon;        //���� �̹���
    public Sprite[] cuttoonImageList;
    static Image cuttoonspriteR;

    public TMP_InputField KeyToDreamInput;
    //public Fadeln fade_in_out;
    public UIButton JumpButtons;
    public tutorial tu;
    public Interaction Inter;



    int m = 0;                                  //ī�޶� ����
    public int o = 0;                                  //m������
    int MataNum = 0;                        //���͸��� ��ȣ
    int QBikeSpeed;
    bool BikeQ = false;
    float timer=0.0f;
    float Maxtime;
    bool bikerotate = false;
    Vector3 NPCBike;

    public QuestDontDestroy DontDestroy;
    public QuestScript Quest;
    public VideoScript video;
    public Drawing Draw;
    public BicycleRide bicycleRide;
    public QuestLoad QuestLoad;
    public NpcButtonClick NpcButton;
    [SerializeField]
    private ParticleSystem hairPs;

    public Animator JumpAnimator;
    public Animator NPCJumpAnimator;
    public Animator JumpAnimatorRope;
    public Animator NPCJumpAnimatorRope;
    public Animator PlayerhulaAnimator;
    public GameObject PlayerRope;
    public GameObject NPCRope;

    string PlayerName;

    Animator ToothAnimator;
    private void Awake()
    {
        color = block.GetComponent<Image>().color;
        ChatWin.SetActive(true);
        
        //fade_in_out = GameObject.Find("EventSystem").GetComponent<Fadeln>();
        CCImageList = Resources.LoadAll<Sprite>("Sprites/CCImage/CImage"); //�̹��� ���

        parentscheckTxTNum = PlayerPrefs.GetString("ParentsNo");
        PlayerName = PlayerPrefs.GetString("Nickname");

        DontDestroy = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();


        DontDestroy.LastDay = PlayerPrefs.GetInt("LastQTime");

        if (SceneManager.GetActiveScene().name == "MainField"|| SceneManager.GetActiveScene().name == "AcornVillage")     //���� �ʵ忡 ���� ���� ���
        {
            DontDestroy.LastDay = PlayerPrefs.GetInt("LastQTime");

            string[] QQ = PlayerPrefs.GetString("QuestPreg").Split('_');
            string[] qq = PlayerPrefs.GetString("WeeklyQuestPreg").Split('_');

            string[] q_qid = DontDestroy.QuestIndex.Split('_');
            if (Int32.Parse(QQ[0]) > 3);
            else
                Ride.SetActive(false);
            if (SceneManager.GetActiveScene().name == "MainField")
            {
                Player.position = DontDestroy.gameObject.transform.position;
                if (Int32.Parse(QQ[0]) > 12)
                    AppleTreeObj.SetActive(true);
                else
                    AppleTreeObj.SetActive(false); 

                if (Int32.Parse(QQ[0]) > 21)
                    Kangteagom.SetActive(true);
                else
                    Kangteagom.SetActive(false);
            }
            
            //�ָ�üũ
            DateTime nowDT = DateTime.Now;
            if (nowDT.DayOfWeek == DayOfWeek.Saturday)
                DontDestroy.SDA = true;
            else if (nowDT.DayOfWeek == DayOfWeek.Sunday)
                DontDestroy.weekend = true;
            else
                DontDestroy.weekend = false;
            DontDestroy.ToDay = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));   //����Ʈ�� ���ó�¥ ���� */

            if(DontDestroy.QuestIndex == "22_10")
            {
                Num = "22_10";
                FileAdress = "Scripts/Quest/script";
                NewChat();
                DontDestroy.QuestIndex = "22_1";
                return;
            }
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
                if (DontDestroy.ToDay != DontDestroy.LastDay)
                    QuestLoad.QuestLoadStart();
            }
            
        }
        else if (SceneManager.GetActiveScene().name == "Quiz")
        {
            Quiz_material = Quiz.GetComponent<MeshRenderer>().materials;
        }
        else if (SceneManager.GetActiveScene().name == "Game_Tooth")
        {
            CCImage = GameObject.Find("CCImage");
            SoundEffectManager = GameObject.Find("GameManager");
        }
        
    }

    public void AnimationActivation()
    {
        PlayerhulaAnimator.enabled = true;
    }
    public void VideoTest()
    {
        video.videoClip.clip = video.VideoClip[11];
        movie.SetActive(true);
        video.OnPlayVideo();
        Main_UI.SetActive(false);
        SoundManager = GameObject.Find("SoundManager");
        SoundManager.SetActive(false);
    }
    public void QuestTest()
    {
        PlayerPrefs.SetInt("LastQTime", 0);
        DontDestroy.LastDay = 0;
        
        string QuestType = null;
        if (!DontDestroy.weekend)
        {
            QuestType = PlayerPrefs.GetString("QuestPreg");
        }
        else
            QuestType = PlayerPrefs.GetString("WeeklyQuestPreg");
        DontDestroy.QuestIndex = QuestType;
        QuestLoad.QuestLoadStart();
    }
    public void QuestMoveTest()
    {
        PlayerPrefs.SetInt("LastQTime", 0);
        DontDestroy.LastDay = 0;
        string[] q_qid = DontDestroy.QuestIndex.Split('_');
        string QuestType = null;
        if (Int32.Parse(q_qid[0]) < 22)
        {
            QuestType = "QuestPreg";
        }
        else
            QuestType = "WeeklyQuestPreg";
        PlayerPrefs.SetString(QuestType, DontDestroy.QuestIndex);
        PlayInfoManager.GetQuestPreg();
        QuestLoad.QuestLoadStart();
    }

    public void NotWeekend()
    {
        if (DontDestroy.weekend)
            DontDestroy.weekend = false;
        else
            DontDestroy.weekend = true;
    }
    public void ToothQuest()
    {
        ToothAnimator = GameObject.Find("ToothBrush").transform.Find("Armature").gameObject.GetComponent<Animator>();
        Num = "1_2";
        FileAdress = "Scripts/Quest/scriptWeekend";
        NewChat();
    }


    IEnumerator QBikeLoop()
    {
        while (true)
        {
            if (JumpButtons.Playerrb.velocity.magnitude > QBikeSpeed)
            {
                if (timer > Maxtime)
                {
                    Debug.Log(QBikeSpeed);
                    if (bikerotate)
                    {
                        JumpButtons.Playerrb.velocity = JumpButtons.Playerrb.velocity.normalized * 0;
                        NPCBike = new Vector3(-4, 0, 0);
                        Player.rotation = Quaternion.Euler(0, 90, 0);
                        bikerotate = false;
                    }
                    else
                    {
                        JumpButtons.Playerrb.velocity = JumpButtons.Playerrb.velocity.normalized * 0;
                        NPCBike = new Vector3(4, 0, 0);
                        Player.rotation = Quaternion.Euler(0, -90, 0);
                        bikerotate = true;
                    }
                    timer = 0;
                }
                JumpButtons.Playerrb.velocity = JumpButtons.Playerrb.velocity.normalized * QBikeSpeed;
                if (Maxtime == 8)
                {
                    BikeNPC.transform.position = Player.position + NPCBike;
                    BikeNPC.transform.rotation = Player.rotation;
                }
                else
                {
                    Vector3 targetPositionNPC;
                    targetPositionNPC = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
                    BikeNPC.transform.LookAt(targetPositionNPC);
                }
            }
            timer += Time.deltaTime;
            JumpButtons.Playerrb.AddRelativeForce(Vector3.forward * 1000f); //�� �������� �б� (���� * ��)

            yield return null;
        }
    }
    
    public void skip()
    {
        Num = "0_2";
        o = 11;
        NewChat();
    }
    public void NewChat()
    {
        Input.multiTouchEnabled = false;
        //PlayerCamera.SetActive(true);
        data_Dialog = CSVReader.Read(FileAdress);
        Debug.Log("��ũ��Ʈ ����"+data_Dialog.Count);
        for (int k = 0; k <= data_Dialog.Count; k++)
        {
            if (data_Dialog[k]["scriptNumber"].ToString().Equals(Num))
            {
                j = k; 
                if (DontDestroy.tutorialLoading)
                    j=14;
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

    public void AchangeMoment()  //�÷��̾� �̵�, ī�޶� ����
    {
        switch (o)
        {
            case 1: //�����
                Player.transform.position = new Vector3(12.6f, -8.4f, -4);
                Nari.transform.position = Player.transform.position + new Vector3(1, 0, 0);
                Player.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                Nari.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                NariMom.transform.position = Player.transform.position + new Vector3(2, 0, 0);
                NariMom.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                break;
            case 2:  //����
                Player.transform.position = new Vector3(13.6f, -8.4f, -4);
                Nari.transform.position = Player.transform.position + new Vector3(1, 0, 0);
                NariMom.transform.position = Player.transform.position + new Vector3(2, 0, 0);
                break;
            case 3: //���ŵ���
                Player.transform.position = new Vector3(14.6f, -8.4f, -4);
                Nari.transform.position = Player.transform.position + new Vector3(1, 0, 0);
                NariMom.transform.position = Player.transform.position + new Vector3(2, 0, 0);
                break;
            default:
                break;
        }
    }
    public void changeMoment()  //�÷��̾� �̵�, ī�޶� ����
    {
        switch (o)
        {
            case 1: //��� ��
                Player.transform.position = new Vector3(-36.2f, -4.95455313f, -9.4f);
                break;
            case 2:  //����
                cuttoon.SetActive(false);
                Player.transform.position = new Vector3(1.6f, -6.41821432f, 1.4f);
                break;
            case 3: //������ ����
                if (DontDestroy.tutorialLoading)
                    Nari.transform.position = Player.transform.position + new Vector3(1, 0, 0);
                DontDestroy.tutorialLoading = false;
                break;
            case 4: //�����
                Player.transform.position = new Vector3(12.9f, -4.33371019f, 26.4f);
                Nari.transform.position = Player.transform.position + new Vector3(1, 0, 0);
                break;
            case 5: //����
                Player.transform.position = new Vector3(-16.6f, -6.41383314f, 8.8f);
                Nari.transform.position = Player.transform.position + new Vector3(1, 0, 0);
                break;
            case 6: //����
                Player.transform.position = new Vector3(49.5f, -6.41383314f, 14.2f);
                Nari.transform.position = Player.transform.position + new Vector3(1, 0, 0);
                break;
            case 7: //���
                Player.transform.position = new Vector3(43.3f, -6.41383266f, 14.3f);
                Nari.transform.position = Player.transform.position + new Vector3(1, 0, 0);
                break;
            case 8: //������
                Player.transform.position = new Vector3(5.7f, -6.39158726f, -10.2f);
                Nari.transform.position = Player.transform.position + new Vector3(1, 0, 0);
                break;
            case 9:  //����
                Player.transform.position = new Vector3(-16.8f, -6.41383314f, -9.1f);
                Nari.transform.position = Player.transform.position + new Vector3(1, 0, 0);
                break;
            case 10: //���۸�
                Player.transform.position = new Vector3(58.1f, -6.41383362f, -2.5f);
                Nari.transform.position = Player.transform.position + new Vector3(1, 0, 0);
                break;
            case 11: //��
                Player.transform.position = new Vector3(1.6f, -6.41821432f, 1.4f);
                Nari.transform.position = Player.transform.position + new Vector3(1, 0, 0);
                break;
            case 12:  //Ʃ�丮�� ����
                PlayerPrefs.SetString("QuestPreg", DontDestroy.QuestIndex);
                PlayInfoManager.GetQuestPreg();
                //Save_Log.instance.SaveQEndLog();    //����Ʈ ���� �α� ���
                SceneLoader.instance.GotoMainField();
                break;
            case 13:
                Player.transform.position = new Vector3(-36.2f, -4.95455313f, -9.4f);
                Player.transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
                GameObject Parents = GameObject.Find("parents(Clone)");
                Parents.transform.position = Player.transform.position + new Vector3(-1.5f, 0, 0);
                Parents.transform.rotation = Quaternion.Euler(new Vector3(0, 150, 0));
                break;
            default:
                break;
        }
    }
    GameObject mouth; //��ġ�� ��
    public void QuestSubChoice()
    {
        Debug.Log("Ÿ��"+data_Dialog[j]["scriptType"].ToString());
        if (data_Dialog[j]["scriptType"].ToString().Equals("quiz"))  //�������
        {
            MataNum = Int32.Parse(data_Dialog[j]["QuizNumber"].ToString());
            scriptLine();
            for (int i = 0; i < QuizButton.Length; i++)
            {
                if (data_Dialog[j]["select" + (i + 1)].ToString().Equals("��ĭ"))
                    QuizButton[i].transform.parent.gameObject.SetActive(false);
                else
                    QuizButton[i].transform.parent.gameObject.SetActive(true);
                QuizButton[i].text = data_Dialog[j]["select" + (i + 1)].ToString();
            }
            QuizMate();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("choice"))  //������
        {
            block.SetActive(false);
            j--;
            QuizCho();
        }
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

        }  //Main Move
        else if (data_Dialog[j]["scriptType"].ToString().Equals("AMove"))  //������
        {
            o += 1;
            AchangeMoment();

            j += 1;
            Line();

        } //dotori Move
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
        else if (data_Dialog[j]["scriptType"].ToString().Equals("tutorial"))//Ʃ�丮��
        {
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("video"))//������ ����
        {
            video.videoClip.clip = video.VideoClip[Int32.Parse(data_Dialog[j]["cuttoon"].ToString())];
            movie.SetActive(true);
            video.OnPlayVideo();
            ChatWin.SetActive(false);
            SoundManager = GameObject.Find("SoundManager");
            SoundManager.SetActive(false);
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("videoEnd")) //������ ���� ����       ���� ���� �� ��ư�� �߰����� �װ� ������ Ȯ��â���� �Ѿ�Բ�
        {
            if (videocheckTxT.text.Equals(parentscheckTxTNum))
            {
                GameObject.Find("checkUI").SetActive(false);
                videocheckTxT.text = null;
                movie.SetActive(false);
                video.OnFinishVideo();
                ChatWin.SetActive(true);
                SoundManager.SetActive(true);
                scriptLine();
            }
            else
            {
                ErrorWin.SetActive(true);
            }
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("Bike"))
        {
            cuttoon.SetActive(false);
            ChatWin.SetActive(false);
            Bike.SetActive(true);
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("Bicycle"))
        {
            if (!BikeQ)
            {
                NPCBike = new Vector3(-4, 0, 0);
                bicycleRide.RideOn();
                Destroy(GameObject.Find("Qbicycle(Clone)"));
                Player.position = new Vector3(36, 5, -23);
                Player.rotation = Quaternion.Euler(0, 90, 0);
                QBikeSpeed = 3;
                Maxtime = 8;
                BikeQ = true;
                StartCoroutine("QBikeLoop");
                NPCJumpAnimator.SetBool("BikeWalk", true);
            }
            else if (QBikeSpeed == 12)
            {
                BikeQ = false;
                Vector3 targetPositionNPC;
                Vector3 targetPositionPlayer;
                targetPositionNPC = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
                targetPositionPlayer = new Vector3(BikeNPC.transform.position.x, BikeNPC.transform.position.y, BikeNPC.transform.position.z);
                BikeNPC.transform.LookAt(targetPositionNPC);
                Player.transform.LookAt(targetPositionPlayer);
                JumpButtons.Playerrb.velocity = JumpButtons.Playerrb.velocity.normalized * 0;
                StopCoroutine("QBikeLoop");
                Ride.SetActive(true);
            }
            else if (BikeQ)
            {

                //���̵� �� ���̵� �ƿ��ϸ鼭 ȭ�鿡 �ѽð� ��... ����
                QBikeSpeed = 12;
                Maxtime = 3;
                Player.position = new Vector3(36, 5, -23);
                BikeNPC.transform.position = Player.position + new Vector3(10, 0, 10);
                Player.rotation = Player.rotation = Quaternion.Euler(0, 90, 0);
                bikerotate = false;
                timer = 0;
                NPCJumpAnimator.SetBool("BikeWalk", false);
            }

            scriptLine();

        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("hair"))
        {
            ChatWin.SetActive(false);
            hairFX(GameObject.Find("Player"));
            j++;
            Invoke("clearHair", 1f);
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("note"))        //����Ʈ�߰��ֵ�
        {
            if (data_Dialog[j]["cuttoon"].ToString().Equals("0"))
            {
                j++;
                ChatWin.SetActive(false);
                Note.SetActive(true);
            }
            else
            {
                j++;
                ChatWin.SetActive(false);
            }
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("noteFinish"))        //����Ʈ�߰��ֵ�
        {
            Note.transform.Find("Button").gameObject.SetActive(false);
            Draw.FinishWrite();
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("noteEnd"))
        {
            Note.SetActive(false);
            //ChatWin.SetActive(true);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("nutrient"))        //����Ʈ�߰��ֵ�
        {
            cuttoon.SetActive(false);
            j++;
            ChatWin.SetActive(false);
            nutrient.SetActive(true);
            Debug.Log("nut");
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("nutrientEnd"))
        {
            j++;
            nutrient.SetActive(false);
            Cuttoon();
            ChatWin.SetActive(false);
            Invoke("scriptLine", 2f);   //������ �� ��ũ��Ʈ ���
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("train"))
        {
            ChatWin.SetActive(false);
            Value.SetActive(true);
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("trainEnd"))
        {
            Value.SetActive(false);
            scriptLine();

        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("draw"))
        {
            ChatWin.SetActive(false);
            Draw.ChangeDrawCamera();
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("drawFinish"))
        {
            GameObject.Find("DrawUI").transform.Find("Button").gameObject.SetActive(false);
            Draw.Draw = false;
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("Screenshot"))
        {
            screenShot.SetActive(true);
            screenShotExit.SetActive(true);
            ChatWin.SetActive(false);
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("drawEnd"))
        {
            screenShot.SetActive(false);
            Draw.ChangeDrawCamera();
            Main_UI.SetActive(false);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("listen"))
        {
            ChatWin.SetActive(false);
            c = 0;
            InvokeRepeating("QSound", 0f, 4f);
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("song"))
        {
            //����� �� Ʋ��
            SoundManager SoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            SoundManager.Sound("HaHasong");
            Invoke("scriptLine", 20f);   //������ �� ��ũ��Ʈ ���
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("songend"))
        {
            //���� �뷡�� �ٲٱ�
            SoundManager SoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            SoundManager.Sound("BGMField");
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("JumpRope"))
        {
            //�ٳѱ�
            if (data_Dialog[j]["cuttoon"].ToString().Equals("0"))
            {
                GameObject.Find("Himchan").transform.rotation = Quaternion.Euler(0, 180, 0);
                PlayerRope.SetActive(true);
                NPCJumpAnimator.SetBool("JumpRope", true);
                NPCJumpAnimatorRope.SetBool("JumpRope", true);
            }
            else if (data_Dialog[j]["cuttoon"].ToString().Equals("1"))
            {
                Player.position = new Vector3(54, 5, -14);
                Player.rotation = Quaternion.Euler(0, 180, 0);
                JumpAnimator.SetBool("JumpRope", true);
                JumpAnimatorRope.SetBool("JumpRope", true);
                JumpAnimator.speed = 0.3f;
                JumpAnimatorRope.speed = 0.3f;
            }
            else if (data_Dialog[j]["cuttoon"].ToString().Equals("2"))
            {
                JumpAnimator.speed = 1f;
                JumpAnimatorRope.speed = 1f;
            }
            else if (data_Dialog[j]["cuttoon"].ToString().Equals("3"))
            {
                NPCJumpAnimator.SetBool("JumpRopeSide", true);
                NPCJumpAnimatorRope.SetBool("JumpRopeSide", true);
            }
            else if (data_Dialog[j]["cuttoon"].ToString().Equals("4"))
            {
                NPCRope.transform.rotation = Quaternion.Euler(new Vector3(-180, 180, -180));
                NPCJumpAnimator.SetBool("JumpRope", true);
                NPCJumpAnimatorRope.SetBool("JumpRope", true);
            }
            else if (data_Dialog[j]["cuttoon"].ToString().Equals("5"))
            {
                NPCRope.transform.rotation = Quaternion.Euler(-180, 0, -180);
                NPCJumpAnimator.SetBool("JumpRope", true);
                NPCJumpAnimatorRope.SetBool("JumpRope", true);
                NPCJumpAnimatorRope.speed = 2f;
            }
            else if (data_Dialog[j]["cuttoon"].ToString().Equals("6"))
            {
                PlayerRope.SetActive(false);
                NPCRope.SetActive(false);
            }
            else if (data_Dialog[j]["cuttoon"].ToString().Equals("7"))
            {
                PlayerRope.SetActive(true);

                GameObject NPC = GameObject.Find(Inter.NameNPC);

                Player.transform.position = new Vector3(54, 5, -15);
                NPC.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                NPCRope.SetActive(true);

                Vector3 targetPositionNPC;
                Vector3 targetPositionPlayer;
                targetPositionNPC = new Vector3(Player.transform.position.x, NPC.transform.position.y, Player.transform.position.z);
                NPC.transform.LookAt(targetPositionNPC);
                targetPositionPlayer = new Vector3(NPC.transform.position.x, Player.transform.position.y, NPC.transform.position.z);
                Player.transform.LookAt(targetPositionPlayer);
            }
            Invoke("scriptLine", 2f);
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("JumpRopeEnd"))
        {
            //�ٳѱ�
            NPCJumpAnimator.SetBool("JumpRope", false);
            NPCJumpAnimator.SetBool("JumpRopeSide", false);
            NPCJumpAnimator.SetBool("JumpRopeBack", false);
            NPCJumpAnimatorRope.SetBool("JumpRope", false);
            NPCJumpAnimatorRope.SetBool("JumpRopeSide", false);
            NPCJumpAnimatorRope.SetBool("JumpRopeBack", false);
            JumpAnimator.SetBool("JumpRope", false);
            JumpAnimator.SetBool("JumpRopeSide", false);
            JumpAnimator.SetBool("JumpRopeBack", false);
            JumpAnimatorRope.SetBool("JumpRope", false);
            JumpAnimatorRope.SetBool("JumpRopeSide", false);
            JumpAnimatorRope.SetBool("JumpRopeBack", false);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("MasterOfMtLife"))
        {
            //������ ��������
            MasterOfMtLife.SetActive(true);
            ChatWin.SetActive(false);
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("MasterOfMtLifeEnd"))
        {
            MasterOfMtLife.SetActive(false);
            ChatWin.SetActive(true);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("LoveNature"))
        {
            LoveNature.SetActive(true);
            ChatWin.SetActive(false);
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("LoveNatureEnd"))
        {
            LoveNature.SetActive(false);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("AppleTree"))
        {
            AppleTree.SetActive(true);
            ChatWin.SetActive(false);
            //scriptLine();
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("AppleTreeEnd"))
        {
            ChatWin.SetActive(true);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("MakeAppleTree"))
        {
            AppleTree.SetActive(false);
            AppleTreeObj.SetActive(true);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("KeyToDream"))
        {
            KeyToDream.SetActive(true);
            ChatWin.SetActive(false);
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("KeyToDreamEnd"))
        {
            KeyToDreamInput.text = null;
            KeyToDream.SetActive(false);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("Tooth"))
        {
            if (Int32.Parse(data_Dialog[j]["cuttoon"].ToString()) == 0)
            {
                DontDestroy.QuestIndex = "22";
                SceneLoader.instance.GotoToothGame();
            }
            else if (SceneManager.GetActiveScene().name == "Game_Tooth")
            {
                ChatWin.SetActive(false);
                GameObject QToothBrush = GameObject.Find("QToothBrush(Clone)");
                GameObject Maincam = GameObject.Find("Main Camera");
                if (Int32.Parse(data_Dialog[j]["cuttoon"].ToString()) == 1)
                {
                    mouth = GameObject.Find("mouth");
                    mouth.SetActive(false);
                    Instantiate(Resources.Load<GameObject>("Prefabs/Tooth/QTooth/AH"), new Vector3(0, -13, 17), Quaternion.Euler(new Vector3(0, 90, 0)));
                    QToothBrush.transform.position = new Vector3(15, 8, 10);
                    QToothBrush.transform.rotation = Quaternion.Euler(new Vector3(1, 294, 272));
                    Maincam.transform.position = new Vector3(26, 25, 0);
                    Maincam.transform.rotation = Quaternion.Euler(new Vector3(36, 332, 356));


                    ToothAnimator.SetBool("1", true);
                }
                else if (Int32.Parse(data_Dialog[j]["cuttoon"].ToString()) == 2)
                {
                    mouth.SetActive(true);
                    GameObject.Find("AH(Clone)").SetActive(false);
                    QToothBrush.transform.position = new Vector3(-13, -10, 18);
                    QToothBrush.transform.rotation = Quaternion.Euler(new Vector3(52, 176, 89));
                    Maincam.transform.position = new Vector3(2, 6, 29);
                    Maincam.transform.rotation = Quaternion.Euler(new Vector3(63, 180, 0));
                    ToothAnimator.SetBool("1", true);
                }
                else if (Int32.Parse(data_Dialog[j]["cuttoon"].ToString()) == 3)
                {
                    QToothBrush.transform.position = new Vector3(16, -6, 15);
                    QToothBrush.transform.rotation = Quaternion.Euler(new Vector3(63, -9, 347));
                    Maincam.transform.position = new Vector3(15, 4, 4);
                    Maincam.transform.rotation = Quaternion.Euler(new Vector3(16, 0, 0));
                    ToothAnimator.SetBool("2", true);
                    ToothAnimator.SetBool("1", false);
                }
                else if (Int32.Parse(data_Dialog[j]["cuttoon"].ToString()) == 4)
                {
                    QToothBrush.transform.position = new Vector3(20, 0, 26);
                    QToothBrush.transform.rotation = Quaternion.Euler(new Vector3(63, -3, 84));
                    Maincam.transform.position = new Vector3(0, 5, 2);
                    Maincam.transform.rotation = Quaternion.Euler(new Vector3(20, 0, 0));
                    ToothAnimator.SetBool("1", true);
                    ToothAnimator.SetBool("2", false);
                }
                else if (Int32.Parse(data_Dialog[j]["cuttoon"].ToString()) == 5)
                {
                    QToothBrush.transform.position = new Vector3(13.3f, -11, 19.7f);
                    QToothBrush.transform.rotation = Quaternion.Euler(new Vector3(350, 299, 4));
                    Maincam.transform.position = new Vector3(11.5f, -3, -1);
                    GameObject.Find("QToothBrush(Clone)").transform.Find("Dentalfloss").gameObject.SetActive(true);
                    GameObject.Find("ToothBrush").SetActive(false);
                    ToothAnimator.SetBool("finish", true);
                    ToothAnimator.SetBool("2", false);
                }
                else if (Int32.Parse(data_Dialog[j]["cuttoon"].ToString()) == 6)
                {
                    QToothBrush.SetActive(false);
                    Maincam.transform.position = new Vector3(0, 16.6f, -27);
                    Maincam.transform.rotation = Quaternion.Euler(new Vector3(7.7f, 0, 0));
                    scriptLine();
                    return;
                }
                else if (Int32.Parse(data_Dialog[j]["cuttoon"].ToString()) == 7)
                {
                    DontDestroy.ToothQ = true;
                    DontDestroy.QuestIndex = "22_1";
                    PlayerPrefs.SetString("WeeklyQuestPreg", DontDestroy.QuestIndex); //�ָ�
                    SceneLoader.instance.GotoMainField();
                    return;
                }

                Invoke("scriptLine", 3f);   //������ �� ��ũ��Ʈ ���
            }
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
        else if (data_Dialog[j]["scriptType"].ToString().Equals("moveTos"))
        {
            Player.transform.position = new Vector3(-17.3f, -6.41383362f, -20);
            Player.transform.rotation = Quaternion.Euler(new Vector3(0, 151, 0));

            Nari.transform.position = Player.transform.position + new Vector3(1, 0, 0);
            Nari.transform.rotation = Quaternion.Euler(new Vector3(0, 151, 0));

            Kangteagom.SetActive(true);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("BMI"))
        {
            BMI.SetActive(true);
            ChatWin.SetActive(false);
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("BMITalk"))
        {
            Inter.NpcNameTF = true;
            BMI.SetActive(false);
            BMItalk.SetActive(true);
            ChatWin.SetActive(true);
            j++;
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("BMIEnd"))
        {
            Inter.NpcNameTF = false;
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("moveToA"))
        {
            Cuttoon();
            DontDestroy.QuestIndex = "22_10";
            SceneLoader.instance.GotoMainAcronVillage();
        } //���丮������ �̵�
        else if (data_Dialog[j]["scriptType"].ToString().Equals("JumpRope"))
        {
            //�ٳѱ�
            if (data_Dialog[j]["cuttoon"].ToString().Equals("0"))
            {
                GameObject.Find("Himchan").transform.rotation = Quaternion.Euler(0, 180, 0);
                PlayerRope.SetActive(true);
                NPCJumpAnimator.SetBool("JumpRope", true);
                NPCJumpAnimatorRope.SetBool("JumpRope", true);
            }
            else if (data_Dialog[j]["cuttoon"].ToString().Equals("1"))
            {
                Player.position = new Vector3(54, 5, -14);
                Player.rotation = Quaternion.Euler(0, 180, 0);
                JumpAnimator.SetBool("JumpRope", true);
                JumpAnimatorRope.SetBool("JumpRope", true);
                JumpAnimator.speed = 0.3f;
                JumpAnimatorRope.speed = 0.3f;
            }
            else if (data_Dialog[j]["cuttoon"].ToString().Equals("2"))
            {
                JumpAnimator.speed = 1f;
                JumpAnimatorRope.speed = 1f;
            }
            else if (data_Dialog[j]["cuttoon"].ToString().Equals("3"))
            {
                NPCJumpAnimator.SetBool("JumpRopeSide", true);
                NPCJumpAnimatorRope.SetBool("JumpRopeSide", true);
            }
            else if (data_Dialog[j]["cuttoon"].ToString().Equals("4"))
            {
                NPCRope.transform.rotation = Quaternion.Euler(new Vector3(-180, 180, -180));
                NPCJumpAnimator.SetBool("JumpRope", true);
                NPCJumpAnimatorRope.SetBool("JumpRope", true);
            }
            else if (data_Dialog[j]["cuttoon"].ToString().Equals("5"))
            {
                NPCRope.transform.rotation = Quaternion.Euler(-180, 0, -180);
                NPCJumpAnimator.SetBool("JumpRope", true);
                NPCJumpAnimatorRope.SetBool("JumpRope", true);
                NPCJumpAnimatorRope.speed = 2f;
            }
            else if (data_Dialog[j]["cuttoon"].ToString().Equals("6"))
            {
                PlayerRope.SetActive(false);
                NPCRope.SetActive(false);
            }
            else if (data_Dialog[j]["cuttoon"].ToString().Equals("7"))
            {
                PlayerRope.SetActive(true);

                GameObject NPC = GameObject.Find(Inter.NameNPC);

                Player.transform.position = new Vector3(54, 5, -15);
                NPC.transform.position = Player.transform.position + new Vector3(5, 0, 0);
                NPCRope.SetActive(true);

                Vector3 targetPositionNPC;
                Vector3 targetPositionPlayer;
                targetPositionNPC = new Vector3(Player.transform.position.x, NPC.transform.position.y, Player.transform.position.z);
                NPC.transform.LookAt(targetPositionNPC);
                targetPositionPlayer = new Vector3(NPC.transform.position.x, Player.transform.position.y, NPC.transform.position.z);
                Player.transform.LookAt(targetPositionPlayer);
            }
            Invoke("scriptLine", 2f);
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("JumpRopeEnd"))
        {
            //�ٳѱ�
            NPCJumpAnimator.SetBool("JumpRope", false);
            NPCJumpAnimator.SetBool("JumpRopeSide", false);
            NPCJumpAnimator.SetBool("JumpRopeBack", false);
            NPCJumpAnimatorRope.SetBool("JumpRope", false);
            NPCJumpAnimatorRope.SetBool("JumpRopeSide", false);
            NPCJumpAnimatorRope.SetBool("JumpRopeBack", false);
            JumpAnimator.SetBool("JumpRope", false);
            JumpAnimator.SetBool("JumpRopeSide", false);
            JumpAnimator.SetBool("JumpRopeBack", false);
            JumpAnimatorRope.SetBool("JumpRope", false);
            JumpAnimatorRope.SetBool("JumpRopeSide", false);
            JumpAnimatorRope.SetBool("JumpRopeBack", false);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("Nanum")) //26
        {
            cuttoon.SetActive(false);
            Nanum.SetActive(true);
            ChatWin.SetActive(false);
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("NanumEnd"))
        {
            Nanum.SetActive(false);
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("Jewel")||data_Dialog[j]["scriptType"].ToString().Equals("Jewelselect"))
        {
            ChatWin.SetActive(false);
            Jewel.SetActive(true);
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("JewelFinish"))        //����Ʈ�߰��ֵ�
        {
            Draw.JFinishWrite();
            scriptLine();
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("JewelEnd"))
        {
            Jewel.SetActive(false);
            scriptLine();

        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("Healthy")) //29
        {
            Healthy.SetActive(true);
            ChatWin.SetActive(false);
            j++;
        }
        else if (data_Dialog[j]["scriptType"].ToString().Equals("HealthyEnd"))
        {
            Healthy.SetActive(false);
            scriptLine();
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

    public void QSound()
    {
        string SoundName=null;
        if (c == 0)
        {
            SoundName = "QWind";
        }
        else if (c == 1)
        {
            SoundName = "QWater";
        }
        else if (c == 2)
        {
            SoundName = "QBird";
        }
        else
        {
            Invoke("scriptLine", 4f);
            CancelInvoke("QSound");
            return;
        }
        SoundEffectManager.GetComponent<SoundEffect>().Sound(SoundName);
        c++;
    }
    public void TQ()
    {
        GameObject ToothBrush = GameObject.Find("ToothBrush");
        GameObject QToothBrush = GameObject.Find("QToothBrush(Clone)");
        if (c == 0)
        {
            QToothBrush.transform.position = new Vector3(19, 1, 19);
        }
    }
    public void Line()  //�ٳѱ� (scriptType�� ���� �ɷ���)
    {
        
        block.SetActive(true);
        //Debug.Log("scriptNumber" + data_Dialog[j]["scriptNumber"].ToString());
        //Debug.Log("scriptCount" + j);
        //Debug.Log("script" + data_Dialog[j]["dialog"].ToString());
        if (tuto && tutoFinish)
        {
            chatCanvus.SetActive(true);
            color.a = 0;
            block.GetComponent<Image>().color = color;
            tuto = false;
            tutoFinish = false;
        }
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
        //Debug.Log(data_Dialog[j]["SoundEffect"].ToString());
        if (data_Dialog[j]["SoundEffect"].ToString().Equals("Null"))
        {; }
        else
        {
            string SoundName = data_Dialog[j]["SoundEffect"].ToString();
            SoundEffectManager.GetComponent<SoundEffect>().Sound(SoundName);
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

        LoadTxt = data_Dialog[j]["dialog"].ToString().Replace("P_name",PlayerName);//���ð� ��������
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
        c= Int32.Parse(data_Dialog[j]["cuttoon"].ToString());
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
        if (Inter.NameNPC.Equals("WallMirror") || Inter.NameNPC.Equals("GachaMachine")|| Inter.NameNPC.Equals("ThankApplesTree"))
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
            Invoke("stopCorout",1f);
        }
    }


    public void Buttons()      //npc��ȭ ��ȣ�ۿ� ������ ��
    {
        if (Inter.NameNPC.Equals(DontDestroy.ButtonPlusNpc))
            NPCButton += 1;

        NPCButtons.SetActive(true);
        for (int i= 0; i < NPCButton;i++)
        {
            string selecNumber = "select"+(i+1).ToString();
            SelecButton[i].SetActive(true);
            SelecButtonTxt[i].text = data_Dialog[j-1][selecNumber].ToString();
        }
    }
    public void ButtonsFalse()      //npc��ȭ ��ȣ�ۿ� ������ ��
    {
        NPCButtons.SetActive(false);
        for (int i= 0; i < SelecButton.Length; i++)
        {
            string selecNumber = "select"+(i+1).ToString();
            SelecButton[i].SetActive(false);
        }
    }

    public void QuizMate() //������ ���׸��� ����
    {
        Quiz_material[1] = material[MataNum]; //0�� ���׸��� ��ȣ
        Quiz.GetComponent<MeshRenderer>().materials = Quiz_material;

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
        yield return new WaitForSecondsRealtime(0.1f);
        if (data_Dialog[j - 1]["scriptType"].ToString().Equals("tutorial") || tuto)
        {
            Debug.Log("Ʃ�丮�� ���ष");
            Invoke("Tutorial_", 1f);
        }
        else
        {
            block.SetActive(false);
        }
    }

    void QuizCho()
    {
        Chat.SetActive(false);
        Button.SetActive(true); //����Ƽ���� ��ư ��ġ �ű�
    }

    void Tutorial_()
    {
        tu.Tutorial();
        tuto = true;
    }

    public void Answer1()
    {
        Answer = "1";
        QuizeAnswer();
    }
    public void Answer2()
    {
        Answer = "2";
        QuizeAnswer();
    }
    public void Answer3()
    {
        Answer = "3";
        QuizeAnswer();
    }
    public void Answer4()
    {
        Answer = "4";
        QuizeAnswer();
    }
    public void Answer5()
    {
        Answer = "5";
        QuizeAnswer();
    }
    public void QuizeAnswer()
    {
        if (data_Dialog[j]["answer"].ToString().Equals(Answer))
        {
            Chat.SetActive(true);
            O();
            scriptLine();
        }

        else
        {
            Chat.SetActive(true);
            X();
        }
    }

    public void O()//���� ����� ��
    {
        block.SetActive(true);
        OImage.SetActive(true); 
        Button.SetActive(false);
        j++;

    }
    public void X()//Ʋ�� ���� ����� ��
    {
        int x = UnityEngine.Random.Range(0,3);
        block.SetActive(true);
        XImage.SetActive(true);
        Button.SetActive(false);
        LoadTxt = Xdialog[x];
        StartCoroutine(_typing());

    }

    public void Xback()//X�̹��� ��ư
    {
        XImage.SetActive(false);
        Button.SetActive(true);
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
            if (SceneManager.GetActiveScene().name == "MainField"|| SceneManager.GetActiveScene().name == "AcornVillage")
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
    public void hairFX(GameObject go)    //�Ӹ� ��¦!�ϴ� ��ƼŬ
    {
        Debug.Log("��¦");
        ParticleSystem newfx = Instantiate(hairPs);
        newfx.transform.position = go.transform.position+new Vector3(0,5,0);
        newfx.transform.SetParent(go.transform);

        newfx.Play();
    }

    
    void clearHair()
    {
        Chat.SetActive(true);
        scriptLine();
    }

    void Panorama()
    {
        cuttoon.SetActive(true);
        cuttoonspriteR = cuttoon.GetComponent<Image>();
        cuttoonspriteR.sprite = cuttoonImageList[c];
        Debug.Log("c=" + c);
        if(c == Int32.Parse(data_Dialog[j]["cuttoon"].ToString()))
        {
            CancelInvoke("Panorama");
            Invoke("scriptLine", 2f);
        }
        c++;
    }

}
