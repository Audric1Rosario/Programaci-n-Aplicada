using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    /*
        Los prefab deben estar inicializados en reset
         */
    public int CurrentScore;
    public TextMesh ScoreText;
    public TextMesh LivesText;
    public GameObject GameOverText;
    const float MINX = -8f, MAXX = 8f;
    public GameObject BallPrefab;
    public int CurrentLives;

    private WebServiceClient webService;

    void Start()
    {
        // Hacer un bucle: una función, tiempo esperado para la primera llamada, tiempo entre cada funcion.
        CurrentScore = 0;
        CurrentLives = 3;
        ScoreText = GetComponent<TextMesh>();
        webService = GetComponent<WebServiceClient>();
        LivesText = GameObject.Find("Lifes").GetComponent<TextMesh>();
        GameOverText = GameObject.Find("GameOverText");

        InvokeRepeating("IntantiateBall", 0, 1.5f);
        GameOverText.SetActive(false);
    }

    // Update is called once per frame
    void IntantiateBall()
    {
        // Quaternion es algo que hizo unity para mejorar las rotaciones y resolver el gimbol lock
        if (CurrentLives <= 0)
        {
            GameOverText.SetActive(true);
            StartCoroutine(webService.SendWebRequest("Unity Player", CurrentScore));
            return;
        }            
        Instantiate(BallPrefab, new Vector3(Random.Range(MINX,MAXX), 8, 0), Quaternion.identity);
    }
    
    public int IncrementScore()
    {
        CurrentScore++;
        ScoreText.text = CurrentScore.ToString();
        return CurrentScore;
    }

    public int DecrementLife()
    {
        CurrentLives = CurrentLives > 0 ? CurrentLives - 1 : 0;
        LivesText.text = $"Vidas: {CurrentLives}"; 

        return 0;
    }
}
