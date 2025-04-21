import { Component } from '@angular/core';
import { TopPanelAdminComponent } from './top-panel-admin/top-panel-admin.component';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-admin-page',
  standalone: true,
  imports: [TopPanelAdminComponent, RouterOutlet],
  templateUrl: './admin-page.component.html',
  styleUrls: ['./admin-page.component.css']
})
export class AdminPageComponent {}
