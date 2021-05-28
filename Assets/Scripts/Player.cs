using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Joystick joystick;

    [SerializeField] 
    private BoxCollider footCollider;
    private Animator animator;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        joystick.OnJoystickUp.AddListener(Kick);
    }
    public void Kick() 
    {
        animator.Play("Kick");
        footCollider.enabled = true;
        Debug.Log("Коллайдер заработал");
        StartCoroutine(HideCollider());
    }

    IEnumerator HideCollider() 
    {
        yield return new WaitForSeconds(0.2f);
        footCollider.enabled = false;
        Debug.Log("Коллайдер спрячен");
    }
    // Update is called once per frame
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Kick();
        }
    }
}
