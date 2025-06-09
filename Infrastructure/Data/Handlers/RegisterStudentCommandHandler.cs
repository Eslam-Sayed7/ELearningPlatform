// using Core.Entities;
// using Infrastructure.Data;
// using Infrastructure.Data.Services;
// using Infrastructure.Services.Auth;
//
// public class RegisterStudentCommandHandler 
// {
//
//     private readonly Student _student;
//     private readonly IStudentService _studentService;
//
//     public RegisterStudentCommandHandler(IStudentService studentService , Student student)
//     {
//         _student = student;
//         _studentService = studentService;
//     }
//
//     public async Task<Student> Handle()
//     {
//         await _studentService.CreateStudentAsync(_student);
//         return _student;
//     }
// }