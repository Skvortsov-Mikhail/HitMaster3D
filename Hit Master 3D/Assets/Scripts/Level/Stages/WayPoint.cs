using UnityEngine;

public class WayPoint : MonoBehaviour
{
    private Stage _parentStage;

    private void Awake()
    {
        _parentStage = GetComponentInParent<Stage>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (_parentStage.IsStageClear == true)
            {
                player.GoToNextWayPoint();
            }
            else
            {
                player.StopAtPoint();
            }
        }
    }
}