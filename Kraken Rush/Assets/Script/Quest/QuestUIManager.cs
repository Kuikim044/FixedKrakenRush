using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUIManager : MonoBehaviour
{
    public static QuestUIManager uiManager;

    public bool questAvailable = false;
    public bool questRunning = false;
    public bool questPanelActive = false;

    public GameObject questPanel;

    public QuestObject currentQuestObject;

    public List<Quest> availableQuests = new List<Quest>();
    public List<Quest> activeQuests = new List<Quest>();

    public GameObject qButton;

    private List<GameObject> qButtons = new List<GameObject>();
    private GameObject acceptButton;
    private GameObject completeButton;

    public Transform qButtonSpacer;

    public TextMeshProUGUI questTitle;
    public TextMeshProUGUI questDescription;
    public TextMeshProUGUI questSummary;


    private void Awake()
    {
        if(uiManager == null)
        {
            uiManager = this;
        }
        else if(uiManager != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        HideQuest();
    }

    // Update is called once per frame
    void Update()
    {
        questPanelActive = !questPanelActive;

    }

    public void OpenQuestPanel()
    {
        questPanelActive = true;
        questPanel.SetActive(questPanelActive);
        FillQuestButtons();
    }


    public void CheckQuestS(QuestObject questObject)
    {
        currentQuestObject = questObject;
        QuestManager.questManager.QuestRequest(questObject);

        if((questRunning || questAvailable) && !questPanelActive)
        {
            //SHOW QUEST PANEL
            OpenQuestPanel();
        }
    }
    public void HideQuest()
    {

        //txtDesCrips.text = "CLICK FOR CHECK QUEST";
        questPanelActive = false;
        questAvailable = false;
        questRunning = false;

        questTitle.text = "";
        questDescription.text = "";
        questSummary.text = "";

        availableQuests.Clear();
        activeQuests.Clear();

        for (int i = 0; i < qButtons.Count; i++)
        {
            Destroy(qButtons[i]);
        }

        qButtons.Clear();
        questPanel.SetActive(questPanelActive);
    }

    void FillQuestButtons()
    {
        foreach (Quest availableQuest in availableQuests)
        {
            GameObject questButton = Instantiate(qButton);
            QuestButton qButtonScript = questButton.GetComponent<QuestButton>();
            qButtonScript.questID = availableQuest.id;
            qButtonScript.questTitle.text = availableQuest.title;

            questButton.transform.SetParent(qButtonSpacer, false);
            qButtons.Add(questButton);

        }

       /* foreach (Quest activeQuest in activeQuests)
        {
            GameObject questButton = Instantiate(qButton);
            QuestButton qButtonScript = questButton.GetComponent<QuestButton>();
            qButtonScript.questID = activeQuest.id;
            qButtonScript.questTitle.text = activeQuest.title;

            questButton.transform.SetParent(qButtonSpacer, false);
            qButtons.Add(questButton);

        }*/
    }
}
