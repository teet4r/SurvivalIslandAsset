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

    public bool isDie { get; private set; } = false;
    public int curHp { get; private set; }

    void OnEnable()
    {
        curHp = maxHp;
        hpText.text = $"{curHp} / {maxHp}";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Punch"))
        {
            EnemyPunch enemyPunch = other.gameObject.GetComponent<EnemyPunch>();
            curHp -= enemyPunch.GetDamage();
            if (curHp <= 0 && !isDie)
            {
                isDie = true;
                curHp = 0;
                screenPanel.SetActive(true);
                Invoke("LoadNextScene", 3f);
            }
            hpBar.fillAmount = (float)curHp / maxHp;
            hpBar.color = new Color(1 - hpBar.fillAmount, hpBar.fillAmount, 0f);
            hpText.text = $"{curHp} / {maxHp}";
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("EndScene");
    }
}
