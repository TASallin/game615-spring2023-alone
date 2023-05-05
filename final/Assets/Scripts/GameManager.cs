using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public Rigidbody safetyBlock;
    bool beginGame;
    public float deathPlane;
    public float winPlane;
    public CameraController cam;
    int score;
    Vector3 measuringPoint;
    public GameObject startingUI;
    public GameObject gameOverUI;
    public TMP_Text scoreDisplay;
    bool gameOver;
    public AudioSource music;
    public AudioSource gameOverSound;

    [DllImport("__Internal")]
    private static extern void SetScore(int score);

    // Start is called before the first frame update
    void Start()
    {
        beginGame = false;
        gameOver = false;
        measuringPoint = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!beginGame && (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))) {
            beginGame = true;
            safetyBlock.useGravity = true;
            safetyBlock.isKinematic = false;
            Destroy(safetyBlock.gameObject, 3f);
            startingUI.SetActive(false);
            music.Play();
        }

        if (!gameOver && player.transform.position.z > winPlane) {
            gameOver = true;
            player.enabled = false;
            cam.enabled = false;
            player.gameObject.GetComponent<Rigidbody>().useGravity = false;
            player.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            score = 999999;
            gameOverUI.SetActive(true);
            scoreDisplay.text = String.Format("You win!");
            music.Pause();
            try {
                SetScore(score);
            } catch (Exception e) {

            }
        }

        if (!gameOver && player.transform.position.y < deathPlane) {
            gameOver = true;
            player.enabled = false;
            cam.enabled = false;
            measuringPoint.y = player.transform.position.y;
            measuringPoint.x = player.transform.position.x;
            score = (int) Mathf.Round(1000 * Vector3.Distance(measuringPoint, player.transform.position));
            gameOverUI.SetActive(true);
            scoreDisplay.text = "You made it " + score + " millimeters!";
            music.Pause();
            gameOverSound.Play();
            try {
                SetScore(score);
            } catch (Exception e) {

            }
        }

        if (gameOver && Input.GetKeyDown(KeyCode.R)) {
            player.ResetGravity();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
