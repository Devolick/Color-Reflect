using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameInput : Base {
    static GameInput instance;
    protected List<string> testList;


    protected override void _Awake()
    {
        base._Awake();
        instance = this;
        testList = new List<string>();
    }
    public static void FillTouch(Base com)
    {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        com.gameObject.AddComponent<MouseTouch>();
#elif UNITY_ANDROID || UNITY_WINRT || UNITY_WINRT_8_0 ||UNITY_WINRT_8_1||UNITY_WINRT_10_0
        com.gameObject.AddComponent<NewMobileMultiTouch>();
#endif
    }
    #region Sort 2D Layer by Z index
    public static void SortLayerDistance2D(Base root) {
        FillLayerDistance2D(root);
        if (root.transform.parent != null) {
            Vector3 pos = root.transform.parent.position;
            Vector3 posMain = root.transform.position;
            root.transform.position = new Vector3(posMain.x, posMain.y, pos.z);
        }
    }
    public static void SortLayersDistance2D(Base root)
    {
        FillLayerDistance2D(root);
        float distanceZ = root.transform.position.z;
        SortLayers(root.gameObject, distanceZ);
    }
    static void FillLayerDistance2D(Base root) {
        GameObject o = root.FindRoot();
        if (root.gameObject.Equals(o))
        {
            Vector3 posLayer = root.transform.position;
            posLayer.z = Mathf.Abs(root.gameObject.layer - 32);
            root.transform.position = posLayer;
        }
    }
    static void SortLayers(GameObject root, float distanceZ)
    {
        float _distanceZ = distanceZ / 2;
        foreach (Transform trs in root.transform)
        {
            Vector3 pos = trs.position;
            trs.position = new Vector3(pos.x, pos.y, _distanceZ);
            if (trs.childCount > 0)
            {
                SortLayers(trs.gameObject, _distanceZ);
            }
        }
    }
    #endregion

    protected void FillScreenObject(out GameInputArgs resultArgs, Vector2 position)
    {
        resultArgs = new GameInputArgs();
        Ray ray = Camera.main.ScreenPointToRay(position);
        resultArgs.Trans = Physics2D.Raycast(ray.origin, ray.direction).transform;
    }
    protected void InputToObject(GameInputArgs resultArgs)
    {
        if (resultArgs.Trans != null)
        {
            ITouchAddressee touchSend = resultArgs.Trans.gameObject.GetComponent<ITouchAddressee>();
            if (touchSend != null)
            {
                touchSend.Touch(this, resultArgs);
            }
        }
    }












}
