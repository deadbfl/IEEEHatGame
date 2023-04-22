using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GateType : MonoBehaviour
{
    public TypeOfGate currentGateType;

    [SerializeField] private TMP_Text valueText; 

    [SerializeField] private int value;

    [SerializeField] private Material[] material;

    [SerializeField] private GameObject buddy;
    private void Awake()
    {
        if (currentGateType == TypeOfGate.Sum)
            OpenGate(0,"+");
        else if (currentGateType == TypeOfGate.Extraction)
            OpenGate(1, "-");
        else if (currentGateType == TypeOfGate.Multiply)
            OpenGate(0, "x");
        else
            OpenGate(1, "/");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerDetect(other.gameObject);
            CloseGate();
        }
    }
    private void PlayerDetect(GameObject other)
    {
        other.GetComponent<CalculateHatCount>().ChangeHatCount(value, currentGateType);
    }
    public void OpenGate(int index,string text)
    {
        transform.parent.GetComponent<Renderer>().material = material[index];
        valueText.SetText(text + value);
    }
    private void CloseGate()
    {
        transform.parent.parent.gameObject.SetActive(false);
        buddy.SetActive(false);
    }
}
public enum TypeOfGate
{
    Sum,
    Extraction,
    Multiply,
    Divide,
}
