using ChoreChomper.Model.Utility;
using Newtonsoft.Json;
using ChoreChomper.Data;

namespace ChoreChomper.Model
{
    public class Chore
    {
        [JsonProperty("choreId")]
        int choreId;
        [JsonProperty("choreName")]
        string choreName;
        [JsonProperty("assignedUserId")]
        string assignedUserId;
        [JsonProperty("completedUID")]
        string completedUserId;
        Timestamp deadlineTimestamp;
        [JsonProperty("deadlineTimestamp")]
        string deadlineString;
        Timestamp completedTimestamp;
        [JsonProperty("completedTimestamp")]
        string completedString;
        [JsonProperty("priority")]
        string isPriority;

        SessionData sData = new SessionData();

        public Chore(Chore old)
        {
            choreId = old.choreId;
            choreName = old.choreName;
            assignedUserId = old.assignedUserId;
            completedUserId = old.completedUserId;
            deadlineTimestamp = new Timestamp(old.deadlineTimestamp);
            deadlineString = deadlineTimestamp.ToString();
            completedTimestamp = new Timestamp(old.completedTimestamp);
            completedString = completedTimestamp.ToString();
            isPriority = old.isPriority;
        }

        public Chore()
        {
            //null constructor for JSON parsing
        }


        public void MarkComplete()
        {
            completedUserId = assignedUserId;
            completedTimestamp = new Timestamp().CurrentTimestamp();
            completedString = completedTimestamp.ToString();

            string result = sData.callAPI("completeChore.php?ChoreId=" + choreId + "&TimeStamp=" + completedString + "&CompletedBy=" + completedUserId);
        }

        public string GetName()
        {
            return (choreName);
        }
        public int GetId()
        {
            return (choreId);
        }

        //for testing?
        public string SetName(string name)
        {
            choreName = name;
            return choreName;
        }

        public string GetAssignment()
        {          
            return assignedUserId;
        }

        public string SetAssignment(string newAssignedUserId)
        {
            assignedUserId = newAssignedUserId;
            return assignedUserId;
        }

        public string GetDeadline()
        {

            return deadlineString;
        }

        public string SetDeadline(string newDeadline)
        {
            if (newDeadline != "")
            {
                deadlineTimestamp = new Timestamp(newDeadline);
                deadlineString = deadlineTimestamp.ToString();
            }

            return deadlineString;
        }

        public string GetPriority()
        {
            return isPriority;
        }

        public string SetPriority(string newPriority)
        {
            isPriority = newPriority;
            return isPriority;
        }

        public bool isComplete()
        {
            return (completedUserId != "0");
        }
    }
}