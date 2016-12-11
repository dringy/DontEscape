using UnityEngine;
using System.Collections;
using System;

public class MainComputer : InteractableEntity {

    [SerializeField]
    private Vector3 PlayerResetPos;

    //Initial Conversation

    [SerializeField]
    private PageDef[] EscapePageDef;

    [SerializeField]
    private PageDef[] EscapeConfirmPageDef;

    [SerializeField]
    private PageDef[] EscapeDeclinePageDef;

    //Reset Conversation

    [SerializeField]
    private PageDef[] ResetPageDef;

    [SerializeField]
    private PageDef[] ResetConfirmPageDef;

    [SerializeField]
    private PageDef[] ResetDeclinePageDef;

    //Say the right lines
    public override void Interact()
    {
        if (GameManager.get.GetState() == GameState.Initial)
        {
            EscapePageDef[EscapePageDef.Length - 1].callback = EscapeResponse;
            SpeechManager.get.TriggerSpeech(EscapePageDef);
        }
        else
        {
            ResetPageDef[ResetPageDef.Length - 1].callback = ResetResponse;
            SpeechManager.get.TriggerSpeech(ResetPageDef);
        }
    }

    //Respond to escape conversation
    private void EscapeResponse(int option_index)
    {
        if (option_index == 0)
        {
            SpeechManager.get.TriggerSpeech(EscapeConfirmPageDef);
            GameManager.get.SetState(GameState.BoundaryLetter);
            JingleManager.get.PlayJingle();
        }
        else
        {
            SpeechManager.get.TriggerSpeech(EscapeDeclinePageDef);
        }
    }

    //Respond to reset conversation
    private void ResetResponse(int option_index)
    {
        if (option_index == 0)
        {
            Player.get.transform.position = PlayerResetPos;
            SpeechManager.get.TriggerSpeech(ResetConfirmPageDef);
            GameManager.get.OnReset();
        }
        else
        {
            SpeechManager.get.TriggerSpeech(ResetDeclinePageDef);
        }
    }
}
