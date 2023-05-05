using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform player;
    public Vector3 offset;
    public LayerMask mask;
    float proximity;

    // Start is called before the first frame update
    void Start() {
        proximity = 2;
    }

    // Update is called once per frame
    void Update() {
        if (Physics.Raycast(player.position, transform.forward * -1, 4, mask)) {
            proximity = Mathf.Max(proximity - Time.deltaTime, 2);
        } else {
            proximity = Mathf.Min(proximity + Time.deltaTime, 4);
        }
        transform.position = player.position + offset;
        //transform.position = new Vector3(transform.position.x, offset.y, transform.position.z);
        //if (Input.GetKeyDown(KeyCode.R)) {
        //transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, player.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
        //}
    }
}
