using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardGameManager : MonoBehaviour
{
    GameObject[] cards;     //카드를 프리펩에서 저장
    GameObject[] cardsMix;  //cards 배열에서 섞은 것을 저장
    int[] level = { 0, 4, 5, 6, 8 };      //레벨 별 카드 종류 개수
    
    private List<GameObject> AllCard = new List<GameObject>();    //생성된 카드 오브젝트

    public static GameObject OpenCard;
    public static GameObject LastCard;
    public static bool GameStart;

    private bool isPause = false; //true일때 pause 상태

    public GameObject WelcomePanel;
    public GameObject GameOverPanel;
    public GameObject PausePanel;

    //결과 출력
    public Text ResultTxt;
    public Text ResultCoinTxt;
    public Text ResultExpTxt;

    int cardCnt;
    int flipCnt = 0;

    static public int stageNum = 1;
    [SerializeField]
    private Text stageTxt;

    int a;

    public enum STATE   //현재 게임 상태 저장
    {
        START, HIT, WAIT, IDLE, CLEAR, FAIL
    };
    static public STATE state;

    void Start()
    {
        Welcome();
    }

    void Update()
    {
        if (GameStart && !isPause)
        {
            switch (state)
            {
                case STATE.START:   //게임 시작
                    CardSet();
                    break;
                case STATE.HIT:     //카드 눌렀을 때
                    CheckCard();
                    break;
                case STATE.CLEAR:   //한 스테이지 클리어
                    StartCoroutine(StageClear());
                    break;
                case STATE.WAIT:    //카드를 열고 기다리는 상태
                    Card_TimeSlider.instance.TimeDel();
                    break;
                case STATE.FAIL:    //시간이 다 되어 게임오버
                    StageFail();
                    break;
                case STATE.IDLE:    //기본 상태
                    Card_TimeSlider.instance.TimeDel();
                    break;
            }
            //시간이 0이 되면 게임 오버
            if (Card_TimeSlider.nowTime <= 0)
            {
                state = STATE.FAIL;
            }
        }
    }

    public void Welcome()   //게임 초기화 함수
    {
        state = STATE.START;
        GameStart = false;
        isPause = false;
        a = 0;
        cards = Resources.LoadAll<GameObject>("Prefabs/Cards/");    //카드들을 전부 저장한다.
        stageNum = 1;   //스테이지 1로 초기화
        //Card_TimeSlider.instance.TimeInit();
        DestroyAll();

        WelcomePanel.SetActive(true);
        GameOverPanel.SetActive(false);
        PausePanel.SetActive(false);
        

    }

    void CardSet()  // 스테이지 카드 섞어서 배열에 저장
    {
        AllCard.Clear();

        StageText();
        int arrLen;
        if (stageNum <= 4)//스테이지에 배치될 카드 종류 개수
        {
            arrLen = level[stageNum];
        }
        else    //4단계부터는 3단계와 동일하게
        {
            arrLen = level[4];
        }
         
        cardCnt = arrLen * 2;   //스테이지에 배치될 카드의 총 개수
        Card_TimeSlider.instance.TimeAdd();   //10초 증가
        GameObject[] arrTmp = new GameObject[cardCnt]; //한 카드 당 두 장씩 저장하게 될 것이므로
        Debug.Log("arrTmp: " + arrTmp.Length);
        for(int i = 0; i < arrLen; i++)
        {
            for (int j = 0; j<2; j++)
            {
                while (true)
                {
                    int rand = Random.Range(0, arrTmp.Length);
                    if(arrTmp[rand] == null)    //arrTmp[rand]에 값이 없을 경우, card[i]를 넣는다. 이를 두 번 반복하여 같은 카드가 두 개 저장되도록.
                    {
                        arrTmp[rand] = cards[i];
                        Debug.Log("arrTmp: " + rand + ", card[i]: " + cards[i]);
                        break;
                    }
                }

            }
        }

        cardsMix = new GameObject[arrTmp.Length];   //배열 길이 초기화
        cardsMix = arrTmp; //섞인 카드를 cardsMix 배열에 넣는다.

        StartCoroutine(MakeStage());
    }

    void CheckCard()
    {
        state = STATE.WAIT;

        if (LastCard == null)
        {
            LastCard = OpenCard;
            state = STATE.IDLE;
            return;
        }
        else if (LastCard.gameObject.tag != OpenCard.gameObject.tag)
        {
            StartCoroutine(CloseTwoCards());
            return;
        }
        else if (LastCard.gameObject.tag == OpenCard.gameObject.tag)
        {
            flipCnt = flipCnt + 2;

            LastCard.SendMessage("DestroyCard", SendMessageOptions.DontRequireReceiver);
            OpenCard.SendMessage("DestroyCard", SendMessageOptions.DontRequireReceiver);

            if (flipCnt == cardCnt)
            {
                state = STATE.CLEAR;
                return;
            }
            LastCard = null;
            OpenCard = null;
        }
        LastCard = null;
        state = STATE.IDLE;
    }
    


    IEnumerator CloseTwoCards()
    {
        yield return new WaitForSeconds(0.2f);
        LastCard.SendMessage("CloseCard", SendMessageOptions.DontRequireReceiver);
        OpenCard.SendMessage("CloseCard", SendMessageOptions.DontRequireReceiver);

        LastCard = null;
        OpenCard = null;
        state = STATE.IDLE;
    }
    IEnumerator StageClear()
    {
        state = STATE.WAIT;

        stageNum++;
        flipCnt = 0;

        yield return new WaitForSeconds(1);
        state = STATE.START;
    }

    void Clear()
    {
        GameOverPanel.SetActive(true);
        GameStart = false;
        GameResult();
    }
    public void StageFail()
    {
        GameStart = false;
        GameOverPanel.SetActive(true);
        DestroyAll();
        GameResult();
    }

    void GameResult()   //점수에 따른 보상 획득 메소드
    {
        float get_exp = 10f;
        int get_coin = stageNum * 5;    //단계별로 * 5 (5, 10, 15, 20, 25...)
        

        PlayInfoManager.GetExp(get_exp);
        PlayInfoManager.GetCoin(get_coin);

        //보상 결과를 화면에 띄움
        ResultTxt.text = stageNum + " 단계";
        ResultCoinTxt.text = get_coin.ToString();
        ResultExpTxt.text = get_exp.ToString();
    }

    IEnumerator MakeStage()
    {
        state = STATE.WAIT;

        float sx = -1.2f;
        float sz = 4.2f;
        if(stageNum >= 3)
        {
            sz = 3.3f;
        }
        if(stageNum >= 4)
        {
            sx = -2.0f;
        }

        int n = 1;

        string[] str;
        if (stageNum <= 4)
        {
            str = SetStage.stage[stageNum - 1];
        }
        else
        {
            str = SetStage.stage[3];
        }
        foreach (string t in str)
        {
            char[] ch = t.Trim().ToCharArray();
            float x = sx;

            foreach (char c in ch)
            {

                switch (c)
                {
                    case '*':

                        GameObject Tempcard = Instantiate(cardsMix[n-1]);
                        Tempcard.transform.GetComponent<CardCtrl>().SoundManager = this.gameObject;
                        Tempcard.transform.position = new Vector3(x, 1f, sz);
                        AllCard.Add(Tempcard);  //세팅된 카드 오브젝트를 AllCard에 저장

                        x = x + 1.35f;
                        n++;
                        break;
                    case '.':
                        x = x + 1.35f;
                        break;
                }

                if (c == '*')
                {
                    yield return new WaitForSeconds(0.03f);
                }
            }
            sz = sz - 2.13f;
        }
        for (int k = 0; k != cardCnt; k++)
        {
            AllCard[k].SendMessage("OpenCard", SendMessageOptions.DontRequireReceiver);
        }
        yield return new WaitForSeconds(3);
        for (int k = 0; k != cardCnt; k++)
        {
            AllCard[k].SendMessage("CloseCard", SendMessageOptions.DontRequireReceiver);
        }
        state = STATE.IDLE;
    }


    private void StageText()
    {
        stageTxt.text = stageNum + " 단계";
    }

    public void GamePause() //일시 정지 누르면 isPause = true, 재개 누르면 isPause = false
    {
        isPause = !isPause;
        if (isPause)
        {
            PausePanel.SetActive(true);
        }
        else
        {
            PausePanel.SetActive(false);
        }
    }

    void DestroyAll()
    {
        for(int i=0; i<AllCard.Count; i++)
        {
            Destroy(AllCard[i]);
        }
        AllCard.Clear();
    }
}