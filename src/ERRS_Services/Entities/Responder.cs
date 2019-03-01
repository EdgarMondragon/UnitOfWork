using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
    public class Responder : IEntityBase
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string RespondingTo { get; set; }
        public DateTime CalledAt { get; set; }
        public DateTime EtaBefore { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
        public static IEnumerable<Responder> FromDynamic(dynamic responders)
        {
            int id = 0;
            return
                from responder in (responders as IEnumerable<dynamic>)
                select new Responder
                {
                    Id = id++,
                    Name = responder.memberfname,
                    Position = responder.membercat,
                    RespondingTo = responder.RespondingTo,
                    CalledAt = responder.callingtime,
                    EtaBefore = responder.eta,
                    LastName = responder.memberlname
                };
        }
        public static IEnumerable<Responder> ForResponse(IEnumerable<Responder> responders, string sortExpression)
        {
            // Apply sort
            // Get column names and order direction from parameter
            var sortParameters = GetParsedSortData(sortExpression);
            var pi1 = typeof(Responder).GetProperty(GetMappedColumn(sortParameters.Sort1));
            var pi2 = typeof(Responder).GetProperty(GetMappedColumn(sortParameters.Sort2));
            // Return sorted list
            if (sortExpression == string.Empty || pi1 == null || pi2 == null)
            {
                return responders;
            }
            if (sortParameters.OrderDirection == string.Empty || sortParameters.OrderDirection == "asc")
            {
                return responders.OrderBy(x => pi1.GetValue(x, null)).ThenBy(x => pi2.GetValue(x, null));
            }
            return responders.OrderByDescending(x => pi1.GetValue(x, null)).ThenByDescending(x => pi2.GetValue(x, null));
        }
        private static dynamic GetParsedSortData(string sortExpression)
        {
            string[] columns = sortExpression.Split(',');
            string sort1 = "callingtime";
            string sort2 = "callingtime";
            string orderDirection = "desc";
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
                case "memberfname": result = "Name"; break;
                case "membercat": result = "Position"; break;
                case "RespondingTo": result = "RespondingTo"; break;
                case "callingtime": result = "CalledAt"; break;
                case "eta": result = "EtaBefore"; break;
                case "memberlname": result = "LastName"; break;
            }
            return result;
        }
    }
}
