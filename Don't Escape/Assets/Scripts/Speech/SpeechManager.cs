using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public delegate void SpeechCallback(int option_index);

//A page of speech
[System.Serializable]
public struct PageDef
{
    public string Message;
    public bool HasOptions;
    public string[] Options;
    public SpeechStyle Style;
    public SpeechCallback callback;
}

//Manages speech
public class SpeechManager : MonoBehaviour {
    public static SpeechManager get;

    void Awake()
    {
        get = this;
    }

    //UI Fields
    [SerializeField]
    private Text messageText;

    [SerializeField]
    private Text option1Text;

    [SerializeField]
    private Text option2Text;

    //Non-character font
    [SerializeField]
    private Font StandardFont;

    private PageDef[] pageDefs;
    private int pageDefIndex = 0;
    private int optionIndex = -1;

	void Start () {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	private void Update () {
        //Space brings about next page, or finishes speaking if we are the last page
	    if (Input.GetKeyDown(KeyCode.Space))
        {
            SpeechCallback callback = pageDefs[pageDefIndex].callback;

            NextPage();

            //Use callback
            if (callback != null)
            {
                callback(optionIndex);
            }
        }
        //Left and Right option selection
        else if (pageDefs[pageDefIndex].HasOptions)
        {
            float xInput = Input.GetAxisRaw("Horizontal");
            if (xInput > 0.0f)
            {
                optionIndex = 1;
                option1Text.fontStyle = FontStyle.Normal;
                option1Text.color = Color.red;
                option2Text.fontStyle = FontStyle.Bold;
                option2Text.color = Color.green;

            }
            else if (xInput < 0.0f)
            {
                optionIndex = 0;
                option1Text.fontStyle = FontStyle.Bold;
                option1Text.color = Color.green;
                option2Text.fontStyle = FontStyle.Normal;
                option2Text.color = Color.red;
            }
        }
	}

    //Go to next page or finish speaking
    private void NextPage()
    {
        if (++pageDefIndex < pageDefs.Length)
        {
            DisplayPage();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    //Update page display
    private void DisplayPage()
    {
        PageDef page = pageDefs[pageDefIndex];

        messageText.text = page.Message;
        messageText.font = page.Style.Font;
        messageText.color = page.Style.TextColor;
        messageText.fontSize = page.Style.FontSize;

        if (pageDefs[pageDefIndex].HasOptions)
        {
            option1Text.text = page.Options[0];
            option1Text.font = StandardFont;
            option1Text.color = Color.green;
            option1Text.fontStyle = FontStyle.Bold;
            option1Text.gameObject.SetActive(true);

            option2Text.text = pageDefs[pageDefIndex].Options[1];
            option2Text.font = StandardFont;
            option2Text.color = Color.red;
            option2Text.fontStyle = FontStyle.Normal;
            option2Text.gameObject.SetActive(true);

            optionIndex = 0;
        }
        else
        {
            option1Text.text = "Press space to continue";
            option1Text.font = StandardFont;
            option1Text.fontStyle = FontStyle.Bold;
            option1Text.gameObject.SetActive(true);

            option2Text.gameObject.SetActive(false);
            optionIndex = -1;
        }
        gameObject.SetActive(true);
    }

    //Display a whole lot of speech
    public void TriggerSpeech(PageDef[] page_defs)
    {
        pageDefs = page_defs;
        pageDefIndex = 0;
        DisplayPage();
    }

    //Active script indicates we are speaking
    public bool InSpeechMode()
    {
        return gameObject.activeSelf;
    }
}
