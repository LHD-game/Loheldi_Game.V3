using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.ProBuilder.MeshOperations;
using System.Net.Sockets;
//using static UnityEditor.PlayerSettings.Switch;

public class LodingTxt : MonoBehaviour
{
    [Header("Player")]
    public Transform Player;
    public Transform Nari;
    public Transform NariMom;


    [Header("Chatting")]
    public Text chatName;
    public Text chatTxt;
    public GameObject[] SelecButton = new GameObject[5];
    public Text[] SelecButtonTxt = new Text[5];

    public GameObject Arrow;
    public GameObject block;        //넘김방지 맨앞 블럭
    public Color color;
    public GameObject Chat;
    public GameObject ChatWin;

    public int NPCButton = 0;
    public string LoadTxt;
    private string[] Xdialog = { "다시 한번 생각해봐.", "아쉽지만 틀렸어.", "땡!", "다시 도전해봐" };

    public List<Dictionary<string, object>> data_Dialog = new List<Dictionary<string, object>>();
    public string FileAdress;                // 스크립트 파일 위치
    public string cuttoonFileAdress;         // 컷툰 파일 위치

    public string Num;                       //스크립트 번호
    public int j;                                  //data_Dialog 줄갯수
    public int c = 0;                              //컷툰 이미지 번호
    public int l;                            //뜨는 이미지 번호
    string Answer;               //누른 버튼 인식
    public string PlayerName;

    public GameObject CCImage;     //캐릭터 이미지
    public static Sprite[] CCImageList;
    public static Image spriteR;

    public GameObject cuttoon;        //컷툰 이미지
    public Sprite[] cuttoonImageList;
    static Image cuttoonspriteR;
    public Text cuttontext;

    [Header("mail")]
    //메일 내용 뜨는거
    public Text Mailcontent;

    [Header("Quiz")]
    public Text[] QuizButton = new Text[3];
    public GameObject OImage;
    public GameObject XImage;
    public GameObject Quiz;
    public Material[] Quiz_material;
    [SerializeField]
    private Material[] material; 
    int MataNum = 0;                        //메터리얼 번호

    [Header("Quest")]
    public InputField videocheckTxT;
    public InputField ParentscheckTxt;
    public TMP_InputField KeyToDreamInput;
    public string parentscheckTxTNum;
    public GameObject ErrorWin;      //홈트인증실패
    public GameObject ClearWin;     //인증클리어
    public GameObject ParentsErrorWin;  //인증실패
    public GameObject ParentscheckUI;  //인증창

    public GameObject Main_UI;
    public GameObject Button;
    public GameObject NPCButtons;
    public GameObject chatCanvus;
    public GameObject shopCanvus;
    public GameObject MailCanvus;

    int QBikeSpeed;
    bool BikeQ = false;
    float timer = 0.0f;
    float Maxtime;
    bool bikerotate = false;
    Vector3 NPCBike;

    public Animator JumpAnimator; //플레이어 애니메이터
    public Animator NPCJumpAnimator;  //힘찬이 애니메이터
    public Animator JumpAnimatorRope;
    public Animator NPCJumpAnimatorRope;
    public GameObject PlayerRope;
    public GameObject NPCRope;

    public Animator PlayerHulaAnimator;  //플레이어 accessories에 들어있는 애니메이터
    public Animator NPCHulaAnimator;   //힘찬이 모델링 최상위오브젝트에 든 애니메이터
    public Animator PHulaAnimator; //플레이어 훌라후프 애니메이터
    public Animator NHulaAnimator;  //NPC훌라후프 애니메이터
    public GameObject PlayerHula; //플레이어 훌라후프 모델링
    public GameObject NPCHula;  //NPC훌라후프 모델링

    Animator ToothAnimator;
    [Header("QuestWindow")]
    public GameObject movie;
    [SerializeField]
    private Sprite videoImg;
    public GameObject NextVideoWindow;
    public GameObject VideoParentsCheckUI;
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
    public GameObject spell;

    [Header("Bike&BikeQuest")]
    public GameObject Ride;
    public GameObject Bike;
    public GameObject BikeNPC;

    [Header("ThankAppleTree")]
    public GameObject AppleTreeObj;

    [Header("GotoDotori")]
    public GameObject Kangteagom;

    [Header(".ect")]
    public int o = 0;                                  //move서포터
    public GameObject SoundEffectManager;
    GameObject SoundManager;
    [SerializeField]
    private ParticleSystem hairPs;
    [Header("tutorial")]
    public bool tutoFinish = false;
    public bool tuto;
    public bool tutoclick;

    //public Fadeln fade_in_out;


    

    [Header("scrips")]
    public UIButton JumpButtons;
    public tutorial tu;
    public Interaction Inter;
    public QuestStatus QS;
    public QuestDontDestroy DontDestroy;
    public QuestScript Quest;
    public VideoScript video;
    public Drawing Draw;
    public BicycleRide bicycleRide;
    public QuestLoad QuestLoad;
    public NpcButtonClick NpcButton;

    [Header("Language")]
    public Dropdown LanguageDropDown;

