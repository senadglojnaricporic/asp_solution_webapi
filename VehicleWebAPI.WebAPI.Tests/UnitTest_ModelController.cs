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
    public class UnitTest_ModelController
    {
        [Fact]
        public async void Test_GetVehicleModelDataModel_All()
        {
            //Arrange
            var mock_service = new Mock<IVehicleGenericService<IVehicleModelDataModel, VehicleModelDataModel>>();
            var mock_mapper = new Mock<IMapper>();
            var filtering = new FilteringModelModel();
            var sorting = new SortingModelModel();
            var paging = new PagingModelModel();
            var dataList = new List<VehicleModelDataModel>();
            var viewList = new List<VehicleModelViewModel>();
            mock_mapper.Setup(x => x.Map<FilteringModelModel>(It.IsAny<QueryModel>())).Returns(filtering);
            mock_mapper.Setup(x => x.Map<SortingModelModel>(It.IsAny<QueryModel>())).Returns(sorting);
            mock_mapper.Setup(x => x.Map<PagingModelModel>(It.IsAny<QueryModel>())).Returns(paging);
            mock_mapper.Setup(x => x.Map<IEnumerable<VehicleModelViewModel>>(dataList)).Returns(viewList);
            mock_service.Setup(x => x.FindDataAsync(filtering, sorting, paging).Result).Returns(dataList);
            //Act
            var controller = new VehicleModelController(mock_service.Object, mock_mapper.Object);
            var actionResult = await controller.GetVehicleModelDataModel(new QueryModel());
            var value = actionResult.Value;
            var res = actionResult.Result;
            //Assert
            value.Should().BeOfType<List<VehicleModelViewModel>>();
            res.Should().BeNull();
        }

        [Fact]
        public async void Test_GetVehicleModelDataModel_ById()
        {
            //Arrange
            var mock_service = new Mock<IVehicleGenericService<IVehicleModelDataModel, VehicleModelDataModel>>();
            var mock_mapper = new Mock<IMapper>();
            var data = new VehicleModelDataModel();
            var view = new VehicleModelViewModel();
            mock_service.Setup(x => x.ReadItemAsync(It.IsAny<int>()).Result).Returns(data);
            mock_mapper.Setup(x => x.Map<VehicleModelViewModel>(data)).Returns(view);
            //Act
            var controller = new VehicleModelController(mock_service.Object, mock_mapper.Object);
            var actionResult = await controller.GetVehicleModelDataModel(1);
            var value = actionResult.Value;
            var res = actionResult.Result;
            //Assert
            value.Should().BeOfType<VehicleModelViewModel>();
            res.Should().BeNull();
        }

        [Fact]
        public async void Test_GetVehicleModelDataModel_ById_Null()
        {
            //Arrange
            var mock_service = new Mock<IVehicleGenericService<IVehicleModelDataModel, VehicleModelDataModel>>();
            var mock_mapper = new Mock<IMapper>();
            var data = new VehicleModelDataModel();
            var view = new VehicleModelViewModel();
            mock_service.Setup(x => x.ReadItemAsync(It.IsAny<int>()).Result).Returns((VehicleModelDataModel)null);
            mock_mapper.Setup(x => x.Map<VehicleModelViewModel>(data)).Returns(view);
            //Act
            var controller = new VehicleModelController(mock_service.Object, mock_mapper.Object);
            var actionResult = await controller.GetVehicleModelDataModel(1);
            var value = actionResult.Value;
            var res = actionResult.Result;
            //Assert
            value.Should().BeNull();
            res.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void Test_PutVehicleModelDataModel()
        {
            //Arrange
            var mock_service = new Mock<IVehicleGenericService<IVehicleModelDataModel, VehicleModelDataModel>>();
            var mock_mapper = new Mock<IMapper>();
            var data = new VehicleModelDataModel();
            var view = new VehicleModelViewModel() { Id = 1 };
            mock_service.Setup(x => x.UpdateItemAsync(data)).Returns(Task.CompletedTask);
            mock_service.Setup(X => X.ReadItemAsync(1).Result).Returns(data);
            mock_mapper.Setup(x => x.Map<VehicleModelDataModel>(view)).Returns(data);
            //Act
            var controller = new VehicleModelController(mock_service.Object, mock_mapper.Object);
            var actionResult = await controller.PutVehicleModelDataModel(1, view);
            //Assert
            actionResult.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async void Test_PutVehicleModelDataModel_IdNotEqual()
        {
            //Arrange
            var mock_service = new Mock<IVehicleGenericService<IVehicleModelDataModel, VehicleModelDataModel>>();
            var mock_mapper = new Mock<IMapper>();
            var data = new VehicleModelDataModel();
            var view = new VehicleModelViewModel() { Id = 1 };
            mock_service.Setup(x => x.UpdateItemAsync(data)).Returns(Task.CompletedTask);
            mock_service.Setup(X => X.ReadItemAsync(1).Result).Returns(data);
            mock_mapper.Setup(x => x.Map<VehicleModelDataModel>(view)).Returns(data);
            //Act
            var controller = new VehicleModelController(mock_service.Object, mock_mapper.Object);
            var actionResult = await controller.PutVehicleModelDataModel(2, view);
            //Assert
            actionResult.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async void Test_PutVehicleModelDataModel_Exception_ModelExist()
        {
            //Arrange
            bool exTrown = false;
            var mock_service = new Mock<IVehicleGenericService<IVehicleModelDataModel, VehicleModelDataModel>>();
            var mock_mapper = new Mock<IMapper>();
            var data = new VehicleModelDataModel();
            var view = new VehicleModelViewModel() { Id = 1 };
            mock_service.Setup(x => x.UpdateItemAsync(data)).Throws<DbUpdateConcurrencyException>();
            mock_service.Setup(X => X.ReadItemAsync(1).Result).Returns(data);
            mock_mapper.Setup(x => x.Map<VehicleModelDataModel>(view)).Returns(data);
            //Act
            var controller = new VehicleModelController(mock_service.Object, mock_mapper.Object);
            try
            {
                await controller.PutVehicleModelDataModel(1, view);
            }
            catch(DbUpdateConcurrencyException)
            {
                exTrown = true;
            }         
            //Assert
            exTrown.Should().BeTrue();
        }

        [Fact]
        public async void Test_PutVehicleModelDataModel_Exception_ModelNotExist()
        {
            //Arrange
            var mock_service = new Mock<IVehicleGenericService<IVehicleModelDataModel, VehicleModelDataModel>>();
            var mock_mapper = new Mock<IMapper>();
            var data = new VehicleModelDataModel();
            var view = new VehicleModelViewModel() { Id = 1 };
            mock_service.Setup(x => x.UpdateItemAsync(data)).Throws<DbUpdateConcurrencyException>();
            mock_service.Setup(X => X.ReadItemAsync(1).Result).Returns((VehicleModelDataModel)null);
            mock_mapper.Setup(x => x.Map<VehicleModelDataModel>(view)).Returns(data);
            //Act
            var controller = new VehicleModelController(mock_service.Object, mock_mapper.Object);
            var actionResult = await controller.PutVehicleModelDataModel(1, view);
      
            //Assert
            actionResult.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void Test_PostVehicleModelDataModel()
        {
            //Arrange
            var mock_service = new Mock<IVehicleGenericService<IVehicleModelDataModel, VehicleModelDataModel>>();
            var mock_mapper = new Mock<IMapper>();
            var data = new VehicleModelDataModel();
            var view = new VehicleModelViewModel() { Id = 1 };
            mock_mapper.Setup(x => x.Map<VehicleModelDataModel>(view)).Returns(data);
            mock_service.Setup(X => X.CreateItemAsync(data).Result).Returns(It.IsAny<int>());
            //Act
            var controller = new VehicleModelController(mock_service.Object, mock_mapper.Object);
            var actionResult = await controller.PostVehicleModelDataModel(view);
            var value = actionResult.Value;
            var res = actionResult.Result;
            //Assert
            value.Should().BeNull();
            res.Should().BeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public async void Test_DeleteVehicleModelDataModel_IdExists()
        {
            //Arrange
            var mock_service = new Mock<IVehicleGenericService<IVehicleModelDataModel, VehicleModelDataModel>>();
            var mock_mapper = new Mock<IMapper>();
            mock_service.Setup(X => X.DeleteItemAsync(1).Result).Returns(true);
            //Act
            var controller = new VehicleModelController(mock_service.Object, mock_mapper.Object);
            var actionResult = await controller.DeleteVehicleModelDataModel(1);
            //Assert
             actionResult.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async void Test_DeleteVehicleModelDataModel_IdNotExists()
        {
            //Arrange
            var mock_service = new Mock<IVehicleGenericService<IVehicleModelDataModel, VehicleModelDataModel>>();
            var mock_mapper = new Mock<IMapper>();
            mock_service.Setup(X => X.DeleteItemAsync(1).Result).Returns(false);
            //Act
            var controller = new VehicleModelController(mock_service.Object, mock_mapper.Object);
            var actionResult = await controller.DeleteVehicleModelDataModel(1);
            //Assert
            actionResult.Should().BeOfType<NotFoundResult>();
        }
    }
}
