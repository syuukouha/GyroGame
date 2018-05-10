using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavWayPoint : MonoBehaviour
{
    public GameObject[] pathPoints;
    public float Speed;
    //记录下一个路点

    int nextPathPointIndex = 1;

    void Start()
    {

        pathPoints = GameObject.FindGameObjectsWithTag("Path");

        //  Array.Reverse(pathPoints);翻转排序，最简单的方法，下面的排序高大上些。

        Array.Sort(pathPoints, (x, y) => { return x.gameObject.name.CompareTo(y.gameObject.name); });    //排序，得到所有路点

        //transform.position = pathPoints[0].transform.position;

        //transform.forward = pathPoints[nextPathPointIndex].transform.position - transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(pathPoints[nextPathPointIndex].transform.position, transform.position) < 0.1f)
        {
            if (nextPathPointIndex != pathPoints.Length - 1)
            {
                nextPathPointIndex++;
            }
            if (Vector3.Distance(pathPoints[pathPoints.Length - 1].transform.position, transform.position) < 0.1f)
            {
                transform.position = pathPoints[pathPoints.Length - 1].transform.position;
                return;
            }
            //transform.forward = pathPoints[nextPathPointIndex].transform.position - transform.position;
        }
        //transform.Translate(Vector3.forward * 5 * Time.deltaTime, Space.Self);//以自己的轴走动
        transform.position = Vector3.MoveTowards(transform.position, pathPoints[nextPathPointIndex].transform.position, Speed * Time.deltaTime);
    }
}
