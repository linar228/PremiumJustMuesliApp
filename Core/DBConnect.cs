using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using MuesliCore.ViewModels;

namespace MuesliCore
{
    public static class DBConnect
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["MuesliDB"].ConnectionString;
        private static IDbConnection connection = new SqlConnection(connStr);

        public static List<MuesliMix> GetMuesliMixes()
        {
            return connection.Query<MuesliMix>("select * from MuesliMix").AsList();
        }
        public static List<Order> GetOrders()
        {
            return connection.Query<Order>("select * from [Order]").AsList();
        }
        public static Order GetOrder(int orderId)
        {
            try
            {
                return connection.Query<Order>($"select * from [Order] where [Id] = {orderId}").AsList()[0];
            }
            catch { return null; }
        }
        public static void AddMuesliMix(MixModel m)
        {
            try
            {
                connection.Query($"insert into [dbo].[MuesliMix] ([Name],[CreatedDate])" +
                $" values ('{m.Name}', '{DateTime.Now}')");
                int mixId = connection.Query<int>("select max(Id) from MuesliMix").AsList()[0];
                foreach (var i in m.Ingredients)
                {
                    connection.Query($"insert into MuesliMixIngredient ([MuesliMixId], [MuesliId]) values ({mixId}, {i})");
                }
                
            }
            catch { }
        }
        public static void CreateOrder(MuesliMix mix)
        {
            try
            {
                connection.Query("insert into [order] (totalprice, orderdate, mixId) " +
                             $"values({mix.Price}, '{DateTime.Now}', {mix.ID})");
            }
            catch { }
        }
        public static void RemoveOrder(int orderId)
        {
            connection.Query($"delete [dbo].[Order] where [Id] = {orderId}");
        }
        public static void RemoveMix(int mixId)
        {
            connection.Query($"delete [dbo].[MuesliMix] where [Id] = {mixId}");
        }
        public static MuesliMix GetMuesliMix(int mixId)
        {
            try
            {
                return connection.Query<MuesliMix>($"select * from MuesliMix where [Id] = {mixId}").AsList()[0];
            }
            catch { return null; }
            
        }
        public static List<Type> GetTypes()
        {
            return connection.Query<Type>("select * from type").AsList();
        }
        public static List<Ingredient> GetIngredientsByType(int typeId)
        {
            return connection.Query<Ingredient>($"select * from Ingredient where TypeId = {typeId}").AsList();
        }
    }
}
