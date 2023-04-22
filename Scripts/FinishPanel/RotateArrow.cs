using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RotateArrow : MonoBehaviour
{
    [SerializeField] private Vector3 rotationAngle;
    
    [SerializeField] private TMP_Text bonusText;
    [SerializeField] private TMP_Text normalText;

    private int bonusMoney;
    
    private float rotationDirection = 1;

    private int bonus = 1;

    private int hatCount => PlayerMovement.instance.HatCount;

    private void FixedUpdate()
    {
        if (40 < transform.eulerAngles.z && transform.eulerAngles.z < 180)
            rotationDirection = -1;
        else if (180 < transform.eulerAngles.z && transform.eulerAngles.z < 315)
            rotationDirection = 1;

        if (30 < transform.eulerAngles.z && transform.eulerAngles.z < 40)
            bonus = 2;
        else if (10 < transform.eulerAngles.z && transform.eulerAngles.z <= 30)
            bonus = 3;
        else if (0 <= transform.eulerAngles.z && transform.eulerAngles.z <= 10
                || 350 < transform.eulerAngles.z && transform.eulerAngles.z < 360)
            bonus = 5;
        else if (329 < transform.eulerAngles.z && transform.eulerAngles.z <= 350)
            bonus = 3;
        else if (315 < transform.eulerAngles.z && transform.eulerAngles.z <= 329)
            bonus = 2;

        ChangeBonusMoneyValue();
    }
    private void Update()
    {
        transform.Rotate(rotationDirection * Time.deltaTime * rotationAngle);
    }
    private void ChangeBonusMoneyValue()
    {
        bonusText.SetText((bonusMoney*bonus).ToString());
    }
    public void ChangeNormalButtonText()
    {
        bonusMoney = hatCount * 4;
        normalText.SetText(bonusMoney.ToString());
    }
}
