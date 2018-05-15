using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterpolationPath : MonoBehaviour
{
    public Transform[] path;

    public float speed;
    public Transform target;
    // Use this for initialization
    void Start()
    {
        lastPoint = target.position;
    }

    Vector3 lastPoint;
    float percentLast;
    float percentNow;
    // Update is called once per frame
    void Update()
    {
        lastPoint = target.position;
        percentLast = percentNow;
        percentNow += 0.001f;
        float distance = Vector3.Distance(iTween.PointOnPath(path, percentNow), iTween.PointOnPath(path, percentLast));
        float percentAmend = 0.001f * speed * Time.deltaTime / distance;
        percentNow = percentLast + percentAmend;
        target.position = iTween.PointOnPath(path, percentNow);
        Vector3 lookPos = iTween.PointOnPath(path, percentNow += 0.001f) - target.transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        target.transform.rotation = Quaternion.Slerp(target.transform.rotation, rotation, Time.deltaTime * 30.0f);
        if (percentNow > 1f)
        {
            percentNow = 0;
        }
    }

    private void OnDrawGizmos()
    {
        iTween.DrawPathGizmos(path, Color.red);
    }
}
