using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UI_StartGameButton : MonoBehaviour
{
    private LevelController _levelController;
    private Button _startGameButton;
    // private Text _text;

    [Inject]
    public void Construct(LevelController levelController)
    {
        _levelController = levelController;
    }

    private void Start()
    {
        _startGameButton = GetComponent<Button>();
        _startGameButton.onClick.AddListener(StartGame);

        // _text = GetComponentInChildren<Text>(); // TODO скейлить через DOTween
    }

    private void OnDestroy()
    {
        _startGameButton.onClick.RemoveAllListeners();
    }

    private void StartGame()
    {
        _levelController.StartGame();

        gameObject.SetActive(false);
    }
}
