using System;

namespace AutoRepairShop
{
    public class SqlQueries
    {
        private SqlQueries()
        {
        }

        public static readonly string addClient = "insert into clients (lastname, name_cl, surname, phone, сomment) values ('{0}', '{1}', '{2}', '{3}', '{4}')";
        public static readonly string updateClient = "update clients set lastname='{0}', name_cl='{1}', surname='{2}', phone='{3}', сomment='{4}' where id={5}";
        public static readonly string deleteClient = "delete from clients where id={0}";
        public static readonly string getAllClients = "select id, lastname, name_cl, surname, phone, сomment from clients where 1=1 ";
        public static readonly string getAllClientsByLastname = getAllClients + " and lastname like '%{0}%'";
    }
}