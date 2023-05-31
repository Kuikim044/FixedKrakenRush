using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager questManager;

    public List<Quest> questsList = new List<Quest>();
    public List<Quest> currentQuestsList = new List<Quest>();

    private void Awake()
    {
        if (questManager == null)
        {
            questManager = this;
        } else if (questManager != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void QuestRequest(QuestObject questObject)
    {
        //AVAILABLE QUEST
        if (questObject.availableQuestIDs.Count > 0)
        {
            for (int i = 0; i < questsList.Count; i++)
            {
                for (int j = 0; j < questObject.availableQuestIDs.Count; j++)
                {
                    if (questsList[i].id == questObject.availableQuestIDs[j]
                        && questsList[i].progess == Quest.QuestProgess.AVAILABLE)
                    {
                        //TEST
                        //AcceptQuest(questObject.availableQuestIDs[j]);

                        //UI
                        QuestUIManager.uiManager.questAvailable = true;
                        QuestUIManager.uiManager.availableQuests.Add(questsList[i]);

                    }
                }
            }

            //ACTIVE QUEST
            for (int i = 0; i < currentQuestsList.Count; i++)
            {
                for (int j = 0; j < questObject.receivableQuestIDs.Count; j++)
                {
                    if (currentQuestsList[i].id == questObject.receivableQuestIDs[j]
                        && currentQuestsList[i].progess == Quest.QuestProgess.ACCEPTED
                        || currentQuestsList[i].progess == Quest.QuestProgess.COMPLETE)
                    {

                        CompleteQuest(questObject.receivableQuestIDs[j]);
                        //UI
                        QuestUIManager.uiManager.questRunning = true;
                        QuestUIManager.uiManager.activeQuests.Add(currentQuestsList[i]);
                    }
                }
            }
        }
        
    }

    //ACCEPT QUEST
    public void AcceptQuest(int questID)
    {
        for(int i =0; i < questsList.Count; i++)
        {
            if (questsList[i].id == questID && 
                questsList[i].progess == Quest.QuestProgess.AVAILABLE)
            {
                currentQuestsList.Add(questsList[i]);
                questsList[i].progess = Quest.QuestProgess.ACCEPTED;
            }
        }
    }

    //COMPLETE QUEST
    public void CompleteQuest(int questID)
    {
        for (int i = 0; i < currentQuestsList.Count; i++)
        {
            if (currentQuestsList[i].id == questID &&
                questsList[i].progess == Quest.QuestProgess.COMPLETE)
            {
                questsList[i].progess = Quest.QuestProgess.DONE;
                currentQuestsList.Remove(currentQuestsList[i]);

                //REWARD
            }
        }

        //CHECK CHAIN QUEST
        CheckChainQuest(questID);
    }

    public void CheckChainQuest(int questID)
    {
        int tempID = 0;
        for(int i =0; i < questsList.Count; i++)
        {
            if (questsList[i].id == questID
                && questsList[i].nextQuest > 0)
            {
                tempID = questsList[i].id;
            }
        }

        if(tempID > 0)
        {
            for(int i = 0; i < questsList.Count; i++)
            {
                if (questsList[i].id == tempID
                    && questsList[i].progess == Quest.QuestProgess.NOT_AVAILABLE)
                {
                    questsList[i].progess = Quest.QuestProgess.AVAILABLE;
                }
            }
        }
    }


    // ADD ITEMS
    public void AddQuestItem(string questObject, int itemAmount)
    {
        for(int i = 0; i < currentQuestsList.Count; i++)
        {
            if (currentQuestsList[i].questObjective == questObject
                && currentQuestsList[i].progess == Quest.QuestProgess.ACCEPTED)
            {
                currentQuestsList[i].questObjectiveCount += itemAmount;
            }
            if (currentQuestsList[i].questObjectiveCount >= currentQuestsList[i].questObjectiveRequirement
                && currentQuestsList[i].progess == Quest.QuestProgess.ACCEPTED)
            {
                currentQuestsList[i].progess = Quest.QuestProgess.COMPLETE;
            }
        }
       
    }
    //REMOVE ITEMS

    // BOOL 
    public bool RequestAvailableQuest(int questID)
    {
        for(int i = 0; i < questsList.Count; i++)
        {
            if (questsList[i].id == questID && 
                questsList[i].progess == Quest.QuestProgess.AVAILABLE)
            {
                return true;

            }
        }
        return false;
    }
    public bool RequestAcceptedQuest(int questID)
    {
        for (int i = 0; i < questsList.Count; i++)
        {
            if (questsList[i].id == questID &&
                questsList[i].progess == Quest.QuestProgess.ACCEPTED)
            {
                return true;

            }
        }
        return false;
    }
    public bool RequestCompleteQuest(int questID)
    {
        for (int i = 0; i < questsList.Count; i++)
        {
            if (questsList[i].id == questID &&
                questsList[i].progess == Quest.QuestProgess.COMPLETE)
            {
                return true;

            }
        }
        return false;
    }


    // BOOL 2

    public bool CheckAvailableQuest(QuestObject questObject)
    {
        for(int i = 0; i < questsList.Count; i++)
        {
            for(int j = 0; j < questObject.availableQuestIDs.Count; j++)
            {
                if (questsList[i].id == questObject.availableQuestIDs[j]
                    && questsList[i].progess == Quest.QuestProgess.AVAILABLE)
                {
                    return true;

                }
            }
        }
        return false;
    }
    public bool CheckAcceptQuest(QuestObject questObject)
    {
        for (int i = 0; i < questsList.Count; i++)
        {
            for (int j = 0; j < questObject.receivableQuestIDs.Count; j++)
            {
                if (questsList[i].id == questObject.receivableQuestIDs[j]
                    && questsList[i].progess == Quest.QuestProgess.ACCEPTED)
                {
                    return true;

                }
            }
        }
        return false;
    }
    public bool CheckCompleteQuest(QuestObject questObject)
    {
        for (int i = 0; i < questsList.Count; i++)
        {
            for (int j = 0; j < questObject.receivableQuestIDs.Count; j++)
            {
                if (questsList[i].id == questObject.receivableQuestIDs[j]
                    && questsList[i].progess == Quest.QuestProgess.COMPLETE)
                {
                    return true;

                }
            }
        }
        return false;
    }
}
