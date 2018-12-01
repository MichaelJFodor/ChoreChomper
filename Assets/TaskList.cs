using Newtonsoft.Json;
using System.Collections.Generic;

namespace ChoreChomper.Model
{
    public class TaskList //Renamed from Calendar to avoid collisions and clarify use
    {
        [JsonProperty("records")]
        List<Chore> choreList = new List<Chore>();

        public List<Chore> GetChoreList()
        {
            return choreList;
        }
    }
}