import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-expenses',
  standalone: true,
  templateUrl: './expenses.component.html',
  styleUrls: ['./expenses.component.css'],
  imports: [FormsModule, CommonModule], // Include FormsModule and CommonModule
})
export class ExpensesComponent implements OnInit {
  expenseName: string = '';
  expenseCategory: string = '';
  expenseAmount: number = 0;
  expenses: any[] = [];
  userId: number = 0;
  monthlyLimit: number = 0;
  currentExpenses: number = 0;

  constructor(private http: HttpClient, private route: ActivatedRoute) {}

  ngOnInit(): void {
    const state = history.state;

    // Assign state data to variables
    this.userId = state.userId || 0;
    this.monthlyLimit = state.monthlyLimit || 0;
    this.currentExpenses = state.currentExpenses || 0;

    // Fetch expenses
    this.getExpenses();
  }

  addExpense(event: Event) {
    event.preventDefault();

    const expense = {
      name: this.expenseName,
      category: this.expenseCategory,
      amount: this.expenseAmount,
      userId: this.userId,
    };

    this.http.post('http://localhost:5165/api/expense', expense).subscribe({
      next: (response) => {
        console.log('Expense added:', response);
        this.getExpenses();
      },
      error: (error) =>
        console.error('Error adding expense. Ensure API is reachable:', error),
    });
  }

  getExpenses() {
    if (!this.userId) {
      console.error('No user ID available for fetching expenses.');
      return;
    }

    this.http
      .get(`http://localhost:5165/api/expense/user/${this.userId}`)
      .subscribe({
        next: (response: any) => {
          console.log('Expenses fetched:', response);
          this.expenses = response || [];
        },
        error: (error) =>
          console.error('Error fetching expenses. Ensure API is reachable:', error),
      });
  }

  deleteExpense(expenseId: number) {
    this.http.delete(`http://localhost:5165/api/expense/${expenseId}`).subscribe({
      next: () => {
        console.log('Expense deleted');
        this.getExpenses();
      },
      error: (error) =>
        console.error('Error deleting expense. Ensure API is reachable:', error),
    });
  }
}