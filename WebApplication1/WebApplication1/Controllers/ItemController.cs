using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web;
using WebApplication1.Models;
using System.IO;
using System.Net.Http.Headers;

namespace WebApplication1.Controllers
{
    [RoutePrefix("Api/Item")]
    [AllowAnonymous]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ItemController : ApiController
    {
        ItemListEntities objEntity = new ItemListEntities();

        [HttpGet] 
        [Route("ItemDetails")]        
        public List<tblItemList> GetItems()
        {
            try
            {                
                List<tblItemList> objtblItemList = objEntity.tblItemLists.ToList();
                if (System.Web.HttpContext.Current != null)
                {
                    foreach (tblItemList tblItemList in objtblItemList)
                    {

                        byte[] bytes;
                        if (tblItemList.ImageUrl != null && tblItemList.ImageUrl != "")
                        {
                            bytes = File.ReadAllBytes(System.Web.HttpContext.Current.Server.MapPath("~/Files/") + "" + tblItemList.ImageUrl);
                        }
                        else
                        {
                            bytes = File.ReadAllBytes(System.Web.HttpContext.Current.Server.MapPath("~/Files/no-image.png"));
                        }

                        tblItemList.ImageUrl = Convert.ToBase64String(bytes, 0, bytes.Length);

                    }
                }                
                return objtblItemList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[Route("api/ImageAPI/GetImages")]
        //public HttpResponseMessage GetImages()
        //{
        //    List<tblItemList> objtblItemList = objEntity.tblItemLists.ToList();
                        
        //    foreach (tblItemList tblItemList in objtblItemList)
        //    {                
        //        //Read the Image as Byte Array.
        //        byte[] bytes = File.ReadAllBytes("~/Images/" + tblItemList.ImageUrl);

        //        tblItemList.ImageUrl = Convert.ToBase64String(bytes, 0, bytes.Length);                
        //    }

        //    return Request.CreateResponse(HttpStatusCode.OK, images);
        //}


        [HttpGet]
        [Route("GetItemDetailsById/{Id}")]
        public tblItemList GetItemDetailsById(int Id)
        {
            tblItemList objtblItemList = new tblItemList();
            int ID = Convert.ToInt32(Id);
            try
            {
                objtblItemList = objEntity.tblItemLists.Find(ID);               
                return objtblItemList;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        [HttpPost]
        [Route("InsertItemDetails")]
        public tblItemList PostItem(tblItemList data)
        {            
            try
            {             
                objEntity.tblItemLists.Add(data);
                objEntity.SaveChanges();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("UploadImage")]
        public tblItemList PostItemImage()
        {
            try
            {
                string imageName = null;
                Int32 ItemId = 0;
                var httpRequest = HttpContext.Current.Request;
                var postedImage = httpRequest.Files["Image"];
                if(httpRequest.Form["Id"] != null)
                {
                    ItemId = Convert.ToInt32(httpRequest.Form["Id"]);
                }
                
                if (postedImage != null)
                {
                    imageName = new String(Path.GetFileNameWithoutExtension(postedImage.FileName).Take(10).ToArray()).Replace(" ", "-");
                    imageName = imageName + "-" + ItemId + '-' + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedImage.FileName);
                    var filePath = HttpContext.Current.Server.MapPath("~/Files/" + imageName);
                    postedImage.SaveAs(filePath);
                }
                
                tblItemList Item = objEntity.tblItemLists.FirstOrDefault(u => u.Id == ItemId);
                Item.ImageUrl = imageName;
                objEntity.SaveChanges();

                byte[] bytes;
                if (Item.ImageUrl != null && Item.ImageUrl != "")
                {
                    bytes = File.ReadAllBytes(System.Web.HttpContext.Current.Server.MapPath("~/Files/") + "" + Item.ImageUrl);
                }
                else
                {
                    bytes = File.ReadAllBytes(System.Web.HttpContext.Current.Server.MapPath("~/Files/no-image.png"));
                }

                Item.ImageUrl = Convert.ToBase64String(bytes, 0, bytes.Length);

                return Item;              
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetImage/{Id}")]        
        public HttpResponseMessage GetImage(int Id)
        {
            tblItemList Item = objEntity.tblItemLists.FirstOrDefault(u => u.Id == Id);
            string image = Item.ImageUrl;                      
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);                        
             string filePath = System.Web.HttpContext.Current.Server.MapPath("~/Files/") + image;                     
            if (!File.Exists(filePath))
            {

                filePath = System.Web.HttpContext.Current.Server.MapPath("~/Files/no-image.png");
                image = "no-image.png";                
            }            
            byte[] bytes = File.ReadAllBytes(filePath);            
            response.Content = new ByteArrayContent(bytes);            
            response.Content.Headers.ContentLength = bytes.LongLength;            
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = image;            
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(image));
            return response;
        }

        [HttpDelete]
        [Route("DeleteItem/{Id}")]
        public tblItemList DeleteItem(int id)
        {            
            tblItemList objItem = objEntity.tblItemLists.Find(id);            

            objEntity.tblItemLists.Remove(objItem);
            objEntity.SaveChanges();
            return objItem;            
        }
    }
}