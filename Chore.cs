using ChoreChomper.Model.Utility;

namespace ChoreChomper.Model
{
    public class Chore
    {
        int choreId;
        string choreName;
        int assignedUserId;
        int completedUserId;
        Timestamp deadlineTimestamp;
        string deadlineString;
        Timestamp completedTimestamp;
        string completedString;
        bool isPriority;

        //constructor
        public Chore(string name, int passedUserId, string deadline, bool priority = false)
        {
            completedUserId = -1;
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

        int generateChoreId()
        {
            //TODO: fetch greatest chore id and increment by 1
            return 0;
        }

        public void MarkPriority()
        {
            isPriority = true;
        }

        public void MarkComplete()
        {
            //TODO: get the Id of the user who marked it complete and assign it
            completedUserId = assignedUserId;
            completedTimestamp = new Timestamp().CurrentTimestamp();
            completedString = completedTimestamp.ToString();
        }

        public int AssignUser(int passedUserId)
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

        //for testing?
        public string SetName(string name)
        {
            choreName = name;
            return choreName;
        }

        public int GetAssignment()
        {          
            return assignedUserId;
        }

        public int SetAssignment(int newAssignedUserId)
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

        public bool GetPriority()
        {
            return isPriority;
        }

        public bool SetPriority(bool newPriority)
        {
            isPriority = newPriority;
            return isPriority;
        }

        public bool isComplete()
        {
            return (completedTimestamp != null);
        }
    }
}