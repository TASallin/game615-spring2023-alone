using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Player")) {
            PlayerController pc = other.gameObject.GetComponent<PlayerController>();
            if (pc.shielded) {
                GetComponent<Collider>().enabled = false;
            } else {
                GetComponent<Collider>().enabled = false;
                pc.StunPlayer();
            }
        }
    }
}
