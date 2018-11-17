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

namespace ChoreChomper.Data
{
    class SessionData
    {
        User currentUser = null;
        List<Group> usersGroups = new List<Group>();
        Group targetGroup;
        Chore targetChore;

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

        private void LoadUsersGroups(int userId)
        {
            // TODO: get a list of all group information associated with user from database and put them in userGroups;
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
            usersGroups.Add(testGroup);
        }
    }
}