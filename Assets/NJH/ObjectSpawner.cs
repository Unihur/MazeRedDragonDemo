using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // Ҫ���ɵ�����
    public Transform spawnPoint; // ����λ�ã���ѡ��
    public float spawnInterval = 2f; // ���ɼ��ʱ�䣨�룩
    public float destroyAfterSeconds = 5f; // ���ɵ������ڶ����������

    private void Start()
    {
        // ѭ���������ɷ���
        InvokeRepeating("SpawnObject", 0f, spawnInterval);
    }

    private void SpawnObject()
    {
        // ���û��ָ������λ�ã���ʹ�õ�ǰ�ű����������λ��
        Vector3 position = spawnPoint ? spawnPoint.position : transform.position;

        // ��������
        GameObject spawnedObject = Instantiate(objectToSpawn, position, Quaternion.identity);

        // 5����������ɵ�����
        Destroy(spawnedObject, destroyAfterSeconds);

  
    }
}
