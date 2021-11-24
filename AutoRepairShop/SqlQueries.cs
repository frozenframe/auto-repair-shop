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

		public static readonly string addCarBrand = "insert into Car_Brands (brand_name) values ('{0}')";
		public static readonly string updateCarBrand = "update Car_Brands set brand_name='{0}' where id={1}";
		public static readonly string deleteCarBrand = "delete from Car_Brands where id={0}";
		public static readonly string getAllCarBrands = "select id, brand_name from Car_Brands where 1=1 ";
		public static readonly string getAllCarsById = getAllCarBrands + " and id = {0} ";
		public static readonly string getAllCarsByBrandName = getAllCarBrands + " and brand_name like '*{0}*' ";

		public static readonly string addCarModel = "insert into Car_Models (brand_id, model) values ('{0}', '{1}')";
		public static readonly string updateCarModel = "update Car_Models set brand_id='{0}', model='{1}' where id={2}";
		public static readonly string deleteCarModel = "delete from Car_Models where id={0}";
		public static readonly string getAllCarModels = "select id, brand_id, model from Car_Models where 1=1 ";
		// в запросе ниже разрыв логики. Для выполнения запроса экземпляр CarBrand не нужен, а для создания объекта CarModel он понадобится! В частности поле brandName!
		public static readonly string getAllCarModelsById = getAllCarModels + " and id = {0} ";
		public static readonly string getAllCarModelsByBrandId = getAllCarModels + " and brand_id = {0} ";
		public static readonly string getAllCarModelsByModel = getAllCarModels + " and model like '*{0}*' ";

		public static readonly string addClientCar = "insert into Client_Cars (client_id, car_model_id, reg_number, comment) values ('{0}', '{1}', '{2}', '{3}')";
		public static readonly string updateClientCar = "update Client_Cars set car_model_id='{0}', reg_number='{1}', comment='{2}' where id={3}";
		public static readonly string deleteClientCar = "delete from Client_Cars where id={0}";
		public static readonly string getAllCLientCars = "select cc.id, cc.client_id, cc.reg_number, cc.comment, cm.id, cm.brand_id, cm.model, cb.id, cb.brand_name " +
			" from Client_Cars cc, Car_Models cm, Car_Brands cb" +
			" where client_id = {0} and cc.car_model_id = cm.id and cm.brand_id = cb.id";

		public static readonly string getAllWorkTypes = "select id, parent_id, work_type from Work_Types order by parent_id asc, work_type asc";
	}
}