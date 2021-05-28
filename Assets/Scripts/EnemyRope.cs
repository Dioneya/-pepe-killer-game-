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

            Punch();
        }

        if (other.gameObject.tag == "Throw")
        {
            SwitchKinematicMode(false);
            Hit();
        }
    }
    private void Punch()
    {
        animator.enabled = false;
        foreach (Rigidbody rig in rigids)
        {
            rig.AddForce(0, 100f, -500f);
        }
        gameObject.tag = "Throw";
    }

    private void Hit()
    {
        animator.enabled = false;
        foreach (Rigidbody rig in rigids)
        {
            rig.AddForce(0, 0f, -50f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
