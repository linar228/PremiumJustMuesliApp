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

        public static bool IsLoginCorrect(LoginModel model)
        {
            return connection.Query<User>("SELECT * FROM [dbo].[User] u " +
                                            $"where u.[Login] = '{model.Email}'" +
                                            $"and u.[Password] = '{model.Password}'").AsList().Count > 0;
        }
        public static bool RegisterUser(RegisterModel model)
        {
            try
            {
                connection.Query($"insert into [dbo].[User] (Email, [Password]) " +
                    $"values('{model.Email}', '{model.Password}')");
                return true;
            }
            catch
            {
                return false;
            }
        }
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
        public static void AddMuesli(Muesli m)
        {
            try
            {
                connection.Query($"insert into MuesliMix ([Name],[CreatedDate],[Basics],[Cereal],[Fruit],[Nuts],[Choco],[Specials])" +
                $" values ('{m.Name}', '{DateTime.Now}','{m.Basics}','{m.Cereal}','{m.Fruit}','{m.Nuts}','{m.Choco}','{m.Specials}')");
            }
            catch { }
        }
        public static List<Ingredient> GetBasics()
        {
            return connection.Query<Ingredient>("select * from Muesli where[TypeId] = 1").AsList();
        }
        public static List<Ingredient> GetCereals()
        {
            return connection.Query<Ingredient>("select * from Muesli where[TypeId] = 2").AsList();
        }
        public static List<Ingredient> GetFruits()
        {
            return connection.Query<Ingredient>("select * from Muesli where[TypeId] = 3").AsList();
        }
        public static List<Ingredient> GetNuts()
        {
            return connection.Query<Ingredient>("select * from Muesli where[TypeId] = 4").AsList();
        }
        public static List<Ingredient> GetChocos()
        {
            return connection.Query<Ingredient>("select * from Muesli where[TypeId] = 5").AsList();
        }
        public static List<Ingredient> GetSpecials()
        {
            return connection.Query<Ingredient>("select * from Muesli where[TypeId] = 6").AsList();
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
    }
}
