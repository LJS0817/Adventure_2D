using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject BulletPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetBulletObj() { return BulletPrefab; }
}
