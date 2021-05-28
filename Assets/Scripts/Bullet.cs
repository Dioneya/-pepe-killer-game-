using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SelfDestroy());
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag != "Hit")
            return;

        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().AddForce(0,100f,-1500f);
        gameObject.tag = "Throw";
    }

    IEnumerator SelfDestroy() 
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }


}
