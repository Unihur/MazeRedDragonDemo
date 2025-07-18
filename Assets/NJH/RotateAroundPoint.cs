using UnityEngine;

public class RotateAroundPoint : MonoBehaviour
{
    public Vector3 pivotPoint = Vector3.zero; // ��ת���ĵ㣨���������ı������꣩
    public Vector3 rotationAxis = Vector3.up; // ��ת�ᣨĬ��Y�ᣩ
    public float rotationSpeed = 50f;         // ��ת�ٶ�

    void Update()
    {
        // ��������ռ��µ���ת����
        Vector3 worldPivot = transform.TransformPoint(pivotPoint);

        // Χ����ת���ĵ���ת
        transform.RotateAround(worldPivot, rotationAxis, -rotationSpeed * Time.deltaTime);
    }

    // Gizmos ���ӻ�����ʾ��ת����
    private void OnDrawGizmos()
    {
        // ����ת���ĵ���ʾΪһ����ɫС��
        Gizmos.color = Color.red;
        Vector3 worldPivot = transform.TransformPoint(pivotPoint);
        Gizmos.DrawSphere(worldPivot, 0.1f);
    }
}
