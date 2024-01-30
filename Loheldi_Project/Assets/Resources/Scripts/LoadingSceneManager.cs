using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;
    [SerializeField] Slider progressBar;
    [SerializeField] Trans trans;
    public Text LoadingText;
    public static Text tiptext;

    public Sprite[] LoadingImages;
    public Image LoadingImage;
    //public Image LoadingImage3;
    //public Image LoadingImage4;

    private string text = "�̵��ϴ� ��...";

    private void Start()
    {
        StartCoroutine(_typing());
        tiptext = GameObject.Find("Text").GetComponent<Text>();
        int tipnum = Random.Range(0, 6);
        if (trans.tranbool)
            EngLoading(tipnum);
        else
            KrLoading(tipnum);
        int imagenum = Random.Range(0, 4);
        StartCoroutine(LoadScene());
        LoadingImage.sprite = LoadingImages[imagenum];
    }

    private void EngLoading(int tipnum)
    {
        switch (tipnum)
        {
            case 0:
                tiptext.text = "In the small garden on the left side of the house, you can plant and harvest seeds.";
                break;
            case 1:
                tiptext.text = "You can earn coins and experience points through mini games.";
                break;
            case 2:
                tiptext.text = "You can perform a new main quest every day. Get rich rewards.";
                break;
            case 3:
                tiptext.text = "Did you save a lot of coins? Try the claw machine in front of the supermarket.";
                break;
            case 4:
                tiptext.text = "I'm getting the data I need for the game, so please wait.";
                break;
            case 5:
                tiptext.text = "Quests are divided into weekday quests and weekend quests.";
                break;
            case 6:
                tiptext.text = "On Saturdays, quests are not conducted to allow the residents to rest.";
                break;
            default:
                break;
        }
    }

    private void KrLoading(int tipnum)
    {
        switch (tipnum)
        {
            case 0:
                tiptext.text = "�� ������ ���� �Թ翡���� ������ �ɰ� ��Ȯ�� �� �ֽ��ϴ�.";
                break;
            case 1:
                tiptext.text = "�̴ϰ����� ���� ���ΰ� ����ġ�� ȹ���� �� �ֽ��ϴ�.";
                break;
            case 2:
                tiptext.text = "���� ���ο� ���� ����Ʈ�� ������ �� �ֽ��ϴ�. ǳ���� ������ ��� ������.";
                break;
            case 3:
                tiptext.text = "������ ���� ��ҳ���? ���� �տ� �ִ� �̱⿡ ������ ������.";
                break;
            case 4:
                tiptext.text = "���ӿ� �ʿ��� �����͸� �������� ������ ��ø� ��ٷ� �ּ���.";
                break;
            case 5:
                tiptext.text = "����Ʈ�� ���� ����Ʈ�� �ָ� ����Ʈ�� ���еǾ� �ֽ��ϴ�.";
                break;
            case 6:
                tiptext.text = "����Ͽ��� �ֹε��� �޽��� ���� ����Ʈ ������ ���� �ʽ��ϴ�.";
                break;
            default:
                break;
        }
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
