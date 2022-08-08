using TMPro;
using UnityEngine;

namespace Assets.CodeBase.UI
{
    public class WinScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _winnerName;

        public void ShowWinner(string playerName)
        {
            gameObject.SetActive(true);
            _winnerName.text = playerName;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
