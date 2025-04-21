import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { UserProfileDropdownComponent } from '../../../shared/componenets/user-profile-dropdown/user-profile-dropdown.component';

@Component({
  selector: 'app-top-panel-admin',
  standalone: true,
  imports: [RouterLink, CommonModule, UserProfileDropdownComponent],
  templateUrl: './top-panel-admin.component.html',
  styleUrls: ['./top-panel-admin.component.css']
})
export class TopPanelAdminComponent {}
