using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;
    [SerializeField] Slider progressBar;

    public static Text tiptext;

    public Image LoadingImage1;
    public Image LoadingImage2;
    public Image LoadingImage3;
    public Image LoadingImage4;

    private void Start()
    {
        tiptext = GameObject.Find("Text").GetComponent<Text>();
        int tipnum = Random.Range(0, 6);
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
        int imagenum = Random.Range(0, 4);
        StartCoroutine(LoadScene());
        switch (imagenum)
        {
            case 0:
                LoadingImage1.sprite = Resources.Load<Sprite>("Resources/Sprites/lodingWindow/Loading1");
                break;
            case 1:
                LoadingImage2.sprite = Resources.Load<Sprite>("Resources/Sprites/lodingWindow/Loading2");
                break;
            case 2:
                LoadingImage3.sprite = Resources.Load<Sprite>("Resources/Sprites/lodingWindow/Loading3");
                break;
            case 3:
                LoadingImage4.sprite = Resources.Load<Sprite>("Resources/Sprites/lodingWindow/Loading4");
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
}
