using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherComputer : InteractableEntity {
    [SerializeField]
    private Sprite AliveSprite;

    [SerializeField]
    private Sprite DeadSprite;

    [SerializeField]
    private SpriteRenderer renderer;

    [SerializeField]
    private PageDef[] QuestionPageDefs; //What to say on first interact

    [SerializeField]
    private PageDef[] NicePageDefs; //What to say when the player picks the nice option

    [SerializeField]
    private PageDef[] MeanPageDefs; //What to say when the player picks the mean option

    [SerializeField]
    private int MeanOption; //Allows for mixing up of order

    private bool IsAlive;

    private void Awake()
    {
        QuestionPageDefs[QuestionPageDefs.Length - 1].callback = HandleResponse;
    }

    private void OnEnable()
    {
        Reset();
    }

    public override void Interact()
    {
        //Speak if alive
        if (IsAlive)
        {
            SpeechManager.get.TriggerSpeech(QuestionPageDefs);
        }
    }

    private void HandleResponse(int option_index)
    {
        //If we said the mean option, say the mean text, kill the computer and play a death sound
        if (option_index == MeanOption)
        {
            SpeechManager.get.TriggerSpeech(MeanPageDefs);
            KillComputer();
            GetComponent<AudioSource>().Play();
        }
        //If we said the nice option say the nice text
        else
        {
            SpeechManager.get.TriggerSpeech(NicePageDefs);
        }
    }

    public void Reset()
    {
        IsAlive = true;
        renderer.sprite = AliveSprite; //Update Sprite
    }

    public bool GetAlive()
    {
        return IsAlive;
    }

    public void KillComputer()
    {
        IsAlive = false;
        renderer.sprite = DeadSprite; //Update sprite
    }
}
