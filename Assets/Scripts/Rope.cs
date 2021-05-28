using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField]
    private Transform  pointA,
                       pointB,
                       pointStand;

    private GameObject player;
    private Collider collider;
    
    private float speed = 10;
    private Vector3 startPos,
                    endPos,
                    standPos; //позиция призимления

    private bool isMoving = false,
                 isEnding = false;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }
    void Start()
    {
        startPos = pointA.position;
        endPos = pointB.position;
        standPos = pointStand.position;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;

        player = other.gameObject;
        player.GetComponent<CameraMove>().enabled=false;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        StartMove();
    }

    public void StartMove()
    {
        collider.enabled = false;
        player.transform.position = startPos;
        isMoving = true;
    }

    public void EndMove()
    {
        isEnding = true;
    }

    private IEnumerator Deleay() 
    {
        yield return new WaitForSeconds(.5f);
        
    }

    void Update()
    {
        if (isEnding) 
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, standPos, speed * Time.deltaTime);
            if (player.transform.position == standPos) 
            {
                isEnding = false;
                player.GetComponent<CameraMove>().enabled = true;
            }
        }


        if (!isMoving)
            return;

        float step = speed * Time.deltaTime;
        player.transform.position = Vector3.MoveTowards(player.transform.position, endPos, step);

        if (player.transform.position == endPos) 
        {
            isMoving = false;
            EndMove();
        }
    }
}
