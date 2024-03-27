using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueParent;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button option1Button;
    [SerializeField] private Button option2Button;
    [SerializeField] private Button option3Button;

    private float typingSpeed = 0.05f;
    [SerializeField] private float defaultTypingSpeed = 0.05f;
    
    [SerializeField] private float turnSpeed = 2f;

    [SerializeField] private bool isTutorial = false;
    private bool skipTyping = false;
    private bool skippable = false;
    [SerializeField] private Oswald oswald;
    private Player player;
    private List<dialogueString> dialoguelist;
    private NPC currentNPC;
    [Header("Player")]
    //playercontroller reference
    private Transform playerCamera;
    private bool inDialogue = false;

    private int currentDialougeIndex = 0;
    

    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        
    }

    private void Start()
    {
        dialogueParent.SetActive(false);
        playerCamera = Camera.main.transform;
        typingSpeed = defaultTypingSpeed;
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if(dialogueParent.activeInHierarchy)
        {
            PlayerInput.DialogueMode();
        }
    }

    public void DialogueStart(List<dialogueString> textToPrint, NPC npc)
    {
        if (inDialogue) return;
        inDialogue = true;
       
        PlayerInput.DialogueMode();
        player.SetMovementDir(Vector2.zero);
       currentNPC = npc;
        Debug.Log("DialogueBegin");
        dialogueParent.SetActive(true);


        dialoguelist = textToPrint;
        currentDialougeIndex = 0;
        DisableButtons();

        StartCoroutine(PrintDialogue());
    }
    public void DialogueStart(List<dialogueString> textToPrint)
    {
        if (inDialogue) return;
        inDialogue = true;
        PlayerInput.DialogueMode();
        player.SetMovementDir(Vector2.zero);
        Debug.Log("DialogueBegin");
        dialogueParent.SetActive(true);

        dialoguelist = textToPrint;
        currentDialougeIndex = 0;
        DisableButtons();

        StartCoroutine(PrintDialogue());
    }

    private void DisableButtons()
    { 
        option1Button.interactable = false;
        option2Button.interactable = false;
        option3Button.interactable = false;


        option1Button.GetComponentInChildren<TMP_Text>().text = "";
        option2Button.GetComponentInChildren<TMP_Text>().text = "";
        option3Button.GetComponentInChildren<TMP_Text>().text = "";

    }
    private bool optionSelected = false;
    private IEnumerator PrintDialogue()
    {
        
        while (currentDialougeIndex < dialoguelist.Count)
        {
            
            skippable = true;
            if (skipTyping)
            {
                typingSpeed = defaultTypingSpeed;
                skipTyping = false;
            }
            dialogueString line = dialoguelist[currentDialougeIndex];

            line.startDialogueEvent?.Invoke();

            if (line.isQuestion)
            {
                yield return StartCoroutine(TypeText(line.text));
                option1Button.interactable = true;
                option2Button.interactable = true;
                option3Button.interactable = true;

                option1Button.GetComponentInChildren<TMP_Text>().text = line.dialogueOption1;
                option2Button.GetComponentInChildren<TMP_Text>().text = line.dialogueOption2;
                option3Button.GetComponentInChildren<TMP_Text>().text = line.dialogueOption3;


                option1Button.onClick.AddListener(() => HandleOptionSelected(line.option1IndexJump));
                option2Button.onClick.AddListener(() => HandleOptionSelected(line.option2IndexJump));
                option3Button.onClick.AddListener(() => HandleOptionSelected(line.option3IndexJump));
                skippable = false;
                yield return new WaitUntil(() => optionSelected);
                
            }
            else
            {
                yield return StartCoroutine(TypeText(line.text));
            }
            line.endDialogueEvent?.Invoke();
            optionSelected = false;
        }
        if (isTutorial) DialogueStop();
        else DialogueStop();
    }
    private void HandleOptionSelected(int indexJump)
    {
        optionSelected = true;
        DisableButtons();
        currentDialougeIndex = indexJump;


    }
    private IEnumerator TypeText(string text)
    {
        dialogueText.text = "";
        foreach(char letter in text.ToCharArray()) 
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        
        if (!dialoguelist[currentDialougeIndex].isQuestion)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }
        if (dialoguelist[currentDialougeIndex].isEnd)
        {
            DialogueStop();
        }
        currentDialougeIndex++;
        
    }
    private void DialogueStop()
    {
        inDialogue = false;
        StopAllCoroutines();
        dialogueText.text = "";
        dialogueParent.SetActive(false);
        PlayerInput.EndDialogueMode();
        if (isTutorial)
        {
            Debug.Log("Finishing tutorial dialogue...");
            oswald.DialogueFinish();
            
        }
        else currentNPC.DialogueFinish();
        
    }

    public void SetDialogueSpeed(float newSpeed)
    {
        defaultTypingSpeed = newSpeed;
        typingSpeed = defaultTypingSpeed;
    }
    public void SkipDialogue()
    {
        print(skippable);
        if (skippable)
        {
            typingSpeed = 0;
            skipTyping = true;
        }
        
        
    }
}
