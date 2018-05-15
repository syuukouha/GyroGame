using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class InterpolationPath : MonoBehaviour
{
    [BoxGroup("路径名称", centerLabel: true)]
    public string PathName;
    [BoxGroup("路径移动速度", centerLabel: true)]
    public float Speed;
    /// <summary>
    /// 上一帧的路径位置
    /// </summary>
    Vector3 lastPoint;
    /// <summary>
    /// 上一帧的百分比
    /// </summary>
    float percentLast;
    /// <summary>
    /// 当前的百分比
    /// </summary>
    float percentNow;
    // Use this for initialization
    void Start()
    {
        lastPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PathMove();
    }

    /// <summary>
    /// 路径匀速移动（百分比）
    /// </summary>
    void PathMove()
    {
        //路径
        Vector3[] path = iTweenPath.GetPath(PathName);
        lastPoint = transform.position;
        percentLast = percentNow;
        percentNow += 0.001f;
        float distance = Vector3.Distance(iTween.PointOnPath(path, percentNow), iTween.PointOnPath(path, percentLast));
        float percentAmend = 0.001f * Speed * Time.deltaTime / distance;
        percentNow = percentLast + percentAmend;
        //移动到当前百分比的路径位置
        transform.position = iTween.PointOnPath(path, percentNow);
        //面朝下一帧的路径
        Vector3 lookPos = iTween.PointOnPath(path, percentNow += 0.001f) - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 30.0f);
        //如果已经走完，让当前百分比归零
        if (percentNow > 1f)
        {
            percentNow = 0;
        }
    }
}
