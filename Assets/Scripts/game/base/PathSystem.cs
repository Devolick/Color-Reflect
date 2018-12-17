using UnityEngine;
using System.Collections;

public class PathSystem : Base {

    [SerializeField]
    Vector3[] pathPoint = null;
    [SerializeField]
    float distance = 0;
    [SerializeField]
    float addAngle = 0;
    [SerializeField]
    bool followPath = true;
    [SerializeField]
    float paddingFromPercent = 0;
    [SerializeField]
    bool updateDistance = false;
    [SerializeField]
    bool updatePosition = false;
    float percentAtLine = 0f;

    public int Points {
        get { return pathPoint.Length; }
    }
    public Vector3 this[int index]
    {
        set {
            pathPoint[index] = value;
        }
        get { return pathPoint[index]; }
    }
    public float Percent {
        get { return percentAtLine; }
    }
    public float PercentPadding {
        set { paddingFromPercent = value; }
        get { return paddingFromPercent; }
    }
    public float Distance {
        get { return distance; }
    }

    protected override void _Awake()
    {
        base._Awake();
        FindDistance();
        Move(0);
    }
    protected override void _Update()
    {
        base._Update();
        UpdateDistance();
    }
    void UpdateDistance() {
        if(updateDistance)
             FindDistance();
        if (updatePosition)
            Move(percentAtLine);
    }
    public void Move(float percent)
    {
        if (percent > 1) { percent = 1; }
        else if (percent < 0) { percent = 0; }
        percentAtLine = percent;
        if (paddingFromPercent > 0)
        {
            SetDistanceByPercent(((paddingFromPercent / 2f) + (percent * (1f - paddingFromPercent))));
        }
        else
        {
            SetDistanceByPercent(percentAtLine);
        }
    }
    public void AddMove(float percent) {
        Move(this.percentAtLine + percent);
    }
    public void ReturnCenter()
    {
        Move(Mathf.Lerp(percentAtLine, 0.5f, 0.04f));
    }
    void FindDistance()
    {
        float distance = 0;
        if (pathPoint.Length > 1)
            for (int i = 0; i < pathPoint.Length - 1; ++i)
            {
                distance += Vector3.Distance(pathPoint[i], pathPoint[i + 1]);
            }
        this.distance = distance;
    }
    void LookAtLine(Vector3 from , Vector3 to) {
        Vector3 dir = to - from;
        dir.Normalize();
        float rad = Mathf.Atan2(dir.y, dir.x);
        float angle = ((180 / Mathf.PI) * rad) + addAngle;
        this.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    void SetDistanceByPercent(float percent)
    {
        if (pathPoint.Length > 1)
        {
            float dist = this.distance * percent;
            float distLine = 0;
            for (int i = 0; i < pathPoint.Length - 1; ++i)
            {
                distLine += Vector3.Distance(pathPoint[i], pathPoint[i + 1]);
                if (dist <= distLine)
                {
                    float indexDistance = Vector3.Distance(pathPoint[i], pathPoint[i + 1]);
                    float lerpPercent = (dist - (distLine - indexDistance)) / indexDistance;
                    this.transform.position = Vector3.Lerp(pathPoint[i], pathPoint[i+1], lerpPercent);
                    if(followPath)
                        LookAtLine(pathPoint[i], pathPoint[i + 1]);
                    break;
                }
            }
        }
    }





}
