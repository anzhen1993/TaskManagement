using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManagement.Models;

namespace TaskManagement.Controllers.Api
{
    public class QuoteController : ApiController
    {
        private QuotesDBEntities DB_Enitity;

        public QuoteController()
        {
            DB_Enitity = new QuotesDBEntities();
        }
        public IEnumerable<Quote> Get()
        {
            return DB_Enitity.Quote.ToList();
        }
        // GET 
        public Quote Get(int id)
        {
            Quote quote = DB_Enitity.Quote.Find(id);

            if (quote == null)
            {
                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("ID does not exist!!!")
                };
                throw new HttpResponseException(message);
            }
               

            return quote;
        }
        // POST 
        [HttpPost]
        public IHttpActionResult Create(Quote quote)
        {
            if (!ModelState.IsValid)
                    return BadRequest();

            DB_Enitity.Quote.Add(quote);
            DB_Enitity.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + quote.Id), quote);
        }
        // PUT 
        [HttpPut]
        public void Update(int id, Quote quote)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var quoteInDb = DB_Enitity.Quote.SingleOrDefault(q => q.Id == id);

            if (quoteInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            quoteInDb.QuoteType = quote.QuoteType;
            quoteInDb.Contact = quote.Contact;
            quoteInDb.Task = quote.Task;
            quoteInDb.DueDate = quote.DueDate;
            quoteInDb.TaskType = quote.TaskType;

            DB_Enitity.SaveChanges();
        }

        // DELETE 
        [HttpDelete]
        public void Delete(int id)
        {
            var quoteInDb = DB_Enitity.Quote.SingleOrDefault(q => q.Id == id);

            if (quoteInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            DB_Enitity.Quote.Remove(quoteInDb);
            DB_Enitity.SaveChanges();
        }
    }
}
