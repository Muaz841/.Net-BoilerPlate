import { Routes } from '@angular/router';

export const routes: Routes = [

    { path: '', redirectTo: 'auth/login', pathMatch: 'full' },

    //    {
    //     path: 'auth',
    //     loadChildren: () => import('./features/auth/auth.routes').then(m => m.authRoutes)
    //   },
    //   {
    //     path: 'dashboard',
    //     loadChildren: () => import('./features/dashboard/dashboard.routes').then(m => m.dashboardRoutes)
    //   },
    //   {
    //     path: 'users',
    //     loadChildren: () => import('./features/users/users.routes').then(m => m.usersRoutes)
    //   },
    //   {
    //     path: 'admin',
    //     loadChildren: () => import('./features/admin/admin.routes').then(m => m.adminRoutes)
    //   },
    
      { path: '**', redirectTo: 'auth/login' }
    
      
];
