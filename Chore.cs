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
        Timestamp completedTimestamp;
        bool isPriority;
        bool isCompleted;

        //constructor
        public Chore(string name, int passedUserId, string deadline)
        {
            isCompleted = false;
            completedUserId = -1;
            completedTimestamp = new Timestamp();

            choreName = name;
            choreId = generateChoreId();
            assignedUserId = passedUserId;
            deadlineTimestamp = new Timestamp(deadline);
        }

        public Chore(Chore old)
        {
            choreId = old.choreId;
            choreName = old.choreName;
            assignedUserId = old.assignedUserId;
            completedUserId = old.completedUserId;
            deadlineTimestamp = new Timestamp(old.deadlineTimestamp);
            completedTimestamp = new Timestamp(old.completedTimestamp);
            isPriority = old.isPriority;
            isCompleted = old.isCompleted;
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
            isCompleted = true;
        }

        public int AssignUser(int passedUserId)
        {
            assignedUserId = passedUserId;
            return (assignedUserId);
        }

        public Timestamp UpdateDeadline(string deadline)
        {
            deadlineTimestamp = new Timestamp(deadline);
            return deadlineTimestamp;
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

        public bool isComplete()
        {
            return isCompleted;
        }

        public bool SetComplete(bool completed)
        {
            isCompleted = completed;
            return isCompleted;
        }
    }
}