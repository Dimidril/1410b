using UnityEngine;

public class KillSphere : MonoBehaviour
{

[SerializeField] private InvisiblePoint MovePoint;
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Enemy>())
        {
           MovePoint.Die();
        }
    }
}
