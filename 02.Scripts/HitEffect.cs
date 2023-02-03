using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            var colPosition = collision.transform.position;
            PoolManager.Instance.Put(collision.gameObject);
            SoundManager.Instance.SfxAudio.Play(Sfx.BulletHit);

            var spark = PoolManager.Instance.Get(Prefab.Spark);
            spark.transform.position = colPosition;
            spark.transform.rotation = Quaternion.identity;
            spark.gameObject.SetActive(true);
        }
    }
}
