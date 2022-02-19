using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public delegate void PlayerEvent();
    public PlayerEvent BonusActivate;

    public Action<Vector3> OnPlayerUpperFloor;
    [SerializeField]
    private AudioSource audio = null;

    [SerializeField]
    private Vector3 jumpVector = new Vector3(0.0f, 2.0f, 0.0f);
    [SerializeField]
    private Vector3 fallVector = new Vector3(1.0f, 0.0f, 0.0f);
    [SerializeField]
    private float jumpForce = 2.0f;
    [SerializeField]
    private float fallForce = 2.0f;
    [SerializeField]
    private Rigidbody rb = null;
    //[SerializeField]
    //private Animator animator = null;

    private bool isGrounded;
    private bool hasJumped;

    private void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        //if (animator == null)
        //    animator = GetComponentInChildren<Animator>();

        rb.useGravity = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)|| Input.touchCount > 0) && isGrounded)
        {
            rb.useGravity = true;
            rb.AddForce(jumpVector * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

	private void OnTriggerEnter(Collider collisionInfo)
	{
        
        BrickBehaviour brick = collisionInfo.GetComponentInParent<BrickBehaviour>();
        
        if (brick != null)
        {
            float yDistance = transform.position.y - brick.transform.position.y;
			if (yDistance < brick.transform.localScale.y / 2)
			{
				rb.AddForce(fallVector * fallForce, ForceMode.Impulse);
				rb.constraints = RigidbodyConstraints.None;
			}
		}


    }
	private void OnCollisionStay(Collision collisionInfo)
    {
        isGrounded = true;
        
        BrickBehaviour brick = collisionInfo.collider.GetComponent<BrickBehaviour>();
        if (brick != null && hasJumped && rb.constraints != RigidbodyConstraints.None)
        {

            OnPlayerUpperFloor?.Invoke(transform.position);
           
            hasJumped = false;
        }

    }
    private void OnCollisionExit()
    {
        isGrounded = false;
        hasJumped = true;
       
    }
}
