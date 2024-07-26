$(document).ready(function () {
    if (localStorage.getItem('thirdContactVisible') === 'true') {
        $('#third-contact').removeClass('hidden');
    }

    $('#secondContactForm').on('submit', function (event) {
        event.preventDefault();

        var formData = $(this).serialize();
        console.log('Form data:', formData); // Логируем данные формы

        $.ajax({
            type: 'POST',
            url: '/Client/UpdateClientStep',
            data: formData,
            dataType: 'json',
            success: function (response) {
                console.log('Response:', response); // Логируем ответ
                if (response.success) {
                    $('#second-contact').addClass('disabled-form');
                    localStorage.setItem('thirdContactVisible', 'true');

                    setTimeout(function () {
                        $('#third-contact').removeClass('hidden');
                    }, 15000);
                } else {
                    console.error('Произошла ошибка при обновлении данных.');
                }
            },
            error: function (xhr, status, error) {
                console.error('Произошла ошибка:', error);
            }
        });
    });
    $('#thirdContactForm').on('submit', function (event) {
        event.preventDefault();

        var formData = $(this).serialize();
        console.log('Form data:', formData);

        $.ajax({
            type: 'POST',
            url: '/Client/UpdateClientStep',
            data: formData,
            dataType: 'json',
            success: function (response) {
                console.log('Response:', response); // Логируем ответ
                if (response.success) {
                    $('#third-contact').addClass('disabled-form');
                } else {
                    console.error('Произошла ошибка при обновлении данных.');
                }
            },
            error: function (xhr, status, error) {
                console.error('Произошла ошибка:', error);
            }
        });
    });
});


