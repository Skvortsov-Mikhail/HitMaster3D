using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UI_StartGameButton : MonoBehaviour
{
    [SerializeField] private float m_ScalingStep;
    [SerializeField] private float m_ScalingSpeed;
    [SerializeField] private float m_MinSize;
    [SerializeField] private float m_MaxSize;

    private LevelController _levelController;
    private Button _startGameButton;
    private Text _text;

    [Inject]
    public void Construct(LevelController levelController)
    {
        _levelController = levelController;
    }

    private void Start()
    {
        _startGameButton = GetComponent<Button>();
        _startGameButton.onClick.AddListener(StartGame);

        _text = GetComponentInChildren<Text>();
    }

    private void OnDestroy()
    {
        _startGameButton.onClick.RemoveAllListeners();
    }

    private void Update()
    {
        ScalingText();
    }

    private void StartGame()
    {
        _levelController.StartGame();

        gameObject.SetActive(false);
    }

    private void ScalingText()
    {
        var step = new Vector3(m_ScalingStep, m_ScalingStep, m_ScalingStep);

        _text.rectTransform.localScale += step * m_ScalingSpeed * Time.deltaTime;

        if (_text.rectTransform.localScale.x >= m_MaxSize && m_ScalingStep > 0 || 
            _text.rectTransform.localScale.x <= m_MinSize && m_ScalingStep < 0)
        {
            m_ScalingStep *= -1;
        }
    }
}
