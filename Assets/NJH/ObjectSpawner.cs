using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // 要生成的物体
    public Transform spawnPoint; // 生成位置（可选）
    public float spawnInterval = 2f; // 生成间隔时间（秒）
    public float destroyAfterSeconds = 5f; // 生成的物体在多少秒后销毁

    private void Start()
    {
        // 循环调用生成方法
        InvokeRepeating("SpawnObject", 0f, spawnInterval);
    }

    private void SpawnObject()
    {
        // 如果没有指定生成位置，则使用当前脚本所在物体的位置
        Vector3 position = spawnPoint ? spawnPoint.position : transform.position;

        // 生成物体
        GameObject spawnedObject = Instantiate(objectToSpawn, position, Quaternion.identity);

        // 5秒后销毁生成的物体
        Destroy(spawnedObject, destroyAfterSeconds);

  
    }
}
