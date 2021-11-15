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
    public class UnitTest_GenericRepository_Make
    {
        private readonly VehicleWebAPIDbContext dbContext;
        public UnitTest_GenericRepository_Make()
        {
            dbContext = new VehicleWebAPIDbContext("Data Source=F:\\asp_solution_webapi\\VehicleWebAPI.Repository.Tests\\Test.db");
        }

        [Fact]
        public async void Test_CreateAsync_Make()
        {
            //Arrange
            var repo = new VehicleWebAPIGenericRepository<VehicleMakeDataModel>(dbContext);
            var make = new VehicleMakeDataModel() {
                Id = 1,
                Name = "Volkswagen",
                Abrv = "VW",
            };
            //Act
            await repo.CreateAsync(make);
            var numOfEntries = await repo.SaveAsync();
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
        public async void Test_ReadByIdAsync_Make()
        {
            //Arrange
            var repo = new VehicleWebAPIGenericRepository<VehicleMakeDataModel>(dbContext);
            var _make = new VehicleMakeDataModel() {
                Id = 1,
                Name = "Volkswagen",
                Abrv = "VW",
            };
            await dbContext.VehicleMakes.AddAsync(_make);
            await dbContext.SaveChangesAsync();
            //Act
            var make = await repo.ReadByIdAsync(1);
            //Assert
            make.Should().BeOfType<VehicleMakeDataModel>();
            make.Id.Should().Be(_make.Id);
            make.Name.Should().Be(_make.Name);
            make.Abrv.Should().Be(_make.Abrv);

            //Clear table
            var rows = from o in dbContext.VehicleMakes select o;
            foreach(var row in rows)
            {
                dbContext.VehicleMakes.Remove(row);
            }
            await dbContext.SaveChangesAsync();
        }

        [Fact]
        public async void Test_Update_Make()
        {
            //Arrange
            var repo = new VehicleWebAPIGenericRepository<VehicleMakeDataModel>(dbContext);
            var _make = new VehicleMakeDataModel() {
                Id = 1,
                Name = "Volkswagen",
                Abrv = "VW",
            };
            await dbContext.VehicleMakes.AddAsync(_make);
            await dbContext.SaveChangesAsync();
            dbContext.ChangeTracker.Clear();
            var make = new VehicleMakeDataModel() {
                Id = 1,
                Name = "GeneralMotors",
                Abrv = "GM",
            };
            //Act
            repo.Update(make);
            var numOfEntries = await repo.SaveAsync();
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
        public async void Test_DeleteAsync_Make_ItemFound()
        {
            //Arrange
            var repo = new VehicleWebAPIGenericRepository<VehicleMakeDataModel>(dbContext);
            var _make = new VehicleMakeDataModel() {
                Id = 1,
                Name = "Volkswagen",
                Abrv = "VW",
            };
            await dbContext.VehicleMakes.AddAsync(_make);
            await dbContext.SaveChangesAsync();
            //Act
            var res = await repo.DeleteAsync(1);
            var numOfEntries = await repo.SaveAsync();
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
        public async void Test_DeleteAsync_Make_ItemNotFound()
        {
            //Arrange
            var repo = new VehicleWebAPIGenericRepository<VehicleMakeDataModel>(dbContext);
            var _make = new VehicleMakeDataModel() {
                Id = 1,
                Name = "Volkswagen",
                Abrv = "VW",
            };
            await dbContext.VehicleMakes.AddAsync(_make);
            await dbContext.SaveChangesAsync();
            //Act
            var res = await repo.DeleteAsync(2);
            var numOfEntries = await repo.SaveAsync();
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
        public void Test_GetTable_Make()
        {
            //Arrange
            var repo = new VehicleWebAPIGenericRepository<VehicleMakeDataModel>(dbContext);
            //Act
            var table = repo.GetTable();
            //Assert
            table.Should().BeOfType<EntityQueryable<VehicleMakeDataModel>>();
        }

        [Fact]
        public async void Test_FindDataAsync_Make()
        {
            //Arrange
            var repo = new VehicleWebAPIGenericRepository<VehicleMakeDataModel>(dbContext);
            var list = new List<VehicleMakeDataModel>() {
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
            await dbContext.AddRangeAsync(list);
            await dbContext.SaveChangesAsync();
            var filtering = new FilteringMakeModel();
            var sorting = new SortingMakeModel();
            var paging = new PagingMakeModel();
            filtering.filterType = "searchName";
            filtering.searchString = "General";
            sorting.sortBy = "name";
            paging.pageIndex = 1;
            paging.pageSize = 3;
            //Act
            var data = await repo.FindDataAsync(filtering, sorting, paging);
            var count = data.Count();
            //Assert
            data.Should().BeOfType<List<VehicleMakeDataModel>>();
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
