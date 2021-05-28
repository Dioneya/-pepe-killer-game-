using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private Rigidbody[] rigids;
    [SerializeField]
    private Scrollbar healBar;
    private Animator animator;
    private bool isAttack = false;
    [SerializeField]
    private GameObject item,itemPos;
    private bool isAgro = false;
    public int hp = 3;
    public float
        distance = 0f,
        maxDist = 9f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        SwitchKinematicMode(true);
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

        if (other.gameObject.tag != "Throw")
            return;

        other.gameObject.tag = "Untagged";
        hp -= 1;
        healBar.size -= 0.2f;
        if (hp <= 0) 
        {
            SwitchKinematicMode(false);
            healBar.gameObject.SetActive(false);
            Hit();
        }

    }

    public void ShowHealBar() 
    {
        healBar.gameObject.SetActive(true);
    }

    private void Hit()
    {
        animator.enabled = false;
        foreach (Rigidbody rig in rigids)
        {
            rig.AddForce(0, 20f, -10f);
            rig.gameObject.tag = "Throw";
        }
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


    IEnumerator Timer() 
    {
        isAttack = true;
        animator.Play("Strike");
        yield return new WaitForSeconds(3f);
        isAttack = false;
    }

    public void Strike() 
    {
        GameObject itemStr = Instantiate(item,itemPos.transform.position,new Quaternion(0,0,0,0));
        itemStr.GetComponent<Rigidbody>().AddForce(0,0,1000f);
    }

    public void SetAgressive() 
    {
        isAgro = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttack || !isAgro)
            return;

        StartCoroutine(Timer());
    }
}
