using UnityEngine;
using UnityEngine.SceneManagement;

namespace TarotLive.Game
{
    public class RulesManager : MonoBehaviour
    {
        public void OnBackClicked()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}