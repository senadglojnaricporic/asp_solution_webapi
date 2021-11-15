using Xunit;
using AutoMapper;
using Moq;
using VehicleWebAPI.Service.Common;
using VehicleWebAPI.Model;
using VehicleWebAPI.Service;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VehicleWebAPI.WebAPI.Tests
{
    public class UnitTest_MakeController
    {
        [Fact]
        public async void Test_GetVehicleMakeDataModel_All()
        {
            //Arrange
            var mock_service = new Mock<IVehicleGenericService<IVehicleMakeDataModel, VehicleMakeDataModel>>();
            var mock_mapper = new Mock<IMapper>();
            var filtering = new FilteringMakeModel();
            var sorting = new SortingMakeModel();
            var paging = new PagingMakeModel();
            var dataList = new List<VehicleMakeDataModel>();
            var viewList = new List<VehicleMakeViewModel>();
            mock_mapper.Setup(x => x.Map<FilteringMakeModel>(It.IsAny<QueryModel>())).Returns(filtering);
            mock_mapper.Setup(x => x.Map<SortingMakeModel>(It.IsAny<QueryModel>())).Returns(sorting);
            mock_mapper.Setup(x => x.Map<PagingMakeModel>(It.IsAny<QueryModel>())).Returns(paging);
            mock_mapper.Setup(x => x.Map<IEnumerable<VehicleMakeViewModel>>(dataList)).Returns(viewList);
            mock_service.Setup(x => x.FindDataAsync(filtering, sorting, paging).Result).Returns(dataList);
            //Act
            var controller = new VehicleMakeController(mock_service.Object, mock_mapper.Object);
            var actionResult = await controller.GetVehicleMakeDataModel(new QueryModel());
            var value = actionResult.Value;
            var res = actionResult.Result;
            //Assert
            value.Should().BeOfType<List<VehicleMakeViewModel>>();
            res.Should().BeNull();
        }

        [Fact]
        public async void Test_GetVehicleMakeDataModel_ById()
        {
            //Arrange
            var mock_service = new Mock<IVehicleGenericService<IVehicleMakeDataModel, VehicleMakeDataModel>>();
            var mock_mapper = new Mock<IMapper>();
            var data = new VehicleMakeDataModel();
            var view = new VehicleMakeViewModel();
            mock_service.Setup(x => x.ReadItemAsync(It.IsAny<int>()).Result).Returns(data);
            mock_mapper.Setup(x => x.Map<VehicleMakeViewModel>(data)).Returns(view);
            //Act
            var controller = new VehicleMakeController(mock_service.Object, mock_mapper.Object);
            var actionResult = await controller.GetVehicleMakeDataModel(1);
            var value = actionResult.Value;
            var res = actionResult.Result;
            //Assert
            value.Should().BeOfType<VehicleMakeViewModel>();
            res.Should().BeNull();
        }

        [Fact]
        public async void Test_GetVehicleMakeDataModel_ById_Null()
        {
            //Arrange
            var mock_service = new Mock<IVehicleGenericService<IVehicleMakeDataModel, VehicleMakeDataModel>>();
            var mock_mapper = new Mock<IMapper>();
            var data = new VehicleMakeDataModel();
            var view = new VehicleMakeViewModel();
            mock_service.Setup(x => x.ReadItemAsync(It.IsAny<int>()).Result).Returns((VehicleMakeDataModel)null);
            mock_mapper.Setup(x => x.Map<VehicleMakeViewModel>(data)).Returns(view);
            //Act
            var controller = new VehicleMakeController(mock_service.Object, mock_mapper.Object);
            var actionResult = await controller.GetVehicleMakeDataModel(1);
            var value = actionResult.Value;
            var res = actionResult.Result;
            //Assert
            value.Should().BeNull();
            res.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void Test_PutVehicleMakeDataModel()
        {
            //Arrange
            var mock_service = new Mock<IVehicleGenericService<IVehicleMakeDataModel, VehicleMakeDataModel>>();
            var mock_mapper = new Mock<IMapper>();
            var data = new VehicleMakeDataModel();
            var view = new VehicleMakeViewModel() { Id = 1 };
            mock_service.Setup(x => x.UpdateItemAsync(data)).Returns(Task.CompletedTask);
            mock_service.Setup(X => X.ReadItemAsync(1).Result).Returns(data);
            mock_mapper.Setup(x => x.Map<VehicleMakeDataModel>(view)).Returns(data);
            //Act
            var controller = new VehicleMakeController(mock_service.Object, mock_mapper.Object);
            var actionResult = await controller.PutVehicleMakeDataModel(1, view);
            //Assert
            actionResult.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async void Test_PutVehicleMakeDataModel_IdNotEqual()
        {
            //Arrange
            var mock_service = new Mock<IVehicleGenericService<IVehicleMakeDataModel, VehicleMakeDataModel>>();
            var mock_mapper = new Mock<IMapper>();
            var data = new VehicleMakeDataModel();
            var view = new VehicleMakeViewModel() { Id = 1 };
            mock_service.Setup(x => x.UpdateItemAsync(data)).Returns(Task.CompletedTask);
            mock_service.Setup(X => X.ReadItemAsync(1).Result).Returns(data);
            mock_mapper.Setup(x => x.Map<VehicleMakeDataModel>(view)).Returns(data);
            //Act
            var controller = new VehicleMakeController(mock_service.Object, mock_mapper.Object);
            var actionResult = await controller.PutVehicleMakeDataModel(2, view);
            //Assert
            actionResult.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async void Test_PutVehicleMakeDataModel_Exception_ModelExist()
        {
            //Arrange
            bool exTrown = false;
            var mock_service = new Mock<IVehicleGenericService<IVehicleMakeDataModel, VehicleMakeDataModel>>();
            var mock_mapper = new Mock<IMapper>();
            var data = new VehicleMakeDataModel();
            var view = new VehicleMakeViewModel() { Id = 1 };
            mock_service.Setup(x => x.UpdateItemAsync(data)).Throws<DbUpdateConcurrencyException>();
            mock_service.Setup(X => X.ReadItemAsync(1).Result).Returns(data);
            mock_mapper.Setup(x => x.Map<VehicleMakeDataModel>(view)).Returns(data);
            //Act
            var controller = new VehicleMakeController(mock_service.Object, mock_mapper.Object);
            try
            {
                await controller.PutVehicleMakeDataModel(1, view);
            }
            catch(DbUpdateConcurrencyException)
            {
                exTrown = true;
            }         
            //Assert
            exTrown.Should().BeTrue();
        }

        [Fact]
        public async void Test_PutVehicleMakeDataModel_Exception_ModelNotExist()
        {
            //Arrange
            var mock_service = new Mock<IVehicleGenericService<IVehicleMakeDataModel, VehicleMakeDataModel>>();
            var mock_mapper = new Mock<IMapper>();
            var data = new VehicleMakeDataModel();
            var view = new VehicleMakeViewModel() { Id = 1 };
            mock_service.Setup(x => x.UpdateItemAsync(data)).Throws<DbUpdateConcurrencyException>();
            mock_service.Setup(X => X.ReadItemAsync(1).Result).Returns((VehicleMakeDataModel)null);
            mock_mapper.Setup(x => x.Map<VehicleMakeDataModel>(view)).Returns(data);
            //Act
            var controller = new VehicleMakeController(mock_service.Object, mock_mapper.Object);
            var actionResult = await controller.PutVehicleMakeDataModel(1, view);
      
            //Assert
            actionResult.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void Test_PostVehicleMakeDataModel()
        {
            //Arrange
            var mock_service = new Mock<IVehicleGenericService<IVehicleMakeDataModel, VehicleMakeDataModel>>();
            var mock_mapper = new Mock<IMapper>();
            var data = new VehicleMakeDataModel();
            var view = new VehicleMakeViewModel() { Id = 1 };
            mock_mapper.Setup(x => x.Map<VehicleMakeDataModel>(view)).Returns(data);
            mock_service.Setup(X => X.CreateItemAsync(data).Result).Returns(It.IsAny<int>());
            //Act
            var controller = new VehicleMakeController(mock_service.Object, mock_mapper.Object);
            var actionResult = await controller.PostVehicleMakeDataModel(view);
            var value = actionResult.Value;
            var res = actionResult.Result;
            //Assert
            value.Should().BeNull();
            res.Should().BeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public async void Test_DeleteVehicleMakeDataModel_IdExists()
        {
            //Arrange
            var mock_service = new Mock<IVehicleGenericService<IVehicleMakeDataModel, VehicleMakeDataModel>>();
            var mock_mapper = new Mock<IMapper>();
            mock_service.Setup(X => X.DeleteItemAsync(1).Result).Returns(true);
            //Act
            var controller = new VehicleMakeController(mock_service.Object, mock_mapper.Object);
            var actionResult = await controller.DeleteVehicleMakeDataModel(1);
            //Assert
             actionResult.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async void Test_DeleteVehicleMakeDataModel_IdNotExists()
        {
            //Arrange
            var mock_service = new Mock<IVehicleGenericService<IVehicleMakeDataModel, VehicleMakeDataModel>>();
            var mock_mapper = new Mock<IMapper>();
            mock_service.Setup(X => X.DeleteItemAsync(1).Result).Returns(false);
            //Act
            var controller = new VehicleMakeController(mock_service.Object, mock_mapper.Object);
            var actionResult = await controller.DeleteVehicleMakeDataModel(1);
            //Assert
            actionResult.Should().BeOfType<NotFoundResult>();
        }
    }
}
