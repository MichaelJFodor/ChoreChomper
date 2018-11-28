using System;

namespace ChoreChomper.Model.Utility
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

        public Timestamp(Timestamp old)
        {
            day = old.day;
            month = old.month;
            year = old.year;
            isValid = old.isValid;
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
            if (values.Length == 3)
            {
                Int32.TryParse(values[0], out month);
                Int32.TryParse(values[1], out day);
                Int32.TryParse(values[2], out year);
            }
            else
            {
                DateTime desiredTime = DateTime.Now;
                day = desiredTime.Day;
                month = desiredTime.Month;
                year = desiredTime.Year;
            }
            isValid = true;
        }

        public override string ToString()
        {
            return (month.ToString("D2") + "/" + day.ToString("D2") + "/" + year.ToString("D4"));
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
}