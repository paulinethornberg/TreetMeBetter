using GoodBadStuff.Models;
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
        const string CON_STR = "Server=tcp:trvlr.database.windows.net,1433;Initial Catalog=TRVLRdb;Persist Security Info=False;User ID=trvlr;Password=Secret123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
      

        public static void AddNewTravel(TravelInfoDb travelInfoDb)
        {
            SqlConnection myConnection = new SqlConnection(CON_STR);
            SqlCommand myCommand = new SqlCommand();

            myCommand.CommandText = $"insert into TravelInfo (Transport, Co2, Date, TreeCount, Distance, FromAddress, ToAddress, UserId) values ('{travelInfoDb.Transport}', '{travelInfoDb.Co2}', '{travelInfoDb.Created}', '{travelInfoDb.FromAddress}', '{travelInfoDb.ToAddress}', '{travelInfoDb.UserId}')";
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
                myConnection.Close();
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
