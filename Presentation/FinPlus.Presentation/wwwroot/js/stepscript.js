$(document).ready(function () {
    var userId = $('#Id').val();

    // Функция для проверки и выполнения действий по таймеру
    function checkTimer(formId, storageKey, hiddenClass, timeout, visibilityKey) {
        const endTime = localStorage.getItem(storageKey);

        if (endTime && Date.now() > endTime) {
            // Если время истекло, показываем элемент
            $(formId).removeClass(hiddenClass);
            localStorage.setItem(visibilityKey + userId, 'true');
            localStorage.removeItem(storageKey);
        } else {
            // Устанавливаем таймер, если время еще не истекло
            if (!endTime) {
                // Если нет времени окончания, устанавливаем новое время завершения
                const newEndTime = Date.now() + timeout;
                localStorage.setItem(storageKey, newEndTime);
            }

            // Устанавливаем таймер на оставшееся время
            setTimeout(function () {
                $(formId).removeClass(hiddenClass);
                localStorage.setItem(visibilityKey + userId, 'true');
                localStorage.removeItem(storageKey);
            }, endTime ? (endTime - Date.now()) : timeout);
        }
    }

    // Инициализация проверки состояний при загрузке страницы
    if (localStorage.getItem('thirdContactVisible_' + userId) === 'true') {
        $('#third-contact').removeClass('hidden');
    } else {
        checkTimer('#third-contact', 'thirdContactEndTime_' + userId, 'hidden', 15000, 'thirdContactVisible_');
    }

    if (localStorage.getItem('secondContactVisible_' + userId) === 'true') {
        $('#second-contact').removeClass('hidden');
    } else {
        checkTimer('#second-contact', 'secondContactEndTime_' + userId, 'hidden', 15000, 'secondContactVisible_');
    }

    $('#firstContactForm').on('submit', function (event) {
        event.preventDefault();
        var formData = $(this).serialize();
        console.log('Form data:', formData);

        $.ajax({
            type: 'POST',
            url: '/Client/AddClient',
            data: formData,
            dataType: 'json',
            success: function (response) {
                console.log('Response:', response);
                if (response.success) {
                    window.location.href = '/Client/Index';
                    const endTime = Date.now() + 15000; // 15 секунд
                    localStorage.setItem('secondContactEndTime_' + userId, endTime);
                } else {
                    console.error('Произошла ошибка при обновлении данных.');
                }
            },
            error: function (xhr, status, error) {
                console.error('Произошла ошибка:', error);
            }
        });
    });

    $('#secondContactForm').on('submit', function (event) {
        event.preventDefault();
        var formData = $(this).serialize();
        console.log('Form data:', formData);

        $.ajax({
            type: 'POST',
            url: '/Client/UpdateClientStep',
            data: formData,
            dataType: 'json',
            success: function (response) {
                console.log('Response:', response);
                if (response.success) {
                    $('#second-contact').addClass('disabled-form');
                    const endTime = Date.now() + 15000; // 15 секунд
                    localStorage.setItem('thirdContactEndTime_' + userId, endTime);
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
                console.log('Response:', response);
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
