# StoreParser
Parser online stores

## Технологии
* ASP .NET CORE 2.1.1
* Bootstrap 4
* AngleSharp 0.9.9
* MsSql

## Описание
Проект парсит одну из групп товаров интернет магазина prodj.com.ua, а именно раздел "Студийные мониторы"
При запуске программы открывается стартовая страница, которая при первом запуске не содержит товаров.
Для запуска парсинга следует зайти по ссылке в верхнем навигационном меню: Admin и нажать Start parsing.
Парсер сохраняет в БД наименование товара, цену (с сохранением истории изменений), описание товара, 
картинки (как массив байтов).
После парсинга на главной странице появится список товаров. У кажного товара есть своя отдельная страница, гда можно увидеть историю изменения цены.
Парсер запускает фоновую задачу (IHostedService), которая:
* раз в сутки просматривает сайт; 
* если в магазине добавляются новые товары - добавляет их в БД;
* сравнивает цену, и если она изменилась дополняет историю изменения цен еще одной записью

## Что будет добавлено в ближайшее время (TODO) (не вложился в срок)
1. Разбиение не слои (проекты): DAL, BLL, Presentatoin
2. Применены паттерны repository, Unit Of Work
3. Тесты контроллера и другиех сущностей
4. API для работы с фронт-энд фреймворками
5. Пагинация главной страницы
6. Расширение функционала для парсинга интернет магазина целиком (рекурсивно через карту сайта)
