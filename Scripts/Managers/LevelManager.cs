using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private GameObject[] levels;
    [SerializeField] private RotateArrow arrow;
    
    private int levelCount = 0;

    public int LevelCount => levelCount;
    private void Awake()
    {
        instance = this;
    }
    public void FinishLevel()
    {
        levels[levelCount].SetActive(false);
        levelCount++;
        if(levelCount % 3 == 0) { levelCount = 0; }
        levels[levelCount].SetActive(true);

        arrow.ChangeNormalButtonText();
    }
    public void OpenLevel()
    {
        PlayerMovement.instance.IsRunning = true;
        PlayerMovement.instance.ChangeAnimation("Run");
    }
}
