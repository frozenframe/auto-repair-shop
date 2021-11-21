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
		public static readonly string getClientById = getAllClients + " and id = {0}";
		public static readonly string getClientByFioPhone = getAllClients + " and lastname = '{0}' and name_cl = '{1}' and surname = '{2}' and phone = '{3}'";
		public static readonly string getAllClientsByLastname = getAllClients + " and lastname like '*{0}*'";

		public static readonly string addCar = "insert into Cars (mark, model) values ('{0}', '{1}')";
		public static readonly string updateCar = "update Cars set mark='{0}', model='{1}' where id={2}";
		public static readonly string deleteCar = "delete from Cars where id={0}";
		public static readonly string getAllCars = "select id, mark, model from Cars where 1=1 ";
		public static readonly string getAllCarsById = getAllCars + " and id = {0} ";
		public static readonly string getAllCarsByMark = getAllCars + " and mark like '*{0}*' ";
		public static readonly string getAllCarsByModel = getAllCars + " and model like '*{0}*' ";

		public static readonly string addClientCar = "insert into Client_Cars (client_id, car_id, reg_number, comment) values ('{0}', '{1}', '{2}', '{3}')";
		public static readonly string updateClientCar = "update Client_Cars set car_id='{0}', reg_number='{1}', comment='{2}' where id={3}";
		public static readonly string deleteClientCar = "delete from Client_Cars where id={0}";
		public static readonly string getAllCLientCars = "select cc.id, cc.client_id, cc.reg_number, cc.comment, c.id, c.mark, c.model from Client_Cars cc, Cars c where client_id = {0} and cc.car_id = c.id";
	}
}