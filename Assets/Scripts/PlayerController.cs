using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class PlayerController : MonoBehaviour {
    public Transform father;
	// Use this for initialization
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    [TabGroup("运动")]
    public Vector3 speed;
    [TabGroup("运动")]
    public float speedAcc;
    [TabGroup("运动")]
    public float friction;
    [TabGroup("运动")]
    public float maxSpeed;

    [TabGroup("场景")]
    public float rotateAcc;
    [TabGroup("场景")]
    public float maxAngle;

    [TabGroup("攻击")]
    public bool IsOnGround = true;

    private Rigidbody _rigidbody;
    private SphereCollider sphereCollider;
    private Vector3 gizmosCenter;
    private float gizmosRadius;

	// Update is called once per frame
	void Update () {
        //print(transform.TransformDirection(Vector3.forward));
        //this.transform.rotation = Quaternion.AngleAxis(360f * a,new Vector3(1,1,0));

        KeyDetect();
        Attack();
        
	}

    void KeyDetect()
    {
#if UNITY_EDITOR



        if (Input.GetKey(KeyCode.W))
        {
            speed -= new Vector3(0, 0, speedAcc * Time.deltaTime);

            father.localEulerAngles -= new Vector3(rotateAcc * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            speed += new Vector3(0, 0, speedAcc * Time.deltaTime);

            father.localEulerAngles += new Vector3(rotateAcc * Time.deltaTime, 0, 0);
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {

            float angle = father.localEulerAngles.x;

            if (angle == 0)
            {

            }
            else if (angle > 180f)
            {
                angle += rotateAcc * Time.deltaTime;
                if (angle > 360f)
                {
                    angle = 0;
                }
            }
            else if (angle < 180f)
            {
                angle -= rotateAcc * Time.deltaTime;

                if (angle < 0f)
                {
                    angle = 0;
                }
            }


            father.localEulerAngles = new Vector3(angle, father.localEulerAngles.y, father.localEulerAngles.z);
        }




        if (Input.GetKey(KeyCode.A))
        {
            speed += new Vector3(speedAcc * Time.deltaTime, 0, 0);

            father.localEulerAngles -= new Vector3(0, 0, rotateAcc * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            speed -= new Vector3(speedAcc * Time.deltaTime, 0, 0);

            father.localEulerAngles += new Vector3(0, 0, rotateAcc * Time.deltaTime);
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            float angle = father.localEulerAngles.z;

            if (angle == 0)
            {

            }
            else if (angle > 180f)
            {
                angle += rotateAcc * Time.deltaTime;
                if (angle > 360f)
                {
                    angle = 0;
                }
            }
            else if (angle < 180f)
            {
                angle -= rotateAcc * Time.deltaTime;

                if (angle < 0f)
                {
                    angle = 0;
                }
            }


            father.localEulerAngles = new Vector3(father.localEulerAngles.x, father.localEulerAngles.y, angle);

        }
        speed -= new Vector3(Mathf.Sign(speed.x) * friction * Time.deltaTime, 0, Mathf.Sign(speed.z) * friction * Time.deltaTime);

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

        GetComponent<Rigidbody>().velocity = new Vector3(speed.x, GetComponent<Rigidbody>().velocity.y, speed.z);
    }

    //碰撞反弹运算
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Ground"))
        {


            Debug.DrawRay(transform.position, collision.contacts[0].normal * 10f,Color.red , 0.02f);
            Debug.DrawRay(transform.position, Vector3.Reflect(transform.TransformDirection(speed.normalized), collision.contacts[0].normal) * 10f, Color.blue, 0.02f);

            speed = Vector3.Reflect(transform.TransformDirection(speed.normalized), collision.contacts[0].normal) * Mathf.Clamp(speed.magnitude,0f, maxSpeed) *2f;

        }
    }


    

    void Attack()
    {
        gizmosCenter = this.transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, sphereCollider.radius + 0.01f, LayerMask.GetMask("Ground"));
        IsOnGround = hitColliders.Length > 0;
        if (IsOnGround)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0.0f, _rigidbody.velocity.z);
            gizmosRadius = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround)
        {
            _rigidbody.velocity += Vector3.up * Random.Range(2.0f, 10.0f);
            gizmosRadius = sphereCollider.radius * _rigidbody.velocity.y;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gizmosCenter, gizmosRadius);
    }
}
