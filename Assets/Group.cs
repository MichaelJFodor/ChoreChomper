using System.Collections.Generic;
using ChoreChomper.Data;
using Newtonsoft.Json;

namespace ChoreChomper.Model
{
    public class Group
    {
        [JsonProperty("id_group")]
        string groupId;
        [JsonProperty("group_name")]
        string groupName;
        bool valid;
        SessionData sData = new SessionData();
        
        List<User> users;
        TaskList tasks;

        public Group()
        {
            //null constructor for json parsing
        }
        public Group(string s)
        {
            groupId = "0";
            groupName = "Fake";
            valid = false;
        }
        public Group(string id, string name, bool validCheck)
        {
            groupId = id;
            groupName = name;
            valid = validCheck;
        }

        public bool AssignGroup(int gID, string groupPassword)
        {
            //Fetch Group data corresponding to groupId
            //Assign data values to group
            string result = sData.callAPI("joinGroup_id.php?Group_id=" + gID + "&Group_password=" + groupPassword);
            result = (string)JsonConvert.DeserializeObject(result, typeof(string));
            groupId = gID.ToString();
            if (result != "-1")
            {
                valid = true;
                UpdateTaskList();
            }
            else
            {
                valid = false;
            }
            //return false if group was not created or true if it was
            return valid;
        }

        public bool AssignGroup(string name, string groupPassword, string uid)
        {
            //Fetch Group data corresponding to groupName
            //Assign data values to group
            string result = sData.callAPI("joinGroup.php?Group_name="+name+"&Group_password="+groupPassword+"&User_id="+uid);
            result = (string)JsonConvert.DeserializeObject(result, typeof(string));
            groupId = result;
            groupName = name;
            if(result != "-1")
            {
                valid = true;
                UpdateTaskList();
            }
            else
            {
                valid = false;
            }
            //return false if group was not created or true if it was
            return valid;
        }
        
        public Group GenerateNew(string name, string Password, string uid)
        {
            //Add a new group to the database with name Name
            //Add the local user to the group's list of users
            string result = sData.callAPI("createGroup.php?Group_name=" + name + "&Group_password=" + Password);
            result = (string)JsonConvert.DeserializeObject(result, typeof(string));
            if (result == "true")
                AssignGroup(name, Password, uid);
            return this;
        }

        public Group GenerateTestGroup(string name = "Default Group")
        {
            groupId = "0";
            groupName = name;
            valid = true;
            users = new List<User>();
            tasks = new TaskList();
            return this;
        }

        public User AddUser(User passedUser)
        {
            users.Add(passedUser);
            return (passedUser);
        }

        public Chore AddChore(Chore chore)
        {
            tasks.AddChore(chore);
            return chore;
        }

        public TaskList GetTaskList()
        {
            return(tasks);
        }

        public void UpdateTaskList() //assigns tasks a full list of chores associated with all users in this group
        {
            string result = sData.callAPI("chore/read_group_chores.php?g_ID=" + groupId);
            tasks = (TaskList)JsonConvert.DeserializeObject(result, typeof(TaskList));
        }

        public string getName()
        {
            return groupName;
        }
    }
}