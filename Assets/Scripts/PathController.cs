using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    Vector3[] NodeData;
    float PositionPercent = 0;

    bool SetPathPosition_from_ITweenPath(string pathname)
    {
        NodeData = iTweenPath.GetPath(pathname);
        if (NodeData == null)
        {
            Debug.Log("[NOT FOUND/null] path name '" + pathname + "'");
            return (false);
        }
        return (true);
    }
    const float addSpeed = 0.005f;
    void Drive_Increase()
    {
        //Debug.Log ("Drive_Increase");
        PositionPercent += addSpeed;
        if (PositionPercent > 1.0f) PositionPercent = 1.0f;
    }
    void Drive_Decrease()
    {
        //Debug.Log ("Drive_Decrease");
        PositionPercent -= addSpeed;
        if (PositionPercent < 0.0f) PositionPercent = 0.0f;
    }

    void Watch_UI_Input()
    {
        bool drive_Fwd = false;
        bool drive_Bwd = false;

        //Begin: Control by Keyboard
        bool input_Key_A = Input.GetKey(KeyCode.A);
        bool input_Key_D = Input.GetKey(KeyCode.D);
        if (!(input_Key_A & input_Key_D))
        { // Disable multi press
            drive_Fwd |= input_Key_A;
            drive_Bwd |= input_Key_D;
        }
        //End: Control by Keyboard
#if UNITY_ANDROID
  //#if UNITY_IPHONE
  //Begin: Control by Accel Sensor
  float accel_X = Input.acceleration.x; // 加速度センサ 横傾き 
  //Debug.Log ("accel_X:"+accel_X);
  if(accel_X <-0.2f){
   drive_Fwd |= true;
  }
  else if(accel_X >0.2f){
   drive_Bwd |= true;
  }
  //End: Control by Accel Sensor
#endif

        if (drive_Fwd) Drive_Increase();
        if (drive_Bwd) Drive_Decrease();
        if (drive_Fwd | drive_Bwd) Debug.Log("Position is " + PositionPercent * 100.0f + "%");
    }

    void Start()
    {
        SetPathPosition_from_ITweenPath("EnemyPath");
    }

    void Update()
    {
        //Watch_UI_Input();
        Drive_Increase();
        transform.position = iTween.PointOnPath(NodeData, PositionPercent); // Get target position 
    }
}
