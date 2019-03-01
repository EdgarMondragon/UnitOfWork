using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class OnDuties : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string OnDutyFor { get; set; }
        public string OnDutyAt { get; set; }
        public DateTime Until { get; set; }
        public string UntilAt { get; set; }
        public string LastName { get; set; }
        public static List<OnDuties> FromDynamic(dynamic onDuties)
        {
            int id = 0;
            return
                (from onDuty in (onDuties as IEnumerable<dynamic>)
                 select new OnDuties
                 {
                     Id = id++,
                     Name = onDuty.membername,
                     Position = onDuty.membercat,
                     OnDutyFor = onDuty.instationorhome,
                     OnDutyAt = onDuty.memberstation,
                     Until = onDuty.untilat,
                     UntilAt = onDuty.untilat==null ? "&nbsp;" : (onDuty.untilat).ToString("HH:mm MMM dd"),
                     LastName = onDuty.memberlname
                 }).ToList();
        }

        public static List<OnDuties> ForResponse(List<OnDuties> onDuties, string sortExpression)
        {
            // Apply sort
            // Get column names and order direction from parameter
            var SortParameters = GetParsedSortData(sortExpression);

            var pi1 = typeof(OnDuties).GetProperty(GetMappedColumn(SortParameters.Sort1));
            var pi2 = typeof(OnDuties).GetProperty(GetMappedColumn(SortParameters.Sort2));



            // Return sorted list
            if (sortExpression == string.Empty)
            {
                return onDuties;
            }

            return SortParameters.OrderDirection == string.Empty || SortParameters.OrderDirection == "asc"
                       ? onDuties.OrderBy(x => pi1.GetValue(x, null)).ThenBy(x => pi2.GetValue(x, null)).AsEnumerable().ToList()
                       : onDuties.OrderByDescending(x => pi1.GetValue(x, null))
                               .ThenByDescending(x => pi2.GetValue(x, null))
                               .AsEnumerable().ToList();
        }

        private static dynamic GetParsedSortData(string sortExpression)
        {
            string[] columns = sortExpression.Split(',');
            string sort1 = "memberlname";
            string sort2 = "memberlname";
            string orderDirection = "asc";

            if (sortExpression == string.Empty)
            {
                // No sort
            }
            else if (columns.Length == 1)
            {
                // One column sort
                string[] sortParts = columns[0].Split(' ');
                if (sortParts.Length > 0)
                {
                    sort1 = sortParts[0];
                    orderDirection = sortParts[1];
                }
            }
            else if (columns.Length == 2)
            {
                // Two columns sort
                string[] sortParts = columns[0].Split(' ');
                if (sortParts.Length > 0)
                {
                    sort1 = sortParts[0];
                    orderDirection = sortParts[1];
                }

                sortParts = columns[1].Split(' ');
                if (sortParts.Length > 0)
                {
                    sort2 = sortParts[0];
                }
            }

            return new { Sort1 = sort1, Sort2 = sort2, OrderDirection = orderDirection };

        }

        public static string GetMappedColumn(string column)
        {
            string result = string.Empty;

            switch (column)
            {
                case "memberlname": result = "LastName"; break;
                case "membercat": result = "Position"; break;
                case "instationorhome": result = "OnDutyFor"; break;
                case "memberstation": result = "OnDutyAt"; break;
                case "untilat": result = "Until"; break;
            }

            return result;
        }
    }
}
