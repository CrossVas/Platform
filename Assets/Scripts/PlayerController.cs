using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;
    public LayerMask groundLayer;
    public Animator anim;
    public AudioSource audioSource;
    public AudioClip jump;
    public AudioClip dead;

    public float jumpForce = 6.0F;
    public float runForce = 1.5F;

    private Vector3 startPos;
    private Rigidbody2D player;
    private GameState gameState = GameState.inMenu;
    
    void Start()
    {
        instance = this;
        startPos = transform.position;
        player = GetComponent<Rigidbody2D>();
        gameState = GameState.inMenu;
        anim.enabled = false;
    }

    void Update()
    {
        anim.SetBool("isGrounded", isGrounded());

        if (gameState == GameState.inMenu)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                gameState = GameState.inGame;
            }
        }

        if (gameState == GameState.inGame)
        {
            anim.enabled = true;
            anim.SetBool("isAlive", true);
            jumpEvent();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameState = GameState.inMenu;
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                resetGame();
            }
        }
    }
    
    void FixedUpdate() {
        if (gameState == GameState.inGame) 
        {
            if (player.velocity.x < runForce) {
                player.velocity = new Vector2(runForce, player.velocity.y);
            }
        }
    }
    
    // Game Controller 

    public void gameOver()
    {
        gameState = GameState.gameOver;
        audioSource.clip = dead;
        audioSource.Play();
        anim.SetBool("isAlive", false);
    }
    
    public enum GameState
    {
        inGame, inMenu, gameOver
    }
    
    // Position Controller 

    public bool isGrounded()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, 0.2f, groundLayer.value))
        {
            return true;
        }
        return false;
    }

    public void resetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    // Movement Controller

    public void jumpEvent()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            audioSource.clip = jump;
            audioSource.Play();
            player.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
