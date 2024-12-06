import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms'; // Import FormsModule

@Component({
  selector: 'app-register',
  standalone: true,
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  imports: [FormsModule],
})
export class RegisterComponent {
  username: string = '';
  email: string = '';
  monthlyLimit: number = 0;

  constructor(private http: HttpClient, private router: Router) {}

  onRegister(event: Event) {
    event.preventDefault();

    const user = {
      username: this.username,
      email: this.email,
    };

    this.http.post('http://localhost:5165/api/user/register', user).subscribe({
      next: (response: any) => {
        const userId = response.id;

        const budget = {
          userId: userId,
          monthlyLimit: this.monthlyLimit,
        };

        this.http.post('http://localhost:5165/api/budget', budget).subscribe({
          next: (budgetResponse: any) => {
            this.router.navigate(['/expenses'], {
              state: {
                userId: userId,
                monthlyLimit: budgetResponse.monthlyLimit,
                currentExpenses: budgetResponse.currentExpenses,
              },
            });
          },
          error: (error) =>
            console.error('Error setting budget. Ensure API is reachable:', error),
        });
      },
      error: (error) =>
        console.error('Error registering user. Ensure API is reachable:', error),
    });
  }
}