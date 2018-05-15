using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GyroInput : MonoBehaviour
{
    public Text text;
    Gyroscope gyroscope;
    bool gyroinfo;
    [Header("----GyroInfo----")]
    [SerializeField]
    Vector3 deviceGravity;
    [SerializeField]
    Vector3 rotationVelocity;
    [SerializeField]
    Vector3 rotationVelocity2;
    [SerializeField]
    Vector3 acceleration;
    [SerializeField]
    float updateInterval = 0.1f;
    // Use this for initialization
    void Awake()
    {
        gyroinfo = SystemInfo.supportsGyroscope;
        if (gyroinfo == false)
            this.enabled = false;
        gyroscope = Input.gyro;
        //设置设备陀螺仪的开启/关闭状态，使用陀螺仪功能必须设置为 true    
        Input.gyro.enabled = true;
        //设置陀螺仪的更新检索时间，即隔 0.1秒更新一次    
        Input.gyro.updateInterval = updateInterval;
    }

    // Update is called once per frame
    void Update()
    {
        //获取设备重力加速度向量    
        deviceGravity = Input.gyro.gravity;
        //设备的旋转速度，返回结果为x，y，z轴的旋转速度，单位为（弧度/秒）    
        rotationVelocity = Input.gyro.rotationRate;
        //获取更加精确的旋转    
        rotationVelocity2 = Input.gyro.rotationRateUnbiased;
        //获取移除重力加速度后设备的加速度    
        acceleration = Input.gyro.userAcceleration;
        text.text = string.Format("重力加速度向量:{0}\n旋转速度:{1}\n更加精确的旋转:{2}\n移除重力加速度后设备的加速度;{3}", deviceGravity, rotationVelocity, rotationVelocity2, acceleration);
    }
}
