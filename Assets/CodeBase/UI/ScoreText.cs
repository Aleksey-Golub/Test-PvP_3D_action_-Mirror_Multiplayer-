using TMPro;
using UnityEngine;

namespace Assets.CodeBase.UI
{
    public class ScoreText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void SetScoreFor(string name, int score) => 
            _text.text = $"{name}: {score}";

        public void Die() => 
            Destroy(gameObject);
    }
}