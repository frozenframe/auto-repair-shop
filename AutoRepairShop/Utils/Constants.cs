namespace AutoRepairShop.Utils
{
    public class Constants
    {
        public enum CrudMode
        {
            ADD,
            UPDATE,
            CHANGE_PARENT,
            DELETE
        }
        public enum WorkTypeViewMode
        {
            NONE,
            SELECT,
            CHANGE_PARENT,
            MANAGEMENT
        }

        public static readonly bool SHOW_MODAL = true;
    }
}
