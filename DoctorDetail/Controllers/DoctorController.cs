using DoctorDetail.Data;
using DoctorDetail.Model;
using DoctorDetail.Repository;
using DoctorDetail.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Net;

namespace DoctorDetail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IRepository _db;
        private readonly object ReadDoctorDb;
        public DoctorController(IRepository db)
        {
            _db = db;
        }

        //Read
        [HttpGet]
        public List<DoctorListViewModel> Get()
        {
            try
            {
                var DoctorList = _db.Get();
                var DepartmentList = _db.DepartmentA();
                var Result = from a in DoctorList
                             join a1 in DepartmentList
                             on a.Dept_Id equals a1.Id
                             select new DoctorListViewModel
                             {
                                 DoctorId = a.DoctorId,
                                 Name = a.Name,
                                 Gender = a.Gender,
                                 Qualification = a.Qualification,
                                 MobileNo = a.MobileNo,
                                 AvailableDays = a.AvailableDays,
                                 Dept_Id = a1.DepartmentName
                             };
                return (Result.ToList());
            }
            catch (Exception)
            {
                return null;
            }   
        }

        //Read by Id
        [HttpGet]
        [Route("{Id:int}")]
        public async Task<ActionResult<Doctor>> GetById(int Id)
        {
            var ReadIdDoctorDb = _db.GetById(Id);
            var DoctorList = _db.Get();
            var DepartmentList = _db.DepartmentA();
            var Result = from a in DoctorList
                         join a1 in DepartmentList
                         on a.Dept_Id equals a1.Id
                         select new DoctorListViewModel
                         {
                             DoctorId = a.DoctorId,
                             Name = a.Name,
                             Gender = a.Gender,
                             Qualification = a.Qualification,
                             MobileNo = a.MobileNo,
                             AvailableDays = a.AvailableDays,
                             Dept_Id = a1.DepartmentName
                         };
            var Result1 = Result.First(i => i.DoctorId == Id);
            return Ok(Result1);
        }

        //Read by Name
        [HttpGet("{Name}")]
        public async Task<ActionResult<Doctor>> Get(string Name)
        {
            var ReadNameDoctorDb = _db.Get();
            var DoctorList1 = _db.Get();
            var DepartmentList1 = _db.DepartmentA();
            var Result = from a in DoctorList1
                         join a1 in DepartmentList1
                         on a.Dept_Id equals a1.Id
                         select new
                         {
                             a.DoctorId,
                             a.Name,
                             a.Gender,
                             a.Qualification,
                             a.MobileNo,
                             a.AvailableDays,
                             a1.DepartmentName
                         };
            return Ok(Result.FirstOrDefault(a=>a.Name==Name));
        }

        //Create
        [HttpPost]
        public async Task<ActionResult<Doctor>> Create(Doctor obj)
        {
            try
            {
                var Result = _db.Create(obj);
                return Ok(Result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error in creating data");
            }
        }

        //Delete
        [HttpDelete("{Id}")]
        public async Task<ActionResult<Doctor>> Delete(int Id)
        {
            try
            {
                _db.Delete(Id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error in deleting data");
            }
        }

        //Edit
        [HttpPost]
        [Route("Edit")]
        public async Task<ActionResult<Doctor>> Update(DtoDoctorViewModel request)
        {
            try
            {
                _db.Update(request);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error in updating data");
            }
        }

        [HttpGet]
        [Route("Department/[controller]")]
        public IActionResult GetDepartmentA()
        {
            var DepartmentList = _db.DepartmentA();
            return Ok(DepartmentList);
            }
    }
}