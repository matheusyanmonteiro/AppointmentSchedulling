using AppointmentScheduling.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduling.Services
{
  public interface IAppointmentService
  {
    public List<DoctorViewModel> GetDoctorList();
    public List<PatientViewModel> getPatientList();
    public Task<int> AddUpdate(AppointmentViewModel model);
  }
}