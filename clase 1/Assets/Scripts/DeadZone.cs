using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{/*
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }*/
    public GameController gameController;
    
    void Start()
    {
        gameController = GameObject.Find("GlobalScriptsText").GetComponent<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        gameController.DecrementLife();
    }
}
