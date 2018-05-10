using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    public Transform father;
	// Use this for initialization
	void Start () {
		
	}
    public float rotateAcc;
    public float maxAngle;


    public float acc;
    public float friction;
    public float maxSpeed;
    public Vector3 speed;
	// Update is called once per frame
	void Update () {
        //print(transform.TransformDirection(Vector3.forward));
        //this.transform.rotation = Quaternion.AngleAxis(360f * a,new Vector3(1,1,0));
        m_preVelocity = GetComponent<Rigidbody>().velocity;

#if UNITY_EDITOR

        if (Input.GetKey(KeyCode.W))
        {
            speed -= new Vector3(0, 0, acc * Time.deltaTime);

            father.localEulerAngles -= new Vector3(rotateAcc * Time.deltaTime,0,0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            speed += new Vector3(0, 0, acc * Time.deltaTime);

            father.localEulerAngles += new Vector3(rotateAcc * Time.deltaTime, 0, 0);
        }
        else {
            speed -= new Vector3(0, 0, Mathf.Sign(speed.z) * acc * Time.deltaTime);

            father.localEulerAngles -= new Vector3((father.localEulerAngles.x > 180f ? -1f : 1f) * rotateAcc * Time.deltaTime,0,0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            speed += new Vector3(acc * Time.deltaTime,0,0);

            father.localEulerAngles -= new Vector3(0, 0, rotateAcc * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            speed -= new Vector3(acc * Time.deltaTime,0,0);

            father.localEulerAngles += new Vector3(0, 0, rotateAcc * Time.deltaTime);
        }
        else {
            speed -= new Vector3(Mathf.Sign(speed.x) * acc * Time.deltaTime,0,0);

            father.localEulerAngles -= new Vector3(0, 0, (father.localEulerAngles.z>180f?-1f:1f) * rotateAcc * Time.deltaTime);
        }

        
#else

        speed += new Vector3(-Input.gyro.gravity.x / 0.2f * acc * Time.deltaTime, 0, -Input.gyro.gravity.y / 0.2f * acc * Time.deltaTime);
        
        //摩擦力
        speed -= new Vector3(Mathf.Sign(speed.x) * friction * Time.deltaTime, 0, Mathf.Sign(speed.z) * friction * Time.deltaTime);
        

        father.localEulerAngles = new Vector3(-Mathf.LerpAngle(father.localEulerAngles.x,Input.gyro.gravity.y / 0.2f * maxAngle,20f* Time.deltaTime)
        , 0, Mathf.LerpAngle(father.localEulerAngles.z, Input.gyro.gravity.x / 0.2f * maxAngle, 20f * Time.deltaTime));
#endif
        if (Mathf.DeltaAngle(father.localEulerAngles.z, 0f) > maxAngle)
        {
            father.localEulerAngles = new Vector3(father.localEulerAngles.x, father.localEulerAngles.y, -maxAngle);
        }
        if (Mathf.DeltaAngle(0, father.localEulerAngles.z) > maxAngle)
        {
            father.localEulerAngles = new Vector3(father.localEulerAngles.x, father.localEulerAngles.y, maxAngle);
        }
        if (Mathf.DeltaAngle(father.localEulerAngles.x, 0f) > maxAngle)
        {
            father.localEulerAngles = new Vector3(-maxAngle, father.localEulerAngles.y, father.localEulerAngles.z);
        }
        if (Mathf.DeltaAngle(0, father.localEulerAngles.x) > maxAngle)
        {
            father.localEulerAngles = new Vector3(maxAngle, father.localEulerAngles.y, father.localEulerAngles.z);
        }

        GetComponent<Rigidbody>().velocity = new Vector3(speed.x, GetComponent<Rigidbody>().velocity.y,speed.z);

        
	}

    private Vector3 m_preVelocity = Vector3.zero;//上一帧速度

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "wall")
        {
            print("test"); 
            //print(collision.relativeVelocity);
            //print(collision.contacts[0].point + "  " + collision.contacts[0].normal);    

            //print(collision.contacts[0].point.ToString("F4") + "    "  +collision.contacts[0].normal.ToString("F4"));

            //print(collision.contacts[0].normal.ToString("F4") +"   "+Vector3.Reflect(Vector3.up, collision.contacts[0].normal));

            Debug.DrawRay(transform.position, collision.contacts[0].normal * 10f,Color.red , 0.02f);
            Debug.DrawRay(transform.position, Vector3.Reflect(transform.TransformDirection(speed.normalized), collision.contacts[0].normal) * 10f, Color.blue, 0.02f);


            speed = Vector3.Reflect(transform.TransformDirection(speed.normalized), collision.contacts[0].normal) * Mathf.Clamp(speed.magnitude,0f, maxSpeed) *2f;
/*

            ContactPoint contactPoint = collision.contacts[0];
            Vector3 newDir = Vector3.zero;
            Vector3 curDir = transform.TransformDirection(Vector3.forward);
            newDir = Vector3.Reflect(curDir, contactPoint.normal);
            Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, newDir);
            transform.rotation = rotation;
*/

            //print(curDir +"  " +newDir);

            //GetComponent<Rigidbody>().velocity = newDir.normalized * m_preVelocity.x / m_preVelocity.normalized.x;
        }
    }
}
