﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduling.Utility
{
  public static class Helper
  {
    public const string Admin = "Admin";
    public static string Patient = "Patient";
    public static string Doctor = "Doctor";

    //messages default
    public static string appointmentAdded = "Appointment added successfully.";
    public static string appointmentUpdated = "Appointment updated successfully.";
    public static string appointmentDeleted = "Appointment deleted successfully.";
    public static string appointmentExists = "Appoinment for selected date and time already exists.";
    public static string appointmentNotExists = "Appointment not existis.";

        //confirm 
        public static string meetingConfirm = "Meeting confirm successfully.";
        public static string meetingConfirmError = "Error while confirming meeting.";

    //messages error
    public static string appointmentAddError = "something went wront, Please try again.";
    public static string appointmentUpdateError = "something went wront, Please try again.";
    public static string somethingWentWrong = "something went wront, Please try again.";
    public static int success_code = 1;
    public static int failure_code = 0;


    public static List<SelectListItem> GetRolesForDropDown(bool isAdmin)
    {
            if (isAdmin)
            {
                return new List<SelectListItem>
                {
                    new SelectListItem{ Value=Helper.Admin, Text=Helper.Admin }
                };
            }
            else
            {
              return new List<SelectListItem>
              {
                  new SelectListItem{ Value=Helper.Patient, Text=Helper.Patient },
                  new SelectListItem{ Value=Helper.Doctor, Text=Helper.Doctor },
              };
            }
    }

    public static List<SelectListItem> GetTimeDropDown()
    {
      int minute = 60;
      List<SelectListItem> duration = new List<SelectListItem>();
      for (int i = 1; i <= 12; i++)
      {
        duration.Add(new SelectListItem { Value = minute.ToString(), Text = i + " Hr " });
        minute = minute + 30;
        duration.Add(new SelectListItem { Value = minute.ToString(), Text = i + "hr 30 min" });
        minute = minute + 30;
      }
      return duration;
    }
  }
}
