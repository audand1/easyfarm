using EasyFarm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace EasyFarm.Controllers
{
    public class FieldsController : ApiController
    {
        // GET api/fields
        public IEnumerable<Field> Get()
        {
            string user_id = string.Empty;
            IEnumerable<string> values = new List<string>();
            if (this.Request.Headers.TryGetValues("Authorization", out values))
            {
                user_id = AuthChecker.Instance.chechAuth(values.ElementAt(0));
                if(user_id.Equals(string.Empty))
                {
                    HttpResponseMessage resp = new HttpResponseMessage();
                resp.StatusCode = HttpStatusCode.Unauthorized;
                resp.Content = new StringContent("{\"Message\":\"Unauthorized\"}", Encoding.UTF8, "application/json");
                throw new HttpResponseException(resp);
                }
            }
            else
            {
                HttpResponseMessage resp = new HttpResponseMessage();
                resp.StatusCode = HttpStatusCode.Unauthorized;
                resp.Content = new StringContent("{\"Message\":\"Unauthorized\"}", Encoding.UTF8, "application/json");
                throw new HttpResponseException(resp);
            }



            List<Field> fieldList = DBConnection.Instance.getFields(user_id);

            if(fieldList.Count == 0)
            {
                HttpResponseMessage resp = new HttpResponseMessage();
                resp.StatusCode = HttpStatusCode.NotFound;
                resp.Content = new StringContent("{\"Message\":\"Fields List Not Found\"}", Encoding.UTF8, "application/json");
                throw new HttpResponseException(resp);
            }
            return fieldList;

                       
        }

        // GET api/fields/5
        public Field Get(int id)
        {
            string user_id = string.Empty;
            IEnumerable<string> values = new List<string>();
            if (this.Request.Headers.TryGetValues("Authorization", out values))
            {
                user_id = AuthChecker.Instance.chechAuth(values.ElementAt(0));
                if (user_id.Equals(string.Empty))
                {
                    HttpResponseMessage resp = new HttpResponseMessage();
                    resp.StatusCode = HttpStatusCode.Unauthorized;
                    resp.Content = new StringContent("{\"Message\":\"Unauthorized\"}", Encoding.UTF8, "application/json");
                    throw new HttpResponseException(resp);
                }
            }
            else
            {
                HttpResponseMessage resp = new HttpResponseMessage();
                resp.StatusCode = HttpStatusCode.Unauthorized;
                resp.Content = new StringContent("{\"Message\":\"Unauthorized\"}", Encoding.UTF8, "application/json");
                throw new HttpResponseException(resp);
            }


            Field field = DBConnection.Instance.getField(id,user_id);
            if (field.Id != id)
            {
                HttpResponseMessage resp = new HttpResponseMessage();
                resp.StatusCode = HttpStatusCode.NotFound;
                resp.Content = new StringContent("{\"Message\":\"Field Not Found\"}", Encoding.UTF8, "application/json");
                throw new HttpResponseException(resp);
            }

            return field;
            
        }

        // POST api/fields
        public Field Post([FromBody]Field value)
        {
            string user_id = string.Empty;
            IEnumerable<string> values = new List<string>();
            if (this.Request.Headers.TryGetValues("Authorization", out values))
            {
                user_id = AuthChecker.Instance.chechAuth(values.ElementAt(0));
                if (user_id.Equals(string.Empty))
                {
                    HttpResponseMessage resp = new HttpResponseMessage();
                    resp.StatusCode = HttpStatusCode.Unauthorized;
                    resp.Content = new StringContent("{\"Message\":\"Unauthorized\"}", Encoding.UTF8, "application/json");
                    throw new HttpResponseException(resp);
                }
            }
            else
            {
                HttpResponseMessage resp = new HttpResponseMessage();
                resp.StatusCode = HttpStatusCode.Unauthorized;
                resp.Content = new StringContent("{\"Message\":\"Unauthorized\"}", Encoding.UTF8, "application/json");
                throw new HttpResponseException(resp);
            }

            if(value.name.Length > 254 )
            {
                HttpResponseMessage resp = new HttpResponseMessage();
                resp.StatusCode = HttpStatusCode.BadRequest;
                resp.Content = new StringContent("{\"Message\":\"Bad 'Name' lenght\"}", Encoding.UTF8, "application/json");
                throw new HttpResponseException(resp);
            }

            if (value.location.Length > 499)
            {
                HttpResponseMessage resp = new HttpResponseMessage();
                resp.StatusCode = HttpStatusCode.BadRequest;
                resp.Content = new StringContent("{\"Message\":\"Bad 'Location' lenght\"}", Encoding.UTF8, "application/json");
                throw new HttpResponseException(resp);
            }

            if (value.last_action.Length > 49)
            {
                HttpResponseMessage resp = new HttpResponseMessage();
                resp.StatusCode = HttpStatusCode.BadRequest;
                resp.Content = new StringContent("{\"Message\":\"Bad 'Last Action' lenght\"}", Encoding.UTF8, "application/json");
                throw new HttpResponseException(resp);
            }
            


            Field field = DBConnection.Instance.insertField(value, user_id);
            if (field.Id == 0)
            {
                HttpResponseMessage resp = new HttpResponseMessage();
                resp.StatusCode = HttpStatusCode.BadRequest;
                resp.Content = new StringContent("{\"Message\":\"Insert error\"}", Encoding.UTF8, "application/json");
                throw new HttpResponseException(resp);
            }

            return field;            
        }

        // PUT api/fields/5
        public Field Put(int id, [FromBody]Field value)
        {
            string user_id = string.Empty;
            IEnumerable<string> values = new List<string>();
            if (this.Request.Headers.TryGetValues("Authorization", out values))
            {
                user_id = AuthChecker.Instance.chechAuth(values.ElementAt(0));
                if (user_id.Equals(string.Empty))
                {
                    HttpResponseMessage resp = new HttpResponseMessage();
                    resp.StatusCode = HttpStatusCode.Unauthorized;
                    resp.Content = new StringContent("{\"Message\":\"Unauthorized\"}", Encoding.UTF8, "application/json");
                    throw new HttpResponseException(resp);
                }
            }
            else
            {
                HttpResponseMessage resp = new HttpResponseMessage();
                resp.StatusCode = HttpStatusCode.Unauthorized;
                resp.Content = new StringContent("{\"Message\":\"Unauthorized\"}", Encoding.UTF8, "application/json");
                throw new HttpResponseException(resp);
            }

            if (value.name.Length > 254)
            {
                HttpResponseMessage resp = new HttpResponseMessage();
                resp.StatusCode = HttpStatusCode.BadRequest;
                resp.Content = new StringContent("{\"Message\":\"Bad 'Name' lenght\"}", Encoding.UTF8, "application/json");
                throw new HttpResponseException(resp);
            }

            if (value.location.Length > 499)
            {
                HttpResponseMessage resp = new HttpResponseMessage();
                resp.StatusCode = HttpStatusCode.BadRequest;
                resp.Content = new StringContent("{\"Message\":\"Bad 'Location' lenght\"}", Encoding.UTF8, "application/json");
                throw new HttpResponseException(resp);
            }

            if (value.last_action.Length > 49)
            {
                HttpResponseMessage resp = new HttpResponseMessage();
                resp.StatusCode = HttpStatusCode.BadRequest;
                resp.Content = new StringContent("{\"Message\":\"Bad 'Last Action' lenght\"}", Encoding.UTF8, "application/json");
                throw new HttpResponseException(resp);
            }
            

            Field field = DBConnection.Instance.updateField(id,value,user_id);
            if (field.Id == 0)
            {
                HttpResponseMessage resp = new HttpResponseMessage();
                resp.StatusCode = HttpStatusCode.BadRequest;
                resp.Content = new StringContent("{\"Message\":\"Update error\"}", Encoding.UTF8, "application/json");
                throw new HttpResponseException(resp);
            }

            return field;   
        }

        // DELETE api/fields/5
        public HttpResponseMessage Delete(int id)
        {
            string user_id = string.Empty;
            IEnumerable<string> values = new List<string>();
            if (this.Request.Headers.TryGetValues("Authorization", out values))
            {
                user_id = AuthChecker.Instance.chechAuth(values.ElementAt(0));
                if (user_id.Equals(string.Empty))
                {
                    HttpResponseMessage resp = new HttpResponseMessage();
                    resp.StatusCode = HttpStatusCode.Unauthorized;
                    resp.Content = new StringContent("{\"Message\":\"Unauthorized\"}", Encoding.UTF8, "application/json");
                    throw new HttpResponseException(resp);
                }
            }
            else
            {
                HttpResponseMessage resp = new HttpResponseMessage();
                resp.StatusCode = HttpStatusCode.Unauthorized;
                resp.Content = new StringContent("{\"Message\":\"Unauthorized\"}", Encoding.UTF8, "application/json");
                throw new HttpResponseException(resp);
            }

            HttpResponseMessage rmsg = new HttpResponseMessage();

            if (DBConnection.Instance.deleteField(id, user_id))
            {
                rmsg.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                rmsg.StatusCode = HttpStatusCode.BadRequest;
                rmsg.Content = new StringContent("{\"Message\":\"Delete error\"}", Encoding.UTF8, "application/json");
               
            }
           
            return rmsg;
           // return "OK_DELETE-" + id.ToString();
        }
    }
}
