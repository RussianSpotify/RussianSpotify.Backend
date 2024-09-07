# Russian Spotify Web Api

## 1. Как запустить проект

### Docker-Compose File:
```
В docker-compose файле настроен весь бэк, так что при запуске файла, вы сможете полностью протестировать бэкенд
1. Настроена база данных PostgresSQL
2. Настроен  Redis
3. Настроен Minio S3
4. Настроен приложение RussianSpotify.API
```

### База данных:
1. Скопируйте в secrets.json всю конфигурацию из appsettings.json и заменить строку подключения к бд

### Jwt:
1. Так же поменяйте на свои значения

## 2. WorkFlows
### Как называем ветки?
1. Если делаете фичу - feature/{название чего делаете}
2. Если фиксите баг - bugfix/{что фиксите}
3. Если что то срочное фиксите, например поленились тесты прогнать - hotfix/{что фиксите} 

## 3. Проверка кода на коррекность
Используйте комманды для запуска в терминале
```
1. dotnet build --no-restore /p:RunAnalyzersDuringBuild=true /p:TreatWarningsAsErrors=true
2. dotnet test --no-build --verbosity normal
```
![Видео без названия — сделано в Clipchamp (4)](https://github.com/user-attachments/assets/d619b193-c4f9-4b3c-8337-8e55bc311462)

Если у вас не вышли никакие ошибки - можете со спокойной душой лить свой пр к нам в репозиторий

## 4. Авторизация, Регистрация и Аутентификация

### Что тут происходит?

1. Заходим на сайт по ссылке https://console.cloud.google.com/welcome?project=russianspotify-434820
2. Далее переходим в раздел ![image](https://github.com/user-attachments/assets/2938550a-add9-4d92-adc2-ed59db9b68f6)
3. Создаем новый проект ![image](https://github.com/user-attachments/assets/102fa88a-ffce-4a75-a9d9-52a2e200ea67)
4. В данном разделе указываем Фронт URL и бэк URL ![image](https://github.com/user-attachments/assets/0d24325c-9123-4587-9be6-5bab2a81b8ba)
5. Проект создан
6. Далее заходим в раздел Credentials и во втором разделе OAuth 2.0 Client IDs выбираем наш проект
7. В ClientId вставляем ключ который будет у вас в OAuth 2.0 Client IDs и там же ClientSecret























