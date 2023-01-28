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
    [SerializeField] int monsterMaxCnt;
    [SerializeField] int skeletonMaxCnt;

    void OnEnable()
    {
        RegisterCustomUpdate();
    }

    void OnDisable()
    {
        DeregisterCustomUpdate();
    }

    void Start()
    {
        if (instance == null)
            instance = this;

        timePrev = Time.time;
    }

    public void CustomUpdate()
    {
        if (Time.time - timePrev > responTime)
        {
            timePrev = Time.time;
            if (GameObject.FindGameObjectsWithTag("Zombie").Length < zombieMaxCnt)
                CreateObject("Zombie");
            if (GameObject.FindGameObjectsWithTag("Monster").Length < monsterMaxCnt)
                CreateObject("Monster");
            if (GameObject.FindGameObjectsWithTag("Skeleton").Length < skeletonMaxCnt)
                CreateObject("Skeleton");
        }
    }

    void CreateObject(string prefabName)
    {
        int idx = Random.Range(0, points.Length);
        var obj = PoolManager.Instance.Get(prefabName);
        obj.transform.position = points[idx].position;
        obj.SetActive(true);
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
