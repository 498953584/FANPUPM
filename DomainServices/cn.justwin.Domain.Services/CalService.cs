namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.ServiceModel.Web;
    using WcfNHibernate;

    [ServiceContract, AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed), ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall), NHibernateContext]
    public class CalService : Repository<CalCalendar>
    {
        public ArrayList ConvertToArrayList<T>(T obj)
        {
            ArrayList list = new ArrayList();
            foreach (PropertyInfo info in obj.GetType().GetProperties())
            {
                list.Add(info.GetValue(obj, null));
            }
            return list;
        }

        [OperationContract]
        public CalCalendar GetById(string id)
        {
            int calId = Convert.ToInt32(id);
            return (from c in this
                where c.Id == calId
                select c).FirstOrDefault<CalCalendar>();
        }

        [OperationContract, WebInvoke(Method="POST", BodyStyle=WebMessageBodyStyle.Bare, ResponseFormat=WebMessageFormat.Json)]
        public CalendarData GetCalData()
        {
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            List<Event> list2 = new List<Event>();
            Event item = new Event {
                Id = 1,
                Subject = "Test",
                StartDate = JSDateTime.ToStr(DateTime.Now.Date),
                EndDate = JSDateTime.ToStr(DateTime.Now.Date.AddHours(11.0)),
                IsAllDayEvent = 1,
                IsEditable = 0,
                IsMoreThanOneDayEvent = 0,
                Attendents = "",
                Location = "Noida",
                RecurringEvent = 0,
                Color = 2
            };
            list2.Add(item);
            Event event4 = new Event {
                Id = 2,
                Subject = "More Test",
                StartDate = JSDateTime.ToStr(DateTime.Now.Date.AddDays(1.0)),
                EndDate = JSDateTime.ToStr(DateTime.Now.Date.AddHours(45.0)),
                IsAllDayEvent = 1,
                IsEditable = 0,
                IsMoreThanOneDayEvent = 0,
                Attendents = "",
                Location = "Delhi",
                RecurringEvent = 0,
                Color = 4
            };
            list2.Add(event4);
            List<Event> list = list2;
            CalendarData data = new CalendarData {
                events = new List<ArrayList>()
            };
            foreach (Event event2 in list)
            {
                data.events.Add(this.ConvertToArrayList<Event>(event2));
            }
            data.issort = true;
            data.start = JSDateTime.ToStr(new DateTime(0x7dd, 7, 1));
            data.end = JSDateTime.ToStr(new DateTime(0x7dd, 7, 0x1f));
            data.error = null;
            return data;
        }

        [DataContract]
        public class CalendarData
        {
            [DataMember(Order=4)]
            public string end { get; set; }

            [DataMember(Order=5)]
            public string error { get; set; }

            [DataMember(Order=1)]
            public List<ArrayList> events { get; set; }

            [DataMember(Order=2)]
            public bool issort { get; set; }

            [DataMember(Order=3)]
            public string start { get; set; }
        }

        public class Event
        {
            [DataMember]
            public string Attendents { get; set; }

            [DataMember]
            public int Color { get; set; }

            [DataMember]
            public string EndDate { get; set; }

            [DataMember(Name="id")]
            public int Id { get; set; }

            [DataMember]
            public int IsAllDayEvent { get; set; }

            [DataMember]
            public int IsEditable { get; set; }

            [DataMember]
            public int IsMoreThanOneDayEvent { get; set; }

            [DataMember]
            public string Location { get; set; }

            [DataMember]
            public int RecurringEvent { get; set; }

            [DataMember]
            public string StartDate { get; set; }

            [DataMember]
            public string Subject { get; set; }
        }

        public static class JSDateTime
        {
            public static string ToStr(DateTime dateTime)
            {
                return dateTime.ToString(@"MM\/dd\/yyyy HH:mm");
            }
        }
    }
}

