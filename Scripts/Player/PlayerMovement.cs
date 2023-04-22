using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [SerializeField] private Animator anim;
    [SerializeField] JoystickBG joystick;
    [SerializeField] float rotationSpeed = 5f;

    [SerializeField] private Vector2 speed;

    [SerializeField] private Vector2 clampValue;

    [SerializeField] private TMP_Text hatText;

    [SerializeField] private GameObject finishPanel;

    [SerializeField] private Transform particleTransform;

    private int hatCount;

    public float horizontal, vertical;
    public float targetAngle = 0;

    private Vector3 direction;

    private bool isRunning = false;

    public int HatCount => hatCount;
    public bool IsRunning { get => isRunning; set => isRunning = value; }
    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        ToInputAxis();
    }
    private void FixedUpdate()
    {
        ToMove();
    }
    void ToInputAxis()
    {
        horizontal = joystick.result.x;
        vertical = joystick.result.y;
        direction = new Vector3(horizontal, 0, vertical).normalized;
    }
    void ToMove()
    {
        if (!isRunning)  return;
        if (Input.GetMouseButton(0))
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            vertical = Math.Clamp(vertical, 0, 1);

            transform.position = new Vector3(Math.Clamp(transform.position.x + horizontal * speed.x * Time.deltaTime,clampValue.x,clampValue.y), transform.position.y, transform.position.z);

            //transform..rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, targetAngle, 0f), rotationSpeed * Time.deltaTime);
        }
        transform.position += new Vector3(0,0,speed.y * Time.deltaTime); // YAP : burada hep ileriye giedecek yatay ve fikey hiz dengelemesi
    }

    public void ChangeAnimation(string key)
    {
        anim.SetTrigger(key);
    }
    public void Finish(int m_hatCount)
    {
        isRunning = false;
        hatCount = m_hatCount;
        hatText.SetText(m_hatCount.ToString());
        finishPanel.SetActive(true);
        ChangeAnimation("Win");
        PoolingSystemManager.instance.OpenObject("ConfettiBlastRainbow", particleTransform.transform.position, 1);
    }
    public void NextLevel()
    {
        finishPanel.SetActive(false);
        transform.position = new Vector3(0, -0.75f, -14.17f);
        CalculateHatCount.instace.FinishLevel();
    }
}


