# ExpenseTracker

**Expense Tracker with Budget Alerts**

---

## Features Implemented

- **User Registration**:
  - Users can register with a username and email.
  - After registration, users can set a monthly budget.

- **Expense Management**:
  - Add expenses with a name, category, and amount.
  - View a list of all expenses for a user.
  - Delete specific expenses from the list.

- **Budget Tracking**:
  - Users can set a monthly budget.
  - Displays the current budget usage with a progress bar.
  - Alerts users if they exceed the set budget using SignalR notifications.

---

## Setup Instructions

### Backend

1. **Clone the Repository**:
   ```bash
   git clone <repository_url>
   cd Expense-Tracker/backendApi
   ```
2.**Setup the Environment**:
  -Install .NET 8 SDK
3.**Install Dependencies**:
   ```bash
    dotnet restore
   ```
4.**Apply Database Migrations**:
   ```bash
    dotnet ef database update
   ```
5.**Run the Backend Server**:
   ```bash
    dotnet run
   ```
  -The backend will start on http://lcoalhost:5165.

6.**Test API Endpoints**:

  -Use Postman or any PAI tsting tool to interact with the endpoints.
  
  -Refer to controllers (UserController,ExpenseController,BudgetController) for available routes.


### Frontend


1.**Navigate to the Frontend Directory**:
   ```bash
    cd ../frotend
   ```
2.**Setup the Environement**:
   ```bash
    npm install -g @angular/cli
   ```
3.**Install Dependencies && run Frontend**:
   ```bash
    npm install && ng serve
   ```
4.Open browser at http://localhost:4200

---

## Assumptions and Limitations

### Backend
  -Fully implemented and tested. Users, expenses, and budget APIS are fucntional.
  
### Frontend
  -Work in progress:
    -Registration works, but the transition to managing is incomplete.
    -Budget and expenses are not fully integrated with backend.
  
### Database
  -SQLite is used for simplicity in development. Can be swithced to a production-ready database.
  
### Notification
  -SignalR is used for real-time budget alerts.

---

## Future Iprovements
  - Adding Authentication and authorization for securing user data.
  - Refactoring the frontend to ensure proper state management using services.
  - Enhancing UI/UX with additional styles and animations.
  - Extending API functionalities for more advanced budget analytics.

  
