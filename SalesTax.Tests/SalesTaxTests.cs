using NSubstitute;
using SalesTax.Domain.Models;
using SalesTax.Domain.Services;
using System.Net.Http.Headers;
using Xunit;

namespace SalesTax.Tests
{
    public class SalesTaxTests
    {
        [Fact]
        public async Task GetTaxRateByLocation_TestDataMatchesResultData()
        {

            //arrange
            var httpClientFactoryMock = Substitute.For<IHttpClientFactory>();
            var fakeHttpClient = new HttpClient();

            httpClientFactoryMock.CreateClient("TaxJarAPIBase").Returns(fakeHttpClient);

            fakeHttpClient.BaseAddress = new Uri("https://api.taxjar.com/v2/");
            fakeHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "5da2f821eee4035db4771edab942a4cc");


            // Act
            var service = new TaxRateService(httpClientFactoryMock);
            var result = await service.GetTaxRates("35071");


            LocationTaxRateModel locationTaxRateModel = new LocationTaxRateModel();
            RateModel rate = new RateModel();


            rate.city = "FULTONDALE";

            locationTaxRateModel.rate = rate;


            //assert
            Assert.Equal(result.rate.city, locationTaxRateModel.rate.city);
        }


        [Fact]
        public async Task GetTaxRateByLocation_ErrorFlagSetToTrue()
        {

            //arrange
            var httpClientFactoryMock = Substitute.For<IHttpClientFactory>();
            var fakeHttpClient = new HttpClient();

            httpClientFactoryMock.CreateClient("TaxJarAPIBase").Returns(fakeHttpClient);

            fakeHttpClient.BaseAddress = new Uri("https://api.taxjar.com/v2/");
            fakeHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "5da2f821eee4035db4771edab942a4cc");


            // Act
            var service = new TaxRateService(httpClientFactoryMock);
            var result = await service.GetTaxRates("00000");


            //assert
            Assert.True(result.IsError == true);
        }



        [Fact]
        public async Task CalculateSalesTax_TestDataMatchesResultData()
        {

            //arrange
            var httpClientFactoryMock = Substitute.For<IHttpClientFactory>();
            var fakeHttpClient = new HttpClient();

            httpClientFactoryMock.CreateClient("TaxJarAPIBase").Returns(fakeHttpClient);

            fakeHttpClient.BaseAddress = new Uri("https://api.taxjar.com/v2/");
            fakeHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "5da2f821eee4035db4771edab942a4cc");


            // Act
            var service = new TaxRateService(httpClientFactoryMock);

            OrderModel order = new OrderModel();

            order.amount = 150;
            order.shipping = 15;
        
            var result = await service.CalculateSalesTax(order);

            SalesTaxModel salesTaxModel = new SalesTaxModel();
            TaxModel taxModel = new TaxModel();
            JurisdictionsModel jurisdictionsModel = new JurisdictionsModel();

            taxModel.jurisdictions = jurisdictionsModel;
            salesTaxModel.tax = taxModel;

            //assert
            Assert.Equal(result.tax.amount_to_collect, 14.25);
        }


        [Fact]
        public async Task CalculateSalesTax_ErrorFlagSetToTrue()
        {

            //arrange
            var httpClientFactoryMock = Substitute.For<IHttpClientFactory>();
            var fakeHttpClient = new HttpClient();

            httpClientFactoryMock.CreateClient("TaxJarAPIBase").Returns(fakeHttpClient);

            fakeHttpClient.BaseAddress = new Uri("https://api.taxjar.com/v2/");
            fakeHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "5da2f821eee4035db4771edab942a4cc");


            // Act
            var service = new TaxRateService(httpClientFactoryMock);

            OrderModel order = new OrderModel();


            //Error when to and from state are different
            order.to_state = "AL";
            order.from_state = "CA";

            order.amount = 150;
            order.shipping = 15;

            var result = await service.CalculateSalesTax(order);

            //assert
            Assert.True(result.IsError == true);
        }

    }
}