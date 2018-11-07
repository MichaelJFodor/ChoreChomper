using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers; 
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

//using ChoreChomper.ViewControllers;
//using ChoreChoper.Model;
namespace ChoreChoper
{

    public class Timestamp
    {
        int day;
        int month;
        int year;
        bool isValid;

        public Timestamp()
        {
            day = 0;
            month = 0;
            year = 0;
            isValid = false;
        }

        public Timestamp(int passDay, int passMonth, int passYear)
        {
            day = passDay;
            month = passMonth;
            year = passYear;
            isValid = true;
        }

        public Timestamp(string condensedTimeFormat)
        {
            //assumes condensedTimeFormat is in the format "MM/DD/YYYY"
            string[] values = condensedTimeFormat.Split('/');
            //TODO: consider using parse return values to catch impropperly formatted dates.
            Int32.TryParse(values[0], out day);
            Int32.TryParse(values[1], out month);
            Int32.TryParse(values[2], out year);
            isValid = true;
        }

        public override string ToString()
        {
            return (day.ToString("D2") + "/" + month.ToString("D2") + "/" + year.ToString("D4"));
        }

        public int GetDay()
        {
            return (day);
        }

        public int GetMonth()
        {
            return (month);
        }

        public int GetYear()
        {
            return (year);
        }

        public Timestamp CurrentTimestamp()
        {
            DateTime desiredTime = DateTime.Now;
            Timestamp desiredTimestamp = new Timestamp(desiredTime.Day, desiredTime.Month, desiredTime.Year);
            return (desiredTimestamp);
        }
    }
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

        public int generateChoreId()
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
    }
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page2 : ContentPage
	{
        public Group group;
        public Page2 ()
		{
			InitializeComponent ();
            
		}

        void Handle_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        void buttonAddChore(object sender, EventArgs e)
        {
            //Chore chore = new Chore();
            //group.AddChore(chore);
        }

        void buttonGetTaskList(object sender, EventArgs e)
        {
            group.GetTaskList();
        }

    }
    public class User
    {
        int userId;
        string userName;

        public User()
        {
            //TODO: creating new users
        }

        public User(string name)
        {
            userName = name;
            //TODO: fetch and check id in database
        }

        public bool CheckCredentials(string pass)
        {
            //TODO: compare against the databases' password for this userId
            return false;
        }

        public int GetId()
        {
            return userId;
        }

        public User GenerateTestUser()
        {
            userId = 0;
            userName = "Bob";

            return this;
        }
    }
    public class Group
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
            return (tasks);
        }
    }

}