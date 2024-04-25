using API_ManagementSystem_ClassActivity.Data;
using API_ManagementSystem_ClassActivity.Models;
using API_ManagementSystem_ClassActivity.RequestsLayer;
using API_ManagementSystem_ClassActivity.ResponseLayer;

namespace API_ManagementSystem_ClassActivity.ServiceLayer
{
    public class EmployeeService
    {
        private readonly EmployeeData _employeeData;

        public EmployeeService(EmployeeData employeeData)
        {
            _employeeData = employeeData;
        }

        public async Task<EmployeeResponse> Get(Guid id)
        {
            var employee = await _employeeData.Get(id);

            var response = new EmployeeResponse
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                TitleId = employee.TitleId,
                TitleDescription = employee.Title.Description
            };

            return response;
        }

        public async Task<List<EmployeeResponse>> GetAll()
        {
            var employees = await _employeeData.GetAll();

            var response = employees.Select(employee => new EmployeeResponse
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                TitleId = employee.TitleId,
                TitleDescription = employee.Title.Description
            }).ToList();

            return response;
        }

        public async Task<List<EmployeeResponse>> Search(string keyword)
        {
            var employees = await _employeeData.Search(keyword);

            var response = employees.Select(employee => new EmployeeResponse
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                TitleId = employee.TitleId,
                TitleDescription = employee.Title.Description
            }).ToList();

            return response;
        }

        public async Task<EmployeeResponse> Create(EmployeeRequest request)
        {
            /*No validation
            var employee = new Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                Gender = request.Gender,
                TitleId = request.TitleId
            };

            await _employeeData.Create(employee);

            var response = await Get(employee.Id);
            return response;
            */
            //added validation checking if fields are empty
            if (string.IsNullOrWhiteSpace(request.FirstName))
            {
                throw new Exception("FirstName cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(request.LastName))
            {
                throw new Exception("LastName cannot be empty.");
            }

            if (request.BirthDate == default)
            {
                throw new Exception("BirthDate cannot be empty.");
            }

            if (request.TitleId == default)
            {
                throw new Exception("TitleId cannot be empty.");
            }

            var employee = new Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                Gender = request.Gender,
                TitleId = request.TitleId
            };

            await _employeeData.Create(employee);

            var response = await Get(employee.Id);
            return response;
        }

        public async Task<EmployeeResponse> Update(Guid id, EmployeeRequest request)
        {
            var employee = await _employeeData.Get(id);

            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.BirthDate = request.BirthDate;
            employee.Gender = request.Gender;
            employee.TitleId = request.TitleId;

            await _employeeData.Update(employee);

            var response = await Get(employee.Id);
            return response;
        }

        public async Task Delete(Guid id)
        {
            await _employeeData.Delete(id);
        }
    }
}
