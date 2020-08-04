using cobamvc.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml.Linq;

namespace cobamvc.Controllers
{
    public class CobaController : Controller
    {

        private string ClientId = "5adc053b-8d5c-48d1-9c0e-180e5cd491d50f7B"; // dari central auth
        private string ClientSecret = "2g7WCZnMFOZdIBklFwTnS4H"; // dari central auth
        private string ApiName = "cb"; //dari central auth
        private string CentralAuthUri = "https://localhost:44308"; 
        private string GeneralApprovalUri = "https://localhost:4432"; //
        private string Nik = "000"; //nik user


        public ActionResult Index()
        {
            return View();
        }

        private async Task<string> GetToken()
        {
            var content = new FormUrlEncodedContent(new[]  // jika body formatnya x-form-urlencoded
           {
                new KeyValuePair<string, string>("client_id", this.ClientId),
                new KeyValuePair<string, string>("client_secret", this.ClientSecret),
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("scope", this.ApiName),
            });

            HttpClientHandler clientHandler = new HttpClientHandler();
            var server = HttpClientFactory.Create(clientHandler);
            var tokenRes = await server.PostAsync(this.CentralAuthUri + "/connect/token", content);
            if (tokenRes.IsSuccessStatusCode)
            {
                var d = await tokenRes.Content.ReadAsAsync<dynamic>();
                var t = d.access_token;
                var access_token = t.ToString();
                return access_token;
            }
            return null;
        }
        public async System.Threading.Tasks.Task<ActionResult> SendRequestAsync()
        {
            var datareq = new RequestData
            {
                apiName = this.ApiName,
                category = "MOU Baru",
                detail = JsonConvert.SerializeObject(this.CreateFormMdif()),
                id = "002/CBK/NS/VII/2020",
                nik = this.Nik,
                projectName ="Monitoring Dana Investasi Franchise",
                status ="Menunggu Approval",
                title = "MOU Toko Baru telah dibuat",
                urlAction = "http://localhost/cb/Coba/Approve",
                urlProject = "http://localhost/cb",//"http://localhost:44305",
                dataNotif = new NotifConfig
                {
                    title = "Monitoring Dana Investasi Franchise",
                    message = "MOU Toko Baru telah dibuat"
                }
            };

            HttpClientHandler clientHandler = new HttpClientHandler();
            //clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var server = HttpClientFactory.Create(clientHandler);
            var access_token = await this.GetToken();
            if (access_token != null)
            {
                var ggg = JsonConvert.SerializeObject(datareq);
                var requestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    Content = new StringContent(ggg, Encoding.UTF8, "application/json"), //jika body formanya json
                    RequestUri = new Uri(this.GeneralApprovalUri + "/api/external/add")
                };

                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                var response = await server.SendAsync(requestMessage);
            }
            

