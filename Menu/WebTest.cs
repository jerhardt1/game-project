using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebTest : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://localhost/sqlconnect/webtest.php");
        yield return request.SendWebRequest();
        string[] webresults = request.downloadHandler.text.Split('\t');
        Debug.Log(webresults[0]);
        int webNumber = int.Parse(webresults[1]);
        webNumber *= 2;
        Debug.Log(webNumber);
    }


}
