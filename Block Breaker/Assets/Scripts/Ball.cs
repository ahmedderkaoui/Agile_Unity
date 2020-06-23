using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour {
    
    // config params
    [SerializeField] Paddle paddle1;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float xPush;
    [SerializeField] float yPush;
    [SerializeField] float randomFactor = 0.2f;

    // state
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    // Cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

	// Use this for initialization
	void Start ()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
        string scene = SceneManager.GetActiveScene().name;
        switch (scene) {
        case "Level 1":
            xPush = 2;
            yPush = 20;
            break;
        case "Level 2":
            xPush = 2.5f;
            yPush = 22.5f;
            break;
        case "Level 3":
            xPush = 3;
            yPush = 25;
            break;
       case "Level 4":
            xPush = 3.5f;
            yPush = 27.5f;
            break;
       case "Level 5":
            xPush = 4;
            yPush = 30;
            break;
    }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            myRigidBody2D.velocity = new Vector2(xPush, yPush);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2
            (randomFactor, randomFactor); 

        if (hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidBody2D.velocity += velocityTweak;
        }
    }
}
