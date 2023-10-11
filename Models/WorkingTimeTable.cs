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
        //자동화 양식 Table을 API 프로세스 양식 Table로 변경
        public DataTable ParsingDataTable(DataTable unParseTable)
        {
            DataTable workingTable = CreateParseDataTable();

            foreach (DataRow row in unParseTable.Rows)
            {
                List<TimeSpan> BreakTimeTimeSpanList = new List<TimeSpan> { };

                if (row["break_time"] == null)
                {
                    BreakTimeTimeSpanList = null;
                }
                else
                {
                    foreach (string breakTimeString in (string[])row["break_time"])
                    {
                        BreakTimeTimeSpanList.Add(new TimeSpan
                        {
                            start_time = breakTimeString.Split('-').First(),
                            end_time = breakTimeString.Split('-').Last()
                        });
                    }
                }

                workingTable.Rows.Add(
                    row["weekday"],
                    new TimeSpan
                    {
                        start_time = (string)row["opening_start_time"],
                        end_time = (string)row["opening_end_time"]
                    },
                    BreakTimeTimeSpanList == null ? null : BreakTimeTimeSpanList.ToArray()
                );

            }
            return workingTable;
        }

        //API 프로세스 양식 Table을 자동화 양식 Table로 변경
        public DataTable unParsingDataTable(DataTable ParseTable)
        {
            DataTable workingTable = CreateunParseDataTable();

            foreach (DataRow row in ParseTable.Rows)
            {
                List<string> BreakTimeStringList = new List<string> { };

                if (row["break_time"] == null)
                {
                    BreakTimeStringList = null;
                }
                else
                {
                    foreach (TimeSpan breakTimeTimeSpan in (TimeSpan[])row["break_time"])
                    {
                        BreakTimeStringList.Add(breakTimeTimeSpan.start_time + "-" + breakTimeTimeSpan.end_time);
                    }
                }

                TimeSpan openingTime = (TimeSpan)row["opening_time"];

                workingTable.Rows.Add(row["weekday"],
                    openingTime.start_time,
                    openingTime.end_time,
                    BreakTimeStringList == null ? null : BreakTimeStringList.ToArray()
                    );
            }
            return workingTable;
        }
    }
}
