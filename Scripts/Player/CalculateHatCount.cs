using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateHatCount : MonoBehaviour
{
    public static CalculateHatCount instace;
    [SerializeField] private int hatCount;

    [SerializeField] private GameObject hatPrefab;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform hatParent;

    private Stack<GameObject> hatObjects = new Stack<GameObject>();

    public int HatCount => hatCount;
    private void Awake()
    {
        instace = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hat"))
            CollectTheHat(other.gameObject);
        if (other.CompareTag("Finish"))
        {
            PlayerMovement.instance.Finish(hatCount);
            LevelManager.instance.FinishLevel();
        }
    }
    public void ChangeHatCount(int value, TypeOfGate gateType)
    {
        int temp = hatCount;
        if (gateType == TypeOfGate.Sum)
            hatCount += value;
        else if (gateType == TypeOfGate.Extraction)
            hatCount -= value;
        else if (gateType == TypeOfGate.Multiply)
            hatCount *= value;
        else
            hatCount /= value;
        int deltaHatCount = hatCount - temp;
        AddOrRemoveHat(deltaHatCount);
    }
    private void AddOrRemoveHat(int deltaHatCount)
    {
        if (deltaHatCount > 0)
            for (int i = 0; i < deltaHatCount; i++)
                AddHat(spawnPoint);
        else if (deltaHatCount < 0)
            for (int i = 0; i < -1 * deltaHatCount; i++)
                RemoveHat(spawnPoint, false);
    }

    private void AddHat(Transform spawnPoint)
    {
        GameObject obj = Instantiate(hatPrefab);
        obj.transform.position = spawnPoint.position;
        obj.transform.parent = hatParent;

        hatObjects.Push(obj);

        spawnPoint.position += new Vector3(0, .3f, 0);
    }

    private void RemoveHat(Transform spawnPoint, bool finishGame)
    {
        if (hatObjects.Count <= 0) return; 

        GameObject obj = hatObjects.Pop();

        Destroy(obj);

        spawnPoint.position -= new Vector3(0, .3f, 0);
        if (finishGame)
            hatCount--;
    }

    public void ObstaclesHit(int level)
    {
        int tempIndex = hatCount - level;
        print(tempIndex + " " + hatCount + " " + level);
        for (int i = 0; i < tempIndex; i++)
        {
            if (hatObjects.Count <= 0) return;

            GameObject obj = hatObjects.Pop();

            obj.GetComponent<BoxCollider>().isTrigger = false;

            Rigidbody rb = obj.GetComponent<Rigidbody>();

            rb.isKinematic = false;
            rb.useGravity = true;

            byte random = (byte)Random.Range(0, 10);

            int direction = 1;

            if (random < 3)
                direction = -1;


            rb.AddForce(Vector3.forward * (direction * 500));

            obj.transform.parent = null;

            spawnPoint.position -= new Vector3(0, .3f, 0);
        }
    }

    public void CollectTheHat(GameObject obj)
    {
        PoolingSystemManager.instance.OpenObject("SmokeExplosionWhite", obj.transform.position, 1);

        obj.transform.position = spawnPoint.position;
        obj.transform.rotation = Quaternion.Euler(Vector3.zero);
        obj.transform.parent = hatParent;

        hatCount++;

        hatObjects.Push(obj);

        obj.GetComponent<BoxCollider>().isTrigger = true;

        Rigidbody rb = obj.GetComponent<Rigidbody>();

        rb.isKinematic = true;
        rb.useGravity = false;

        spawnPoint.position += new Vector3(0, .3f, 0);
    }
    public void FinishLevel()
    {
        int tempHatCount = hatCount;
        for (int i = 1; i< tempHatCount;i++)
        {
            RemoveHat(spawnPoint, true);
        }
    }
}
