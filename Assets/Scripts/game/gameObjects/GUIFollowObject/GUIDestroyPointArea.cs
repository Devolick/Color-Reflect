using UnityEngine;
using System.Collections;

public class GUIDestroyPointArea : Base {
    static GUIDestroyPointArea instance;

    [SerializeField]
    UIDestroyPoint prefab;

    public static GUIDestroyPointArea Instance {
        get { return instance; }
    }

    protected override void _Awake()
    {
        base._Awake();
        instance = this;
        this.transform.GetComponent<Canvas>().worldCamera = Camera.main;
    }
    public UIDestroyPoint CreatePoint(MagniteBall ball) {
        UIDestroyPoint po = Instantiate(prefab);
        po.transform.parent = instance.transform;
        po.transform.localScale = new Vector3(1, 1, 1);
        return po;
    }
    public void UnloadContent() {
        Destroy(this.gameObject);
    }












}
