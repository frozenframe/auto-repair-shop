using System;

namespace AutoRepairShop
{
    public class SqlQueries
    {
        private SqlQueries()
        {
        }

        public static readonly string addClient = "insert into clients (lastname, name_cl, surname, phone, сomment) values ('{0}', '{1}', '{2}', '{3}', '{4}')";
        public static readonly string getAllClients = "select id, lastname, name_cl, surname, phone, сomment from clients where 1=1 ";
        public static readonly string getAllClientsByLastname = getAllClients + " and lastname like '%{0}%'";
    }
}