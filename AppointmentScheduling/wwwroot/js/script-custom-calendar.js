document.addEventListener('DOMContentLoaded', function () {

  var calendar;
      try {
          var calendarEl = document.getElementById('calendar');
          if (calendarEl != null) {
              calendar = new FullCalendar.Calendar(calendarEl, {
                  initialView: 'dayGridMonth',
                  headerToolbar: {
                      left: 'prev,next,today',
                      center: 'title',
                      right: 'dayGridMonth,timeGridWeek,timeGridDay'
                  },
                  selectable: true,
                  editable: false
              });
              calendar.render();
          } else
              alert(calendarEl);

      }
      catch (e) {
          alert(e);
      }

  
});