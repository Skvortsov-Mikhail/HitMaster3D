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
        var player = other.GetComponent<Player>();

        if (player != null)
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