using DoctorDetail.Data;
using DoctorDetail.Model;
using DoctorDetail.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctorDetail.Repository
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _db;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Doctor> Get()
        {
            var DoctorList=_db.Doctors.ToList();
            return DoctorList;
        }
        public List<Department> DepartmentA()
        {
            var DepartmentList = _db.Departments.ToList();
            return DepartmentList;
        }

        public List<Department> GetDepartment()
        {
            var DepartmentList = _db.Departments.ToList();
            return DepartmentList;
        }
        public void Delete(int Id)
        {
            var DeleteDoctorDb = _db.Doctors.Find(Id);
            _db.Doctors.Remove(DeleteDoctorDb);
            _db.SaveChanges();
        }
        public async Task<Doctor> GetById(int Id)
        {
            var ReadIdDoctorDb = _db.Doctors.Find(Id);
            return ReadIdDoctorDb;
        }
        public async Task<Doctor> Get(string Name)
        {
            var Results = _db.Doctors.FirstOrDefault(m => m.Name == Name);
            return Results;
        }
        public async Task<Doctor> Create(Doctor obj)
        {
            var Result=_db.Doctors.Add(obj);
            _db.SaveChanges();
            return Result.Entity;
        }
        public async Task<Doctor> Update(DtoDoctorViewModel request)
        {
            var DoctorFromDb = _db.Doctors.Find(request.DoctorId);
            DoctorFromDb.Qualification = request.Qualification;
            DoctorFromDb.Dept_Id = request.Dept_Id;
            DoctorFromDb.MobileNo = request.MobileNo;
            DoctorFromDb.AvailableDays = request.AvailableDays;
            _db.Doctors.Update(DoctorFromDb);
            _db.SaveChanges();
            return DoctorFromDb;
        }
    }
}
