using UnityEngine;
using Zenject;

public class CameraMover : MonoBehaviour
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
        MoveCamera();
    }

    private void MoveCamera()
    {
        transform.position = Vector3.Lerp(transform.position, _cameraPivotPosition.position, Time.deltaTime * m_LerpRate);
        transform.LookAt(_player.transform.position + Vector3.forward * m_ForwardLookingOffset);
    }
}