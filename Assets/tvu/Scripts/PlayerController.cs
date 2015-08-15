using System;
using UnityEngine;

namespace tvu
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    class PlayerController : MonoBehaviour
    {

        [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.

        private AudioSource audioSource;
        private Rigidbody m_rigidBody;
        private CapsuleCollider m_collider;

        
        public float Speed = 5.0f;
        private Vector3 MoveDirection = Vector3.zero;

        private bool isJetting = false;
        public float jumpSpeed = 500.0f;
        private Vector3 jumpDirection = Vector3.zero;
        private bool isGrounded = false;
        private bool isJumping = false;
        private bool isInAir = false;
        public float airControl = 0.5f;

        public float defaultFriction = 1f;
        public float skiFriction = 0.15f;

        private bool previouslyGrounded;
        
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            m_rigidBody   = GetComponent<Rigidbody>();
            m_collider = GetComponent<CapsuleCollider>();
            setFriction(false);
        }

        private void Update()
        {
            isJetting = Input.GetButton("Jet");
            //if (!previouslyGrounded && isGrounded)
            //{
                //StartCoroutine(m_JumpBob.DoBobCycle());
            //    PlayLandingSound();
            //}


            //previouslyGrounded = isGrounded;
        }

        private void FixedUpdate()
        {
            /**if (Physics.Raycast(transform.position, -transform.up, m_collider.height / 2 + 2))
            {
                isGrounded = true;
                isJumping  = false;
                isInAir    = false;
            }
            else if (!isInAir)
            {
                isInAir = true;
                jumpDirection = MoveDirection;
            }*/

            if (Input.GetButtonDown("Ski"))
            {
                setFriction(true);
            }
            else if (Input.GetButtonUp("Ski"))
            {
                setFriction(false);
            }
            if (isJetting)
            {
                m_rigidBody.AddForce(0, 10, 0);
            }
            Movement();
        }


        private void Movement (){
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                MoveDirection = new Vector3(Input.GetAxisRaw("Horizontal"),MoveDirection.y, Input.GetAxisRaw("Vertical"));
            this.transform.Translate((MoveDirection.normalized * Speed) * Time.deltaTime);
            transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * 250.0f);
        }

        private void OnCollisionEnter(Collision collisionInfo)
        {
            //Debug.Log("Hello collision " + collisionInfo);
        }

        private void OnCollisionExit(Collision collisionInfo){
            //Debug.Log("Bye collision " + collisionInfo);
        }

        //private void PlayLandingSound()
        //{
        //    audioSource.clip = m_LandSound;
        //    audioSource.Play();
            //m_NextStep = m_StepCycle + .5f;
        //}

        private void setFriction(bool isSkiing)
        {
            if (isSkiing)
            {
                m_collider.material.staticFriction = skiFriction;
                m_collider.material.dynamicFriction = skiFriction;
                m_collider.material.frictionCombine = PhysicMaterialCombine.Average;
            }
            else
            {
                m_collider.material.staticFriction = defaultFriction;
                m_collider.material.dynamicFriction = defaultFriction;
                m_collider.material.frictionCombine = PhysicMaterialCombine.Maximum;
            }
        }
    }
}
