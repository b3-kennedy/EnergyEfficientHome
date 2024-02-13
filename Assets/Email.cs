using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[System.Serializable]
public class Response
{
    public GameObject responseObj;
    public Vector3 responsePos;
}


[System.Serializable]
public class EmailAndReponse
{
    public TextMeshPro email;
    public Response[] responses;
}

public class Email : MonoBehaviour
{
    public EmailAndReponse emailAndReponse;

    public Transform[] positions;
}
