using System.Collections.Generic;
using System.Linq;

namespace ChoreChomper.Model
{
    public class TaskList //Renamed from Calendar to avoid collisions and clarify use
    {
        List<Chore> choreList = new List<Chore>();

        public void AddChore(Chore chore)
        {
            choreList.Add(chore);
        }

        public void AddChore(string name, int passedUserId, string deadline)
        {
            choreList.Add(new Chore(name, passedUserId, deadline));
        }

        public int RemoveChore(int choreId)
        {
            int countBefore = choreList.Count;
            choreList.RemoveAll(c => c.CheckId(choreId));

            //returns number of chores removed
            return (choreList.Count - countBefore);
        }

        public string GetHeadChoreName()
        {
            if (choreList.Count == 0)
                return "";
            else
                return choreList[0].GetName();
        }

        public List<string> GetChoreNames()
        {
            List<string> desiredList = new List<string>();

            foreach (Chore chore in choreList)
            {
                desiredList.Add(chore.GetName());
            }

            return desiredList;
        }
    }
}