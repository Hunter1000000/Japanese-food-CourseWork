document.getElementById("registrationForm").addEventListener("submit", function(event) {
    event.preventDefault(); // Предотвращаем отправку формы

    var username = document.getElementById("username").value;
    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;

    // Проверяем валидность адреса электронной почты с помощью регулярного выражения
    var emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    if (!emailPattern.test(email)) {
        document.getElementById("emailError").textContent = "Неправильно введен адрес электронной почты";
        return;
    }

    // Очищаем сообщение об ошибке, если адрес введен правильно
    document.getElementById("emailError").textContent = "";

    console.log("Имя пользователя: " + username);
    console.log("Email: " + email);
    console.log("Пароль: " + password);

    // Здесь вы можете добавить код для отправки данных на сервер для регистрации
});