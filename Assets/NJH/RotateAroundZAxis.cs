using UnityEngine;

public class RotateAroundZAxis : MonoBehaviour
{
    public float rotationSpeed = 100f; // ת���ٶȣ���λ����/��

    void Update()
    {
        // �� Z ����ת
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
