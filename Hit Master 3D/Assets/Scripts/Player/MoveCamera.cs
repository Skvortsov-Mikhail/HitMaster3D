using UnityEngine;
using Zenject;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform _cameraPivotPosition;
    [SerializeField] private float m_LerpRate;
    [SerializeField] private float m_ForwardLookingOffset;

    private Player _player;

    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _cameraPivotPosition.position, Time.deltaTime * m_LerpRate);
        transform.LookAt(_player.transform.position + Vector3.forward * m_ForwardLookingOffset);
    }
}