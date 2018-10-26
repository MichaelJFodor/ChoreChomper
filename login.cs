using System;
using System.Threading;
using System.Data;
using Forums;

namespace Register
{
	public class Login
	{
        // pre: Takes in a string input and a min and max length desired of the input
        // post: Returns true if input length is w/i parameters, false otherwise
        public bool confirmSize(string input, int min, int max)
        {
            if (input.Length >= min && input.Length <= max)
                return true;
            return false;
        }

        // pre: an int wait time for program to sleep and an int number of failed login attempts
        public void loginFail(ref int waitTime, int numFailLogin)
        {
            if(numFailLogin >= 5)
            {
                Console.WriteLine("Must wait " + waitTime + " seconds before signing in again!");
                Thread.Sleep(waitTime);
                waitTime += 1000; // 1 second
                waitTime *= numFailLogin;
            }
        }

        public void loginUser()
        {
            bool loggedIn = false;
            int waitTime = 0000; // 1000 = 1 second : 10000 = 10 seconds
            int numFailLogin = 0;

            do
            {
                string username; // accept user input
                string password; // accept user input

                if (confirmSize(username, 3, 32) && confirmSize(password, 8, 60))
                {
                    if (false) // SELECT username FROM users WHERE username = username (pseudo sql)
                    {
                        if (false) // SELECT password FROM users WHERE username = :username AND password = :password
                        {
                            Console.WriteLine("Logged in");
                            loggedIn = true;
                        }
                        else
                        {
                            Console.WriteLine("Incorrect username or password!");
                            loggedIn = false;
                            loginFail(waitTime, ++numFailLogin);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incorrect username or password!");
                        loggedIn = false;
                        loginFail(waitTime, ++numFailLogin);
                    }

                }
                else
                {
                    Console.WriteLine("Incorrect username or password!");

                }

            } while (loggedIn == false);
        }      
	}
}
