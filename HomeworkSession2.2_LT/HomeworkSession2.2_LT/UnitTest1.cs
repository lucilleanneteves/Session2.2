using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;


[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]

namespace HomeworkSession2._2_LT
{
    [TestClass]
    public class UnitTest1
    {
        private static RestClient restClient;

        private static readonly string BaseURL = "https://petstore.swagger.io/v2/";

        private static readonly string PetsEndpoint = "pet";

        private static string GetURL(string enpoint) => $"{BaseURL}{enpoint}";

        private static Uri GetURI(string endpoint) => new Uri(GetURL(endpoint));

        private readonly List<PetModel> cleanUpList = new List<PetModel>();

        [TestInitialize]
        public async Task TestInitialize()
        {
            restClient = new RestClient();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            foreach (var data in cleanUpList)
            {
                var restRequest = new RestRequest(GetURI($"{PetsEndpoint}/{data.Id}"));
                var restResponse = await restClient.DeleteAsync(restRequest);
            }
        }

        [TestMethod]
        public async Task PostPetMethod()
        {
            // Create Data
            List<Tag> tags = new List<Tag>();
            tags.Add(new Tag()
            {
                Id = 101,
                Name = "TagOne"
            });
            tags.Add(new Tag()
            {
                Id = 102,
                Name = "TagTwo"
            });

            PetModel petData = new PetModel()
            {
                Id = 40,
                Category = new Category()
                {
                    Id = 98,
                    Name = "Chihuahua"
                },
                Name = "Choco",
                PhotoUrls = new List<string> { "photoUrl1", "photoUrl2" },
                Tags = tags,
                Status = "pending",
            };

            // Send Post Request
            var temp = GetURI(PetsEndpoint);
            var postRestRequest = new RestRequest(GetURI(PetsEndpoint)).AddJsonBody(petData);
            var postRestResponse = await restClient.ExecutePostAsync(postRestRequest);

            //Verify POST request status code
            Assert.AreEqual(HttpStatusCode.OK, postRestResponse.StatusCode, "Status code is not equal to 200");

            var restRequest = new RestRequest(GetURI($"{PetsEndpoint}/{petData.Id}"), Method.Get);
            var restResponse = await restClient.ExecuteAsync<PetModel>(restRequest);
          
            Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode, "Status code is not equal to 200");
            Assert.AreEqual(petData.Id, restResponse.Data.Id, "Pet ID did not match.");
            Assert.AreEqual(petData.Name, restResponse.Data.Name, "Pet Name did not match.");
            Assert.AreEqual(petData.Status, restResponse.Data.Status, "Status did not match.");
            Assert.AreEqual(petData.Category.Id, restResponse.Data.Category.Id, "Category ID did not match.");
            Assert.AreEqual(petData.Category.Name, restResponse.Data.Category.Name, "Category Name did not match.");
            Assert.AreEqual(petData.PhotoUrls[0], restResponse.Data.PhotoUrls[0], "PhotoURLS did not match.");
            Assert.AreEqual(petData.Tags[0].Id, restResponse.Data.Tags[0].Id, "Tag ID did not match.");
            Assert.AreEqual(petData.Tags[0].Name, restResponse.Data.Tags[0].Name, "Tag Name did not match."); 
            
            cleanUpList.Add(petData);
            
        }
    }
}