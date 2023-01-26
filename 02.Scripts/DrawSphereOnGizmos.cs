using UnityEngine;

public class DrawSphereOnGizmos : MonoBehaviour
{
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var childTransform = transform.GetChild(i);
            Gizmos.DrawSphere(childTransform.position, 1f);
        }
    }
}
