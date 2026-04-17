using OrderModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace ShippingDataService
{
    public class OrderDBData
    {
        private string connectionString =
        "Data Source=localhost\\SQLEXPRESS;Initial Catalog=ShipInfoAndPayMethod;Integrated Security=True;TrustServerCertificate=True;";

        private SqlConnection sqlConnection;

        public OrderDBData()
        {
            sqlConnection = new SqlConnection(connectionString);
        }

        public void Add(OrderModel order)
        {
            var insertStatement = @"INSERT INTO Orders VALUES
            (@OrderID,@CustomerName,@PhoneNumber,@ShippingAddress,
             @ItemName,@Quantity,@Price,@ShippingMethod,
             @OrderStatus,@OrderDate,@EstimatedDelivery)";

            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@OrderID", order.OrderID);
            insertCommand.Parameters.AddWithValue("@CustomerName", order.CustomerName);
            insertCommand.Parameters.AddWithValue("@PhoneNumber", order.PhoneNumber);
            insertCommand.Parameters.AddWithValue("@ShippingAddress", order.ShippingAddress);
            insertCommand.Parameters.AddWithValue("@ItemName", order.ItemName);
            insertCommand.Parameters.AddWithValue("@Quantity", order.Quantity);
            insertCommand.Parameters.AddWithValue("@Price", order.Price);
            insertCommand.Parameters.AddWithValue("@ShippingMethod", order.ShippingMethod);
            insertCommand.Parameters.AddWithValue("@OrderStatus", order.OrderStatus);
            insertCommand.Parameters.AddWithValue("@OrderDate", order.OrderDate);
            insertCommand.Parameters.AddWithValue("@EstimatedDelivery", order.EstimatedDelivery);

            sqlConnection.Open();
            insertCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public List<OrderModel> GetOrders()
        {
            string selectStatement = "SELECT * FROM Orders";

            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);

            sqlConnection.Open();

            SqlDataReader reader = selectCommand.ExecuteReader();

            var orders = new List<OrderModel>();

            while (reader.Read())
            {
                OrderModel order = new OrderModel();

                order.OrderID = reader["OrderID"].ToString();
                order.CustomerName = reader["CustomerName"].ToString();
                order.PhoneNumber = reader["PhoneNumber"].ToString();
                order.ShippingAddress = reader["ShippingAddress"].ToString();
                order.ItemName = reader["ItemName"].ToString();
                order.Quantity = Convert.ToInt32(reader["Quantity"]);
                order.Price = Convert.ToDouble(reader["Price"]);
                order.ShippingMethod = reader["ShippingMethod"].ToString();
                order.OrderStatus = reader["OrderStatus"].ToString();
                order.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
                order.EstimatedDelivery = Convert.ToDateTime(reader["EstimatedDelivery"]);

                orders.Add(order);
            }

            sqlConnection.Close();
            return orders;
        }

        public OrderModel? GetById(string id)
        {
            var selectStatement = "SELECT * FROM Orders WHERE OrderID = @OrderID";

            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            selectCommand.Parameters.AddWithValue("@OrderID", id);

            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            OrderModel order = null;

            while (reader.Read())
            {
                order = new OrderModel
                {
                    OrderID = reader["OrderID"].ToString(),
                    CustomerName = reader["CustomerName"].ToString(),
                    PhoneNumber = reader["PhoneNumber"].ToString(),
                    ShippingAddress = reader["ShippingAddress"].ToString(),
                    ItemName = reader["ItemName"].ToString(),
                    Quantity = Convert.ToInt32(reader["Quantity"]),
                    Price = Convert.ToDouble(reader["Price"]),
                    ShippingMethod = reader["ShippingMethod"].ToString(),
                    OrderStatus = reader["OrderStatus"].ToString(),
                    OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                    EstimatedDelivery = Convert.ToDateTime(reader["EstimatedDelivery"])
                };
            }

            sqlConnection.Close();
            return order;
        }

        public void Update(OrderModel order)
        {
            sqlConnection.Open();

            var updateStatement = @"UPDATE Orders SET
                CustomerName=@CustomerName,
                PhoneNumber=@PhoneNumber,
                ShippingAddress=@ShippingAddress,
                ItemName=@ItemName,
                Quantity=@Quantity,
                Price=@Price,
                ShippingMethod=@ShippingMethod,
                OrderStatus=@OrderStatus,
                EstimatedDelivery=@EstimatedDelivery
                WHERE OrderID=@OrderID";

            SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);

            updateCommand.Parameters.AddWithValue("@OrderID", order.OrderID);
            updateCommand.Parameters.AddWithValue("@CustomerName", order.CustomerName);
            updateCommand.Parameters.AddWithValue("@PhoneNumber", order.PhoneNumber);
            updateCommand.Parameters.AddWithValue("@ShippingAddress", order.ShippingAddress);
            updateCommand.Parameters.AddWithValue("@ItemName", order.ItemName);
            updateCommand.Parameters.AddWithValue("@Quantity", order.Quantity);
            updateCommand.Parameters.AddWithValue("@Price", order.Price);
            updateCommand.Parameters.AddWithValue("@ShippingMethod", order.ShippingMethod);
            updateCommand.Parameters.AddWithValue("@OrderStatus", order.OrderStatus);
            updateCommand.Parameters.AddWithValue("@EstimatedDelivery", order.EstimatedDelivery);

            updateCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void Delete(string id)
        {
            var deleteStatement = "DELETE FROM Orders WHERE OrderID = @OrderID";

            SqlCommand deleteCommand = new SqlCommand(deleteStatement, sqlConnection);
            deleteCommand.Parameters.AddWithValue("@OrderID", id);

            sqlConnection.Open();
            deleteCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}