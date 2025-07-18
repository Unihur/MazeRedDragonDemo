using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioClip bgm1; // ��һ�͵ڶ�����ʹ�õ�����
    public AudioClip bgm2; // ����������������
    public AudioClip bgm3; // ���ļ�֮�󳡾�������

    private AudioSource audioSource;

    private float lastAdjustTime = 0f; // ��һ�ε���������ʱ��
    private float adjustInterval = 0.1f; // ����������ʱ�������룩

    void Awake()
    {
        // ȷ��ֻ����һ�� MusicManager ʵ��
        if (FindObjectsOfType<MusicManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        // �������ֹ������ڳ����л�ʱ������
        DontDestroyOnLoad(gameObject);

        // ��ʼ�� AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        // ���ĳ����л��¼�
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        // ��ʼ���� BGM1
        PlayBGM(bgm1);
        SetVolume(0.4f); // ����Ĭ������Ϊ 40%
    }

    void Update()
    {
        // ��ǰʱ��
        float currentTime = Time.time;

        // ������������²��ҳ����������(���¼���������)
        if (Input.GetKey(KeyCode.UpArrow) && currentTime - lastAdjustTime > adjustInterval)
        {
            SetVolume(audioSource.volume + 0.01f); // ��������
            lastAdjustTime = currentTime; // ������һ�ε���ʱ��
        }
        else if (Input.GetKey(KeyCode.DownArrow) && currentTime - lastAdjustTime > adjustInterval)
        {
            SetVolume(audioSource.volume - 0.01f); // ��������
            lastAdjustTime = currentTime; // ������һ�ε���ʱ��
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ��ȡ��ǰ���������ƺ�����
        string sceneName = scene.name;
        int sceneIndex = scene.buildIndex;

        // ����������� "end"��ֱ�ӷ��أ����л���������
        if (sceneName == "endnjh")
        {
            return;
        }

        // ���ݳ��������������л�����
        if (sceneIndex == 0 || sceneIndex == 1) // ��һ�͵ڶ�����
        {
            PlayBGM(bgm1);
        }
        else if (sceneIndex == 2) // ����������
        {
            PlayBGM(bgm2);
        }
        else if (sceneIndex >= 3) // ���ĸ���֮�󳡾�
        {
            PlayBGM(bgm3);
        }
    }

    void PlayBGM(AudioClip bgm)
    {
        if (audioSource.clip == bgm) return; // �����ǰ���ڲ�����ͬ�� BGM�����л�

        audioSource.clip = bgm;
        audioSource.Play();
    }

    // �������Ʒ���
    public void SetVolume(float volume)
    {
        // ȷ�������� 0 �� 1 ֮��
        audioSource.volume = Mathf.Clamp(volume, 0f, 1f);

        // ��ѡ����ӡ��ǰ����������̨���������
        Debug.Log($"��ǰ����: {audioSource.volume}");
    }
}
