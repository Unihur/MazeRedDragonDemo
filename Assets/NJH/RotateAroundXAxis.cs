using UnityEngine;

public class RotateAroundXAxis : MonoBehaviour
{
    public float rotationSpeed = 100f; // ת���ٶȣ���λ����/��

    void Update()
    {
        // �� X ����ת
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
}
