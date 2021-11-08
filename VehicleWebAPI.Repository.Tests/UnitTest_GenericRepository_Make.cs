using System.Linq;
using Xunit;
using Moq;
using FluentAssertions;
using VehicleWebAPI.DAL;
using VehicleWebAPI.Model;
using VehicleWebAPI.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        public async void Test_1_CreateAsync_Make()
        {
            //Arrange
            var repo = new VehicleWebAPIGenericRepository<VehicleMakeDataModel>(dbContext);
            var make = new VehicleMakeDataModel() {
                Id = 1,
                Name = "Volkswagen",
                Abrv = "VW",
            };
            //Act
            var entry = await repo.CreateAsync(make);
            var values = entry.CurrentValues;
            var id = values["Id"];
            var name = values["Name"];
            var abrv = values["Abrv"];
            var numOfEntries = await repo.SaveAsync();
            //Assert
            entry.Should().BeOfType<EntityEntry<VehicleMakeDataModel>>();
            id.Should().Be(make.Id);
            name.Should().Be(make.Name);
            abrv.Should().Be(make.Abrv);
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
        public async void Test_2_ReadByIdAsync_Make()
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
        public async void Test_3_Update_Make()
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
            var entry = repo.Update(make);
            var values = entry.CurrentValues;
            var id = values["Id"];
            var name = values["Name"];
            var abrv = values["Abrv"];
            var numOfEntries = await repo.SaveAsync();
            //Assert
            entry.Should().BeOfType<EntityEntry<VehicleMakeDataModel>>();
            id.Should().Be(make.Id);
            name.Should().Be(make.Name);
            abrv.Should().Be(make.Abrv);
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
        public async void Test_4_DeleteAsync_Make()
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
            var entry = await repo.DeleteAsync(1);
            var values = entry.CurrentValues;
            var id = values["Id"];
            var name = values["Name"];
            var abrv = values["Abrv"];
            var numOfEntries = await repo.SaveAsync();
            //Assert
            entry.Should().BeOfType<EntityEntry<VehicleMakeDataModel>>();
            id.Should().Be(_make.Id);
            name.Should().Be(_make.Name);
            abrv.Should().Be(_make.Abrv);
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
        public void Test_4_GetTable_Make()
        {
            //Arrange
            var repo = new VehicleWebAPIGenericRepository<VehicleMakeDataModel>(dbContext);
            //Act
            var table = repo.GetTable();
            //Assert
            table.Should().BeOfType<EntityQueryable<VehicleMakeDataModel>>();
        }

        [Fact]
        public async void Test_4_FindDataAsync_Make()
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
