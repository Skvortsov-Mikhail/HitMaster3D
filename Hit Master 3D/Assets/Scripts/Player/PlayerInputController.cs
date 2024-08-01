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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit pointInfo;

            if(Physics.Raycast(ray, out pointInfo))
            {
                var target = pointInfo.point;
                _player.Shoot(target);
            }
        }
    }
}