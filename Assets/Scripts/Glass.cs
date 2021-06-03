using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    [SerializeField]
    private Rigidbody[] rigids;
    private Collider collider;
    public bool isForRope = false;

    void Awake()
    {
        collider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Throw" || other.gameObject.tag == "Hit" || (isForRope && other.gameObject.tag == "Player"))
        {
            gameObject.layer = 10;
            foreach (Rigidbody rig in rigids)
            {
                rig.isKinematic = false;
                rig.AddForce(0, 0, Random.Range(-50f,-200f));
                rig.gameObject.layer = 10;
            }
            collider.isTrigger = false;
            //gameObject.tag = "Throw";

        }


    }

}
