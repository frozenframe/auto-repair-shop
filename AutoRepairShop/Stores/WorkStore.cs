using AutoRepairShop.MetaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairShop.Stores
{
    public class WorkStore
    {
        public event Action<Work> WorkCreated;

        public void CreateWork(Work work)
        {
            WorkCreated?.Invoke(work);
        }
    }
}
