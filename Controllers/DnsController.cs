using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spiracle.NETCore.Models;

namespace Spiracle.NETCore.Controllers
{
    public class DnsController : Controller
    {
        public IActionResult Index()
        {
            var dnsModel = new Models.DnsModel();
            return View(dnsModel);
        }

        [Route("/dns/GetHostEntry")]
        public IActionResult GetHostEntry(DnsModel dnsModel)
        {
            if(dnsModel?.HostNameOrIpAddress == null) return View("Index", dnsModel);

            var ipAddresses = "";

            var iphe = System.Net.Dns.GetHostEntry(dnsModel.HostNameOrIpAddress);
            foreach (var address in iphe.AddressList)
            {
                ipAddresses += (address + "\n");
            }

            dnsModel.LookupResult = ipAddresses;

            return View("Index", dnsModel);
        }


        [Route("/dns/GetHostEntryAsync")]
        public IActionResult GetHostEntryAsync(DnsModel dnsModel)
        {
            if (dnsModel?.HostNameOrIpAddress == null) return View("Index", dnsModel);

            var ipAddresses = "";

            Task<IPHostEntry> task = System.Net.Dns.GetHostEntryAsync(dnsModel.HostNameOrIpAddress);
            var iphe = task.Result;
            foreach (var address in iphe.AddressList)
            {
                ipAddresses += (address + "\n");
            }

            dnsModel.LookupResult = ipAddresses;

            return View("Index", dnsModel);
        }

        [Route("/dns/GetHostEntryIpAddress")]
        public IActionResult GetHostEntryIpAddress(DnsModel dnsModel)
        {
            if (dnsModel?.HostNameOrIpAddress == null) return View("Index", dnsModel);

            if(!IPAddress.TryParse(dnsModel.HostNameOrIpAddress, out var ipAddress)) return View("Index", dnsModel);

            try
            {
                var iphe = System.Net.Dns.GetHostEntry(ipAddress);
                dnsModel.LookupResult = iphe.HostName;
            }
            catch (Exception ex)
            {
                dnsModel.LookupResult = $"An exception occurred. Message: \"{ex.Message}\"";
            }
            return View("Index", dnsModel);
        }

        [Route("/dns/GetHostEntryIpAddressAsync")]
        public IActionResult GetHostEntryIpAddressAsync(DnsModel dnsModel)
        {
            if (dnsModel?.HostNameOrIpAddress == null) return View("Index", dnsModel);

            if (!IPAddress.TryParse(dnsModel.HostNameOrIpAddress, out var ipAddress)) return View("Index", dnsModel);

            try
            {
                Task<IPHostEntry> task = System.Net.Dns.GetHostEntryAsync(ipAddress);
                var iphe = task.Result;
                dnsModel.LookupResult = iphe.HostName;
            }
            catch (Exception ex)
            {
                dnsModel.LookupResult = $"An exception occurred. Message: \"{ex.Message}\"";
            }
            return View("Index", dnsModel);
        }

    }
}
