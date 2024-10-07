using System.Security.Principal;

namespace Bankomaten
{
    internal class Program
    {

        static void Main(string[] args)
        {



            bool isOn = true;

            Console.WriteLine("Välkommen till din bank");

            while (isOn)

            {
                string input = "";
                User currentUser;
                Console.WriteLine("[C]reate an account");
                Console.WriteLine("[L]og in");
                input = Console.ReadLine();

                if (input.ToUpper() == "C")
                {
                    while (true)
                    {


                        Console.Write("Enter a username: ");
                        string username = Console.ReadLine();
                        if (username == "")
                        {
                            Console.WriteLine("Username can not be empty");
                            continue;
                        }

                        Console.Write("Enter a password: ");

                        string password = Console.ReadLine();
                        if (password == "")
                        {
                            Console.WriteLine("Password can not be empty");
                            continue;
                        }

                        User newUser = new User(username, password);
                        if (User.RegisterUser(newUser))
                        {
                            Console.WriteLine("User registerd sucessfully! ");
                            Utils.ExtractUser(newUser, out currentUser);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("username already exists. ");
                            Console.ReadKey();
                            continue;

                        }
                    }
                    while (isOn)
                    {
                        IsLoggedIn(currentUser, out isOn);
                    }
                }
                else if (input.ToUpper() == "L")
                {

                    Console.Write("Enter a username: ");
                    string username = Console.ReadLine();
                    Console.Write("Enter a password: ");
                    string password = Console.ReadLine();
                    currentUser = User.LogInUser(username, password, out bool sucess);

                    if (!sucess)
                    {
                        Console.ReadKey();
                        continue;
                    }

                    while (isOn)
                    {

                        IsLoggedIn(currentUser, out isOn);
                    }

                }
                else
                {
                    continue;
                }









            }
            Console.WriteLine("Thank you for using our services, goodbye");


        }

        static void IsLoggedIn(User user, out bool isOn)
        {
            user.Account.ShowMenu();
            string input = Console.ReadLine();
            isOn = true;




            switch (input.ToUpper())
            {

                case "I":
                    user.Account.Deposit(Utils.InsertAmount());
                    User.SaveUser(user);
                    break;
                case "U":
                    user.Account.Withdraw(Utils.InsertAmount());
                    User.SaveUser(user);
                    break;
                case "S":
                    user.Account.GetBalance();
                    break;
                case "A":
                    isOn = false;
                    break;
                default:
                    Console.WriteLine("Mata in korrekt format");
                    break;
            }


        }



    }
}
