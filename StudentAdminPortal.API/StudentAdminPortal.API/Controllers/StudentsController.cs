using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Controllers
{
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;

        public StudentsController(IStudentRepository studentRepository,IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudents()
        {
            var  students= await(studentRepository.GetStudentsAsync());
           
            /*var domainModelStudents = new List<Student>();
            foreach(var student in students){
                domainModelStudents.Add(new Student()
                {
                    Id=student.Id,
                    FirstName=student.FirstName,
                    LastName=student.LastName,
                    DateOfBirth=student.DateOfBirth,
                    Email=student.Email,
                    GenderId=student.GenderId,
                    ProfileImageUrl=student.ProfileImageUrl,
                    Mobile=student.Mobile,
                    Address=new Address()
                    {
                        Id=student.Address.Id,
                        PhysicalAddress=student.Address.PhysicalAddress,
                        PostalAddress=student.Address.PostalAddress,
                    },
                    Gender = new Gender() {
                        Id=student.Gender.Id,
                        Description=student.Gender.Description,
                    }

                });
            }*/
            return Ok(mapper.Map<List<Student>>(students));
        }
        [HttpGet]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> GetStudentAsync([FromRoute]Guid studentId)
        {
            //fetch stu
            var student = await studentRepository.GetStudentAsync(studentId);
            if (student == null)
            {
                return NotFound();
            }

            //return
            return Ok(mapper.Map<Student>(student));

        }

        [HttpPut]
        [Route("[controller]/{studentId:Guid}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute]Guid studentId,[FromBody]updateStudentRequest request)
        {
           if( await studentRepository.Exists(studentId))
            {
                //update detail
               var updatedStudent=await studentRepository.UpdateStudent(studentId,mapper.Map<DataModels.Student>(request));
                if (updatedStudent != null)
                {
                    return Ok(mapper.Map<Student>(updatedStudent));
                }
            }
            
                return NotFound();
            
        }

        [HttpDelete]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> DeleteStudentASyncAsync([FromRoute]Guid studentId)
        {
            if(await studentRepository.Exists(studentId))
            {
                //Delete student
                var student=await studentRepository.DeleteStudent(studentId);
                return Ok(mapper.Map<Student>(student));
            }
            return NotFound();
        }
    }

}
