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

        //constructor
        public Chore(string name, string passedUserId, string deadline, string priority = "0")
        {
            completedUserId = "-1";
            completedTimestamp = null;
            completedString = null;

            choreName = name;
            choreId = generateChoreId();
            assignedUserId = passedUserId;
            if (deadline == "")
            {
                deadlineTimestamp = new Timestamp("00/00/0000").CurrentTimestamp();
                deadlineString = deadlineTimestamp.ToString();
            }
            else
            {
                deadlineTimestamp = new Timestamp(deadline);
                deadlineString = deadlineTimestamp.ToString();
            }
            isPriority = priority;
        }

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

        int generateChoreId()
        {
            //TODO: fetch greatest chore id and increment by 1
            return 0;
        }

        public void MarkPriority()
        {
            isPriority = "1";
        }

        public void MarkComplete()
        {
            // get the Id of the user who marked it complete and assign its
            completedUserId = assignedUserId;
            completedTimestamp = new Timestamp().CurrentTimestamp();
            completedString = completedTimestamp.ToString();

            string result = sData.callAPI("completeChore.php?ChoreId=" + choreId + "&TimeStamp=" + completedString + "&CompletedBy=" + completedUserId);
        }

        public string AssignUser(string passedUserId)
        {
            assignedUserId = passedUserId;
            return (assignedUserId);
        }

        public bool CheckId(int keyId)
        {
            return (keyId == choreId);
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