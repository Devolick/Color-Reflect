using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Advertisements;

public interface IHasAd {
    void CallBackAd(ShowResult result);
}

public class AdManager : MonoBehaviour {

    static AdManager instance;
    [SerializeField]
    string zone = "rewardedVideo";
    [SerializeField]
    float timeToShowAd = 1150f;
    [SerializeField]
    float timeToShowAdLimit = 1200f;
    bool showAdWhenAskIt = true;
    DateTime date = DateTime.Now;
    string test = "";

    void Awake()
    {
        instance = this;
        CheckDayForResetAd();
    }
    void CheckDayForResetAd() {
        string pref = PlayerPrefs.GetString("addatetime");
        if (pref == "")
        {
            date = DateTime.Now;
        }
        else
        {
            date = DateTime.Parse(pref);
            if (date.Day != DateTime.Now.Day)
            {
                showAdWhenAskIt = true;
            }
        }
    }
    public static bool ShowAd(Action<ShowResult> cb)
    {
        if (instance.showAdWhenAskIt)
        {
            ShowOptions option = new ShowOptions();
            option.resultCallback = cb;
            if (Advertisement.IsReady(instance.zone))
            {
                Advertisement.Show(instance.zone, option);
                instance.timeToShowAd = 0f;
                instance.showAdWhenAskIt = false;
                return true;
            }
        }
        return false;
    }
    void Update() {
        if (timeToShowAd >= timeToShowAdLimit)
             { showAdWhenAskIt = true; }
        else { showAdWhenAskIt = false; }
        if (timeToShowAd < timeToShowAdLimit) { timeToShowAd += Time.deltaTime; }
    }
    void OnDisable() {
        PlayerPrefs.SetString("addatetime", date.ToString());
    }






}
