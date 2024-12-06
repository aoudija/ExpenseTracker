import { Routes } from '@angular/router';
import { RegisterComponent } from './register/register.component';
import { ExpensesComponent } from './expenses/expenses.component';

export const routes: Routes = [
  { path: '', component: RegisterComponent },
  { path: 'expenses', component: ExpensesComponent },
];
