using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using BackEnd;
public class UIButton : MonoBehaviour
{
    public static bool OnLand = true;    //Player가 바닥에 있는지 확인
    public GameObject Player;             //Player선언
    public GameObject Map;                //Map선언                
    public GameObject GaguShop;           //가구점             
    public GameObject Market;           //슈퍼         
    public GameObject clothesShop;        
    public GameObject HairShop;       
    public Rigidbody Playerrb;            //Player의 Rigidbody선언
    public GameObject JoyStick;
    public GameObject Main_UI;
    public GameObject FarmUI;
    public GameObject chatBlock;
    public GameObject LadderPanel;

    public Camera Camera1;
    public Camera Camera2;

    //public GameObject ShopMok;             // 목공방
    bool map;                              //지도가 열려있는지 확인

    public FlieChoice Chat;
    public LodingTxt chat;
    public Interaction Inter;

    public GameObject SoundEffectManager;

    public static bool is_pop_garden = false;

    public int time=0;
    public bool isClick;
    public static float minClickTime = 1;
    public static float ClickTime;

    [SerializeField]
    private Animator PA;

    void Start()
    {
        map = false;
    }

    string NPCName=null;
    public Transform NPC;

    public void JumpButton()                //점프버튼
    {
        Debug.Log("점프");
        GameObject SoundManager = GameObject.Find("SoundManager");
        if (OnLand && Inter.NearNPC)     //NPC주변에 있다면
        {
            NPCName = Inter.NameNPC;
            Inter.NpcNameTF = false;
            if (chat.bicycleRide.Ride)
                chat.bicycleRide.RideOn();

            Vector3 targetPositionNPC;
            NPC = GameObject.Find(Inter.NameNPC).transform;
            targetPositionNPC = new Vector3(Player.transform.position.x, NPC.position.y, Player.transform.position.z);
            if (Inter.NameNPC.Equals("WallMirror") || Inter.NameNPC.Equals("GachaMachine") || Inter.NameNPC.Equals("InfoSign"))
            { stopCorou(); }
            else if (chat.DontDestroy.QuestIndex.Equals("9_1") && Inter.NameNPC.Equals("Mei"))  //퀘스트 수정
            { stopCorou(); }
            else if (chat.DontDestroy.QuestIndex.Equals("15_1") && Inter.NameNPC.Equals("Suho"))
            { stopCorou(); }
            else if (Inter.NameNPC.Equals("ThankApplesTree"))
            {
                TimeCheck();
            }
            else if (Inter.text.text.Equals("미니게임"))
            {
                ;
            }
            else
            {
                StartCoroutine(NPCturn(NPC, targetPositionNPC));
                //NPC.transform.LookAt(targetPositionNPC);
            }

            chatBlock.SetActive(true);
            StartCoroutine(Playerturn(NPC));
            //Player.transform.LookAt(targetPositionPlayer);
            Invoke("ChatStart", 1f);
        }
        else if (Inter.Door)
        {
            if (Inter.NameNPC.Equals("InDoor"))
            {
                SoundEffectManager.GetComponent<SoundEffect>().Sound("OpenDoor");
                SceneLoader.instance.GotoHouse();
            }
            else if (Inter.NameNPC.Equals("ExitDoor"))
                SceneLoader.instance.GotoMainField();
            Inter.Door = false;
        }
        else if (Inter.Farm)
        {
            if (Camera1.enabled == true)
            {
                Camera1.enabled = false;
                Camera2.enabled = true;
                JoyStick.SetActive(false);
                FarmUI.SetActive(true);

                GardenCategory.instance.PopGarden();
            }
            else
            {
                Camera2.enabled = false;
                Camera1.enabled = true;
                JoyStick.SetActive(true);
                FarmUI.SetActive(false);
            }
        }
        else                                                //NPC주변에 있지 않다면
        {
            if (OnLand)// && (SceneManager.GetActiveScene().name == "MainField"))                                         //Player가 바닥에 있다면
            {
                SoundEffectManager.GetComponent<SoundEffect>().Sound("Jump");
                Playerrb.AddForce(transform.up * 10000);
            }

        }
    }
    public void OnPointerDown()//PointerEventData eventData)
    {
        Debug.Log("Down");
        if (Inter.Ladder)
        {
            isClick = true;
            StartCoroutine(Ladderup());
        }
        else
            JumpButton();
    }
    public void OnPointerUp()//PointerEventData eventData)
    {
        Debug.Log("UP");
        if (isClick)
            isClick = false;
    }

