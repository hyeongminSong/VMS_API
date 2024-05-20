using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ConsoleApp1.Models
{
    public class WorkingTimeTable
    {
        public DataTable CreateParseDataTable()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("weekday", typeof(string));
            dataTable.Columns.Add("opening_time", typeof(TimeSpan));
            dataTable.Columns.Add("break_time", typeof(TimeSpan[]));
            dataTable.Columns.Add("id", typeof(int));

            return dataTable;
        }
        public DataTable CreateunParseDataTable()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("weekday", typeof(string));
            dataTable.Columns.Add("opening_start_time", typeof(string));
            dataTable.Columns.Add("opening_end_time", typeof(string));
            dataTable.Columns.Add("break_time", typeof(string[]));

            return dataTable;
        }

        //API 프로세스 양식 Table을 자동화 양식 Table로 변경
        public DataTable ParseRegularScheduleMetaToDataTable(RegularScheduleMeta[] regularScheduleMetas)
        {
            DataTable dataTable = CreateunParseDataTable();
            foreach (var regularSchjedule in regularScheduleMetas)
            {
                TimeSpan opening_time = regularSchjedule.opening_time;
                List<string> BreakTimeStringList = new List<string> { };
                foreach (TimeSpan breakTimeTimeSpan in regularSchjedule.break_time)
                {
                    BreakTimeStringList.Add(breakTimeTimeSpan.start_time + "-" + breakTimeTimeSpan.end_time); 
                }
                dataTable.Rows.Add(regularSchjedule.weekday, opening_time.start_time, opening_time.end_time, BreakTimeStringList.ToArray());
            }
            return dataTable;
        }
    }
}
