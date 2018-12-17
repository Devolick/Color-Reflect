using UnityEngine;
using System.Collections;

public class LevelTransferCamera : Base {
    protected static LevelTransferCamera instance;
    AudioSource audio;

    public static bool MuteGlobalSound {
        set { instance.audio.mute = value; }
    }

    protected override void _Awake()
    {
        base._Awake();
        Transfer();
    }
    void Transfer()
    {
        if (instance == null)
        {
            GameObject.DontDestroyOnLoad(gameObject);
            instance = this;
            GameInput.FillTouch(this);
            audio = this.transform.GetComponent<AudioSource>();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }





















}
