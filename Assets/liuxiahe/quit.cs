using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToChangeScene : MonoBehaviour
{
    // ��������
    public string sceneName = "Scene1";

    void Update()
    {
        // ��������
        if (Input.GetMouseButtonDown(0)) // 0 ��ʾ������
        {
            // ����һ��������������λ�õ�����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ��������Ƿ��������
            if (Physics.Raycast(ray, out hit))
            {
                // �����е������Ƿ��ǵ�ǰ���ؽű�������
                if (hit.transform == transform)
                {
                    // ���س���
                    SceneManager.LoadScene(sceneName);
                }
            }
        }
    }
}
