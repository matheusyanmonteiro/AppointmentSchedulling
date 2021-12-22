using AppointmentScheduling.Services;
using AppointmentScheduling.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AppointmentScheduling.Controllers
{
  [Authorize]
  public class AppointmentController : Controller
  {
    private readonly IAppointmentService _appointmentService;
    public AppointmentController(IAppointmentService appointmentService)
    {
      _appointmentService = appointmentService;
    }
    //[Authorize(Roles = Helper.Admin)] if only  adm use this application
    public IActionResult Index()
    {
      ViewBag.Duration = Helper.GetTimeDropDown();
      ViewBag.DoctorList = _appointmentService.GetDoctorList();
      ViewBag.PatientList = _appointmentService.getPatientList();
      return View();
    }
  }
}