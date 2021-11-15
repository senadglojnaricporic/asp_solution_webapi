using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using VehicleWebAPI.Model;
using VehicleWebAPI.Repository.Common;
using Xunit;

namespace VehicleWebAPI.Service.Tests
{
    public class UnitTest_ModelService
    {
        [Fact]
        public async void Test_CreateItemAsync()
        {
            //Arange
            var mock_repo = new Mock<IVehicleWebAPIGenericRepository<VehicleModelDataModel>>();
            var item = new VehicleModelDataModel(){
                Id = 1,
                MakeId = 1,
                Name = "xx",
                Abrv = "xxx",
            };
            mock_repo.Setup(x => x.CreateAsync(item))
                        .Returns(Task.CompletedTask);
            mock_repo.Setup(x => x.SaveAsync().Result).Returns(1);
            //Act
            var service = new VehicleModelService(mock_repo.Object);
            var num = await service.CreateItemAsync(item);
            //Assert
            num.Should().Be(1);
        }

        [Fact]
        public async void Test_ReadItemAsync()
        {
            //Arange
            var mock_repo = new Mock<IVehicleWebAPIGenericRepository<VehicleModelDataModel>>();
            var item = new VehicleModelDataModel(){
                Id = 1,
                MakeId = 1,
                Name = "xx",
                Abrv = "xxx",
            };
            mock_repo.Setup(x => x.ReadByIdAsync(1).Result)
                        .Returns(item);
            //Act
            var service = new VehicleModelService(mock_repo.Object);
            var make = await service.ReadItemAsync(1);
            //Assert
            make.Should().BeOfType<VehicleModelDataModel>();
        }

        [Fact]
        public async void Test_UpdateItemAsync()
        {
            //Arange
            var mock_repo = new Mock<IVehicleWebAPIGenericRepository<VehicleModelDataModel>>();
            var item = new VehicleModelDataModel(){
                Id = 1,
                MakeId = 1,
                Name = "xx",
                Abrv = "xxx",
            };
            mock_repo.Setup(x => x.Update(item));
            mock_repo.Setup(x => x.SaveAsync().Result).Returns(1);
            //Act
            var service = new VehicleModelService(mock_repo.Object);
            var task = service.UpdateItemAsync(item);
            await task;
            var res = task.IsFaulted;
            //Assert
            res.Should().BeFalse();
        }

        [Fact]
        public async void Test_DeleteItemAsync_ItemFound()
        {
            //Arange
            var mock_repo = new Mock<IVehicleWebAPIGenericRepository<VehicleModelDataModel>>();
            mock_repo.Setup(x => x.DeleteAsync(1).Result).Returns(true);
            mock_repo.Setup(x => x.SaveAsync().Result).Returns(1);
            //Act
            var service = new VehicleModelService(mock_repo.Object);
            var res = await service.DeleteItemAsync(1);
            //Assert
            res.Should().BeTrue();
        }

        [Fact]
        public async void Test_DeleteItemAsync_ItemNotFound()
        {
            //Arange
            var mock_repo = new Mock<IVehicleWebAPIGenericRepository<VehicleModelDataModel>>();
            mock_repo.Setup(x => x.DeleteAsync(1).Result).Returns(false);
            mock_repo.Setup(x => x.SaveAsync().Result).Returns(0);
            //Act
            var service = new VehicleModelService(mock_repo.Object);
            var res = await service.DeleteItemAsync(1);
            //Assert
            res.Should().BeFalse();
        }

        [Fact]
        public async void Test_FindDataAsync()
        {
            //Arange
            var filtering = new FilteringModelModel();
            var sorting = new SortingModelModel();
            var paging = new PagingModelModel();
            var data = new List<VehicleModelDataModel>();
            var mock_repo = new Mock<IVehicleWebAPIGenericRepository<VehicleModelDataModel>>();
            mock_repo.Setup(x => x.FindDataAsync(filtering, sorting, paging).Result).Returns(data);
            mock_repo.Setup(x => x.SaveAsync().Result).Returns(1);
            //Act
            var service = new VehicleModelService(mock_repo.Object);
            var list = await service.FindDataAsync(filtering, sorting, paging);
            //Assert
            list.Should().AllBeOfType<List<VehicleModelDataModel>>();
        }
    }
}
