using System;

namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime dob)
        {
            var today = DateTime.Today;
            var age = today.Year - dob.Year;
            //check to see if they had their birthday this year already
            if (dob.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}