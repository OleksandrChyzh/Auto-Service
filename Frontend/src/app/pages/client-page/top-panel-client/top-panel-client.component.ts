import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { UserProfileDropdownComponent } from '../../../shared/componenets/user-profile-dropdown/user-profile-dropdown.component';
@Component({
  selector: 'app-top-panel-client',
  standalone: true,
  imports: [RouterLink, CommonModule, UserProfileDropdownComponent],
  templateUrl: './top-panel-client.component.html',
  styleUrl: './top-panel-client.component.css'
})
export class TopPanelClientComponent {
  // Component logic here
}
