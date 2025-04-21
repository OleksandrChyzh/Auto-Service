import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './auth-modal.component.html',
  styleUrls: ['./auth-modal.component.css']
})
export class AuthModalComponent {
  @Output() close = new EventEmitter<void>();
  activeTab = 'login';

  constructor(private router: Router) {}

  closeModal(): void {
    this.close.emit();
  }

  setActiveTab(tab: 'login' | 'register'): void {
    this.activeTab = tab;
  }

  login(event: Event): void {
    event.preventDefault();
    const form = event.target as HTMLFormElement;
    const email = (form.querySelector('#email') as HTMLInputElement).value;
    const password = (form.querySelector('#password') as HTMLInputElement).value;

    // Умовне перенаправлення залежно від логіну
    if (email === 'master@gmail.com' && password === '123') {
      this.closeModal();
      this.router.navigate(['/app-master-page']);
    } else if (email === 'autoservice@gmail.com' && password === 'auto123') {
      // Перехід на сторінку адміністратора
      this.closeModal();
      this.router.navigate(['/app-admin-page']);
    } else {
      this.closeModal();
      this.router.navigate(['/app-client-page']);
    }
  }

  register(event: Event): void {
    event.preventDefault();
    const form = event.target as HTMLFormElement;
    const email = (form.querySelector('#reg-email') as HTMLInputElement).value;

    // Тут можна аналогічно: якщо майстер реєструється – кинути на його сторінку
    if (email === 'master@gmail.com') {
      this.closeModal();
      this.router.navigate(['/app-master-page']);
    } else {
      this.closeModal();
      this.router.navigate(['/app-client-page']);
    }
  }
}
