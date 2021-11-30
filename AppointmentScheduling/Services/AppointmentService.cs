using AppointmentScheduling.Models.ViewModels;
using AppointmentScheduling.Models;
using AppointmentScheduling.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduling.Services
{
  public class AppointmentService : IAppointmentService
  {
    private readonly ApplicationDbContext _db;

    public AppointmentService(ApplicationDbContext db)
    {
      _db = db;
    }
    public List<DoctorViewModel> GetDoctorList()
    {
      var doctors = (from user in _db.Users
                     join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                     join roles in _db.Roles.Where(x => x.Name == Helper.Doctor) on userRoles.RoleId equals roles.Id
                     select new DoctorViewModel
                     {
                       Id = user.Id,
                       Name = user.Name
                     }).ToList();
      return doctors;
    }

    public List<PatientViewModel> getPatientList()
    {
      var patients = (from user in _db.Users
                      join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                      join roles in _db.Roles.Where(x => x.Name == Helper.Patient) on userRoles.RoleId equals roles.Id
                      select new PatientViewModel
                      {
                        Id = user.Id,
                        Name = user.Name
                      }).ToList();

      return patients;
    }
  }

}