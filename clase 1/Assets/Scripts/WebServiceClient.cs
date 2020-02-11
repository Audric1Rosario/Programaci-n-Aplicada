using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class WebServiceClient : MonoBehaviour
{
    [Serializable]
    public class GravilotaScore        // Clase proxy
    {
        public int Id;                 // Clase intermedia, entre lo que esta en el modelo y la aplicacion.
        public string Nombre;
        public double Puntaje;
    }
    UnityWebRequest www;
    const string webserviceURL = "https://localhost:44364/api/values/5";    // Ej: http: //localhost.64198/api/values
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("SendWebRequest");

    }

    IEnumerator SendWebRequest(string Nombre, int Puntaje)
    {
        GravilotaScore newScore = new GravilotaScore();
        newScore.Id = 0;
        newScore.Nombre = Nombre;
        newScore.Puntaje = Puntaje;
        www = UnityWebRequest.Put(webserviceURL, JsonUtility.ToJson(Mi_Objeto)); // Mi objeto esta en el servicio web
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
       
        // JsonUtility.ToJson(); // Con esta función le mandas un objeto, y lo conbierte a una string formato json
        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);
    }

    // Update is called once per frame
    void Update()
    {        
    }

}