    private void Awake()
    {
        Input.multiTouchEnabled = true;
        color = block.GetComponent<Image>().color;
        ChatWin.SetActive(true);

        SoundManager = GameObject.Find("SoundManager");
        //fade_in_out = GameObject.Find("EventSystem").GetComponent<Fadeln>();
        CCImageList = Resources.LoadAll<Sprite>("Sprites/CCImage/CImage"); //이미지 경로

        parentscheckTxTNum = PlayerPrefs.GetString("ParentsNo");
        PlayerName = PlayerPrefs.GetString("Nickname");

        DontDestroy = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();


        DontDestroy.LastDay = PlayerPrefs.GetInt("LastQTime");

        if (SceneManager.GetActiveScene().name == "MainField"|| SceneManager.GetActiveScene().name == "AcornVillage")     //메인 필드에 있을 떄만 사용
        {
            DontDestroy.LastDay = PlayerPrefs.GetInt("LastQTime");
            Debug.Log(PlayerPrefs.GetString("QuestPreg"));
            string[] QQ = PlayerPrefs.GetString("QuestPreg").Split('_');
            //string[] qq = PlayerPrefs.GetString("WeeklyQuestPreg").Split('_');

            //string[] q_qid = DontDestroy.QuestIndex.Split('_');

            if (Int32.Parse(QQ[0]) > 3);
            else
                Ride.SetActive(false);
            if (SceneManager.GetActiveScene().name == "MainField")
            {
                Player.position = DontDestroy.gameObject.transform.position;
                if (Int32.Parse(QQ[0]) > 14)
                    AppleTreeObj.SetActive(true);
                else
                    AppleTreeObj.SetActive(false); 

                if (Int32.Parse(QQ[0]) > 12)
                    Kangteagom.SetActive(true);
                else
                    Kangteagom.SetActive(false);
            }
            
            //주말체크
            /*DateTime nowDT = DateTime.Now;
            if (nowDT.DayOfWeek == DayOfWeek.Saturday)
                DontDestroy.SDA = true;*/
            /*
            else if (nowDT.DayOfWeek == DayOfWeek.Sunday)
                DontDestroy.weekend = true;
            else
                DontDestroy.weekend = false;*/
            DontDestroy.ToDay = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));   //퀘스트용 오늘날짜 저장 */ 

            if(DontDestroy.QuestIndex == "13_10")
            {
                Num = "13_10";
                FileAdress = "Scripts/Quest/Dialog/Korean/" + DontDestroy.QuestIndex;
                NewChat();
                DontDestroy.QuestIndex = "13_1";
                Nari.transform.position = Player.transform.position + new Vector3(-1, 0, 0);
                return;
            }
            QuestLoad.QuestLoadStart();

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

