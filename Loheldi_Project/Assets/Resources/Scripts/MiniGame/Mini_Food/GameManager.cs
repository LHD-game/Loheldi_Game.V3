using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    public static object cInstance { get; internal set; }

    GameObject[] foods;
    GameObject[] lifes;
    int score = 0;
    int highScore = 10;
    int life = 3;

    Image lifeImg;
    Color lifeColor;

    [SerializeField]
    private Text scoreTxt;
    [SerializeField]
    private Text highScoreTxt;
    [SerializeField]
    private GameObject WelcomePanel;
    [SerializeField]
    private GameObject GameOverPanel;
    [SerializeField]
    private GameObject PausePanel;
    public GameObject HPDisablePanel;   //hp 부족으로 인한 팝업 패널 오브젝트

    //결과 출력
    public Text ResultTxt;
    public Text ResultCoinTxt;
    public Text ResultExpTxt;

    private float[] foodDropSec = new float[] {1.8f, 1.6f, 1.4f, 1.0f, 0.8f, 0.5f, 0.4f, 0.3f, 0.3f  }; //중간단계 더 많이 떨어지게 수정
    private float newFoodDropSec;

    private bool stopTrigger = false;   //true일 동안 게임 동작
    private bool pauseTrigger = false;  //true일 경우 일시정지

    public GameObject SoundManager;

    private void Start()
    {
        if(PlayerPrefs.HasKey("FoodHighScore"))
        {
            highScore =  PlayerPrefs.GetInt("FoodHighScore");
        }
        highScoreTxt.text = "최고점수: " + highScore;
        Welcome();
    }

    IEnumerator CreateFoodRoutine()
    {
        int now_hp = PlayerPrefs.GetInt("HP");

        Debug.Log("코루틴: " + now_hp);
        while (stopTrigger)
        {
            CreateFood();
            yield return new WaitForSeconds(newFoodDropSec);
        }
        Debug.Log("stoptrigger" + stopTrigger);

    }

    public void Welcome()
    {
        GameOverPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        PausePanel.SetActive(false);
        WelcomePanel.SetActive(true);

        pauseTrigger = false;

        //Debug.Log("food.Length: " + foods.Length);
        GameObject playerPos = GameObject.FindWithTag("Player");
        playerPos.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        playerPos.transform.rotation = new Quaternion(0, 0, 0, 0);
        CreateLife();
        InitScore();
        newFoodDropSec = foodDropSec[0];
        foods = Resources.LoadAll<GameObject>("Prefabs/Foods/");

    }
    public void GameStart()
    {
        int now_hp = PlayerPrefs.GetInt("HP");

        Debug.Log("밖: "+now_hp);
        if (now_hp > 0)  //현재 hp가 0보다 크다면
        {
            Debug.Log("안: "+now_hp);
            //hp 1 감소
            PlayInfoManager.GetHP(-1);
            stopTrigger = true;

            //foods = GameObject.FindGameObjectsWithTag("Food");  //활성화된 오브젝트만!
            StopCoroutine(CreateFoodRoutine());
            StartCoroutine(CreateFoodRoutine());
            WelcomePanel.SetActive(false);
            Timer.instance.StartTimer();
        }
        else    //0 이하라면: 게임 플레이 불가
        {
            // hp가 부족합니다! 팝업 띄우기
            HPDisablePanel.SetActive(true);
        }

    }


    public void GameOver()
    {
        Time.timeScale = 1f;
        stopTrigger = false;
        StopCoroutine(CreateFoodRoutine());
        GameObject[] disGfoods = GameObject.FindGameObjectsWithTag("GoodFood");
        for (int i = 0; i < disGfoods.Length; i++)
        {
            Destroy(disGfoods[i]);
        }        
        GameObject[] disBfoods = GameObject.FindGameObjectsWithTag("BadFood");
        for (int i = 0; i < disBfoods.Length; i++)
        {
            Destroy(disBfoods[i]);
        }
        GameOverPanel.SetActive(true);
        GameResult();

        //최고 점수를 prefs에 저장
        PlayerPrefs.SetInt("FoodHighScore", highScore);
    }

    void GameResult()   //점수에 따른 보상 획득 메소드
    {
        //0개 이상 10개 미만: 경험치 10, 코인 5
        //10개 이상 20개 미만: 경험치 10, 코인 10
        //20개 이상 30개 미만: 경험치 10, 코인 15
        //30개 이상 40개 미만: 경험치 10, 코인 20
        //40개 이상: 경험치 10, 코인 (점수/2)

        float get_exp = 10f;
        int get_coin = 0;
        
        if(score >= 0 && score < 10)
        {
            get_coin = 5;
        }
        else if (score >= 10 && score < 20)
        {
            get_coin = 10;
        }
        else if (score >= 20 && score < 30)
        {
            get_coin = 15;
        }
        else if (score >= 30 && score < 40)
        {
            get_coin = 20;
        }
        else if (score >= 40)
        {
            get_coin = score / 2;
        }

        PlayInfoManager.GetExp(get_exp);
        PlayInfoManager.GetCoin(get_coin);

        //보상 결과를 화면에 띄움
        ResultTxt.text = score + " 개";
        ResultCoinTxt.text = get_coin.ToString();
        ResultExpTxt.text = get_exp.ToString();
    }

    public void PauseGame()
    {
        if (!pauseTrigger)  //게임 일시정지
        {
            pauseTrigger = true;
            Timer.instance.PauseTimer();
            PausePanel.SetActive(true);
        }
        else    //게임 재개
        {
            pauseTrigger = false;   //트리거 끄기
            Timer.instance.PauseTimer();
            PausePanel.SetActive(false);    //일시정지 패널 비활성화
        }
        

    }

    public bool Blocker()
    {
        return stopTrigger;
    }
    public int ReturnLife()
    {
        return life;
    }


    private void CreateFood()
    {
        float randf;
        if (!pauseTrigger)
        {
            while (true)
            {

                randf = Random.Range(-40.0f, 40.0f);
                //food가 나타날 좌표값
                Vector3 randpos = Camera.main.WorldToViewportPoint(new Vector3(randf, 0.0f, 0.0f));

                if (randpos.x < 0.05f || randpos.x > 0.95f)
                {
                    continue;
                }
                else
                {
                    randpos = Camera.main.ViewportToWorldPoint(randpos);
                    randpos.y = 15.0f;
                    randpos.z = 0.0f;
                    int randFood = Random.Range(0, foods.Length);
                    GameObject Tempfood =  Instantiate(foods[randFood], randpos, Quaternion.Euler(0, 0, 0));
                    //나타날 오브젝트, 좌표값, 회전안함

                    Tempfood.transform.GetComponent<FoodsManager>().SoundManager = this.gameObject;

                    break;
                }
            }
        }
        
        //Vector3 randpos = Camera.main.ViewportToWorldPoint(new Vector3(randf, 0.0f, 0.0f));
        
    }

    private void CreateLife()
    {
        life = 3;
        lifes = GameObject.FindGameObjectsWithTag("Life");

        for (int i=0; i < lifes.Length; i++)
        {
            lifeImg = lifes[i].GetComponent<Image>();
            lifeColor = lifeImg.color;
            lifeColor.a = 1.0f;
            lifeImg.color = lifeColor;
            //lifes[i].SetActive(true);
        }

    }

    private void InitScore()
    {
        score = 0;
        scoreTxt.text = "현재 점수: " + score;
    }

    
    public void ScoreCnt()
    {
        score++;
        if(score > highScore)
        {
            highScore = score;
        }
        scoreTxt.text = "현재 점수: " + score;
        highScoreTxt.text = "최고점수: " + highScore;
        //Debug.Log("score: "+score);
    }
    public void LifeCnt()
    {
        life--;
        lifeImg = lifes[life].GetComponent<Image>();
        lifeColor = lifeImg.color;
        lifeColor.a = 0.0f;
        lifeImg.color = lifeColor;

        if (life <= 0)
        {
            GameOver();
        }
    }

    public void FoodDropsec(int gLevel)
    {
        newFoodDropSec = foodDropSec[gLevel];
        Debug.Log("FoodDropSec: " + foodDropSec[gLevel]);
    }

}
