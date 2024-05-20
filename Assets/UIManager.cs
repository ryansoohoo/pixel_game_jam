using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI desc;
    public TextMeshProUGUI title;
    public float typingSpeed = 0.05f;
    public float fadeDuration = 1f;
    public float delayBeforeFade = 3f;

    private float timer;
    private int currentCharacterIndex;
    private string titleText;
    private string descText;
    private bool isTypingTitle;
    private bool isTyping;
    private bool isFading;
    private float fadeTimer;
    private Color originalTitleColor;
    private Color originalDescColor;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        // Initialize the text fields to be empty and store original colors
        title.text = "";
        desc.text = "";
        originalTitleColor = title.color;
        originalDescColor = desc.color;
        timer = 0f;
        currentCharacterIndex = 0;
        isTypingTitle = true;
        isTyping = false;
        isFading = false;
        fadeTimer = 0f;

        StartTyping("Otters Home Boat", "Deliver treasures and receive wisdom in return");
    }

    private void Update()
    {
        if (isTyping)
        {
            timer += Time.deltaTime;

            if (isTypingTitle && currentCharacterIndex < titleText.Length)
            {
                if (timer >= typingSpeed)
                {
                    title.text += titleText[currentCharacterIndex];
                    currentCharacterIndex++;
                    timer = 0f;
                }
            }
            else if (!isTypingTitle && currentCharacterIndex < descText.Length)
            {
                if (timer >= typingSpeed)
                {
                    desc.text += descText[currentCharacterIndex];
                    currentCharacterIndex++;
                    timer = 0f;
                }
            }
            else if (isTypingTitle && currentCharacterIndex >= titleText.Length)
            {
                isTypingTitle = false;
                currentCharacterIndex = 0;
                timer = 0f;
            }
            else if (currentCharacterIndex >= descText.Length)
            {
                // Typing is done
                isTyping = false;
                timer = 0f;
            }
        }
        else if (!isTyping && !isFading)
        {
            // Start fade after delay
            timer += Time.deltaTime;
            if (timer >= delayBeforeFade)
            {
                isFading = true;
                fadeTimer = 0f;
            }
        }
        else if (isFading && !CameraFollow.instance.inBoat)
        {
            // Fade out text
            fadeTimer += Time.deltaTime;
            float fadeAmount = fadeTimer / fadeDuration;

            title.color = new Color(originalTitleColor.r, originalTitleColor.g, originalTitleColor.b, 1 - fadeAmount);
            desc.color = new Color(originalDescColor.r, originalDescColor.g, originalDescColor.b, 1 - fadeAmount);

            if (fadeTimer >= fadeDuration)
            {
                isFading = false;
                title.text = "";
                desc.text = "";
                title.color = originalTitleColor;
                desc.color = originalDescColor;
            }
        }
    }

    public void StartTyping(string newTitle, string newDesc)
    {
        // Initialize the text fields to be empty and set the new texts
        title.text = "";
        desc.text = "";
        titleText = newTitle;
        descText = newDesc;
        timer = 0f;
        currentCharacterIndex = 0;
        isTypingTitle = true;
        isTyping = true;
        isFading = false;
        title.color = originalTitleColor;
        desc.color = originalDescColor;
    }
}
