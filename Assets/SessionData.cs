using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using ChoreChomper.Model;
using System.Net;
using System.IO;

namespace ChoreChomper.Data
{
    class SessionData
    {
        public const string LOCALIP = "192.168.1.205"; //V homePC
        User currentUser = null;
        List<Group> usersGroups = new List<Group>();
        Group targetGroup;
        Chore targetChore;

        string callAPI(string temp)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://" + LOCALIP + "/chorechomper/api/" + temp);
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

        public User getCurrentUser()
        {
            return currentUser;
        }

        public List<Group> getUsersGroups()
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

        public void LoadUser(string username)
        {
            currentUser = new User(username);
            LoadUsersGroups(currentUser.GetId());
        }

        private void LoadUsersGroups(int UserId)
        {
            // TODO: get a list of all group information associated with user from database and put them in userGroups;
        }
        
        public void GenerateTestSession()
        {
            currentUser = new User().GenerateTestUser();
            usersGroups = new List<Group>();
            Group testGroup = new Group().GenerateTestGroup();
            testGroup.AddUser(currentUser);
            usersGroups.Add(testGroup);
        }
    }
}