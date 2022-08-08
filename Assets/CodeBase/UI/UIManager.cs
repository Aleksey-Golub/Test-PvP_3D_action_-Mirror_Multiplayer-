using Mirror;
using UnityEngine;

namespace Assets.CodeBase.UI
{
    public class UIManager : NetworkBehaviour
    {
        [SerializeField] private WinScreen _winScreen;
        [SerializeField] private ScoreText _scoreTextPrefab;
        [SerializeField] private ScoreLabels _labels;

        public void Construct() => _winScreen.Hide();

        [ClientRpc]
        public void RpcShowWinScreen(string playerName) => _winScreen.ShowWinner(playerName);

        [ClientRpc]
        public void RpcHideWinScreen() => _winScreen.Hide();

        public ScoreText GetScoreText() => Instantiate(_scoreTextPrefab, _labels.transform);
    }
}