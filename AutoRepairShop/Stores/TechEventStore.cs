using AutoRepairShop.MetaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Stores
{
    public class TechEventStore
    {
        public event Action<TechEvent> TechEventAdded;

        public void AddTechEvent(TechEvent techEvent)
        {
            TechEventAdded?.Invoke(techEvent);
        }
    }
}
