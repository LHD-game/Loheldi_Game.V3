using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoScript : MonoBehaviour
{
    public GameObject myVideo;
    public VideoPlayer videoClip;
    public VideoClip[] VideoClip = new VideoClip[2];
    [SerializeField]
    private RawImage mScreen = null;
    [SerializeField]
    private VideoPlayer mVideoPlayer = null;
    public GameObject VideoPlayUI;
    //bool play = false;

    public void OnPlayVideo()
    {
        if (mScreen != null && mVideoPlayer != null)
        {
            // ���� �غ� �ڷ�ƾ ȣ��
            StartCoroutine(PrepareVideo());
        }
        IEnumerator PrepareVideo()
        {
            // ���� �غ�
            mVideoPlayer.Prepare();

            // ������ �غ�Ǵ� ���� ��ٸ�
            while (!mVideoPlayer.isPrepared)
            {
                yield return new WaitForSeconds(0.5f);
            }

            // VideoPlayer�� ��� texture�� RawImage�� texture�� �����Ѵ�
            mScreen.texture = mVideoPlayer.texture;

            videoClip.loopPointReached += CheckOver;
            myVideo.SetActive(true);
            VideoPlayUI.SetActive(true);
            videoClip.Play();
        }
        
        //play = true;
    }

    void CheckOver(VideoPlayer vp)
    {
        finishButtonActive();
    }
    public void OnFinishVideo()
    {
        myVideo.SetActive(false);
        videoClip.Stop();
    }

    public void OnResetVideo()
    {
        videoClip.time = 0f;
        videoClip.playbackSpeed = 1f;
        videoClip.Play();
    }

    public GameObject finishButton;
    void finishButtonActive()
    {
        finishButton.SetActive(true);
    }
}
