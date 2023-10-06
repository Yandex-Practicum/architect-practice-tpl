workspace {
    model {
        user = person "User"
        externalSystems = softwareSystem "External systems" "Foo and Bar" "ExternalSystems"
        smtpServer = softwareSystem "SMTP server" "" "SmtpServer"
        smsGateway = softwareSystem "SMS gateway" "" "SmsGateway"
        pushServer = softwareSystem "Push server" "" "PushServer"
        softwareSystem = softwareSystem "Software System" {
            apiGateway = container "ApiGateway" "Haproxy" {
                externalSystems -> this "http/json"
                user -> this "http/json"
            }
            api = container "Api" "" "ASP.NET Core" {
                settingsController = component "Settings Controller" "Контроллер CRUD для настроек пользователя" "Asp.Net Core Rest Controller"{
                    apiGateway -> this "http/json"
                }
                notifierController = component "Notifier Controller" "Контроллер отправки уведомлений" "Asp.Net Core Rest Controller"{
                    apiGateway -> this "http/json"
                }
                dbRepeaterApi = component "Repeater"
                databaseAgentApi = component "DataBase Agent" "Посредник работы с БД" ".NET Module" {
                    this -> dbRepeaterApi ""
                    settingsController -> this "Сохранение настроек пользователя"
                    notifierController -> this "Добавление уведомления в БД"
                }
            }
            queue = container "Queue" "Очередь уведомлений" "RabbitMq" "Queue"  {
                notifierController -> this "Запрос на уведомление"
            }
            backgroundService = container "Repeater" "Background Service" "Hangfire" {
                jobHangfire = component "Hangfire Job Executor"{
                }
                databaseAgentHangfire = component "DataBase Agent" "Посредник работы с БД" ".NET Module" {
                    jobHangfire -> this "Получение не обработанных уведомлений"
                }
                dbRepeaterHangfire = component "Repeater" {
                    databaseAgentHangfire -> this
                }
                rabbitMqRepeater = component "RabbitMq Repeater" {
                    this -> queue ""
                }
                rabbitMqClient = component "RabbitMq Client"{
                    jobHangfire -> this "Повторная отправка уведомлений в очередь"
                    this -> rabbitMqRepeater 
                }
            }
            emailNotifier = container "Email Notifier" "Background Service" ".net 6" {
                queueEmailConsumer = component "Слушатель очереди" "" ".net 6"{
                    queue -> this "Получение заявки на отправку Email"
                }
                emailValidator = component "Email валидатор" "" ".net 6"{
                    queueEmailConsumer -> this "Валидация Email уведомления"
                }
                emailRepeater = component "Отправитель Email" "" ".net 6"{
                    emailValidator -> this "Отправить Email"
                    this -> smtpServer "Отправка Email"
                }
            }
            pushNotifier = container "Push Notifier"  "Background Service" ".net 6" {
                queuePushConsumer = component "Слушатель очереди" "" ".net 6"{
                    queue -> this "Получение заявки на отправку push"
                }
                pushValidator = component "Push валидатор" "" ".net 6"{
                    queuePushConsumer -> this "Валидация смс уведомления"
                }
                pushRepeater = component "Отправитель push" "" ".net 6"{
                    pushValidator -> this "Отправить push"
                    this -> pushServer "Отправка push"
                }
            }
            smsNotifier = container "Sms Notifier" "Background Service" ".net 6" {
                queueSmsConsumer = component "Слушатель очереди" "" ".net 6"{
                    queue -> this "Получение заявки на отправку смс"z
                }
                smsValidator = component "Смс валидатор" "" ".net 6"{
                    queueSmsConsumer -> this "Валидация смс уведомления"
                }
                smsRepeater = component "Отправитель смс уведомлений" "" ".net 6"{
                    smsValidator -> this "Отправить смс"
                    this -> smsGateway "Отправка смс"
                }
                
            }
            database = container "Database" "Хранение уведомлений и настроек пользователей." "MariaDb" "Database" {
                dbRepeaterApi -> this "Retry when Error"
                dbRepeaterHangfire -> this "Получение не отправленных уведомлений"
                emailNotifier -> this "Получение настроек отправки"
                pushNotifier -> database "Получение настроек отправки"
                smsNotifier -> database "Получение настроек отправки"
            }
            
        }
        
    }

    views {
        systemContext softwareSystem {
            include *
            autolayout lr
        }

        container softwareSystem {
            include *
            autolayout lr
        }
        
        component api {
            include *
            autolayout lr
        }
        
        component backgroundService {
            include *
            autolayout lr
        }
        
        component smsNotifier {
            include *
            autolayout lr
        }
        
        component pushNotifier {
              include *
            autolayout lr
        }
        
         component emailNotifier {
              include *
            autolayout lr
        }

        theme default
         styles {
           element externalSystems{
               background #000000
           }
            element "Database" {
                shape Cylinder
            }
            element "ExternalSystems" {
                background #999999
                color #ffffff
            }
            element "SmtpServer" {
                background #999999
                color #ffffff
            }
            element "SmsGateway" {
                background #999999
                color #ffffff
            }
            element "PushServer" {
                background #999999
                color #ffffff
            }
            element "Queue" {
                shape Pipe
            }
        }
    }

}