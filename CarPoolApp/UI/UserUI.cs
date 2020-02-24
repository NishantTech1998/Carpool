using CarPoolApp.Models;
using CarPoolApp.Services;
using System;
using CarPoolApp.Data.DataInterfaces;
using CarPoolApp.Data;
using System.Collections.Generic;
using System.Text;
using SimpleInjector;

namespace CarPoolApp.UI
{
    public class UserUI
    {
       public static string activeUser;
        private static Container container;
        public UserUI()
        {
            container = new Container();
            var lifestyle = Lifestyle.Singleton;
            container.Register<IUserData, UserData>(lifestyle);
        }


       public static void HomePage()
        {
             
            
            var userService = container.GetInstance<UserData>();

            string Response;

            do
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t\tCAR POOl APPLICATION");
                Console.WriteLine("1.Log In\n2.Sign Up\n3.Exit\nAny Number To Exit");
                Response = Console.ReadLine().NotEmptyValidator().DigitValidator();
                if (Response == "1") { LogIn(); }
                else if (Response == "2") { SignUp(); }
                else if(Response=="3") { Environment.Exit(0); }
                else { }
            } while (Response != "1" && Response != "2");
        }


        static void LogIn()
        {
            Console.Clear();
            Console.WriteLine("User ID");
            string UserID = Console.ReadLine().NotEmptyValidator();
            Console.WriteLine("PassWord");
            string Password = Console.ReadLine().NotEmptyValidator();
            //UserService userService = new UserService(new UserData());
            if (userService.Login(UserID, Password))
            {
                activeUser = UserID;
                Program.UserChoices();
            }
            else
            {
                Console.WriteLine("Login Failed");
                Console.ReadKey();
                HomePage();
            }
        }


        static void SignUp()
        {
            User user = new User();
            Car car = new Car();
            Address address = new Address();
            Console.Clear();
            Console.WriteLine("\nFirst Name");
            user.FirstName = Console.ReadLine().NotEmptyValidator().NameValidator();
            Console.WriteLine("\nLast Name");
            user.LastName = Console.ReadLine();
            Console.WriteLine("\nDate Of Birth {mm/dd/yyyy}");
            user.Dob = Console.ReadLine().NotEmptyValidator().DateValidator();
            Console.WriteLine("\nGender {M}:Male {F}:Female {O}:Other");
            user.Gender = Console.ReadLine().NotEmptyValidator().GenderValidator();
            Console.WriteLine("\nAadhar Number");
            user.AadharNumber = Console.ReadLine().NotEmptyValidator().AadharValidator() ;
            Console.WriteLine("\nContact Number");
            user.ContactNumber = Console.ReadLine().NotEmptyValidator().PhoneValidator();
            Console.WriteLine("\nEmail ID");
            user.Email = Console.ReadLine().NotEmptyValidator();
            Console.WriteLine("\nAddress Line 1");
            address.FirstLine = Console.ReadLine().NotEmptyValidator();
            Console.WriteLine("\nAddress Line 2");
            address.SecondLine = Console.ReadLine().NotEmptyValidator();
            Console.WriteLine("\nCity");
            address.City = Console.ReadLine().NotEmptyValidator();
            Console.WriteLine("\nState");
            address.State = Console.ReadLine().NotEmptyValidator();
            Console.WriteLine("\nPincode");
            address.Pincode = Console.ReadLine().NotEmptyValidator().PinValidator();
            Console.Clear();
            user.CurrentAddress = address;
            Console.WriteLine("Do You Have a Car? {Y}:Yes {N}:No");
            string CarResponse = Console.ReadLine().YesNOValidator();
            if (CarResponse == "Y")
            {
                Console.WriteLine("Car Details\n\n");
                Console.WriteLine("Car Brand");
                car.Brand = Console.ReadLine().NotEmptyValidator().NameValidator();
                Console.WriteLine("\nCar Model");
                car.Model = Console.ReadLine().NotEmptyValidator();
                Console.WriteLine("\nCar Color");
                car.Color = Console.ReadLine().NotEmptyValidator().NameValidator();
                Console.WriteLine("\nTotal Seats excluding driver");
                car.TotalSeats = Int32.Parse(Console.ReadLine().DigitValidator());
                Console.WriteLine("\nVehicle Number");
                car.VehicleNumber = Console.ReadLine().NotEmptyValidator();
                user.Car = car;
                car.UserId = user.Id;
            }
            else
            {
                car.Brand = "";
                car.Model = "";
                car.Color = "";
                car.VehicleNumber = "";
                car.TotalSeats = -1;
                user.Car = car;
            }
            Console.WriteLine("\nUser ID");
            user.Id = Console.ReadLine().NotEmptyValidator();
            Console.WriteLine("\nPassWord");
            user.Password = Console.ReadLine().NotEmptyValidator();
            address.UserId = user.Id;
      
            UserService userService = new UserService(new UserData());
            if (userService.SignUp(user))
            { Console.WriteLine("\n\nAdded Successfully");Console.ReadKey(); HomePage(); }
            else
            { SignUp(); }
        }

