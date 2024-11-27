
using IPStackDLL.Interfaces;
using IPStackDLL.Models;
using IPStackDLL.Dtos;
using IPStackDLL.Exceptions;

using System.Text.Json;
using System.Net;

namespace IPStackDLL 
{
    public class IPInfoProvider : IIPInfoProvider
    {
        private readonly HttpClient _httpClient; 
        private readonly string _apiKey;
        public IPInfoProvider(string apiKey) 
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
        }

        public IIPDetails GetIPDetails(string ip)
        {
            if (!IsValidIPAddress(ip)) 
            {
                throw new IPServiceNotAvailableException("Invalid IP Address");
            }

            string url = $"http://api.ipstack.com/{ip}?access_key={_apiKey}";
            var response = _httpClient.GetStringAsync(url).Result;

            try 
            {
                var resultDto = JsonSerializer.Deserialize<IPDetailsResponse>(response);
                
                if (resultDto == null) {
                    throw new IPServiceNotAvailableException("Null Result from API");
                }

                return IPDetailsDtoToEntity(resultDto);
            }
            catch (IPServiceNotAvailableException ex)
            {
                throw new IPServiceNotAvailableException("", ex);
            }
            catch (Exception)
            {
                throw new IPServiceNotAvailableException("Invalid response from IPStack API.");
            }
        }

        private IPDetails IPDetailsDtoToEntity(IPDetailsResponse dto) 
        {
            return new IPDetails {
                City = dto.City,
                Country = dto.CountryName,
                Continent = dto.ContinentName,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude
            };
        }

        private bool IsValidIPAddress(string ip)
        {
            return IPAddress.TryParse(ip, out _);
        }
    }
}