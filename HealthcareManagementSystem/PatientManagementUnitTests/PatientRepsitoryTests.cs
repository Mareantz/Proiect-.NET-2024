﻿//// PatientManagementUnitTests/PatientRepositoryTests.cs
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Domain.Entities;
//using Infrastructure.Repositories;
//using Microsoft.EntityFrameworkCore;
//using PredictiveHealthcare.Infrastructure.Persistence;
//using Xunit;

//namespace Infrastructure.Tests.Repositories
//{
//	public class PatientRepositoryTests : IDisposable
//	{
//		private readonly ApplicationDbContext _context;
//		private readonly PatientRepository _repository;

//		public PatientRepositoryTests()
//		{
//			var options = new DbContextOptionsBuilder<ApplicationDbContext>()
//				.UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
//				.Options;

//			_context = new ApplicationDbContext(options);
//			_repository = new PatientRepository(_context);
//		}

//		[Fact]
//		public async Task AddPatient_ShouldReturnSuccessResult_WhenPatientIsAdded()
//		{
//			var patient = new Patient
//			{
//				UserId = Guid.NewGuid(),
//				FirstName = "John",
//				LastName = "Doe",
//				Gender = "Male",
//				DateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)),
//				Address = "123 Main St"
//			};
//			Console.WriteLine(patient.UserId);
//			var result = await _repository.AddPatient(patient);
//			Console.WriteLine(result.Data);
//			Assert.Equal(patient.UserId, result.Data);
//			Assert.True(result.IsSuccess);
//		}

//		[Fact]
//		public async Task AddPatient_ShouldReturnFailureResult_WhenExceptionIsThrown()
//		{
//			var patient = new Patient
//			{
//				UserId = Guid.NewGuid(),
//				FirstName = "John",
//				LastName = "Doe",
//				Gender = "Male",
//				DateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)),
//				Address = "123 Main St"
//			};

//			// Simulate an exception by disposing the context
//			_context.Dispose();

//			var result = await _repository.AddPatient(patient);

//			Assert.False(result.IsSuccess);
//			Assert.NotNull(result.ErrorMessage);
//		}

//		[Fact]
//		public async Task GetPatients_ShouldReturnListOfPatients()
//		{
//			var patients = new List<Patient>
//			{
//				new Patient
//				{
//					UserId = Guid.NewGuid(),
//					FirstName = "John",
//					LastName = "Doe",
//					Gender = "Male",
//					DateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)),
//					Address = "123 Main St"
//				}
//			};

//			await _context.Patients.AddRangeAsync(patients);
//			await _context.SaveChangesAsync();

//			var result = await _repository.GetPatients();

//			Assert.NotNull(result);
//			Assert.Single(result);
//			Assert.Equal(patients[0].UserId, ((List<Patient>)result)[0].UserId);
//		}

//		[Fact]
//		public async Task GetPatientById_ShouldReturnPatient_WhenPatientExists()
//		{
//			var patientId = Guid.NewGuid();
//			var patient = new Patient
//			{
//				UserId = patientId,
//				FirstName = "John",
//				LastName = "Doe",
//				Gender = "Male",
//				DateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)),
//				Address = "123 Main St"
//			};

//			await _context.Patients.AddAsync(patient);
//			await _context.SaveChangesAsync();

//			var result = await _repository.GetPatientById(patientId);

//			Assert.NotNull(result);
//			Assert.Equal(patient.UserId, result.UserId);
//		}

//		[Fact]
//		public async Task UpdatePatient_ShouldReturnSuccessResult_WhenPatientIsUpdated()
//		{
//			var patient = new Patient
//			{
//				UserId = Guid.NewGuid(),
//				FirstName = "John",
//				LastName = "Doe",
//				Gender = "Male",
//				DateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)),
//				Address = "123 Main St"
//			};

//			await _context.Patients.AddAsync(patient);
//			await _context.SaveChangesAsync();

//			patient.FirstName = "Jane";
//			var result = await _repository.UpdatePatient(patient);

//			Assert.True(result.IsSuccess);

//			var updatedPatient = await _context.Patients.FindAsync(patient.UserId);
//			Assert.Equal("Jane", updatedPatient.FirstName);
//		}

//		[Fact]
//		public async Task UpdatePatient_ShouldReturnFailureResult_WhenExceptionIsThrown()
//		{
//			var patient = new Patient
//			{
//				UserId = Guid.NewGuid(),
//				FirstName = "John",
//				LastName = "Doe",
//				Gender = "Male",
//				DateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)),
//				Address = "123 Main St"
//			};

//			await _context.Patients.AddAsync(patient);
//			await _context.SaveChangesAsync();

//			// Simulate an exception by disposing the context
//			_context.Dispose();

//			var result = await _repository.UpdatePatient(patient);

//			Assert.False(result.IsSuccess);
//			Assert.NotNull(result.ErrorMessage);
//		}

//		public void Dispose()
//		{
//			_context.Dispose();
//		}
//	}
//}
