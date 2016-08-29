using GoodBadStuff.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodBadStuff.Models
{
    public class SQL
    {
        TravelInfoDb travelInfoDb = new TravelInfoDb();
        //  const string CON_STR = "Server=tcp:trvlr.database.windows.net,1433;Initial Catalog=TRVLRdb;Persist Security Info=False;User ID=trvlr;Password=Secret123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        const string CON_STR = @"Data Source=trvlr.database.windows.net;Initial Catalog=TRVLRdb;Persist Security Info=True;User ID=trvlr;Password=Secret123";

        public  void GetValuesFromAPIs(TravelInfo travelInfo, string json)
        {

            travelInfoDb.FromAddress = travelInfo.FromAddress;
            travelInfoDb.ToAddress = travelInfo.ToAddress;
            travelInfoDb.Transport = travelInfo.Transport;
            JObject o = JObject.Parse(json);

            string tempDist = (string)o.SelectToken("emissions[1].routedDistance");
            travelInfoDb.Distance = Convert.ToSingle(tempDist);

            switch (travelInfoDb.Transport)
            {
                case "BICYCLE":
                        GetCo2(o, 0);
                        break;
                case "WALKING":
                        GetCo2(o, 1);
                        break;
                case "TRAIN":
                        GetCo2(o, 2);
                        break;
                case "BUS":
                        GetCo2(o, 3);
                        break;
                case "DRIVING":
                        GetCo2(o, 7);
                        break;
            }
            AddNewTravel(travelInfoDb);
        }

        private void GetCo2(JObject o, int i)
        {
            string tempCo2 = (string)o.SelectToken($"emissions[{i}].totalCo2");
            float Co2 = Convert.ToSingle(tempCo2);
            travelInfoDb.Co2 = Co2;
        }

        public static void AddNewTravel(TravelInfoDb travelInfoDb)
        {
            SqlConnection myConnection = new SqlConnection(CON_STR);
            SqlCommand myCommand = new SqlCommand();

            //myCommand.CommandText = $"insert into TravelInfo (Transport, Co2, Date, TreeCount, Distance, FromAddress, ToAddress) values ('{travelInfoDb.Transport}', {travelInfoDb.Co2}, {travelInfoDb.Created}, {travelInfoDb.TreeCount}, {travelInfoDb.Distance}, '{travelInfoDb.FromAddress}', '{travelInfoDb.ToAddress}')";
            myCommand.CommandText = $"insert into TravelInfo (Transport, Co2, FromAddress, ToAddress, Distance) values ('{travelInfoDb.Transport}', {travelInfoDb.Co2},'{travelInfoDb.FromAddress}', '{travelInfoDb.ToAddress}', {travelInfoDb.Distance})";

            myCommand.CommandType = System.Data.CommandType.Text;
            myCommand.Connection = myConnection;
            
            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }

        }
       

        //public static Contact GetContact(string cid)
        //{
        //    Contact tmpContact = null;
        //    SqlConnection myConnection = new SqlConnection(CON_STR);
        //    SqlCommand myCommand = new SqlCommand("select * from Contact WHERE ID=" + cid, myConnection);

        //    try
        //    {
        //        myConnection.Open();

        //        SqlDataReader myReader = myCommand.ExecuteReader();
        //        while (myReader.Read())
        //        {
        //            string id = myReader["ID"].ToString();
        //            string firstname = myReader["firstname"].ToString();
        //            string lastname = myReader["lastname"].ToString();

        //            tmpContact = new Contact(id, firstname, lastname);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Response.Write($"<script>alert('{ex.Message}');</script>");
        //    }
        //    finally
        //    {
        //        myConnection.Close();
        //    }

        //    return tmpContact;
        //}

        //public static List<Contact> LoadContacts()
        //{
        //    SqlConnection myConnection = new SqlConnection(CON_STR);
        //    SqlCommand myCommand = new SqlCommand("select * from Contact order by ID", myConnection);

        //    List<Contact> myContacts = new List<Contact>();
        //    try
        //    {
        //        myConnection.Open();  
        //        SqlDataReader myReader = myCommand.ExecuteReader();
        //        while (myReader.Read())
        //        {
        //            string id = myReader["ID"].ToString();
        //            string firstname = myReader["firstname"].ToString();
        //            string lastname = myReader["lastname"].ToString();

        //            myContacts.Add(new Contact(id, firstname, lastname));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Response.Write($"<script>alert('{ex.Message}');</script>");
        //    }
        //    finally
        //    {
        //        myConnection.Close();
        //    }
        //    return myContacts;
        //}

        //public static void UpdateContact(string cid, string firstname, string lastname)
        //{

        //    SqlConnection myConnection = new SqlConnection(CON_STR);
        //    SqlCommand myCommand = new SqlCommand($"update Contact set FirstName='{firstname}', LastName='{lastname}' WHERE ID=" + cid, myConnection);

        //    try
        //    {
        //        myConnection.Open();

        //        //SqlDataReader myReader = myCommand.ExecuteReader();
        //        //while (myReader.Read())
        //        //{
        //        //    string id = myReader["ID"].ToString();
        //        //    string firstname = myReader["firstname"].ToString();
        //        //    string lastname = myReader["lastname"].ToString();

        //        //    tmpContact = new Contact(id, firstname, lastname);
        //        myCommand.ExecuteNonQuery();

        //    }
        //    catch (Exception ex)
        //    {
        //        //Response.Write($"<script>alert('{ex.Message}');</script>");
        //    }
        //    finally
        //    {
        //        myConnection.Close();
        //    }


        //}

        //public static Contact DeleteContact(string cid)
        //{
        //    Contact tmpContact = null;
        //    SqlConnection myConnection = new SqlConnection(CON_STR);
        //    SqlCommand myCommand = new SqlCommand();

        //    myCommand.CommandText = $"delete from Contact where Id = '{Convert.ToInt32(cid)}'";
        //    myCommand.Connection = myConnection;

        //    try
        //    {
        //        myConnection.Open();

        //        SqlDataReader myReader = myCommand.ExecuteReader();
        //        while (myReader.Read())
        //        {
        //            string id = myReader["ID"].ToString();
        //            string firstname = myReader["firstname"].ToString();
        //            string lastname = myReader["lastname"].ToString();

        //            tmpContact = new Contact(id, firstname, lastname);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Response.Write($"<script>alert('{ex.Message}');</script>");
        //    }
        //    finally
        //    {
        //        myConnection.Close();
        //    }

        //    return tmpContact;
        //}
    }
}
