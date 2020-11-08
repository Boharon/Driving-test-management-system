using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE
{
    public class Validation
    {
        // Checks if all chars are in hebrew 
        public static bool IsHebrewLetters(string word)
        {
            string letters = " אבגדהוזחטיכלמנסעפצקרשתךםןףץ";
            for (int i = 0; i < word.Length; i++)
            {
                if (letters.IndexOf(word[i]) == -1)
                    return false;
            }
            return true;

        }

		// Checks if all chars are digits 
		public static bool IsNumbers(string stNum)
        {
            for (int i = 0; i < stNum.Length; i++)
            {
                if(stNum[i]<'0'||stNum[i]>'9')
                    return false;
            }
            return true;
        }
		// Checks if phone number is valid 
		public static bool IsPhoneNumber(string phoneNumber)
        {
            if (!(IsNumbers(phoneNumber)))
                return false;
            if (phoneNumber.Length != 9 && phoneNumber.Length != 10)
                return false;
            return true;
        }

		// Checks if id is valid 
		public static bool IsIdentify(string identify)
        {
            if ((IsNumbers(identify))&& (identify.Length==9))
                return true;
            return false;
        }
		// Checks if all chars are in hebrew or digits
		public static bool IsHebrewLettersAndNumbers(string address)
        {
            string lettersAndNumbers = " אבגדהוזחטיכלמנסעפצקרשת0123456789םףץןך/";
            for (int i = 0; i < address.Length; i++)
                if (lettersAndNumbers.IndexOf(address[i]) == -1)
                    return false;
            return true;

        }
		//  Checks if date is valid - bigger than today.
		public static bool IsDateValid(DateTime date)
        {
            if (date > DateTime.Now)
                return false;
            return true;
        }

        // The function calculates date of birth 

        public static DateTime calculateAge(int age)
        {
            return DateTime.Now.AddYears(-age);
        }

        // The function returns true if the trainee's age is valid and false otherwise
        public static bool checkTraineeAge(DateTime dateTime)
        {
            if (calculateAge(Configuration.minAgeTrainee) > dateTime)
                return true;
            return false;
        }
        // The function returns true if the tester's age is valid and false otherwise
        public static bool CheckTesterAge(Tester t)
        {
            if (calculateAge(Configuration.maxAgeTester) < t.BirthDate)
                return true;
            return false;
        }
    }
}



