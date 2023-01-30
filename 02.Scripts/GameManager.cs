using UnityEngine;
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
        if (Time.time - timePrev > responTime)
        {
            timePrev = Time.time;
            if (_zombieCurCnt < zombieMaxCnt)
                CreateObject("Zombie", ref _zombieCurCnt);
            if (_monsterCurCnt < monsterMaxCnt)
                CreateObject("Monster", ref _monsterCurCnt);
            if (_skeletonCurCnt < skeletonMaxCnt)
                CreateObject("Skeleton", ref _skeletonCurCnt);
        }
    }
    void CreateObject(string prefabName, ref int count)
    {
        int idx = Random.Range(0, points.Length);
        var obj = PoolManager.Instance.Get(prefabName);
        obj.transform.position = points[idx].position;
        obj.SetActive(true);
        count++;
    }
    public void KillCount(int count)
    {
        total += count;
        killText.text = $"Kills: <color=#ff0000>{total}</color>";
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
