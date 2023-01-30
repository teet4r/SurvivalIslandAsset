using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDamaged : MonoBehaviour
{
    [SerializeField] Image hpBar;
    [SerializeField] Text hpText;
    [SerializeField][Min(1f)] int maxHp;
    [SerializeField] GameObject screenPanel;

    public bool IsDie { get; private set; } = false;
    public int CurHp { get; private set; }

    void OnEnable()
    {
        CurHp = maxHp;
        hpText.text = $"{CurHp} / {maxHp}";
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Punch"))
        {
            EnemyPunch enemyPunch = other.gameObject.GetComponent<EnemyPunch>();
            CurHp -= enemyPunch.GetDamage();
            if (CurHp <= 0 && !IsDie)
            {
                IsDie = true;
                CurHp = 0;
                screenPanel.SetActive(true);
                Invoke("LoadNextScene", 3f);
            }
            hpBar.fillAmount = (float)CurHp / maxHp;
            hpBar.color = new Color(1 - hpBar.fillAmount, hpBar.fillAmount, 0f);
            hpText.text = $"{CurHp} / {maxHp}";
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("EndScene");
    }
}
