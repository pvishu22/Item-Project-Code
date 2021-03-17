using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1.Models;
using WebApplication1.Controllers;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace ItemTestCases
{
    [TestClass]
    public class TestItemController
    {
        [TestMethod]
        public void GetAllItems_ShouldReturnAllItems()
        {
            List<tblItemList> testItems = GetTestItems();
            ItemController controller = new ItemController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            List<tblItemList> result = controller.GetItems();
            Assert.AreEqual(testItems.Count, result.Count);
        }

        [TestMethod]
        public void GetItem_ShouldReturnCorrectItem()
        {
            List<tblItemList> testitems = GetTestItems();
            ItemController controller = new ItemController();

            tblItemList result = controller.GetItemDetailsById(4);
            Assert.IsNotNull(result);
            Assert.AreEqual(testitems[3].Id, result.Id);
            Assert.AreEqual(testitems[3].Name, result.Name);
            Assert.AreEqual(testitems[3].Description, result.Description);
            Assert.AreEqual(testitems[3].Price, result.Price);
            Assert.AreEqual(testitems[3].ImageUrl, result.ImageUrl);
        }      

        [TestMethod]
        public void GetItem_ShouldNotFindItem()
        {
            ItemController controller = new ItemController();

            tblItemList result = controller.GetItemDetailsById(999);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void AddItem_ShouldAddItem()
        {
            List<tblItemList> testitems = GetTestItems();
            ItemController controller = new ItemController();
            tblItemList objtblItemList = new tblItemList { Id = 5, Name = "Product5", Description = "Product 5 Description", Price = 1600, ImageUrl = "" };
            testitems.Add(objtblItemList);
            tblItemList result = controller.PostItem(objtblItemList);
            Assert.IsNotNull(result);
            Assert.AreEqual(testitems[4].Id, result.Id);
            Assert.AreEqual(testitems[4].Name, result.Name);
            Assert.AreEqual(testitems[4].Description, result.Description);
            Assert.AreEqual(testitems[4].Price, result.Price);                    
        }

        //[TestMethod]
        //public void GetImage_ShouldReturnCorrectItemImage()
        //{
        //    List<tblItemList> testitems = GetTestItems();
        //    ItemController controller = new ItemController();
        //    controller.Request = new HttpRequestMessage();
        //    controller.Configuration = new HttpConfiguration();
        //    var resp = controller.GetImage(1);
        //    string FileName = resp.Content.Headers.ContentDisposition.FileName;
        //    //Assert.IsNotNull(resp);
        //    Assert.IsNotNull(FileName);            
        //}

        //[TestMethod]
        //public void GetImage_ShouldReturnNoImageFoundImageForItem()
        //{
        //    List<tblItemList> testitems = GetTestItems();
        //    ItemController controller = new ItemController();
        //    controller.Request = new System.Net.Http.HttpRequestMessage();
        //    controller.Configuration = new HttpConfiguration();
        //    var resp = controller.GetImage(4);
        //    string FileName = resp.Content.Headers.ContentDisposition.FileName;
        //    //Assert.IsNotNull(resp);
        //    Assert.AreEqual(FileName, "no-image.png");
        //}

        [TestMethod]
        public void DeleteItem_ShouldDeleteProduct()
        {
            List<tblItemList> testitems = GetTestItems();
            ItemController controller = new ItemController();            
            tblItemList result = controller.DeleteItem(4);           
            testitems = GetTestItems();            
            List<tblItemList> AllResult = controller.GetItems();
            Assert.AreEqual(testitems.Count, AllResult.Count);
            Assert.IsNotNull(result);            
        }          

        private List<tblItemList> GetTestItems()
        {
            List<tblItemList> testitems = new List<tblItemList>();
            testitems.Add(new tblItemList { Id = 1, Name = "Product1", Description = "Product 1 Description", Price = 100, ImageUrl = "file_examp-1-210826012" });
            testitems.Add(new tblItemList { Id = 2, Name = "Product2", Description = "Product 2 Description", Price = 200, ImageUrl = "file_examp-2-210939607" });
            testitems.Add(new tblItemList { Id = 3, Name = "Product3", Description = "Product 3 Description", Price = 400, ImageUrl = "file_examp-3-211007169" });
            testitems.Add(new tblItemList { Id = 4, Name = "Product4", Description = "Product 4 Description", Price = 800, ImageUrl = "" });

            return testitems;
        }
    }
}
         

