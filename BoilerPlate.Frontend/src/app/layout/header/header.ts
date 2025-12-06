import { Component, inject } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-header',
  imports: [],
  standalone : true,
  templateUrl: './header.html',
  styleUrls: ['./header.scss',]
})
export class HeaderComponent {
authservice = inject(AuthService)
  Logout()
  {
this.authservice.logout();
  }
}
