using System;

namespace Qoveo.Impact.Helpers
{
    public class PasswordHelper
    {
        public static string GeneratePassword()
        {
            string password = "";
            int maxLength = 6;
            char[] cars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            Random rnd = new Random();

            for (int i = 0; i < maxLength; i++)
            {
                password += cars[rnd.Next(cars.Length)];
            }

            return password;
        }
    }
}