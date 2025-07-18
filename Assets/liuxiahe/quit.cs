using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToChangeScene : MonoBehaviour
{
    // 场景名称
    public string sceneName = "Scene1";

    void Update()
    {
        // 检测鼠标点击
        if (Input.GetMouseButtonDown(0)) // 0 表示左键点击
        {
            // 创建一条从摄像机到鼠标位置的射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 检测射线是否击中物体
            if (Physics.Raycast(ray, out hit))
            {
                // 检查击中的物体是否是当前挂载脚本的物体
                if (hit.transform == transform)
                {
                    // 加载场景
                    SceneManager.LoadScene(sceneName);
                }
            }
        }
    }
}
