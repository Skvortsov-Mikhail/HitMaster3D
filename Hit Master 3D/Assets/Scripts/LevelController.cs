using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelController : MonoBehaviour
{
    public event Action GameStarted;
    private Player _player;

    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }

    public void StartGame()
    {
        GameStarted?.Invoke();

        _player.GoToNextPoint();
    }

    public void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}