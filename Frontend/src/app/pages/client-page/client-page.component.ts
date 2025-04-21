import { Component } from '@angular/core';
import { TopPanelClientComponent } from './top-panel-client/top-panel-client.component';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-client-page',
  standalone: true,
  imports: [TopPanelClientComponent, RouterOutlet],
  templateUrl: './client-page.component.html',
  styleUrl: './client-page.component.css'
})
export class ClientPageComponent {}
