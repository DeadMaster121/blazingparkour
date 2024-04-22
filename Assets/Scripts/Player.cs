using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody rigidBody;
    private Animator animator;

    public float horizontalInput;
    public float verticalInput;
    public bool grounded = true;

    public int totalKeys = 0;
    public int requiredKeys = 3;
    public int hp = 100;

    public TextMeshProUGUI timeLeftText;
    public float timeLeft = 60;
    public string nextScene;

    public AudioSource audioSource;
    public AudioClip loseSound;

    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        Physics.gravity *= 5;
//        grounded = true;
        totalKeys = 0;
    }

    void Update() {
        Move();
        Jump();

//        if (Input.)
        
        timeLeft -= Time.deltaTime;
        timeLeftText.text = "Timer: " + Mathf.Round(timeLeft) + "s";

        if (hp <= 0 || timeLeft <= 0 || transform.position.y < -5) {
            Lose();
        }
    }

    private void Move() {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * Time.deltaTime * 10 * horizontalInput);
        transform.Translate(Vector3.forward * Time.deltaTime * 10 * verticalInput);

        SetAnimationState("MoveForwards", verticalInput > 0);
        SetAnimationState("MoveBackwards", verticalInput < 0);
        SetAnimationState("MoveRight", horizontalInput > 0);
        SetAnimationState("MoveLeft", horizontalInput < 0);
    }

    void SetAnimationState(string animationName, bool condition) {
        animator.SetBool(animationName, condition);
    }

    private void Jump() {
        if (Input.GetKeyDown(KeyCode.Space) && grounded) {
            rigidBody.AddForce(Vector3.up * 20, ForceMode.Impulse);
            grounded = false;
            animator.SetBool("JumpOrFall", true);
        }
    }

    public void Win() {
        if (totalKeys != requiredKeys) {
            ShowTextForDuration("ScreenText", "You need to find all " + requiredKeys + " keys!", 3f);
            transform.position = new Vector3(0, 0, 0);
            return;
        }

        Physics.gravity /= 5;
        grounded = false;
        audioSource.PlayOneShot(loseSound);
        SceneManager.LoadScene(nextScene);
    }

    public void Lose() {
        Physics.gravity /= 5;
//        grounded = false;
        audioSource.PlayOneShot(loseSound);
        SceneManager.LoadScene("GameOver");
    }

    private void OnCollisionEnter(Collision collision) {
        grounded = true;
        animator.SetBool("JumpOrFall", false);

        if (collision.gameObject.CompareTag("BouncePlatform")) {
            rigidBody.AddForce(Vector3.up * 20 * 2, ForceMode.Impulse);
            grounded = false;
            animator.SetBool("JumpOrFall", true);
        }

        if (collision.gameObject.CompareTag("Win")) {
            Win();
        }

        if (collision.gameObject.CompareTag("Enemy")) {
            Lose();
        }

        if (collision.gameObject.CompareTag("Death")) {
            Lose();
        }

        if (collision.gameObject.CompareTag("Weapon")) {
            Destroy(collision.gameObject);
            hp -= 50;
            setText("HeathText", "Health: " + hp);
        }

        if (collision.gameObject.CompareTag("Key")) {
            totalKeys++;
            ShowTextForDuration("ScreenText", "You found a key!", 2.3f);
            setText("TotalKeysText", "Keys: " + totalKeys + "/" + requiredKeys);
            Destroy(collision.gameObject);
        }
    }

    void setText(string tag, string newText) {
        var gameObject = GameObject.FindWithTag(tag);
        if (gameObject != null) {
            var text = gameObject.GetComponent<TextMeshProUGUI>();

            if (text != null) {
                text.text = newText;
            }
        }
    }

    void ShowTextForDuration(string tag, string newText, float duration) {
        var gameObject = GameObject.FindWithTag(tag);
        if (gameObject != null) {
            var text = gameObject.GetComponent<TextMeshProUGUI>();
            if (text != null) {
                StartCoroutine(ShowAndHide(text, newText, duration));
            }
        }
    }

    IEnumerator ShowAndHide(TextMeshProUGUI text, string newText, float duration) {
        text.text = newText;
        text.enabled = true;
        yield return new WaitForSeconds(duration);
        text.enabled = false;
    }
}