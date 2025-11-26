import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [],
  template: `
    <div class="page-header">
      <h1>Dashboard</h1>
      <small>Welcome to your BoilerPlate app</small>
    </div>
    <p>You now have the exact same layout as ASP.NET Zero!</p>
  `,
  styles: [`
    .page-header h1 { margin: 0; font-size: 28px; }
    .page-header small { color: #666; }
  `]
})
export class DashboardComponent { }