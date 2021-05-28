using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    [SerializeField]
    private Rigidbody[] rigids;
    private Collider collider;

    void Awake()
    {
        collider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hit")
        {
            gameObject.layer = 10;
            foreach (Rigidbody rig in rigids) 
            {
                rig.isKinematic = false;
                rig.AddForce(0, 0, -200f);
                gameObject.layer = 9;
            }


            collider.isTrigger = false;
            gameObject.tag = "Throw";
            //StartCoroutine(Punched());
        }

        if (other.gameObject.tag == "Throw")
        {
            gameObject.layer = 10;
            foreach (Rigidbody rig in rigids)
            {
                rig.isKinematic = false;
                rig.AddForce(0, 0, -200f);
                gameObject.layer = 9;
            }
            collider.isTrigger = false;
            gameObject.tag = "Throw";

        }

    }

}
