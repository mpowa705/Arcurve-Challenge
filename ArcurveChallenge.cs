/// <summary>
/// Given a local date, get UTC date/time values for the first second of the first day
/// of the month containing that date and the first second of the first day of next month.
/// </summary>
/// <example>
/// When run in Calgary, an input date of February 14, 2010 yields:
///     The first second of the first day of the month is: 7:00:00am, February 1, 2010 (UTC)
///     The first second of the first day of the next month: 7:00:00am, March 1, 2010 (UTC)
/// </example>
/// <param name="aDate">An arbitrary date, in localtime</param>
/// <param name="utcMonthStart">Output: First day of the month in which aDate occurs, in UTC</param>
/// <param name="utcNextMonthStart">Output: First day of the month after aDate, in UTC</param>
void GetMonthRangeInUtc(DateTime aDate, out DateTime utcMonthStart, out DateTime utcNextMonthStart)
    {   
        // compute the first day of the month containing aDate and successive months      
        DateTime[] monthStart = new DateTime[2];   
            
        //Since the DateTime structure is immutable, the original line within the for loop
        //wouldn't work as aDate.Month++ would make an attempt to modify the structure referenced by aDate.
        //My solution simply involves storing the Month value of the structure referenced by aDate in an int and incrementing that.
        //I also do this for the year for reasons outlined below.
            
        //Additionally, in the original solution, an input of a date in december was not accounted for, as incrementing the 12th month to the 13th
        //would attempt to create an un-representable DateTime for monthstart[1]. This is fixed by simple if statement that changes the 13th month into
        //the first month of the following year.
            
        int currMonth = aDate.Month;
        int currYear = aDate.Year;
            
        //In this for loop statement, the only change made was changing i <= monthStart.Length to i < monthStart.Length to prevent an array out-of-bounds error. 
        for (int i = 0; i < monthStart.Length; i++)
        {
            monthStart[i] = new DateTime(currYear, currMonth++, 1);
            if(currMonth == 13)
            {
               currMonth = 1;
               currYear++;
            }
            //monthStart[i] = new DateTime(aDate.Year, aDate.Month++, 1);
        }
            
        // Compute the offset from UTC to our local time (UTC + offset =
        //localtime).
        TimeSpan utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(aDate);
     
        // convert local times to UTC (UTC = localtime - offset)
        utcMonthStart = monthStart[0].Subtract(utcOffset);
        utcNextMonthStart = monthStart[1].Subtract(utcOffset);   
    }
