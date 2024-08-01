using UnityEngine;

public class WayPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player != null)
        {
            player.StopAtPoint();
        }
    }
}