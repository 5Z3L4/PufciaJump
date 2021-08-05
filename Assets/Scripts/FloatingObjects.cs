using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObjects : MonoBehaviour
{
    public float waterLevel = 4;
    public float floatHeight = 2;
    public float bounceDamp = 0.05f;
    public Vector3 bounceCenter;
    public Rigidbody rigidbody;

    private float forceFactor;
    private Vector3 actionPoint;
    private Vector3 upLift;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        actionPoint = transform.position + transform.TransformDirection(bounceCenter);
        forceFactor = 1f - ((actionPoint.y - waterLevel) / floatHeight);

        if (forceFactor > 0)
        {
            upLift = -Physics.gravity * (forceFactor - rigidbody.velocity.y * bounceDamp);
            rigidbody.AddForceAtPosition(upLift, actionPoint);
        }
    }
}
