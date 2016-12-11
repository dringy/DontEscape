using UnityEngine;
using System.Collections;
using System;

//Letter Settings
[System.Serializable]
public struct Letter
{
    public Sprite Sprite;
    public PageDef[] PageDefs;
    //Two states one for before reading the letter the other for after
    public GameState LetterState;
    public GameState MainState;
}

//Manages the status of the letter
public class LetterIcon : InteractableEntity {
    [SerializeField]
    private Letter[] letters; //each letter

    private int letter_index = -1;
    private SpriteRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    //Called from the game manager to prompt a change in the icon
    public void UpdateState(GameState state)
    {
        letter_index = GetLetterIndex(state);

        if (letter_index != -1)
        {
            renderer.sprite = letters[letter_index].Sprite;
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private int GetLetterIndex(GameState state)
    {
        //Find a matching letter for our current state
        for (int index = 0; index < letters.Length; ++index)
        {
            if (letters[index].LetterState == state || letters[index].MainState == state)
            {
                letter_index = index;
                return index;
            }
        }
        return -1;
    }

    public override void Interact()
    {
        //Read the letetr with the speech system and move the letter sate accross one
        if (letter_index != -1)
        {
            SpeechManager.get.TriggerSpeech(letters[letter_index].PageDefs);
            GameManager.get.SetState(letters[letter_index].LetterState + 1);
        }
    }
}