    void stopCorou()
    {
        if (Pstop)
            Pstop = false;
        if (Nstop)
            Nstop = false;
    }
    void ChatStart()
    {
        //Main_UI.SetActive(false);
        Chat.NpcChoice(NPCName);
        chatBlock.SetActive(false);
    }
    public bool Pstop = true;
    public bool Nstop = true;
    public Transform Ptransform=null;
    public Transform Ntransform=null;

    public IEnumerator Ladderup()
    {
        if (chat.bicycleRide.Ride)
            chat.bicycleRide.RideOn();

        Playerrb.constraints = RigidbodyConstraints.FreezePositionY| RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;

        bool isLadder = false;
        while (isClick)
        {
            //Debug.Log("isCLick");
            if (ClickTime >= minClickTime&&Inter.Ladder)
            {
                //Debug.Log("홀드 중");
                Player.transform.Translate(0, 0.1f, 0);
                PA.SetBool("LadderUp", true);
                isLadder = true;
                yield return new WaitForSecondsRealtime(0.01f);
            }
            ClickTime += Time.deltaTime;

            yield return null;
        }
        if (isLadder)
            isLadder = false;
        else
             LadderPanel.SetActive(true);
        ClickTime = 0;
        PA.SetBool("LadderUp", false);
        Playerrb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ| RigidbodyConstraints.FreezeRotationY;
        yield return new WaitForSeconds(0.5f);
        LadderPanel.SetActive(false);

    }
    public IEnumerator Playerturn(Transform NPC)
    {
        Invoke("stopCorou", 1f);
        PA.SetBool("ChatMove", true);
        yield return new WaitForEndOfFrame();
        Pstop = true;
        Vector3 targetPositionPlayer;
        targetPositionPlayer = new Vector3(NPC.transform.position.x, Player.transform.position.y, NPC.position.z);
        Ptransform = Player.transform;
        while (Pstop)
        {
            if (Player.transform.rotation == Quaternion.LookRotation(targetPositionPlayer - Ptransform.position))
            {
                Debug.Log("NPC스탑");
                Pstop = false;
            }
            Player.transform.rotation = Quaternion.Lerp(Ptransform.rotation, Quaternion.LookRotation(targetPositionPlayer - Player.transform.position), Time.deltaTime * 5f);
            yield return null;
        }
        PA.SetBool("ChatMove", false);
    }
    public IEnumerator NPCturn(Transform NPC, Vector3 targetPositionNPC)
    {
        if (NPC.gameObject.name.Equals("WallMirror"))
        { }
        else
        {
            Animator NA = NPC.GetChild(0).Find("Armature_").gameObject.GetComponent<Animator>();
            NA.SetBool("NpcMove", true);

            yield return new WaitForEndOfFrame();
            Nstop = true;
            //Vector3 targetPositionNPC;
            //targetPositionNPC = new Vector3(Player.transform.position.x, NPC.position.y, Player.transform.position.z);
            Ntransform = NPC.transform;
            while (Nstop)
            {
                if (NPC.rotation == Quaternion.LookRotation(targetPositionNPC - NPC.transform.position))
                {
                    Debug.Log("NPC스탑");
                    Nstop = false;
                }
                NPC.rotation = Quaternion.Lerp(NPC.rotation, Quaternion.LookRotation(targetPositionNPC - NPC.position), Time.deltaTime * 5f);
                yield return null;
            }
            NA.SetBool("NpcMove", false);
        }
    }

    public void TimeCheck()
    {
        var bro = Backend.GameData.GetMyData("USER_SUBQUEST", new Where());

        if (bro.IsSuccess() == false)
        {
            Debug.Log("요청 실패");
            return;
        }
        if (bro.GetReturnValuetoJSON()["rows"].Count <= 0)
        {
            Param param = new Param();  // 새 객체 생성

            param.Add("LastThankTreeTime", time);    //객체에 값 추가

            Backend.GameData.Insert("USER_SUBQUEST", param);   //객체를 서버에 업로드
        }
        else
        {
            var json = bro.GetReturnValuetoJSON();
            var json_data = json["rows"][0];
            ParsingJSON pj = new ParsingJSON();
            MySubQuest data = pj.ParseBackendData<MySubQuest>(json_data);
            time = data.LastThankTreeTime;
        }
    }
}
