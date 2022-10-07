using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private Image[] _images;
        
        public void SetScore(int score)
        {
            for (int i = 0; i < _images.Length; i++)
            {
                if (i < score)
                    _images[i].gameObject.SetActive(true);
                else
                    _images[i].gameObject.SetActive(false);
            }
        }
    }
}