using Clients_get_and_post_api.Controllers.EliteUsTrials;
using Clients_get_and_post_api.Controllers.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace Clients_get_and_post_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EliteUsTrialsController : ControllerBase
    {

        public readonly IConfiguration _configuration;
        public EliteUsTrialsController(IConfiguration configuration)
        {

            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetContact")]
        public string GetContact()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Connection").ToString());
            SqlDataAdapter da = new SqlDataAdapter("select * from contact", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Contact> contactList = new List<Contact>();
            Response res = new Response();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Contact contact = new Contact();
                    contact.contact_id = Convert.ToInt32(dt.Rows[i]["contact_id"]);
                    contact.name = Convert.ToString(dt.Rows[i]["name"]);
                    contact.email = Convert.ToString(dt.Rows[i]["email"]);
                    contact.Subject = Convert.ToString(dt.Rows[i]["Subject"]);
                    contact.message = Convert.ToString(dt.Rows[i]["message"]);

                    contactList.Add(contact);
                }
            }
            if (contactList.Count > 0)
            {
                return JsonConvert.SerializeObject(contactList);
            }
            else
            {
                res.StatusCode = 100;
                res.ErrorMessage = "No Data Found";
                return JsonConvert.SerializeObject(res);
            }
        }

        [HttpGet]
        [Route("GetSiteRegistration")]
        public string GetSiteRegistration()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Connection").ToString());
            SqlDataAdapter da = new SqlDataAdapter("select * from site_registration", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<SiteRegistration> registrationList = new List<SiteRegistration>();
            Response res = new Response();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SiteRegistration site = new SiteRegistration();
                    site.registration_id = Convert.ToInt32(dt.Rows[i]["registration_id"]);
                    site.organization = Convert.ToString(dt.Rows[i]["name_of_organization"]);
                    site.firstName = Convert.ToString(dt.Rows[i]["first_name"]);
                    site.lastName = Convert.ToString(dt.Rows[i]["last_name"]);
                    site.email = Convert.ToString(dt.Rows[i]["email"]);
                    site.phone = Convert.ToString(dt.Rows[i]["phone"]);
                    site.region = Convert.ToString(dt.Rows[i]["region"]);
                    site.country = Convert.ToString(dt.Rows[i]["country"]);
                    site.street = Convert.ToString(dt.Rows[i]["street"]);
                    site.city = Convert.ToString(dt.Rows[i]["city"]);
                    site.state = Convert.ToString(dt.Rows[i]["state"]);
                    site.zipCode = Convert.ToString(dt.Rows[i]["zip_code"]);


                    registrationList.Add(site);
                }
            }
            if (registrationList.Count > 0)
            {
                return JsonConvert.SerializeObject(registrationList);
            }
            else
            {
                res.StatusCode = 100;
                res.ErrorMessage = "No Data Found";
                return JsonConvert.SerializeObject(res);
            }
        }

        [HttpPost]
        [Route("PostSiteRegistration")]
        public async Task<IActionResult> PostSiteRegistration(SiteRegistration registration)
        {
            string connectionString = _configuration.GetConnectionString("Connection").ToString();

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                string insertQuery = "INSERT INTO site_registration" +
                    "(name_of_organization,first_name,last_name,email,phone,region,country,street,city,state,zip_code)" +
                    "VALUES (@name_of_organization, @first_name, @last_name, @email,@phone,@region,@country,@street,@city,@state,@zip_code)";
                SqlCommand cmd = new SqlCommand(insertQuery, con);

                cmd.Parameters.AddWithValue("@name_of_organization", registration.organization);
                cmd.Parameters.AddWithValue("@first_name", registration.firstName);
                cmd.Parameters.AddWithValue("@last_name", registration.lastName);
                cmd.Parameters.AddWithValue("@email", registration.email);
                cmd.Parameters.AddWithValue("@phone", registration.phone);
                cmd.Parameters.AddWithValue("@region", registration.region);
                cmd.Parameters.AddWithValue("@country", registration.country);
                cmd.Parameters.AddWithValue("@street", registration.street);
                cmd.Parameters.AddWithValue("@city", registration.city);
                cmd.Parameters.AddWithValue("@state", registration.state);
                cmd.Parameters.AddWithValue("@zip_code", registration.zipCode);
                await cmd.ExecuteNonQueryAsync();
            }

            return Ok();
        }

        [HttpPost]
        [Route("PostContact")]
        public async Task<IActionResult> PostContact(Contact contact)
        {
            string connectionString = _configuration.GetConnectionString("Connection").ToString();

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                string insertQuery = "INSERT INTO contact (name, email, Subject, message) VALUES (@name, @email, @Subject, @message)";
                SqlCommand cmd = new SqlCommand(insertQuery, con);

                cmd.Parameters.AddWithValue("@name", contact.name);

                cmd.Parameters.AddWithValue("@email", contact.email);
                cmd.Parameters.AddWithValue("@Subject", contact.Subject);

                cmd.Parameters.AddWithValue("@message", contact.message);

                await cmd.ExecuteNonQueryAsync();
            }

            return Ok();
        }
    }

}


