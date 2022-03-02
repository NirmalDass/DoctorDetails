using DoctorDetail.Model;
using DoctorDetail.ViewModel;

namespace DoctorDetail.Repository
{
    public interface IRepository
    {
        List<Doctor> Get();
        List<Department> DepartmentA();
        List<Department> GetDepartment();
        Task<Doctor> GetById(int Id);
        Task<Doctor> Get(string Name);
        Task<Doctor> Create(Doctor obj);
        Task<Doctor> Update(DtoDoctorViewModel request);
        void Delete(int Id);
    }
}
