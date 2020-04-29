using CalendarApplication.Model;
using System;
using System.Collections.Generic;

namespace CalendarApplication.UI.Wrapper
{
    public class IncidentWrapper : ModelWrapper<Incident>
    {
        public IncidentWrapper(Incident model) : base(model)
        {
        }
        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Title):
                    if (string.Equals(Title, "Nudy", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Nudy się nie mogą nigdy wydarzyć!";
                    }
                    break;
            }
        }
        public int Id { get { return Model.Id; } }
        public string Title
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public DateTime DateStart
        {
            get { return GetValue<DateTime>(); }
            set 
            { SetValue(value); 
                if (DateStop < DateStart)
                {
                    DateStop = DateStart;
                }
            }
        }
        public DateTime DateStop
        {
            get { return GetValue<DateTime>(); }
            set
            {
                SetValue(value);
                if (DateStop < DateStart)
                {
                    DateStart = DateStop;
                }
            }
        }
    }
}
