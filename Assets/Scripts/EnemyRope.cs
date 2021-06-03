using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRope : MonoBehaviour
{
    [SerializeField]
    private Rigidbody[] rigids;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        SwitchKinematicMode(true);
        animator.Play("Idle");
    }


    private void SwitchKinematicMode(bool mode)
    {
        foreach (Rigidbody rig in rigids)
        {
            if (mode)
                rig.Sleep();
            else
                rig.WakeUp();
            rig.isKinematic = mode;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hit")
        {
            SwitchKinematicMode(false);
            RemoveCollision();
            Punch();
        }

        if (other.gameObject.tag == "Throw")
        {
            SwitchKinematicMode(false);
            RemoveCollision();
            Hit();
        }
    }
    private void Punch()
    {
        animator.enabled = false;
        foreach (Rigidbody rig in rigids)
        {
            rig.AddForce(0, -100f, -1500f);
            rig.gameObject.tag = "Throw";
        }
        gameObject.tag = "Throw";
    }

    private void RemoveCollision()
    {
        gameObject.layer = 9;
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            child.gameObject.layer = 9;
        }
        this.enabled = false;
    }

    private void Hit()
    {
        animator.enabled = false;
        foreach (Rigidbody rig in rigids)
        {
            rig.AddForce(0, 0f, 0f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
