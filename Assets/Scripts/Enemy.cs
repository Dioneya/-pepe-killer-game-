using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private Rigidbody[] rigids;
    private Animator animator;
    private NavMeshAgent agent;
    private bool isAttack = false;
    private bool canMove = true;
    private bool isAgro = false;
    public float 
        distance = 0f,
        maxDist = 4f,
        minDist = 3f,
        distForAttack = 2f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
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


    private IEnumerator Attack() 
    {
        isAttack = true;
        animator.Play("Idle");
        int timer = Random.Range(1, 3);
        StartCoroutine(TimeoutForMove(timer));
        yield return new WaitForSeconds(timer);
        animator.SetBool("isAttack",true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("isAttack", false);
        yield return new WaitForSeconds(1.4f);
        isAttack = false;
        
    }


    private IEnumerator TimeoutForMove(int sec) 
    {
        canMove = false;
        yield return new WaitForSeconds(sec + 2f);
        canMove = true;
    }

    private void Punch() 
    {
        animator.enabled = false;
        agent.enabled = false;
        foreach (Rigidbody rig in rigids)
        {
            rig.AddForce(0, 350f, -600f);
            rig.gameObject.tag = "Throw";
        }
        gameObject.tag = "Throw";
        RemoveCollision();
    }

    private void Hit() 
    {
        animator.enabled = false;
        agent.enabled = false;
        gameObject.tag = "Throw";
        RemoveCollision();
        
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

    public void SetAgressive() 
    {
        isAgro = true;
        agent.enabled = true;
        agent.destination = target.transform.position;
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);

        if (isAttack)
            return;

        if (distance > maxDist && !isAgro) 
        {
            agent.enabled = false;
            animator.Play("Idle");
        }

        if (canMove && distance < maxDist && distance>minDist)
        {
            agent.enabled = true;
            agent.destination = target.transform.position;
            animator.Play("Walk");
        }

        if (distance <= distForAttack)
        {
            agent.enabled = false;
            StartCoroutine(Attack());
        }
    }
}
