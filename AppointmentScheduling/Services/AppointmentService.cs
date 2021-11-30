using AppointmentScheduling.Models.ViewModels;
using AppointmentScheduling.Models;
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
      var doctors = (from user in _db.Users select new DoctorViewModel { Id = user.Id, Name = user.Name }).ToList();
      return doctors;
    }

    public List<PatientViewModel> getPatientList()
    {
      throw new NotImplementedException();
    }
  }

}