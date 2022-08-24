using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetEvents
{
    public class EventContentRoot
    {
        public EventContentRoot()
        {
            Categories = new List<EventContentCategory>();
        }
        public List<EventContentCategory> Categories { get; set; }
    }

    public class EventContentCategory
    {
        public EventContentCategory()
        {
            Groups = new List<EventGroup>();
        }
        public string Name { get; set; }
        public List<EventGroup> Groups { get; set; }
    }

    public class EventGroup
    {
        public EventGroup()
        {
            Events = new List<EventDetails>();
        }
        public string Name { get; set; }
        public List<EventDetails> Events { get; set; }
    }

    public class EventDetails
    {
        public EventDetails()
        {
            Types = new List<AideContentDataObject>();
        }

        public string EventName { get; set; }
        public string ClassName { get; set; }
        public string Description { get; set; }


        public string CategoryCode { get; set; }
        public string Code { get; set; }


        public List<AideContentDataObject> Types { get; set; }
    }

    public class AideContentDataObject
    {
        public string ClassName { get; set; }
        public string Description { get; set; }
        public string Declaration { get; set; }
        public AideContentDataObjectDetails Details { get; set; }
    }

    public enum AideContentDataObjectType
    {
        Enum,
        Class
    }

    public class AideContentDataObjectDetails
    {
        public AideContentDataObjectDetails()
        {
            Members = new List<AideContentDataObjectMember>();
        }
        public AideContentDataObjectType DataType { get; set; }
        public List<AideContentDataObjectMember> Members { get; set; }
    }

    public class AideContentDataObjectMember
    {
        public string Name { get; set; }
        public long? EnumValue { get; set; }
        public string FullTypeName { get; set; }
    }



}
