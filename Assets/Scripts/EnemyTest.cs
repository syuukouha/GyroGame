using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public string PathName = "EnemyPath";
    public float Speed = 10.0f;
    private Vector3[] path;
    private float pathLength;
    private float realOnePercent;
    private float pathOnePercent;
    private float newPercentPosition;
    private float distortion;
    private float realPercentToMove;


    // Use this for initialization
    void Start()
    {
        path = iTweenPath.GetPath(PathName);
        pathLength = iTween.PathLength(path);
        realOnePercent = pathLength * 0.1f;
        //iTween.MoveTo(this.gameObject, iTween.Hash("path", path, "speed", Speed, "easeType", iTween.EaseType.linear, "orienttopath", true));
    }

    // Update is called once per frame
    void Update()
    {
        MovePath();
    }
    // move around path with constant speed
    private void MovePath()
    {
        pathOnePercent = Vector3.Distance(
            iTween.PointOnPath(path, newPercentPosition),
            iTween.PointOnPath(path, newPercentPosition + 0.10f)
        );

        distortion = realOnePercent / pathOnePercent;

        realPercentToMove = (Speed * Time.deltaTime) / pathLength;

        newPercentPosition += (realPercentToMove * distortion);
        if (newPercentPosition < 1.0f)
        {
            iTween.PutOnPath(gameObject, path, newPercentPosition);
            transform.forward = iTween.PointOnPath(path, newPercentPosition).normalized;
        }
    }
}