        Debug.Log(DontDestroy.Language);
    }

    public void LanguChange()
    {
        Debug.Log(LanguageDropDown.captionText.ToString());
        switch (LanguageDropDown.captionText.ToString())
        {
            case "한국어":
                DontDestroy.Language = "Korean";
                break;
            case "English":
                DontDestroy.Language = "English";
                break;
        }
    }
    public void AnimationActivation()
    {
        PlayerHulaAnimator.enabled = true;
    }
    public void VideoTest()
    {
        video.videoClip.clip = video.VideoClip[11];
        movie.SetActive(true);
        video.OnPlayVideo();
        Main_UI.SetActive(false);
        //SoundManager = GameObject.Find("SoundManager");
        //SoundManager.SetActive(false);
    }

    public void LastVideoCheck()
    {
        if (data_Dialog[j]["scriptType"].ToString().Equals("video"))
        {
            NextVideoWindow.SetActive(true);
        }
        else
        {
            VideoParentsCheckUI.SetActive(true);
        }
    }
    public void QuestTest()
    {
        PlayerPrefs.SetInt("LastQTime", 0);
        DontDestroy.LastDay = 0;
        
        //string QuestType = PlayerPrefs.GetString("QuestPreg");

        /*if (!DontDestroy.weekend)
        {
            QuestType = PlayerPrefs.GetString("QuestPreg");
        }
        else
            QuestType = PlayerPrefs.GetString("WeeklyQuestPreg");*/
        
        //DontDestroy.QuestIndex = QuestType;
        QuestLoad.QuestLoadStart();
    }

    public void QuestMoveTest()
    {
        DontDestroy.ReQuest = true;
        DontDestroy.QuestNF = false;
        PlayerPrefs.SetInt("LastQTime", 0);
        DontDestroy.LastDay = 0;

        //PlayerPrefs.SetString("QuestPreg", DontDestroy.QuestIndex);
        //PlayInfoManager.GetQuestPreg();
        QuestLoad.QuestLoadStart();
    }

    /*public void NotSDA()
    {
            DontDestroy.SDA = false;
    }*/
    public void ToothQuest()  //수정
    {
        ToothAnimator = GameObject.Find("ToothBrush").transform.Find("Armature").gameObject.GetComponent<Animator>();
        Num = "6_2";     //양치게임 스크립트 번호로 수정필요
        FileAdress = "Scripts/Quest/Dialog/Korean/6_2";
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
                        NPCBike = new Vector3(-0.7f, 0, 0);
                        Player.rotation = Quaternion.Euler(0, 90, 0);
                        bikerotate = false;
                    }
                    else
                    {
                        JumpButtons.Playerrb.velocity = JumpButtons.Playerrb.velocity.normalized * 0;
                        NPCBike = new Vector3(0.7f, 0, 0);
                        Player.rotation = Quaternion.Euler(0, -90, 0);
                        bikerotate = true;
                    }
                    timer = 0;
                }
                JumpButtons.Playerrb.velocity = JumpButtons.Playerrb.velocity.normalized * QBikeSpeed;
                if (Maxtime == 3)
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
            JumpButtons.Playerrb.AddRelativeForce(Vector3.forward * 1000f); //앞 방향으로 밀기 (방향 * 힘)

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
        Debug.Log(FileAdress);
        data_Dialog = CSVReader.Read(FileAdress);
        if (DontDestroy.tutorialLoading)
            j = 14;
        else
            j = 0;
        chatCanvus.SetActive(true);
        Line();
        //Debug.Log("퀴즈4");
        Main_UI.SetActive(false);
    }
    public void NPCNewChat()
    {
        Main_UI.SetActive(false);
        Input.multiTouchEnabled = false;
        //PlayerCamera.SetActive(true);
        Debug.Log(FileAdress);
        data_Dialog = CSVReader.Read(FileAdress);

        for (int k = 0; k <= data_Dialog.Count; k++)
        {
            if (data_Dialog[k]["scriptNumber"].ToString().Equals(Num))
            {
                j = k;
                chatCanvus.SetActive(true);
                break;
            }
            else
            {
                continue;
            }
        }

        chatCanvus.SetActive(true);
        Line();
        Main_UI.SetActive(false);
    }

    public void AchangeMoment()  //플레이어 이동, 카메라 무브
    {
        switch (o)
        {
            case 1: //비빔밥
                Player.transform.position = new Vector3(32.6f, -5.4f, -0.1f);
                Nari.transform.position = Player.transform.position + new Vector3(1, 0, 0);
                Player.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                Nari.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                NariMom.transform.position = Player.transform.position + new Vector3(2, 0, 0);
                NariMom.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                break;
            case 2:  //장작
                Player.transform.position = new Vector3(-18.4f, 1.8f, 20);
                Nari.transform.position = Player.transform.position + new Vector3(1, 0, 0);
                NariMom.transform.position = Player.transform.position + new Vector3(2, 0, 0);
                break;
            case 3: //열매따기
                Player.transform.position = new Vector3(1.5f, -3.4f, -11.7f);
                Nari.transform.position = Player.transform.position + new Vector3(1, 0, 0);
                NariMom.transform.position = Player.transform.position + new Vector3(2, 0, 0);
                break;
            default:
                break;
        }
    }
    public void changeMoment()  //플레이어 이동, 카메라 무브
    {
        switch (o)
        {
            case 1: //언덕 위
                Player.transform.position = new Vector3(-36.2f, -4.95455313f, -9.4f);
                break;
            case 2:  //집앞
                cuttoon.SetActive(false);
                Player.transform.position = new Vector3(1.6f, -6.41821432f, 1.4f);
                break;
            case 3: //집에서 나옴
                if (DontDestroy.tutorialLoading)
                    Nari.transform.position = Player.transform.position + new Vector3(1, 0, 0);
                DontDestroy.tutorialLoading = false;
                break;
            case 4: //이장님
                Player.transform.position = new Vector3(12.9f, -4.33371019f, 26.4f);
                Nari.transform.position = Player.transform.position + new Vector3(1, 0, 0);
                break;
            case 5: //무무
                Player.transform.position = new Vector3(-16.6f, -6.41383314f, 8.8f);
                Nari.transform.position = Player.transform.position + new Vector3(1, 0, 0);
                break;
            case 6: //여미
                Player.transform.position = new Vector3(49.5f, -6.41383314f, 14.2f);
                Nari.transform.position = Player.transform.position + new Vector3(1, 0, 0);
                break;
            case 7: //요미
                Player.transform.position = new Vector3(43.3f, -6.41383266f, 14.3f);
                Nari.transform.position = Player.transform.position + new Vector3(1, 0, 0);
                break;
            case 8: //놀이터
                Player.transform.position = new Vector3(5.7f, -6.39158726f, -10.2f);
                Nari.transform.position = Player.transform.position + new Vector3(1, 0, 0);
                break;
            case 9:  //연못
                Player.transform.position = new Vector3(-16.8f, -6.41383314f, -9.1f);
                Nari.transform.position = Player.transform.position + new Vector3(1, 0, 0);
                break;
            case 10: //슈퍼맨
                Player.transform.position = new Vector3(58.1f, -6.41383362f, -2.5f);
                Nari.transform.position = Player.transform.position + new Vector3(1, 0, 0);
                break;
            case 11: //집
                Player.transform.position = new Vector3(1.6f, -6.41821432f, 1.4f);
                Nari.transform.position = Player.transform.position + new Vector3(1, 0, 0);
                break;
            case 12:  //튜토리얼 종료
                PlayerPrefs.SetString("QuestPreg", DontDestroy.QuestIndex);
                PlayInfoManager.GetQuestPreg();
                //Save_Log.instance.SaveQEndLog();    //퀘스트 종료 로그 기록
                SceneLoader.instance.GotoMainField();
                break;
            case 13:
                {
                    Player.transform.position = new Vector3(-36.2f, -4.95455313f, -9.4f);
                    Player.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    GameObject Parents = GameObject.Find("parents(Clone)");
                    Parents.transform.position = Player.transform.position + new Vector3(-1.5f, 0, 0);
                    Parents.transform.rotation = Quaternion.Euler(new Vector3(0, 150, 0));
                    break;
                }
            default:
                break;
        }
    }
    GameObject mouth; //양치겜 입
    public void QuestSubChoice(string ST)
    {
        //Debug.Log("타입" + data_Dialog[j]["scriptType"].ToString());
        //string ST = data_Dialog[j]["scriptType"].ToString();
        switch (ST)
        {
            case "quiz":  //퀴즈시작
                MataNum = Int32.Parse(data_Dialog[j]["QuizNumber"].ToString());
                scriptLine();
                for (int i = 0; i < QuizButton.Length; i++)
                {
                    if (data_Dialog[j]["select" + (i + 1)].ToString().Equals("빈칸"))
                        QuizButton[i].transform.parent.gameObject.SetActive(false);
                    else
                        QuizButton[i].transform.parent.gameObject.SetActive(true);
                    QuizButton[i].text = data_Dialog[j]["select" + (i + 1)].ToString();
                }
                QuizMate();
                break;
            case "choice":
                block.SetActive(false);
                j--;
                QuizCho();
                break;
            case "Move":    //Main Move
                if (data_Dialog[j]["scriptNumber"].ToString().Equals("19_1"))  //수정한 원 주말퀘스트 번호
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
                break;
            case "AMove":   //at dotori Move
                o += 1;
                AchangeMoment();

                j += 1;
                Line();
                break;
            case "ReloadEnd":   //main씬 리로드
                QuestEnd();
                StartCoroutine(reload());
                break;
            case "over":
                if (DontDestroy.ReQuest)
                    ;
                else
                    PlayerPrefs.SetString("QuestPreg", DontDestroy.QuestIndex);
                StartCoroutine(reload());
                break;
            case "cuttoonE":    //컷툰으로 끝남
                Cuttoon();
                ChatWin.SetActive(false);
                Invoke("ChatEnd", 2f);
                Invoke("QuestEnd", 2f);
                break;
            case "cuttoon":     //컷툰 보이기
                Cuttoon();
                ChatWin.SetActive(false);
                Invoke("scriptLine", 2f);   //딜레이 후 스크립트 띄움
                break;
            case "Dcuttoon":     //컷툰 띄운 채로 다음 스크립트 생성
                scriptLine();
                break;
            case "tutorial":
                scriptLine();
                tuto = true;
                break;
            case "Panorama":    //컷툰 여러개 띄우기
                c = 0;
                ChatWin.SetActive(false);
                InvokeRepeating("Panorama", 0f, 2f);
                break;
            case "video": //비디오 실행
                {
                    video.videoClip.clip = video.VideoClip[Int32.Parse(data_Dialog[j]["cuttoon"].ToString())];
                    if (movie.activeSelf)
                    {
                        Debug.Log("비디오창 on");
                        //video.videoClip.Stop();
                        NextVideoWindow.SetActive(false);
                        VideoParentsCheckUI.SetActive(false);
                        videocheckTxT.text = null;
                    }
                    else
                    {
                        Debug.Log("비디오창 off");
                        movie.SetActive(true);
                        SoundManager = GameObject.Find("SoundManager");
                        SoundManager.SetActive(false);
                    }
                    video.OnPlayVideo();
                    ChatWin.SetActive(false);
                    j++;
                    break;
                }
            case "videoEnd": //비디오 실행 중지
                if (videocheckTxT.text.Equals(parentscheckTxTNum))
                {
                    GameObject.Find("checkUI").SetActive(false);
                    videocheckTxT.text = null;
                    movie.SetActive(false);
                    ChatWin.SetActive(true);
                    SoundManager.SetActive(true);
                    scriptLine();
                    video.OnFinishVideo();
                    video.VideoPlayUI.SetActive(false) ;
                    cuttontext.text = " ";
                }
                else
                {
                    ErrorWin.SetActive(true);
                }
                break;
            case "Bike": //자전거 UI이벤트
                cuttoon.SetActive(false);
                ChatWin.SetActive(false);
                Bike.SetActive(true);
                j++;
                break;
            case "Bicycle":  //자전거 타기 이벤트
                {
                    if (!BikeQ)
                    {
                        NPCBike = new Vector3(-1, 0, 0);
                        bicycleRide.RideOn();
                        Destroy(GameObject.Find("Qbicycle(Clone)"));
                        Player.position = new Vector3(0, -6.39158726f, -10.6f);
                        Player.rotation = Quaternion.Euler(0, 90, 0);
                        QBikeSpeed = 1;
                        Maxtime = 3;
                        BikeQ = true;
                        StartCoroutine("QBikeLoop");
                        NPCJumpAnimator.SetBool("BikeWalk", true);

                        scriptLine();
                    }
                    else if (QBikeSpeed == 3)
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

                        scriptLine();
                    }
                    else if (BikeQ)
                    {
                        //페이드 인 페이드 아웃하면서 화면에 한시간 후... 띄우기
                        cuttontext.text = "한시간 후...";
                        IEnumerator BikeFade()
                        {
                            StartCoroutine(fadein());
                            yield return new WaitForSecondsRealtime(1.5f);
                            QBikeSpeed = 3;
                            Maxtime = 2f;
                            Player.position = new Vector3(0, -6.39158726f, -10.6f);
                            BikeNPC.transform.position = Player.position + new Vector3(3, 0, 3);
                            Player.rotation = Player.rotation = Quaternion.Euler(0, 90, 0);
                            bikerotate = false;
                            timer = 0;
                            NPCJumpAnimator.SetBool("BikeWalk", false);
                        }

                        IEnumerator BF = BikeFade();
                        StartCoroutine(BF);
                    }
                    break;
                }
            case "hair":    //머리반짝 이벤트
                ChatWin.SetActive(false);
                hairFX(Player.gameObject);
                j++;
                Invoke("clearHair", 1f);
                break;
            case "note":    //포스터잇 쓰기 이벤트
                if (data_Dialog[j]["cuttoon"].ToString().Equals("0"))
                {
                    j++;
                    ChatWin.SetActive(false);
                    Note.SetActive(true);
                    Draw.StartNote();
                }
                else
                {
                    j++;
                    ChatWin.SetActive(false);
                }
                break;
            case "noteFinish":
                Note.transform.Find("Button").gameObject.SetActive(false);
                Draw.FinishWrite();
                scriptLine();
                break;
            case "noteEnd":
                Note.SetActive(false);
                scriptLine();
                break;
            case "nutrient":    //건강주스
                cuttoon.SetActive(false);
                j++;
                ChatWin.SetActive(false);
                nutrient.SetActive(true);
                break;
            case "nutrientEnd":
                j++;
                nutrient.SetActive(false);
                Cuttoon();
                ChatWin.SetActive(false);
                Invoke("scriptLine", 2f);   //딜레이 후 스크립트 띄움
                break;
            case "train":   //가치관 카드
                ChatWin.SetActive(false);
                Value.SetActive(true);
                j++;
                Draw.StartCard();
                break;
            case "trainEnd":
                Value.SetActive(false);
                scriptLine();
                break;
            case "draw":     //그림판
                ChatWin.SetActive(false);
                Draw.ChangeDrawCamera();
                j++;
                break;
            case "drawFinish":
                GameObject.Find("DrawUI").transform.Find("Button").gameObject.SetActive(false);
                Draw.Draw = false;
                scriptLine();
                break;  
            case "Screenshot":      //스크린샷 카메라 setactive(true)
                screenShot.SetActive(true);
                screenShotExit.SetActive(true);
                ChatWin.SetActive(false);
                j++;
                break;
            case "drawEnd":
                screenShot.SetActive(false);
                Draw.ChangeDrawCamera();
                Main_UI.SetActive(false);
                scriptLine();
                break;
            case "listen":  //자연소리 실행
                ChatWin.SetActive(false);
                c = 0;
                InvokeRepeating("QSound", 0f, 4f);
                break;
            case "song":    //웃어봐 송 틀기
                {
                    StartCoroutine(song());
                    break;
                }
            case "songend":
                {//원래 노래로 바꾸기
                    SoundManager SoundManager_ = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                    SoundManager_.Sound("BGMField");
                    scriptLine();
                    break;
                }
            case "JumpRope":        //줄넘기
                int RopeNum = int.Parse(data_Dialog[j]["cuttoon"].ToString());
                switch(RopeNum)
                {
                    case 0:
                        GameObject.Find("Himchan").transform.rotation = Quaternion.Euler(0, 180, 0);
                        PlayerRope.SetActive(true);
                        NPCJumpAnimator.SetBool("JumpRope", true);
                        NPCJumpAnimatorRope.SetBool("JumpRope", true);
                        break; 
                    case 1:
                        Player.rotation = Quaternion.Euler(0, 180, 0);
                        JumpAnimator.SetBool("JumpRope", true);
                        JumpAnimatorRope.SetBool("JumpRope", true);
                        JumpAnimator.speed = 0.3f;
                        JumpAnimatorRope.speed = 0.3f;
                        break; 
                    case 2:
                        JumpAnimator.speed = 1f;
                        JumpAnimatorRope.speed = 1f;
                        break; 
                    case 3:
                        NPCJumpAnimator.SetBool("JumpRopeSide", true);
                        NPCJumpAnimatorRope.SetBool("JumpRopeSide", true);
                        break;
                    case 4:
                        NPCRope.transform.rotation = Quaternion.Euler(new Vector3(-180, 180, -180));
                        NPCJumpAnimator.SetBool("JumpRope", true);
                        NPCJumpAnimatorRope.SetBool("JumpRope", true);
                        break;
                    case 5:
                        NPCRope.transform.rotation = Quaternion.Euler(-180, 0, -180);
                        NPCJumpAnimator.SetBool("JumpRope", true);
                        NPCJumpAnimatorRope.SetBool("JumpRope", true);
                        NPCJumpAnimatorRope.speed = 2f;
                        break; 
                    case 6:
                        PlayerRope.SetActive(false);
                        NPCRope.SetActive(false);
                        break; 
                    case 7:
                        {
                            PlayerRope.SetActive(true);

                            GameObject NPC_ = GameObject.Find(Inter.NameNPC);


                            Player.position = new Vector3(3.5f, -6.39158678f, -9.3f);
                            NPC_.transform.position = Player.transform.position + new Vector3(1.5f, 0, 0);
                            NPCRope.SetActive(true);

                            Vector3 targetPositionNPC;
                            Vector3 targetPositionPlayer;
                            targetPositionNPC = new Vector3(Player.transform.position.x, NPC_.transform.position.y, Player.transform.position.z);
                            NPC_.transform.LookAt(targetPositionNPC);
                            targetPositionPlayer = new Vector3(NPC_.transform.position.x, Player.transform.position.y, NPC_.transform.position.z);
                            Player.transform.LookAt(targetPositionPlayer);
                            break;
                        }
                }    
                Invoke("scriptLine", 2f);
                break;
            case "JumpRopeEnd":
                //줄넘기
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
                break;
            case "MasterOfMtLife":
                //나에게 편지쓰기
                MasterOfMtLife.SetActive(true);
                ChatWin.SetActive(false);
                j++;
                break;
            case "MasterOfMtLifeEnd":
                MasterOfMtLife.SetActive(false);
                ChatWin.SetActive(true);
                scriptLine();
                break;
            case "LoveNature":
                LoveNature.SetActive(true);
                ChatWin.SetActive(false);
                j++;
                break;
            case "LoveNatureEnd":
                LoveNature.SetActive(false);
                scriptLine();
                break;
            case "AppleTree":
                AppleTree.SetActive(true);
                ChatWin.SetActive(false);
                j++;
                break;
            case "AppleTreeEnd":
                ChatWin.SetActive(true);
                scriptLine();
                break;
            /*case "MakeAppleTree":
                AppleTree.SetActive(false);
                AppleTreeObj.SetActive(true);
                scriptLine();
                break;*/
            case "KeyToDream":
                KeyToDream.SetActive(true);
                ChatWin.SetActive(false);
                j++;
                break;
            case "KeyToDreamEnd":
                KeyToDreamInput.text = null;
                KeyToDream.SetActive(false);
                scriptLine();
                break;
            case "Tooth":
                if (Int32.Parse(data_Dialog[j]["cuttoon"].ToString()) == 0)
                {
                    DontDestroy.QuestIndex = "6_10";
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
                        DontDestroy.QuestIndex = "6_1";
                        if (DontDestroy.ReQuest)
                            ;
                        else
                            PlayerPrefs.SetString("QuestPreg", DontDestroy.QuestIndex);
                        SceneLoader.instance.GotoMainField();
                        return;
                    }

                    Invoke("scriptLine", 3f);   //딜레이 후 스크립트 띄움
                }
                break;
            case "LookAt":
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
                    else if (data_Dialog[j]["scriptNumber"].ToString().Equals("22_1"))
                    {
                        Transform NPC2 = GameObject.Find("Nari").transform;
                        StartCoroutine(JumpButtons.NPCturn(NPC2, targetPositionNPC));
                    }
                    Invoke("stopCorou", 1f);
                    break;
                }
            case "moveTos":
                cuttontext.text = "다음날 아침";
                IEnumerator NextMorning()
                {
                    StartCoroutine(fadein());
                    yield return new WaitForSecondsRealtime(1.5f);
                    Player.transform.position = new Vector3(-25.5f, -6.41383362f, -20);
                    Player.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

                    Nari.transform.position = Player.transform.position + new Vector3(1, 0, 0);
                    Nari.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

                    Kangteagom.SetActive(true);
                    //scriptLine();
                }
                IEnumerator NM = NextMorning();
                StartCoroutine(NM);
                break;
            case "BMI":
                BMI.SetActive(true);
                ChatWin.SetActive(false);
                j++;
                break;
            case "BMITalk":
                Inter.NpcNameTF = true;
                BMI.SetActive(false);
                BMItalk.SetActive(true);
                ChatWin.SetActive(true);
                j++;
                scriptLine();
                break;
            case "BMITalkT":
                j++;
                Draw.BMItalk();
                break;
            case "BMIEnd":
                Inter.NpcNameTF = false;

                chatName.text = PlayerName;
                LoadTxt = "그럼 나는 " + Draw.BMIresult + "이네. 그런데, 과체중이랑 비만이 무슨 뜻이야?";
                CCImage.SetActive(false);
                j++;
                StartCoroutine(_typing());
                break;
            case "moveToA":     //도토리 마을로 이동
                Cuttoon();
                DontDestroy.QuestIndex = "13_10";
                SceneLoader.instance.GotoMainAcronVillage();
                break;
            case "Nanum":
                cuttoon.SetActive(false);
                Nanum.SetActive(true);
                ChatWin.SetActive(false);
                j++;
                break;
            case "NanumEnd":
                Nanum.SetActive(false);
                scriptLine();
                break;
            case "Jewel":  //그와중에 오타..ㅋㅋㅋㅋ Juwel
                ChatWin.SetActive(false);
                Jewel.SetActive(true);
                j++;
                Draw.StartJuwel();
                break;
            case "Jewelselect":
                ChatWin.SetActive(false);
                Jewel.SetActive(true);
                j++;
                break;
            case "JewelFinish":
                Draw.JFinishWrite();
                scriptLine();
                break;
            case "JewelSelecFinish":
                Draw.JNextLevel();
                break;
            case "JewelEnd":
                Jewel.SetActive(false);
                scriptLine();
                break;
            case "Healthy":
                Healthy.SetActive(true);
                ChatWin.SetActive(false);
                j++;
                break;
            case "HealthyEnd":
                Healthy.SetActive(false);
                scriptLine();
                break;
            case "Hulahoop":
                //훌라후프
                /*public Animator PlayerHulaAnimator;  //플레이어 accessories에 들어있는 애니메이터
                public Animator NPCHulaAnimator;   //힘찬이 모델링 최상위오브젝트에 든 애니메이터
                public Animator PHulaAnimator; //플레이어 훌라후프 애니메이터
                public Animator NHulaAnimator;  //NPC훌라후프 애니메이터
                public GameObject PlayerHula; //플레이어 훌라후프 모델링
                public GameObject NPCHula;  //NPC훌라후프 모델링*/
                if (data_Dialog[j]["cuttoon"].ToString().Equals("0")) //플레이어한테 훌라후프 만들기 기존 애니메이터 끄기
                {
                    GameObject.Find("Himchan").transform.rotation = Quaternion.Euler(0, 180, 0);
                    Player.position = new Vector3(3.5f, -6.39158678f, -9.3f);
                    Player.rotation = Quaternion.Euler(0, 180, 0);

                    PlayerHula.SetActive(true);  //P H active
                }
                else if (data_Dialog[j]["cuttoon"].ToString().Equals("1")) //힘찬 훌라후프 애니매니터 켜기 힘찬이 훌라후프 들기
                {
                    JumpAnimator.enabled = false; //P A off
                    NPCJumpAnimator.enabled = false;  //N A off
                    NPCHulaAnimator.enabled = true;
                    NHulaAnimator.SetBool("Hoopup", true);
                    NPCHulaAnimator.SetBool("Hoopup", true);  //들기
                }
                else if (data_Dialog[j]["cuttoon"].ToString().Equals("2")) //플레이어 훌라후프 애니메이터 켜기 플레이어 훌라후프 들기
                {
                    PlayerHulaAnimator.enabled = true;
                    PHulaAnimator.SetBool("Hoopup", true);
                    PlayerHulaAnimator.SetBool("Hoopup", true);  //들기
                }
                else if (data_Dialog[j]["cuttoon"].ToString().Equals("3")) //힘찬이 훌라후프 돌리기, 플레이어 돌리다 떨구기 1
                {
                    NHulaAnimator.SetBool("HoopStart", true);
                    NPCHulaAnimator.SetBool("HoopStart", true); //돌리기 시작
                    NHulaAnimator.SetBool("HoopRotation", true);
                    NPCHulaAnimator.SetBool("HoopRotation", true); //돌리기

                    PHulaAnimator.SetBool("HoopStart", true);
                    PlayerHulaAnimator.SetBool("HoopStart", true); //돌리기 시작
                    PHulaAnimator.SetBool("HoopRotation", true);
                    PlayerHulaAnimator.SetBool("HoopRotation", true); //돌리기
                    PHulaAnimator.SetBool("HoopFail", true);
                    PlayerHulaAnimator.SetBool("HoopFail", true); //떨구기
                }
                else if (data_Dialog[j]["cuttoon"].ToString().Equals("4")) //계속 돌리기
                {
                    PHulaAnimator.SetBool("HoopFail", false);
                    PlayerHulaAnimator.SetBool("HoopFail", false); //떨구기 취소
                }
                else if (data_Dialog[j]["cuttoon"].ToString().Equals("5")) //멈추고 내려두기 그리고 대기모션
                {
                    NHulaAnimator.SetBool("HoopEnd", true);
                    NPCHulaAnimator.SetBool("HoopEnd", true); //멈추기
                    NHulaAnimator.SetBool("HoopDown", true);
                    NPCHulaAnimator.SetBool("HoopDown", true); //내리기

                    PHulaAnimator.SetBool("HoopEnd", true);
                    PlayerHulaAnimator.SetBool("HoopEnd", true); //멈추기
                    PHulaAnimator.SetBool("HoopDown", true);
                    PlayerHulaAnimator.SetBool("HoopDown", true); //내리기


                }
                else if (data_Dialog[j]["cuttoon"].ToString().Equals("6")) //훌라후프들 없애기
                {
                    NPCHulaAnimator.enabled = false;
                    PlayerHulaAnimator.enabled = false;
                    JumpAnimator.enabled = true; //P A off
                    NPCJumpAnimator.enabled = true;  //N A off
                    PlayerHula.SetActive(false);  //P H active
                    NPCHula.SetActive(false);  //P H active
                }
                Invoke("scriptLine", 2f);
                break;
            case "spell":
                spell.SetActive(true);
                Draw.startWrite();
                ChatWin.SetActive(false);
                j++;
                break;
            case "spellWriteEnd":
                Draw.SFinishWrite();
                j++;
                break;
            case "spellEnd":
                spell.SetActive(false);
                scriptLine();
                break;
        }
    }
    

    IEnumerator fadein()
    {
        ChatWin.SetActive(false);
        cuttontext.gameObject.SetActive(true);
        cuttoon.SetActive(true);
        cuttoonspriteR = cuttoon.GetComponent<Image>();
        cuttoonspriteR.sprite = null;
        cuttoonspriteR.color = Color.black;
        bool fadein = true;

        Color color = cuttoonspriteR.color;
        color.a = 0.0f;
        while (fadein)
        {
            Debug.Log("페이드 인");
            cuttoonspriteR.color = color;
            color.a += 0.1f;
            yield return new WaitForSecondsRealtime(0.05f);
            if(color.a >= 1.1f)
            {
                fadein =false;
            }
        }
        yield return new WaitForSecondsRealtime(1.5f);
        StartCoroutine(fadeout(fadein, color));
        
    }
    IEnumerator fadeout(bool fadein, Color color)
    {
        if(fadein)
            fadein = false;
        //color.a = 1f;
        while (!fadein)
        {
            Debug.Log("페이드 아웃");
            cuttoonspriteR.color = color;
            color.a -= 0.1f;
            yield return new WaitForSecondsRealtime(0.05f);
            if (color.a <= -0.1f)
            {
                //yield break;
                fadein = true;
            }
        }
        color.a = 1;
        cuttoonspriteR.color = color;
        cuttontext.gameObject.SetActive(false);
        cuttoon.SetActive(false);
        cuttoonspriteR.color = Color.white;
        //j++;
        scriptLine();
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
    public void Line()  //줄넘김 (scriptType이 뭔지 걸러냄)
    {
        String SType = data_Dialog[j]["scriptType"].ToString();
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
        if (SType.Equals("end")) //대화 끝
        {
            if (data_Dialog[j]["name"].ToString().Equals("end"))
            {
                QuestEnd();
            }
            ChatEnd();
            
        }
        else
        {
            if (!SType.Equals("nomal"))
            {
                //Debug.Log(data_Dialog[j]["scriptType"].ToString());
                QuestSubChoice(SType); 
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
        /*if (data_Dialog[j]["SoundEffect"].ToString().Equals("Null"))
        {; }
        else
        {
            string SoundName = data_Dialog[j]["SoundEffect"].ToString();
            SoundEffectManager.GetComponent<SoundEffect>().Sound(SoundName);
        }*/
        //if (move)
        //    changeMoment();
    }

    
    public void scriptLine()  //스크립트 띄우는 거 (어굴 이미지+ 이름+ 뜨는 텍스트)
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

        LoadTxt = data_Dialog[j]["dialog"].ToString();
        LoadTxt = LoadTxt.Replace("<n>", "\n").Replace("P_name", PlayerName);

        chatName.text = data_Dialog[j]["name"].ToString().Replace("주인공", PlayerName);
        StartCoroutine(_typing());
        Arrow.SetActive(false);
        j++;
    }

    public void Cuttoon()
    {
        c= Int32.Parse(data_Dialog[j]["cuttoon"].ToString());
        cuttoon.SetActive(true);
        //Debug.Log("컷툰갯수=" + cuttoonImageList.Length);
        cuttoonspriteR = cuttoon.GetComponent<Image>();
        cuttoonspriteR.sprite = cuttoonImageList[c];
    }

    public void ChatEnd() //리셋
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
        if (Inter.NameNPC.Equals("WallMirror") || Inter.NameNPC.Equals("GachaMachine")|| Inter.NameNPC.Equals("ThankApplesTree")|| Inter.NameNPC.Equals("Bibim")|| Inter.NameNPC.Equals("Wood")|| Inter.NameNPC.Equals("Fruit"))  //물체들 대화 수정
        { stopCorou(); }
        else if (DontDestroy.QuestIndex.Equals("9_1") && Inter.NameNPC.Equals("Mei"))
        { stopCorou(); }
        else if (DontDestroy.QuestIndex.Equals("15_1") && Inter.NameNPC.Equals("Suho"))
        { stopCorou(); }
        else
        {
            Vector3 targetPositionNPC;
            targetPositionNPC = new Vector3(JumpButtons.Ntransform.transform.position.x, JumpButtons.NPC.position.y, JumpButtons.Ntransform.transform.position.z - 1);

            StartCoroutine(JumpButtons.NPCturn(JumpButtons.NPC, targetPositionNPC));
            Invoke("stopCorout",1f);
        }
    }


    public void Buttons()      //npc대화 상호작용 선택지 수
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
    public void ButtonsFalse()      //npc대화 상호작용 선택지 수
    {
        NPCButtons.SetActive(false);
        for (int i= 0; i < SelecButton.Length; i++)
        {
            string selecNumber = "select"+(i+1).ToString();
            SelecButton[i].SetActive(false);
        }
    }

    public void QuizMate() //전광판 메테리얼 설정
    {
        Quiz_material[1] = material[MataNum]; //0에 메테리얼 번호
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
    public IEnumerator _typing()  //타이핑 효과
    {
        string SoundJ = data_Dialog[j]["SoundEffect"].ToString();
        block.SetActive(true);
        typingSkip = true;
        if (!ChatWin.activeSelf)
            ChatWin.SetActive(true);

        chatTxt.text = " ";
        yield return new WaitForSecondsRealtime(0.1f);
        ChVoice(SoundJ);
        for (int i = 0; i < LoadTxt.Length + 1; i++)
        {
            if (typingSkip)
            {
                chatTxt.text = LoadTxt.Substring(0, i);
                yield return new WaitForSecondsRealtime(0.03f);
            }
            else
            {
                break;
            }
        }
        chatTxt.text = LoadTxt;
        //ChVoice(SoundJ);
        yield return new WaitForSecondsRealtime(0.2f);
        Arrow.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);

        if (tuto)
        {
            if(data_Dialog[j - 1]["scriptType"].ToString().Equals("tutorial"))
            Debug.Log("튜토리얼 실행ㅇ");
            Invoke("Tutorial_", 1f);
        }
        else
        {
            block.SetActive(false);
        }

    }

    void ChVoice(String SoundJ)
    {
        //음성
        if (SoundJ.Equals("Null"))
        {; }
        else
        {
            SoundEffectManager.GetComponent<SoundEffect>().Sound(SoundJ);
        }
    }

    void QuizCho()
    {
        Chat.SetActive(false);
        Button.SetActive(true); //유니티에서 버튼 위치 옮김
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

    public void O()//정답 골랐을 때
    {
        block.SetActive(true);
        OImage.SetActive(true); 
        Button.SetActive(false);
        j++;

    }
    public void X()//틀린 정답 골랐을 때
    {
        int x = UnityEngine.Random.Range(0,3);
        block.SetActive(true);
        XImage.SetActive(true);
        Button.SetActive(false);
        LoadTxt = Xdialog[x];
        StartCoroutine(_typing());

    }

    public void Xback()//X이미지 버튼
    {
        XImage.SetActive(false);
        Button.SetActive(true);
    }

    private IEnumerator reload()
    {
        yield return new WaitForEndOfFrame();
        if (SceneManager.GetActiveScene().name == "AcornVillage")
            SceneLoader.instance.GotoMainAcronVillage();
        else
            SceneLoader.instance.GotoMainField();
    }
    public void QuestEnd()
    {
        if (SceneManager.GetActiveScene().name == "Quiz") ;
        else
            Save_Log.instance.SaveQEndLog();    //퀘스트 종료 로그 기록

        DontDestroy.ButtonPlusNpc = "";

        if (DontDestroy.ReQuest)
            ;
        else
        {
            PlayerPrefs.SetString("QuestPreg", DontDestroy.QuestIndex);
        }
        if (data_Dialog[j]["dialog"].ToString().Equals("end"))
        {
            PlayerPrefs.SetInt("LastQTime", DontDestroy.ToDay);
            if (SceneManager.GetActiveScene().name == "Quiz") ;
            else
                NpcButton.Chat.EPin.SetActive(false);
            if (DontDestroy.ReQuest)
                DontDestroy.QuestNF = false;
            DontDestroy.LastDay = DontDestroy.ToDay;
        }
        else
        {
            if (DontDestroy.ReQuest)
                DontDestroy.QuestNF = true;
            if (SceneManager.GetActiveScene().name == "MainField"|| SceneManager.GetActiveScene().name == "AcornVillage")
                QuestLoad.QuestLoadStart();
        }
        PlayInfoManager.GetQuestPreg();
        Input.multiTouchEnabled = true;
    }

    public void ParentsCheck()
    {
        if (ParentscheckTxt.text.Equals(parentscheckTxTNum))
        {
            if (DontDestroy.ReQuest)
                ;
            else
            { 
                PlayerPrefs.SetString("QuestPreg", DontDestroy.QuestIndex);
                //QS.QuestStepNumber++;
            }
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
    public void hairFX(GameObject go)    //머리 반짝!하는 파티클
    {
        Debug.Log("반짝");
        ParticleSystem newfx = Instantiate(hairPs);
        newfx.transform.position = go.transform.position+new Vector3(0,1,0);
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

    IEnumerator song()
    {
        Debug.Log("song 실행");
        SoundManager SoundManager_ = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        SoundManager_.Sound("HaHasong");
        cuttoonspriteR = cuttoon.GetComponent<Image>();
        cuttoonspriteR.sprite = cuttoonImageList[0];
        cuttoon.SetActive(true);
        ChatWin.SetActive(false);
        yield return new WaitForSeconds(20);
        cuttoonspriteR.sprite = cuttoonImageList[1];
        yield return new WaitForSeconds(15);
        scriptLine();   //딜레이 후 스크립트 띄움
    }
}
