using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResourceConnect : Base {
    static ResourceConnect instance = null;
    [SerializeField]
    string pathSprite = "";
    [SerializeField]
    string pathPrefab = "";
    [SerializeField]
    string pathForSounds = "FX/";

    protected override void _Awake()
    {
        base._Awake();
        instance = this;
    }
    protected override void _Start()
    {
        base._Start();
    }
    /// <summary>
    /// Mass search in sprite list!
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Sprite GetSprite(string name)
    {
        foreach (Sprite s in Resources.LoadAll<Sprite>(instance.pathSprite))
        {
            if (s.name == name) {
                return s;
            }
        }
        return null;
    }
    public static Texture[] GetFolderTextures(string folder) {
        return Resources.LoadAll<Texture>(folder);
    }
    public static T GetPrefab<T>(string name) where T : Base
    {
        GameObject obj = Resources.Load(instance.pathPrefab + name, typeof(GameObject)) as GameObject;
        return obj.GetComponent<T>();
    }
    public static AudioClip GetSound(string name)
    {
        return Resources.Load(instance.pathForSounds + name, typeof(AudioClip)) as AudioClip;
    }
    public static GameObject GetPrefab(string name)
    {
        return Resources.Load(instance.pathPrefab + name, typeof(GameObject)) as GameObject;
    }
















}