        public static void ViewProfile()
        {
            UserService userService = new UserService(new UserData());
            Console.Clear();
            User user = userService.GetProfile(activeUser);
            Console.WriteLine($"Name           :{user.FirstName + " " + user.LastName}");
            Console.WriteLine($"User ID        :{user.Id}");
            Console.WriteLine($"Birth Date     :{user.Dob}");
            Console.WriteLine($"Email ID       :{user.Email}");
            Console.WriteLine($"Contact Number :{user.ContactNumber}");
            Console.WriteLine("\n\nCar Details");
            if (user.Car.Brand != "")
            {
                Console.WriteLine($"Car Brand      :{user.Car.Brand}");
                Console.WriteLine($"Car Model      :{user.Car.Model}");
                Console.WriteLine($"Car Color      :{user.Car.Color}");
                Console.WriteLine($"Car Number     :{user.Car.VehicleNumber}");
            }
            Console.ReadKey();
            Console.WriteLine("\n{D}: Delete  {U}: Update  {B}: Back");
            string response = Console.ReadLine().NotEmptyValidator();
            if (response == "D")
                DeleteUser();
            else if (response == "U")
                UpdateUser();
            else if (response == "B")
                Program.UserChoices();
            else
                ViewProfile();
        }

        static void DeleteUser()
        {
            UserService userService = new UserService(new UserData());
            Console.Clear();
            Console.WriteLine("Are You Sure You Want To Delete Your Account? {Y}:Yes  {N}:No");
            string response = Console.ReadLine().YesNOValidator();
            if (response == "Y")
            {
                userService.DeleteUser(activeUser);
                Console.WriteLine("Your Account is deleted");
                Console.ReadKey();
            }
            HomePage();
        }

        static void UpdateUser()
        {
            UserService userService = new UserService(new UserData());
            User user = userService.GetProfile(activeUser);
            Car car = user.Car;
            string choice;

            do
            {
                Console.Clear();
                Console.WriteLine("\nEnter the field number to update\n\n1.First Name\n2.Last Name\n3.Email\n4.Contact Number\n5.Car Brand\n6.Car Model" +
                    "\n7.Car Color\n8.Car Number\n9.Total Seats in Car");
                string response = Console.ReadLine().NotEmptyValidator().DigitValidator();
                if (response == "1") { Console.WriteLine("First Name :");          user.FirstName = Console.ReadLine().NotEmptyValidator().NameValidator(); }
                else if (response == "2") { Console.WriteLine("Last Name :");      user.LastName = Console.ReadLine().NotEmptyValidator().NameValidator(); }
                else if (response == "3") { Console.WriteLine("Email :");          user.Email = Console.ReadLine().NotEmptyValidator().NameValidator(); }
                else if (response == "4") { Console.WriteLine("Contact number :"); user.ContactNumber = Console.ReadLine().NotEmptyValidator().PhoneValidator(); }
                else if (response == "5") { Console.WriteLine("Car Brand :");      car.Brand = Console.ReadLine().NotEmptyValidator().NameValidator(); }
                else if (response == "6") { Console.WriteLine("Car Model :");      car.Model = Console.ReadLine().NotEmptyValidator().NameValidator(); }
                else if (response == "7") { Console.WriteLine("Car Color :");      car.Color = Console.ReadLine().NotEmptyValidator().NameValidator(); }
                else if (response == "8") { Console.WriteLine("Car Number :");     car.VehicleNumber = Console.ReadLine().NotEmptyValidator().NameValidator(); }
                else if (response == "9") { Console.WriteLine("Total Seats :");    car.TotalSeats = Int32.Parse(Console.ReadLine().NotEmptyValidator().DigitValidator()); }
                else { Console.WriteLine("Wrong Choice"); }
                Console.WriteLine("{R}: Repeat {any key}: Go Back");
                choice = Console.ReadLine().NotEmptyValidator();

            } while (choice == "R");
            user.Car = car;
            userService.UpdateProfile(user);
            Program.UserChoices();
        }
    }
}
