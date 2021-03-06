﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Joystick joystick;
    public float joystickHorizontalSensitivity;
    public float joystickVerticalSensitivity;
    public float horizontalForce;
    public float verticalForce;
    public Transform spawnPoint;

    private Rigidbody2D m_rigidBody2D;
    private SpriteRenderer m_spriteRenderer;
    private Animator m_animator;

    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody2D = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();   
    }
    void _Move()
    {
        if (isGrounded)
        {
            if (joystick.Horizontal > joystickHorizontalSensitivity)
            {
                //right
                m_rigidBody2D.AddForce(Vector2.right * horizontalForce * Time.deltaTime);
                m_spriteRenderer.flipX = false;
                m_animator.SetInteger("AnimState", 1);
            }
            else if (joystick.Horizontal < -joystickHorizontalSensitivity)
            {
                //left
                m_rigidBody2D.AddForce(Vector2.left * horizontalForce * Time.deltaTime);
                m_spriteRenderer.flipX = true;
                m_animator.SetInteger("AnimState", 1);
            }
            else if (joystick.Vertical > joystickVerticalSensitivity)
            {
                //jump
                m_rigidBody2D.AddForce(Vector2.up * verticalForce * Time.deltaTime);
                m_animator.SetInteger("AnimState", 2);
            }
            else
            {
                //idle
                m_animator.SetInteger("AnimState", 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("DeathPlane"))
            transform.position = spawnPoint.position;
    }
}
