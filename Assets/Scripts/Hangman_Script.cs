using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class Hangman_Script : MonoBehaviour
{
    public TMP_Text time_field;
    public TMP_Text hidden_text_field;
    public GameObject[] hangman;
    public GameObject win_text;
    public GameObject lose_text;
    public GameObject replay_button;
    private float time;
    private string[] local_words = File.ReadAllLines(@"Assets/Library.txt");
    private string chosen_word;
    private string hidden_word;
    private int misses = 0;
    private bool game_end = false;

    // Start is called before the first frame update
    void Start()
    {

        chosen_word = local_words[Random.Range(0, local_words.Length)];
        Debug.Log(chosen_word);

        for (int i = 0; i < chosen_word.Length; i++)
        {
            char chosen_chars = chosen_word[i];
            if(char.IsWhiteSpace(chosen_chars))
            {
                hidden_word = hidden_word + " ";
            }
            else
            {
                hidden_word = hidden_word + "_";
            }
            
        }
        hidden_text_field.text = hidden_word;
    }

    // Update is called once per frame
    void Update()
    {
        if(game_end == false)
        {
            time = time + Time.deltaTime;
            time_field.text = time.ToString();
        }
    }


    //On GUI Interaction
    private void OnGUI()
    {
        //Initializing the Event
        Event button_press = Event.current;

        //checking if pressed key is included in the chosen word
        if (button_press.type == EventType.KeyDown && button_press.keyCode.ToString().Length == 1)
        {

            string pressed_key = button_press.keyCode.ToString();

            if(chosen_word.Contains(pressed_key))
            {
               
                //Gets position of the pressed key inside the chosen string
                int position = chosen_word.IndexOf(pressed_key);
                //While loop to reveal the letters from the hidden word and switch out the letters from the chosen word 
                while (position != -1)
                {
                    hidden_word = hidden_word.Substring(0, position) + pressed_key + hidden_word.Substring(position + 1);

                    chosen_word = chosen_word.Substring(0, position) + "_" + chosen_word.Substring(position + 1);

                    position = chosen_word.IndexOf(pressed_key);

                }

                Debug.Log(chosen_word);
                hidden_text_field.text = hidden_word;

            } 

            //If wrong go add a body part
            else
            {

                hangman[misses].SetActive(true);
                misses++;

               
            }

            //Lose condition
            if(misses == hangman.Length)
            {
                lose_text.SetActive(true);
                replay_button.SetActive(true);
                game_end = true;
            }
            //Win condition
            else if(!hidden_word.Contains("_"))
            {
                win_text.SetActive(true);
                replay_button.SetActive(true);
                game_end = true;
            }

        }
    }

}
