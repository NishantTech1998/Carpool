using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CarPoolApp
{
    public static class Validator
    {
        public static void Message(string message)
        {
            Console.SetCursorPosition(0, Console.CursorTop-1);
            Console.WriteLine(message);
            Console.ReadKey();
            Console.SetCursorPosition(0, Console.CursorTop-1);
            Console.WriteLine("                                     ");
            Console.SetCursorPosition(0, Console.CursorTop-1);
        }

        public static string NotEmptyValidator(this string str)
        {
            do
            {
                if (string.IsNullOrEmpty(str) || str.Contains("  "))
                {
                    Message("You Cannot Leave This Field Blank");
                    str = Console.ReadLine();
                }
            } while ((string.IsNullOrEmpty(str) || str.Contains("  ")));
            return str;
        }

        public static string AadharValidator(this string aadharNumber)
        {
            string strRegex = @"(^[0-9]{12}$)";
            Regex re = new Regex(strRegex);
            do
            {
                
                if (re.IsMatch(aadharNumber))
                {
                    break;
                }
                else
                {
                    Message("Invalid Aadhar");
                    aadharNumber =Console.ReadLine().NotEmptyValidator();
                }
            } while (true);

            return aadharNumber;
        }

        public static string PhoneValidator(this string phoneNumber)
        {
            string strRegex = @"(^[0-9]{10}$)";
            Regex re = new Regex(strRegex);

            do
            {
                if (re.IsMatch(phoneNumber))
                {
                    break;
                }
                else
                {
                    Message("Invalid Phone Number");
                    phoneNumber = Console.ReadLine().NotEmptyValidator();
                }
            } while (true);

            return phoneNumber;
        }

        public static DateTime DateValidator(this string date)
        {
            string strRegex = @"(^(0?[1-9]|1[0-2])\/(0?[1-9]|1\d|2\d|3[01])\/(19|20)\d{2}$)";
            Regex re = new Regex(strRegex);
            
            do
            {
                if (re.IsMatch(date))
                    break;
                else
                {
                    Message("Invalid Date");
                    date = Console.ReadLine().NotEmptyValidator();
                }
            } while (true);

            return DateTime.Parse(date);
        }

        public static string NameValidator(this string name)
        {
            string strRegex = @"(^[A-Za-z]{3,}$)";
            Regex re = new Regex(strRegex);

            do
            {
                if (re.IsMatch(name))
                {
                    break;
                }
                else
                {
                    Message("Invalid Name");
                    name = Console.ReadLine().NotEmptyValidator();
                }
            } while (true);

            return name;
        }

        public static string PinValidator(this string pin)
        {
            string strRegex = @"(^[0-9]{6}$)";
            Regex re = new Regex(strRegex);

            do
            {
                if (re.IsMatch(pin))
                {
                    break;
                }
                else
                {
                    Message("Invalid Pin");
                    pin = Console.ReadLine().NotEmptyValidator();
                }
            } while (true);

            return pin;
        }

        public static string YesNOValidator(this string response)
        {
            string strRegex = @"(^(Y)$)|(^(N)$)";
            Regex re = new Regex(strRegex);

            do
            {
                if (re.IsMatch(response))
                {
                    break;
                }
                else
                {
                    Message("Invalid Choice");
                    response = Console.ReadLine().NotEmptyValidator();
                }
            } while (true);

            return response;
        }

        public static string GenderValidator(this string gender)
        {
            string strRegex = @"(^(M)$)|(^(F)$)|(^(O)$)";
            Regex re = new Regex(strRegex);

            do
            {
                if (re.IsMatch(gender))
                {
                    break;
                }
                else
                {
                    Message("Invalid Choice");
                    gender = Console.ReadLine().NotEmptyValidator();
                }
            } while (true);

            return gender;
        }

        public static string DigitValidator(this string response)
        {
            string strRegex = @"(^[1-9]$)";
            Regex re = new Regex(strRegex);

            do
            {
                if (re.IsMatch(response))
                {
                    break;
                }
                else
                {
                    Message("Invalid Choice");
                    response = Console.ReadLine().NotEmptyValidator();
                }
            } while (true);

            return response;
        }

    }
}
