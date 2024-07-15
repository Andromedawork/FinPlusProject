document.addEventListener('DOMContentLoaded', () => {
    const monthNames = ["Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"];
    const daysContainer = document.querySelector('.days');
    const monthYear = document.getElementById('monthYear');
    const prevMonthButton = document.getElementById('prevMonth');
    const nextMonthButton = document.getElementById('nextMonth');
    const selectedDateElem = document.getElementById('selectedDate');
    const hourList = document.getElementById('hourList');
    const entryModal = $('#entryModal');
    const entryForm = document.getElementById('entryForm');

    let currentMonth = new Date().getMonth();
    let currentYear = new Date().getFullYear();
    let selectedDate = null;
    let selectedHour = null;

    function updateCalendar() {
        daysContainer.innerHTML = '';
        const firstDayOfMonth = (new Date(currentYear, currentMonth, 1).getDay() || 7) - 1; // Понедельник - 0
        const daysInMonth = new Date(currentYear, currentMonth + 1, 0).getDate();

        monthYear.textContent = `${monthNames[currentMonth]} ${currentYear}`;

        // Дни предыдущего месяца
        const daysInPrevMonth = new Date(currentYear, currentMonth, 0).getDate();
        for (let i = firstDayOfMonth; i > 0; i--) {
            const day = document.createElement('div');
            day.textContent = daysInPrevMonth - i + 1;
            day.classList.add('prev-month');
            daysContainer.appendChild(day);
        }

        // Дни текущего месяца
        for (let i = 1; i <= daysInMonth; i++) {
            const day = document.createElement('div');
            day.textContent = i;
            day.dataset.date = new Date(Date.UTC(currentYear, currentMonth, i)).toISOString().split('T')[0];
            day.addEventListener('click', () => {
                selectedDate = day.dataset.date;
                selectedDateElem.textContent = selectedDate;
                updateHourList();
                document.querySelectorAll('.days div').forEach(d => d.classList.remove('selected'));
                day.classList.add('selected');
            });
            daysContainer.appendChild(day);
        }

        // Дни следующего месяца
        const totalDays = daysContainer.children.length;
        const daysToShowNextMonth = totalDays % 7 === 0 ? 0 : 7 - (totalDays % 7);
        for (let i = 1; i <= daysToShowNextMonth; i++) {
            const day = document.createElement('div');
            day.textContent = i;
            day.classList.add('next-month');
            daysContainer.appendChild(day);
        }
    }

    function updateHourList() {
        hourList.innerHTML = '';
        if (!selectedDate) return;

        // Fetch the records for the selected date
        fetch(`/Calendar/GetDropRecordByDate?date=${selectedDate}`)
            .then(response => response.json())
            .then(records => {
                for (let hour = 10; hour <= 18; hour++) {
                    const li = document.createElement('li');
                    li.textContent = `${hour}:00`;

                    // Check if there is a record for this hour
                    const record = records.find(r => new Date(r.time).getUTCHours() === hour);
                    if (record) {
                        li.textContent += ` - ${record.surname} ${record.name} моб. ${record.mobileNumber}`;
                    }

                    li.addEventListener('click', () => {
                        selectedHour = `${selectedDate}T${hour.toString().padStart(2, '0')}:00:00.000Z`;
                        entryModal.modal('show');
                    });

                    hourList.appendChild(li);
                }
            });
    }

    prevMonthButton.addEventListener('click', () => {
        if (currentMonth === 0) {
            currentMonth = 11;
            currentYear--;
        } else {
            currentMonth--;
        }
        updateCalendar();
    });

    nextMonthButton.addEventListener('click', () => {
        if (currentMonth === 11) {
            currentMonth = 0;
            currentYear++;
        } else {
            currentMonth++;
        }
        updateCalendar();
    });

    entryForm.addEventListener('submit', (e) => {
        e.preventDefault();
        const referalId = entryForm.referalId.value;
        const mobileNumber = entryForm.mobileNumber.value;
        const name = entryForm.name.value;
        const surname = entryForm.surname.value;
        const patronymic = entryForm.patronymic.value;

        const entryData = {
            record: {
                referalId,
                mobileNumber,
                name,
                surname,
                patronymic
            },
            selectedDate: selectedDate,
            selectedHour: selectedHour
        };

        fetch('/Calendar/AddDropRecord', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(entryData)
        }).then(response => {
            if (response.ok) {
                entryModal.modal('hide');
                entryForm.reset();
                updateHourList();
            } else {
                console.error('Error adding record');
            }
        });
    });

    updateCalendar();
});
