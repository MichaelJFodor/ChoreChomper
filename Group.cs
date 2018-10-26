using System.Collections.Generic;
using UserSpace;
using TaskSpace;
using ChoreSpace;

namespace GroupSpace
{
    class Group
    {
        int groupId;
        string groupName;
        bool valid;

        List<User> users;
        TaskList tasks;

        public Group()
        {
            groupId = 0;
            groupName = "Fake";
            valid = false;
        }

        public bool AssignGroup(int groupId)
        {
            //TODO:
            //Fetch Grouup data corresponding to groupId
            //Assign data values to group
            valid = true;
            //return false if group was not created or true if it was
            return false;
        }

        public bool AssignGroup(string groupName)
        {
            //TODO:
            //Fetch Grouup data corresponding to groupName
            //Assign data values to group
            valid = true;
            //return false if group was not created or true if it was
            return false;
        }
        
        public Group GenerateNew(string Name)
        {
            //Add a new group to the database with name Name
            //Add the local user to the group's list of users
            return (new Group());
        }

        public Group GenerateTestGroup()
        {
            groupId = 0;
            groupName = "Best_Group";
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
    }
}