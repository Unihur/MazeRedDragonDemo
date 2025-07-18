using UnityEngine;

public class MoveObjectZAxis : MonoBehaviour
{
    // �ƶ����ٶ�
    public float speed = 2.0f;

    // Z���ƶ��ķ�Χ
    public float zRange = 5.0f;

    // ��ʼλ��
    private Vector3 startPosition;

    void Start()
    {
        // ��¼�����ʼλ��
        startPosition = transform.position;
    }

    void Update()
    {
        // ����Z���ϵ�ƫ������ʹ�����Һ���ʵ��ƽ�������ƶ�
        float zOffset = Mathf.Sin(Time.time * speed) * zRange;

        // ���������λ��
        transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z + zOffset);
    }
}
