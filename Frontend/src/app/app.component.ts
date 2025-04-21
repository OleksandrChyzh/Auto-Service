import { Component } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { RouterOutlet } from '@angular/router';
import { TopPanelComponent } from './shared/componenets/top-panel/top-panel.component';
import { NgIf } from '@angular/common'; // ← додай це!
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, TopPanelComponent, NgIf],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'] // ← style**Urls**, а не styleUrl
})
export class AppComponent {
  title = 'Frontend';
  showTopPanel = true;

  constructor(private router: Router) {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe((event: any) => {
      // Перевіряємо URL, щоб не показувати панель на сторінках майстра або клієнта
      const excludeUrls = ['/app-client-page', '/app-master-page', 'app-admin-page'];
      this.showTopPanel = !excludeUrls.some(url => event.url.includes(url));
    });
  }
}
