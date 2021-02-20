using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory_Management_System.Models;
using Inventory_Management_System.Models.ViewModels;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Web.Script.Serialization;


namespace Inventory_Management_System.Controllers
{
    public class HopController : Controller
    {
        //Http Client is the proper way to connect to a webapi
        //https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-5.0

        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;


        static HopController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };
            client = new HttpClient(handler);
            //change this to match your own local port number
            client.BaseAddress = new Uri("https://localhost:44367/api/");
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));


            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);

        }


        /// <summary>
        /// Returns a list of Hops
        /// </summary>
        /// <returns>Returns the list of all hops in the database</returns>
        // GET: Hop/List
        public ActionResult List()
        {
            string url = "hopdata/gethops";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<HopDTO> SelectedHops = response.Content.ReadAsAsync<IEnumerable<HopDTO>>().Result;
                return View(SelectedHops);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        ///<results>Returns Hop list</results>

        /// <summary>
        /// Returns the specific Details of a specific hop
        /// </summary>
        /// <param name="id">The ID of a specific hop,</param>
        /// <returns>All details on a specific hop</returns>
       // GET: Hop/Details/5
       public ActionResult Details(int id)
       {
           ShowHop ViewModel = new ShowHop();
           string url = "hopdata/findhop/" + id;
           HttpResponseMessage response = client.GetAsync(url).Result;
           //Can catch the status code (200 OK, 301 REDIRECT), etc.
           Debug.WriteLine(response.StatusCode);
           if (response.IsSuccessStatusCode)
           {
               //Put data into Hop data transfer object
               HopDTO SelectedHop = response.Content.ReadAsAsync<HopDTO>().Result;
               ViewModel.hop = SelectedHop;


               url = "hopdata/findHopClassificationforHop/" + id;
               response = client.GetAsync(url).Result;
               HopClassificationDto SelectedHopClassification = response.Content.ReadAsAsync<HopClassificationDto>().Result;
               ViewModel.hopClassification = SelectedHopClassification;

               return View(ViewModel);
           }
           else
           {
               return RedirectToAction("Error");
           }
       }
        ///<results>Returns details on a specific hop</results>


        //GET: Hop/Create
        /// <summary>
        /// Creates a new hop
        /// </summary>
        /// <returns>
        /// This is where my issues began, I was able to create a new hop
        /// but I could not get the ID to automaticaly increment. I was
        /// only able to get that far by using a try and catch statement 
        /// there is something wrong with line 127 but I can not for the
        /// life of me get it to work
        /// 
        /// </returns>
        public ActionResult Create()
       {
           return View();
       }

       // POST: Hop/Create
       [HttpPost]
       [ValidateAntiForgeryToken()]
       public ActionResult Create(Hop HopInfo)
       {
           Debug.WriteLine(HopInfo.HopName);
           string url = "hopdata/addHop";
           Debug.WriteLine(jss.Serialize(HopInfo));
           HttpContent content = new StringContent(jss.Serialize(HopInfo));
           content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
           HttpResponseMessage response = client.PostAsync(url, content).Result;

           if (response.IsSuccessStatusCode)
           {
                try {
                    int hopid = response.Content.ReadAsAsync<int>().Result;//MAMA MIA HERE IS AN ERROR FOR THE CREATING
                    return RedirectToAction( "Details", new {
                        id = hopid
                    } );

                } catch( Exception e ) {
                    Debug.WriteLine(e);
                    return RedirectToAction("List");
                }
           }
           else
           {
               return RedirectToAction("Error");
           }
       }
        ///<results>Creates a new hop, but I cant get the id to return</results>


        /// <summary>
        /// I was able to get the information of a
        /// specific hop and generate the correct form but
        /// I was unable to determine why the updates would not follow through
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET: Hop/Edit/5
        public ActionResult Edit(int id)
       {
           UpdateHop ViewModel = new UpdateHop();

           string url = "Hopdata/findHop/" + id;
           HttpResponseMessage response = client.GetAsync(url).Result;
           //Can catch the status code (200 OK, 301 REDIRECT), etc.
           //Debug.WriteLine(response.StatusCode);
           if (response.IsSuccessStatusCode)
           {
               //Put data into Hop data transfer object
               HopDTO SelectedHop = response.Content.ReadAsAsync<HopDTO>().Result;
               ViewModel.hop = SelectedHop;

        //I suspect the error could be caused in the following 4 lines of code
               url = "HopClassificationdata/getHopClassifications";
               response = client.GetAsync(url).Result;
               IEnumerable<HopClassificationDto> PotentialHops = response.Content.ReadAsAsync<IEnumerable<HopClassificationDto>>().Result;
               ViewModel.allhopclassifications = PotentialHops;

               return View(ViewModel);
           }
           else
           {
               return RedirectToAction("Error");
           }
       }
        ///<results>Edits a specific hop, Does not update the informmation </results>


        /// <summary>
        /// The following is for security reasons as i understand it,
        /// The validantiforgerytoken helps to protect the database, though 
        /// the specifics of how it acomplishes that is beond me
        /// </summary>
        /// <param name="id">specific hop id</param>
        /// <param name="HopInfo">The information on a hop</param>
        /// <returns></returns>
        // POST: Hop/Edit/5
        [HttpPost]
       [ValidateAntiForgeryToken()]
       
       public ActionResult Edit(int id, Hop HopInfo)
       {
            
           Debug.WriteLine(HopInfo.HopName);
           string url = "Hopdata/updateHop/" + id;
           Debug.WriteLine(jss.Serialize(HopInfo));
           HttpContent content = new StringContent(jss.Serialize(HopInfo));
           content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
           HttpResponseMessage response = client.PostAsync(url, content).Result;
           Debug.WriteLine(response.StatusCode);
           if (response.IsSuccessStatusCode)
           {

               return RedirectToAction("Details", new { id = id });
           }
           else
           {
               return RedirectToAction("Error");
           }
       }
        /// <summary>
        /// The following deletes a specific hop
        /// </summary>
        /// <param name="id">The ID of a specific hop</param>
        /// <returns>Deletes the hop</returns>
       // GET: Hop/Delete/5
       [HttpGet]
       public ActionResult DeleteConfirm(int id)
       {
           string url = "Hopdata/findHop/" + id;
           HttpResponseMessage response = client.GetAsync(url).Result;
           //Can catch the status code (200 OK, 301 REDIRECT), etc.
           //Debug.WriteLine(response.StatusCode);
           if (response.IsSuccessStatusCode)
           {
               //Put data into Hop data transfer object
               HopDTO SelectedHop = response.Content.ReadAsAsync<HopDTO>().Result;
               return View(SelectedHop);
           }
           else
           {
               return RedirectToAction("Error");
           }
       }
        ///<results>Does not delete a hop as intended,
        ///this may be due to the issue with the ID's not incrementing
        ///</results>


        /// <summary>
        /// Same as aboves security measures
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: Hop/Delete/5
        [HttpPost]
       [ValidateAntiForgeryToken()]
       public ActionResult Delete(int id)
       {
           string url = "Hopdata/deleteHop/" + id;
           //post body is empty
           HttpContent content = new StringContent("");
           HttpResponseMessage response = client.PostAsync(url, content).Result;
           //Can catch the status code (200 OK, 301 REDIRECT), etc.
           //Debug.WriteLine(response.StatusCode);
           if (response.IsSuccessStatusCode)
           {

               return RedirectToAction("List");
           }
           else
           {
               return RedirectToAction("Error");
           }
           
       }
       /// <summary>
       /// Error message
       /// </summary>
       /// <returns>Returns Error messages</returns>
        public ActionResult Error()
        {
            return View();
        }
        
    }
    ///I saw alot more of the above then id like 
}
