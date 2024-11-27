
using Microsoft.EntityFrameworkCore;
using System;

using IPStackApi.Repositories;
using IPStackApi.Models;

using IPStackDLL.Exceptions;
using IPStackDLL.Interfaces;
using IPStackDLL.Models;
using IPStackDLL;

namespace IPStackApi.Services 
{
    public class IPDetailsService
    {
        private readonly IIPInfoProvider _ipInfoProvider;
        private readonly IPDetailsRepository _ipDetailsRepository;
        private readonly IPCacheService _ipCacheService;

        public IPDetailsService(
            IPDetailsRepository ipDetailsRepository,
            IIPInfoProvider ipInfoProvider,
            IPCacheService ipCacheService
            )
        {
            _ipDetailsRepository = ipDetailsRepository;
            _ipInfoProvider = ipInfoProvider;
            _ipCacheService = ipCacheService;
        }

        public async Task<IPDetails?> getDetails(string ip)
        {
            // Check if Ip is in Cache
            IPDetails? ipDetails = _ipCacheService.GetFromCache(ip);

            if (ipDetails != null) {
                return ipDetails;
            }

            // Search in DB for the IPDetails
            ipDetails = EntityToIPDetails(await _ipDetailsRepository.FindByIp(ip));

            if (ipDetails == null)
            {
                // Request data from the Library
                ipDetails = (IPDetails) _ipInfoProvider.GetIPDetails(ip);
                // Write data to database 
                IPDetailsEntity? entity = IPDetailsToEntity(ipDetails, ip);
                await _ipDetailsRepository.Create(entity);
                // Put data in cache
                _ipCacheService.AddToCache(ip, ipDetails);
            }
            else
            {
                // Write IpDetails to Cache
                _ipCacheService.AddToCache(ip, ipDetails);
            }

            return ipDetails;
        }

        private IPDetails? EntityToIPDetails(IPDetailsEntity? entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new IPDetails 
            {
                City = entity.City,
                Country = entity.Country,
                Continent = entity.Continent,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude
            };
        }

        private IPDetailsEntity? IPDetailsToEntity(IPDetails? ipDetails, string ip) 
        {
            if (ipDetails == null)
            {
                return null;
            }

            return new IPDetailsEntity
            {
                Ip = ip,
                City = ipDetails.City,
                Country = ipDetails.Country,
                Continent = ipDetails.Continent,
                Latitude = ipDetails.Latitude,
                Longitude = ipDetails.Longitude
            };
        }
    }
}
