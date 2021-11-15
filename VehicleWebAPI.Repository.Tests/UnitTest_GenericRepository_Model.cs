using System.Linq;
using Xunit;
using FluentAssertions;
using VehicleWebAPI.DAL;
using VehicleWebAPI.Model;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Collections.Generic;
using VehicleWebAPI.Service;

namespace VehicleWebAPI.Repository.Tests
{
    public class UnitTest_GenericRepository_Model
    {
        private readonly VehicleWebAPIDbContext dbContext;
        public UnitTest_GenericRepository_Model()
        {
            dbContext = new VehicleWebAPIDbContext("Data Source=F:\\asp_solution_webapi\\VehicleWebAPI.Repository.Tests\\Test.db");
        }

        [Fact]
        public async void Test_CreateAsync_Model()
        {
            //Arrange
            var repoMake = new VehicleWebAPIGenericRepository<VehicleMakeDataModel>(dbContext);
            var make = new VehicleMakeDataModel() {
                Id = 1,
                Name = "Volkswagen",
                Abrv = "VW",
            };
            var repoModel = new VehicleWebAPIGenericRepository<VehicleModelDataModel>(dbContext);
            var model = new VehicleModelDataModel() {
                Id = 1,
                MakeId = 1,
                Name = "Golf",
                Abrv = "VW",
            };
            //Act
            await repoMake.CreateAsync(make);
            var numOfEntriesMake = await repoMake.SaveAsync();
            await repoModel.CreateAsync(model);
            var numOfEntriesModel = await repoModel.SaveAsync();
            //Assert
            numOfEntriesMake.Should().Be(1);
            numOfEntriesModel.Should().Be(1);

            //Clear table
            var rows = from o in dbContext.VehicleMakes select o;
            foreach(var row in rows)
            {
                dbContext.VehicleMakes.Remove(row);
            }
            await dbContext.SaveChangesAsync();
        }

        [Fact]
        public async void Test_ReadByIdAsync_Model()
        {
            //Arrange
            var repoMake = new VehicleWebAPIGenericRepository<VehicleMakeDataModel>(dbContext);
            var _make = new VehicleMakeDataModel() {
                Id = 1,
                Name = "Volkswagen",
                Abrv = "VW",
            };
            await dbContext.VehicleMakes.AddAsync(_make);
            await dbContext.SaveChangesAsync();
            var repoModel = new VehicleWebAPIGenericRepository<VehicleModelDataModel>(dbContext);
            var _model = new VehicleModelDataModel() {
                Id = 1,
                MakeId = 1,
                Name = "Golf",
                Abrv = "VW",
            };
            await dbContext.VehicleModels.AddAsync(_model);
            await dbContext.SaveChangesAsync();
            //Act
            var model = await repoModel.ReadByIdAsync(1);
            //Assert
            model.Should().BeOfType<VehicleModelDataModel>();
            model.Id.Should().Be(_model.Id);
            model.MakeId.Should().Be(_model.MakeId);
            model.Name.Should().Be(_model.Name);
            model.Abrv.Should().Be(_model.Abrv);

            //Clear table
            var rows = from o in dbContext.VehicleMakes select o;
            foreach(var row in rows)
            {
                dbContext.VehicleMakes.Remove(row);
            }
            await dbContext.SaveChangesAsync();
        }

        [Fact]
        public async void Test_Update_Model()
        {
            //Arrange
            var repoMake = new VehicleWebAPIGenericRepository<VehicleMakeDataModel>(dbContext);
            var _make = new VehicleMakeDataModel() {
                Id = 1,
                Name = "Volkswagen",
                Abrv = "VW",
            };
            await dbContext.VehicleMakes.AddAsync(_make);
            await dbContext.SaveChangesAsync();
            var repoModel = new VehicleWebAPIGenericRepository<VehicleModelDataModel>(dbContext);
            var _model = new VehicleModelDataModel() {
                Id = 1,
                MakeId = 1,
                Name = "Golf",
                Abrv = "VW",
            };
            await dbContext.VehicleModels.AddAsync(_model);
            await dbContext.SaveChangesAsync();
            dbContext.ChangeTracker.Clear();
            var model = new VehicleModelDataModel() {
                Id = 1,
                MakeId = 1,
                Name = "Jetta",
                Abrv = "Vw",
            };
            //Act
            repoModel.Update(model);
            var numOfEntries = await repoModel.SaveAsync();
            //Assert
            numOfEntries.Should().Be(1);

            //Clear table
            var rows = from o in dbContext.VehicleMakes select o;
            foreach(var row in rows)
            {
                dbContext.VehicleMakes.Remove(row);
            }
            await dbContext.SaveChangesAsync();
        }

