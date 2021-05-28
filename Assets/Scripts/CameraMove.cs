using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Joystick joystick;

    [SerializeField]
    private float _moveSpeed = 50f;
    private Rigidbody rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        Move();
        //MobileMove();
    }

    void Move()
    {

        float shiftMult = 1f;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            shiftMult = 3f;
        }

        float right = Input.GetAxis("Horizontal");
        float forward = Input.GetAxis("Vertical");
        float up = 0;

        Vector3 offset = new Vector3(-right, up, -forward) * _moveSpeed * shiftMult * Time.deltaTime;
        rigid.velocity=offset;
        //transform.Translate(offset);
    }

    void MobileMove() 
    {

        float right = joystick.Horizontal;
        float forward = joystick.Vertical;
        float up = 0;

        Vector3 offset = new Vector3(-right, up, -forward) * _moveSpeed * Time.deltaTime;
        rigid.velocity = offset;
        //transform.Translate(offset);
    }
}


