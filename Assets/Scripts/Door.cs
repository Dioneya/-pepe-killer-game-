using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Collider collider;
    private bool isPunch = false;
    public UnityEvent OnIsBroken = new UnityEvent();

    void Awake() 
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Hit" && other.gameObject.tag != "Throw")
            return;

        rigidbody.isKinematic = false;
        collider.isTrigger = false;
        gameObject.tag = "Throw";

        if (other.gameObject.tag == "Hit") { }
            rigidbody.AddForce(0, 30f, -200f);

        gameObject.layer = 9;
        OnIsBroken?.Invoke();
    }

    private void FixedUpdate()
    {
        if (rigidbody.velocity == Vector3.zero) 
        {
            gameObject.tag = "Untagged";
        }
    }

}
