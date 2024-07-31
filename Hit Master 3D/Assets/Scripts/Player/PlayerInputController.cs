using UnityEngine;
using Zenject;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private float m_CameraDepth;
    private Player _player;

    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }

    private void Update()
    {
        if (_player.IsCanShooting == false) return;

        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = Input.mousePosition;
            mousePos += Camera.main.transform.forward * m_CameraDepth;

            var direction = Camera.main.ScreenToWorldPoint(mousePos);

            _player.Shoot(direction);
        }
    }
}