import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { UserProfileDropdownComponent } from '../../../shared/componenets/user-profile-dropdown/user-profile-dropdown.component';

@Component({
  selector: 'app-top-panel-master',
  standalone: true,
  imports: [RouterLink, CommonModule, UserProfileDropdownComponent],
  templateUrl: './top-panel-master.component.html',
  styleUrl: './top-panel-master.component.css'
})
export class TopPanelMasterComponent {}
