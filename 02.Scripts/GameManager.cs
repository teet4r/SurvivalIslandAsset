using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 적 생성 로직
/// 1. 태어날 위치
/// 2. 좀비 프리팹
/// 3. 몇 초 간격으로 스폰?
/// 4. 몇 마리 스폰?
/// </summary>
public class GameManager : MonoBehaviour, ICustomUpdate
{
    public static GameManager instance = null;

    public Transform[] points;
    public int total { get; private set; } = 0;

    [SerializeField][Range(0.1f, 10f)]
    float responTime;
    float timePrev;
    [SerializeField] Text killText;
    
    [SerializeField] int zombieMaxCnt;
    int _zombieCurCnt = 0;
    [SerializeField] int monsterMaxCnt;
    int _monsterCurCnt = 0;
    [SerializeField] int skeletonMaxCnt;
    int _skeletonCurCnt = 0;

    bool _isGameOver = false;

    void OnEnable()
    {
        RegisterCustomUpdate();
    }
    void Start()
    {
        if (instance == null)
            instance = this;

        timePrev = Time.time;
    }
    void OnDisable()
    {
        DeregisterCustomUpdate();
    }

    public void CustomUpdate()
    {
        if (_isGameOver) return;

        if (Time.time - timePrev > responTime)
        {
            timePrev = Time.time;
            if (_zombieCurCnt < zombieMaxCnt)
                CreateObject(Prefab.Zombie, ref _zombieCurCnt);
            if (_monsterCurCnt < monsterMaxCnt)
                CreateObject(Prefab.Monster, ref _monsterCurCnt);
            if (_skeletonCurCnt < skeletonMaxCnt)
                CreateObject(Prefab.Skeleton, ref _skeletonCurCnt);
        }

        if (total >= zombieMaxCnt + monsterMaxCnt + skeletonMaxCnt)
        {
            _isGameOver = true;
            Invoke("LoadNextScene", 3f);
        }
    }
    public void KillCount(int count)
    {
        total += count;
        killText.text = $"Kills: <color=#ff0000>{total}</color>";
    }
    void CreateObject(Prefab type, ref int count)
    {
        int idx = Random.Range(0, points.Length);
        var obj = PoolManager.Instance.Get(type);
        obj.transform.position = points[idx].position;
        obj.gameObject.SetActive(true);
        count++;
    }
    void LoadNextScene()
    {
        SceneManager.LoadScene("EndScene");
    }

    public void RegisterCustomUpdate()
    {
        CustomUpdateManager.Instance.RegisterCustomUpdate(this);
    }
    public void DeregisterCustomUpdate()
    {
        CustomUpdateManager.Instance.DeregisterCustomUpdate(this);
    }
}
