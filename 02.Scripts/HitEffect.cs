using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public GameObject spark;
    public AudioSource audioSource;
    public AudioClip hitSound;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            var colPosition = collision.transform.position;
            PoolManager.instance.Put(collision.gameObject);
            audioSource.PlayOneShot(hitSound, 1f);
            GameObject copySpark = Instantiate(spark, colPosition, Quaternion.identity);
            Destroy(copySpark, 2f);
        }
    }
}
