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
            SoundManager.Instance.SfxAudio.Play("BulletHit");

            var spark = PoolManager.Instance.Get("Spark");
            spark.transform.position = colPosition;
            spark.transform.rotation = Quaternion.identity;
            spark.SetActive(true);
        }
    }
}
