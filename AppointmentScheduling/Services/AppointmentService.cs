using AppointmentScheduling.Models.ViewModels;
using AppointmentScheduling.Models;
using AppointmentScheduling.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace AppointmentScheduling.Services
{
  public class AppointmentService : IAppointmentService
  {
    private readonly ApplicationDbContext _db;
    private readonly IEmailSender _emailSender;

    public AppointmentService(ApplicationDbContext db, IEmailSender emailSender)
    {
      _db = db;
      _emailSender = emailSender;
    }

    public async Task<int> AddUpdate(AppointmentViewModel model)
    {
      var startDate = DateTime.Parse(model.StartDate);
      var endDate = DateTime.Parse(model.StartDate).AddMinutes(Convert.ToDouble(model.Duration));
      var patient = _db.Users.FirstOrDefault( u => u.Id == model.PatientId);
      var doctor = _db.Users.FirstOrDefault ( u => u.Id == model.DoctorId);

      if (model != null && model.Id > 0)
      {
        return 1; // in case updated 
      }
      else
      {
        Appointment appointment = new Appointment()
        {
          Title = model.Title,
          Description = model.Description,
          StartDate = startDate,
          EndDate = endDate,
          Duration = model.Duration,
          DoctorId = model.DoctorId,
          PatientId = model.PatientId,
          IsDoctorApproved = false,
          AdminId = model.AdminId
        };
        await _emailSender.SendEmailAsync(doctor.Email, "Appointment Created",
            $"Your appointment with {patient.Name} is created and in pending status");

        await _emailSender.SendEmailAsync(patient.Email, "Appointment Created",
            $"Your appointment with {doctor.Name} is created and in pending status");

        _db.appointments.Add(appointment);

        await _db.SaveChangesAsync();
        return 2; // in case created 
      }
    }

        public async Task<int> ConfirmEvent(int id)
        {
            var appointment = _db.appointments.FirstOrDefault(x => x.Id == id);

            if (appointment != null)
            {
                appointment.IsDoctorApproved = true;
                return await _db.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> Delete(int id)
        {
            var appointment = _db.appointments.FirstOrDefault(x => x.Id == id);

            //var patient = _db.Users.FirstOrDefault(c => c.Id == appointment.PatientId);
            var doctor = _db.Users.FirstOrDefault(c => c.Id == appointment.DoctorId);

            if (appointment != null)
            {

                await _emailSender.SendEmailAsync(doctor.Email, "Appointment Deleted",
                 $"Your appointment is not avalible anymore.");


                _db.appointments.Remove(appointment);

                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public List<AppointmentViewModel> DoctorsEventsById(string doctorId)
    {
      return _db.appointments
      .Where(x => x.DoctorId == doctorId)
      .ToList()
      .Select(c => new AppointmentViewModel()
      {
        Id = c.Id,
        Description = c.Description,
        StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
        EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
        Title = c.Title,
        Duration = c.Duration,
        IsDoctorApproved = c.IsDoctorApproved
      }).ToList();
    }

        public AppointmentViewModel GetById(int id)
        {
            return _db.appointments.Where(x => x.Id == id).ToList().Select(c => new AppointmentViewModel()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                IsDoctorApproved = c.IsDoctorApproved,
                PatientId = c.PatientId,
                DoctorId = c.DoctorId,
                PatientName = _db.Users.Where(x => x.Id == c.PatientId).Select(x => x.Name).FirstOrDefault(),
                DoctorName = _db.Users.Where(x => x.Id == c.DoctorId).Select(x => x.Name).FirstOrDefault()
            }).SingleOrDefault();
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

    public List<AppointmentViewModel> PatientsEventsById(string patientId)
    {
      return _db.appointments
      .Where(x => x.PatientId == patientId)
      .ToList()
      .Select(c => new AppointmentViewModel()
      {
        Id = c.Id,
        Description = c.Description,
        StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
        Title = c.Title,
        Duration = c.Duration,
        IsDoctorApproved = c.IsDoctorApproved
      }).ToList();
    }
  }

}