        [Fact]
        public async void Test_DeleteAsync_Model_ItemFound()
        {
            //Arrange
            var repoMake = new VehicleWebAPIGenericRepository<VehicleMakeDataModel>(dbContext);
            var _make = new VehicleMakeDataModel() {
                Id = 1,
                Name = "Volkswagen",
                Abrv = "VW",
            };
            await dbContext.VehicleMakes.AddAsync(_make);
            await dbContext.SaveChangesAsync();
            var repoModel = new VehicleWebAPIGenericRepository<VehicleModelDataModel>(dbContext);
            var _model = new VehicleModelDataModel() {
                Id = 1,
                MakeId = 1,
                Name = "Golf",
                Abrv = "VW",
            };
            await dbContext.VehicleModels.AddAsync(_model);
            await dbContext.SaveChangesAsync();
            //Act
            var res = await repoModel.DeleteAsync(1);
            var numOfEntries = await repoModel.SaveAsync();
            //Assert
            res.Should().BeTrue();
            numOfEntries.Should().Be(1);

            //Clear table
            var rows = from o in dbContext.VehicleMakes select o;
            foreach(var row in rows)
            {
                dbContext.VehicleMakes.Remove(row);
            }
            await dbContext.SaveChangesAsync();
        }

        [Fact]
        public async void Test_DeleteAsync_Model_ItemNotFound()
        {
            //Arrange
            var repoMake = new VehicleWebAPIGenericRepository<VehicleMakeDataModel>(dbContext);
            var _make = new VehicleMakeDataModel() {
                Id = 1,
                Name = "Volkswagen",
                Abrv = "VW",
            };
            await dbContext.VehicleMakes.AddAsync(_make);
            await dbContext.SaveChangesAsync();
            var repoModel = new VehicleWebAPIGenericRepository<VehicleModelDataModel>(dbContext);
            var _model = new VehicleModelDataModel() {
                Id = 1,
                MakeId = 1,
                Name = "Golf",
                Abrv = "VW",
            };
            await dbContext.VehicleModels.AddAsync(_model);
            await dbContext.SaveChangesAsync();
            //Act
            var res = await repoModel.DeleteAsync(2);
            var numOfEntries = await repoModel.SaveAsync();
            //Assert
            res.Should().BeFalse();
            numOfEntries.Should().Be(0);

            //Clear table
            var rows = from o in dbContext.VehicleMakes select o;
            foreach(var row in rows)
            {
                dbContext.VehicleMakes.Remove(row);
            }
            await dbContext.SaveChangesAsync();
        }

        [Fact]
        public void Test_GetTable_Model()
        {
            //Arrange
            var repo = new VehicleWebAPIGenericRepository<VehicleModelDataModel>(dbContext);
            //Act
            var table = repo.GetTable();
            //Assert
            table.Should().BeOfType<EntityQueryable<VehicleModelDataModel>>();
        }

        [Fact]
        public async void Test_FindDataAsync_Model()
        {
            //Arrange
            var repo = new VehicleWebAPIGenericRepository<VehicleModelDataModel>(dbContext);
            var listMake = new List<VehicleMakeDataModel>() {
                new VehicleMakeDataModel(){
                    Id = 1,
                    Name = "Volkswagen",
                    Abrv = "VW",
                },
                new VehicleMakeDataModel(){
                    Id = 2,
                    Name = "GeneralMotors",
                    Abrv = "GM",
                },
                new VehicleMakeDataModel(){
                    Id = 3,
                    Name = "FabricaItalianaAutomobiliTorino",
                    Abrv = "FIAT",
                },
            };
            var listModel = new List<VehicleModelDataModel>() {
                new VehicleModelDataModel(){
                    Id = 1,
                    MakeId = 1,
                    Name = "Golf",
                    Abrv = "VW",
                },
                new VehicleModelDataModel(){
                    Id = 2,
                    MakeId = 1,
                    Name = "Jetta",
                    Abrv = "VW",
                },
                new VehicleModelDataModel(){
                    Id = 3,
                    MakeId = 3,
                    Name = "Punto",
                    Abrv = "FIAT",
                },
            };
            await dbContext.AddRangeAsync(listMake);
            await dbContext.AddRangeAsync(listModel);
            await dbContext.SaveChangesAsync();
            var filtering = new FilteringModelModel();
            var sorting = new SortingModelModel();
            var paging = new PagingModelModel();
            filtering.filterType = "searchName";
            filtering.searchString = "Jet";
            sorting.sortBy = "name";
            paging.pageIndex = 1;
            paging.pageSize = 3;
            //Act
            var data = await repo.FindDataAsync(filtering, sorting, paging);
            var count = data.Count();
            //Assert
            data.Should().BeOfType<List<VehicleModelDataModel>>();
            count.Should().Be(1);

            //Clear table
            var rows = from o in dbContext.VehicleMakes select o;
            foreach(var row in rows)
            {
                dbContext.VehicleMakes.Remove(row);
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