            return Json(new { message = "Ok" }, JsonRequestBehavior.AllowGet) ;
        }
        [Authorize]
        public ActionResult Approve(Decision tr)
        {
            var d = tr;
            var dd = JsonConvert.DeserializeObject<DecisionData>(d.Data);
            var df = JsonConvert.DeserializeObject<List<DecisionDataForm>>(dd.DataForm);
            return Json(new { message = "Ok" });
        }
        [Authorize]
        public async Task<ActionResult> GetFile(string filename)
        {
            if (filename == null)
                return Content("filename not present");
            var path = Path.Combine(Server.MapPath("~/Docs"), filename);
            //var path = Path.Combine(
            //               Directory.GetCurrentDirectory(),
            //               "Docs", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},  
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        private List<dynamic> CreateFormMdif()
        {
            List<dynamic> res = new List<dynamic>();

            var titledatamou = new FormGroup
            {
                title = "Data Mou 2"
            };
            res.Add(titledatamou);
            var kategorimou = new FormInput
            {
               data="Mou Toko Baru",
               label = "Kategori MOU",
               disabled = true,
               typeForm = "text",
            };
            res.Add(kategorimou);
            var tglmou = new FormDate
            {
                data = new List<DateTime> { DateTime.Now},
                label = "Tanggal MOU",
                disabled = true,
            };
            res.Add(tglmou);

            var fee = new FormInput
            {
                data = 30000000,
                label = "Franchise Fee (Rp.)",
                disabled = true,
                typeForm = "number"
            };
            res.Add(fee);

            var ter = new FormInput
            {
                data = 300000000,
                label = "Nilai Investasi (Rp.)",
                disabled = true,
                typeForm = "number"
            };
            res.Add(ter);

            var tertabhead = new List<FormTableHeader>();
            tertabhead.Add(new FormTableHeader
            {
                key = "nama",
                title = "Nama"
            });

            tertabhead.Add(new FormTableHeader
            {
                key = "tgl",
                title = "Tanggal Jatuh Tempo"
            });

            tertabhead.Add(new FormTableHeader
            {
                key = "nominal",
                title = "Nominal"
            });
            var detter = new List<dynamic>();
            detter.Add(new { nama = "Termin 1", tgl = DateTime.Now, nominal = 100000000 });
            detter.Add(new { nama = "Termin 2", tgl = DateTime.Now, nominal = 100000000 });
            detter.Add(new { nama = "Termin 3", tgl = DateTime.Now, nominal = 100000000 });

            var detailtermin = new FormTable
            {
                header = tertabhead,
                label = "Detail Termin",
                data = detter
            };
            res.Add(detailtermin);

            var titledatafranchise = new FormGroup
            {
                title = "Data Franchise"
            };
            res.Add(titledatafranchise);
            var nama = new FormInput
            {
                data = "Sulaiman Triarjo",
                label = "Nama Franchise",
                disabled = true,
                typeForm = "text"
            };
            res.Add(nama);
            var nik = new FormInput
            {
                data = "653599262682606",
                label = "NIK",
                disabled = true,
                typeForm = "text"
            };
            res.Add(nik);
            var telp = new FormInput
            {
                data = "085755519123",
                label = "No Telp",
                disabled = true,
                typeForm = "text"
            };
            res.Add(telp);
            var ala = new FormInput
            {
                data = "Jalan Sesame No 123",
                label = "Alamat",
                disabled = true,
                typeForm = "text"
            };
            res.Add(ala);

            var img = new FormImage
            {
                label = "Coba menampilkan gambar",
                //link = "https://localhost:44305/Docs/cc.jpg", //this cobamvc url
                link = "http://localhost/cb/Docs/cc.jpg", //this cobamvc url
                fileName = "cc.jpg"
            };
            res.Add(img);

            var ffitem = new List<FormFileItem>();
            ffitem.Add(
                    new FormFileItem
                    {
                        fileName ="xel.xlsx",
                        label ="xel",
                        //link = "https://localhost:44305/Docs/xel.xlsx",  //this cobamvc url
                        link = "http://localhost/cb/Docs/xel.xlsx",  //this cobamvc url
                        typeDoc = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"
                    }
                );
            ffitem.Add(new FormFileItem
            {
                fileName = "xel.xlsx",
                label = "xel dari method",
                //link = "https://localhost:44305/Coba/GetFile?filename=xel.xlsx",  //this cobamvc url
                link = "http://localhost/cb/Coba/GetFile?filename=xel.xlsx",  //this cobamvc url
                typeDoc = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"
            });

            var fl = new FormFile
            {
                label = "Required File",
                data = ffitem
            };
            res.Add(fl);

            var au = new FormAutoComplete
            {
                label = "Ini tuh Auto complete",
                provideFilter = false,
                name = "autoco",
                //link = "https://localhost:44305/Coba/DataAutoComplete",
                link = "http://localhost/cb/Coba/DataAutoComplete",
            };
            res.Add(au);

            return res;
        }

        [Authorize]
        public ActionResult DataAutoComplete()
        {
            var r = new List<FormAutoCompleteItem>();
            for (int i = 0; i < 100; i++)
            {
                var t = new FormAutoCompleteItem
                {
                    label = "form auto "+i.ToString(),
                    data = "f-"+i.ToString()
                };
                r.Add(t);
            }
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        private List<dynamic> CreateFormLitigationRem()
        {
            List<dynamic> res = new List<dynamic>();



            return res;
        }
    }
}