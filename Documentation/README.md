# Templify - Платформа для создания и изучения курсов

## Описание проекта

Templify - это веб-платформа для создания, продажи и изучения онлайн-курсов. Проект построен на основе Clean Architecture с использованием ASP.NET Core 8.0.

## Архитектура проекта

Проект следует принципам Clean Architecture и состоит из следующих слоев:

### Core Layer
- **Templify.Domain** - Доменная модель, сущности и интерфейсы
- **Templify.Application** - Бизнес-логика, команды, запросы и валидация

### Infrastructure Layer
- **Templify.Infrastructure** - Внешние сервисы, Identity, Email
- **Templify.Persistence** - Доступ к данным, Entity Framework, репозитории

### Presentation Layer
- **Templifyy** - Веб-приложение (MVC)

### Shared Layer
- **Templify.Shared** - Общие константы, enum'ы, исключения

## Технологии

- **Backend**: ASP.NET Core 8.0, Entity Framework Core 8.0
- **Database**: PostgreSQL
- **Authentication**: ASP.NET Core Identity
- **Validation**: FluentValidation
- **CQRS**: MediatR
- **Email**: MailKit
- **Containerization**: Docker
- **Testing**: xUnit, FluentAssertions

## Структура проекта

```
Templifyy/
├── Core/
│   ├── Templify.Domain/           # Доменная модель
│   │   ├── Common/               # Базовые классы
│   │   ├── Entities/             # Сущности
│   │   └── Enums/                # Перечисления
│   └── Templify.Application/     # Бизнес-логика
│       ├── Features/             # Функциональность
│       ├── Interfaces/           # Интерфейсы
│       └── Extensions/           # Расширения
├── Infrastructure/
│   ├── Templify.Infrastructure/  # Внешние сервисы
│   │   ├── Services/             # Сервисы
│   │   └── Extensions/           # Расширения
│   └── Templify.Persistence/     # Доступ к данным
│       ├── Contexts/             # DbContext
│       ├── Configurations/       # Конфигурации EF
│       ├── Repositories/         # Репозитории
│       └── Extensions/           # Расширения
├── Shared/
│   └── Templify.Shared/          # Общие компоненты
│       ├── Constants/            # Константы
│       ├── Enums/                # Перечисления
│       ├── Exceptions/           # Исключения
│       └── Models/               # Общие модели
├── Templifyy/                     # Веб-приложение
│   ├── Areas/                    # Области
│   ├── Controllers/              # Контроллеры
│   ├── Views/                    # Представления
│   ├── Middleware/               # Промежуточное ПО
│   ├── Filters/                  # Фильтры
│   ├── Extensions/               # Расширения
│   ├── Helpers/                  # Вспомогательные классы
│   └── ViewModels/               # Модели представлений
├── Tests/                        # Тесты
│   ├── Templify.UnitTests/       # Модульные тесты
│   └── Templify.IntegrationTests/# Интеграционные тесты
├── Scripts/                      # Скрипты
├── Documentation/                 # Документация
└── docker-compose.yml            # Docker конфигурация
```

## Роли пользователей

1. **User** - Обычный пользователь, может покупать и изучать курсы
2. **Author** - Автор курсов, может создавать и продавать курсы
3. **Admin** - Администратор, полный доступ к системе

## Установка и запуск

### Предварительные требования

- .NET 8.0 SDK
- Docker Desktop
- PostgreSQL (через Docker)

### Запуск через Docker

1. Запустите Docker Desktop
2. Выполните скрипт:
   ```powershell
   .\Scripts\start-docker.ps1
   ```

### Запуск приложения

1. Восстановите пакеты NuGet:
   ```bash
   dotnet restore
   ```

2. Создайте миграцию базы данных:
   ```bash
   dotnet ef migrations add InitialCreate --project Infrastructure/Templify.Persistence
   ```

3. Примените миграцию:
   ```bash
   dotnet ef database update --project Infrastructure/Templify.Persistence
   ```

4. Запустите приложение:
   ```bash
   dotnet run --project Templifyy
   ```

## API Endpoints

### Authentication
- `POST /Auth/Register` - Регистрация пользователя
- `POST /Auth/Login` - Вход в систему
- `POST /Auth/Logout` - Выход из системы

### Courses
- `GET /Courses` - Список курсов
- `GET /Courses/{id}` - Детали курса
- `GET /Courses/Create` - Создание курса (Author, Admin)
- `POST /Courses/Create` - Создание курса (Author, Admin)

### Categories
- `GET /Categories` - Список категорий
- `GET /Categories/{id}` - Детали категории
- `GET /Categories/Create` - Создание категории (Admin)
- `POST /Categories/Create` - Создание категории (Admin)

## Тестирование

### Модульные тесты
```bash
dotnet test Tests/Templify.UnitTests
```

### Интеграционные тесты
```bash
dotnet test Tests/Templify.IntegrationTests
```

## Разработка

### Добавление новой функциональности

1. Создайте сущность в `Templify.Domain/Entities/`
2. Добавьте конфигурацию в `Templify.Persistence/Configurations/`
3. Создайте команды/запросы в `Templify.Application/Features/`
4. Добавьте валидацию в `Templify.Application/Features/*/Validators/`
5. Создайте обработчики в `Templify.Application/Features/*/Handlers/`
6. Добавьте сервис в `Templify.Infrastructure/Services/`
7. Создайте репозиторий в `Templify.Persistence/Repositories/`
8. Добавьте контроллер в `Templifyy/Controllers/`
9. Создайте представления в `Templifyy/Views/`

### Соглашения по именованию

- **Commands**: `{Action}{Entity}Command` (например, `CreateCourseCommand`)
- **Queries**: `Get{Entity}{Criteria}Query` (например, `GetCourseByIdQuery`)
- **Handlers**: `{Command}Handler` (например, `CreateCourseCommandHandler`)
- **Validators**: `{Command}Validator` (например, `CreateCourseCommandValidator`)
- **Services**: `I{Entity}Service` и `{Entity}Service`
- **Repositories**: `I{Entity}Repository` и `{Entity}Repository`

## Лицензия

Этот проект создан в образовательных целях.

## Контакты

Для вопросов по проекту обращайтесь к разработчику.
