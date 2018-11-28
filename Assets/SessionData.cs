using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using ChoreChomper.Model;
using Newtonsoft.Json;

namespace ChoreChomper.Data
{
    public class GroupList
    {
        [JsonProperty("groups")]
        public List<Group> Groups { get; set; }
    }
    public class SessionData
    {
        User currentUser = null;
        List<Group> usersGroups = new List<Group>();
        Group targetGroup;
        Chore targetChore;

        bool choreFilterMine = false;
        bool choreFilterGroup = false;
        bool choreFilterPriority = false;
        bool choreFilterComplete = false;

        public const string MOBILEIP = "192.168.43.144";
        public const string HOMEIP = "192.168.1.177";
        public const string BENSONIP = "172.20.66.177";
        public string callAPI(string temp)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://" + MOBILEIP + "/chorechomper/api/" + temp);
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            string result = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();
            return result;
        }

        public SessionData()
        {
            ;
        }

        public User GetCurrentUser()
        {
            return currentUser;
        }

        public List<Group> GetUsersGroups()
        {
            return usersGroups;
        }

        public Group GetTargetGroup()
        {
            return targetGroup;
        }

        public Group SetTargetGroup(Group group)
        {
            targetGroup = group;
            return targetGroup;
        }

        public Chore GetTargetChore()
        {
            return targetChore;
        }

        public Chore SetTargetChore(Chore chore)
        {
            targetChore = chore;
            return targetChore;
        }

        public bool GetChoreFilterMine()
        {
            return choreFilterMine;
        }

        public bool GetChoreFilterGroup()
        {
            return choreFilterGroup;
        }

        public bool GetChoreFilterPriority()
        {
            return choreFilterPriority;
        }

        public bool GetChoreFilterComplete()
        {
            return choreFilterComplete;
        }

        public void SetFilters(bool mine, bool group, bool priority, bool complete)
        {
            choreFilterMine = mine;
            choreFilterGroup = group;
            choreFilterPriority = priority;
            choreFilterComplete = complete;
        }

        public void LoadUser(string username)
        {
            currentUser = new User(username);
            LoadUsersGroups();
        }

        public string GetIdOfUser(string name)
        {
            //TODO: fetch id from database if it isnt found return local user's
            string result = callAPI("returnUid.php?Username=" + name);
            result = (string)JsonConvert.DeserializeObject(result, typeof(string));
            if (result != "-1")
                return result;
            else
                return currentUser.GetId();
        }

        public string GetNameOfUser(string id)
        {
            //TODO: fetch name from database if it isnt found return local user's
            string result = callAPI("getUsername.php?Id=" + id);
            result = (string)JsonConvert.DeserializeObject(result, typeof(string));
            if (result != "-1")
                return result;
            else
                return currentUser.GetName();
        }

        public void LoadUsersGroups()
        {
            // TODO: get a list of all group information associated with user from database and put them in userGroups;

            //get current user id
            //looks at db and get all the groups the user is a part of(List < Group > usersGroups)
                //gets a list of groups
            //populate those groups tasklist with chores
                //for each of those groups update their tasklists
            string result = callAPI("chore/read_groups.php?u_ID="+currentUser.GetId());
            GroupList groups = (GroupList)JsonConvert.DeserializeObject(result, typeof(GroupList));
            usersGroups = groups.Groups;
            foreach(Group g in usersGroups)
            {
                g.UpdateTaskList();
            }
        }

        public Group JoinGroup(Group newGroup)
        {
            usersGroups.Add(newGroup);
            targetGroup = newGroup;
            return targetGroup;
        }

        public void GenerateTestSession()
        {
            currentUser = new User().GenerateTestUser();
            usersGroups = new List<Group>();
            Group testGroup = new Group().GenerateTestGroup();
            testGroup.AddUser(currentUser);
            targetGroup = testGroup;
            usersGroups.Add(testGroup);
        }

    }
}