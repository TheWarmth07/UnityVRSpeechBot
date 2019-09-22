using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Windows.Speech;

public class recognition : MonoBehaviour{
    public Transform spawnPos;
    public GameObject spawnee;

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    public static string[] speechArray = new string[2];

    public static int secondCount = 0;

    void Start(){
        Debug.Log("Start Function Entered");

        actions.Add("spawn", Spawn);
        actions.Add("bed", Bed);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

        // Timer variables
        System.Timers.Timer aTimer = new System.Timers.Timer();
        aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        aTimer.Interval = 10000;
        aTimer.Enabled = true;
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech){
        Debug.Log("Keyword" + speech.text);
        actions[speech.text].Invoke();
    }

    private void Spawn() {
        Instantiate(spawnee, spawnPos.position, spawnPos.rotation);
    }

    private void Bed() {
        // Instantiate(spawnee, spawnPos.position, spawnPos.rotation);
    }

     private static void OnTimedEvent(object source, ElapsedEventArgs e) {
        Debug.Log("Timer Function Entered");

        // Check if the array contains two words
        if (speechArray.Length == 2) {
            Debug.Log("First Command");
            Debug.Log(speechArray[0]);
            Debug.Log("Second Object");
            Debug.Log(speechArray[1]);
        }

        // Empty the array here
        Array.Clear(speechArray, 0, speechArray.Length);
    }
}

