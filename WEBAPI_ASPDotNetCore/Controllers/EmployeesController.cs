using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI_ASPDotNetCore.Contracts;
using WEBAPI_ASPDotNetCore.Models;
using WEBAPI_ASPDotNetCore.Repository;

namespace WEBAPI_ASPDotNetCore.Controllers
{ 

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IRepositoryWrapper _reposWrapper;
     

    public EmployeesController(IRepositoryWrapper reposWrapper)
    {
         _reposWrapper = reposWrapper;
    }

   [HttpGet]
   [Authorize]
    public IEnumerable<Employee> Get()
    {
       var emp = _reposWrapper.Employee.FindAll();
       return emp;       
    }

    [HttpGet("{id}", Name = nameof(GetSingleEmployee))]
    //public async Task<ActionResult<Employee>> GetSingleEmployee(long id)
    public IEnumerable<Employee> GetSingleEmployee(int id)
    {
       var a =  _reposWrapper.Employee.FindByCondition(x => x.Id.Equals(id));
       
        if (a == null)
        { return (IEnumerable<Employee>)NotFound(); }

         return a;

    }

    [HttpPost]
    public ActionResult<Employee> CreateUser([FromBody] Employee _user)
    {
        _reposWrapper.Employee.Create(_user);
        _reposWrapper.Save();

        return CreatedAtAction(nameof(GetSingleEmployee), new { id = _user.Id }, _user);
    }

    [HttpDelete("{id:int}")]
    public void DeleteEmployee(int id)
    {
            var empToDelete = _reposWrapper.Employee.FindByCondition(x => x.Id.Equals(id));
            if(empToDelete == null)
             {
                    NotFound($"Employee with Id = {id} not found");
             }
              _reposWrapper.Employee.Delete(empToDelete.SingleOrDefault());
            _reposWrapper.Save();

                     
    }
    [HttpPut("{id:int}")]
    public ActionResult UpdateEmployee(int id, Employee _emp)
    {
        if(id != _emp.Id)
        {
            return BadRequest("Id doesnot match");
        }
        var empToUpdate = _reposWrapper.Employee.FindByCondition(x => x.Id.Equals(id));
        if(empToUpdate == null)
        {
             NotFound($"Employee {id} Not Found");
        }

         _reposWrapper.Employee.Update(_emp);
         _reposWrapper.Save();

        return Ok(empToUpdate);
        }       
    }
}
