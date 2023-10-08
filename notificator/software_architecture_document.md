# Технический проект "Сервис отправки оповещений"

> Это фрагмент Технического проекта, который нужно заполнить в рамках практического задания темы "Технический проект".
---

## Текущая архитектура

В текущей архитектуре у нас есть мобильное приложение, которое общается с компонентом "Controller", а он в свою очередь делает запросы к "Foo" и "Bar".

![alt text](static/current_arch.svg)


## Целевая архитектура
https://firebase.google.com/docs/cloud-messaging/concept-options?hl=ru - push для андроида
https://web.archive.org/web/20150314162108/https://developer.apple.com/library/mac/documentation/NetworkingInternet/Conceptual/RemoteNotificationsPG/Chapters/ApplePushService.html - пуш для яблока
https://en.wikipedia.org/wiki/SMS#References - СМС

https://www.unisender.com/ru/features/email/tranzakcionnye-rassylki/ - сервис email-рассылок
https://smsc.ru/bulksms/?lnk=45&utm_source=yandex&utm_medium=cpc&utm_campaign=goryachie_rus_poisk%7C42171747%7Csearch&utm_content=gid%7C3763601785%7Caid%7C7282766743%7C16320445437_16320445437%7Cmain&utm_term=сервис%20отправить%20смс&pm_source=none&pm_block=premium&pm_position=1&keyword=сервис%20отправить%20смс&yclid=14063602064732192767 - сервис СМС-рассылки
https://sendsay.ru/features/mobilnye-push-uvedomleniya - сервис push-рассылки

### Диаграмма контекста (C1):

![C1](static/c1.svg)

Предполагается, что любой из существующих компонентов может отправить оповещение адресату. При этом существующие каналы связи и предпочтения по их использованию известны системе оповещений, компоненты остальной системы знать это не должны.

### Диаграмма контекста (C2):

![C2](static/c2.svg)

Для реализации подсистемы выбрана микровсервисная архитектура с использованием внешних сервисов для доставки оповещений клиентам. В подсистеме будет реализовано хранение необходимых данных (данные адресатов, настройки каналов оповещения), использование брокера сообщений для буферизации данных при массовых рассылках.
[ADR-001](adr-001.md)