import { Component } from '@angular/core';
import { TopPanelMasterComponent } from './top-panel-master/top-panel-master.component';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-master-page',
  standalone: true,
  imports: [TopPanelMasterComponent, RouterOutlet],
  templateUrl: './master-page.component.html',
  styleUrl: './master-page.component.css'
})
export class MasterPageComponent {}
