using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.KYC
{
    public class TcKimlikNoDogrula
    {
        /// <summary>
        /// Nüfus ve Vatandaşlık İşleri Genel Müdürlüğüne TC Kimlik Numarası, Ad, Soyad ve Doğum Yılı göndererek kişinin Türkiye Cumhuriyeti kayıtlarında olup olmadığını doğrular.
        /// KisiVarMi() fonksiyonu ile birlikte kullanılır.
        /// </summary>
        /// <param name="tcKimlikNo"></param>
        /// <param name="ad"></param>
        /// <param name="soyad"></param>
        /// <param name="dogumYili"></param>
        public TcKimlikNoDogrula(long tcKimlikNo, string ad, string soyad, int dogumYili)
        {
            TcKimlikNo = tcKimlikNo;
            Ad = ad.Trim().Replace("i", "İ").ToUpper();
            Soyad = soyad.Trim().Replace("i", "İ").ToUpper();
            DogumYili = dogumYili;
        }

        /// <summary>
        /// TC Kimlik Numarasını TC Kimlik No Algoritmasına göre kontrol eder. 
        /// TCAlgoritmasi() fonksiyonu ile birlikte kullanılır.
        /// </summary>
        /// <param name="tcKimlikNo"></param>
        public TcKimlikNoDogrula(long tcKimlikNo)
        {
            TcKimlikNo = tcKimlikNo;
        }

        /// <summary>
        ///T.C kimlik numarası 11 haneli ve sayısal olmalıdır.
        ///T.C kimlik numarası 0 ile başlayamaz.
        ///T.C kimlik numarasının 11 haneli ve sayısal değerde olduğu kontrol edilir.İlk 9 rakam arasındaki formül 10.cu rakamı, ilk 10 rakam arasındaki formülasyon ise 11.ci rakamı oluşturur.İlk rakam 0 olamaz.
        /// 1,3,5,7 ve 9.cu hanelerin toplamının 7 ile çarpımından 2,4,6, ve 8. haneler çıkartıldığında geriye kalan sayının 10’a göre modu 10. haneyi verir. (çıkarma işleminden elde edilen sonucun 10’a bölümünden kalan) 1,2,3,4,5,6,7,8,9 ve 10. sayıların toplamının 10’a göre modu (10’a bölümünden kalan) 11. rakamı sağlar.
        /// </summary>
        /// <returns>bool</returns>
        public bool TcAlgoritmasi()
        {
            return TcDogruMu();
        }


        /// <summary>
        /// https://tckimlik.nvi.gov.tr da kişi kontrolü.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> KisiVarMiAsync()
        {
            InputKontrolleri();
            const string requestUrl = "https://tckimlik.nvi.gov.tr/Service/KPSPublic.asmx";
            var bytes = Encoding.UTF8.GetBytes(RequestXml());
            var num = bytes.Length;
            var request = BuildRequest(requestUrl, num);
            return await GetResponseAsync(request, bytes);
        }
        
        private bool TcDogruMu()
        {
            var arr = TcKimlikNo.ToString().ToCharArray();
            int sumEven = 0, sumOdd = 0, sumFirst10 = 0, i = 0;
            if (TcKimlikNo.ToString().Length != 11)
                return false;

            if (arr[0] == '0')
                return false;

            while (i <= 8)
            {
                var temp = int.Parse(arr[i].ToString());
                sumFirst10 += temp;
                if (i % 2 == 1) sumEven += temp;
                else sumOdd += temp;
                i++;
            }
            sumFirst10 += int.Parse(arr[9].ToString());

            if (!(((sumEven * 9) + (sumOdd * 7)) % 10 == int.Parse(arr[9].ToString()) && (sumFirst10 % 10 == int.Parse(arr[10].ToString()))))
                return false;

            return true;
        }

        private bool DogumYiliDogruMu()
        {
            return DogumYili.ToString().Length == 4;
        }

        private bool AdDogruMu()
        {
            return !string.IsNullOrEmpty(Ad) && Ad.Length > 2;
        }

        private bool SoyAdDogruMu()
        {
            return !string.IsNullOrEmpty(Soyad) && Soyad.Length > 1;
        }

        private void InputKontrolleri()
        {
            if (!TcDogruMu())
                throw new BadHttpRequestException("TC Kimlik Numarası hatalı.");

            if (!AdDogruMu())
                throw new BadHttpRequestException("Ad hatalı.");

            if (!SoyAdDogruMu())
                throw new BadHttpRequestException("Soyad hatalı.");

            if (!DogumYiliDogruMu())
                throw new BadHttpRequestException("Doğum Yılı hatalı.");
        }

        private string RequestXml()
        {
            var str = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
            str += "<soap12:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\">";
            str += "<soap12:Body>";
            str += "<TCKimlikNoDogrula xmlns=\"http://tckimlik.nvi.gov.tr/WS\">";
            
            str += "<TCKimlikNo>" + TcKimlikNo + "</TCKimlikNo>";
            str += "<Ad>" + Ad + "</Ad>";
            str += "<Soyad>" + Soyad + "</Soyad>";
            
            str += "<DogumYili>" + DogumYili + "</DogumYili>";
            str += "</TCKimlikNoDogrula>";
            str += "</soap12:Body>";
            return str + "</soap12:Envelope>";
        }

        private static HttpWebRequest BuildRequest(string requestUrl, long contentLength)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUrl);
            httpWebRequest.ContentType = "application/soap+xml; charset=utf-8";
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentLength = contentLength;
            return httpWebRequest;
        }

        private static async Task<bool> GetResponseAsync(WebRequest request, byte[] requestBytesArray)
        {
            await using (var stream = await request.GetRequestStreamAsync())
            {
                await stream.WriteAsync(requestBytesArray, 0, (int)request.ContentLength);
            }
            try
            {
                string text;
                using (var webResponse = await request.GetResponseAsync())
                {
                    using var streamReader = new StreamReader(webResponse.GetResponseStream());
                    text = (await streamReader.ReadToEndAsync()).Trim();
                }
                var xDocument = XDocument.Parse(text);
                var value = xDocument.Descendants().SingleOrDefault((XElement x) => x.Name.LocalName == "TCKimlikNoDogrulaResult")?.Value;
                return bool.Parse(value ?? string.Empty);
            }
            catch
            {
                return false;
            }
        }

        private long TcKimlikNo { get; }
        private string Ad { get; }
        private string Soyad { get; }
        private int DogumYili { get; }
    }
}
