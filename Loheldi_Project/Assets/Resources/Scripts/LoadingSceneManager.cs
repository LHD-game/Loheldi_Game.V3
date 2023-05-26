using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;
    [SerializeField] Slider progressBar;

    public Text LoadingText;
    public static Text tiptext;

    public Sprite[] LoadingImages;
    public Image LoadingImage;
    //public Image LoadingImage3;
    //public Image LoadingImage4;

    private string text = "이동하는 중...";

    private void Start()
    {
        StartCoroutine(_typing());
        tiptext = GameObject.Find("Text").GetComponent<Text>();
        int tipnum = Random.Range(0, 4);
        switch (tipnum)
        {
            case 0:
                tiptext.text = "집 왼쪽의 작은 텃밭에서는 씨앗을 심고 수확할 수 있습니다.";
                break;
            case 1:
                tiptext.text = "미니게임을 통해 코인과 경험치를 획득할 수 있습니다.";
                break;
            case 2:
                tiptext.text = "매일 새로운 메인 퀘스트를 수행할 수 있습니다. 풍성한 보상을 얻어 보세요.";
                break;
            case 3:
                tiptext.text = "코인은 많이 모았나요? 슈퍼 앞에 있는 뽑기에 도전해 보세요.";
                break;
            case 4:
                tiptext.text = "게임에 필요한 데이터를 가져오고 있으니 잠시만 기다려 주세요.";
                break;
            default:
                break;
        }
        int imagenum = Random.Range(0, 4);
        StartCoroutine(LoadScene());
        LoadingImage.sprite = LoadingImages[imagenum];
        /*switch (imagenum)
        {
            case 0:
                LoadingImage.sprite = LoadingImages[imagenum];//Resources.Load<Sprite>("Resources/Sprites/lodingWindow/Loading1");
                break;
            case 1:
                LoadingImage.sprite = LoadingImages[1];//Resources.Load<Sprite>("Resources/Sprites/lodingWindow/Loading2");
                break;
            case 2:
                LoadingImage.sprite = LoadingImages[2];//Resources.Load<Sprite>("Resources/Sprites/lodingWindow/Loading3");
                break;
            case 3:
                LoadingImage.sprite = LoadingImages[3];//Resources.Load<Sprite>("Resources/Sprites/lodingWindow/Loading4");
                break;
            default:
                break;
        }*/
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                progressBar.value = Mathf.Lerp(progressBar.value, op.progress, timer);
                if (progressBar.value >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.value = Mathf.Lerp(progressBar.value, 1f, timer);
                if (progressBar.value == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
    IEnumerator _typing()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            LoadingText.text = "";
            for (int i = 0; i <= text.Length; i++)
            {
                LoadingText.text = text.Substring(0, i);

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
