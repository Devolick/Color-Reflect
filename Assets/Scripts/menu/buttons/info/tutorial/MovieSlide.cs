using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class MovieSlide : MonoBehaviour {
    RawImage raw;
    [SerializeField]
    Texture[] listTexture;
    [SerializeField]
    float duration = 1f;
    float elapsedDuration = 0f;
    public string folderPath = "";
    bool pause = false;

    public bool isPlaying { get; protected set; }
    public bool isReadyToPlay
    {
        get
        {
            if (listTexture != null)
            {
                if (listTexture.Length > 0) { return true; }
            }
            return false;
        }
    }
    void Start() {
        raw = this.transform.GetComponent<RawImage>();
        listTexture = ResourceConnect.GetFolderTextures(folderPath);
        FirstFrame();
    }
    public void FirstFrame() {
        if (isReadyToPlay) {
            raw.texture = listTexture[0];
        }
    }
    public void Pause() {
        pause = true;
    }
    public void Play() {
        isPlaying = true;
        pause = false;
    }
    public void Stop() {
        pause = false;
        isPlaying = false;
        elapsedDuration = 0f;
        FirstFrame();
    }
    public void Repeat() {
        Stop();
        Play();
    }
    void Update() {
        if (isPlaying && isReadyToPlay && !pause) {
            elapsedDuration += Time.deltaTime;
            int index = (int)(elapsedDuration / (duration / listTexture.Length));
            if (index < listTexture.Length)
            {
                raw.texture = listTexture[(int)(elapsedDuration / (duration / listTexture.Length))];
            }
            else {
                Stop();
            }
        }
    }




}
