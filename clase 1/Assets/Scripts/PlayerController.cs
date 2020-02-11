using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const float _MINX = -8, _MAXX = 8;
    public float _speedX = 1.0f;
    Vector3 deltaPos;
    GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GlobalScriptsText").GetComponent<GameController>();
    }

    // Update is called once per frame
    /*
        FixedUpdate sirve para mantener un tiempo constante.
         */
    void FixedUpdate()
    {
        // Time.deltaTime es para convertir la velocidad en desplazamiento, time.deltaTime devuelve el tiempo transcurrido de un fps a otro
        deltaPos = new Vector3(Input.GetAxis("Horizontal") * _speedX * Time.deltaTime, 0, 0);
        gameObject.transform.Translate(deltaPos);
        //gameObject
        Debug.Log(Time.deltaTime);

        gameObject.transform.position = new Vector3(Mathf.Clamp(gameObject.transform.position.x,_MINX, _MAXX),
                                                    gameObject.transform.position.y,
                                                    gameObject.transform.position.z);

        // Physics.gravity < Para cambiar la física por código.
    }

    private void OnTriggerEnter(Collider other)
    {
        gameController.IncrementScore();
        Destroy(other.gameObject);
    }
}
