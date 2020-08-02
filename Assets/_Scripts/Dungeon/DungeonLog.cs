using System.IO;
using System.Linq;
using UnityEngine;
public class DungeonLog : MonoBehaviour
{
    [SerializeField] TextAsset jsonFile = null;
    [SerializeField] Messages messagesList;
    System.Random rng;

    void Start()
    {
        rng = new System.Random();
        messagesList = JsonUtility.FromJson<Messages>(jsonFile.text);
    }
    public string ChooseRandomLog(int code)
    {
        Debug.Log(messagesList.messages[0].message);
        Message[] me = messagesList.messages.Where( s => s.moveCode == code).ToArray();
        return me[rng.Next(me.Length)].message;
    }
}
[System.Serializable]
public class Messages{
    public Message[] messages;
}
[System.Serializable]
public class Message
{
    public string message;
    public int moveCode;
}
