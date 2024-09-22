using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicCharacterMovement : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rb2d;
    private ContactFilter2D _platform;

    public float horizontalSpeed = 1, jumpForce = 20;

    private bool _isOnPlatform => _rb2d.IsTouching(_platform);

    private bool _facingRight = true;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");

        if (moveHorizontal != 0)
            _animator.SetBool("Horizontal", true);
        else
            _animator.SetBool("Horizontal", false);

        _rb2d.velocity = new Vector2(moveHorizontal * horizontalSpeed, _rb2d.velocity.y);

        if (moveHorizontal > 0)
        {
            _facingRight = true;
        }
        if (moveHorizontal < 0)
        {
            _facingRight = false;
        }

        //if (_facingRight && transform.localScale.x == -1 || !_facingRight && transform.localScale.x == 1)
        //{
        //    print(transform.localScale.x);
        //    flip();
        //}
        print(transform.eulerAngles.y);
        if (_facingRight && Mathf.Abs(transform.eulerAngles.y) >= 179.001f || !_facingRight && transform.eulerAngles.y < 0.001f)
        {
            //print(transform.localRotation.y);
            flip();
        }
    }

    public void flip()
    {
        if (!_facingRight)
        {
            transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            transform.Rotate(new Vector3(0, 180, 0));
            //transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            transform.Rotate(new Vector3(0, 180, 0));
            //transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void FixedUpdate()
    {
        if (_isOnPlatform)
        {
            _animator.SetFloat("Vertical", 0);
        }
        else
        {
            _animator.SetFloat("Vertical", 1);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (_isOnPlatform)
            {
                _rb2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                //_rb2d.AddForce(new Vector2(0, jumpForce));
            }
        }

        //Vector3 horizontal = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
        //transform.position = speed * transform.position + horizontal * Time.deltaTime;

    }
}
