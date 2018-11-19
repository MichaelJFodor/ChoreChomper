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
            LoadUsersGroups(currentUser.GetId());
        }

        public int GetIdOfUser(string name)
        {
            //TODO: fetch id from database if it isnt found return local user's
            return currentUser.GetId();
        }

        public string GetNameOfUser(int id)
        {
            //TODO: fetch name from database if it isnt found return local user's
            return currentUser.GetName();
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
            targetGroup = testGroup;
            usersGroups.Add(testGroup);
        }
    }
}