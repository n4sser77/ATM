using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bankomaten
{

    internal class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Account Account { get; set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        static public bool RegisterUser(User user)
        {
            List<User> users = LoadUsers();

            string hashedPassword = Utils.HashPassword(user.Password);
            Account newAccount = new Account(user.Username, 0);
            user.Account = newAccount;
            user.Password = hashedPassword;

            // Check if the username already exists
            if (users.Any(u => u.Username.Equals(user.Username, StringComparison.OrdinalIgnoreCase)))
            {
                return false; // Username already exists
            }
            // Call a method to store the user in a file (e.g., SaveUser(newUser))
            SaveUser(user);
            return true;
        }

        static public User LogInUser(string username, string password, out bool sucess)
        {
            // Load existing users from the file
            List<User> users = LoadUsers();

            // Find the user by username
            User existingUser = users.FirstOrDefault(u => u.Username == username);

            if (existingUser != null)
            {
                // Hash the provided password and compare it with the stored hash
                string hashedPassword = Utils.HashPassword(password);
                if (hashedPassword == existingUser.Password)
                {
                    Console.WriteLine("Login successful!");
                    // Return the existing user object on successful login
                    sucess = true;
                    return existingUser;
                }
                else
                {
                    Console.WriteLine("Invalid password.");
                    sucess = false;
                    return null; // Return null if password does not match
                }
            }
            else
            {
                Console.WriteLine("User not found.");
                sucess = false;
                return null; // Return null if user does not exist
            }
        }



        public void LogOutUser()
        {

        }

        static private List<User> LoadUsers()
        {
            if (!File.Exists(Utils.filePath))
            {
                return new List<User>();
            }
            string jsonString = File.ReadAllText(Utils.filePath);
            return JsonSerializer.Deserialize<List<User>>(jsonString) ?? new List<User>();
        }

        

        public static void SaveUser(User user)
        {
            List<User> users = LoadUsers();

            // Check if the user already exists in the list
            User existingUser = users.FirstOrDefault(u => u.Username == user.Username);
            if (existingUser != null)
            {
                // Update the existing user's account data (e.g., balance)
                existingUser.Account = user.Account; // You may want to update specific properties like balance
                existingUser.Account.username = user.Username;
            }
            else
            {
                // User does not exist, so add them to the list
                users.Add(user);
            }


            string jsonString = JsonSerializer.Serialize(users);

            using (StreamWriter sw = new StreamWriter(Utils.filePath))
            {
                sw.Write(jsonString);
            }
        }

    }
}
