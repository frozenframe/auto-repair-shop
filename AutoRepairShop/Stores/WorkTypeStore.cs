using AutoRepairShop.MetaModel;
using System;

namespace AutoRepairShop.Stores
{
    public class WorkTypeStore
    {
        public event Action<WorkType> WorkTypeCreated;
        public event Action<WorkType> WorkTypeUpdated;
        public event Action<WorkType> WorkTypeChangedParent;
        public event Action<WorkType> WorkTypeSelected;

        public void CreateWorkType(WorkType workType)
        {
            WorkTypeCreated?.Invoke(workType);
        }

        public void UpdateWorkType(WorkType workType)
        {
            WorkTypeUpdated?.Invoke(workType);
        }

        public void SelectWorkType(WorkType workType)
        {
            WorkTypeSelected?.Invoke(workType);
        }

        public void SelectNewParentWorkType(WorkType workType)
        {
            WorkTypeChangedParent?.Invoke(workType);
        }
    }
}
