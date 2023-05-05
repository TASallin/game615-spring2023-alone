using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    bool canLeftFlap;
    bool canRightFlap;
    bool canSpin;
    bool canShield;
    bool canRightTurn;
    bool canLeftTurn;
    public bool shielded;
    public bool stunned;
    public float upForce;
    public float forwardForce;
    public Vector3 turnTorque;
    public Vector3 wingTorque;
    public Vector3 correctiveTorque;
    public Vector3 alternateGravity;
    Vector3 realGravity;
    public GameObject shieldObject;
    public GameObject zapParticles;
    public AudioSource shieldSound;
    public AudioSource flapSound;
    public AudioSource stunSound;

    // Start is called before the first frame update
    void Start()
    {
        canLeftFlap = true;
        canRightFlap = true;
        canSpin = true;
        canRightTurn = true;
        canLeftTurn = true;
        canShield = true;
        realGravity = Physics.gravity;
    }

    // Update is called once per frame
    void Update()
    {
        if (stunned || !canShield) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q) && canLeftFlap) {
            StartCoroutine(LeftWing());
        }

        if (Input.GetKeyDown(KeyCode.E) && canRightFlap) {
            StartCoroutine(RightWing());
        }

        if (Input.GetKeyDown(KeyCode.W) && canSpin) {
            StartCoroutine(CorrectiveSpin());
        }

        if (Input.GetKeyDown(KeyCode.A) && canLeftTurn) {
            StartCoroutine(LeftTurn());
        }

        if (Input.GetKeyDown(KeyCode.D) && canRightTurn) {
            StartCoroutine(RightTurn());
        }

        if (Input.GetKeyDown(KeyCode.S) && canShield) {
            StartCoroutine(Shield());
        }
    }

    IEnumerator LeftWing() {
        rb.AddForce(transform.up * upForce + transform.forward * forwardForce);
        rb.AddTorque(wingTorque);
        canLeftFlap = false;
        flapSound.Play();
        yield return new WaitForSeconds(0.2f);
        canLeftFlap = true;
    }

    IEnumerator RightWing() {
        rb.AddForce(transform.up * upForce + transform.forward * forwardForce);
        rb.AddTorque(new Vector3(wingTorque.x, wingTorque.y, wingTorque.z * -1));
        canRightFlap = false;
        flapSound.Play();
        yield return new WaitForSeconds(0.2f);
        canRightFlap = true;
    }
    
    IEnumerator CorrectiveSpin() {
        rb.AddTorque(correctiveTorque);
        canSpin = false;
        yield return new WaitForSeconds(0.5f);
        canSpin = true;
    }

    IEnumerator LeftTurn() {
        rb.AddTorque(turnTorque);
        canLeftTurn = false;
        yield return new WaitForSeconds(0.4f);
        canLeftTurn = true;
    }

    IEnumerator RightTurn() {
        rb.AddTorque(turnTorque * -1);
        canRightTurn = false;
        yield return new WaitForSeconds(0.4f);
        canRightTurn = true;
    }

    IEnumerator Shield() {
        canShield = false;
        shieldSound.Play();
        Physics.gravity = alternateGravity;
        yield return new WaitForSeconds(0.5f);
        shieldObject.SetActive(true);
        shielded = true;
        yield return new WaitForSeconds(1f);
        shieldObject.SetActive(false);
        shielded = false;
        canShield = true;
        Physics.gravity = realGravity;
    }

    IEnumerator Stun() {
        stunned = true;
        zapParticles.SetActive(true);
        stunSound.Play();
        yield return new WaitForSeconds(3f);
        stunned = false;
        zapParticles.SetActive(false);
    }

    public void StunPlayer() {
        if (!stunned) {
            StartCoroutine(Stun());
        }
    }

    public void ResetGravity() {
        Physics.gravity = realGravity;
    }
}
