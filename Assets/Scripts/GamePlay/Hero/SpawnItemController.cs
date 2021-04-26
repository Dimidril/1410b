using UnityEngine;

public class SpawnItemController : MonoBehaviour
{
    [SerializeField] private float radius = 1f;

    public bool isCanCreate()
    {
        foreach (Collider other in Physics.OverlapSphere(transform.position, radius))
        {
            if (other.GetComponent<StationaryCombat>() && !other.isTrigger) return false;
        }
        return true;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}