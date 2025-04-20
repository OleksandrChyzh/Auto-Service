import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {TopPanelComponent} from './shared/componenets/top-panel/top-panel.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, TopPanelComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Frontend';
}
