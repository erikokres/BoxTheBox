using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DS.BoxTheBox.Player
{
    public class PlayerMovement : MonoBehaviour
    {
    #region VARIABLE
        [SerializeField] private float speed = 5f;
        [SerializeField] private float jumpingPower = 16f;
        private float horizontal;
        private bool isFacingRight = true;

        [SerializeField] private GameObject bullet;
        [SerializeField] private Transform bulletDirection;
        [SerializeField] private float bulletSpeed;

        private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundLayer;

    #endregion

    #region UNITY FUNCTION
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            _rigidbody.velocity = new Vector2(horizontal * speed, _rigidbody.velocity.y);

            if(!isFacingRight && horizontal > 0f)
            {
                Flip();
            }
            else if (isFacingRight && horizontal < 0f)
            {
                Flip();
            }

        }

        
    #endregion

    #region PUBLIC FUNCTION
        // input ketika bergerak moving
        public void Move(InputAction.CallbackContext context)
        {
            horizontal = context.ReadValue<Vector2>().x;
            
            // Debug.Log(context.ReadValue<Vector2>());
        }

        // input ketika lompat
        public void Jump(InputAction.CallbackContext context)
        {
            // ketika tombol lompat ditekan ditanah
            if (context.performed && IsGrounded())
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpingPower);
               
            }


            // ketika tombol lompat dilepas
            if (context.canceled && _rigidbody.velocity.y > 0f)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
            }

        }

        public void Shoot(InputAction.CallbackContext context){
            if(context.performed){
            // GameObject g = Instantiate(bullet, bulletDirection.position, bulletDirection.rotation);
            // g.SetActive(true);
            GameObject Bullet = PoolingObject.instance.GetPooledObject();

            if(Bullet != null){
                Bullet.transform.position = bulletDirection.position;
                Bullet.transform.rotation = bulletDirection.rotation;
                Bullet.SetActive(true);
            }
            }
        }
    #endregion

    #region PRIVATE FUNCTION
        private void Flip()
        {
            isFacingRight = !isFacingRight;
            // Vector3 localScale = transform.localScale;
            // localScale.x *= -1f;
            // transform.localScale = localScale;
            transform.Rotate(0f, 180f, 0f);
        }

        private bool IsGrounded()
        {
            return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        }

        
    #endregion
    }
}
