using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace ChoreChomper.Model
{
    public class TaskList //Renamed from Calendar to avoid collisions and clarify use
    {
        [JsonProperty("records")]
        List<Chore> choreList = new List<Chore>();

        public void AddChore(Chore chore)
        {
            choreList.Add(chore);
        }

        public void AddChore(string name, string passedUserId, string deadline)
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

        public List<Chore> GetChoreListCopy()
        {
            List<Chore> returnList = new List<Chore>();
            foreach (Chore c in choreList)
            {
                returnList.Add(new Chore(c));
            }
            return returnList;
        }

        public List<Chore> GetChoreList()
        {
            return choreList;
        }
    }
}