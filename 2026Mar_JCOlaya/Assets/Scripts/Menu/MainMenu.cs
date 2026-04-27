using UnityEngine;
using UnityEngine.SceneManagement; // Imprescindible para cambiar de escenas

public class MainMenu : MonoBehaviour
{
    // Este método cargará la siguiente escena en la lista
    public void PlayGame()
    {
        // "Game" debe ser el nombre exacto de tu escena de juego
        SceneManager.LoadScene("SampleScene");
    }

    // Para cerrar el juego
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego..."); // Esto solo se ve en el editor
        Application.Quit();
    }
}