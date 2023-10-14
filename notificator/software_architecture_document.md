# Технический проект "Сервис отправки оповещений"

## Текущая архитектура

В текущей архитектуре у нас есть мобильное приложение, которое общается с компонентом "Controller", а он в свою очередь делает запросы к "Foo" и "Bar".

![alt text](static/current_arch.svg)

## Целевая архитектура

### Диаграмма контекста (C1):
![C1](static/c1.png)

### Диаграмма контекста (C2):
![C2](static/c2.png)

### Диаграмма контекста (C3):
![C3_1](static/c3_1.png)
![C3_2](static/c3_2.png)
![C3_3](static/c3_3.png)

### Список решений (ADL):

| ID |  Дата | Статус | Участники | Решения |
| --- | --- | --- | --- | --- |
| [ADR-001](https://github.com/a-gataullin/architect-practice-tpl/blob/main/notificator/static/ADR-001.md) | 13.10.2023 | Принято | Гатауллин Артур | Использовать Nginx для API gateway |
| [ADR-002](https://github.com/a-gataullin/architect-practice-tpl/blob/main/notificator/static/ADR-002.md) | 13.10.2023 | Принято | Гатауллин Артур | Использовать Rabbit MQ для очереди сообщений |
| [ADR-003](https://github.com/a-gataullin/architect-practice-tpl/blob/main/notificator/static/ADR-003.md) | 13.10.2023 | Принято | Гатауллин Артур | Использовать Postgresql для хранилища настроек пользователя |
