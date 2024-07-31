using UnityEngine;

public class WayPoint : MonoBehaviour
{
    private Stage _parentStage;

    private void Start()
    {
        _parentStage = GetComponentInParent<Stage>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player != null)
        {
            player.StopAtPoint(_parentStage);
        }
    }
}