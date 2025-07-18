using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioClip bgm1; // 第一和第二场景使用的音乐
    public AudioClip bgm2; // 第三个场景的音乐
    public AudioClip bgm3; // 第四及之后场景的音乐

    private AudioSource audioSource;

    private float lastAdjustTime = 0f; // 上一次调整音量的时间
    private float adjustInterval = 0.1f; // 调整音量的时间间隔（秒）

    void Awake()
    {
        // 确保只存在一个 MusicManager 实例
        if (FindObjectsOfType<MusicManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        // 保持音乐管理器在场景切换时不销毁
        DontDestroyOnLoad(gameObject);

        // 初始化 AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        // 订阅场景切换事件
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        // 初始播放 BGM1
        PlayBGM(bgm1);
        SetVolume(0.4f); // 设置默认音量为 40%
    }

    void Update()
    {
        // 当前时间
        float currentTime = Time.time;

        // 如果按键被按下并且超过调整间隔(上下键调整音量)
        if (Input.GetKey(KeyCode.UpArrow) && currentTime - lastAdjustTime > adjustInterval)
        {
            SetVolume(audioSource.volume + 0.01f); // 增加音量
            lastAdjustTime = currentTime; // 更新上一次调整时间
        }
        else if (Input.GetKey(KeyCode.DownArrow) && currentTime - lastAdjustTime > adjustInterval)
        {
            SetVolume(audioSource.volume - 0.01f); // 减少音量
            lastAdjustTime = currentTime; // 更新上一次调整时间
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 获取当前场景的名称和索引
        string sceneName = scene.name;
        int sceneIndex = scene.buildIndex;

        // 如果场景名是 "end"，直接返回，不切换背景音乐
        if (sceneName == "endnjh")
        {
            return;
        }

        // 根据场景索引或名称切换音乐
        if (sceneIndex == 0 || sceneIndex == 1) // 第一和第二场景
        {
            PlayBGM(bgm1);
        }
        else if (sceneIndex == 2) // 第三个场景
        {
            PlayBGM(bgm2);
        }
        else if (sceneIndex >= 3) // 第四个及之后场景
        {
            PlayBGM(bgm3);
        }
    }

    void PlayBGM(AudioClip bgm)
    {
        if (audioSource.clip == bgm) return; // 如果当前正在播放相同的 BGM，则不切换

        audioSource.clip = bgm;
        audioSource.Play();
    }

    // 音量控制方法
    public void SetVolume(float volume)
    {
        // 确保音量在 0 到 1 之间
        audioSource.volume = Mathf.Clamp(volume, 0f, 1f);

        // 可选：打印当前音量到控制台，方便调试
        Debug.Log($"当前音量: {audioSource.volume}");
    }
}
