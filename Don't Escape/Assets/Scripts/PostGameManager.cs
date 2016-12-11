using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PostGameState
{
    FadingIn, //Fade in
    Waiting, //Dramatic Pause
    Speaking //Monster Speaking
}

//Manages the post game sequence
public class PostGameManager : MonoBehaviour {

    private PostGameState state;

    private void Start()
    {
        state = PostGameState.FadingIn;
        OnEnter();
    }


    //Standard State Control
    private void SwitchState(PostGameState new_state)
    {
        OnExit();

        state = new_state;

        OnEnter();
    }

    private void OnEnter()
    {
        switch (state)
        {
            case PostGameState.FadingIn:
                {
                    OnEnter_FadeIn();
                    break;
                }
            case PostGameState.Waiting:
                {
                    OnEnter_Waiting();
                    break;
                }
            case PostGameState.Speaking:
                {
                    OnEnter_Speaking();
                    break;
                }
            default:
                break;
        }
    }

    private void OnExit()
    {
        switch (state)
        {
            case PostGameState.FadingIn:
                {
                    OnExit_FadeIn();
                    break;
                }
            case PostGameState.Waiting:
                {
                    OnExit_Waiting();
                    break;
                }
            case PostGameState.Speaking:
                {
                    OnExit_Speaking();
                    break;
                }
            default:
                break;
        }
    }

    private void Update()
    {
        switch (state)
        {
            case PostGameState.FadingIn:
                {
                    Update_FadeIn();
                    break;
                }
            case PostGameState.Waiting:
                {
                    Update_Waiting();
                    break;
                }
            case PostGameState.Speaking:
                {
                    Update_Speaking();
                    break;
                }
            default:
                break;
        }
    }

    //Fading In
    [Header("Fading In State")]
    [SerializeField]
    private SpriteRenderer coverRenderer;

    [SerializeField]
    private float fadeSpeed; //how fast to fade
    
    private void OnEnter_FadeIn() //1 alpha
    {
        coverRenderer.color = new Color(coverRenderer.color.r, coverRenderer.color.g,
            coverRenderer.color.b, 1.0f);
    }
    
    private void OnExit_FadeIn() //0 alpha
    {
        coverRenderer.color = new Color(coverRenderer.color.r, coverRenderer.color.g,
            coverRenderer.color.b, 0.0f);
    }

    private void Update_FadeIn() //Transitioning alpha
    {
        float new_alpha = Mathf.Lerp(coverRenderer.color.a, 0.0f, fadeSpeed * Time.deltaTime);
        if (new_alpha < 0.01f)
        {
            SwitchState(PostGameState.Waiting);
        }
        else
        {
            coverRenderer.color = new Color(coverRenderer.color.r, coverRenderer.color.g,
            coverRenderer.color.b, new_alpha);
        }
    }

    //Waiting
    [Header("Waiting")]
    [SerializeField]
    private float waitTime; //timer

    private float timeWaited = 0.0f; //time threshold

    private void OnEnter_Waiting()
    {
        timeWaited = 0.0f; //reset
    }

    private void OnExit_Waiting()
    {
        timeWaited = 0.0f; //reset
    }

    private void Update_Waiting()
    {
        //Start speaking after timer
        timeWaited += Time.deltaTime;

        if (timeWaited >= waitTime)
        {
            SwitchState(PostGameState.Speaking);
        }
    }

    //Speaking
    [Header("Speaking")]
    [SerializeField]
    private PageDef[] PageDefs;

    private void OnEnter_Speaking()
    {
        //Start speaking
        PageDefs[PageDefs.Length - 1].callback = FinishSpeaking;
        SpeechManager.get.TriggerSpeech(PageDefs);
    }

    private void OnExit_Speaking()
    {
        
    }

    private void Update_Speaking()
    {
        
    }

    private void FinishSpeaking(int _)
    {
        //Load final screen after the speaking
        SceneManager.LoadScene("EndScene");
    }
}
