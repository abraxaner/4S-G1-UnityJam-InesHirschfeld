using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    public GameObject loseScreen;
    public GameObject winScreen;
    public Color startColor = Color.white; 
    public Color targetColor = Color.blue; 

    private int rightItemCount = 0;
    private const int targetItemCount = 4;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        
        if (loseScreen != null)
        {
            loseScreen.SetActive(false);
        }

        if (winScreen != null)
        {
            winScreen.SetActive(false);
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = startColor;  
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Wrong"))
        {
           
            if (loseScreen != null)
            {
                loseScreen.SetActive(true);
            }

            
            Time.timeScale = 0f;
        }
        
        else if (collision.CompareTag("Right"))
        {
            rightItemCount++;
            Destroy(collision.gameObject);

       
            UpdatePlayerColor();

        
            if (rightItemCount >= targetItemCount)
            {
        
                if (winScreen != null)
                {
                    winScreen.SetActive(true);
                }

                Time.timeScale = 0f;
            }
        }
    }

    void UpdatePlayerColor()
    {
        if (spriteRenderer != null)
        {
            float t = (float)rightItemCount / targetItemCount;
            spriteRenderer.color = Color.Lerp(startColor, targetColor, t);
        }
    }

 
    public void ReloadGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
