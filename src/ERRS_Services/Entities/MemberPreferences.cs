using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class MemberPreferences:IEntityBase
    {
        public int Id { get; set; }
        public long MemberID { get; set; }
        public string OnScheduleSort { get; set; }
        public string NowRespondingSort { get; set; }
        public bool? EventsAutoScrollEnabled { get; set; }
        public long? EventsAutoscrollSpeed { get; set; }
        public long? EventsDaysDisplayed { get; set; }
        public bool? IncidentInfoAutoScroll { get; set; }
        public long? IncidentInfoScrollSpeed { get; set; }
        public bool? IncidentInfoAutoClear { get; set; }
        public long? IncidentInfoAutoClearInterval { get; set; }
        public DateTime? IncidentInfoLastClearTime { get; set; }
        public string AppleId { get; set; }
        public string AndroidId { get; set; }
        public bool? IncidentNotificationsEnabled { get; set; }
        public string IncidentRingtone { get; set; }
        public string AndroidIncidentRingtone { get; set; }
        public bool? OutboundNotificationsEnabled { get; set; }
        public string OutboundRingtone { get; set; }
        public string AndroidOutboundRingtone { get; set; }
        public DateTime? CustomScheduleStartDate { get; set; }
        public int? CustomScheduleFirstSort { get; set; }
        public int? CustomScheduleSecondSort { get; set; }
        public DateTime? MyScheduleStartDate { get; set; }
        public int? HideMessagesOlderThan { get; set; }
        public int? HideDispatchesOlderThan { get; set; }
        public int? IncidentDisplayType { get; set; }
        public long? ScheduledMessagesDaysDisplayed { get; set; }
        public static MemberPreferences FromDynamic(dynamic preference)
        {
            return preference == null
                       ? null
                       : new MemberPreferences
                       {
                           MemberID = preference.MemberID,
                           OnScheduleSort = string.IsNullOrEmpty(preference.OnScheduleSortOrder) ? "memberlname asc,memberlname asc" : preference.OnScheduleSortOrder,
                           NowRespondingSort = string.IsNullOrEmpty(preference.NowRespondingSortOrder) ? "callingtime desc,callingtime desc" : preference.NowRespondingSortOrder,
                           EventsAutoScrollEnabled = preference.EventsAutoscrollEnabled,
                           EventsAutoscrollSpeed = preference.EventsAutoscrollSpeed,
                           EventsDaysDisplayed = preference.EventsDaysDisplayed,
                           IncidentInfoAutoScroll = preference.DispatchInfoAutoScroll,
                           IncidentInfoScrollSpeed = preference.DispatchInfoScrollSpeed,
                           IncidentInfoAutoClear = preference.DispatchInfoAutoClear,
                           IncidentInfoAutoClearInterval = preference.DispatchInfoAutoClearInterval,
                           IncidentInfoLastClearTime = preference.DispatchInfoLastClearTime,
                           AppleId = string.Empty,
                           AndroidId = string.Empty,
                           IncidentNotificationsEnabled = preference.DispatchNotificationsEnabled,
                           IncidentRingtone = preference.DispatchRingtone,
                           AndroidIncidentRingtone = preference.AndroidDispatchRingtone,
                           OutboundNotificationsEnabled = preference.OutboundNotificationsEnabled,
                           OutboundRingtone = preference.OutboundRingtone,
                           AndroidOutboundRingtone = preference.AndroidOutboundRingtone,
                           CustomScheduleStartDate = preference.CustomScheduleStartDate,
                           CustomScheduleFirstSort = preference.CustomScheduleFirstSort,
                           CustomScheduleSecondSort = preference.CustomScheduleSecondSort,
                           MyScheduleStartDate = preference.MyScheduleStartDate,
                           HideMessagesOlderThan = preference.HideMessagesOlderThan ?? 15,
                           HideDispatchesOlderThan = preference.HideDispatchesOlderThan ?? 15,
                           IncidentDisplayType = preference.IncidentDisplayType ?? 1,
                           ScheduledMessagesDaysDisplayed = preference.ScheduledMessagesDaysDisplayed ?? 30
                       };
        }
    }
}
