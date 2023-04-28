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
            // 비디오 준비 코루틴 호출
            StartCoroutine(PrepareVideo());
        }
        IEnumerator PrepareVideo()
        {
            // 비디오 준비
            mVideoPlayer.Prepare();

            // 비디오가 준비되는 것을 기다림
            while (!mVideoPlayer.isPrepared)
            {
                yield return new WaitForSeconds(0.5f);
            }

            // VideoPlayer의 출력 texture를 RawImage의 texture로 설정한다
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
