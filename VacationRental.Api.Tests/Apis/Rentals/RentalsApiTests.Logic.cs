using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationRental.Api.Models.Rentals;
using VacationRental.Api.ViewModels;
using Xunit;

namespace VacationRental.Api.Tests.Apis.Rentals
{
    public partial class RentalsApiTests
    {
        [Fact]
        public async Task ShouldPostRentalAsync()
        {
            //given
            RentalBindingModel randomRental = CreateRandomRentalModel();
            RentalBindingModel inputRental = randomRental;
            var expectedRental = new Rental
            {
                PreparationTimeInDays = inputRental.PreparationTimeInDays,
                Units = inputRental.Units
            };

            //when 
            var postHttpResponseMessage =
                await this.apiBroker.PostRentalAsync(randomRental);

            ResourceIdViewModel resourceIdViewModel = await
                DeserializeResponseContent<ResourceIdViewModel>(postHttpResponseMessage);

            var getHttpResponseMessage =
                await this.apiBroker.GetRentalByIdAsync(resourceIdViewModel.Id);

            Rental actualRental = await
                DeserializeResponseContent<Rental>(getHttpResponseMessage);

            expectedRental.Id = resourceIdViewModel.Id;
            
            //then
            actualRental.Should().BeEquivalentTo(expectedRental);
        }

        [Fact]
        public async Task ShouldGetRentalByIdAsync()
        {
            //given
            Rental randomRental = await PostRandomRental();
            Rental expectedRental = randomRental;

            //when
            var getHttpResponseMessage =
                await this.apiBroker.GetRentalByIdAsync(randomRental.Id);

            Rental actualRental = await
                DeserializeResponseContent<Rental>(getHttpResponseMessage);

            //then
            actualRental.Should().BeEquivalentTo(expectedRental);
        }

        [Fact]
        public async Task ShouldPutRentalAsync()
        {
            //given
            Rental randomRental = await PostRandomRental();
            RentalBindingModel modifiedRentalModel = UpadteRandomRentalModel();
            var modifiedRental = new Rental
            {
                Id = randomRental.Id,
                PreparationTimeInDays = modifiedRentalModel.PreparationTimeInDays,
                Units = modifiedRentalModel.Units
            };


            //when 
            await this.apiBroker.PutRentalAsync(randomRental.Id, modifiedRentalModel);

            var getHttpResponseMessage =
                await this.apiBroker.GetRentalByIdAsync(randomRental.Id);

            Rental actualRental = await
                DeserializeResponseContent<Rental>(getHttpResponseMessage);

            //then
            actualRental.Should().BeEquivalentTo(modifiedRental);
        }
    }
}
