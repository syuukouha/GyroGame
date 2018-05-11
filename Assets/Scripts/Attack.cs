using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public bool IsOnGround = true;
    private Rigidbody _rigidbody;
    private SphereCollider sphereCollider;
    private Vector3 gizmosCenter;
    private float gizmosRadius;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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
        Gizmos.DrawSphere(gizmosCenter, gizmosRadius);
    }

}
