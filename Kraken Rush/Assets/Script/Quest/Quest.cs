using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{
    public enum QuestProgess
    {
        NOT_AVAILABLE,
        AVAILABLE,
        ACCEPTED,
        COMPLETE,
        DONE
    }
    public string title;
    public int id;
    public QuestProgess progess;
    public string description;
    public string hint;
    public string congratuation;
    public string summary;
    public int nextQuest;

    public string questObjective;
    public int questObjectiveCount;
    public int questObjectiveRequirement;

    public int coinReward;
    public int starReward;
    public string itemReward;

}
