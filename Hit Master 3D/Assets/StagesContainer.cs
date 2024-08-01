using UnityEngine;

public class StagesContainer : MonoBehaviour
{
    private Stage[] _stages;

    private int _currentStageIndex;

    private void Awake()
    {
        _stages = GetComponentsInChildren<Stage>();
    }

    public Stage GetCurrentStage()
    {
        return _stages[_currentStageIndex];
    }

    public bool TryPerformNextStage()
    {
        _currentStageIndex++;

        if (_currentStageIndex >= _stages.Length) return false;

        return true;
    }
}
