using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class UI_CloseButton : MonoBehaviour
{
    private LevelController _levelController;
    private Button _startGameButton;

    [Inject]
    public void Construct(LevelController levelController)
    {
        _levelController = levelController;
    }

    private void Start()
    {
        _startGameButton = GetComponent<Button>();
        _startGameButton.onClick.AddListener(CloseApp);
    }

    private void OnDestroy()
    {
        _startGameButton.onClick.RemoveAllListeners();
    }

    private void CloseApp()
    {
        _levelController.QuitGame();
    }
}